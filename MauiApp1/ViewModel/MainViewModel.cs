using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.Model;
using MongoDB.Bson;
using Realms;
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
            LoadTasksCommand = new AsyncRelayCommand(LoadTasksAsync);
        }
        
        [ObservableProperty]
        ObservableCollection<TaskItem> items;


        [ObservableProperty]
        string text;
        public IAsyncRelayCommand LoadTasksCommand { get; }

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

            var task = TaskItem.Create(Text);
            var realm = await Realm.GetInstanceAsync();

            await realm.WriteAsync(() =>
            {
                realm.Add(task);
            });

            Items.Add(task);
            Text = string.Empty;
        }

        [RelayCommand]
        async Task Delete(ObjectId id)
        {
            var realm = await Realm.GetInstanceAsync();

            var item = realm.Find<TaskItem>(id);
            await realm.WriteAsync(() =>
            {
                realm.Remove(item);
            });
            await LoadTasksAsync();
        }

        [RelayCommand]
        async Task Tap(ObjectId id)
        {
            await Shell.Current.GoToAsync($"{nameof(DetailPage)}?Id={id.ToString()}");
        }

        private async Task LoadTasksAsync()
        {
            var realm = await Realm.GetInstanceAsync();
            var allItems = realm.All<TaskItem>();
            Items = new ObservableCollection<TaskItem>(allItems);
        }
    }
}
