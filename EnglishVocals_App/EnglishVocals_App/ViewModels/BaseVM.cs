using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace EnglishVocals_App.ViewModels
{
    public abstract class BaseVM : INotifyPropertyChanged
    {
        //declares the (required) PropertyChanged event that is defined by the interface.
        public event PropertyChangedEventHandler PropertyChanged;
        // next it is checked if someone has registered for the event,
        // and in this case the event will be raised with the name of the property being updated.
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        // This is a helper method to make setting the property easier. Right now you can just take this with faith
        // or if you are familiar with generics you can study it a little to see how it works.
        protected bool SetProperty<T>(ref T backingStore, T value,
           [CallerMemberName] string propertyName = "",
           Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

    }
}
