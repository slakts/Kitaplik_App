namespace Kitaplik_Mvc.Models.Entities
{
    public class Kitap
    {
        public int Id { get; set; }
        public string Baslik { get; set; }
        public DateTime YayinTarihi { get; set; }
        public string ISBN { get; set; }
        public int KategoriId { get; set; }
        public Kategori Kategori { get; set; }
        public int YazarId { get; set; }
        public Yazar Yazar{ get; set; }
        public string? Image { get; set; }
    }
}
