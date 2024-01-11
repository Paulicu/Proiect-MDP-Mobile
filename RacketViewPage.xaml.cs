using Proiect_MDP_Mobile.Models;

namespace Proiect_MDP_Mobile;

public partial class RacketViewPage : ContentPage
{
    private Racket _racket;

    public RacketViewPage(Racket racket)
    {
        InitializeComponent();
        _racket = racket;
        BindingContext = _racket;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (_racket != null)
        {
            labelName.Text = _racket.Name;
            labelMaterial.Text = _racket.Material;
            labelTechnology.Text = _racket.Technology;
            labelWeight.Text = _racket.Weight.ToString(); 
            labelEdition.Text = _racket.Edition.ToString("MMMM dd, yyyy");
            labelShop.Text = (_racket.ShopID != 0) ? App.Database.GetShopAsync(_racket.ShopID).Result?.Name : "N/A";
        }
    }

    private async void OnEditRacketClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RacketEditPage(_racket));
    }

    private async void OnBackToListClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}