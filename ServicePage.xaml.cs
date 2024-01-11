using Proiect_MDP_Mobile.Models;
using System.Collections.ObjectModel;

namespace Proiect_MDP_Mobile;

public partial class ServicePage : ContentPage
{
    public ObservableCollection<Racket> Rackets { get; set; }
    public Racket SelectedRacket { get; set; }

    async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var slist = (ServiceList)BindingContext;
        slist.Date = DateTime.Now;

        // Verifica daca a fost selectata o racheta
        if (RacketPicker.SelectedItem is Racket selectedRacket)
        {
            slist.RacketID = selectedRacket.ID;
            await App.Database.SaveServiceListAsync(slist);
            await Navigation.PopAsync();
        }
        else
        {
            await DisplayAlert("No Racket Selected", "Please select a racket for the service.", "OK");
        }

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

        var items = await App.Database.GetRacketsAsync();
        RacketPicker.ItemsSource = (System.Collections.IList)items;
        RacketPicker.ItemDisplayBinding = new Binding("Name");

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

    // Selectare Racheta pentru Serviciu:
    private async void LoadRacketsAsync()
    {
        var rackets = await App.Database.GetRacketsAsync();
        foreach (var racket in rackets)
        {
            Rackets.Add(racket);
        }
    }
    public ServicePage()
	{
		InitializeComponent();

        Rackets = new ObservableCollection<Racket>();

        LoadRacketsAsync();
        BindingContext = this;
    }
}