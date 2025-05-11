using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Modul322_Omda_Maric.Models;
using Modul322_Omda_Maric.Services;
using Microsoft.Maui.Controls;

namespace Modul322_Omda_Maric.ViewModels
{
    public partial class AddProductViewModel : ObservableObject
    {
        [ObservableProperty]
        private string newProductName;

        [ObservableProperty]
        private string newProductPrice;

        [ObservableProperty]
        private string selectedIcon;

        public IAsyncRelayCommand SaveCommand { get; }
        public IAsyncRelayCommand CancelCommand { get; }
        public IRelayCommand<string> IconSelectedCommand { get; }

        public AddProductViewModel()
        {
            SaveCommand = new AsyncRelayCommand(OnSaveAsync);
            CancelCommand = new AsyncRelayCommand(OnCancelAsync);
            IconSelectedCommand = new RelayCommand<string>(icon => SelectedIcon = icon);
        }

        private async Task OnSaveAsync()
        {
            // 1) Validierung
            if (string.IsNullOrWhiteSpace(NewProductName) ||
                !double.TryParse(NewProductPrice, out var price))
                return;

            // 2) Neuen Eintrag erstellen & speichern
            var item = new SavedItem
            {
                Name = NewProductName,
                Price = price,
                CreatedAt = DateTime.Now,
                IconSource = SelectedIcon ?? "default_icon.png"
            };
            MainViewModel.Instance.Products.Add(item);
            await FileService.SaveAsync(MainViewModel.Instance.Products);

            // 3) Felder zurücksetzen
            NewProductName = string.Empty;
            NewProductPrice = string.Empty;
            SelectedIcon = null;

            // 4) Zurück zur Hauptseite
            await Shell.Current.GoToAsync("///MainPage");
        }

        private async Task OnCancelAsync()
        {
            // Nur zurück zur Hauptseite (Felder bleiben, falls du sie erhalten willst)
            await Shell.Current.GoToAsync("///MainPage");
        }
    }
}
