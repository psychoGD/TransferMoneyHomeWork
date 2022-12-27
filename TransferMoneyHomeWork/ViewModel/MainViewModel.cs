using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using TransferMoneyHomeWork.RelayCommands;

namespace TransferMoneyHomeWork.ViewModel
{
    public class MainViewModel : BaseViewModel
    {

        public RelayCommand CloseCommand { get; set; }
        public RelayCommand InsertCardCommand { get; set; }
        public RelayCommand LoadDataCommand { get; set; }
        public RelayCommand TransferMoneyCommand { get; set; }
        public RelayCommand ShowTest { get; set; }


        public string CardId { get; set; }
        //User 
        public int BalanceInt { get; set; }
        private string username;

        public string Username
        {
            get { return username; }
            set { username = value; OnPropertyChanged(); }
        }

        private string balance;

        public string Balance
        {
            get { return balance; }
            set { balance = value; OnPropertyChanged(); }
        }


        private string flagEnabled;

        public string FlagEnabled
        {
            get { return flagEnabled; }
            set { flagEnabled = value; OnPropertyChanged(); }
        }

        private string message;

        public string Message
        {
            get { return message; }
            set { message = value; OnPropertyChanged(); }
        }


        public MainWindow mainWindow { get; set; }


        public DispatcherTimer dispatcherTimer { get; set; }
        public bool ProgramCanClose { get; set; } = true;

        public static Mutex mutex { get; set; }
        string mutexName = "TestMutex";
        static Object obj = new Object();
        public MainViewModel(MainWindow window)
        {
            dispatcherTimer = new DispatcherTimer();
            //This Is For Trigger Logic For Transfer Money
            dispatcherTimer.Tick += TransferMoneyFunc;
            dispatcherTimer.Interval = TimeSpan.FromSeconds(1);

            FlagEnabled = "False";
            mainWindow = window;

            mutex = new Mutex(true, mutexName);


            CloseCommand = new RelayCommand((o) =>
            {
                if (ProgramCanClose == true)
                    mainWindow.Close();
            });

            InsertCardCommand = new RelayCommand((o) =>
            {
                FlagEnabled = "True";
            });
            LoadDataCommand = new RelayCommand((o) =>
            {

                //This Is For Only Test
                Username = "John";
                Balance = "500";
                BalanceInt = int.Parse(Balance);

            });
            TransferMoneyCommand = new RelayCommand((o) =>
            {
                mutex.WaitOne();
                Balance = "500";
                ProgramCanClose = false;
                dispatcherTimer.Start();
                App.IsProgressRun = true;
                
            });

            ShowTest = new RelayCommand((o) =>
            {
                mainWindow.Close();
            });
        }



        //Logic For Transfer Money
        private void TransferMoneyFunc(object sender, EventArgs e)
        {
            var balance2 = double.Parse(Balance);
            if (balance2 != 0)
            {
                balance2 -= (BalanceInt / 100) * 10;
                Balance = balance2.ToString();
            }
            else
            {
                dispatcherTimer.Stop();
                ProgramCanClose = true;
                Message = "Transfer Completed Succesfully";
                mutex.ReleaseMutex();
            }
        }

    }
}
