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
        private MainPage mainpage;
        private SpeakText speak;
        private Button bt_Eng_Ger, bt_Ger_Eng, bt_BackToMain;
        private bool isVisGrid, isVisBtn;
        
        private Grid grid;
        public MainPageViewModel(INavigation navigation, Grid grid, MainPage mainPage)
        {
            this.Navigation = navigation;
            this.grid = grid;  
            this.mainpage = mainPage;
            speak = new SpeakText();
            //WelcomeTextEnglish("Hello, welcome to this App. Let's start learning English.");
            //WelcomeTextGerman("Hallo willkommen zu dieser App. Lass uns mit dem Englisch lernen starten.");
            IsVisGrid = true;
            IsVisBtn = true;
            BTN_LearnEnglish = new Command(() => SelectionMenue());
            //async () => await Navigation.PushAsync(new VocalsView())
        }
        private void SelectionMenue()
        {
            IsVisBtn = false;
            mainpage.Title = "Optionen";
            Button eng_German = new Button 
            { 
                Text = "Englisch - Deutsch",
                ImageSource = "england",
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
            eng_German.Clicked += Eng_German_Clicked;
            Button ger_English = new Button
            {
                Text = "Deutsch - Englisch",
                ImageSource = "deutschland",
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
            ger_English.Clicked += Ger_English_Clicked;
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
            bt_Eng_Ger = eng_German;
            bt_Ger_Eng = ger_English;
            Grid.SetRow(bt_Eng_Ger, 1);
            Grid.SetRow(bt_Ger_Eng, 2);
            Grid.SetRow(bt_BackToMain, 3);
            grid.Children.Add(eng_German);
            grid.Children.Add(ger_English);
            grid.Children.Add(btn_BackToMain);
            
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
            CallVocalsView(1);
        }

        private void Eng_German_Clicked(object sender, EventArgs e)
        {
            CallVocalsView(2);
        }

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
        private async void CallVocalsView(int switchGerEng)
        {
            VocalsView vV = new VocalsView(switchGerEng);
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
