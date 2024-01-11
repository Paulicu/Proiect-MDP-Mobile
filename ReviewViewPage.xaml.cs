using Proiect_MDP_Mobile.Models;

namespace Proiect_MDP_Mobile;

public partial class ReviewViewPage : ContentPage
{
    private Review _selectedReview;

    public ReviewViewPage(Review selectedReview)
    {
        InitializeComponent();
        _selectedReview = selectedReview;
        LoadReviewDetails();
    }

    private void LoadReviewDetails()
    {
        labelUserName.Text = _selectedReview.UserName;
        labelComment.Text = _selectedReview.Comment;
        labelRating.Text = _selectedReview.Rating.ToString();

        
        Racket associatedRacket = App.Database.GetRacketAsync(_selectedReview.RacketID).Result;
        if (associatedRacket != null)
            labelRacket.Text = associatedRacket.Name;
    }

    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}