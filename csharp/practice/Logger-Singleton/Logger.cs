// Logger system — poore application mein ek hi logger hona chahiye.
// Multiple classes se log karo, sab SAME file/console mein jaaye.
// Logger features:
// - Log(message) → timestamp ke saath print karo
// - SetLevel(level) → "INFO", "WARN", "ERROR"
// - Sirf us level aur usse upar ke logs dikhao
//   - INFO shows: INFO + WARN + ERROR
//   - WARN shows: WARN + ERROR
//   - ERROR shows: ERROR only

using System;
namespace LldPractice.CSharp.LoggerSingleton;

public enum Loglevel{
    INFO = 1,
    WARN = 2,
    ERROR = 3
}

class Logger
{
    private static Logger instance;
    private static readonly object lockObj = new object();
    private Loglevel level = Loglevel.INFO;

    private Logger(){}

    public static Logger GetInstance()
    {
        if(instance == null)
        {
            lock (lockObj)
            {
                if(instance == null) // do baal null check : why ? kyuki multiple thread ho skte h na isliye
                    instance = new Logger();
                // instance = new (); 
            }
        }
        return instance;
    }


    public void SetLevel(string level)
    {
        if (Enum.TryParse<Loglevel>(level, true, out var parsedLevel))
        {
            this.level = parsedLevel;
        }
        else
        {
            Console.WriteLine($"Invalid log level: {level}");
        }
    }


    private void Log(Loglevel level, string msg) // 1 template for all
    {
        Console.WriteLine($"[{level}] {DateTime.Now:yyyy-MM-dd, HH:mm:ss} - {msg}");
    }
    
    // 4. Hierarchy Checks (>= use kiya hai!)
        public void Info(string msg)
        {
            // Agar current level INFO(1) hai, toh (1 >= 1) True! Print hoga.
            if (Loglevel.INFO >= this.level)
                Log(Loglevel.INFO, msg);
        }
        public void Warn(string msg)
        {
            // Agar current INFO(1) hai, toh (2 >= 1) True!
            // Agar current WARN(2) hai, toh (2 >= 2) True!
            // Agar current ERROR(3) hai, toh (2 >= 3) False! Print nahi hoga.
            if (Loglevel.WARN >= this.level)
                Log(Loglevel.WARN, msg);
        }
        public void Error(string msg)
        {
            if (Loglevel.ERROR >= this.level)
                Log(Loglevel.ERROR, msg);
        }

}