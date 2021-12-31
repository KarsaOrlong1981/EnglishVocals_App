using EnglishVocals_App.Models;
using EnglishVocals_App.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace EnglishVocals_App.ViewModels
{
    public class MainPageViewModel : BaseVM
    {
        public INavigation Navigation { get; set; }
        public ICommand BTN_LearnEnglish { get; set; }
        public ICommand BTN_LearnAdvanced { get; set; }
        public ICommand BTN_TrueAnswers { get; set; }
        public ICommand BTN_FalseAnswers { get; set; }
        private MainPage mainpage;
        private SpeakText speak;
        private Button bt_Eng_Ger, bt_Ger_Eng, bt_BackToMain,bt_Hotel, bt_Hobby, bt_School;
        private Button bt_Complex;
        private bool isVisGrid, isVisBtn;
        private Grid grid;
        private bool advanced;
        private string topic;
        public bool IsVisGrid
        {
            get => isVisGrid;
            set => SetProperty(ref isVisGrid, value);
        }
        public bool IsVisBtn
        {
            get => isVisBtn;
            set => SetProperty(ref isVisBtn, value);
        }
        public MainPageViewModel(INavigation navigation, Grid grid, MainPage mainPage)
        {
            this.Navigation = navigation;
            this.grid = grid;  
            this.mainpage = mainPage;
            advanced = false;
            topic = string.Empty;
            speak = new SpeakText();
            //WelcomeTextEnglish("Hello, welcome to this App. Let's start learning English.");
            //WelcomeTextGerman("Hallo willkommen zu dieser App. Lass uns mit dem Englisch lernen starten.");
            IsVisGrid = true;
            IsVisBtn = true;
            BTN_LearnEnglish = new Command(() => SelectionMenue());
            BTN_LearnAdvanced = new Command(() => AdvancedEnglish());
            BTN_FalseAnswers = new Command(LoadFalseAnswersPage);
            BTN_TrueAnswers = new Command(LoadTrueAnswersPage);
            
        }
        private void LoadTrueAnswersPage()
        {
            CallTrueAnswerspage();
        }
        private void LoadFalseAnswersPage()
        {
            CallFalseAnswersPage();
        }
        private void AdvancedEnglish()
        {
            advanced = true;
            IsVisBtn = false;
            mainpage.Title = "Themen Auswahl";
            bt_Hobby = CreateNewButton("Hobby und Freizeit", string.Empty);
            bt_Hobby.Clicked += Bt_Hobby_Clicked;
            bt_Hotel = CreateNewButton("Im Hotel", string.Empty);
            bt_Hotel.Clicked += Bt_Hotel_Clicked;
            bt_School = CreateNewButton("Schule", string.Empty);
            bt_School.Clicked += Bt_School_Clicked;
            bt_Complex = CreateNewButton("Komplexe Sätze", string.Empty);
            bt_Complex.Clicked += Bt_Complex_Clicked;
            Grid.SetRow(bt_Hobby , 0);
            Grid.SetRow(bt_Hotel, 1);
            Grid.SetRow(bt_School, 2);
            grid.Children.Add(bt_Hobby);
            grid.Children.Add(bt_Hotel);
            grid.Children.Add(bt_School);
        }

        private void Bt_Complex_Clicked(object sender, EventArgs e)
        {
            topic = "komplex.txt";
            SelectionMenue();
        }

       
        private void Bt_School_Clicked(object sender, EventArgs e)
        {
            topic = "School.txt";
            SelectionMenue();
        }

        private void Bt_Hotel_Clicked(object sender, EventArgs e)
        {
            topic = "InTheHotel.txt";
            SelectionMenue();
        }

        private void Bt_Hobby_Clicked(object sender, EventArgs e)
        {
            topic = "Hobby_Freizeit.txt";
            SelectionMenue();
        }

        private void SelectionMenue()
        {
            IsVisBtn = false;
            bt_School.IsVisible = false;
            bt_Hotel.IsVisible = false;
            bt_Hobby.IsVisible = false;
            mainpage.Title = "Optionen";
            bt_Eng_Ger = CreateNewButton("Englisch - Deutsch", "england");
            bt_Eng_Ger.Clicked += Eng_German_Clicked;
            bt_Ger_Eng = CreateNewButton("Deutsch - Englisch", "deutschland");
            bt_Ger_Eng.Clicked += Ger_English_Clicked;
           
            Button btn_BackToMain = new Button
            {
                Text = "Zrück zum Hauptmenü",
                ImageSource = "back",
                ContentLayout = new Button.ButtonContentLayout(Button.ButtonContentLayout.ImagePosition.Left, 0),
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Margin = new Thickness(20),
                BackgroundColor = Color.Black,
                BorderColor = Color.SteelBlue,
                BorderWidth = 5,
                CornerRadius = 20,
                TextColor = Color.White
            };
            btn_BackToMain.Clicked += Btn_BackToMain_Clicked;
            bt_BackToMain = btn_BackToMain;
           
            Grid.SetRow(bt_Eng_Ger, 1);
            Grid.SetRow(bt_Ger_Eng, 2);
            Grid.SetRow(bt_BackToMain, 3);
            grid.Children.Add(bt_Eng_Ger);
            grid.Children.Add(bt_Ger_Eng);
            grid.Children.Add(bt_BackToMain);
            
        }
        private Button CreateNewButton(string txt, string imageSource)
        {
            Button bt = new Button
            {
                Text = txt,
                ImageSource = imageSource,
                ContentLayout = new Button.ButtonContentLayout(Button.ButtonContentLayout.ImagePosition.Top, 0),
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Margin = new Thickness(20),
                BackgroundColor = Color.Black,
                BorderColor = Color.SteelBlue,
                BorderWidth = 5,
                CornerRadius = 20,
                TextColor = Color.White
            };
            return bt;
        }
        private void Btn_BackToMain_Clicked(object sender, EventArgs e)
        {
            mainpage.Title = "Hauptmenü";
            bt_Ger_Eng.IsVisible = false;
            bt_Eng_Ger.IsVisible = false;
            bt_BackToMain.IsVisible = false;
            IsVisBtn = true;
        }

        // 1 = Deutsch-Englisch; 2 = Englisch-Deutsch
        private void Ger_English_Clicked(object sender, EventArgs e)
        {
            CallVocalsView(1, advanced, topic);
        }

        private void Eng_German_Clicked(object sender, EventArgs e)
        {
            CallVocalsView(2, advanced, topic);
        }

        private async void CallTrueAnswerspage()
        {
            if (App.DatabaseTrue.GetAllItemsAsync().Result.Count > 0)
            {
                TrueAnswersPage trueAnswersPage = new TrueAnswersPage();
                await Navigation.PushAsync(trueAnswersPage);
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Sorry", "Diese Datenbank ist Leer", "Ok");
            }
        }
        private async void CallFalseAnswersPage()
        {
            if (App.DatabaseFalse.GetAllItemsAsync().Result.Count > 0)
            {
                FalseAnswersPage falseAnswersPage = new FalseAnswersPage();
                await Navigation.PushAsync(falseAnswersPage);
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Sorry", "Diese Datenbank ist Leer", "Ok");
            }
        }
        private async void CallVocalsView(int switchGerEng, bool advanced, string topic)
        {
            VocalsView vV = new VocalsView(switchGerEng, advanced, topic);
            await Navigation.PushAsync(vV);
        }
        private async void WelcomeTextEnglish(string txt)
        {
           await speak.SpeakEnglish(txt);
        }
        private async void WelcomeTextGerman(string txt)
        {
            await speak.SpeakGerman(txt);
        }

    }
}
