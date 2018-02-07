using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Security.Principal;
using System.Web.Security;

//using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.EnterpriseLibrary.Security;
//using Microsoft.Practices.EnterpriseLibrary.Security.Authorization;
using Microsoft.Practices.EnterpriseLibrary.Security.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
//using Microsoft.Practices.EnterpriseLibrary.Configuration;
using System.Configuration.Provider;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

namespace DXInfo
{
	/// <summary>
	/// Class for retrieving rules from the database
	/// </summary>
	public class DbRulesManager
	{

		private Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase dbRules = null;
		/// <summary>
		/// Creates a Database Rules Manager instance
		/// </summary>
		/// <param name="databaseService">The Database Instance to use to query the data(要查询数据的数据库实例)</param>
		/// <param name="config">The configuration context</param>
		public DbRulesManager(string databaseService)
		{
			//DatabaseProviderFactory factory = new DatabaseProviderFactory(config);
			dbRules = DatabaseFactory.CreateDatabase(databaseService) as SqlDatabase;
		}


		/// <summary>
		/// Retrieves a rule from the database
		/// </summary>
		/// <param name="Name">The name of the rule</param>
		/// <returns>An AuthorizationRuleData object</returns>
		public AuthorizationRuleData GetRule(string name)
		{
			
			AuthorizationRuleData rule = null;

			DbCommand cmd = dbRules.GetStoredProcCommand("dbo.GetRuleByName");
			dbRules.AddInParameter(cmd, "Name", DbType.String, name);
			
			using(IDataReader reader = dbRules.ExecuteReader(cmd))
			{
				if(reader.Read())
				{
					rule = GetRuleFromReader(reader);
				}
			}

			return rule;
		}

		private AuthorizationRuleData GetRuleFromReader(IDataReader reader)
		{
			AuthorizationRuleData rule = new AuthorizationRuleData();
			rule.Name = reader.GetString(reader.GetOrdinal("Name"));
			rule.Expression = reader.GetString(reader.GetOrdinal("Expression"));

			return rule;
		}

		
        ///// <summary>
        ///// Retrieves all rules in the database as a DataSet
        ///// </summary>
        ///// <returns>A DataSet containing all of the rules</returns>
        //public DataSet GetAllRules()
        //{
        //    DbCommand cmd = dbRules.GetStoredProcCommand("dbo.GetAllRules");

        //    using(DataSet ds = dbRules.ExecuteDataSet(cmd))
        //    {
        //        return ds;
        //    }
		//}


		/// <summary>
		/// Retrieves all rules in the database as a Collection
		/// </summary>
		/// <returns>An AuthorizationRuleDataCollection containing all of the rules</returns>
		public List<AuthorizationRuleData> GetAllRulesAsCollection()
		{
			List<AuthorizationRuleData> rules = new List<AuthorizationRuleData>();

			DbCommand cmd = dbRules.GetStoredProcCommand("dbo.GetAllRules");
 
			using(IDataReader reader = dbRules.ExecuteReader(cmd))
			{
				while(reader.Read())
				{
					AuthorizationRuleData rule = GetRuleFromReader(reader);
					rules.Add(rule);
				}
			}
			return rules;
		}

		/// <summary>
		/// Inserts a rule into the database
		/// </summary>
		/// <param name="name">The name of the rule</param>
		/// <param name="expression">The expression defining the rule</param>
		public void InsertRule(string name, string expression,string description)
		{
			DbCommand cmd = dbRules.GetStoredProcCommand("dbo.InsertRule");
			dbRules.AddInParameter(cmd, "Name", DbType.String, name);
			dbRules.AddInParameter(cmd, "Expression", DbType.String, expression);
            //dbRules.AddInParameter(cmd, "Description",DbType.String, description);

			dbRules.ExecuteNonQuery(cmd);
		}

		/// <summary>
		/// Saves the rule to the database
		/// </summary>
		/// <param name="ruleId">The Rule Id</param>
		/// <param name="name">The name of the rule</param>
		/// <param name="expression">The expression</param>
		public void UpdateRuleById(int ruleId, string name, string expression)
		{
			DbCommand cmd = dbRules.GetStoredProcCommand("dbo.UpdateRuleById");
            dbRules.AddInParameter(cmd, "id", DbType.Int32, ruleId);
            dbRules.AddInParameter(cmd, "Name", DbType.String, name);
            dbRules.AddInParameter(cmd, "Expression",  DbType.String, expression);
            //dbRules.AddInParameter(cmd, "Description", DbType.String, description);

			dbRules.ExecuteNonQuery(cmd);
		}

