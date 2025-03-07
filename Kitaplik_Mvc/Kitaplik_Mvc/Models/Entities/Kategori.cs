namespace Kitaplik_Mvc.Models.Entities
{
    public class Kategori
    {
        public int Id { get; set; }
        public string Isim { get; set; }
        public List<Kitap> Kitaplar { get; set; } = new List<Kitap>();
    }
}
