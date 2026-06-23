namespace module6_contact
{
    public partial class App : Application
        {
            public App()
            {
                InitializeComponent();
                MainPage = new NavigationPage(new MainPage())
                {
                    BarBackgroundColor = Colors.White,
                    BarTextColor = Color.FromArgb("#1C1C1E")
                };
            }
        }
}