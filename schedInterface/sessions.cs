using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Text.RegularExpressions;
using RestSharp;
using System.Web.Script.Serialization;

namespace schedInterface
{
    public class sessions
    {
        private schedDataContext db = new schedDataContext();
        private locations _locations = new locations();

        public List<Session> by_event(Int32 id)
        {
            List<Session> _sessions = new List<Session>();

            var result = from sess in db.sessions
                where sess.event_id == id
                orderby sess.title
                select sess;

            foreach (var item in result)
            {
                Session s = new Session();

                s.description = item.description;
                s.end = Convert.ToDateTime(item.session_end);
                s.event_type = item.type;
                s.goers = item.attendees.ToString();
                s.id = item.id.ToString();
                s.event_key = item.event_key;
                s.internal_id = item.id;
                s.name = item.title;
                s.speakers = item.speakers;
                s.start = Convert.ToDateTime(item.session_start);
                s.venue = item.venue;
                
                _sessions.Add(s);
            }

            return _sessions;
        }

        public List<Session> by_event_by_location_by_day(Int32 event_id, string location)
        {
            List<Session> _sessions = new List<Session>();

            foreach (var item in db.sessions_by_event_location_date(event_id, location))
            {
                Session s = new Session();

                s.active = item.active.ToString();
                s.description = item.description;
                s.end = Convert.ToDateTime(item.session_end);
                s.event_id = event_id;
                s.event_type = item.type;
                s.goers = item.attendees.ToString();
                s.id = item.id.ToString();
                s.event_key = item.event_key;
                s.internal_id = item.id;
                s.name = item.title;
                s.speakers = item.speakers;
                s.start = Convert.ToDateTime(item.session_start);
                s.event_start = Convert.ToDateTime(s.start).ToShortTimeString();
                s.venue = item.venue;

                _sessions.Add(s);
            }

            return _sessions;
        }

        public Boolean delete(Int32 id)
        {
            session s = db.sessions.Single(x => x.id == id);

            db.sessions.DeleteOnSubmit(s);

            db.SubmitChanges();

            return true;
        }

        public List<Session> by_venue(Int32 event_id, Int32 venue_id)
        {
            List<Session> _sessions = new List<Session>();

            var result = from sess in db.sessions
                         where sess.event_id == event_id && sess.venue_id==venue_id.ToString()
                         orderby sess.title
                         select sess;

            foreach (var item in result)
            {
                Session s = new Session();

                s.description = item.description;
                s.end = Convert.ToDateTime(item.session_end);
                s.event_type = item.type;
                s.goers = item.attendees.ToString();
                s.id = item.id.ToString();
                s.internal_id = item.id;
                s.name = item.title;
                s.speakers = item.speakers;
                s.start = Convert.ToDateTime(item.session_start);
                s.venue = item.venue;

                _sessions.Add(s);
            }

            return _sessions;
        }

        public List<Session> marketplace()
        {
            List<Session> _sessions = new List<Session>();

            var result = from sess in db.sessions
                         where sess.event_id == 1000 && sess.venue_id == "348337" && Convert.ToDateTime(sess.session_start).Date==DateTime.Now.Date
                         orderby sess.session_start
                         select sess;

            foreach (var item in result)
            {
                Session s = new Session();

                s.description = item.description;
                s.end = Convert.ToDateTime(item.session_end);
                s.event_type = item.type;
                s.goers = item.attendees.ToString();
                s.id = item.id.ToString();
                s.internal_id = item.id;
                s.name = item.title;
                s.speakers = item.speakers;
                s.start = Convert.ToDateTime(item.session_start);
                s.venue = item.venue;

                _sessions.Add(s);
            }

            return _sessions;
        }

        public List<Session> brownbag()
        {
            List<Session> _sessions = new List<Session>();

            var result = from sess in db.sessions
                         where sess.event_id == 1000 && sess.type=="#vBrownBag" && Convert.ToDateTime(sess.session_start).Date == DateTime.Now.Date
                         orderby sess.session_start, sess.title
                         select sess;

            foreach (var item in result)
            {
                Session s = new Session();

                s.description = item.description;
                s.end = Convert.ToDateTime(item.session_end);
                s.event_type = item.type;
                s.goers = item.attendees.ToString();
                s.id = item.id.ToString();
                s.internal_id = item.id;
                s.name = item.title;
                s.speakers = item.speakers;
                s.start = Convert.ToDateTime(item.session_start);
                s.venue = item.venue;

                _sessions.Add(s);
            }

            return _sessions;
        }

        public List<Session> summit()
        {
            List<Session> _sessions = new List<Session>();

            var result = from sess in db.sessions
                         where sess.event_id == 1001 && Convert.ToDateTime(sess.session_start).Date == DateTime.Now.Date
                         orderby sess.session_start, sess.title
                         select sess;

            foreach (var item in result)
            {
                Session s = new Session();

                s.description = item.description;
                s.end = Convert.ToDateTime(item.session_end);
                s.event_type = item.type;
                s.goers = item.attendees.ToString();
                s.id = item.id.ToString();
                s.internal_id = item.id;
                s.name = item.title;
                s.speakers = item.speakers;
                s.start = Convert.ToDateTime(item.session_start);
                s.venue = item.venue;

                _sessions.Add(s);
            }

            return _sessions;
        }

