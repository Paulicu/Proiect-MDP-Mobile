using Proiect_MDP_Mobile.Models;

namespace Proiect_MDP_Mobile;

public partial class RacketEntryPage : ContentPage
{
	public RacketEntryPage()
	{
		InitializeComponent();
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
            Edition = datePickerEdition.Date 
        };

        await DisplayAlert("Success", "Racket added successfully!", "OK");

        await App.Database.SaveRacketAsync(newRacket);
        await Navigation.PopAsync();
    }
}