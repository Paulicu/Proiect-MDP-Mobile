using Proiect_MDP_Mobile.Models;

namespace Proiect_MDP_Mobile;

public partial class RacketListPage : ContentPage
{
	public RacketListPage()
	{
		InitializeComponent();
	}

    private async Task LoadRacketsAsync()
    {
        List<Racket> rackets = await App.Database.GetRacketsAsync();
        racketListView.ItemsSource = rackets;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadRacketsAsync();
    }

    private void OnViewRacketClicked(object sender, EventArgs e)
    {
        var selectedRacket = (Racket)racketListView.SelectedItem;
        if (selectedRacket != null)
        {
            Navigation.PushAsync(new RacketViewPage(selectedRacket));
            racketListView.SelectedItem = null;
            UpdateButtonVisibility();
        }
    }

    private void OnAddRacketClicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new RacketEntryPage());
    }

    private void OnEditRacketClicked(object sender, EventArgs e)
    {
        var selectedRacket = (Racket)racketListView.SelectedItem;
        if (selectedRacket != null)
        {
            Navigation.PushAsync(new RacketEditPage(selectedRacket));
            racketListView.SelectedItem = null;
            UpdateButtonVisibility();
        }
    }

    private async void OnDeleteRacketClicked(object sender, EventArgs e)
    {
        var selectedRacket = (Racket)racketListView.SelectedItem;
        if (selectedRacket == null)
        {
            await DisplayAlert("Error", "No racket selected for deletion.", "OK");
            return;
        }

        bool answer = await DisplayAlert("Confirm", "Are you sure you want to delete this racket?", "Yes", "No");
        if (answer)
        {
            await App.Database.DeleteRacketAsync(selectedRacket);
            await Navigation.PopAsync();
        }
    }

    private void OnRacketListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        UpdateButtonVisibility();
    }

    private void UpdateButtonVisibility()
    {
        bool isRacketSelected = racketListView.SelectedItem != null;

        buttonContainer.IsVisible = isRacketSelected;
    }

    private void OnCancelClicked(object sender, EventArgs e)
    {
        racketListView.SelectedItem = null;
        UpdateButtonVisibility();
    }
}