using Proiect_MDP_Mobile.Models;
using System.Collections.ObjectModel;

namespace Proiect_MDP_Mobile;

public partial class RacketEditPage : ContentPage
{
    private Racket _racket;
    private ObservableCollection<Shop> _shops;

    public RacketEditPage(Racket racket)
    {
        InitializeComponent();
        _racket = racket;
        BindingContext = _racket;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        _shops = new ObservableCollection<Shop>(await App.Database.GetShopsAsync());
        pickerShop.ItemsSource = _shops;
        pickerShop.ItemDisplayBinding = new Binding("Name");

        if (_racket != null)
        {
            entryName.Text = _racket.Name;
            entryMaterial.Text = _racket.Material;
            entryTechnology.Text = _racket.Technology;
            entryWeight.Text = _racket.Weight.ToString();
            datePickerEdition.Date = _racket.Edition;

            if (_racket.ShopID != 0)
            {
                var selectedShop = await App.Database.GetShopAsync(_racket.ShopID);
                pickerShop.SelectedItem = selectedShop;
            }
        }
    }

    private async void OnSaveChangesButtonClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(entryName.Text) || string.IsNullOrWhiteSpace(entryMaterial.Text) || string.IsNullOrWhiteSpace(entryTechnology.Text))
        {
            await DisplayAlert("Error", "All fields must be filled in.", "OK");
            return;
        }

        _racket.Name = entryName.Text;
        _racket.Material = entryMaterial.Text;
        _racket.Technology = entryTechnology.Text;
        _racket.Weight = decimal.TryParse(entryWeight.Text, out decimal weightValue) ? weightValue : 0;
        _racket.Edition = datePickerEdition.Date;

        var selectedShop = (Shop)pickerShop.SelectedItem;
        _racket.ShopID = selectedShop?.ID ?? 0;

        await App.Database.UpdateRacketAsync(_racket);
        await Navigation.PopAsync();
    }

    private void OnCancelClicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
}