using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;

namespace Modul322_Omda_Maric.ViewModels
{
    public partial class InfoViewModel : ObservableObject
    {
        public IAsyncRelayCommand HomeCommand { get; }

        public InfoViewModel()
        {
            HomeCommand = new AsyncRelayCommand(GoHomeAsync);
        }

        // und hier auch absolute URI
        private async Task GoHomeAsync() =>
            await Shell.Current.GoToAsync("///MainPage");
    }
}
