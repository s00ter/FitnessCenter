using System.ComponentModel;

namespace FitnessCenter.Models
{
    public class Subscriber
    {
        [DisplayName("ID")]
        public int Id { get; set; }
        [DisplayName("Ф.И.О.")]
        public string Name { get; set; }
        [DisplayName("ID группы")]
        public int GroupId { get; set; }
        [DisplayName("ID абонента")]
        public int SubscribtionId { get; set; }
    }
}
