using Proiect_MDP_Mobile.Models;
using System.Collections.ObjectModel;

namespace Proiect_MDP_Mobile;

public partial class ReviewEntryPage : ContentPage
{
    public ObservableCollection<Racket> Rackets { get; set; }
    public ReviewEntryPage()
	{
		InitializeComponent();
        LoadRacketsAsync();
    }

    private async void LoadRacketsAsync()
    {
        Rackets = new ObservableCollection<Racket>(await App.Database.GetRacketsAsync());
        racketPicker.ItemsSource = Rackets;
    }

    private async void OnSaveReviewButtonClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(entryUserName.Text) || string.IsNullOrWhiteSpace(entryComment.Text) || string.IsNullOrWhiteSpace(entryRating.Text))
        {
            await DisplayAlert("Error", "All fields must be completed.", "OK");
            return;
        }

        if (!int.TryParse(entryRating.Text, out int ratingValue))
        {
            await DisplayAlert("Error", "Please enter a valid numeric value for rating.", "OK");
            return;
        }

        Racket selectedRacket = racketPicker.SelectedItem as Racket;

        if (selectedRacket == null)
        {
            await DisplayAlert("Error", "Please select a Racket.", "OK");
            return;
        }

        // Creare obiect Review cu datele introduse
        Review newReview = new Review
        {
            UserName = entryUserName.Text,
            Comment = entryComment.Text,
            Rating = ratingValue,
            RacketID = selectedRacket.ID
        };

        await DisplayAlert("Success", "Review added successfully!", "OK");

        await App.Database.SaveReviewAsync(newReview);
        await Navigation.PopAsync();
    }
}