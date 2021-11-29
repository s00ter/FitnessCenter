using System.ComponentModel;

namespace FitnessCenter.Models
{
    public class Schedule
    {
        [DisplayName("ID")]
        public int Id { get; set; }
        [DisplayName("Номер группы")]
        public int GroupId { get; set; }
        [DisplayName("Время")]
        public string Time { get; set; }
        [DisplayName("День недели")]
        public string DayOfWeek { get; set; }
    }
}
