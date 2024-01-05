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
        var product = (ServiceType)BindingContext;
        await App.Database.SaveServiceAsync(product);
        listView.ItemsSource = await App.Database.GetServicesAsync();
    }

    async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        var product = (ServiceType)BindingContext;
        await App.Database.DeleteServiceAsync(product);
        listView.ItemsSource = await App.Database.GetServicesAsync();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        listView.ItemsSource = await App.Database.GetServicesAsync();
    }

    async void OnAddButtonClicked(object sender, EventArgs e)
    {
        ServiceType p;
        if (listView.SelectedItem != null)
        {
            p = listView.SelectedItem as ServiceType;
            var lp = new ListService()
            {
                ServiceListID = sl.ID,
                ServiceTypeID = p.ID
            };
            await App.Database.SaveListServiceTypeAsync(lp);
            p.ListServices = new List<ListService> { lp };
            await Navigation.PopAsync();
        }
    }
}