using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autofac;
using ImageConverterLib.ConfigHelper;
using ImageConverterLib.Configuration;
using ImageConverterLib.Services;
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
            GlobalSettings.Initialize(Assembly.GetExecutingAssembly().GetName().Name, !debugMode);

            Log.Verbose("Application started");

            using (var scope = Container.BeginLifetimeScope())
            {
                // Begin startup async jobs
                ApplicationSettingsService settingsService = scope.Resolve<ApplicationSettingsService>();

                //settingsService.

                        
                Task.Delay(1000);
                try
                {

                    MainForm frmMain = scope.Resolve<MainForm>();
                    Application.Run(frmMain);
                }
                catch (Exception ex)
                {
                    Log.Fatal(ex, "Main program failureException: {Message}", ex.Message);
                }



                //settingsService.SaveSettings();
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
