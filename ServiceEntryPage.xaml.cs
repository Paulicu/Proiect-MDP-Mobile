using Proiect_MDP_Mobile.Models;

namespace Proiect_MDP_Mobile;

public partial class ServiceEntryPage : ContentPage
{
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        listView.ItemsSource = await App.Database.GetServiceListsAsync();
    }

    async void OnServiceListAddedClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ServicePage
        {
            BindingContext = new ServiceList()
        });
    }

    async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem != null)
        {
            await Navigation.PushAsync(new ServicePage
            {
                BindingContext = e.SelectedItem as ServiceList
            });
        }
    }

    public ServiceEntryPage()
	{
		InitializeComponent();
	}
}