		/// <summary>
		/// Removes a rule from the database
		/// </summary>
		/// <param name="ruleId">The ruleid to remove</param>
		public void DeleteRuleById(Guid ruleId)
		{
			DbCommand cmd = dbRules.GetStoredProcCommand("dbo.DeleteRuleById");
			dbRules.AddInParameter(cmd, "RoleId", SqlDbType.UniqueIdentifier, ruleId);

			dbRules.ExecuteNonQuery(cmd);
		}


        /***************** Follow Function Created by levinknight 2006.06.07 *****************/

        #region GetAllRules
        public string[] GetAllRules()
        {
            string rules = string.Empty;
            DbCommand cmd = dbRules.GetStoredProcCommand("dbo.GetAllRules");

            using (DataSet ds = dbRules.ExecuteDataSet(cmd))
            {
                foreach (DataRow rule in ds.Tables[0].Rows)
                {
                    rules += (string)rule["Name"] + ",";
                }

                if (rules.Length >0)
                {
                    rules = rules.Substring(0,rules.Length -1);
                    return rules.Split(',');
                }

                return new string[0];
            }
        }
        #endregion

        #region GetRulesForUser by IPrincipal
        public string[] GetRulesForUser(IPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentException("Principal cannot be null.");
            }

            return GetEffectiveRules(principal);
        }
        #endregion

        #region GetRulesForuser by Username
        public string[] GetRulesForUser(string username)
        {
            string[] roles = Roles.GetRolesForUser(username);
            IPrincipal principal = new GenericPrincipal(new GenericIdentity(username),roles);

            return GetEffectiveRules(principal);
        }
        #endregion

        #region GetRulesForRole by Role'Name
        public string[] GetRulesForRole(string rolename)
        {
            string[] roles = new string[1]{rolename};
            IPrincipal principal = new GenericPrincipal(new GenericIdentity(""), roles);

            return GetEffectiveRules(principal);
        }
        #endregion

        #region GetEffectiveRules Service for GetRulesFor User or Role
        private string[] GetEffectiveRules(System.Security.Principal.IPrincipal principal)
        {
            string rules = "";
            List<AuthorizationRuleData> ruleCollection = GetAllRulesAsCollection();

            try
            {
                foreach (AuthorizationRuleData rule in ruleCollection)
                {
                    if ( IsInRule(principal,rule.Expression) )
                    {
                        rules += rule.Name + ",";
                    }
                }
            }
            catch (SyntaxException)
            {
                throw new ProviderException("返回有效权限时发生了错误，权限表达式非法");
            }

            if (rules.Length > 0)
            {
                //删除最末尾的逗号
                rules = rules.Substring(0,rules.Length - 1);
                return rules.Split(',');
            }

            return new string[0];
        }
        #endregion

        #region AddUserToRule

        public void AddUserToRule(string ruleName,string username)
        {
            if (ruleName.Length == 0 || username.Length == 0)
            {
                throw new ProviderException("权限名和用户名都不能为空");
            }
            
            string[] roles = Roles.GetRolesForUser(username);
            IPrincipal principal = new GenericPrincipal(new GenericIdentity(username), roles);

            AuthorizationRuleData rule = GetRule(ruleName);

            if (rule == null)
            {
                throw new ProviderException(string.Format("权限: '{0}'不在数据库中",ruleName));
            }

            
            if ( IsInRule(principal,rule.Expression) )
            {
                throw new ProviderException(string.Format("用户: '{0}'已经拥有权限: '{1}'",username,ruleName));
            }

            string ruleExpression = string.Empty;
            string tempExpression = string.Empty;

            if (rule.Expression.Contains(string.Format(" AND (NOT I:{0})", username)))
            {
                tempExpression = ruleExpression = rule.Expression.Replace(string.Format(" AND (NOT I:{0})", username), "");
                if (IsInRule(principal, tempExpression))
                {
                    ruleExpression = rule.Expression.Replace(string.Format(" AND (NOT I:{0})", username), "");
                }
                else
                {
                    ruleExpression = rule.Expression.Replace(string.Format(" AND (NOT I:{0})", username),
                        string.Format(" OR (I:{0})", username)
                        );
                }
            }
            else if (rule.Expression.Contains(string.Format("(NOT I:{0})", username)))
            {
                ruleExpression = rule.Expression.Replace(string.Format("(NOT I:{0})", username),
                    string.Format("(I:{0})", username)
                    );
            }
            else
            {
                ruleExpression = rule.Expression + string.Format(" OR (I:{0})", username);
            }

            try
            {
                new Parser().Parse(ruleExpression);
            }
            catch (SyntaxException)
            {
                throw;
            }

            UpdateRuleByName(rule.Name,ruleExpression);
        }
       
