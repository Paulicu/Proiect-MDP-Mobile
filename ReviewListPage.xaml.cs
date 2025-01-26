using Proiect_MDP_Mobile.Models;

namespace Proiect_MDP_Mobile;

public partial class ReviewListPage : ContentPage
{
	public ReviewListPage()
	{
		InitializeComponent();
	}

    private async Task LoadReviewsAsync()
    {
        List<Review> reviews = await App.Database.GetReviewsAsync();
        reviewListView.ItemsSource = reviews;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadReviewsAsync();
    }

    private void OnViewReviewClicked(object sender, EventArgs e)
    {
        var selectedReview = (Review)reviewListView.SelectedItem;
        if (selectedReview != null)
        {
            Navigation.PushAsync(new ReviewViewPage(selectedReview));
            reviewListView.SelectedItem = null;
            UpdateButtonVisibility();
        }
    }

    private void OnAddReviewClicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new ReviewEntryPage());
    }

    private void OnEditReviewClicked(object sender, EventArgs e)
    {
        var selectedReview = (Review)reviewListView.SelectedItem;
        if (selectedReview != null)
        {
            Navigation.PushAsync(new ReviewEditPage(selectedReview));
            reviewListView.SelectedItem = null;
            UpdateButtonVisibility();
        }
    }

    private async void OnDeleteReviewClicked(object sender, EventArgs e)
    {
        var selectedReview = (Review)reviewListView.SelectedItem;
        if (selectedReview == null)
        {
            await DisplayAlert("Error", "No review selected for deletion.", "OK");
            return;
        }

        bool answer = await DisplayAlert("Confirm", "Are you sure you want to delete this review?", "Yes", "No");
        if (answer)
        {
            await App.Database.DeleteReviewAsync(selectedReview);
            await Navigation.PopAsync();
        }
    }

    private void OnReviewListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        UpdateButtonVisibility();
    }

    private void UpdateButtonVisibility()
    {
        bool isReviewSelected = reviewListView.SelectedItem != null;

        buttonContainer.IsVisible = isReviewSelected;
    }

    private void OnCancelClicked(object sender, EventArgs e)
    {
        reviewListView.SelectedItem = null;
        UpdateButtonVisibility();
    }
}