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
            // Implementați logica pentru navigarea către pagina cu lista de rachete (RacketListPage)
            Navigation.PushAsync(new RacketListPage());
        }

    }

}
