using System;
using System.Collections.Generic;
using System.Text;
using EnglishVocals_App.Views;
using Xamarin.Forms;

namespace EnglishVocals_App.ViewModels
{
    public class FalseAnswersViewModel : BaseVM 
    {
        public INavigation Navigation { get; set; }
        public FalseAnswersViewModel(INavigation navigation)
        {
            this.Navigation = navigation;
        }
    }
}
