using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace schedInterface
{
    public class notify
    {
        private schedDataContext db = new schedDataContext();

        public bool add_notify_user(EventNotifyUser enu)
        {
            events_notify_user entity = new events_notify_user();
            entity.event_id = enu.event_Id;
            entity.after = enu.after;
            entity.user_id = enu.user_id;
            this.db.events_notify_users.InsertOnSubmit(entity);
            this.db.SubmitChanges();
            enu.id = entity.id;
            return true;
        }

        public List<Event> events_notify_for_user(int user_id)
        {
            List<Event> eventList = new List<Event>();
            foreach (events_notify_by_usersResult eventsNotifyByUser in (IEnumerable<events_notify_by_usersResult>)this.db.events_notify_by_users(new int?(user_id)))
                eventList.Add(new Event()
                {
                    event_end = eventsNotifyByUser.event_end,
                    event_start = eventsNotifyByUser.event_start,
                    id = eventsNotifyByUser.id,
                    interval = eventsNotifyByUser.interval,
                    last_update = eventsNotifyByUser.last_update,
                    title = eventsNotifyByUser.title,
                    t_hashtag = eventsNotifyByUser.t_hashtag,
                    t_username = eventsNotifyByUser.t_username
                });
            return eventList;
        }

        public List<User> users_notify_for_event(int event_id)
        {
            List<User> userList = new List<User>();
            foreach (users_notify_by_eventResult notifyByEventResult in (IEnumerable<users_notify_by_eventResult>)this.db.users_notify_by_event(new int?(event_id)))
                userList.Add(new User()
                {
                    carrier = notifyByEventResult.carrier.Value,
                    mms = notifyByEventResult.mms,
                    mobile = notifyByEventResult.mobile,
                    active = notifyByEventResult.active,
                    company = notifyByEventResult.company,
                    created = notifyByEventResult.created,
                    email = notifyByEventResult.email,
                    first_name = notifyByEventResult.first_name,
                    id = notifyByEventResult.id,
                    last_name = notifyByEventResult.last_name,
                    notify_every_minutes = notifyByEventResult.notify_every_minutes,
                    password = notifyByEventResult.pwd,
                    picture = notifyByEventResult.pic,
                    security = notifyByEventResult.security
                });
            return userList;
        }
    }

    #region EventNotifyUser
    public class EventNotifyUser
    {
        public int id { get; set; }

        public int event_Id { get; set; }

        public int user_id { get; set; }

        public int after { get; set; }
    }
    #endregion
}

