using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using EnglishVocals_App.ViewModels;

namespace EnglishVocals_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FalseAnswersPage : ContentPage
    {
        public FalseAnswersPage()
        {
            InitializeComponent();
            BindingContext = new FalseAnswersViewModel(Navigation,grid);
        }
    }
}