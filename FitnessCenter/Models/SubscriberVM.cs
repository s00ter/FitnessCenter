using System.ComponentModel;

namespace FitnessCenter.Models
{
    public class SubscriberVM
    {
        [DisplayName("ID")]
        public int Id { get; set; }
        [DisplayName("Ф.И.О.")]
        public string Name { get; set; }
        [DisplayName("Группа")]
        public int GroupID { get; set; }
        [DisplayName("Занятия по абонементу")]
        public string SubscribtionName { get; set; }
    }
}
