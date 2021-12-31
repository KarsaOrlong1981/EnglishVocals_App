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
        public ICommand BTN_Children { get; set; }
        public ICommand BTN_FalseAnswers { get; set; }
        private MainPage mainpage;
        private SpeakText speak;
        private Button bt_Eng_Ger, bt_Ger_Eng, bt_BackToMain,bt_Hotel, bt_Hobby, bt_School;
        private Button bt_Complex, bt_Phrase, bt_Restaurant, bt_Sport, bt_AllTopics;
        private Button bt_AllTopicsChildren, bt_Countrys, bt_Numbers, bt_ABC, bt_Persons, bt_Vehicles, bt_Colors;
        private Button bt_Month, bt_Words;
        private bool isVisGrid, isVisBtn;
        private Grid grid;
        private bool advancedChildren, children, advanced;
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
            children = false;
            advanced = false;   
            advancedChildren = false;
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
            BTN_Children = new Command(ChildrenEnglish);
            
        }
        private void LoadTrueAnswersPage()
        {
            CallTrueAnswerspage();
        }
        private void LoadFalseAnswersPage()
        {
            CallFalseAnswersPage();
        }
        private void ChildrenEnglish()
        { 
            children = true;
            advanced = false;
            advancedChildren = true;
            IsVisBtn = false;
            mainpage.Title = "Themen Auswahl";
            bt_AllTopicsChildren = CreateNewButton("Alle Themen", "alleZufall");
            bt_AllTopicsChildren.Clicked += Bt_AllTopicsChildren_Clicked;
            bt_ABC  = CreateNewButton("ABC", "abc");
            bt_ABC.Clicked += Bt_ABC_Clicked;
            bt_Countrys = CreateNewButton("Länder", "laender");
            bt_Countrys.Clicked += Bt_Countrys_Clicked; ; 
            bt_Numbers = CreateNewButton("Zahlen", "zahlen");
            bt_Numbers.Clicked += Bt_Numbers_Clicked;
            bt_Colors = CreateNewButton("Farben", "farben");
            bt_Colors.Clicked += Bt_Colors_Clicked;
            bt_Persons  = CreateNewButton("Personen", "person");
            bt_Persons.Clicked += Bt_Persons_Clicked;
            bt_Vehicles = CreateNewButton("Fahrzeuge", "fahrzeuge");
            bt_Vehicles.Clicked += Bt_Vehicles_Clicked;
            bt_Month = CreateNewButton("Monate/Jahreszeiten", "jahresZeit");
            bt_Month.Clicked += Bt_Month_Clicked;
            bt_Words = CreateNewButton("Wichtige Wörter", "worte");
            bt_Words.Clicked += Bt_Words_Clicked;
            Grid.SetRow(bt_AllTopicsChildren, 0); 
            Grid.SetRow(bt_ABC, 1);
            Grid.SetRow(bt_Countrys, 2);
            Grid.SetRow(bt_Numbers, 3);
            Grid.SetRow(bt_Colors, 4);
            Grid.SetRow(bt_Persons, 5);
            Grid.SetRow(bt_Vehicles, 6);
            Grid.SetRow(bt_Month, 7);
            Grid.SetRow(bt_Words, 8);
            grid.Children.Add(bt_AllTopicsChildren);
            grid.Children.Add(bt_ABC);
            grid.Children.Add(bt_Countrys);
            grid.Children.Add(bt_Numbers);
            grid.Children.Add(bt_Colors);
            grid.Children.Add(bt_Persons);
            grid.Children.Add(bt_Vehicles);
            grid.Children.Add(bt_Month);   
            grid.Children.Add(bt_Words);
        }

        private void Bt_Words_Clicked(object sender, EventArgs e)
        {
           
        }

        private void Bt_Month_Clicked(object sender, EventArgs e)
        {
           
        }

        private void Bt_AllTopicsChildren_Clicked(object sender, EventArgs e)
        {
            topic = "kinderAlleThemen.txt";
            SelectionMenue();
        }

        private void Bt_Vehicles_Clicked(object sender, EventArgs e)
        {
            topic = "fahrzeuge.txt";
            SelectionMenue();
        }

        private void Bt_Persons_Clicked(object sender, EventArgs e)
        {
          
        }

        private void Bt_Colors_Clicked(object sender, EventArgs e)
        {
           
        }

        private void Bt_Numbers_Clicked(object sender, EventArgs e)
        {
            topic = "zahlen.txt";
            SelectionMenue();
        }

        private void Bt_Countrys_Clicked(object sender, EventArgs e)
        {
            topic = "laender.txt";
            SelectionMenue();
        }

        private void Bt_ABC_Clicked(object sender, EventArgs e)
        {
            topic = "ABC.txt";
            SelectionMenue();
        }

        private void AdvancedEnglish()
        {
            advanced = true;
            children = false;
            advancedChildren = true;
            IsVisBtn = false;
            mainpage.Title = "Themen Auswahl";
            bt_AllTopics = CreateNewButton("Alle Themen gemischt", string.Empty);
            bt_AllTopics.Clicked += Bt_AllTopics_Clicked;
            bt_Hobby = CreateNewButton("Hobby und Freizeit", string.Empty);
            bt_Hobby.Clicked += Bt_Hobby_Clicked;
            bt_Hotel = CreateNewButton("Im Hotel", string.Empty);
            bt_Hotel.Clicked += Bt_Hotel_Clicked;
            bt_School = CreateNewButton("Schule", string.Empty);
            bt_School.Clicked += Bt_School_Clicked;
            bt_Complex = CreateNewButton("Komplexe Sätze", string.Empty);
            bt_Complex.Clicked += Bt_Complex_Clicked;
            bt_Phrase = CreateNewButton("Redewendungen/Begrüßung", string.Empty);
            bt_Phrase.Clicked += Bt_Phrase_Clicked;
            bt_Restaurant = CreateNewButton("Im Restaurant", string.Empty);
            bt_Restaurant.Clicked += Bt_Restaurant_Clicked;
            bt_Sport = CreateNewButton("Sport", string.Empty) ;
            bt_Sport.Clicked += Bt_Sport_Clicked;
            Grid.SetRow(bt_AllTopics, 0);
            Grid.SetRow(bt_Hobby , 1);
            Grid.SetRow(bt_Hotel, 2);
            Grid.SetRow(bt_School, 3);
            Grid.SetRow(bt_Complex, 4);
            Grid.SetRow(bt_Phrase, 5);
            Grid.SetRow(bt_Restaurant, 6);
            Grid.SetRow(bt_Sport, 7);
            grid.Children.Add(bt_AllTopics);
            grid.Children.Add(bt_Hobby);
            grid.Children.Add(bt_Hotel);
            grid.Children.Add(bt_School);
            grid.Children.Add(bt_Complex);
            grid.Children.Add(bt_Phrase);
            grid.Children.Add(bt_Restaurant);
            grid.Children.Add(bt_Sport);
        }

        private void Bt_AllTopics_Clicked(object sender, EventArgs e)
        {
            topic = "AlleThemen.txt";
            SelectionMenue();
        }

        private void Bt_Sport_Clicked(object sender, EventArgs e)
        {
            topic = "Sport.txt";
            SelectionMenue();
        }

        private void Bt_Restaurant_Clicked(object sender, EventArgs e)
        {
            topic = "ImRestaurant.txt";
            SelectionMenue();
        }

        private void Bt_Phrase_Clicked(object sender, EventArgs e)
        {
            topic = "Redewendungen.txt";
            SelectionMenue();
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
            if (advanced)
            {
                bt_School.IsVisible = false;
                bt_Hotel.IsVisible = false;
                bt_Hobby.IsVisible = false;
                bt_Restaurant.IsVisible = false;
                bt_Phrase.IsVisible = false;
                bt_Complex.IsVisible = false;
                bt_AllTopics.IsVisible = false;
                bt_Sport.IsVisible = false;
            }
            if (children)
            {
                bt_Month.IsVisible = false;
                bt_Numbers.IsVisible = false;
                bt_Persons.IsVisible = false;
                bt_ABC.IsVisible = false;
                bt_AllTopicsChildren.IsVisible = false;
                bt_Colors.IsVisible = false;
                bt_Countrys.IsVisible = false;
                bt_Vehicles.IsVisible = false;
                bt_Words.IsVisible = false;
            }
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
            CallVocalsView(1, advancedChildren, topic);
        }

        private void Eng_German_Clicked(object sender, EventArgs e)
        {
            CallVocalsView(2, advancedChildren, topic);
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
