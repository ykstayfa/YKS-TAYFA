// File: Services/DatabaseService.cs
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;
using SQLite;
using YKSTayfa.Models;

namespace YKSTayfa.Services
{
    public class DatabaseService
    {
        private static readonly object _lock = new object();
        private static SQLiteAsyncConnection _conn;

        public static string DatabaseFileName { get { return "ykstayfa.db3"; } }

        private static string DbPath
        {
            get
            {
                string folder = FileSystem.AppDataDirectory;
                return Path.Combine(folder, DatabaseFileName);
            }
        }

        /// <summary>Ba�lant�y� haz�rlar ve geri d�ner.</summary>
        public async Task<SQLiteAsyncConnection> GetConnectionAsync()
        {
            if (_conn != null) return _conn;

            lock (_lock)
            {
                if (_conn == null)
                {
                    var flags = SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.SharedCache;
                    _conn = new SQLiteAsyncConnection(DbPath, flags);
                }
            }
            await _conn.EnableWriteAheadLoggingAsync();
            return _conn;
        }

        /// <summary>Tablolar� olu�turur (Reset/ilk kurulum i�in �a�r�l�r).</summary>
        public async Task InitAsync()
        {
            var db = await GetConnectionAsync();
            await db.CreateTableAsync<User>();
            await db.CreateTableAsync<ChatMessage>();
            await db.CreateTableAsync<AdBanner>();
            await db.CreateTableAsync<ExamPaper>();
            await db.CreateTableAsync<HealthCheckResult>();
            await db.CreateTableAsync<AppSettings>();
        }

        // �rnek CRUD
        public async Task<int> AddUserAsync(User u)
        {
            var db = await GetConnectionAsync();
            return await db.InsertAsync(u);
        }

        public async Task<User> GetUserByGuidAsync(Guid id)
        {
            var db = await GetConnectionAsync();
            return await db.Table<User>().Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        /// <summary>DB dosyas�n� siler (varsa) � dikkatli kullan�n.</summary>
        public async Task ResetDatabaseFileAsync()
        {
            // Ba�lant�y� sal
            if (_conn != null) { await _conn.CloseAsync(); _conn = null; }

            string path = DbPath;
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public string GetDatabasePath() { return DbPath; }
    }
}
