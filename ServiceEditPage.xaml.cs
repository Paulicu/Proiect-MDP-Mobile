using Proiect_MDP_Mobile.Models;
using System.Collections.ObjectModel;

namespace Proiect_MDP_Mobile;

public partial class ServiceEditPage : ContentPage
{
    private Service _service;
    private ObservableCollection<Racket> _rackets;

    public ServiceEditPage(Service service)
    {
        InitializeComponent();
        _service = service;
        BindingContext = _service;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        _rackets = new ObservableCollection<Racket>(await App.Database.GetRacketsAsync());
        pickerRacket.ItemsSource = _rackets;
        pickerRacket.ItemDisplayBinding = new Binding("Name");

        if (_service != null)
        {
            entryType.Text = _service.Type;
            entryDescription.Text = _service.Description;
            datePickerDate.Date = _service.Date;

            if (_service.RacketID != 0)
            {
                var selectedRacket = await App.Database.GetRacketAsync(_service.RacketID);
                pickerRacket.SelectedItem = selectedRacket;
            }
        }
    }

    private async void OnSaveChangesClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(entryType.Text) || string.IsNullOrWhiteSpace(entryDescription.Text))
        {
            await DisplayAlert("Error", "Type and Description must be filled in.", "OK");
            return;
        }

        _service.Type = entryType.Text;
        _service.Description = entryDescription.Text;
        _service.Date = datePickerDate.Date;

        var selectedRacket = (Racket)pickerRacket.SelectedItem;
        _service.RacketID = selectedRacket?.ID ?? 0;

        await App.Database.SaveServiceAsync(_service);
        await Navigation.PopAsync();
    }

    private void OnCancelClicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
}