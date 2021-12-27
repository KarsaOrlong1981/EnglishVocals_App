using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using SQLite;

namespace EnglishVocals_App.Models
{
    public class Words
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string GermanWords { get; set;}
        public string EnglishWords { get; set; }
    }
}
