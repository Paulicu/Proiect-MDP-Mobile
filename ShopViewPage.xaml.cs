using Proiect_MDP_Mobile.Models;

namespace Proiect_MDP_Mobile;

public partial class ShopViewPage : ContentPage
{
    private Shop _shop;
    public ShopViewPage(Shop shop)
    {
        InitializeComponent();
        _shop = shop;
        BindingContext = shop;
    }

    private async void OnBackToListClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private async void OnEditClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ShopEditPage(_shop));
    }
}