using FitnessCenter.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FitnessCenter.BL
{
    public static class Groups
    {
        private static FitnessCenterContext db;

        public static IEnumerable<GroupCash> GetCash(SelectGr item, FitnessCenterContext db)
        {
            var first = db.Groups.ToList().Select(x =>
            {
                return new
                {
                    GroupId = x.Id,
                    GroupName = x.Name,
                    Subs = db.Subscribers.Where(x => x.GroupId == x.Id).ToList()
                };
            }).Select(x =>
            {
                return new GroupCash
                {
                    GroupName = x.GroupName,
                    Cash = x.Subs.Sum(sub =>
                    {
                        var subsc = db.Subscribtions.FirstOrDefault(b => b.Id == sub.SubscribtionId);
                        if (subsc == null)
                        {
                            return 0;
                        }
                        else
                        {
                            return subsc.Price;
                        }
                    })
                };
            });
            return first.ToList();
        }

        public static IEnumerable<GroupVM> GetGroup(FitnessCenterContext context, GroupSearchKey key)
        {
            db = context;
            return key.Key switch
            {
                SearchKey.День => SelectGroup(SearchByDay(key.Value), db).ToList(),
                SearchKey.Время => SelectGroup(SearchByTime(key.Value), db).ToList(),
                _ => new List<GroupVM>()
            };
        }

        private static IEnumerable<Group> SearchByTime(string time)
        {
            return db.Groups.ToList().Where(gr =>
                db.Schedules.Any(sch => (sch.GroupId == gr.Id) && CompareStr(sch.Time, time))).ToList();
        }

        private static IEnumerable<Group> SearchByDay(string time)
        {
            return db.Groups.ToList().Where(gr =>
                db.Schedules.ToList().Any(sch => (sch.GroupId == gr.Id) && CompareStr(sch.DayOfWeek, time))).ToList();
        }

        private static bool CompareStr(string str1, string str2)
        {
            return str1.ToLower().Contains(str2.ToLower());
        }

        public static IEnumerable<GroupVM> SelectGroup(IEnumerable<Group> source, FitnessCenterContext db)
        {
            return source.Select(x => new GroupVM()
            {
                Id = x.Id,
                Name = x.Name,
                InstructorId = db.Instructors.First(org => org.Id == x.InstructorId).Id,
                InstructorSpecialization = db.Instructors.First(org => org.Id == x.InstructorId).Specialization,
                SubscribersCount = x.SubscribersCount
            });
        }

        public static bool CheckGroup(Group item, FitnessCenterContext db)
        {
            if (db.Instructors.Any(x => x.Id == item.InstructorId))
            {
                return true;
            }
            return false;
        }
    }
}
