using Plugin.LocalNotification;
using Proiect_MDP_Mobile.Models;

namespace Proiect_MDP_Mobile;

public partial class RacketEntryPage : ContentPage
{
    public RacketEntryPage()
    {
        InitializeComponent();

        // Populare Picker cu magazinele existente
        LoadShops();
    }

    private async void LoadShops()
    {
        var shops = await App.Database.GetShopsAsync();

        foreach (var shop in shops)
        {
            pickerShop.Items.Add(shop.Name);
        }
    }

    private async void OnSaveRacketButtonClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(entryName.Text) || string.IsNullOrWhiteSpace(entryMaterial.Text) || string.IsNullOrWhiteSpace(entryTechnology.Text) || string.IsNullOrWhiteSpace(entryWeight.Text))
        {
            await DisplayAlert("Error", "All fields must be completed.", "OK");
            return;
        }

        if (!decimal.TryParse(entryWeight.Text, out decimal weightValue))
        {
            await DisplayAlert("Error", "Please enter a valid numeric value for weight.", "OK");
            return;
        }

        // Creare obiect Racket cu datele introduse
        Racket newRacket = new Racket
        {
            Name = entryName.Text,
            Material = entryMaterial.Text,
            Technology = entryTechnology.Text,
            Weight = weightValue,
            Edition = datePickerEdition.Date,

            // Setare ShopID cu ID-ul magazinului selectat
            ShopID = pickerShop.SelectedIndex + 1 // +1 pentru a ajusta de la 0-based la 1-based index
        };

        await DisplayAlert("Success", "Racket added successfully!", "OK");
        await App.Database.SaveRacketAsync(newRacket);
        await Navigation.PopAsync();
    }
}