using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace TransferMoneyHomeWork
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static Mutex _mutex = null;
        public static bool IsProgressRun = false;
        protected override void OnStartup(StartupEventArgs e)
        {
            
            const string appName = "MyAppName";
            bool createdNew;
            
            _mutex = new Mutex(true, appName, out createdNew);

            //For test

            //if (!createdNew)
            //{
            //    //app is already running! Exiting the application  
            //    MessageBox.Show("App is already running");
            //    Application.Current.Shutdown();
            //}

            base.OnStartup(e);
        }

    }
}
