using Proiect_MDP_Mobile.Models;

namespace Proiect_MDP_Mobile;

public partial class ServicePage : ContentPage
{
    async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var slist = (ServiceList)BindingContext;
        slist.Date = DateTime.Now;
        await App.Database.SaveServiceListAsync(slist);
        await Navigation.PopAsync();
    }

    async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        var slist = (ServiceList)BindingContext;
        await App.Database.DeleteServiceListAsync(slist);
        await Navigation.PopAsync();
    }

    public ServicePage()
	{
		InitializeComponent();
	}
}