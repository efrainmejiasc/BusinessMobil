using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace BusinessMobil.App.ViewModel
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public INavigation Navigation { get => Application.Current.MainPage.Navigation; } 
        public event PropertyChangedEventHandler PropertyChanged;
        public BaseViewModel()
        {
            
        }
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}