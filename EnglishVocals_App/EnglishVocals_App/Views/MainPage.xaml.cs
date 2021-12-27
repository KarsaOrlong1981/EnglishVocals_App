using Android.Content.Res;
using EnglishVocals_App.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace EnglishVocals_App
{
    public partial class MainPage : ContentPage
    {
       
        public MainPage()
        {
            InitializeComponent();
           
            BindingContext = new MainPageViewModel(Navigation, grid, this);
        }
    }
}
