using System;
using System.Collections.Generic;
using System.Text;
using EnglishVocals_App.Views;
using Xamarin.Forms;
using EnglishVocals_App.Models;
using System.Windows.Input;

namespace EnglishVocals_App.ViewModels
{
    public class TrueAnswersViewModel : BaseVM 
    {
        Vocals vocals;
        Grid grid;
        int switchGerEng;
        private bool isVisBtn;
        public INavigation Navigation { get; set; }
        public ICommand BTN_GermanEnglish { get; set; }
        public ICommand BTN_EnglishGerman { get; set; }
       
        public bool IsVisBtn
        {
            get => isVisBtn;
            set => SetProperty(ref isVisBtn, value);
        }
        public TrueAnswersViewModel(INavigation navigation,Grid grid)
        {
            this.Navigation = navigation;
            this.grid = grid;
            IsVisBtn = true;
            vocals = new Vocals();

            BTN_GermanEnglish = new Command(() => GermanEnglish());
            BTN_EnglishGerman = new Command(() => EnglishGerman());
        }
        private void GermanEnglish()
        {
            IsVisBtn = false;
            switchGerEng = 1;
            vocals.GetDBTrueVocals(grid, switchGerEng);
        }
        private void EnglishGerman()
        {
            IsVisBtn = false;
            switchGerEng = 2;
            vocals.GetDBTrueVocals(grid, switchGerEng);
        }
    }
}
