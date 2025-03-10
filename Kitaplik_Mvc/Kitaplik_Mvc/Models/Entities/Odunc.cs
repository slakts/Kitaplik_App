using System.ComponentModel.DataAnnotations.Schema;

namespace Kitaplik_Mvc.Models.Entities
{
    public class Odunc
    {
        public int Id { get; set; }
        public int UyeID { get; set; }
        public int KitapID { get; set; }

        public DateTime OduncAlimTarihi { get; set; }
        public DateTime? IadeTarihi { get; set; }

        [ForeignKey("KitapID")]
        public Kitap Kitap { get; set; }

        [ForeignKey("UyeID")]
        public Uye Uye { get; set; }
    }
}
