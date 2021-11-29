using FitnessCenter.Models;
using System.Collections.Generic;
using System.Linq;

namespace FitnessCenter.BL
{
    public static class Instructors
    {
        private static FitnessCenterContext db;

        public static IEnumerable<Instructor> GetInstructors(SearchProp prop,
                                                             string str,
                                                             FitnessCenterContext db)
        {
            Instructors.db = db;
            return prop switch
            {
                SearchProp.Специализация => SearchBySpecialization(str),
                SearchProp.Квалификация => SearchByQualification(str),
                SearchProp.Группа => SearchByGroup(str),
                SearchProp.Стаж => SearchByExp(str),
                _ => new List<Instructor>()
            };
        }

        private static bool CompareStr(string str1, string str2)
        {
            return str1.ToLower().Contains(str2.ToLower());
        }

        private static IEnumerable<Instructor> SearchBySpecialization(string str)
        {
            return db.Instructors.ToList().Where(x => CompareStr(x.Specialization, str));
        }

        private static IEnumerable<Instructor> SearchByQualification(string str)
        {
            return db.Instructors.ToList().Where(x => CompareStr(x.Qualification, str));
        }

        private static IEnumerable<Instructor> SearchByGroup(string str)
        {
            var groups = db.Groups.Where(x => x.Id.ToString() == str).ToList();

            return db.Instructors.ToList().Where(x => groups.Any(z => z.InstructorId == x.Id));
        }

        private static IEnumerable<Instructor> SearchByExp(string str)
        {
            return db.Instructors.ToList().Where(x => x.WorkExp.ToString() == str);
        }
    }
}
