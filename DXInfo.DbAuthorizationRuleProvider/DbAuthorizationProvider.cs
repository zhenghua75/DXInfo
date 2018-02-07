using System;
using System.Configuration;
//using Microsoft.Practices.EnterpriseLibrary.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Security;
using Microsoft.Practices.EnterpriseLibrary.Security.Instrumentation;
//using Microsoft.Practices.EnterpriseLibrary.Security.Authorization;
using Microsoft.Practices.EnterpriseLibrary.Security.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;


namespace DXInfo
{
    /// <summary>
    /// Authentication provider for rules stored in a database table.
    /// </summary>
    /// <remarks>
    /// This provider uses the same Authentication rules provided in the default
    /// AuthorizationRuleProvider, except it stores them in a database table rather than
    /// the configuration Xml file.
    /// 
    /// </remarks>
    /// 
    [ConfigurationElementType(typeof(CustomAuthorizationProviderData))]
    public class DbAuthorizationProvider : AuthorizationProvider
    {

        public DbAuthorizationProvider()
        {
            // No constructor logic needed
        }

        public DbAuthorizationProvider(string database)
        {
            this.database = database;
        }

        public DbAuthorizationProvider(System.Collections.Specialized.NameValueCollection config)
            : this(config["database"])
        {
            
        }

        private string database;
        private DbRulesManager mgr = null;

        #region IAuthorizationProvider Members

        /// <summary>
        /// Checks a user's authorization against a given rule
        /// </summary>
        /// <param name="principal">The user to authorize</param>
        /// <param name="context">The name of the rule to check</param>
        /// <returns>boolean indicating whether the user is authorized</returns>
        public override bool Authorize(System.Security.Principal.IPrincipal principal, string context)
        {
            if (principal == null)
            {
                throw new ArgumentNullException("principal");
            }
            if (context == null || context.Length == 0)
            {
                throw new ArgumentNullException("context");
            }

            if (mgr == null)
            {
                mgr = new DbRulesManager(database);
            }

            //SecurityAuthorizationCheckEvent.Fire(principal.Identity.Name, context);
            InstrumentationProvider.FireAuthorizationCheckPerformed(principal.Identity.Name, context);

            BooleanExpression expression = GetParsedExpression(context, mgr);
            if (expression == null)
            {
                //todo : better exception
                throw new ApplicationException(String.Format("Authorization Rule {0} not found in the database.", context));
            }

            bool result = expression.Evaluate(principal);

            if (result == false)
            {
                //SecurityAuthorizationFailedEvent.Fire(principal.Identity.Name, context);
                InstrumentationProvider.FireAuthorizationCheckFailed(principal.Identity.Name, context);
            }
            return result;

        }

        #endregion

        /// <summary>
        /// Retrieves a rule from the database and parses it into a boolean expresssion
        /// </summary>
        /// <param name="context">The Rule name</param>
        /// <param name="mgr">The Database Rules Manager that queries the database</param>
        /// <returns>A BooleanExpression object</returns>
        private BooleanExpression GetParsedExpression(string context, DbRulesManager mgr)
        {

            AuthorizationRuleData rule = mgr.GetRule(context);
            if (rule == null)
            {
                return null;
            }

            Parser p = new Parser();
            return p.Parse(rule.Expression);

        }

    }
}
