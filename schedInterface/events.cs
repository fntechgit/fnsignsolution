using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace schedInterface
{
    public class events
    {
        private schedDataContext db = new schedDataContext();

        public List<Event> all()
        {
            List<Event> _events = new List<Event>();

            var result = from evts in db.events
                orderby evts.title
                select evts;

            foreach (var item in result)
            {
                Event ev = new Event();

                ev.api_key = item.api_key;
                ev.event_end = item.event_end;
                ev.event_start = item.event_start;
                ev.id = item.id;
                ev.interval = item.interval;
                ev.title = item.title;
                ev.url = item.url;
                ev.last_update = item.last_update;
                ev.t_hashtag = item.t_hashtag;
                ev.t_username = item.t_username;
                
                _events.Add(ev);
            }

            return _events;
        }

        public Boolean delete(Int32 id)
        {
            @event ev = db.events.Single(x => x.id == id);

            db.events.DeleteOnSubmit(ev);

            db.SubmitChanges();

            return true;
        }

        public List<Event> need_updating()
        {
            List<Event> _events = new List<Event>();

            foreach (var item in db.events_to_update())
            {
                Event e = new Event();

                e.api_key = item.api_key;
                e.event_end = item.event_end;
                e.event_start = item.event_start;
                e.id = item.id;
                e.interval = item.interval;
                e.last_update = item.last_update;
                e.title = item.title;
                e.url = item.url;
                e.t_hashtag = item.t_hashtag;
                e.t_username = item.t_username;

                if (!string.IsNullOrEmpty(e.t_hashtag))
                {
                    e.hashtags = e.t_hashtag.Split(',').ToList();
                }

                _events.Add(e);
            }

            return _events;
        }

        public Event update(Event ev)
        {
            @event evv = db.events.Single(x => x.id == ev.id);

            evv.last_update = DateTime.Now;

            db.SubmitChanges();

            return ev;
        }

        public Event appupdate(Event ev)
        {
            @event e = db.events.Single(x => x.id == ev.id);

            e.api_key = ev.api_key;
            e.event_end = ev.event_end;
            e.event_start = ev.event_start;
            e.interval = ev.interval;
            e.title = ev.title;
            e.url = ev.url;
            e.t_hashtag = ev.t_hashtag;
            e.t_username = ev.t_username;

            db.SubmitChanges();

            return ev;
        }

        public Event add(Event ev)
        {
            @event e = new @event();

            e.api_key = ev.api_key;
            e.event_end = ev.event_end;
            e.event_start = ev.event_start;
            e.interval = ev.interval;
            e.last_update = DateTime.Now;
            e.title = ev.title;
            e.url = ev.url;
            e.t_hashtag = ev.t_hashtag;
            e.t_username = ev.t_username;
            
            db.events.InsertOnSubmit(e);

            db.SubmitChanges();

            ev.id = e.id;

            return ev;
        }

        public Event single(Int32 id)
        {
            var result = from evs in db.events
                where evs.id == id
                select evs;

            Event ev = new Event();

            foreach (var item in result)
            {
                ev.api_key = item.api_key;
                ev.event_end = item.event_end;
                ev.event_start = item.event_start;
                ev.id = item.id;
                ev.interval = item.interval;
                ev.last_update = item.last_update;
                ev.title = item.title;
                ev.url = item.url;
                ev.t_hashtag = item.t_hashtag;
                ev.t_username = item.t_username;

                if (!string.IsNullOrEmpty(ev.t_hashtag))
                {
                    ev.hashtags = ev.t_hashtag.Split(',').ToList();
                }
            }

            return ev;
        }

        /// <summary>
        /// Selects All Events in the Database
        /// </summary>
        /// <returns>Enumerable List of Event Models</returns>
        public List<Event> select_list()
        {
            List<Event> _events = new List<Event>();

            var result = from eve in db.events
                         orderby eve.title
                         select eve;

            foreach (var ev in result)
            {
                Event e = new Event();

                e.id = ev.id;
                e.interval = ev.interval;
                e.title = ev.title;
                e.last_update = ev.last_update;
                e.event_start = ev.event_start;
                e.event_end = ev.event_end;
                e.interval = ev.interval;
                e.url = ev.url;
                e.api_key = ev.api_key;

                _events.Add(e);
            }

            return _events;
        }
    }

    public class Event
    {
        public Int32 id { get; set; }    
        public string title { get; set; }
        public DateTime? event_start { get; set; }
        public DateTime? event_end { get; set; }
        public string api_key { get; set; }
        public string url { get; set; }
        public Int32 interval { get; set; }
        public DateTime last_update { get; set; }
        public string t_username { get; set; }
        public string t_hashtag { get; set; }
        public List<string> hashtags { get; set; }
    }

}
