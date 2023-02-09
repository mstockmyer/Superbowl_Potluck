using SB_MAUI.Model;
using SB_MAUI.ViewModel;
using System.Xml.Serialization;

namespace SB_MAUI;

public partial class MainPage : ContentPage
{

    ContestViewModel VM;


	public MainPage()
	{
		InitializeComponent();

        VM = new ContestViewModel();   

        BindingContext = VM;
	}

	private async void OnSimLeaderboardClicked(object sender, EventArgs e)
	{
        bool answer = await DisplayAlert("Simulate Leaderboard?", "Would you like to delete all the current live contest entries and insert simulated contest entries?", "Yes", "No");

        if( answer == true )
        {
            VM.SimulateLeaderboard();
        }

    }
    private async void OnEditEntryClicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new EditEntryPage());
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if( false == await VM.SaveAsync())
        {
            await DisplayAlert("Load Failed", $"Attempt to load data from local storage failed.", "OK");
        }

    }

    private async void OnLoadClicked(object sender, EventArgs e)
    {
        if( false == await VM.LoadAsync())
        {
            await DisplayAlert("Load Failed", $"Attempt to load data from local storage failed.", "OK");
        }


    }

    private void OnNewClicked(object sender, EventArgs e)
    {
        VM.NewEntry();


    }

    private async void OnDeleteAllClicked(object sender, EventArgs e)
    {
        bool answer = await DisplayAlert("Delete All Data?", "Would you like to delete all the current live contest entries?", "Yes", "No");

        if (answer == true)
        {
            if (false == await VM.DeleteAllAsync())
            {
                await DisplayAlert("Delete All Failed", $"Attempt to delete all data from local storage failed.", "OK");
            }
        }
    }



    private async void OnUploadClicked(object sender, EventArgs e)
    {
        if (false == await VM.UploadAsync())
        {
            await DisplayAlert("Upload to AWS Failed", $"Attempt upload contest entry data to AWS failed.", "OK");
        }
    }

    
    private async void OnCredEditClicked(object sender, EventArgs e)
    {

        await Navigation.PushModalAsync(new CredEditPage());

    }






}

