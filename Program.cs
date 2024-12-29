using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Application = System.Windows.Forms.Application;

namespace WindowsFormsApp1
{
    internal static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        ///
        private static Mutex mutex;

        [STAThread]
        private static void Main()
        {
            mutex = new Mutex(true, "meteorseverbottool", out bool isNewInstance);
            if (!isNewInstance)
            {
                MessageBox.Show("运行中！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            Application.Run(new MySvrForm());
        }

        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            //MessageBox.Show("An error occurred: " + e.Exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            DateTime cTime = DateTime.Now;
            string m = $"{cTime:yyyy-MM-dd HH:mm:ss}\n{e.Exception.Message}\n{e.Exception.StackTrace}";
            string directory = AppDomain.CurrentDomain.BaseDirectory;
            string fullPath = Path.Combine(directory, "Error.log");
            using (StreamWriter writer = new StreamWriter(fullPath, true))
            {
                writer.WriteLine(m);
            }
        }
    }
}