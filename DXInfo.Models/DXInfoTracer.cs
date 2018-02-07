using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Data;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Threading;
using System.Reflection;

namespace DXInfo.Models
{
    public static class DXInfoTracer
    {
        private enum LogLevels
        {
            None,
            Error,
            Warning,
            Info,
            Verbose
        }
        private const string dateFormat = "yyyy-MM-dd HH:mm:ss:fff";
        private const string indentString = "   ";
        private static readonly string procName = Process.GetCurrentProcess().ProcessName;
        private static readonly TraceSwitch traceSwitch = new TraceSwitch("DXInfoTracer", null);
        public static bool IsInfoEnabled()
        {
            return DXInfoTracer.traceSwitch.TraceInfo;
        }
        public static void Info(string format, params object[] args)
        {
            if (DXInfoTracer.IsInfoEnabled())
            {
                DXInfoTracer.Info(string.Format(CultureInfo.InvariantCulture, format, args));
            }
        }
        public static void Info(string message)
        {
            if (DXInfoTracer.IsInfoEnabled())
            {
                DXInfoTracer.TraceLine(DXInfoTracer.LogLevels.Info, message);
            }
        }
        public static void Info(int indentLevel, string format, params object[] args)
        {
            if (DXInfoTracer.IsInfoEnabled())
            {
                for (int i = 0; i < indentLevel; i++)
                {
                    format = "   " + format;
                }
                DXInfoTracer.Info(string.Format(CultureInfo.InvariantCulture, format, args));
            }
        }
        public static void Info(int indentLevel, string message)
        {
            if (DXInfoTracer.IsInfoEnabled())
            {
                for (int i = 0; i < indentLevel; i++)
                {
                    message = "   " + message;
                }
                DXInfoTracer.TraceLine(DXInfoTracer.LogLevels.Info, message);
            }
        }
        public static bool IsErrorEnabled()
        {
            return DXInfoTracer.traceSwitch.TraceError;
        }
        public static void Error(string format, params object[] args)
        {
            if (DXInfoTracer.IsErrorEnabled())
            {
                DXInfoTracer.Error(string.Format(CultureInfo.InvariantCulture, format, args));
            }
        }
        public static void Error(string message)
        {
            if (DXInfoTracer.IsErrorEnabled())
            {
                DXInfoTracer.TraceLine(DXInfoTracer.LogLevels.Error, message);
            }
        }
        public static void Error(int indentLevel, string format, params object[] args)
        {
            if (DXInfoTracer.IsErrorEnabled())
            {
                for (int i = 0; i < indentLevel; i++)
                {
                    format = "   " + format;
                }
                DXInfoTracer.Error(string.Format(CultureInfo.InvariantCulture, format, args));
            }
        }
        public static void Error(int indentLevel, string message)
        {
            if (DXInfoTracer.IsErrorEnabled())
            {
                for (int i = 0; i < indentLevel; i++)
                {
                    message = "   " + message;
                }
                DXInfoTracer.TraceLine(DXInfoTracer.LogLevels.Error, message);
            }
        }
        public static bool IsWarningEnabled()
        {
            return DXInfoTracer.traceSwitch.TraceWarning;
        }
        public static void Warning(string format, params object[] args)
        {
            if (DXInfoTracer.IsWarningEnabled())
            {
                DXInfoTracer.Warning(string.Format(CultureInfo.InvariantCulture, format, args));
            }
        }
        public static void Warning(string message)
        {
            if (DXInfoTracer.IsWarningEnabled())
            {
                DXInfoTracer.TraceLine(DXInfoTracer.LogLevels.Warning, message);
            }
        }
        public static void Warning(int indentLevel, string format, params object[] args)
        {
            if (DXInfoTracer.IsWarningEnabled())
            {
                for (int i = 0; i < indentLevel; i++)
                {
                    format = "   " + format;
                }
                DXInfoTracer.Warning(string.Format(CultureInfo.InvariantCulture, format, args));
            }
        }
        public static void Warning(int indentLevel, string message)
        {
            if (DXInfoTracer.IsWarningEnabled())
            {
                for (int i = 0; i < indentLevel; i++)
                {
                    message = "   " + message;
                }
                DXInfoTracer.TraceLine(DXInfoTracer.LogLevels.Warning, message);
            }
        }
        public static bool IsVerboseEnabled()
        {
            return DXInfoTracer.traceSwitch.TraceVerbose;
        }
        public static void Verbose(string format, params object[] args)
        {
            if (DXInfoTracer.IsVerboseEnabled())
            {
                DXInfoTracer.Verbose(string.Format(CultureInfo.InvariantCulture, format, args));
            }
        }
        public static void Verbose(string message)
        {
            if (DXInfoTracer.IsVerboseEnabled())
            {
                DXInfoTracer.TraceLine(DXInfoTracer.LogLevels.Verbose, message);
            }
        }
        public static void Verbose(int indentLevel, string format, params object[] args)
        {
            if (DXInfoTracer.IsVerboseEnabled())
            {
                for (int i = 0; i < indentLevel; i++)
                {
                    format = "   " + format;
                }
                DXInfoTracer.Verbose(string.Format(CultureInfo.InvariantCulture, format, args));
            }
        }
        public static void Verbose(int indentLevel, string message)
        {
            if (DXInfoTracer.IsVerboseEnabled())
            {
                for (int i = 0; i < indentLevel; i++)
                {
                    message = "   " + message;
                }
                DXInfoTracer.TraceLine(DXInfoTracer.LogLevels.Verbose, message);
            }
        }
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        private static void TraceLine(DXInfoTracer.LogLevels level, string message)
        {
            string text;
            switch (level)
            {
                case DXInfoTracer.LogLevels.Error:
                    text = "ERROR  ";
                    break;
                case DXInfoTracer.LogLevels.Warning:
                    text = "WARNING";
                    break;
                case DXInfoTracer.LogLevels.Info:
                    text = "INFO   ";
                    break;
                case DXInfoTracer.LogLevels.Verbose:
                    text = "VERBOSE";
                    break;
                default:
                    text = "DEFAULT";
                    break;
            }
            string message2 = string.Format(CultureInfo.InvariantCulture, "{0}, {1}, {2}, {3}, {4}", new object[]
			{
				text,
				DXInfoTracer.procName,
				Thread.CurrentThread.ManagedThreadId,
				DateTime.UtcNow.ToString("MM/dd/yyyy HH:mm:ss:fff", CultureInfo.InvariantCulture),
				message
			});
            try
            {
                Trace.WriteLine(message2);
            }
            catch (Exception ex)
            {
                text = "WARNING";
                string message3 = string.Format(CultureInfo.InvariantCulture, "{0}, {1}, {2}, {3}, {4} {5}", new object[]
				{
					text,
					DXInfoTracer.procName,
					Thread.CurrentThread.ManagedThreadId,
					DateTime.UtcNow.ToString("MM/dd/yyyy HH:mm:ss:fff", CultureInfo.InvariantCulture),
					"Caught Exception while trying to write to Trace Log:",
					ex
				});
                Trace.WriteLine(message3);
            }
        }
        //internal static void TraceV1ReceivedAnchorOperation(string table, string operation, SyncAnchor anchorValue)
        //{
        //    if (DXInfoTracer.IsVerboseEnabled())
        //    {
        //        string text = "NULL";
        //        if (anchorValue != null && anchorValue.Anchor != null)
        //        {
        //            MemoryStream serializationStream = new MemoryStream(anchorValue.Anchor);
        //            BinaryFormatter binaryFormatter = new BinaryFormatter();
        //            object obj = binaryFormatter.Deserialize(serializationStream);
        //            byte[] array = obj as byte[];
        //            if (array != null)
        //            {
        //                text = BitConverter.ToString(array);
        //            }
        //            else
        //            {
        //                text = obj.ToString();
        //            }
        //        }
        //        DXInfoTracer.Verbose("{0}: {1} ReceivedAnchor value: {2}", new object[]
        //        {
        //            table,
        //            operation,
        //            text
        //        });
        //    }
        //}
        //internal static void TraceV1SentAnchorOperation(string table, string operation, SyncAnchor anchorValue)
        //{
        //    if (DXInfoTracer.IsVerboseEnabled())
        //    {
        //        string text = "NULL";
        //        if (anchorValue != null && anchorValue.Anchor != null)
        //        {
        //            text = BitConverter.ToUInt64(anchorValue.Anchor, 0).ToString(CultureInfo.InvariantCulture);
        //        }
        //        DXInfoTracer.Verbose("{0}: {1} SentAnchor value: {2}", new object[]
        //        {
        //            table,
        //            operation,
        //            text
        //        });
        //    }
        //}
        internal static void TraceCommandAndParameters(IDbCommand command)
        {
            DXInfoTracer.TraceCommandAndParameters(1, command);
        }
        internal static void TraceCommandAndParameters(int indentLevel, IDbCommand command)
        {
            if (!DXInfoTracer.IsVerboseEnabled())
            {
                return;
            }
            DXInfoTracer.Verbose(indentLevel, "Executing Command: {0}", new object[]
			{
				command.CommandText
			});
            foreach (DbParameter parameter in command.Parameters)
            {
                DXInfoTracer.TraceCommandParameter(indentLevel + 1, parameter);
            }
        }
        internal static void TraceCommandResultCount(int count)
        {
            DXInfoTracer.TraceCommandResultCount(1, count);
        }
        internal static void TraceCommandResultCount(int indentLevel, int count)
        {
            DXInfoTracer.Verbose(indentLevel, "Rows affected: {0}", new object[]
			{
				count
			});
        }
        internal static void TraceCommandParameter(DbParameter parameter)
        {
            DXInfoTracer.TraceCommandParameter(1, parameter);
        }
        internal static void TraceCommandParameter(int indentLevel, DbParameter parameter)
        {
            if (!DXInfoTracer.IsVerboseEnabled())
            {
                return;
            }
            if (parameter.Direction != ParameterDirection.Input && parameter.Direction != ParameterDirection.InputOutput)
            {
                DXInfoTracer.Verbose(indentLevel, "Parameter: {0} Value: Skipped since Not Input/InputOutput", new object[]
				{
					parameter.ParameterName
				});
                return;
            }
            string text = "";
            int num = parameter.Size;
            string text2;
            if (parameter == null || parameter.Value is DBNull)
            {
                text2 = "NULL";
            }
            else
            {
                if (parameter.Value is byte[])
                {
                    byte[] array = (byte[])parameter.Value;
                    num = array.Length;
                    if (array.Length == 0)
                    {
                        text2 = "<zero length value>";
                    }
                    else
                    {
                        text2 = BitConverter.ToString(array);
                    }
                }
                else
                {
                    text2 = parameter.Value.ToString();
                    Type type = parameter.Value.GetType();
                    PropertyInfo property = type.GetProperty("Length");
                    if (property != null && property.PropertyType.ToString().Equals("System.Int32"))
                    {
                        num = (int)property.GetValue(parameter.Value, null);
                    }
                }
            }
            string text3;
            if (text2.Length > 50)
            {
                text3 = text2.Substring(0, 50);
                text = "...";
            }
            else
            {
                text3 = text2;
            }
            string text4 = (num > 0) ? string.Format(CultureInfo.InvariantCulture, "Len: {0} ", new object[]
			{
				num
			}) : "";
            DXInfoTracer.Verbose(indentLevel, "Parameter: {0} {1}Value: {2}{3}", new object[]
			{
				parameter.ParameterName,
				text4,
				text3,
				text
			});
        }
    }
}
