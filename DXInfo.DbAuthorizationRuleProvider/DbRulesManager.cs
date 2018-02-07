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
		/// <param name="databaseService">The Database Instance to use to query the data(Ҫ��ѯ���ݵ����ݿ�ʵ��)</param>
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
                throw new ProviderException("������ЧȨ��ʱ�����˴���Ȩ�ޱ��ʽ�Ƿ�");
            }

            if (rules.Length > 0)
            {
                //ɾ����ĩβ�Ķ���
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
                throw new ProviderException("Ȩ�������û���������Ϊ��");
            }
            
            string[] roles = Roles.GetRolesForUser(username);
            IPrincipal principal = new GenericPrincipal(new GenericIdentity(username), roles);

            AuthorizationRuleData rule = GetRule(ruleName);

            if (rule == null)
            {
                throw new ProviderException(string.Format("Ȩ��: '{0}'�������ݿ���",ruleName));
            }

            
            if ( IsInRule(principal,rule.Expression) )
            {
                throw new ProviderException(string.Format("�û�: '{0}'�Ѿ�ӵ��Ȩ��: '{1}'",username,ruleName));
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
                throw new ProviderException("Ȩ�������û���������Ϊ��");
            }
            
            string[] roles = Roles.GetRolesForUser(username);
            IPrincipal principal = new GenericPrincipal(new GenericIdentity(username), roles);

            Parser parser = new Parser();
            AuthorizationRuleData rule = GetRule(ruleName);

            if ( !parser.Parse(rule.Expression).Evaluate(principal) )
            {
                throw new ProviderException(string.Format("�û�: '{0}'�Ѿ�û��Ȩ��: '{1}'", username,ruleName));
            }

            string ruleExpression;
            
            //���û��Ѿ�ӵ���˴�Ȩ��
            if (rule.Expression.Contains(string.Format(" OR (I:{0})", username)))
            {
                ruleExpression = rule.Expression.Replace(string.Format(" OR (I:{0})", username), "");
            }
            //�����б��ʽ OR...
            else if (rule.Expression.Contains(string.Format("(I:{0}) OR ", username)))
            {
                ruleExpression = rule.Expression.Replace(string.Format("(I:{0}) OR ", username), "");

            }
            //�����б��ʽ AND...
            else if (rule.Expression.Contains(string.Format("(I:{0}) AND ", username)))
            {
                ruleExpression = rule.Expression.Replace(string.Format("(I:{0}) AND ", username), "");
            }
            //ֻ�д��û�ӵ�д�Ȩ��
            else if (rule.Expression.Contains(string.Format("(I:{0})", username)))
            {
                //ruleExpression = rule.Expression.Replace(string.Format("(I:{0})", username), "");
                throw new ProviderException("Ȩ�ޱ�����������һ����ɫ���û�!!!");
            }
            //ֻ�Ǵ��û������Ľ�ɫӵ�д�Ȩ��
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
                throw new ProviderException("Ȩ�����ͽ�ɫ��������Ϊ��");
            }
            
            string[] roles = new string[1] {roleName};
            IPrincipal principal = new GenericPrincipal( new GenericIdentity(""),roles );

            Parser parser = new Parser();
            AuthorizationRuleData rule = GetRule(ruleName);
            BooleanExpression parsedExpression;

            if (rule == null)
            {
                throw new ProviderException(string.Format("Ȩ��: '{0}'�������ݿ���", ruleName));
            }

            parsedExpression = parser.Parse(rule.Expression);
            if (parsedExpression.Evaluate(principal))
            {
                throw new ProviderException(string.Format("��ɫ: '{0}'�Ѿ�ӵ��Ȩ��: '{1}'", roleName, ruleName));
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
            catch (SyntaxException)//Ȩ�ޱ��ʽ�Ƿ�
            {
                throw new ApplicationException("Ȩ�ޱ��ʽ�Ƿ�");
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
                throw new ProviderException("Ȩ�����ͽ�ɫ��������Ϊ��");
            }
            
            IPrincipal principal;
            principal= new GenericPrincipal(new GenericIdentity(""), roles);

            Parser parser = new Parser();
            AuthorizationRuleData rule = GetRule(ruleName);

            if (!parser.Parse(rule.Expression).Evaluate(principal))
            {
                throw new ProviderException(string.Format("��ɫ: '{0}'�Ѿ�û��Ȩ��: '{1}'", roleName, ruleName));
            }

            string ruleExpression = string.Empty;
            int i = 0;

            //�����м�����ɫӵ�д�Ȩ��
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
                throw new ProviderException("ÿ��Ȩ������Ҫ����һ����ɫ!");
            }
            
            
            //�˽�ɫ�Ѿ�ӵ���˴�Ȩ��
            if (rule.Expression.Contains(string.Format(" OR (R:{0})", roleName)))
            {
                ruleExpression = rule.Expression.Replace(string.Format(" OR (R:{0})", roleName), "");
            }
            //�����б��ʽ OR...
            else if (rule.Expression.Contains(string.Format("(R:{0}) OR ", roleName)))
            {
                ruleExpression = rule.Expression.Replace(string.Format("(R:{0}) OR ", roleName), "");

            }
            //�����б��ʽ AND...
            else if (rule.Expression.Contains(string.Format("(R:{0}) AND ", roleName)))
            {
                ruleExpression = rule.Expression.Replace(string.Format("(R:{0}) AND ", roleName), "");
            }
            //ֻ�д˽�ɫӵ�д�Ȩ��
            //else if (rule.Expression.Contains(string.Format("(R:{0})", roleName)))
            //{
            //    //ruleExpression = rule.Expression.Replace(string.Format("(I:{0})", username), "");
            //    throw new ProviderException("Ȩ�ޱ�����������һ����ɫ���û�!!!");
            //}
            ////ֻ�Ǵ˽�ɫӵ�д�Ȩ��
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
                throw new ProviderException("Ҫɾ����Ȩ��������Ϊ��");
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
                throw new ArgumentException("Ȩ��������Ϊ��");
            }

            if (roles.Length == 0)
            {
                throw new ProviderException("����Ȩ��ʱ����ָ��Ȩ�޵�������ɫ");
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
