using System;
using System.IO;

class Test
{
    static void Main()
    {
        string logPath = Path.Combine(Path.GetTempPath(), "BetterDesktop_Test.log");
        File.WriteAllText(logPath, "Test started at " + DateTime.Now + "\r\n");

        try
        {
            // Try to load the handler DLL
            var asm = System.Reflection.Assembly.LoadFrom(
                @"F:\XiaoLv\ZcodeProject\.zcode\workspace\default\BetterDesktop\BetterDesktopHandler\bin\x64\Debug\BetterDesktopHandler.dll");
            File.AppendAllText(logPath, "DLL loaded: " + asm.FullName + "\r\n");

            // Try to create the handler type
            Type type = asm.GetType("BetterDesktop.InfoTipHandler");
            if (type != null)
            {
                File.AppendAllText(logPath, "Type found: " + type.FullName + "\r\n");
                object obj = Activator.CreateInstance(type);
                File.AppendAllText(logPath, "Instance created successfully!\r\n");
            }
            else
            {
                File.AppendAllText(logPath, "Type not found!\r\n");
                foreach (var t in asm.GetTypes())
                    File.AppendAllText(logPath, "  Available: " + t.FullName + "\r\n");
            }
        }
        catch (Exception ex)
        {
            File.AppendAllText(logPath, "ERROR: " + ex.ToString() + "\r\n");
        }
    }
}