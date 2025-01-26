using Proiect_MDP_Mobile.Models;
using System.Collections.ObjectModel;

namespace Proiect_MDP_Mobile;

public partial class ReviewEditPage : ContentPage
{
    private Review _selectedReview;
    private ObservableCollection<Racket> _rackets;

    public ReviewEditPage(Review selectedReview)
    {
        InitializeComponent();
        _selectedReview = selectedReview;
        LoadReviewDetails();
        LoadRacketsAsync();
    }

    private void LoadReviewDetails()
    {
        entryUserName.Text = _selectedReview.UserName;
        entryComment.Text = _selectedReview.Comment;
        entryRating.Text = _selectedReview.Rating.ToString();
    }

    private async Task LoadRacketsAsync()
    {
        _rackets = new ObservableCollection<Racket>(await App.Database.GetRacketsAsync());
        racketPicker.ItemsSource = _rackets;
        racketPicker.SelectedItem = _rackets.FirstOrDefault(r => r.ID == _selectedReview.RacketID);
    }

    private async void OnSaveReviewButtonClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(entryUserName.Text) || string.IsNullOrWhiteSpace(entryComment.Text) || string.IsNullOrWhiteSpace(entryRating.Text))
        {
            await DisplayAlert("Error", "All fields must be completed.", "OK");
            return;
        }

        if (!int.TryParse(entryRating.Text, out int ratingValue) || ratingValue < 1 || ratingValue > 5)
        {
            await DisplayAlert("Error", "Please enter a valid rating between 1 and 5.", "OK");
            return;
        }

        
        _selectedReview.UserName = entryUserName.Text;
        _selectedReview.Comment = entryComment.Text;
        _selectedReview.Rating = ratingValue;

        
        if (racketPicker.SelectedItem is Racket selectedRacket)
        {
            _selectedReview.RacketID = selectedRacket.ID;
        }

        await App.Database.SaveReviewAsync(_selectedReview);
        await DisplayAlert("Success", "Review updated successfully!", "OK");
        await Navigation.PopAsync();
    }

    private async void OnCancelClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}