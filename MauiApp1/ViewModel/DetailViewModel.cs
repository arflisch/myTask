using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.Model;
using MongoDB.Bson;

namespace MauiApp1.ViewModel
{
    [QueryProperty("Id", "Id")]
    public partial class DetailViewModel : ObservableObject
    {
        [ObservableProperty]
        TaskItem item;

        private string _id;
        public string Id
        {
            get => _id;
            set
            {
                _id = value;
                Task.Run(async() => await MainThread.InvokeOnMainThreadAsync(LoadTaskItemAsync));
            }
        }

        public async Task LoadTaskItemAsync()
        {
            if (ObjectId.TryParse(Id, out var id))
            {
                var realm = await Realm.GetInstanceAsync();

                Item = realm.Find<TaskItem>(id);

            }
        }

        [RelayCommand]
        async Task GoBack()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
