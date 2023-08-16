using MauiApp1.ViewModel;

namespace MauiApp1;

public partial class MainPage : ContentPage
{

    public MainPage(MainViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
        _vm = vm;
	}

    private readonly MainViewModel _vm;

    protected async override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        await _vm.LoadTasksAsync().ConfigureAwait(true);
    }

    
}

