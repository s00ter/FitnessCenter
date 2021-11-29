using System.ComponentModel;

namespace FitnessCenter.Models
{
    public class GroupVM
    {
        [DisplayName("ID")]
        public int Id { get; set; }
        [DisplayName("Название")]
        public string Name { get; set; }
        [DisplayName("ID инструктора")]
        public int InstructorId { get; set; }
        [DisplayName("Специализация инструктора")]
        public string InstructorSpecialization { get; set; }
        [DisplayName("Количество участников")]
        public int SubscribersCount { get; set; }
    }
}
