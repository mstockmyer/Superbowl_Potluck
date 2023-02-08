using SB_MAUI.Model;

namespace SB_MAUI;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new AppShell();

	}
}
