namespace EgeHavaleProjeOdevi.Models
{
    public class Ev
    {
        public int Id { get; set; }
        public string Ad { get; set; } = string.Empty;
        public string Detaylar { get; set; } = string.Empty;
        public string Adres { get; set; } = string.Empty;
        public decimal Fiyat { get; set; }
        public string GorselUrl { get; set; } = string.Empty;
        public string OdaSayisi { get; set; } = string.Empty;
        public int Metrekare { get; set; }
        public string Isitma { get; set; } = string.Empty;
        public bool Esyali { get; set; }
        public string Kimden { get; set; } = string.Empty;
        public DateTime EklenmeTarihi { get; set; } = DateTime.UtcNow;
        public bool Aktif { get; set; } = true;
    }
}
