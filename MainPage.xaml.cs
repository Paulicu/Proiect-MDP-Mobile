namespace Proiect_MDP_Mobile
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void OnViewRacketsClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RacketListPage());
        }

        private void OnViewReviewsClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ReviewListPage());
        }

    }

}
