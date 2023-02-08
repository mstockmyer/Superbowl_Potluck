using SB_MAUI.Model;
using SB_MAUI.ViewModel;

namespace SB_MAUI;

public partial class CredEditPage : ContentPage
{

    AWS_Creds VM;

    public CredEditPage()
	{
		InitializeComponent();

        VM = GlobalStorage.GetCcredentials();
        BindingContext= VM;

    }


}