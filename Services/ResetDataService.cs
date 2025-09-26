// File: Services/ResetDataService.cs
using System.Threading.Tasks;

namespace YKSTayfa.Services
{
    public class ResetDataService
    {
        private readonly DatabaseService _db;

        public ResetDataService()
        {
            _db = new DatabaseService();
        }

        /// <summary>
        /// Uygulama veritaban�n� s�f�rlar: dosyay� siler ve tablolar� yeniden olu�turur.
        /// </summary>
        public async Task ResetAsync()
        {
            // DB dosyas�n� sil
            await _db.ResetDatabaseFileAsync();

            // Tablolar� yeniden kur
            await _db.InitAsync();
        }
    }
}
