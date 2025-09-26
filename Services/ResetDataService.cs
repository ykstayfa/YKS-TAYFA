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
        /// Uygulama veritabanýný sýfýrlar: dosyayý siler ve tablolarý yeniden oluþturur.
        /// </summary>
        public async Task ResetAsync()
        {
            // DB dosyasýný sil
            await _db.ResetDatabaseFileAsync();

            // Tablolarý yeniden kur
            await _db.InitAsync();
        }
    }
}
