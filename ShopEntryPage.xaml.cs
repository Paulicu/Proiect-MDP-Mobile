using Proiect_MDP_Mobile.Models;

namespace Proiect_MDP_Mobile;

public partial class ShopEntryPage : ContentPage
{
	public ShopEntryPage()
	{
		InitializeComponent();
	}

    private async void OnSaveShopButtonClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(entryName.Text) || string.IsNullOrWhiteSpace(entryAddress.Text))
        {
            await DisplayAlert("Error", "All fields must be completed.", "OK");
            return;
        }

        // Creare obiect Shop cu datele introduse
        Shop newShop = new Shop
        {
            Name = entryName.Text,
            Address = entryAddress.Text
        };

        await DisplayAlert("Success", "Shop added successfully!", "OK");

        await App.Database.SaveShopAsync(newShop);
        await Navigation.PopAsync();
    }
}