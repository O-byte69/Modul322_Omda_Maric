// Datei: App.xaml.cs
using Microsoft.Maui.Controls;

namespace Modul322_Omda_Maric
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            // Shell statt NavigationPage nutzen
            MainPage = new AppShell();
        }
    }
}
