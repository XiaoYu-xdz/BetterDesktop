using System;
using System.Runtime.InteropServices;

class ComTest
{
    [DllImport("ole32.dll")]
    static extern int CoCreateInstance(
        [In] ref Guid rclsid,
        IntPtr pUnkOuter,
        uint dwClsContext,
        [In] ref Guid riid,
        out IntPtr ppv);

    static Guid IID_IQueryInfo = new Guid("00021500-0000-0000-C000-000000000046");
    static Guid CLSID_InfoTipHandler = new Guid("B8E2D3F1-5A7C-4E9B-8D1F-3C6A9B2E7F4D");

    const uint CLSCTX_INPROC_SERVER = 1;

    [DllImport("ole32.dll")]
    static extern int CoInitializeEx(IntPtr pvReserved, uint dwCoInit);

    const uint COINIT_APARTMENTTHREADED = 2;
    const uint COINIT_DISABLE_OLE1DDE = 4;

    static void Main()
    {
        string logPath = System.IO.Path.Combine(
            Environment.GetEnvironmentVariable("TEMP"),
            "BetterDesktop_COM_Test.log");

        try
        {
            System.IO.File.WriteAllText(logPath, "COM Test starting...\r\n");

            CoInitializeEx(IntPtr.Zero, COINIT_APARTMENTTHREADED | COINIT_DISABLE_OLE1DDE);

            System.IO.File.AppendAllText(logPath, "Calling CoCreateInstance...\r\n");

            IntPtr ptr;
            int hr = CoCreateInstance(
                ref CLSID_InfoTipHandler,
                IntPtr.Zero,
                CLSCTX_INPROC_SERVER,
                ref IID_IQueryInfo,
                out ptr);

            System.IO.File.AppendAllText(logPath, string.Format("CoCreateInstance returned HRESULT=0x{0:X8}\r\n", hr));

            if (hr >= 0 && ptr != IntPtr.Zero)
            {
                System.IO.File.AppendAllText(logPath, "SUCCESS! Object created and IQueryInfo obtained.\r\n");
                Marshal.Release(ptr);
            }
            else
            {
                System.IO.File.AppendAllText(logPath, string.Format("FAILED! HRESULT=0x{0:X8}\r\n", hr));
            }
        }
        catch (Exception ex)
        {
            System.IO.File.AppendAllText(logPath, string.Format("EXCEPTION: {0}\r\n", ex));
        }

        Console.WriteLine("Done. Check log: " + logPath);
    }
}