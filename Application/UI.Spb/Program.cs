using Ninject;
using System;
using System.Windows.Forms;

namespace MeteringDevices.UI.Spb
{
    static class Program
    {
        public static IKernel Kernel { get; private set; }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Kernel = new StandardKernel();
            Kernel.Load(new Data.Register());

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
