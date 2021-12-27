using Android.Content.Res;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace EnglishVocals_App.Models
{
  
    public class Vocals
    {
        List<string> list;
        List<string> listGerman;
        List<string> listEnglish;
        Random random;
        Button bt_German,bt_English;
        Grid grid;
        Button btn_ShowAnswer;
        SpeakText speak;
        int switchGerEng;
        public Vocals()
        {
            list = new List<string>();
            speak = new SpeakText();
            random = new Random();
            switchGerEng = 0;
            ReadAsset();
        }
        //Die Methoden
        //Über eine Instanz dieser Klasse kann nur die methode GetRandomVocal aufgerufen werden
        public void GetRandomVocal(Grid grid, int switchGerEng)
        {
            this.grid = grid;
            this.switchGerEng = switchGerEng;
            listGerman = GetGermanVocals();
            listEnglish = GetEnglishVocals();
            int randomValue = random.Next(0, listGerman.Count);
            string vocalGerman = listGerman[randomValue];
            string vocalEnglish = listEnglish[randomValue];

            Button bt_German = new Button
            {
                Text = vocalGerman,
                ImageSource = "speaker24",
                BackgroundColor = Color.FromHex("#292929"),
                ContentLayout = new Button.ButtonContentLayout(Button.ButtonContentLayout.ImagePosition.Top, 0),
                TextColor = Color.SteelBlue,
                HorizontalOptions = new LayoutOptions(LayoutAlignment.Center, false),
                VerticalOptions = new LayoutOptions(LayoutAlignment.Start, false),
                FontSize = 30.0,

            };
            this.bt_German = bt_German;
            bt_German.Clicked += Bt_German_Clicked;
            Button bt_English = new Button
            {
                Text = vocalEnglish,
                ImageSource = "speaker24",
                BackgroundColor = Color.FromHex("#292929"),
                ContentLayout = new Button.ButtonContentLayout(Button.ButtonContentLayout.ImagePosition.Top, 0),
                TextColor = Color.Green,
                HorizontalOptions = new LayoutOptions(LayoutAlignment.Center, false),
                VerticalOptions = new LayoutOptions(LayoutAlignment.Center, false),
                FontSize = 30.0,
            };

            this.bt_English = bt_English;
            bt_English.Clicked += Bt_English_Clicked;


            Button btn_ShowAnswer = new Button
            {
                Text = "Antwort zeigen",
                ImageSource = "icons848",
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
            this.btn_ShowAnswer = btn_ShowAnswer;
            btn_ShowAnswer.Clicked += Btn_ShowAnswer_Clicked;

            switch (switchGerEng)
            {
                case 1:
                    SpeakGerman(vocalGerman);
                    Grid.SetRow(bt_German, 1);
                    grid.Children.Add(bt_German); break;
                case 2:
                    SpeakEnglish(vocalEnglish);
                    Grid.SetRow(bt_English, 1);
                    grid.Children.Add(bt_English); break;
            }
            Grid.SetRow(btn_ShowAnswer, 3);
            grid.Children.Add(btn_ShowAnswer);
        }
        //Die Vokale aus dem asset einlesen und in einer generischen liste speichern
        private void ReadAsset()
        {

            string filename = "EnglishVocals.txt";
            AssetManager assets = Android.App.Application.Context.Assets;
            using (StreamReader reader = new StreamReader(assets.Open(filename)))
            {
                while (!(reader.EndOfStream))
                {
                    string line = reader.ReadLine();
                    list.Add(line);
                }
            }
        }
        //value 1 = German, value 2 = English
        private List<string> ExtractWordsFromList(int value)
        {
            List<string> extractWords = new List<string>();
            extractWords.Clear();
            switch (value)
            {
                case 1:
                    foreach(string word in list)
                    {
                        string[] words = word.Split(';');
                        //get the German Vocals
                        extractWords.Add(words[0]);

                    }
                    break;
                case 2:
                    foreach (string word in list)
                    {
                        string[] words = word.Split(';');
                        //get the English Vocals
                        extractWords.Add(words[1]);

                    }
                    break;
            }
            return extractWords;
        }
        //Die deutschen Wörter aus der Liste lesen
        private List<string> GetGermanVocals()
        {
            List<string> vocalsList = new List<string>();
           
            vocalsList = ExtractWordsFromList(1);
            return vocalsList;
        }
        //Die Englischen Wörter aus der liste lesen
        private List<string> GetEnglishVocals()
        {
            List<string> vocalsList = new List<string>();
          
            vocalsList = ExtractWordsFromList(2);
            return vocalsList;
        }
       
        // Methode zum selbstbewerten ob die Anwort richtig oder falsch ist
        private void AnswerTrue_False()
        {
            btn_ShowAnswer.IsVisible = false;
            Label lblAnswer = new Label
            {
                Text = "Haben Sie die Anwort gewusst ?",
                TextColor = Color.White,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Margin = new Thickness(20)

            };
            ImageButton imgBT_Rigth = new ImageButton
            {
                Source = "haken.png",
                BackgroundColor = Color.FromHex("#292929"),
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Margin = new Thickness(20),
            };
            imgBT_Rigth.Clicked += ImgBT_Rigth_Clicked;
            ImageButton imgBT_False = new ImageButton
            {
                Source = "cross.png",
                BackgroundColor = Color.FromHex("#292929"),
                HorizontalOptions = LayoutOptions.EndAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Margin = new Thickness(20),
            };
            imgBT_False.Clicked += ImgBT_False_Clicked;
            Grid.SetRow(lblAnswer, 3);
            Grid.SetRow(imgBT_Rigth, 4);
            Grid.SetRow(imgBT_False, 4);
            grid.Children.Add(lblAnswer);
            grid.Children.Add(imgBT_False);
            grid.Children.Add(imgBT_Rigth);
        }
        private async void SpeakEnglish(string txt)
        {
            await speak.SpeakEnglish(txt);
        }
        private async void SpeakGerman(string txt)
        {
            await speak.SpeakGerman(txt);
        }
        //Die Click events
        private void Bt_English_Clicked(object sender, EventArgs e)
        {
           SpeakEnglish(bt_English.Text);
        }

        private void Bt_German_Clicked(object sender, EventArgs e)
        {
           SpeakGerman(bt_German.Text);
        }

        private void Btn_ShowAnswer_Clicked(object sender, EventArgs e)
        {
           
            switch (switchGerEng)
            {
                case 1: 
                   SpeakEnglish(bt_English.Text);
                   
                    Grid.SetRow(bt_English, 2);
                    grid.Children.Add(bt_English);
                    
                    break;
                    case 2:
                    SpeakGerman(bt_German.Text);
                   
                    Grid.SetRow(bt_German, 2);
                    grid.Children.Add(bt_German);
                   
                    break;
            }
            AnswerTrue_False();
        }
        //Methode für die Richtig falsch buttons und den entsprechenden text übergeben
     

        private void ImgBT_False_Clicked(object sender, EventArgs e)
        {
            //Datenbank zuweisen
            grid.Children.Clear();
            GetRandomVocal(grid, switchGerEng);
        }

        private void ImgBT_Rigth_Clicked(object sender, EventArgs e)
        {
            grid.Children.Clear();
            GetRandomVocal(grid, switchGerEng);
        }
       

    }
}
