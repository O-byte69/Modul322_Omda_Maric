// Datei: Services/FileService.cs
using System.Collections.ObjectModel;
using System.Text.Json;
using Modul322_Omda_Maric.Models;

namespace Modul322_Omda_Maric.Services
{
    public static class FileService
    {
        static string filePath = Path.Combine(FileSystem.AppDataDirectory, "savedItems.json");

        public static async Task<ObservableCollection<SavedItem>> LoadAsync()
        {
            if (!File.Exists(filePath))
                return new ObservableCollection<SavedItem>();

            var json = await File.ReadAllTextAsync(filePath);
            return JsonSerializer.Deserialize<ObservableCollection<SavedItem>>(json)
                   ?? new ObservableCollection<SavedItem>();
        }

        public static async Task SaveAsync(ObservableCollection<SavedItem> items)
        {
            var json = JsonSerializer.Serialize(items);
            await File.WriteAllTextAsync(filePath, json);
        }
    }
}
