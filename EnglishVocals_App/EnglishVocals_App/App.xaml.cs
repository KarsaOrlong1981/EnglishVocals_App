using EnglishVocals_App.Models;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EnglishVocals_App
{
    public partial class App : Application
    {
        static Database dbTrue;
        static Database dbFalse;
        public static NavigationPage NavPage { get; set; }
        public static Database DatabaseTrue
        {
            get
            {
                if (dbTrue == null)
                {
                    dbTrue = new Database(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TrueAnswers.db7"));
                }
                return dbTrue;
            }
        }
        public static Database DatabaseFalse
        {
            get
            {
                if (dbFalse == null)
                {
                    dbFalse = new Database(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "FalseAnswers.db7"));
                }
                return dbFalse;
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
