//===============================================================================
// Microsoft Data Access Application Block for .NET
// 
//
// SQLHelper.cs
//
// This file contains the implementations of the OLEDBHelper class.
//
// 
//===============================================================================
// Copyright (C) 2000-2001 Microsoft Corporation
// All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR
// FITNESS FOR A PARTICULAR PURPOSE.
//==============================================================================

using System;
using System.Data;
using System.Xml;
using System.Data.SqlClient;
using System.Collections;

namespace DataAccess
{
	/// <summary>
	/// this enum is used to indicate weather the connection was provided by the caller, or created by SqlHelper, so that
	/// we can set the appropriate CommandBehavior when calling ExecuteReader()
	/// </summary>
	internal enum ConnectionOwnership	
	{
		/// <summary>Connection is owned and managed by OLEDBHelper</summary>
		Internal, 
		/// <summary>Connection is owned and managed by the caller</summary>
		External
	}

	/// <summary>
	/// The SqlHelper class is intended to encapsulate high performance, scalable best practices for 
	/// common uses of SqlClient.
	/// </summary>


	public sealed class SqlHelper
	{
		#region private utility methods & constructors

		//Since this class provides only static methods, make the default constructor private to prevent 
		//instances from being created with "new SqlHelper()".
		private SqlHelper()
		{
			
		}

		/// <summary>
		/// This method is used to attach array's of SqlParameters to a SqlCommand.
		/// 
		/// This method will assign a value of DbNull to any parameter with a direction of
		/// InputOutput and a value of null.  
		/// 
		/// This behavior will prevent default values from being used, but
		/// this will be the less common case than an intended pure output parameter (derived as InputOutput)
		/// where the user provided no input value.
		/// </summary>
		/// <param name="command">The command to which the parameters will be added</param>
		/// <param name="commandParameters">an array of SqlParameters tho be added to command</param>
		private static void AttachParameters(SqlCommand command, SqlParameter[] commandParameters)
		{
			foreach (SqlParameter p in commandParameters)
			{
				//check for derived output value with no value assigned
				if ((p.Direction == ParameterDirection.InputOutput) && (p.Value == null))
				{
					p.Value = DBNull.Value;
				}
				
				command.Parameters.Add(p);
			}
		}

		/// <summary>
		/// This method assigns an array of values to an array of SqlParameters.
		/// </summary>
		/// <param name="commandParameters">array of SqlParameters to be assigned values</param>
		/// <param name="parameterValues">array of objects holding the values to be assigned</param>
		private static void AssignParameterValues(SqlParameter[] commandParameters, object[] parameterValues)
		{
			if ((commandParameters == null) || (parameterValues == null)) 
			{
				//do nothing if we get no data
				return;
			}

			// we must have the same number of values as we pave parameters to put them in
			if (commandParameters.Length != parameterValues.Length)
			{
				throw new ArgumentException("Parameter count does not match Parameter Value count.");
			}

			//iterate through the SqlParameters, assigning the values from the corresponding position in the 
			//value array
			for (int i = 0, j = commandParameters.Length; i < j; i++)
			{
				commandParameters[i].Value = parameterValues[i];
			}
		}

		/// <summary>
		/// This method opens (if necessary) and assigns a connection, transaction, command type and parameters 
		/// to the provided command.
		/// </summary>
		/// <param name="command">the SqlCommand to be prepared</param>
		/// <param name="connection">a valid SqlConnection, on which to execute this command</param>
		/// <param name="transaction">a valid SqlTransaction, or 'null'</param>
		/// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
		/// <param name="commandText">the stored procedure name or T-SQL command</param>
		/// <param name="commandParameters">an array of SqlParameters to be associated with the command or 'null' if no parameters are required</param>
		private static void PrepareCommand(SqlCommand command, SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, SqlParameter[] commandParameters)
		{
			//if the provided connection is not open, we will open it
			if (connection.State != ConnectionState.Open)
			{
				connection.Open();
			}

			//associate the connectoin with the command
			command.Connection = connection;

			//set the command text (stored procedure name or SQL statement)
			command.CommandText = commandText;

			//set the command timeout
			command.CommandTimeout = 360; //Modified by sundh 2005-1-6

			//if we were provided a transaction, assign it.
			if (transaction != null)
			{
				command.Transaction = transaction;
			}

			//set the command type
			command.CommandType = commandType;

			//attach the command parameters if they are provided
			if (commandParameters != null)
			{
				AttachParameters(command, commandParameters);
			}

			return;
		}


		#endregion private utility methods & constructors

		#region ExecuteNonQuery
		/// <summary>
		/// Execute a SqlCommand (that returns no resultset and takes no parameters) against the provided SqlConnection. 
		/// </summary>
		/// <remarks>
		/// e.g.:  
		///  int result = ExecuteNonQuery(conn, CommandType.StoredProcedure, "PublishOrders");
		/// </remarks>
		/// <param name="connection">a valid SqlConnection</param>
		/// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
		/// <param name="commandText">the stored procedure name or T-SQL command</param>
		/// <returns>an int representing the number of rows affected by the command</returns>
		public static int ExecuteNonQuery(SqlConnection connection, CommandType commandType, string commandText)
		{
			//pass through the call using a null transaction value
			return ExecuteNonQuery(connection, null, commandType, commandText);
		}

