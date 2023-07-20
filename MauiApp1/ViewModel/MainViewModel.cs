using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace MauiApp1.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {
        IConnectivity connectivity;
        public MainViewModel(IConnectivity connectivity) 
        {
            Items = new ObservableCollection<TaskItem>();
            this.connectivity = connectivity;
        }
        
        [ObservableProperty]
        ObservableCollection<TaskItem> items;


        [ObservableProperty]
        string text;

        [RelayCommand]
        async Task Add()
        {
            if (string.IsNullOrWhiteSpace(Text))
                return;

            if(connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("Uh Oh!", "No Internet", "Ok");
                return;
            }

            Items.Add(new TaskItem(Text));
            Text = string.Empty;
        }

        [RelayCommand]
        void Delete(string s)
        {
            
            //if (Items.Contains(s)) Items.Remove(s);
        }

        [RelayCommand]
        async Task Tap(string s)
        {
            await Shell.Current.GoToAsync($"{nameof(DetailPage)}?Text={s}");
        }
    }
}