        #endregion

        #region RemoveUserFromRule
        public void RemoveUserFromRule(string ruleName, string username)
        {
            if (ruleName.Length == 0 || username.Length == 0)
            {
                throw new ProviderException("权限名和用户名都不能为空");
            }
            
            string[] roles = Roles.GetRolesForUser(username);
            IPrincipal principal = new GenericPrincipal(new GenericIdentity(username), roles);

            Parser parser = new Parser();
            AuthorizationRuleData rule = GetRule(ruleName);

            if ( !parser.Parse(rule.Expression).Evaluate(principal) )
            {
                throw new ProviderException(string.Format("用户: '{0}'已经没有权限: '{1}'", username,ruleName));
            }

            string ruleExpression;
            
            //此用户已经拥有了此权限
            if (rule.Expression.Contains(string.Format(" OR (I:{0})", username)))
            {
                ruleExpression = rule.Expression.Replace(string.Format(" OR (I:{0})", username), "");
            }
            //后面有表达式 OR...
            else if (rule.Expression.Contains(string.Format("(I:{0}) OR ", username)))
            {
                ruleExpression = rule.Expression.Replace(string.Format("(I:{0}) OR ", username), "");

            }
            //后面有表达式 AND...
            else if (rule.Expression.Contains(string.Format("(I:{0}) AND ", username)))
            {
                ruleExpression = rule.Expression.Replace(string.Format("(I:{0}) AND ", username), "");
            }
            //只有此用户拥有此权限
            else if (rule.Expression.Contains(string.Format("(I:{0})", username)))
            {
                //ruleExpression = rule.Expression.Replace(string.Format("(I:{0})", username), "");
                throw new ProviderException("权限必须属于至少一个角色或用户!!!");
            }
            //只是此用户所属的角色拥有此权限
            else
            {
                ruleExpression = rule.Expression + string.Format(" AND (NOT I:{0})", username);
            }

            UpdateRuleByName(ruleName,ruleExpression);
        }
        #endregion

        #region AddRoleToRule
        public void AddRoleToRule(string ruleName,string roleName)
        {
            if (ruleName.Length == 0 || roleName.Length ==0)
            {
                throw new ProviderException("权限名和角色名都不能为空");
            }
            
            string[] roles = new string[1] {roleName};
            IPrincipal principal = new GenericPrincipal( new GenericIdentity(""),roles );

            Parser parser = new Parser();
            AuthorizationRuleData rule = GetRule(ruleName);
            BooleanExpression parsedExpression;

            if (rule == null)
            {
                throw new ProviderException(string.Format("权限: '{0}'不在数据库中", ruleName));
            }

            parsedExpression = parser.Parse(rule.Expression);
            if (parsedExpression.Evaluate(principal))
            {
                throw new ProviderException(string.Format("角色: '{0}'已经拥有权限: '{1}'", roleName, ruleName));
            }

            string ruleExpression = string.Empty;

            if (rule.Expression.Contains(string.Format(" AND (NOT R:{0})", roleName)))
            {
                ruleExpression = rule.Expression.Replace(string.Format(" AND (NOT R:{0})", roleName),
                    string.Format(" OR (R:{0})", roleName)
                    );
            }
            else
            {
                ruleExpression = rule.Expression + string.Format(" OR (R:{0})", roleName);
            }

            ruleExpression = rule.Expression + string.Format(" OR (R:{0})", roleName);

            try
            {
                parser.Parse(ruleExpression);
            }
            catch (SyntaxException)//权限表达式非法
            {
                throw new ApplicationException("权限表达式非法");
            }

            UpdateRuleByName(rule.Name, ruleExpression);

        }
        #endregion

