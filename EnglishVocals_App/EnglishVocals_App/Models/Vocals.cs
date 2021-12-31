using Android.Content.Res;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace EnglishVocals_App.Models
{
  
    public class Vocals
    {
        List<string> listRookie;
        List<string> listAdvancedChildren;
        List<string> listGerman;
        List<string> listEnglish;
        Random random;
        Button bt_German,bt_English;
        Grid grid;
        Button btn_ShowAnswer;
        SpeakText speak;
        string txtEnglish, txtGerman;
        bool dbTrue, dbFalse,advancedChildren;
        int switchGerEng;
        public Vocals(bool advanced, string topic)
        {
            listRookie = new List<string>();
            listAdvancedChildren = new List<string>();
            speak = new SpeakText();
            random = new Random();
            dbFalse = false;
            dbTrue = false;
            advancedChildren = false;
            switch (advanced)
            {
                case true: ReadAdvancedAsset(topic); break;
                case false: ReadAsset(); break;
            }
            
        }
        //Die Methoden
        public  void GetDBTrueVocals(Grid grid, int switchGerEng)
        {
            dbTrue = true;
            dbFalse = false;
            advancedChildren = false;
            if (App.DatabaseTrue.GetAllItemsAsync().Result.Count > 0)
            {
                this.grid = grid;
                this.switchGerEng = switchGerEng;
                int dbCount = App.DatabaseTrue.GetAllItemsAsync().Result.Count;
                int randomValue = random.Next(0, dbCount);
                string germanVocal =  App.DatabaseTrue.GetAllItemsAsync().Result[randomValue].GermanWords;
                string englishVocal = App.DatabaseTrue.GetAllItemsAsync().Result[randomValue].EnglishWords;
                
                SetButtonsToGrid(germanVocal, englishVocal);
            }
            else
            {
                LabelToCall();
            }
           
        }
        public  void GetDBFalseVocals(Grid grid, int switchGerEng)
        {
            dbTrue = false;
            dbFalse = true;
            advancedChildren = false;
            if (App.DatabaseFalse.GetAllItemsAsync().Result.Count > 0)
            {
                this.grid = grid;
                this.switchGerEng = switchGerEng;
                int dbCount = App.DatabaseFalse.GetAllItemsAsync().Result.Count;
                int randomValue = random.Next(0, dbCount);
                string germanVocal = App.DatabaseFalse.GetAllItemsAsync().Result[randomValue].GermanWords;
                string englishVocal = App.DatabaseFalse.GetAllItemsAsync().Result[randomValue].EnglishWords;
                SetButtonsToGrid(germanVocal, englishVocal);
            }
            else
            {
                LabelToCall();
            }

        }
        public void GetRandomVocal(Grid grid, int switchGerEng)
        {
            dbTrue = false;
            dbFalse = false;
            advancedChildren = false;
            this.grid = grid;
            this.switchGerEng = switchGerEng;
            listGerman = GetGermanVocals(false);
            listEnglish = GetEnglishVocals(false);
            int randomValue = random.Next(0, listGerman.Count);
            string vocalGerman = listGerman[randomValue];
            string vocalEnglish = listEnglish[randomValue];
            SetButtonsToGrid(vocalGerman , vocalEnglish);
          
        }
        //Wird über einen Klick aktiviert aus dem optionen menü
        public void GetRandomAdvancedVocal(Grid grid, int switchGerEng)
        {
            dbTrue = false;
            dbFalse = false;
            advancedChildren = true;
            this.grid = grid;
            this.switchGerEng = switchGerEng;
            listGerman = GetGermanVocals(true);
            listEnglish = GetEnglishVocals(true);
            int randomValue = random.Next(0, listGerman.Count);
            string vocalGerman = listGerman[randomValue];
            string vocalEnglish = listEnglish[randomValue];
            SetButtonsToGrid(vocalGerman, vocalEnglish);

        }
        private void SetButtonsToGrid(string vocalGerman, string vocalEnglish)
        {
            txtEnglish = vocalEnglish;
            txtGerman = vocalGerman;
          
           
            Button bt_German = new Button
            {
                Text = txtGerman,
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
                Text = txtEnglish,
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
                    // SpeakGerman(txtGerman);
                    Grid.SetRow(bt_German, 1);
                    grid.Children.Add(bt_German); break;
                case 2:
                    // SpeakEnglish(txtEnglish);
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
                    listRookie.Add(line);
                }
            }
        }
        private void ReadAdvancedAsset(string topic)
        {

            string filename = topic;
            AssetManager assets = Android.App.Application.Context.Assets;
            using (StreamReader reader = new StreamReader(assets.Open(filename)))
            {
                while (!(reader.EndOfStream))
                {
                    string line = reader.ReadLine();
                    listAdvancedChildren.Add(line);
                }
            }
        }
        //value 1 = German, value 2 = English
        private List<string> ExtractWordsFromList(int value, bool advancedChildren)
        {
            List<string> extractWords = new List<string>();
            extractWords.Clear();
            switch (advancedChildren)
            {
                //hier muss ich etwas tricksen da die Fortgeschrittenen Text Dateien andersherum geschrieben sind,
                //quasi Englisch - Deutsch nicht Deutsch - Englisch
                case true:
                    switch (value)
                    {
                        case 1:
                            foreach (string word in listAdvancedChildren)
                            {
                                string[] words = word.Split(';');
                                //get the German Vocals
                                extractWords.Add(words[1]);

                            }
                            break;
                        case 2:
                            foreach (string word in listAdvancedChildren)
                            {
                                string[] words = word.Split(';');
                                //get the English Vocals
                                extractWords.Add(words[0]);

                            }
                            break;
                    }
                    break;
                    case false:
                    switch (value)
                    {
                        case 1:
                            foreach (string word in listRookie)
                            {
                                string[] words = word.Split(';');
                                //get the German Vocals
                                extractWords.Add(words[0]);

                            }
                            break;
                        case 2:
                            foreach (string word in listRookie)
                            {
                                string[] words = word.Split(';');
                                //get the English Vocals
                                extractWords.Add(words[1]);

                            }
                            break;
                    }
                    break;
            }
           
            return extractWords;
        }

        //Die deutschen Wörter aus der Liste lesen
        private List<string> GetGermanVocals(bool advancedChildren)
        {
            List<string> vocalsList = new List<string>();
           
            vocalsList = ExtractWordsFromList(1, advancedChildren);
            return vocalsList;
        }
        //Die Englischen Wörter aus der liste lesen
        private List<string> GetEnglishVocals(bool avancedChildren)
        {
            List<string> vocalsList = new List<string>();
          
            vocalsList = ExtractWordsFromList(2, avancedChildren);
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
            imgBT_Rigth.Clicked += ImgBT_True_Clicked;
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
           SpeakEnglish(txtEnglish);
        }

        private void Bt_German_Clicked(object sender, EventArgs e)
        {
           SpeakGerman(txtGerman);
        }

        private void Btn_ShowAnswer_Clicked(object sender, EventArgs e)
        {
           
            switch (switchGerEng)
            {
                case 1: 
                  // SpeakEnglish(bt_English.Text);
                   
                    Grid.SetRow(bt_English, 2);
                    grid.Children.Add(bt_English);
                    
                    break;
                    case 2:
                   // SpeakGerman(bt_German.Text);
                   
                    Grid.SetRow(bt_German, 2);
                    grid.Children.Add(bt_German);
                   
                    break;
            }
            AnswerTrue_False();
        }
        //Methode für die Richtig falsch buttons und den entsprechenden text übergeben
     

        private void ImgBT_False_Clicked(object sender, EventArgs e)
        {
            AddToFalseDB(txtGerman, txtEnglish);
            grid.Children.Clear();
            IsInTrueDB(txtGerman, txtEnglish);
            GetVocalsFromListOrDB();
        }

        private void ImgBT_True_Clicked(object sender, EventArgs e)
        {
            AddToTrueDB(txtGerman, txtEnglish);
            grid.Children.Clear();
            IsInFalseDB(txtGerman, txtEnglish);
            GetVocalsFromListOrDB();
        }
        private void LabelToCall()
        {
            Label lbl = new Label
            {
                Text = "Diese Datenbank ist nun leer.",
                FontSize = 20,
                TextColor = Color.White,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Margin = new Thickness(20)

            };
            Grid.SetRow(lbl, 2);
            grid.Children.Add(lbl);
        }
        private void GetVocalsFromListOrDB()
        {
            if (dbFalse == false && dbTrue == false && advancedChildren == false)
            {
                GetRandomVocal(grid, switchGerEng);
            }
            else
            {
                if (dbTrue == true)
                {
                    GetDBTrueVocals(grid, switchGerEng);
                }
                if (dbFalse == true)
                {
                    GetDBFalseVocals(grid, switchGerEng);
                }
                if (advancedChildren == true)
                {
                    GetRandomAdvancedVocal(grid, switchGerEng);
                }
            }
        }
       
        private async void AddToTrueDB(string txtGer, string txtEng)
        {
            if (!string.IsNullOrWhiteSpace(txtGer) && !string.IsNullOrWhiteSpace(txtEng))
            {
                
                bool hit = false;
                
                for (int i = 0; i < App.DatabaseTrue.GetDBCount().Result; i++)
                {
                   if (App.DatabaseTrue.GetAllItemsAsync().Result[i].EnglishWords == txtEng)
                   {
                      hit = true;
                      
                      break;
                   }
                   
                }
                if (!hit)
                {
                    await App.DatabaseTrue.AddToDBAsync(new Models.Words
                    {
                        
                        GermanWords = txtGer,
                        EnglishWords = txtEng
                        
                    });
                }
                
                IsInFalseDB(txtGer, txtEng);
               
            }
        }
        //Durchsuchen ob die Vokable in DatenbankFalse vorhanden ist und löschen.Diese methode nur wenn die Vokabel richtig beantwortet wurde einsetzen
        private async void IsInFalseDB(string txtGer, string txtEng)
        {
            if (!string.IsNullOrWhiteSpace(txtGer) && !string.IsNullOrWhiteSpace(txtEng))
            {
                if (App.DatabaseFalse.GetAllItemsAsync().Result.Count > 0)
                {
                    bool hit = false;
                    int id = 0;
                    for (int i = 0; i < App.DatabaseFalse.GetDBCount().Result; i++)
                    {
                        if (App.DatabaseFalse.GetAllItemsAsync().Result[i].EnglishWords == txtEng)
                        {
                            hit = true;
                            id = App.DatabaseFalse.GetAllItemsAsync().Result[i].Id;
                            break;
                        }

                    }
                    if (hit)
                    {
                        await App.DatabaseFalse.DeleteItemAsync(id);
                    }
                }
            }
        }
        private async void AddToFalseDB(string txtGer, string txtEng)
        {
            if (!string.IsNullOrWhiteSpace(txtGer) && !string.IsNullOrWhiteSpace(txtEng))
            {
               
                bool hit = false;
                for (int i = 0; i < App.DatabaseFalse.GetDBCount().Result; i++)
                {
                    if (App.DatabaseFalse.GetAllItemsAsync().Result[i].EnglishWords == txtEng)
                    {
                        hit = true;
                        break;
                    }

                }
                if (!hit)
                {
                    await App.DatabaseFalse.AddToDBAsync(new Models.Words
                    {
                        GermanWords = txtGer,
                        EnglishWords = txtEng

                    });
                }
               
                IsInTrueDB(txtGer, txtEng);
               
            }
        }
        //Durchsuchen ob die Vokable in DatenbankTrue vorhanden ist und löschen.Diese methode nur wenn die Vokabel falsch beantwortet wurde einsetzen
        private async void IsInTrueDB(string txtGer,string txtEng)
        {
            if (!string.IsNullOrWhiteSpace(txtGer) && !string.IsNullOrWhiteSpace(txtEng))
            {
                if (App.DatabaseTrue.GetAllItemsAsync().Result.Count > 0)
                {
                    bool hit = false;
                    int id = 0;
                    for (int i = 0; i < App.DatabaseTrue.GetDBCount().Result; i++)
                    {
                        if (App.DatabaseTrue.GetAllItemsAsync().Result[i].EnglishWords == txtEng)
                        {
                            hit = true;
                            id = App.DatabaseTrue.GetAllItemsAsync().Result[i].Id;
                            break;
                        }

                    }
                    if (hit)
                    {
                        await App.DatabaseTrue.DeleteItemAsync(id);
                    }
                }
            }
        }
      
    }
}
