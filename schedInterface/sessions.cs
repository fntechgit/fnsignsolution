using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;
using System.Web.Script.Serialization;

namespace schedInterface
{
    public class sessions
    {
        private schedDataContext db = new schedDataContext();
        private locations _locations = new locations();

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

        public Session add(Session s, Int32 event_id)
        {
            // check to see if one exists;
            var result = from sess in db.sessions
                where sess.event_key == s.event_key && sess.event_id==event_id
                select sess;

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
                    se.session_end = Convert.ToDateTime(s.event_end);
                    se.session_start = Convert.ToDateTime(s.event_start);
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
                se.session_end = Convert.ToDateTime(s.event_end);
                se.session_start = Convert.ToDateTime(s.event_start);
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
    }

    public class Session
    {
        public string event_key { get; set; }
        public string active { get; set; }
        public string name { get; set; }
        public string event_start { get; set; }
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
    }
}
