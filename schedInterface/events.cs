using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
                
                _events.Add(ev);
            }

            return _events;
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
    }

}