        #region RemoveRoleFromRule
        public void RemoveRoleFromRule(string ruleName,string roleName)
        {
            string[] roles;
            roles = new string[1] { roleName };

            if (ruleName.Length == 0 || roleName.Length == 0)
            {
                throw new ProviderException("权限名和角色名都不能为空");
            }
            
            IPrincipal principal;
            principal= new GenericPrincipal(new GenericIdentity(""), roles);

            Parser parser = new Parser();
            AuthorizationRuleData rule = GetRule(ruleName);

            if (!parser.Parse(rule.Expression).Evaluate(principal))
            {
                throw new ProviderException(string.Format("角色: '{0}'已经没有权限: '{1}'", roleName, ruleName));
            }

            string ruleExpression = string.Empty;
            int i = 0;

            //计算有几个角色拥有此权限
            foreach (string role in Roles.GetAllRoles())
            {
                roles[0] = role;
                principal = new GenericPrincipal(new GenericIdentity(""),roles);

                if (parser.Parse(rule.Expression).Evaluate(principal))
                {
                    i++;
                }
            }

            if (i < 2)
            {
                throw new ProviderException("每个权限至少要属于一个角色!");
            }
            
            
            //此角色已经拥有了此权限
            if (rule.Expression.Contains(string.Format(" OR (R:{0})", roleName)))
            {
                ruleExpression = rule.Expression.Replace(string.Format(" OR (R:{0})", roleName), "");
            }
            //后面有表达式 OR...
            else if (rule.Expression.Contains(string.Format("(R:{0}) OR ", roleName)))
            {
                ruleExpression = rule.Expression.Replace(string.Format("(R:{0}) OR ", roleName), "");

            }
            //后面有表达式 AND...
            else if (rule.Expression.Contains(string.Format("(R:{0}) AND ", roleName)))
            {
                ruleExpression = rule.Expression.Replace(string.Format("(R:{0}) AND ", roleName), "");
            }
            //只有此角色拥有此权限
            //else if (rule.Expression.Contains(string.Format("(R:{0})", roleName)))
            //{
            //    //ruleExpression = rule.Expression.Replace(string.Format("(I:{0})", username), "");
            //    throw new ProviderException("权限必须属于至少一个角色或用户!!!");
            //}
            ////只是此角色拥有此权限
            //else
            //{
            //    ruleExpression = rule.Expression + string.Format(" AND (NOT I:{0})", roleName);
            //}

            UpdateRuleByName(ruleName, ruleExpression);
        }
        #endregion

        #region UpdateRuleByName
        private void UpdateRuleByName(string ruleName,string ruleExpression)
        {
			DbCommand cmd = dbRules.GetStoredProcCommand("dbo.UpdateRuleByName");
            dbRules.AddInParameter(cmd, "Name", DbType.String, ruleName);
            dbRules.AddInParameter(cmd, "Expression", DbType.String, ruleExpression);

			dbRules.ExecuteNonQuery(cmd);
        }
        #endregion

        #region DeleteRuleByName
        public void DeleteRuleByName(string ruleName)
        {
            if (ruleName.Length == 0)
            {
                throw new ProviderException("要删除的权限名不能为空");
            }
            
            DbCommand cmd = dbRules.GetStoredProcCommand("dbo.DeleteRuleByName");
            dbRules.AddInParameter(cmd, "Name", DbType.String, ruleName);

            dbRules.ExecuteNonQuery(cmd);
        }
        #endregion

        #region CreateRule
        public void CreateRule(string ruleName,string description,string[] roles)
        {
            string ruleExpression;
            string roleRules = string.Empty;
            //string userRules = string.Empty;
            
            if (ruleName == null)
            {
                throw new ArgumentException("权限名不能为空");
            }

            if (roles.Length == 0)
            {
                throw new ProviderException("创建权限时必须指明权限的所属角色");
            }

            if (roles.Length > 0)
            {
                foreach (string role in roles)
                {
                    roleRules += string.Format("(R:{0}) OR ",role);
                }
                
                if (roles.Rank > 0)
                {
                    roleRules = roleRules.Substring(0, roleRules.Length - 4);
                }
            }

            ruleExpression = roleRules;

            try
            {
                new Parser().Parse(ruleExpression);
            }
            catch (SyntaxErrorException)
            {
                throw;
            }
            
            InsertRule(ruleName,ruleExpression,description);
        }
        #endregion

        #region IsInRule
        private bool IsInRule(IPrincipal principal,string ruleExpression)
        {
            Parser parser = new Parser();
            BooleanExpression parsedExpression;

            try
            {
                parsedExpression = parser.Parse(ruleExpression);
            }
            catch (SyntaxException)
            {
                throw;   
            }

            if (parsedExpression.Evaluate(principal))
            {
                return true;
            }

            return false;
        }
        #endregion
    }
}
