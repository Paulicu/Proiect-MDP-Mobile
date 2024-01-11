using Proiect_MDP_Mobile.Models;
using System.Collections.ObjectModel;

namespace Proiect_MDP_Mobile;

public partial class ServiceEntryPage : ContentPage
{
    public ObservableCollection<Racket> Rackets { get; set; }
    public Racket SelectedRacket { get; set; }

    public ServiceEntryPage()
    {
        InitializeComponent();
        Rackets = new ObservableCollection<Racket>();
        LoadRacketsAsync();
        BindingContext = this;
    }

    private async void LoadRacketsAsync()
    {
        var rackets = await App.Database.GetRacketsAsync();
        foreach (var racket in rackets)
        {
            Rackets.Add(racket);
        }

        // Add Rackets to the Picker
        racketPicker.ItemsSource = Rackets;
        racketPicker.ItemDisplayBinding = new Binding("Name");
    }

    private void OnRacketPickerSelectedIndexChanged(object sender, EventArgs e)
    {
        SelectedRacket = (Racket)racketPicker.SelectedItem;
    }

    private async void OnSaveServiceButtonClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(entryType.Text) || string.IsNullOrWhiteSpace(entryDescription.Text))
        {
            await DisplayAlert("Error", "All fields must be completed.", "OK");
            return;
        }

        // Creare obiect Service cu datele introduse
        Service newService = new Service
        {
            Type = entryType.Text,
            Description = entryDescription.Text,
            Date = datePickerDate.Date,
            RacketID = SelectedRacket?.ID ?? 0
        };

        await DisplayAlert("Success", "Service added successfully!", "OK");

        await App.Database.SaveServiceAsync(newService);
        await Navigation.PopAsync();
    }
}