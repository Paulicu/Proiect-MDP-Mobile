using Proiect_MDP_Mobile.Models;

namespace Proiect_MDP_Mobile;

public partial class ServiceListPage : ContentPage
{
	public ServiceListPage()
	{
		InitializeComponent();
	}

    private async Task LoadServicesAsync()
    {
        List<Service> services = await App.Database.GetServicesAsync();
        serviceListView.ItemsSource = services;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadServicesAsync();
    }

    private void OnViewServiceClicked(object sender, EventArgs e)
    {
        var selectedService = (Service)serviceListView.SelectedItem;
        if (selectedService != null)
        {
            Navigation.PushAsync(new ServiceViewPage(selectedService));
            serviceListView.SelectedItem = null;
            UpdateButtonVisibility();
        }
    }

    private void OnAddServiceClicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new ServiceEntryPage());
    }

    private void OnEditServiceClicked(object sender, EventArgs e)
    {
        var selectedService = (Service)serviceListView.SelectedItem;
        if (selectedService != null)
        {
            Navigation.PushAsync(new ServiceEditPage(selectedService));
            serviceListView.SelectedItem = null;
            UpdateButtonVisibility();
        }
    }

    private async void OnDeleteServiceClicked(object sender, EventArgs e)
    {
        var selectedService = (Service)serviceListView.SelectedItem;
        if (selectedService == null)
        {
            await DisplayAlert("Error", "No service selected for deletion.", "OK");
            return;
        }

        bool answer = await DisplayAlert("Confirm", "Are you sure you want to delete this service?", "Yes", "No");
        if (answer)
        {
            await App.Database.DeleteServiceAsync(selectedService);
            await Navigation.PushAsync(new ServiceListPage());
        }
    }

    private void OnServiceListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        UpdateButtonVisibility();
    }

    private void UpdateButtonVisibility()
    {
        bool isServiceSelected = serviceListView.SelectedItem != null;

        buttonContainer.IsVisible = isServiceSelected;
    }

    private void OnCancelClicked(object sender, EventArgs e)
    {
        serviceListView.SelectedItem = null;
        UpdateButtonVisibility();
    }
}