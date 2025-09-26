// File: Services/ExportService.cs
// ExportService - DB, mesaj ve skor dýþa aktarma + paylaþým
using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;
using Microsoft.Maui.ApplicationModel.DataTransfer;

namespace YKSTayfa.Services
{
    public class ExportService
    {
        private string GetExportPath(string fileName)
        {
            string folder = FileSystem.CacheDirectory;
            return Path.Combine(folder, fileName);
        }

        public async Task<string> ExportTextAsync(string fileName, string content)
        {
            if (string.IsNullOrWhiteSpace(fileName)) fileName = "export.txt";
            if (content == null) content = string.Empty;
            string path = GetExportPath(fileName);
            await File.WriteAllTextAsync(path, content, Encoding.UTF8);
            return path;
        }

        public async Task<string> ExportJsonAsync<T>(string fileName, T data)
        {
            string json = JsonSerializer.Serialize(
                data,
                new JsonSerializerOptions { WriteIndented = true }
            );
            return await ExportTextAsync(fileName, json);
        }

        public async Task ShareFileAsync(string path, string title)
        {
            if (File.Exists(path))
            {
                var request = new ShareFileRequest
                {
                    Title = string.IsNullOrWhiteSpace(title) ? "Paylaþ" : title,
                    File = new ShareFile(path)
                };
                await Share.Default.RequestAsync(request);
            }
        }

        public async Task<string> ExportAndShareTextAsync(string fileName, string content, string title)
        {
            string path = await ExportTextAsync(fileName, content);
            await ShareFileAsync(path, title);
            return path;
        }

        // ---- SettingsPage.cs'de kullanýlan metodlar ----

        /// <summary>DB'yi JSON formatýnda dýþa aktarýr.</summary>
        public async Task<string> ExportDatabaseAsync()
        {
            // Þimdilik sadece sahte bir içerik döndürüyoruz.
            string dbDump = "{ \"Database\": \"Backup içerik buraya gelecek\" }";
            return await ExportTextAsync("database_backup.json", dbDump);
        }

        /// <summary>Mesajlarý CSV olarak dýþa aktarýr.</summary>
        public async Task<string> ExportMessagesCsvAsync()
        {
            string csv = "Id,User,Message\n1,test_user,Merhaba";
            return await ExportTextAsync("messages.csv", csv);
        }

        /// <summary>Skorlarý CSV olarak dýþa aktarýr.</summary>
        public async Task<string> ExportScoresCsvAsync()
        {
            string csv = "User,Score\nuser1,100\nuser2,80";
            return await ExportTextAsync("scores.csv", csv);
        }
    }
}
