using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Timers;
using System.Web.Script.Serialization;
using RestSharp;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

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
                ev.openstack_id = item.openstack_id;

                ev.eod_category = item.eod_category;
                ev.eod_description = item.eod_description;
                ev.eod_time = item.eod_time;
                ev.eod_title = item.eod_title;
                ev.last_timerun = item.last_timerun;
                ev.timewarp = item.timewarp;
                ev.timerun = item.timerun;
                ev.full_session = item.full_session;

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

                //e.eod_title = item.eod_title;
                //e.eod_category = item.eod_category;
                //e.eod_description = item.eod_description;
                //e.eod_time = item.eod_time;
                //e.timewarp = item.timewarp;
                //e.timerun = item.timerun;
                //e.overridedisplay = item.overridedisplay;
                //e.overridetime = item.overridetime;
                //e.timezone = item.timezone;

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
            e.openstack_id = ev.openstack_id;
            e.offset = ev.offset;

            db.SubmitChanges();

            return ev;
        }

        public Event fullupdate(Event ev)
        {
            @event @event = this.db.events.Single<@event>((Expression<Func<@event, bool>>)(x => x.id == ev.id));
            @event.api_key = ev.api_key;
            @event.event_end = ev.event_end;
            @event.event_start = ev.event_start;
            @event.interval = ev.interval;
            @event.title = ev.title;
            @event.url = ev.url;
            @event.openstack_id = ev.openstack_id;
            @event.offset = ev.offset;
            @event.eod_category = ev.eod_category;
            @event.eod_description = ev.eod_description;
            @event.eod_time = ev.eod_time;
            @event.eod_title = ev.eod_title;
            @event.t_username = ev.t_username;
            @event.t_hashtag = ev.t_hashtag;
            @event.timewarp = ev.timewarp;
            @event.timerun = ev.timerun;
            @event.overridedisplay = ev.overridedisplay;
            @event.overridetime = ev.overridetime;
            @event.timezone = ev.timezone;
            @event.last_timerun = ev.last_timerun;
            @event.full_session = ev.full_session;
            this.db.SubmitChanges();
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
            e.openstack_id = ev.openstack_id;
            e.offset = ev.offset;
            e.eod_category = ev.eod_category;
            e.eod_description = ev.eod_description;
            e.eod_time = ev.eod_time;
            e.eod_title = ev.eod_title;
            e.timewarp = ev.timewarp;
            e.timerun = ev.timerun;
            e.overridedisplay = ev.overridedisplay;
            e.overridetime = ev.overridetime;
            e.timezone = ev.timezone;
            e.full_session = ev.full_session;

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
                ev.openstack_id = item.openstack_id;
                ev.eod_description = item.eod_description;
                ev.eod_category = item.eod_category;
                ev.eod_time = item.eod_time;
                ev.eod_title = item.eod_title;
                ev.overridedisplay = item.overridedisplay;
                ev.overridetime = item.overridetime;
                ev.timerun = item.timerun;
                ev.timewarp = item.timewarp;
                ev.timezone = item.timezone.Value;
                ev.full_session = item.full_session;

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
                e.openstack_id = ev.openstack_id;
                e.eod_description = ev.eod_description;
                e.eod_category = ev.eod_category;
                e.eod_time = ev.eod_time;
                e.eod_title = ev.eod_title;
                e.timewarp = ev.timewarp;
                e.timerun = ev.timerun;
                e.overridedisplay = ev.overridedisplay;
                e.overridetime = ev.overridetime;
                e.timezone = ev.timezone.Value;
                e.full_session = ev.full_session;

                _events.Add(e);
            }

            return _events;
        }

        public Event find_by_openstack_id(Int32 id)
        {
            return all().Where(x => x.openstack_id == id).Single();
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
        public Int32? openstack_id { get; set; }
        public Int32 offset { get; set; }

        public DateTime? last_timerun { get; set; }
        public Boolean overridedisplay { get; set; }
        public Boolean overridetime { get; set; }
        public Boolean timerun { get; set; }
        public DateTime? timewarp { get; set; }

        public string eod_category { get; set; }
        public string eod_description { get; set; }
        public string eod_time { get; set; }
        public string eod_title { get; set; }
        public string full_session { get; set; }
        public Int32 timezone { get; set; }


    }

    #region openstackAPI

    public class openstackEvents
    {
        private osettings _settings = new osettings();

        public List<OpenStackEvent> push_events()
        {
            var client = new RestClient("https://openstackid-resources.openstack.org/api/v1/");

            var request = new RestRequest("summits");

            request.AddParameter("access_token", _settings.auth_key());
            request.AddParameter("token_type", "Bearer");

            IRestResponse response = client.Execute(request);

            var mySessions = new JavaScriptSerializer().Deserialize<List<OpenStackEvent>>(response.Content);

            return mySessions;
        }

        public List<OpenStackEventType> push_event_types(Int32 id)
        {
            var client = new RestClient("https://openstackid-resources.openstack.org/api/v1/");

            var request = new RestRequest("summits/" + id + "/event-types");

            request.AddParameter("access_token", _settings.auth_key());
            request.AddParameter("token_type", "Bearer");

            IRestResponse response = client.Execute(request);

            var mySessions = new JavaScriptSerializer().Deserialize<List<OpenStackEventType>>(response.Content);

            return mySessions;
        }
    }

    public class OpenStackEvent
    {
        public Int32 id { get; set; }
        public string name { get; set; }
        public Int32? start_date { get; set; }
        public Int32? end_date { get; set; }
        public Int32? start_showing_venues_date { get; set; }
        public Boolean active { get; set; }
        public TimeZone time_zone { get; set; }
        public string logo { get; set; }


    }

    public class event_types
    {
        private readonly schedDataContext db = new schedDataContext();

        public Boolean add(EventType t)
        {
            // need to add
            event_type et = new event_type();

            et.event_type_id = t.event_type_id;
            et.title = t.title;
            et.bg_color = t.bgcolor;
            et.event_id = t.event_id;

            db.event_types.InsertOnSubmit(et);

            db.SubmitChanges();

            return true;
        }

        public Boolean update(EventType t)
        {

            // need to update
            event_type et = db.event_types.Single(x => x.id == t.id);

            et.event_type_id = t.event_type_id;
            et.title = t.title;
            et.bg_color = t.bgcolor;

            db.SubmitChanges();

            return true;
        }



        public Boolean addUpdate(EventType t)
        {
            var result = from ets in db.event_types
                         where ets.event_type_id == t.event_type_id
                         select ets;

            if (result.Any())
            {
                // need to update
                event_type et = db.event_types.Single(x => x.event_type_id == t.event_type_id);

                et.title = t.title;
                et.event_id = t.event_id;

                db.SubmitChanges();
            }
            else
            {
                // need to add
                event_type et = new event_type();

                et.event_type_id = t.event_type_id;
                et.title = t.title;
                et.event_id = t.event_id;

                db.event_types.InsertOnSubmit(et);

                db.SubmitChanges();
            }

            return true;
        }

        public List<EventType> by_event(Int32 id)
        {
            List<EventType> _types = new List<EventType>();

            var result = from tps in db.event_types
                         where tps.event_id == id
                         select tps;

            foreach (var item in result)
            {
                EventType t = new EventType();

                t.event_type_id = item.event_type_id;
                t.event_id = id;
                t.title = item.title;
                t.id = item.id;
                t.bgcolor = item.bg_color;

                _types.Add(t);
            }

            return _types;
        }

        public EventType single(Int32 id)
        {
            var result = from evts in db.event_types
                         where evts.id == id
                         select evts;

            EventType t = new EventType();

            foreach (var item in result)
            {
                t.id = item.id;
                t.title = item.title;
                t.event_type_id = item.event_type_id;
                t.event_id = item.event_id;
                t.bgcolor = item.bg_color;
            }

            return t;
        }

        public Boolean delete(Int32 id)
        {
            event_type ev = db.event_types.Single(x => x.id == id);

            db.event_types.DeleteOnSubmit(ev);

            db.SubmitChanges();

            return true;
        }

        public Boolean delete_by_event(Int32 id)
        {
            List<EventType> _types = new List<EventType>();

            var result = from tps in db.event_types
                         where tps.event_id == id
                         select tps;

            foreach (var item in result)
            {
                db.event_types.DeleteOnSubmit(item);
            }

            db.SubmitChanges();

            return true;
        }

        public List<EventType> import(string conference_url, string api_key, int event_id)
        {
            RestClient restClient = new RestClient(conference_url + "/api");
            RestRequest restRequest = new RestRequest("site/sync", Method.POST);
            restRequest.AddParameter("api_key", (object)api_key);
            restRequest.AddParameter("format", (object)"json");
            restRequest.AddHeader("User-Agent", "schedInterface");

            JavaScriptSerializer jsdes = new JavaScriptSerializer();
            IRestResponse response = restClient.Execute((IRestRequest)restRequest);
            var data = jsdes.Deserialize<OCPEventType>(response.Content);

            var result = new List<EventType>();
            var bgcolor = "";
            var rg = @"(?<=\.ev_TYPENUMBER \{ border-color: ).*?(?= \})";
            foreach (var item in data.types)
            {
                bgcolor = Regex.Match(data.style, rg.Replace("TYPENUMBER", item.Key)).Value;
                result.Add(new EventType() { event_id = event_id , event_type_id = int.Parse(item.Key), bgcolor = bgcolor, title = item.Value.name });
            }

            return result;
        }

    }

    public class TimeZone
    {
        public string country_code { get; set; }
        public Decimal? latitude { get; set; }
        public Decimal? longitude { get; set; }
        public string comments { get; set; }
        public string name { get; set; }
        public Int32? offset { get; set; }
    }

    public class EventType
    {
        public Int32 id { get; set; }
        public Int32 event_type_id { get; set; }
        public string title { get; set; }
        public string bgcolor { get; set; }
        public Int32 event_id { get; set; }
        public string name { get; set; }
    }

    public class OpenStackEventType
    {
        public Int32 id { get; set; }
        public string name { get; set; }
    }

    public class OCPEventType
    {
        public string style { get; set; }
        public Dictionary<string, EventType> types { get; set; }
    }

    #endregion

}
