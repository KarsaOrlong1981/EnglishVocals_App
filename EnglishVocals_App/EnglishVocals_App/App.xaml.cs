using EnglishVocals_App.Models;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EnglishVocals_App
{
    public partial class App : Application
    {
        static Database db;
        public static NavigationPage NavPage { get; set; }
        public static Database Database
        {
            get
            {
                if (db == null)
                {
                    db = new Database(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Worte.db1"));
                }
                return db;
            }
        }
        public App()
        {
            InitializeComponent();

            NavPage = new NavigationPage(new MainPage());
            MainPage = NavPage;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
