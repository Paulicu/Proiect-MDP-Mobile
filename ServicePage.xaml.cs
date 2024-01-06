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
        await Navigation
            .PushAsync(new ServiceTypePage((ServiceList)this.BindingContext)
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
        var serviceList = BindingContext as ServiceList;
        var selectedServiceType = listView.SelectedItem as ServiceType;

        if (selectedServiceType != null && serviceList != null)
        {
            await App.Database.DeleteServiceTypeFromServiceListAsync(selectedServiceType.ID, serviceList.ID);

            listView.ItemsSource = await App.Database.GetListServicesAsync(serviceList.ID);

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