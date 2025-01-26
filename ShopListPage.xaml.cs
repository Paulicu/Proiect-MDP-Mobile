using Proiect_MDP_Mobile.Models;

namespace Proiect_MDP_Mobile;

public partial class ShopListPage : ContentPage
{
	public ShopListPage()
	{
		InitializeComponent();
	}

    private async Task LoadShopsAsync()
    {
        List<Shop> shops = await App.Database.GetShopsAsync();
        shopListView.ItemsSource = shops;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadShopsAsync();
    }

    private void OnViewShopClicked(object sender, EventArgs e)
    {
        var selectedShop = (Shop)shopListView.SelectedItem;
        if (selectedShop != null)
        {
            Navigation.PushAsync(new ShopViewPage(selectedShop));
            shopListView.SelectedItem = null;
            UpdateButtonVisibility();
        }
    }

    private void OnAddShopClicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new ShopEntryPage());
    }

    private void OnEditShopClicked(object sender, EventArgs e)
    {
        var selectedShop = (Shop)shopListView.SelectedItem;
        if (selectedShop != null)
        {
            Navigation.PushAsync(new ShopEditPage(selectedShop));
            shopListView.SelectedItem = null;
            UpdateButtonVisibility();
        }
    }

    private async void OnDeleteShopClicked(object sender, EventArgs e)
    {
        var selectedShop = (Shop)shopListView.SelectedItem;
        if (selectedShop == null)
        {
            await DisplayAlert("Error", "No shop selected for deletion.", "OK");
            return;
        }

        bool answer = await DisplayAlert("Confirm", "Are you sure you want to delete this shop?", "Yes", "No");
        if (answer)
        {
            await App.Database.DeleteShopAsync(selectedShop);
            await Navigation.PushAsync(new ShopListPage());
        }
    }

    private void OnShopListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        UpdateButtonVisibility();
    }

    private void UpdateButtonVisibility()
    {
        bool isShopSelected = shopListView.SelectedItem != null;
        buttonContainer.IsVisible = isShopSelected;
    }

    private void OnCancelClicked(object sender, EventArgs e)
    {
        shopListView.SelectedItem = null;
        UpdateButtonVisibility();
    }
}