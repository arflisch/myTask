using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.Model;
using MongoDB.Bson;
using System.Threading.Tasks;

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
        async Task Save()
        {
            var realm = await Realm.GetInstanceAsync();

            var item = realm.Find<TaskItem>(Item.Id);

            await realm.WriteAsync(() =>
            {
                item.Description = Item.Description;
                item.Name = Item.Name;
            });
            
        }
        
        [RelayCommand]
        private async Task Delete()
        {
            var realm = await Realm.GetInstanceAsync();
            var item = realm.Find<TaskItem>(Item.Id);


            await realm.WriteAsync(() =>
            {
                realm.Remove(item);
            });
            await GoBack();
        }

        async void OnButtonClicked(object sender, EventArgs args) 
        {
            bool answer = await DisplayAlert("Do you want to delete the task", "Yes", "No");
            if (answer)
            {
                await Delete();
            }
        }

        private Task<bool> DisplayAlert(string v1, string v2, string v3)
        {
            throw new NotImplementedException();
        }

        [RelayCommand]
        async Task GoBack()
        {
            await Shell.Current.GoToAsync("..");
            
        }
        
    }
}
