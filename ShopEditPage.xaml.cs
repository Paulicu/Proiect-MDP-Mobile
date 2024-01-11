using Proiect_MDP_Mobile.Models;

namespace Proiect_MDP_Mobile;

public partial class ShopEditPage : ContentPage
{
    private Shop _shop;

    public ShopEditPage(Shop shop)
    {
        InitializeComponent();

        _shop = shop;
        BindingContext = _shop;
    }

    private async void OnSaveChangesClicked(object sender, EventArgs e)
    {
        _shop.Name = entryName.Text;
        _shop.Address = entryAddress.Text;

        await App.Database.SaveShopAsync(_shop);

        await DisplayAlert("Success", "Shop details updated successfully!", "OK");
        await Navigation.PopAsync();
    }

    private void OnCancelClicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
}