		/// <summary>
		/// Execute a SqlCommand (that returns no resultset) against the specified SqlConnection 
		/// using the provided parameters.
		/// </summary>
		/// <remarks>
		/// e.g.:  
		///  int result = ExecuteNonQuery(conn, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
		/// </remarks>
		/// <param name="connection">a valid SqlConnection</param>
		/// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
		/// <param name="commandText">the stored procedure name or T-SQL command</param>
		/// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
		/// <returns>an int representing the number of rows affected by the command</returns>
		public static int ExecuteNonQuery(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
		{	
			//pass through the call using a null transaction value
			return ExecuteNonQuery(connection, null, commandType, commandText, commandParameters);
		}

		/// <summary>
		/// Execute a stored procedure via a SqlCommand (that returns no resultset) against the specified SqlConnection 
		/// using the provided parameter values.  This method will query the database to discover the parameters for the 
		/// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
		/// </summary>
		/// <remarks>
		/// This method provides no access to output parameters or the stored procedure's return value parameter.
		/// 
		/// e.g.:  
		///  int result = ExecuteNonQuery(conn, "PublishOrders", 24, 36);
		/// </remarks>
		/// <param name="connection">a valid SqlConnection</param>
		/// <param name="spName">the name of the stored prcedure</param>
		/// <param name="parameterValues">an array of objects to be assigned as the input values of the stored procedure</param>
		/// <returns>an int representing the number of rows affected by the command</returns>
		public static int ExecuteNonQuery(SqlConnection connection, string spName, params object[] parameterValues)
		{
			//pass through the call using a null transaction value
			return ExecuteNonQuery(connection, null, spName, parameterValues);
		}

		//these three method overloads currently take both connection and transaction.  In post-beta2 builds, only 
		//transaction will need to be passed in, and the .Connection property will be available from that transaction

		/// <summary>
		/// Execute a SqlCommand (that returns no resultset and takes no parameters) against the provided SqlConnection
		/// and SqlTransaction. 
		/// </summary>
		/// <remarks>
		/// e.g.:  
		///  int result = ExecuteNonQuery(conn, trans, CommandType.StoredProcedure, "PublishOrders");
		/// </remarks>
		/// <param name="connection">a valid SqlConnection</param>
		/// <param name="transaction">a valid SqlTransaction associated with the connection</param>
		/// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
		/// <param name="commandText">the stored procedure name or T-SQL command</param>
		/// <returns>an int representing the number of rows affected by the command</returns>
		public static int ExecuteNonQuery(SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText)
		{
			//pass through the call providing null for the set of SqlParameters
			return ExecuteNonQuery(connection, transaction, commandType, commandText, (SqlParameter[])null);
		}

		/// <summary>
		/// Execute a SqlCommand (that returns no resultset) against the specified SqlConnection and SqlTransaction
		/// using the provided parameters.
		/// </summary>
		/// <remarks>
		/// e.g.:  
		///  int result = ExecuteNonQuery(conn, trans, CommandType.StoredProcedure, "GetOrders", new SqlParameter("@prodid", 24));
		/// </remarks>
		/// <param name="connection">a valid SqlConnection</param>
		/// <param name="transaction">a valid SqlTransaction associated with the connection</param>
		/// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
		/// <param name="commandText">the stored procedure name or T-SQL command</param>
		/// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
		/// <returns>an int representing the number of rows affected by the command</returns>
		
		public static int ExecuteNonQuery(SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
		{
			//create a command and prepare it for execution
			SqlCommand cmd = new SqlCommand();
			PrepareCommand(cmd, connection, transaction, commandType, commandText, commandParameters);
			
			//finally, execute the command.			
			int iReturn = -2;
			try
			{
				iReturn =  cmd.ExecuteNonQuery();
			}
			catch(System.Data.SqlClient.SqlException se)
			{
				CommCenter.AMSLog logins=new CommCenter.AMSLog();
				logins.WriteLine(commandText);
				throw se;
			}
			return iReturn;
			
		}

		public static int ExecuteNonQueryLongTrans(SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
		{
			//create a command and prepare it for execution
			SqlCommand cmd = new SqlCommand();
			//			cmd.CommandTimeout = 180;
			cmd.CommandTimeout = 600;	//Modify By wjx 2007-9-6 超时时间修改为10分钟

			PrepareCommand(cmd, connection, transaction, commandType, commandText, commandParameters);
			
			//finally, execute the command.			
			int iReturn = -2;
			try
			{
				iReturn =  cmd.ExecuteNonQuery();
			}
			catch(System.Data.SqlClient.SqlException se)
			{				
				CommCenter.AMSLog logins=new CommCenter.AMSLog();
				logins.WriteLine(commandText);
				throw se;
			}
			return iReturn;
			
		}

		/// <summary>
		/// Execute a stored procedure via a SqlCommand (that returns no resultset) against the specified SqlConnection 
		/// and SqlTransaction using the provided parameter values.  This method will query the database to discover the parameters for the 
		/// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
		/// </summary>
		/// <remarks>
		/// This method provides no access to output parameters or the stored procedure's return value parameter.
		/// 
		/// e.g.:  
		///  int result = ExecuteNonQuery(conn, trans, "PublishOrders", 24, 36);
		/// </remarks>
		/// <param name="connection">a valid SqlConnection</param>
		/// <param name="transaction">a valid SqlTransaction associated with the connection</param>
		/// <param name="spName">the name of the stored prcedure</param>
		/// <param name="parameterValues">an array of objects to be assigned as the input values of the stored procedure</param>
		/// <returns>an int representing the number of rows affected by the command</returns>
		public static int ExecuteNonQuery(SqlConnection connection, SqlTransaction transaction, string spName, params object[] parameterValues)
		{
			//if we got parameter values, we need to figure out where they go
			if ((parameterValues != null) && (parameterValues.Length > 0)) 
			{
				//pull the parameters for this stored procedure from the parameter cache (or discover them & populet the cache)
				SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(connection.ConnectionString, spName);

				//assign the provided values to these parameters based on parameter order
				AssignParameterValues(commandParameters, parameterValues);

				//call the overload that takes an array of SqlParameters
				return ExecuteNonQuery(connection, transaction, CommandType.StoredProcedure, spName, commandParameters);
			}
				//otherwise we can just call the SP without params
			else 
			{
				return ExecuteNonQuery(connection, transaction, CommandType.StoredProcedure, spName);
			}
		}


		#endregion ExecuteNonQuery

		#region ExecuteDataSet
		/// <summary>
		/// Execute a SqlCommand (that returns a resultset and takes no parameters) against the provided SqlConnection. 
		/// </summary>
		/// <remarks>
		/// e.g.:  
		///  DataSet ds = ExecuteDataSet(conn, CommandType.StoredProcedure, "GetOrders");
		/// </remarks>
		/// <param name="connection">a valid SqlConnection</param>
		/// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
		/// <param name="commandText">the stored procedure name or T-SQL command</param>
		/// <returns>a dataset containing the resultset generated by the command</returns>
		public static DataSet ExecuteDataSet(SqlConnection connection, CommandType commandType, string commandText)
		{
			//pass through the call using a null transaction value
			return ExecuteDataSet(connection, null, commandType, commandText);
		}
		

		/// <summary>
		/// Execute a SqlCommand (that returns a resultset) against the specified SqlConnection 
		/// using the provided parameters.
		/// </summary>
		/// <remarks>
		/// e.g.:  
		///  DataSet ds = ExecuteDataSet(conn, CommandType.StoredProcedure, "GetOrders", new SqlParameter("@prodid", 24));
		/// </remarks>
		/// <param name="connection">a valid SqlConnection</param>
		/// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
		/// <param name="commandText">the stored procedure name or T-SQL command</param>
		/// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
		/// <returns>a dataset containing the resultset generated by the command</returns>
		public static DataSet ExecuteDataSet(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
		{
			//pass through the call using a null transaction value
			return ExecuteDataSet(connection, null, commandType, commandText, commandParameters);
		}
		

		/// <summary>
		/// Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified SqlConnection 
		/// using the provided parameter values.  This method will query the database to discover the parameters for the 
		/// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
		/// </summary>
		/// <remarks>
		/// This method provides no access to output parameters or the stored procedure's return value parameter.
		/// 
		/// e.g.:  
		///  DataSet ds = ExecuteDataSet(conn, "GetOrders", 24, 36);
		/// </remarks>
		/// <param name="connection">a valid SqlConnection</param>
		/// <param name="spName">the name of the stored prcedure</param>
		/// <param name="parameterValues">an array of objects to be assigned as the input values of the stored procedure</param>
		/// <returns>a dataset containing the resultset generated by the command</returns>
		public static DataSet ExecuteDataSet(SqlConnection connection, string spName, params object[] parameterValues)
		{
			//pass through the call using a null transaction value
			return ExecuteDataSet(connection, null, spName, parameterValues);
		}

		//these three method overloads currently take both connection and transaction.  In post-beta2 builds, only 
		//transaction will need to be passed in, and the .Connection property will be available from that transaction

		/// <summary>
		/// Execute a SqlCommand (that returns a resultset and takes no parameters) against the provided SqlConnection
		/// and SqlTransaction. 
		/// </summary>
		/// <remarks>
		/// e.g.:  
		///  DataSet ds = ExecuteDataSet(conn, trans, CommandType.StoredProcedure, "GetOrders");
		/// </remarks>
		/// <param name="connection">a valid SqlConnection</param>
		/// <param name="transaction">a valid SqlTransaction associated with the connection</param>
		/// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
		/// <param name="commandText">the stored procedure name or T-SQL command</param>
		/// <returns>a dataset containing the resultset generated by the command</returns>
		public static DataSet ExecuteDataSet(SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText)
		{
			//pass through the call providing null for the set of SqlParameters
			return ExecuteDataSet(connection, transaction, commandType, commandText, (SqlParameter[])null);
		}
		
		/// <summary>
		/// Execute a SqlCommand (that returns a resultset) against the specified SqlConnection and SqlTransaction
		/// using the provided parameters.
		/// </summary>
		/// <remarks>
		/// e.g.:  
		///  DataSet ds = ExecuteDataSet(conn, trans, CommandType.StoredProcedure, "GetOrders", new SqlParameter("@prodid", 24));
		/// </remarks>
		/// <param name="connection">a valid SqlConnection</param>
		/// <param name="transaction">a valid SqlTransaction associated with the connection</param>
		/// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
		/// <param name="commandText">the stored procedure name or T-SQL command</param>
		/// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
		/// <returns>a dataset containing the resultset generated by the command</returns>
		public static DataSet ExecuteDataSet(SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
		{
			//catch Exception add by yinkai 2003-06-18
			//create a command and prepare it for execution
			SqlCommand cmd = new SqlCommand();
			
			PrepareCommand(cmd, connection, transaction, commandType, commandText, commandParameters);
					
			//create the DataAdapter & DataSet
			SqlDataAdapter da = new SqlDataAdapter(cmd);
			DataSet ds = new DataSet();
			try
			{
				//fill the DataSet using default values for DataTable names, etc.
				da.Fill(ds);
			}
			catch(System.Data.SqlClient.SqlException se)
			{				
				CommCenter.AMSLog logins=new CommCenter.AMSLog();
				logins.WriteLine(commandText);
				throw se;
			}	
			//return the dataset
			return ds;		
			
		}
		
		/// <summary>
		/// Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified SqlConnection 
		/// and SqlTransaction using the provided parameter values.  This method will query the database to discover the parameters for the 
		/// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
		/// </summary>
		/// <remarks>
		/// This method provides no access to output parameters or the stored procedure's return value parameter.
		/// 
		/// e.g.:  
		///  DataSet ds = ExecuteDataSet(conn, trans, "GetOrders", 24, 36);
		/// </remarks>
		/// <param name="connection">a valid SqlConnection</param>
		/// <param name="transaction">a valid SqlTransaction associated with the connection</param>
		/// <param name="spName">the name of the stored prcedure</param>
		/// <param name="parameterValues">an array of objects to be assigned as the input values of the stored procedure</param>
		/// <returns>a dataset containing the resultset generated by the command</returns>
		public static DataSet ExecuteDataSet(SqlConnection connection, SqlTransaction transaction, string spName, params object[] parameterValues)
		{
			//if we got parameter values, we need to figure out where they go
			if ((parameterValues != null) && (parameterValues.Length > 0)) 
			{
				//pull the parameters for this stored procedure from the parameter cache (or discover them & populet the cache)
				SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(connection.ConnectionString, spName);

				//assign the provided values to these parameters based on parameter order
				AssignParameterValues(commandParameters, parameterValues);

				//call the overload that takes an array of SqlParameters
				return ExecuteDataSet(connection, transaction, CommandType.StoredProcedure, spName, commandParameters);
			}
				//otherwise we can just call the SP without params
			else 
			{
				return ExecuteDataSet(connection, transaction, CommandType.StoredProcedure, spName);
			}
		}

		#endregion ExecuteDataSet
		
		#region ExecuteDataTable
		/// <summary>
		/// Execute a SqlCommand (that returns a resultset and takes no parameters) against the provided SqlConnection. 
		/// </summary>
		/// <remarks>
		/// e.g.:  
		///  DataTable ds = ExecuteDataTable(conn, CommandType.StoredProcedure, "GetOrders");
		/// </remarks>
		/// <param name="connection">a valid SqlConnection</param>
		/// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
		/// <param name="commandText">the stored procedure name or T-SQL command</param>
		/// <returns>a DataTable containing the resultset generated by the command</returns>
		public static DataTable ExecuteDataTable(SqlConnection connection, CommandType commandType, string commandText)
		{
			//pass through the call using a null transaction value
			return ExecuteDataTable(connection, null, commandType, commandText);
		}
		

		/// <summary>
		/// Execute a SqlCommand (that returns a resultset) against the specified SqlConnection 
		/// using the provided parameters.
		/// </summary>
		/// <remarks>
		/// e.g.:  
		///  DataTable ds = ExecuteDataTable(conn, CommandType.StoredProcedure, "GetOrders", new SqlParameter("@prodid", 24));
		/// </remarks>
		/// <param name="connection">a valid SqlConnection</param>
		/// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
		/// <param name="commandText">the stored procedure name or T-SQL command</param>
		/// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
		/// <returns>a DataTable containing the resultset generated by the command</returns>
		public static DataTable ExecuteDataTable(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
		{
			//pass through the call using a null transaction value
			return ExecuteDataTable(connection, null, commandType, commandText, commandParameters);
		}
		

		/// <summary>
		/// Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified SqlConnection 
		/// using the provided parameter values.  This method will query the database to discover the parameters for the 
		/// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
		/// </summary>
		/// <remarks>
		/// This method provides no access to output parameters or the stored procedure's return value parameter.
		/// 
		/// e.g.:  
		///  DataTable ds = ExecuteDataTable(conn, "GetOrders", 24, 36);
		/// </remarks>
		/// <param name="connection">a valid SqlConnection</param>
		/// <param name="spName">the name of the stored prcedure</param>
		/// <param name="parameterValues">an array of objects to be assigned as the input values of the stored procedure</param>
		/// <returns>a DataTable containing the resultset generated by the command</returns>
		public static DataTable ExecuteDataTable(SqlConnection connection, string spName, params object[] parameterValues)
		{
			//pass through the call using a null transaction value
			return ExecuteDataTable(connection, null, spName, parameterValues);
		}

		//these three method overloads currently take both connection and transaction.  In post-beta2 builds, only 
		//transaction will need to be passed in, and the .Connection property will be available from that transaction

		/// <summary>
		/// Execute a SqlCommand (that returns a resultset and takes no parameters) against the provided SqlConnection
		/// and SqlTransaction. 
		/// </summary>
		/// <remarks>
		/// e.g.:  
		///  DataTable ds = ExecuteDataTable(conn, trans, CommandType.StoredProcedure, "GetOrders");
		/// </remarks>
		/// <param name="connection">a valid SqlConnection</param>
		/// <param name="transaction">a valid SqlTransaction associated with the connection</param>
		/// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
		/// <param name="commandText">the stored procedure name or T-SQL command</param>
		/// <returns>a DataTable containing the resultset generated by the command</returns>
		public static DataTable ExecuteDataTable(SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText)
		{
			//pass through the call providing null for the set of SqlParameters
			return ExecuteDataTable(connection, transaction, commandType, commandText, (SqlParameter[])null);
		}
		
		/// <summary>
		/// Execute a SqlCommand (that returns a resultset) against the specified SqlConnection and SqlTransaction
		/// using the provided parameters.
		/// </summary>
		/// <remarks>
		/// e.g.:  
		///  DataTable ds = ExecuteDataTable(conn, trans, CommandType.StoredProcedure, "GetOrders", new SqlParameter("@prodid", 24));
		/// </remarks>
		/// <param name="connection">a valid SqlConnection</param>
		/// <param name="transaction">a valid SqlTransaction associated with the connection</param>
		/// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
		/// <param name="commandText">the stored procedure name or T-SQL command</param>
		/// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
		/// <returns>a DataTable containing the resultset generated by the command</returns>
		/// 
		/// try catch add by yinkai 2003-5-17
		public static DataTable ExecuteDataTable(SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
		{
			//create a command and prepare it for execution
			SqlCommand cmd = new SqlCommand();
			cmd.CommandTimeout = 360; //add by msd 2004-12-08
			DataTable ds = new DataTable();
			try
			{
				PrepareCommand(cmd, connection, transaction, commandType, commandText, commandParameters);
				//create the DataAdapter & DataTable
				SqlDataAdapter da = new SqlDataAdapter(cmd);
				//fill the DataTable using default values for DataTable names, etc.
				da.Fill(ds);
			}
			catch(System.Data.SqlClient.SqlException se)
			{
				CommCenter.AMSLog logins=new CommCenter.AMSLog();
				logins.WriteLine(commandText);
				throw se;
			}			
			//return the DataTable
			return ds;
		}

		/// <summary>
		/// for long trans  add by yinkai 2003-10-10
		/// </summary>
		/// <param name="connection"></param>
		/// <param name="transaction"></param>
		/// <param name="commandType"></param>
		/// <param name="commandText"></param>
		/// <param name="commandParameters"></param>
		/// <returns></returns>
		public static DataTable ExecuteDataTableLongTrans(SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
		{
			//create a command and prepare it for execution
			SqlCommand cmd = new SqlCommand();
			DataTable ds = new DataTable();
			try
			{
				PrepareCommand(cmd, connection, transaction, commandType, commandText, commandParameters);
				//create the DataAdapter & DataTable
				SqlDataAdapter da = new SqlDataAdapter(cmd);
				//fill the DataTable using default values for DataTable names, etc.
				da.Fill(ds);
			}
			catch(System.Data.SqlClient.SqlException se)
			{
				CommCenter.AMSLog logins=new CommCenter.AMSLog();
				logins.WriteLine(commandText);
				throw se;
			}			
			//return the DataTable
			return ds;
		}
		
		/// <summary>
		/// Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified SqlConnection 
		/// and SqlTransaction using the provided parameter values.  This method will query the database to discover the parameters for the 
		/// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
		/// </summary>
		/// <remarks>
		/// This method provides no access to output parameters or the stored procedure's return value parameter.
		/// 
		/// e.g.:  
		///  DataTable ds = ExecuteDataTable(conn, trans, "GetOrders", 24, 36);
		/// </remarks>
		/// <param name="connection">a valid SqlConnection</param>
		/// <param name="transaction">a valid SqlTransaction associated with the connection</param>
		/// <param name="spName">the name of the stored prcedure</param>
		/// <param name="parameterValues">an array of objects to be assigned as the input values of the stored procedure</param>
		/// <returns>a DataTable containing the resultset generated by the command</returns>
		public static DataTable ExecuteDataTable(SqlConnection connection, SqlTransaction transaction, string spName, params object[] parameterValues)
		{
			//if we got parameter values, we need to figure out where they go
			if ((parameterValues != null) && (parameterValues.Length > 0)) 
			{
				//pull the parameters for this stored procedure from the parameter cache (or discover them & populet the cache)
				SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(connection.ConnectionString, spName);

				//assign the provided values to these parameters based on parameter order
				AssignParameterValues(commandParameters, parameterValues);

				//call the overload that takes an array of SqlParameters
				return ExecuteDataTable(connection, transaction, CommandType.StoredProcedure, spName, commandParameters);
			}
				//otherwise we can just call the SP without params
			else 
			{
				return ExecuteDataTable(connection, transaction, CommandType.StoredProcedure, spName);
			}
		}

		#endregion ExecuteDataTable
		
		#region ExecuteFillDataSet

		#region dataSet, connection, commandType, commandText
		/// <summary>
		/// Execute a SqlCommand (that returns a resultset and takes no parameters) against the provided SqlConnection. 
		/// </summary>
		/// <remarks>
		/// e.g.:  
		///  ExecuteFillDataSet(dataSet, conn, CommandType.StoredProcedure, "GetOrders");
		/// </remarks>
		/// <param name="dataSet">a valid dataSet to fill in</param>
		/// <param name="connection">a valid SqlConnection</param>
		/// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
		/// <param name="commandText">the stored procedure name or T-SQL command</param>
		/// <returns>a dataset containing the resultset generated by the command</returns>
		public static void ExecuteFillDataSet(DataSet dataSet, SqlConnection connection, CommandType commandType, string commandText)
		{
			//pass through the call using a null transaction value
			ExecuteFillDataSet(dataSet, connection, null, commandType, commandText);
		}
		
		/// <summary>
		/// Execute a SqlCommand (that returns a resultset) against the specified SqlConnection 
		/// using the provided parameters.
		/// </summary>
		/// <remarks>
		/// e.g.:  
		///  ExecuteFillDataSet(DataSet dataSet, conn, CommandType.StoredProcedure, "GetOrders", new SqlParameter("@prodid", 24));
		/// </remarks>
		/// <param name="dataSet">a valid dataSet to fill in</param>
		/// <param name="connection">a valid SqlConnection</param>
		/// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
		/// <param name="commandText">the stored procedure name or T-SQL command</param>
		/// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
		/// <returns>a dataset containing the resultset generated by the command</returns>
		public static void ExecuteFillDataSet(DataSet dataSet, SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
		{
			//pass through the call using a null transaction value
			ExecuteFillDataSet(dataSet, connection, null, commandType, commandText, commandParameters);
		}
		
		/// <summary>
		/// Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified SqlConnection 
		/// using the provided parameter values.  This method will query the database to discover the parameters for the 
		/// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
		/// </summary>
		/// <remarks>
		/// This method provides no access to output parameters or the stored procedure's return value parameter.
		/// 
		/// e.g.:  
		///  ExecuteFillDataSet(DataSet dataSet, conn, "GetOrders", 24, 36);
		/// </remarks>
		/// <param name="dataSet">a valid dataSet to fill in</param>
		/// <param name="connection">a valid SqlConnection</param>
		/// <param name="spName">the name of the stored prcedure</param>
		/// <param name="parameterValues">an array of objects to be assigned as the input values of the stored procedure</param>
		/// <returns>a dataset containing the resultset generated by the command</returns>
		public static void ExecuteFillDataSet(DataSet dataSet, SqlConnection connection, string spName, params object[] parameterValues)
		{
			//pass through the call using a null transaction value
			ExecuteFillDataSet(dataSet, connection, null, spName, parameterValues);
		}
		#endregion

		#region dataSet, tableName, connection, commandType, commandText
		/// <summary>
		/// Execute a SqlCommand (that returns a resultset and takes no parameters) against the provided SqlConnection. 
		/// </summary>
		/// <remarks>
		/// e.g.:  
		///  ExecuteFillDataSet(dataSet, tableName, conn, CommandType.StoredProcedure, "GetOrders");
		/// </remarks>
		/// <param name="dataSet">a valid dataSet to fill in</param>
		/// <param name="tableName">a table name to fill in</param>
		/// <param name="connection">a valid SqlConnection</param>
		/// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
		/// <param name="commandText">the stored procedure name or T-SQL command</param>
		/// <returns>a dataset containing the resultset generated by the command</returns>
		public static void ExecuteFillDataSet(DataSet dataSet, string tableName, SqlConnection connection, CommandType commandType, string commandText)
		{
			//pass through the call using a null transaction value
			ExecuteFillDataSet(dataSet, tableName, connection, null, commandType, commandText);
		}
		
		/// <summary>
		/// Execute a SqlCommand (that returns a resultset) against the specified SqlConnection 
		/// using the provided parameters.
		/// </summary>
		/// <remarks>
		/// e.g.:  
		///  ExecuteFillDataSet(dataSet, tableName, conn, CommandType.StoredProcedure, "GetOrders", new SqlParameter("@prodid", 24));
		/// </remarks>
		/// <param name="dataSet">a valid dataSet to fill in</param>
		/// <param name="tableName">a table name to fill in</param>
		/// <param name="connection">a valid SqlConnection</param>
		/// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
		/// <param name="commandText">the stored procedure name or T-SQL command</param>
		/// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
		/// <returns>a dataset containing the resultset generated by the command</returns>
		public static void ExecuteFillDataSet(DataSet dataSet, string tableName, SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
		{
			//pass through the call using a null transaction value
			ExecuteFillDataSet(dataSet, tableName, connection, null, commandType, commandText, commandParameters);
		}
		
		/// <summary>
		/// Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified SqlConnection 
		/// using the provided parameter values.  This method will query the database to discover the parameters for the 
		/// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
		/// </summary>
		/// <remarks>
		/// This method provides no access to output parameters or the stored procedure's return value parameter.
		/// 
		/// e.g.:  
		///  ExecuteFillDataSet(dataSet, tableName, conn, "GetOrders", 24, 36);
		/// </remarks>
		/// <param name="dataSet">a valid dataSet to fill in</param>
		/// <param name="tableName">a table name to fill in</param>
		/// <param name="connection">a valid SqlConnection</param>
		/// <param name="spName">the name of the stored prcedure</param>
		/// <param name="parameterValues">an array of objects to be assigned as the input values of the stored procedure</param>
		/// <returns>a dataset containing the resultset generated by the command</returns>
		public static void ExecuteFillDataSet(DataSet dataSet, string tableName, SqlConnection connection, string spName, params object[] parameterValues)
		{
			//pass through the call using a null transaction value
			ExecuteFillDataSet(dataSet, tableName, connection, null, spName, parameterValues);
		}
		#endregion

		#region dataSet, connection, transaction, commandType, commandText
		/// <summary>
		/// Execute a SqlCommand (that returns a resultset and takes no parameters) against the provided SqlConnection
		/// and SqlTransaction. 
		/// </summary>
		/// <remarks>
		/// e.g.:  
		///  ExecuteFillDataSet(DataSet dataSet, conn, trans, CommandType.StoredProcedure, "GetOrders");
		/// </remarks>
		/// <param name="dataSet">a valid dataSet to fill in</param>
		/// <param name="connection">a valid SqlConnection</param>
		/// <param name="transaction">a valid SqlTransaction associated with the connection</param>
		/// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
		/// <param name="commandText">the stored procedure name or T-SQL command</param>
		/// <returns>a dataset containing the resultset generated by the command</returns>
		public static void ExecuteFillDataSet(DataSet dataSet, SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText)
		{
			//pass through the call providing null for the set of SqlParameters
			ExecuteFillDataSet(dataSet, connection, transaction, commandType, commandText, (SqlParameter[])null);
		}
		
		/// <summary>
		/// Execute a SqlCommand (that returns a resultset) against the specified SqlConnection and SqlTransaction
		/// using the provided parameters.
		/// </summary>
		/// <remarks>
		/// e.g.:  
		///  ExecuteFillDataSet(DataSet dataSet, conn, trans, CommandType.StoredProcedure, "GetOrders", new SqlParameter("@prodid", 24));
		/// </remarks>
		/// <param name="dataSet">a valid dataSet to fill in</param>
		/// <param name="connection">a valid SqlConnection</param>
		/// <param name="transaction">a valid SqlTransaction associated with the connection</param>
		/// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
		/// <param name="commandText">the stored procedure name or T-SQL command</param>
		/// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
		/// <returns>a dataset containing the resultset generated by the command</returns>
		public static void ExecuteFillDataSet(DataSet dataSet, SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
		{
			//create a command and prepare it for execution
			SqlCommand cmd = new SqlCommand();
			PrepareCommand(cmd, connection, transaction, commandType, commandText, commandParameters);
			
			//create the DataAdapter & DataSet
			SqlDataAdapter da = new SqlDataAdapter(cmd);

			//fill the DataSet using default values for DataTable names, etc.
			da.Fill(dataSet);
		}
		
		/// <summary>
		/// Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified SqlConnection 
		/// and SqlTransaction using the provided parameter values.  This method will query the database to discover the parameters for the 
		/// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
		/// </summary>
		/// <remarks>
		/// This method provides no access to output parameters or the stored procedure's return value parameter.
		/// 
		/// e.g.:  
		///  ExecuteFillDataSet(dataSet, conn, trans, "GetOrders", 24, 36);
		/// </remarks>
		/// <param name="dataSet">a valid dataSet to fill in</param>
		/// <param name="connection">a valid SqlConnection</param>
		/// <param name="transaction">a valid SqlTransaction associated with the connection</param>
		/// <param name="spName">the name of the stored prcedure</param>
		/// <param name="parameterValues">an array of objects to be assigned as the input values of the stored procedure</param>
		/// <returns>a dataset containing the resultset generated by the command</returns>
		public static void ExecuteFillDataSet(DataSet dataSet, SqlConnection connection, SqlTransaction transaction, string spName, params object[] parameterValues)
		{
			//if we got parameter values, we need to figure out where they go
			if ((parameterValues != null) && (parameterValues.Length > 0)) 
			{
				//pull the parameters for this stored procedure from the parameter cache (or discover them & populet the cache)
				SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(connection.ConnectionString, spName);

				//assign the provided values to these parameters based on parameter order
				AssignParameterValues(commandParameters, parameterValues);

				//call the overload that takes an array of SqlParameters
				ExecuteFillDataSet(dataSet, connection, transaction, CommandType.StoredProcedure, spName, commandParameters);
			}
				//otherwise we can just call the SP without params
			else 
			{
				ExecuteFillDataSet(dataSet, connection, transaction, CommandType.StoredProcedure, spName);
			}
		}
		#endregion

		#region dataSet, tableName, connection, transaction, commandType, commandText
		/// <summary>
		/// Execute a SqlCommand (that returns a resultset and takes no parameters) against the provided SqlConnection
		/// and SqlTransaction. 
		/// </summary>
		/// <remarks>
		/// e.g.:  
		///  ExecuteFillDataSet(dataSet, tableName, conn, trans, CommandType.StoredProcedure, "GetOrders");
		/// </remarks>
		/// <param name="dataSet">a valid dataSet to fill in</param>
		/// <param name="tableName">a table name to fill in</param>
		/// <param name="connection">a valid SqlConnection</param>
		/// <param name="transaction">a valid SqlTransaction associated with the connection</param>
		/// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
		/// <param name="commandText">the stored procedure name or T-SQL command</param>
		/// <returns>a dataset containing the resultset generated by the command</returns>
		public static void ExecuteFillDataSet(DataSet dataSet, string tableName, SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText)
		{
			//pass through the call providing null for the set of SqlParameters
			ExecuteFillDataSet(dataSet, tableName, connection, transaction, commandType, commandText, (SqlParameter[])null);
		}
		
		/// <summary>
		/// Execute a SqlCommand (that returns a resultset) against the specified SqlConnection and SqlTransaction
		/// using the provided parameters.
		/// </summary>
		/// <remarks>
		/// e.g.:  
		///  ExecuteFillDataSet(dataSet, tableName, conn, trans, CommandType.StoredProcedure, "GetOrders", new SqlParameter("@prodid", 24));
		/// </remarks>
		/// <param name="dataSet">a valid dataSet to fill in</param>
		/// <param name="tableName">a table name to fill in</param>
		/// <param name="connection">a valid SqlConnection</param>
		/// <param name="transaction">a valid SqlTransaction associated with the connection</param>
		/// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
		/// <param name="commandText">the stored procedure name or T-SQL command</param>
		/// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
		/// <returns>a dataset containing the resultset generated by the command</returns>
		public static void ExecuteFillDataSet(DataSet dataSet, string tableName, SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
		{
			//create a command and prepare it for execution
			SqlCommand cmd = new SqlCommand();
			PrepareCommand(cmd, connection, transaction, commandType, commandText, commandParameters);
			
			//create the DataAdapter & DataSet
			SqlDataAdapter da = new SqlDataAdapter(cmd);

			//fill the DataSet using default values for DataTable names, etc.
			da.Fill(dataSet, tableName);
		}
		
		/// <summary>
		/// Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified SqlConnection 
		/// and SqlTransaction using the provided parameter values.  This method will query the database to discover the parameters for the 
		/// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
		/// </summary>
		/// <remarks>
		/// This method provides no access to output parameters or the stored procedure's return value parameter.
		/// 
		/// e.g.:  
		///  ExecuteFillDataSet(dataSet, tableName, conn, trans, "GetOrders", 24, 36);
		/// </remarks>
		/// <param name="dataSet">a valid dataSet to fill in</param>
		/// <param name="tableName">a table name to fill in</param>
		/// <param name="connection">a valid SqlConnection</param>
		/// <param name="transaction">a valid SqlTransaction associated with the connection</param>
		/// <param name="spName">the name of the stored prcedure</param>
		/// <param name="parameterValues">an array of objects to be assigned as the input values of the stored procedure</param>
		/// <returns>a dataset containing the resultset generated by the command</returns>
		public static void ExecuteFillDataSet(DataSet dataSet, string tableName, SqlConnection connection, SqlTransaction transaction, string spName, params object[] parameterValues)
		{
			//if we got parameter values, we need to figure out where they go
			if ((parameterValues != null) && (parameterValues.Length > 0)) 
			{
				//pull the parameters for this stored procedure from the parameter cache (or discover them & populet the cache)
				SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(connection.ConnectionString, spName);

				//assign the provided values to these parameters based on parameter order
				AssignParameterValues(commandParameters, parameterValues);

				//call the overload that takes an array of SqlParameters
				ExecuteFillDataSet(dataSet, tableName, connection, transaction, CommandType.StoredProcedure, spName, commandParameters);
			}
				//otherwise we can just call the SP without params
			else 
			{
				ExecuteFillDataSet(dataSet, tableName, connection, transaction, CommandType.StoredProcedure, spName);
			}
		}
		#endregion

		#endregion

		#region ExecuteReader

		/// <summary>
		/// Create and prepare a SqlCommand, and call ExecuteReader with the appropriate CommandBehavior.
		/// </summary>
		/// <remarks>
		/// If we created and opened the connection, we want the connection to be closed when the DataReader is closed.
		/// 
		/// If the caller provided the connection, we want to leave it to them to manage.
		/// </remarks>
		/// <param name="connection">a valid SqlConnection, on which to execute this command</param>
		/// <param name="transaction">a valid SqlTransaction, or 'null'</param>
		/// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
		/// <param name="commandText">the stored procedure name or T-SQL command</param>
		/// <param name="commandParameters">an array of SqlParameters to be associated with the command or 'null' if no parameters are required</param>
		/// <param name="connectionOwnership">indicates weather the connection parameter was provided by the caller, or created by SqlHelper</param>
		/// <returns>SqlDataReader containing the results of the command</returns>
		private static SqlDataReader ExecuteReader(SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, SqlParameter[] commandParameters, ConnectionOwnership connectionOwnership)
		{	
			//create a command and prepare it for execution
			SqlCommand cmd = new SqlCommand();
			PrepareCommand(cmd, connection, transaction, commandType, commandText, commandParameters);
			
			//create a reader
			SqlDataReader dr;
			try
			{
				// call ExecuteReader with the appropriate CommandBehavior
				if (connectionOwnership == ConnectionOwnership.External)
				{
					dr = cmd.ExecuteReader();
				}
				else
				{
					dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
				}
			}
			catch(System.Data.SqlClient.SqlException se)
			{
				CommCenter.AMSLog logins=new CommCenter.AMSLog();
				logins.WriteLine(commandText);
				throw se;
			}

			return dr;
		}

		/// <summary>
		/// Execute a SqlCommand (that returns a resultset and takes no parameters) against the provided SqlConnection. 
		/// </summary>
		/// <remarks>
		/// e.g.:  
		///  SqlDataReader dr = ExecuteReader(conn, CommandType.StoredProcedure, "GetOrders");
		/// </remarks>
		/// <param name="connection">a valid SqlConnection</param>
		/// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
		/// <param name="commandText">the stored procedure name or T-SQL command</param>
		/// <returns>a SqlDataReader containing the resultset generated by the command</returns>
		public static SqlDataReader ExecuteReader(SqlConnection connection, CommandType commandType, string commandText)
		{
			//pass through the call using a null transaction value
			return ExecuteReader(connection, null, commandType, commandText);
		}

		/// <summary>
		/// Execute a SqlCommand (that returns a resultset) against the specified SqlConnection 
		/// using the provided parameters.
		/// </summary>
		/// <remarks>
		/// e.g.:  
		///  SqlDataReader dr = ExecuteReader(conn, CommandType.StoredProcedure, "GetOrders", new SqlParameter("@prodid", 24));
		/// </remarks>
		/// <param name="connection">a valid SqlConnection</param>
		/// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
		/// <param name="commandText">the stored procedure name or T-SQL command</param>
		/// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
		/// <returns>a SqlDataReader containing the resultset generated by the command</returns>
		public static SqlDataReader ExecuteReader(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
		{
			//pass through the call using a null transaction value
			return ExecuteReader(connection, null, commandType, commandText, commandParameters);
		}

		/// <summary>
		/// Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified SqlConnection 
		/// using the provided parameter values.  This method will query the database to discover the parameters for the 
		/// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
		/// </summary>
		/// <remarks>
		/// This method provides no access to output parameters or the stored procedure's return value parameter.
		/// 
		/// e.g.:  
		///  SqlDataReader dr = ExecuteReader(conn, "GetOrders", 24, 36);
		/// </remarks>
		/// <param name="connection">a valid SqlConnection</param>
		/// <param name="spName">the name of the stored prcedure</param>
		/// <param name="parameterValues">an array of objects to be assigned as the input values of the stored procedure</param>
		/// <returns>a SqlDataReader containing the resultset generated by the command</returns>
		public static SqlDataReader ExecuteReader(SqlConnection connection, string spName, params object[] parameterValues)
		{
			//pass through the call using a null transaction value
			return ExecuteReader(connection, null, spName, parameterValues);
		}

		//these three method overloads currently take both connection and transaction.  In post-beta2 builds, only 
		//transaction will need to be passed in, and the .Connection property will be available from that transaction

		/// <summary>
		/// Execute a SqlCommand (that returns a resultset and takes no parameters) against the provided SqlConnection
		/// and SqlTransaction. 
		/// </summary>
		/// <remarks>
		/// e.g.:  
		///  SqlDataReader dr = ExecuteReader(conn, trans, CommandType.StoredProcedure, "GetOrders");
		/// </remarks>
		/// <param name="connection">a valid SqlConnection</param>
		/// <param name="transaction">a valid SqlTransaction associated with the connection</param>
		/// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
		/// <param name="commandText">the stored procedure name or T-SQL command</param>
		/// <returns>a SqlDataReader containing the resultset generated by the command</returns>
		public static SqlDataReader ExecuteReader(SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText)
		{
			//pass through the call providing null for the set of SqlParameters
			return ExecuteReader(connection, transaction, commandType, commandText, (SqlParameter[])null);
		}

		/// <summary>
		/// Execute a SqlCommand (that returns a resultset) against the specified SqlConnection and SqlTransaction
		/// using the provided parameters.
		/// </summary>
		/// <remarks>
		/// e.g.:  
		///   SqlDataReader dr = ExecuteReader(conn, trans, CommandType.StoredProcedure, "GetOrders", new SqlParameter("@prodid", 24));
		/// </remarks>
		/// <param name="connection">a valid SqlConnection</param>
		/// <param name="transaction">a valid SqlTransaction associated with the connection</param>
		/// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
		/// <param name="commandText">the stored procedure name or T-SQL command</param>
		/// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
		/// <returns>a SqlDataReader containing the resultset generated by the command</returns>
		public static SqlDataReader ExecuteReader(SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
		{
			//pass through to private overload, indicating that the connection is owned by the caller
			return ExecuteReader(connection, transaction, commandType, commandText, commandParameters, ConnectionOwnership.External);
		}

		/// <summary>
		/// Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified SqlConnection 
		/// and SqlTransaction using the provided parameter values.  This method will query the database to discover the parameters for the 
		/// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
		/// </summary>
		/// <remarks>
		/// This method provides no access to output parameters or the stored procedure's return value parameter.
		/// 
		/// e.g.:  
		///  SqlDataReader dr = ExecuteReader(conn, trans, "GetOrders", 24, 36);
		/// </remarks>
		/// <param name="connection">a valid SqlConnection</param>
		/// <param name="transaction">a valid SqlTransaction associated with the connection</param>
		/// <param name="spName">the name of the stored prcedure</param>
		/// <param name="parameterValues">an array of objects to be assigned as the input values of the stored procedure</param>
		/// <returns>a SqlDataReader containing the resultset generated by the command</returns>
		public static SqlDataReader ExecuteReader(SqlConnection connection, SqlTransaction transaction, string spName, params object[] parameterValues)
		{
			//if we got parameter values, we need to figure out where they go
			if ((parameterValues != null) && (parameterValues.Length > 0)) 
			{
				SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(connection.ConnectionString, spName);

				AssignParameterValues(commandParameters, parameterValues);

				return ExecuteReader(connection, transaction, CommandType.StoredProcedure, spName, commandParameters);
			}
				//otherwise we can just call the SP without params
			else 
			{
				return ExecuteReader(connection, transaction, CommandType.StoredProcedure, spName);
			}
		}

		#endregion ExecuteReader

		#region ExecuteScalar
		
		/// <summary>
		/// Execute a SqlCommand (that returns a 1x1 resultset and takes no parameters) against the provided SqlConnection. 
		/// </summary>
		/// <remarks>
		/// e.g.:  
		///  int orderCount = (int)ExecuteScalar(conn, CommandType.StoredProcedure, "GetOrderCount");
		/// </remarks>
		/// <param name="connection">a valid SqlConnection</param>
		/// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
		/// <param name="commandText">the stored procedure name or T-SQL command</param>
		/// <returns>an object containing the value in the 1x1 resultset generated by the command</returns>
		public static object ExecuteScalar(SqlConnection connection, CommandType commandType, string commandText)
		{
			//pass through the call using a null transaction value
			return ExecuteScalar(connection, null, commandType, commandText);
		}

		/// <summary>
		/// Execute a SqlCommand (that returns a 1x1 resultset) against the specified SqlConnection 
		/// using the provided parameters.
		/// </summary>
		/// <remarks>
		/// e.g.:  
		///  int orderCount = (int)ExecuteScalar(conn, CommandType.StoredProcedure, "GetOrderCount", new SqlParameter("@prodid", 24));
		/// </remarks>
		/// <param name="connection">a valid SqlConnection</param>
		/// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
		/// <param name="commandText">the stored procedure name or T-SQL command</param>
		/// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
		/// <returns>an object containing the value in the 1x1 resultset generated by the command</returns>
		public static object ExecuteScalar(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
		{
			//pass through the call using a null transaction value
			return ExecuteScalar(connection, null, commandType, commandText, commandParameters);
		}

		/// <summary>
		/// Execute a stored procedure via a SqlCommand (that returns a 1x1 resultset) against the specified SqlConnection 
		/// using the provided parameter values.  This method will query the database to discover the parameters for the 
		/// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
		/// </summary>
		/// <remarks>
		/// This method provides no access to output parameters or the stored procedure's return value parameter.
		/// 
		/// e.g.:  
		///  int orderCount = (int)ExecuteScalar(conn, "GetOrderCount", 24, 36);
		/// </remarks>
		/// <param name="connection">a valid SqlConnection</param>
		/// <param name="spName">the name of the stored prcedure</param>
		/// <param name="parameterValues">an array of objects to be assigned as the input values of the stored procedure</param>
		/// <returns>an object containing the value in the 1x1 resultset generated by the command</returns>
		public static object ExecuteScalar(SqlConnection connection, string spName, params object[] parameterValues)
		{
			//pass through the call using a null transaction value
			return ExecuteScalar(connection, null, spName, parameterValues);
		}

		//these three method overloads currently take both connection and transaction.  In post-beta2 builds, only 
		//transaction will need to be passed in, and the .Connection property will be available from that transaction

		/// <summary>
		/// Execute a SqlCommand (that returns a 1x1 resultset and takes no parameters) against the provided SqlConnection
		/// and SqlTransaction. 
		/// </summary>
		/// <remarks>
		/// e.g.:  
		///  int orderCount = (int)ExecuteScalar(conn, trans, CommandType.StoredProcedure, "GetOrderCount");
		/// </remarks>
		/// <param name="connection">a valid SqlConnection</param>
		/// <param name="transaction">a valid SqlTransaction associated with the connection</param>
		/// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
		/// <param name="commandText">the stored procedure name or T-SQL command</param>
		/// <returns>an object containing the value in the 1x1 resultset generated by the command</returns>
		public static object ExecuteScalar(SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText)
		{
			//pass through the call providing null for the set of SqlParameters
			return ExecuteScalar(connection, transaction, commandType, commandText, (SqlParameter[])null);
		}

		/// <summary>
		/// Execute a SqlCommand (that returns a 1x1 resultset) against the specified SqlConnection and SqlTransaction
		/// using the provided parameters.
		/// </summary>
		/// <remarks>
		/// e.g.:  
		///  int orderCount = (int)ExecuteScalar(conn, trans, CommandType.StoredProcedure, "GetOrderCount", new SqlParameter("@prodid", 24));
		/// </remarks>
		/// <param name="connection">a valid SqlConnection</param>
		/// <param name="transaction">a valid SqlTransaction associated with the connection</param>
		/// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
		/// <param name="commandText">the stored procedure name or T-SQL command</param>
		/// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
		/// <returns>an object containing the value in the 1x1 resultset generated by the command</returns>
		public static object ExecuteScalar(SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
		{
			//create a command and prepare it for execution
			SqlCommand cmd = new SqlCommand();
			PrepareCommand(cmd, connection, transaction, commandType, commandText, commandParameters);
			
			//execute the command & return the results
			return cmd.ExecuteScalar();

		}

		/// <summary>
		/// Execute a stored procedure via a SqlCommand (that returns a 1x1 resultset) against the specified SqlConnection 
		/// and SqlTransaction using the provided parameter values.  This method will query the database to discover the parameters for the 
		/// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
		/// </summary>
		/// <remarks>
		/// This method provides no access to output parameters or the stored procedure's return value parameter.
		/// 
		/// e.g.:  
		///  int orderCount = (int)ExecuteScalar(conn, trans, "GetOrderCount", 24, 36);
		/// </remarks>
		/// <param name="connection">a valid SqlConnection</param>
		/// <param name="transaction">a valid SqlTransaction associated with the connection</param>
		/// <param name="spName">the name of the stored prcedure</param>
		/// <param name="parameterValues">an array of objects to be assigned as the input values of the stored procedure</param>
		/// <returns>an object containing the value in the 1x1 resultset generated by the command</returns>
		public static object ExecuteScalar(SqlConnection connection, SqlTransaction transaction, string spName, params object[] parameterValues)
		{
			//if we got parameter values, we need to figure out where they go
			if ((parameterValues != null) && (parameterValues.Length > 0)) 
			{
				//pull the parameters for this stored procedure from the parameter cache (or discover them & populet the cache)
				SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(connection.ConnectionString, spName);

				//assign the provided values to these parameters based on parameter order
				AssignParameterValues(commandParameters, parameterValues);

				//call the overload that takes an array of SqlParameters
				return ExecuteScalar(connection, transaction, CommandType.StoredProcedure, spName, commandParameters);
			}
				//otherwise we can just call the SP without params
			else 
			{
				return ExecuteScalar(connection, transaction, CommandType.StoredProcedure, spName);
			}
		}

		#endregion ExecuteScalar	

		#region ExecuteXmlReader

		/// <summary>
		/// Execute a SqlCommand (that returns a resultset and takes no parameters) against the provided SqlConnection. 
		/// </summary>
		/// <remarks>
		/// e.g.:  
		///  XmlReader r = ExecuteXmlReader(conn, CommandType.StoredProcedure, "GetOrders");
		/// </remarks>
		/// <param name="connection">a valid SqlConnection</param>
		/// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
		/// <param name="commandText">the stored procedure name or T-SQL command using "FOR XML AUTO"</param>
		/// <returns>an XmlReader containing the resultset generated by the command</returns>
		public static XmlReader ExecuteXmlReader(SqlConnection connection, CommandType commandType, string commandText)
		{
			//pass through the call using a null transaction value
			return ExecuteXmlReader(connection, null, commandType, commandText);
		}

		/// <summary>
		/// Execute a SqlCommand (that returns a resultset) against the specified SqlConnection 
		/// using the provided parameters.
		/// </summary>
		/// <remarks>
		/// e.g.:  
		///  XmlReader r = ExecuteXmlReader(conn, CommandType.StoredProcedure, "GetOrders", new SqlParameter("@prodid", 24));
		/// </remarks>
		/// <param name="connection">a valid SqlConnection</param>
		/// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
		/// <param name="commandText">the stored procedure name or T-SQL command using "FOR XML AUTO"</param>
		/// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
		/// <returns>an XmlReader containing the resultset generated by the command</returns>
		public static XmlReader ExecuteXmlReader(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
		{
			//pass through the call using a null transaction value
			return ExecuteXmlReader(connection, null, commandType, commandText, commandParameters);
		}

		/// <summary>
		/// Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified SqlConnection 
		/// using the provided parameter values.  This method will query the database to discover the parameters for the 
		/// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
		/// </summary>
		/// <remarks>
		/// This method provides no access to output parameters or the stored procedure's return value parameter.
		/// 
		/// e.g.:  
		///  XmlReader r = ExecuteXmlReader(conn, "GetOrders", 24, 36);
		/// </remarks>
		/// <param name="connection">a valid SqlConnection</param>
		/// <param name="spName">the name of the stored prcedure using "FOR XML AUTO"</param>
		/// <param name="parameterValues">an array of objects to be assigned as the input values of the stored procedure</param>
		/// <returns>an XmlReader containing the resultset generated by the command</returns>
		public static XmlReader ExecuteXmlReader(SqlConnection connection, string spName, params object[] parameterValues)
		{
			//pass through the call using a null transaction value
			return ExecuteXmlReader(connection, null, spName, parameterValues);
		}

		//these three method overloads currently take both connection and transaction.  In post-beta2 builds, only 
		//transaction will need to be passed in, and the .Connection property will be available from that transaction

		/// <summary>
		/// Execute a SqlCommand (that returns a resultset and takes no parameters) against the provided SqlConnection
		/// and SqlTransaction. 
		/// </summary>
		/// <remarks>
		/// e.g.:  
		///  XmlReader r = ExecuteXmlReader(conn, trans, CommandType.StoredProcedure, "GetOrders");
		/// </remarks>
		/// <param name="connection">a valid SqlConnection</param>
		/// <param name="transaction">a valid SqlTransaction associated with the connection</param>
		/// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
		/// <param name="commandText">the stored procedure name or T-SQL command using "FOR XML AUTO"</param>
		/// <returns>an XmlReader containing the resultset generated by the command</returns>
		public static XmlReader ExecuteXmlReader(SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText)
		{
			//pass through the call providing null for the set of SqlParameters
			return ExecuteXmlReader(connection, transaction, commandType, commandText, (SqlParameter[])null);
		}

		/// <summary>
		/// Execute a SqlCommand (that returns a resultset) against the specified SqlConnection and SqlTransaction
		/// using the provided parameters.
		/// </summary>
		/// <remarks>
		/// e.g.:  
		///  XmlReader r = ExecuteXmlReader(conn, trans, CommandType.StoredProcedure, "GetOrders", new SqlParameter("@prodid", 24));
		/// </remarks>
		/// <param name="connection">a valid SqlConnection</param>
		/// <param name="transaction">a valid SqlTransaction associated with the connection</param>
		/// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
		/// <param name="commandText">the stored procedure name or T-SQL command using "FOR XML AUTO"</param>
		/// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
		/// <returns>an XmlReader containing the resultset generated by the command</returns>
		public static XmlReader ExecuteXmlReader(SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
		{
			//create a command and prepare it for execution
			SqlCommand cmd = new SqlCommand();
			PrepareCommand(cmd, connection, transaction, commandType, commandText, commandParameters);
			
			//create the DataAdapter & DataSet
			return cmd.ExecuteXmlReader();
		}

		/// <summary>
		/// Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified SqlConnection 
		/// and SqlTransaction using the provided parameter values.  This method will query the database to discover the parameters for the 
		/// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
		/// </summary>
		/// <remarks>
		/// This method provides no access to output parameters or the stored procedure's return value parameter.
		/// 
		/// e.g.:  
		///  XmlReader r = ExecuteXmlReader(conn, trans, "GetOrders", 24, 36);
		/// </remarks>
		/// <param name="connection">a valid SqlConnection</param>
		/// <param name="transaction">a valid SqlTransaction associated with the connection</param>
		/// <param name="spName">the name of the stored prcedure</param>
		/// <param name="parameterValues">an array of objects to be assigned as the input values of the stored procedure</param>
		/// <returns>a dataset containing the resultset generated by the command</returns>
		public static XmlReader ExecuteXmlReader(SqlConnection connection, SqlTransaction transaction, string spName, params object[] parameterValues)
		{
			//if we got parameter values, we need to figure out where they go
			if ((parameterValues != null) && (parameterValues.Length > 0)) 
			{
				//pull the parameters for this stored procedure from the parameter cache (or discover them & populet the cache)
				SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(connection.ConnectionString, spName);

				//assign the provided values to these parameters based on parameter order
				AssignParameterValues(commandParameters, parameterValues);

				//call the overload that takes an array of SqlParameters
				return ExecuteXmlReader(connection, transaction, CommandType.StoredProcedure, spName, commandParameters);
			}
				//otherwise we can just call the SP without params
			else 
			{
				return ExecuteXmlReader(connection, transaction, CommandType.StoredProcedure, spName);
			}
		}


		#endregion ExecuteXmlReader
	}

	/// <summary>
	/// SqlHelperParameterCache provides functions to leverage a static cache of procedure parameters, and the
	/// ability to discover parameters for stored procedures at run-time.
	/// </summary>
	public sealed class SqlHelperParameterCache
	{
		#region private methods, variables, and constructors

		//Since this class provides only static methods, make the default constructor private to prevent 
		//instances from being created with "new SqlHelperParameterCache()".
		private SqlHelperParameterCache() {}

		//these hashtables are used to map the sp_procedure_params_rowset resultset to the SqlCommand property enum values
		private static Hashtable paramCache = Hashtable.Synchronized(new Hashtable());
		private static Hashtable paramTypes = Hashtable.Synchronized(new Hashtable());
		private static Hashtable paramDirections = Hashtable.Synchronized(new Hashtable());

		/// <summary>
		/// resolve at run-time the appropriate set of SqlParameters for a stored procedure
		/// </summary>
		/// <param name="connectionString">a valid connection string for a SqlConnection</param>
		/// <param name="spName">the name of the stored prcedure</param>
		/// <param name="includeReturnValueParameter">weather or not to onclude ther return value parameter</param>
		/// <returns></returns>
		private static SqlParameter[] DiscoverSpParameterSet(string connectionString, string spName, bool includeReturnValueParameter)
		{
			DataTable paramDescriptions = new DataTable("paramDescriptions");
			using (SqlConnection cn = new SqlConnection(connectionString))
			{
				cn.Open();
				SqlCommand cmd = new SqlCommand("sp_procedure_params_rowset",cn);
				cmd.CommandType = CommandType.StoredProcedure;
				//cmd.Parameters.Add("@procedure_name", spName);
                cmd.Parameters.AddWithValue("@procedure_name", spName);
				SqlDataAdapter da = new SqlDataAdapter(cmd);
				da.Fill(paramDescriptions);
			}

			SqlParameter[] discoveredParameters;
			
			if (paramDescriptions.Rows.Count <= 0) 
			{
				//sp not found - throw exception
				throw(new ArgumentException("Stored procedure '" + spName + "' not found", "spName"));
			}

			int startRow;
			if (includeReturnValueParameter) 
			{
				discoveredParameters = new SqlParameter[paramDescriptions.Rows.Count];
				startRow = 0;
			}
			else
			{
				discoveredParameters = new SqlParameter[paramDescriptions.Rows.Count-1];
				startRow = 1;
			}

			for (int i = 0, j = discoveredParameters.Length; i < j; i++)
			{
				DataRow paramRow =  paramDescriptions.Rows[i + startRow];
				discoveredParameters[i] = new SqlParameter();
				discoveredParameters[i].ParameterName = (string)paramRow["PARAMETER_NAME"];
				discoveredParameters[i].SqlDbType = (SqlDbType)paramTypes[(string)paramRow["TYPE_NAME"]];
				discoveredParameters[i].Direction = (ParameterDirection)paramDirections[(short)paramRow["PARAMETER_TYPE"]]; 
				discoveredParameters[i].Size = paramRow["CHARACTER_OCTET_LENGTH"]==DBNull.Value ? 0 : (int)paramRow["CHARACTER_OCTET_LENGTH"];
				discoveredParameters[i].Precision = paramRow["NUMERIC_PRECISION"]==DBNull.Value ? (byte)0 : (byte)(short)paramRow["NUMERIC_PRECISION"];
				discoveredParameters[i].Scale = paramRow["NUMERIC_SCALE"]==DBNull.Value ? (byte)0 : (byte)(short)paramRow["NUMERIC_SCALE"];
			}

			return discoveredParameters;
		}

		static SqlHelperParameterCache()
		{
			//populate the mapping hashtables
			paramTypes.Add("bigint",SqlDbType.BigInt);
			paramTypes.Add("binary",SqlDbType.Binary);
			paramTypes.Add("bit",SqlDbType.Bit);
			paramTypes.Add("char",SqlDbType.Char);
			paramTypes.Add("datetime",SqlDbType.DateTime);
			paramTypes.Add("decimal",SqlDbType.Decimal);
			paramTypes.Add("float",SqlDbType.Float);
			paramTypes.Add("image",SqlDbType.Image);
			paramTypes.Add("int",SqlDbType.Int);
			paramTypes.Add("money",SqlDbType.Money);
			paramTypes.Add("nchar",SqlDbType.NChar);
			paramTypes.Add("ntext",SqlDbType.NText);
			paramTypes.Add("numeric",SqlDbType.Decimal);
			paramTypes.Add("nvarchar",SqlDbType.NVarChar);
			paramTypes.Add("real",SqlDbType.Real);
			paramTypes.Add("smalldatetime",SqlDbType.SmallDateTime);
			paramTypes.Add("smallint",SqlDbType.SmallInt);
			paramTypes.Add("smallmoney",SqlDbType.SmallMoney);
			paramTypes.Add("sql_variant",SqlDbType.Variant);
			paramTypes.Add("text",SqlDbType.Text);
			paramTypes.Add("timestamp",SqlDbType.Timestamp);
			paramTypes.Add("tinyint",SqlDbType.TinyInt);
			paramTypes.Add("uniqueidentifier",SqlDbType.UniqueIdentifier);
			paramTypes.Add("varbinary",SqlDbType.VarBinary);
			paramTypes.Add("varchar",SqlDbType.VarChar);

			paramDirections.Add((short)1,ParameterDirection.Input);
			paramDirections.Add((short)2,ParameterDirection.InputOutput);
			paramDirections.Add((short)4,ParameterDirection.ReturnValue);

		}
		//deep copy of cached SqlParameter array
		private static SqlParameter[] CloneParameters(SqlParameter[] originalParameters)
		{
			SqlParameter[] clonedParameters = new SqlParameter[originalParameters.Length];

			for (int i = 0, j = originalParameters.Length; i < j; i++)
			{
				clonedParameters[i] = (SqlParameter)((ICloneable)originalParameters[i]).Clone();
			}

			return clonedParameters;
		}

		#endregion private methods, variables, and constructors

		#region caching functions

		/// <summary>
		/// add parameter array to the cache
		/// </summary>
		/// <param name="connectionString">a valid connection string for a SqlConnection</param>
		/// <param name="commandText">the stored procedure name or T-SQL command</param>
		/// <param name="commandParameters">an array of SqlParamters to be cached</param>
		public static void CacheParameterSet(string connectionString, string commandText, params SqlParameter[] commandParameters)
		{
			string hashKey = connectionString + ":" + commandText;

			paramCache[hashKey] = commandParameters;
		}

		/// <summary>
		/// retrieve a parameter array from the cache
		/// </summary>
		/// <param name="connectionString">a valid connection string for a SqlConnection</param>
		/// <param name="commandText">the stored procedure name or T-SQL command</param>
		/// <returns>an array of SqlParamters</returns>
		public static SqlParameter[] GetCachedParameterSet(string connectionString, string commandText)
		{
			string hashKey = connectionString + ":" + commandText;

			SqlParameter[] cachedParameters = (SqlParameter[])paramCache[hashKey];
			
			if (cachedParameters == null)
			{			
				return null;
			}
			else
			{
				return CloneParameters(cachedParameters);
			}
		}

		#endregion caching functions

		#region Parameter Discovery Functions

		/// <summary>
		/// Retrieves the set of SqlParameters appropriate for the stored procedure
		/// </summary>
		/// <remarks>
		/// This method will query the database for this information, and then store it in a cache for future requests.
		/// </remarks>
		/// <param name="connectionString">a valid connection string for a SqlConnection</param>
		/// <param name="spName">the name of the stored prcedure</param>
		/// <returns>an array of SqlParameters</returns>
		public static SqlParameter[] GetSpParameterSet(string connectionString, string spName)
		{
			return GetSpParameterSet(connectionString, spName, false);
		}

		/// <summary>
		/// Retrieves the set of SqlParameters appropriate for the stored procedure
		/// </summary>
		/// <remarks>
		/// This method will query the database for this information, and then store it in a cache for future requests.
		/// </remarks>
		/// <param name="connectionString">a valid connection string for a SqlConnection</param>
		/// <param name="spName">the name of the stored prcedure</param>
		/// <param name="includeReturnValueParameter">a bool value indicating weather the return value parameter should be included in the results</param>
		/// <returns>an array of SqlParameters</returns>
		public static SqlParameter[] GetSpParameterSet(string connectionString, string spName, bool includeReturnValueParameter)
		{
			string hashKey = connectionString + ":" + spName + (includeReturnValueParameter ? ":include ReturnValue Parameter":"");

			SqlParameter[] cachedParameters;
			
			cachedParameters = (SqlParameter[])paramCache[hashKey];

			if (cachedParameters == null)
			{			
				cachedParameters = (SqlParameter[])(paramCache[hashKey] = DiscoverSpParameterSet(connectionString, spName, includeReturnValueParameter));
			}
			
			return CloneParameters(cachedParameters);
		}

		#endregion Parameter Discovery Functions

	}
}
