using System.ComponentModel;

namespace FitnessCenter.Models
{
    public class Subscribtion
    {
        [DisplayName("ID")]
        public int Id { get; set; }
        [DisplayName("Количество посещений")]
        public int VisitCount { get; set; }
        [DisplayName("Занятие")]
        public string Target { get; set; }
        [DisplayName("Цена")]
        public decimal Price { get; set; }
    }
}
