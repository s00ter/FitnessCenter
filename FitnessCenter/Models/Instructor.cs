using System.ComponentModel;

namespace FitnessCenter.Models
{
    public class Instructor
    {
        [DisplayName("ID")]
        public int Id { get; set; }
        [DisplayName("Стаж (лет)")]
        public int WorkExp { get; set; }
        [DisplayName("Информация об образовании")]
        public string EducationInfo { get; set; }
        [DisplayName("Специализация")]
        public string Specialization { get; set; }
        [DisplayName("Квалификация")]
        public string Qualification { get; set; }
    }
}
