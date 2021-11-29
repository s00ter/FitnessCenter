using FitnessCenter.Models;
using System.Collections.Generic;
using System.Linq;

namespace FitnessCenter.BL
{
    public static class Subscribers
    {
        private static FitnessCenterContext curContext;

        private static bool CompareStr(string str1, string str2)
        {
            return str1.ToLower().Contains(str2.ToLower());
        }

        public static IEnumerable<SubscriberVM> GetSubscribers(SubscriberSearchKey key, FitnessCenterContext db)
        {
            curContext = db;
            return key.Key switch
            {
                SubscriberKey.Группа => GetByGroupName(key.Value),
                SubscriberKey.Тренер => GetGroupByInstructor(key.Value),
                SubscriberKey.Занятия => GetGroupBySubscribtion(key.Value),
                SubscriberKey.День => GetGroupByDayOfWeek(key.Value),
                _ => new List<SubscriberVM>()
            };
        }

        private static IEnumerable<SubscriberVM> GetGroupByDayOfWeek(string dayOfWeek)
        {
            var subs = curContext.Subscribers.ToList()
                .Where(x =>
                CompareStr(curContext.Schedules.ToList().First(s => s.GroupId == x.GroupId).DayOfWeek, dayOfWeek));
            return SelectSubscriber(subs, curContext).ToList();
        }

        private static IEnumerable<SubscriberVM> GetGroupBySubscribtion(string subscName)
        {
            var subs = curContext.Subscribers.ToList()
                .Where(x => 
                CompareStr(curContext.Subscribtions.ToList().First(s => s.Id == x.SubscribtionId).Target, subscName));
            return SelectSubscriber(subs, curContext).ToList();
        }

        private static IEnumerable<SubscriberVM> GetGroupByInstructor(string insName)
        {
            return SelectSubscriber(
                curContext.Subscribers.ToList()
                .Where(x => curContext.Instructors.ToList().First(g => g.Id == x.GroupId).Id.ToString() == insName
            ), curContext).ToList();
        }

        private static IEnumerable<SubscriberVM> GetByGroupName(string gName)
        {
            return SelectSubscriber(
                curContext.Subscribers.ToList()
                .Where(x => CompareStr
                (
                    curContext.Groups.ToList().First(g => g.Id == x.GroupId).Name,
                    gName
                )
            ), curContext).ToList();
        }

        public static IEnumerable<SubscriberVM> SelectSubscriber(IEnumerable<Subscriber> source,
            FitnessCenterContext db)
        {
            return source.Select(x => new SubscriberVM()
            {
                Id = x.Id,
                Name = x.Name,
                GroupID = x.GroupId,
                SubscribtionName = db.Subscribtions.First(z => z.Id == x.SubscribtionId).Target
            });
        }

        public static bool CheckSubscriber(Subscriber item,
            FitnessCenterContext db)
        {
            if (db.Groups.Any(x => x.Id == item.GroupId)
                && db.Subscribtions.Any(x => x.Id == item.SubscribtionId))
            {
                return true;
            }
            return false;
        }
    }
}
