using System;
using System.Diagnostics;

namespace Model.Entities
{
    public class Logger
    {
        private static readonly string Nl = Environment.NewLine;

        public Logger()
        {
            Trace.AutoFlush = true;
        }

        public static void Write(string msg)
        {
            Trace.Write(msg + Nl);
        }

        public static void TraceWrite(string msg)
        {
            Write("call site" + "call method" + msg + Nl);
        }

        public static void TraceWrite(string msg, Exception ex)
        {
            Trace.Write(ex.Message);
            Write("call site" + "call method" + msg + Nl);
        }
    }
}