using Proiect_MDP_Mobile.Models;

namespace Proiect_MDP_Mobile;

public partial class ServiceTypePage : ContentPage
{
    ServiceList sl;
    public ServiceTypePage(ServiceList slist)
    {
        InitializeComponent();
        sl = slist;
    }

    async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var serviceType = (ServiceType)BindingContext;
        await App.Database.SaveServiceAsync(serviceType);
        listView.ItemsSource = await App.Database.GetServicesAsync();
    }

    async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        var serviceType = (ServiceType)BindingContext;
        await App.Database.DeleteServiceAsync(serviceType);
        listView.ItemsSource = await App.Database.GetServicesAsync();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        listView.ItemsSource = await App.Database.GetServicesAsync();
    }

    async void OnAddButtonClicked(object sender, EventArgs e)
    {
        ServiceType s;
        if (listView.SelectedItem != null)
        {
            s = listView.SelectedItem as ServiceType;
            var ls = new ListService()
            {
                ServiceListID = sl.ID,
                ServiceTypeID = s.ID
            };
            await App.Database.SaveListServiceTypeAsync(ls);
            s.ListServices = new List<ListService> { ls };
            await Navigation.PopAsync();
        }
    }
}