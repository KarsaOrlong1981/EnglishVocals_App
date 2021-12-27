using EnglishVocals_App.Models;
using EnglishVocals_App.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace EnglishVocals_App.ViewModels
{
    public class VocalsViewModel : BaseVM
    {
        public INavigation Navigation { get; set; }
        Grid grid;
        Vocals vocals;
        VocalsView vocalsView;
        public VocalsViewModel(INavigation navigation, Grid grid, int switchGerEng, VocalsView vocalsView)
        {
            this.Navigation = navigation;
            this.grid = grid;
            this.vocalsView = vocalsView;
            vocals = new Vocals();
            switch (switchGerEng)
            {
                case 1: vocalsView.Title = "Deutsch - Englisch"; break;
                case 2: vocalsView.Title = "Englisch - Deutsch"; break;
            }
            vocals.GetRandomVocal(grid, switchGerEng);
            //Zwei Buttons richtig und falsch, in richtig oder falsch datenbank speichern und im click event die nächste vokabel ausgeben.
        }
    }
}
