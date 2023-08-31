using MyTask.ViewModel;
using MyTask.Resources;

namespace MyTask;

public partial class DetailPage : ContentPage
{
	public DetailPage(DetailViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
        _vm = vm;
    }
    private readonly DetailViewModel _vm;
    protected async void OnButtonClicked(object sender, EventArgs args)
    {
        bool answer = await DisplayAlert(Language.Titlepopup, Language.Qpopup, Language.YesPopup, Language.Nopopup);
        if (answer)
        {
            await _vm.Delete();
        }
    }

}