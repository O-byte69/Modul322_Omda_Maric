using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using Modul322_Omda_Maric.Models;
using Modul322_Omda_Maric.Services;

namespace Modul322_Omda_Maric.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        public static MainViewModel Instance { get; private set; }

        [ObservableProperty]
        private ObservableCollection<SavedItem> products;

        [ObservableProperty]
        private double totalSaved;

        public IAsyncRelayCommand AddProductCommand { get; }
        public IRelayCommand<SavedItem> DeleteProductCommand { get; }
        public IRelayCommand InfoCommand { get; }

        public MainViewModel()
        {
            Instance = this;

            AddProductCommand = new AsyncRelayCommand(OnAddAsync);
            DeleteProductCommand = new RelayCommand<SavedItem>(OnDelete);
            InfoCommand = new RelayCommand(OnInfo);

            _ = LoadProductsAsync();
        }

        private async Task LoadProductsAsync()
        {
            Products = await FileService.LoadAsync();
        }

        // Wird aufgerufen, wenn Products neu gesetzt werden
        partial void OnProductsChanged(ObservableCollection<SavedItem> oldValue)
        {
            if (oldValue != null)
                oldValue.CollectionChanged -= Products_CollectionChanged;
            if (Products != null)
                Products.CollectionChanged += Products_CollectionChanged;

            RecalculateTotal();
        }

        // Bei jedem Hinzufügen oder Löschen neu berechnen
        private void Products_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
            => RecalculateTotal();

        private void RecalculateTotal()
            => TotalSaved = Products?.Sum(p => p.Price) ?? 0;

        private async Task OnAddAsync() =>
            await Shell.Current.GoToAsync("///AddProductPage");

        private void OnDelete(SavedItem item)
        {
            if (item == null) return;
            Products.Remove(item);
            _ = FileService.SaveAsync(Products);
        }

        private void OnInfo() =>
            _ = Shell.Current.GoToAsync("///InfoPage");
    }
}