        public List<Session> by_type(Int32 event_id, string type)
        {
            List<Session> _sessions = new List<Session>();

            var result = from sess in db.sessions
                         where sess.event_id == event_id && sess.type==type
                         orderby sess.title
                         select sess;

            foreach (var item in result)
            {
                Session s = new Session();

                s.description = item.description;
                s.end = Convert.ToDateTime(item.session_end);
                s.event_type = item.type;
                s.goers = item.attendees.ToString();
                s.id = item.id.ToString();
                s.internal_id = item.id;
                s.name = item.title;
                s.speakers = item.speakers;
                s.start = Convert.ToDateTime(item.session_start);
                s.venue = item.venue;

                _sessions.Add(s);
            }

            return _sessions;
        }

        public List<Session> all(string conference_url, string api_key)
        {
            var client = new RestClient(conference_url + "/api");

            var request = new RestRequest("session/list");

            request.AddParameter("api_key", api_key);
            request.AddParameter("format", "json");

            IRestResponse response = client.Execute(request);



            var mySessions = new JavaScriptSerializer().Deserialize<List<Session>>(response.Content);

            return mySessions;
        }

        public Boolean clean_summit()
        {
            db.clean_design_summit();

            return true;
        }

        public Session add(Session s, Int32 event_id)
        {
            // check to see if one exists;
            var result = from sess in db.sessions
                where sess.event_key == s.event_key && sess.event_id==event_id
                select sess;

            Regex rgx = new Regex(@"[0-9 :-]");



            if (result.Any())
            {
                // this is an update procedure
                foreach (var se in result)
                {
                    se.active = s.active == "Y";
                    se.attendees = !string.IsNullOrEmpty(s.goers) ? Convert.ToInt32(s.goers) : 0;
                    se.created = DateTime.Now;
                    se.description = s.description;
                    se.event_id = event_id;
                    se.event_key = s.event_key;
                    se.modified = DateTime.Now;
                    se.seats = !string.IsNullOrEmpty(s.seats) ? Convert.ToInt32(s.seats) : 0;

                    DateTime session_end;

                    se.session_end = DateTime.TryParse(s.event_end, out session_end) ? (DateTime?) session_end : null;

                    if (se.session_end != null)
                    {
                        se.session_end = Convert.ToDateTime(se.session_end) < DateTime.Now ? null : se.session_end;
                    }

                    DateTime session_start;

                    se.session_start = DateTime.TryParse(s.event_start, out session_start) ? (DateTime?) session_start : null;

                    if (se.session_start != null)
                    {
                        se.session_start = Convert.ToDateTime(se.session_start) < DateTime.Now ? null : se.session_start;
                    }

                    se.speakers = s.speakers;
                    se.title = s.name;
                    se.type = !string.IsNullOrEmpty(s.event_type) ? s.event_type.Replace("[", "").Replace("]", "") : string.Empty;
                    se.venue = s.venue;
                    se.venue_id = s.venue_id;

                    db.SubmitChanges();
                }
            }
            else
            {
                // this is an add procedure
                session se = new session();

                se.active = s.active == "Y";
                se.attendees = !string.IsNullOrEmpty(s.goers) ? Convert.ToInt32(s.goers) : 0;
                se.created = DateTime.Now;
                se.description = s.description;
                se.event_id = event_id;
                se.event_key = s.event_key;
                se.modified = DateTime.Now;
                se.seats = !string.IsNullOrEmpty(s.seats) ? Convert.ToInt32(s.seats) : 0;

                DateTime session_end;

                se.session_end = DateTime.TryParse(s.event_end, out session_end) ? (DateTime?)session_end : null;

                DateTime session_start;

                se.session_start = DateTime.TryParse(s.event_start, out session_start) ? (DateTime?)session_start : null;

                se.speakers = s.speakers;
                se.title = s.name;

                se.type = !string.IsNullOrEmpty(s.event_type) ? s.event_type.Replace("[", "").Replace("]", "") : string.Empty;

                
                se.venue = s.venue;
                se.venue_id = s.venue_id;

                db.sessions.InsertOnSubmit(se);

                db.SubmitChanges();

                s.internal_id = se.id;
            }


            Location l = new Location();

            l.event_id = event_id;
            l.sched_id = s.venue_id;
            l.title = s.venue;

            l = _locations.add(l);
            
            return s;
        }

