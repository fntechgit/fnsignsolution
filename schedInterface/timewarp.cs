using System;

namespace schedInterface
{
    public class timewarp
    {
        private events _events = new events();

        public DateTime get_time(int eventId)
        {
            Event @event = this._events.single(eventId);
            if (@event.timewarp.HasValue)
                return Convert.ToDateTime((object)@event.timewarp);
            return DateTime.Now;
        }

        public DateTime display(int eventId)
        {
            DateTime dateTime = DateTime.Now;
            if (this._events.single(eventId).overridedisplay)
                dateTime = this.get_time(eventId);
            return dateTime;
        }

        public DateTime manager(int eventId)
        {
            DateTime dateTime = DateTime.Now;
            if (this._events.single(eventId).overridetime)
                dateTime = this.get_time(eventId);
            return dateTime;
        }

        public bool runtime(int id, int minutes)
        {
            bool flag = false;
            Event ev = this._events.single(id);
            if (ev.timerun && ev.timewarp.HasValue)
            {
                ev.timewarp = new DateTime?(Convert.ToDateTime((object)ev.timewarp).AddMinutes((double)minutes));
                ev.last_timerun = new DateTime?(DateTime.Now);
                this._events.fullupdate(ev);
                flag = true;
            }
            return flag;
        }
    }
}
