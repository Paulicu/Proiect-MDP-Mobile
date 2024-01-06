using Proiect_MDP_Mobile.Models;

namespace Proiect_MDP_Mobile;

public partial class RacketEditPage : ContentPage
{
    private Racket editedRacket;
    public RacketEditPage(Racket selectedRacket)
	{
		InitializeComponent();
        editedRacket = selectedRacket;
        LoadRacketDetails();
    }

    private void LoadRacketDetails()
    {
        entryName.Text = editedRacket.Name;
        entryMaterial.Text = editedRacket.Material;
        entryTechnology.Text = editedRacket.Technology;
        entryWeight.Text = editedRacket.Weight.ToString();
        datePickerEdition.Date = editedRacket.Edition;
    }

    private async void OnSaveChangesButtonClicked(object sender, EventArgs e)
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

        // Actualizare obiect Racket cu noile date
        editedRacket.Name = entryName.Text;
        editedRacket.Material = entryMaterial.Text;
        editedRacket.Technology = entryTechnology.Text;
        editedRacket.Weight = weightValue;
        editedRacket.Edition = datePickerEdition.Date;

        await DisplayAlert("Success", "Racket updated successfully!", "OK");

        await App.Database.UpdateRacketAsync(editedRacket);
        await Navigation.PopAsync();
    }
}