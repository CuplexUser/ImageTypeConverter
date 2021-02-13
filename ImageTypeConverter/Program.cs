using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Autofac;
using ImageConverterLib.ConfigHelper;
using ImageConverterLib.Configuration;
using ImageTypeConverter.Configuration;
using Serilog;


namespace ImageTypeConverter
{
    static class Program
    {
        [DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();
        private static IContainer Container { get; set; }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            InitializeAutofac();

            if (Environment.OSVersion.Version.Major >= 6)
                SetProcessDPIAware();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(true);
            bool debugMode = ApplicationBuildConfig.DebugMode;
            GlobalSettings.Settings.Initialize(Assembly.GetExecutingAssembly().GetName().Name, !debugMode);
            Debug.WriteLine(GlobalSettings.Settings.InstanceID);

            Log.Information("Application started");

            using (var scope = Container.BeginLifetimeScope())
            {
                try
                {
                    MainForm frmMain = scope.Resolve<MainForm>();
                    Debug.WriteLine(GlobalSettings.Settings.InstanceID);
                    Application.Run(frmMain);
                }
                catch (Exception ex)
                {
                    Log.Fatal(ex, "Main program failureException: {Message}", ex.Message);
                }
            }

            //Application.Run(new FormMain());
            Log.Information("Application ended");
        }

        private static void InitializeAutofac()
        {
            Container = AutofacConfig.CreateContainer();
        }
    }
}
