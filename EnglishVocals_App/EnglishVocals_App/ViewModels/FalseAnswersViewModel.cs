using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using EnglishVocals_App.Models;
using EnglishVocals_App.Views;
using Xamarin.Forms;

namespace EnglishVocals_App.ViewModels
{
    public class FalseAnswersViewModel : BaseVM 
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
        public FalseAnswersViewModel(INavigation navigation,Grid grid)
        {
            this.Navigation = navigation;
            this.grid = grid;
            IsVisBtn = true;
            vocals = new Vocals(false, string.Empty);

            BTN_GermanEnglish = new Command(() => GermanEnglish());
            BTN_EnglishGerman = new Command(() => EnglishGerman());
        }
        private void GermanEnglish()
        {
            IsVisBtn = false;
            switchGerEng = 1;
            vocals.GetDBFalseVocals(grid, switchGerEng);
        }
        private void EnglishGerman()
        {
            IsVisBtn = false;
            switchGerEng = 2;
            vocals.GetDBFalseVocals(grid, switchGerEng);
        }
    }
}
