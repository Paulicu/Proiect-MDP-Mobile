using Proiect_MDP_Mobile.Models;

namespace Proiect_MDP_Mobile;

public partial class ServiceViewPage : ContentPage
{
    private Service _service;

    public ServiceViewPage(Service service)
    {
        InitializeComponent();
        _service = service;
        BindingContext = _service;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (_service != null)
        {
            labelType.Text = _service.Type;
            labelDescription.Text = _service.Description;
            labelDate.Text = _service.Date.ToString("MMMM dd, yyyy");
            labelRacket.Text = (_service.RacketID != 0) ? App.Database.GetRacketAsync(_service.RacketID).Result?.Name : "N/A";
        }
    }

    private async void OnEditServiceClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ServiceEditPage(_service));
    }

    private async void OnBackToListClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}