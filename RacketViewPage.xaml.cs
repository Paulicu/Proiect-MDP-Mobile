using Proiect_MDP_Mobile.Models;

namespace Proiect_MDP_Mobile;

public partial class RacketViewPage : ContentPage
{
	public RacketViewPage(Racket selectedRacket)
	{
		InitializeComponent();
        BindingContext = selectedRacket;
    }

    private async void OnBackToListClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}