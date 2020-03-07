using BusinessMobil.App.Controls;
using BusinessMobil.App.Controls.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
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

        protected void SetValue<T>(ref T backingField, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingField, value))
            {
                return;
            }

            backingField = value;
            OnPropertyChanged(propertyName);
        }

        private bool _isRunning;
        public bool IsRunning
        {
            get => _isRunning;
            set => SetValue(ref _isRunning, value);
        }

        private bool isEnable;
        public bool IsEnable
        {
            get => isEnable;
            set => SetValue(ref isEnable, value);
        }

        public async void ShowLoad()
        {
            DependencyService.Get<ILodingPageService>().InitLoadingPage(new LoadIndicator());
            DependencyService.Get<ILodingPageService>().ShowLoadingPage();
            await Task.Delay(100);
        }
    }
}