using Proiect_MDP_Mobile.Models;

namespace Proiect_MDP_Mobile;

public partial class ServicePage : ContentPage
{
    async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var slist = (ServiceList)BindingContext;
        slist.Date = DateTime.Now;
        await App.Database.SaveServiceListAsync(slist);
        await Navigation.PopAsync();
    }

    async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        var slist = (ServiceList)BindingContext;
        await App.Database.DeleteServiceListAsync(slist);
        await Navigation.PopAsync();
    }

    async void OnChooseButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ServiceTypePage((ServiceList)
        this.BindingContext)
        {
            BindingContext = new ServiceType()
        });

    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var servicel = (ServiceList)BindingContext;

        listView.ItemsSource = await App.Database.GetListServicesAsync(servicel.ID);
    }

    async void OnDeleteServiceTypeButtonClicked(object sender, EventArgs e)
    {
        var shopList = BindingContext as ServiceList;
        var selectedProduct = listView.SelectedItem as ServiceType;

        if (selectedProduct != null && shopList != null)
        {
            await App.Database.DeleteServiceTypeFromServiceListAsync(selectedProduct.ID, shopList.ID);

            listView.ItemsSource = await App.Database.GetListServicesAsync(shopList.ID);

            listView.SelectedItem = null;
        }
        else
        {
            await DisplayAlert("No Service Type Selected", "Please select a service type to delete.", "OK");
        }
    }
    public ServicePage()
	{
		InitializeComponent();
	}
}