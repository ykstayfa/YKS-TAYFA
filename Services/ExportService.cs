// File: Services/ExportService.cs
// ExportService - DB, mesaj ve skor d��a aktarma + payla��m
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
                    Title = string.IsNullOrWhiteSpace(title) ? "Payla�" : title,
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

        // ---- SettingsPage.cs'de kullan�lan metodlar ----

        /// <summary>DB'yi JSON format�nda d��a aktar�r.</summary>
        public async Task<string> ExportDatabaseAsync()
        {
            // �imdilik sadece sahte bir i�erik d�nd�r�yoruz.
            string dbDump = "{ \"Database\": \"Backup i�erik buraya gelecek\" }";
            return await ExportTextAsync("database_backup.json", dbDump);
        }

        /// <summary>Mesajlar� CSV olarak d��a aktar�r.</summary>
        public async Task<string> ExportMessagesCsvAsync()
        {
            string csv = "Id,User,Message\n1,test_user,Merhaba";
            return await ExportTextAsync("messages.csv", csv);
        }

        /// <summary>Skorlar� CSV olarak d��a aktar�r.</summary>
        public async Task<string> ExportScoresCsvAsync()
        {
            string csv = "User,Score\nuser1,100\nuser2,80";
            return await ExportTextAsync("scores.csv", csv);
        }
    }
}
