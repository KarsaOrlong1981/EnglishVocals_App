using EnglishVocals_App.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EnglishVocals_App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VocalsView : ContentPage
    {
        public VocalsView(int switchGerEng, bool advanced, string topic)
        {
            InitializeComponent();
            BindingContext = new VocalsViewModel(Navigation, grid, switchGerEng, this, advanced, topic);
        }
    }
}