        public Session current(Int32 event_id, string location)
        {
            Session s = new Session();

            foreach (var item in db.session_get_current(event_id, location))
            {
                s.description = item.description;
                s.end = Convert.ToDateTime(item.session_end);
                s.event_type = item.type;
                s.goers = item.attendees.ToString();
                s.internal_id = item.id;
                s.name = item.title;
                s.speakers = item.speakers;
                s.start = Convert.ToDateTime(item.session_start);
                s.venue = item.venue;
                s.event_id = Convert.ToInt32(item.event_id);
            }

            return s;
        }

        public Session next(Int32 event_id, string location, DateTime prev_end)
        {
            Session s = new Session();

            foreach (var item in db.session_get_next(event_id, location, prev_end))
            {
                s.description = item.description;
                s.end = Convert.ToDateTime(item.session_end);
                s.event_type = item.type;
                s.goers = item.attendees.ToString();
                s.internal_id = item.id;
                s.name = item.title;
                s.speakers = item.speakers;
                s.start = Convert.ToDateTime(item.session_start);
                s.event_start = Convert.ToDateTime(s.start).ToShortTimeString();
                s.venue = item.venue;
            }

            if (s.internal_id == 0)
            {
                s.event_start = "No Session";
                s.name = "There is no session upcoming in this location";
            }

            return s;
        }
    }

    public class Session
    {
        public string event_key { get; set; }
        public string active { get; set; }
        public string name { get; set; }
        public string event_start { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public string event_end { get; set; }
        public string event_type { get; set; }
        public string description { get; set; }
        public string seats { get; set; }
        public string goers { get; set; }
        public string invite_only { get; set; }
        public string venue { get; set; }
        public string id { get; set; }
        public string venue_id { get; set; }
        public string speakers { get; set; }
        public Int32 internal_id { get; set; }
        public Int32 event_id { get; set; }
    }

    #region openstackAPI

    public class osessions
    {
        private functions _functions = new functions();
        private locations _locations = new locations();
        private speakers _speakers = new speakers();

        public OpenStack refresh(Int32 id, string page)
        {
            // https://testresource-server.openstack.org/api/v1/summits/6/events&per_page=100&page=1

            var client = new RestClient("https://testresource-server.openstack.org/api/v1/");

            var request = new RestRequest("summits/" + id + "/events");

            request.AddParameter("access_token", "PAH7KdDOiWgZVWuQqYGpr3LCPHv-fj8RsO%7EeozlVBMeEDd8xezJHMx.4VH64T0MFTVV3k2KN");
            request.AddParameter("token_type", "Bearer");
            request.AddParameter("per_page", "100");
            request.AddParameter("page", page);

            IRestResponse response = client.Execute(request);

            var mySessions = new JavaScriptSerializer().Deserialize<OpenStack>(response.Content);

            return mySessions;
        }

        public Session parse(OpenStackSession s, Int32 event_id)
        {
            Session se = new Session();

            se.description = s.description;
            se.end = s.end_date != null ? (DateTime) Convert.ToDateTime(_functions.ConvertUnixTimeStamp(s.end_date.ToString())) : DateTime.Now.AddYears(-50);

            se.event_id = event_id;
            se.event_key = s.id.ToString();
            se.event_type = "OpenStack";
            se.internal_id = s.id;
            se.name = s.title;
            se.start = s.start_date != null ? (DateTime) Convert.ToDateTime(_functions.ConvertUnixTimeStamp(s.start_date.ToString())) : DateTime.Now.AddYears(-50);
            se.venue_id = s.location_id != null ? s.location_id.ToString() : null;
            se.venue = s.location_id != null ? _locations.name_by_reference(s.location_id.ToString(), event_id) : "NOT SET";

            List<string> speakers = new List<string>();

            foreach (int sp in s.speakers)
            {
                speakers.Add(_speakers.name_by_open_id(sp, event_id));
            }

            foreach (string speak in speakers)
            {
                if (speak != "Unknown")
                {
                    se.speakers += speak + " ";
                }
            }

            return se;
        }
    }

    public class OpenStack
    {
        public Int32? total { get; set; }
        public Int32 per_page { get; set; }
        public Int32 current_page { get; set; }
        public Int32 last_page { get; set; }
        public List<OpenStackSession> data { get; set; }
    }

    public class OpenStackSession
    {
        public Int32 id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public Int32? start_date { get; set; }
        public Int32? end_date { get; set; }
        public Int32? location_id { get; set; }
        public Int32 summit_id { get; set; }
        //public Int32 type_id { get; set; }
        public string class_name { get; set; }
        //public Int32? track_id { get; set; }
        //public Int32? moderator_speaker_id { get; set; }
        //public string level { get; set; }
        //public Boolean allow_feedback { get; set; }
        //public Int32? avg_feedback_rate { get; set; }
        public Boolean is_published { get; set; }
        //public Int32? head_count { get; set; }
        //public string rsvp_link { get; set; }
        //public Int32[] summit_types { get; set; }
        //public string[] tags { get; set; }
        public Int32[] speakers { get; set; }
    }

    #endregion
}
