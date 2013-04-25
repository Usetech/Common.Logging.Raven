using System;

namespace Common.Logging.Raven.Test
{
    class Program
    {
        static readonly ILog Log = LogManager.GetCurrentClassLogger();

        static void Main()
        {
            Log.Trace("TRACE!!!!!");
            Log.Debug("DEBUG!!!!!");
            Log.Info("INFO!!!!!!!");
            Log.Warn("WARNING!!!!");
            Log.Error("ERROR!!!!!");
            Log.Fatal("FATAL!!!!!");

            Log.Error("error!!!!", new Exception("Test without a stacktrace."));


            try
            {
                int i2 = 0;
                int i = 10 / i2;
            }
            catch (Exception e)
            {
                Log.Error("Division by zero", e);
            }
        }
    }
}
