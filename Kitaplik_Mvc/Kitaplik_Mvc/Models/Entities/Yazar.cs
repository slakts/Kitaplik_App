namespace Kitaplik_Mvc.Models.Entities
{
    public class Yazar
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public DateTime DogumTarihi { get; set; }
        public string Biyografi { get; set; }
        public List<Kitap> Kitaplar { get; set; } = new List<Kitap>();
        public string AdSoyad => $"{Ad} {Soyad}";
    }
}
