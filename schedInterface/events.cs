﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Timers;
using System.Web.Script.Serialization;
using RestSharp;

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
            e.openstack_id = ev.openstack_id;

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
            e.openstack_id = ev.openstack_id;
            
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
    }

    #region openstackAPI

    public class openstackEvents
    {
        public List<OpenStackEvent> push_events()
        {
            var client = new RestClient("https://testresource-server.openstack.org/api/v1/");

            var request = new RestRequest("summits");

            request.AddParameter("access_token", "PAH7KdDOiWgZVWuQqYGpr3LCPHv-fj8RsO%7EeozlVBMeEDd8xezJHMx.4VH64T0MFTVV3k2KN");
            request.AddParameter("token_type", "Bearer");

            IRestResponse response = client.Execute(request);

            var mySessions = new JavaScriptSerializer().Deserialize<List<OpenStackEvent>>(response.Content);

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

    public class TimeZone
    {
        public string country_code { get; set; }
        public Decimal? latitude { get; set; }
        public Decimal? longitude { get; set; }
        public string comments { get; set; }
        public string name { get; set; }
        public Int32? offset { get; set; }
    }

    #endregion

}
