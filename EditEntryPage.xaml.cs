using SB_MAUI.Model;
using SB_MAUI.ViewModel;

namespace SB_MAUI;

public partial class EditEntryPage : ContentPage
{

    ContestViewModel VM;

    public EditEntryPage()
	{
		InitializeComponent();

        VM = new ContestViewModel();
        BindingContext= VM;

    }


}