using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Text.RegularExpressions;
using RestSharp;
using System.Web.Script.Serialization;
using System.Linq.Expressions;

namespace schedInterface
{
    public class sessions
    {
        private schedDataContext db = new schedDataContext();
        private locations _locations = new locations();

        public bool clear_by_event(int id)
        {
            this.db.sessions_delete_by_event(new int?(id));
            return true;
        }

        public bool toggle_full(int id)
        {
            session session = this.db.sessions.Single<session>((Expression<Func<session, bool>>)(x => x.id == id));
            session.full = !session.full;
            this.db.SubmitChanges();
            return session.full;
        }

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
                s.speaker_companies = item.speaker_companies;
                s.speaker_images = item.speaker_images;
                s.full = item.full;
                
                _sessions.Add(s);
            }

            return _sessions;
        }

        public List<Session> future_by_event_by_location_by_day(int event_id, string location, DateTime start)
        {
            return this.by_event_by_location_by_day(event_id, location, start).ToList<Session>();
        }

        public List<Session> skip_by_event_by_location_by_day(int event_id, string location, int skip, DateTime start)
        {
            return this.by_event_by_location_by_day(event_id, location, start).Skip<Session>(skip).ToList<Session>();
        }

        public List<Session> by_event_by_day(int id, DateTime startdate)
        {
            return this.by_event(id).Where<Session>((Func<Session, bool>)(x => x.start.Date == startdate)).ToList<Session>();
        }

        public List<Session> get_future_by_event_by_day(int id, DateTime startdate)
        {
            return this.by_event_by_day(id, startdate.Date).Where<Session>((Func<Session, bool>)(x => x.start >= startdate)).ToList<Session>();
        }

        public List<Session> future_by_event_by_day(int event_id, DateTime start)
        {
            List<Session> sessionList = new List<Session>();
            foreach (future_sessions_by_dayResult sessionsByDayResult in (IEnumerable<future_sessions_by_dayResult>)this.db.future_sessions_by_day(new int?(event_id), new DateTime?(start)))
                sessionList.Add(new Session()
                {
                    event_start = Convert.ToDateTime((object)sessionsByDayResult.session_start).ToShortTimeString(),
                    name = sessionsByDayResult.title,
                    venue = sessionsByDayResult.venue
                });
            return sessionList;
        }

        public List<Session> by_event_by_day_by_group(int id, DateTime start)
        {
            List<Session> sessionList = new List<Session>();
            foreach (sessions_by_event_by_day_by_groupResult dayByGroupResult in (IEnumerable<sessions_by_event_by_day_by_groupResult>)this.db.sessions_by_event_by_day_by_group(new DateTime?(start)))
            {
                Session session1 = new Session();
                session1.end = Convert.ToDateTime((object)dayByGroupResult.session_end);
                Session session2 = session1;
                DateTime dateTime = session1.end;
                string shortTimeString1 = dateTime.ToShortTimeString();
                session2.event_end = shortTimeString1;
                session1.start = Convert.ToDateTime((object)dayByGroupResult.session_start);
                Session session3 = session1;
                dateTime = session1.start;
                string shortTimeString2 = dateTime.ToShortTimeString();
                session3.event_start = shortTimeString2;
                session1.name = dayByGroupResult.title;
                session1.venue = dayByGroupResult.venue;
                session1.venue_id = dayByGroupResult.venue_id;
                session1.event_id = Convert.ToInt32((object)dayByGroupResult.event_id);
                session1.full = dayByGroupResult.full;
                sessionList.Add(session1);
            }
            return sessionList;
        }

        public List<Session> by_event_by_location_by_day(int event_id, string location, DateTime start)
        {
            sublocations sublocations = new sublocations();
            List<Session> sessionList = new List<Session>();
            int id = this._locations.location_id_by_sched_id(location, event_id);
            if (sublocations.children(id).Count > 0)
            {
                foreach (sessions_by_event_location_children_dateResult childrenDateResult in (IEnumerable<sessions_by_event_location_children_dateResult>)this.db.sessions_by_event_location_children_date(new int?(event_id), new int?(id), new DateTime?(start)))
                {
                    Session session1 = new Session();
                    session1.active = childrenDateResult.active.ToString();
                    session1.description = childrenDateResult.description;
                    session1.end = Convert.ToDateTime((object)childrenDateResult.session_end);
                    session1.event_id = event_id;
                    session1.event_type = childrenDateResult.type;
                    Session session2 = session1;
                    int num = childrenDateResult.attendees;
                    string str1 = num.ToString();
                    session2.goers = str1;
                    Session session3 = session1;
                    num = childrenDateResult.id;
                    string str2 = num.ToString();
                    session3.id = str2;
                    session1.event_key = childrenDateResult.event_key;
                    session1.internal_id = childrenDateResult.id;
                    session1.name = childrenDateResult.title;
                    session1.speakers = childrenDateResult.speakers;
                    session1.start = Convert.ToDateTime((object)childrenDateResult.session_start);
                    session1.event_start = Convert.ToDateTime(session1.start).ToShortTimeString();
                    session1.event_end = Convert.ToDateTime(session1.end).ToShortTimeString();
                    session1.venue = childrenDateResult.venue;
                    session1.speaker_companies = childrenDateResult.speaker_companies;
                    session1.speaker_images = childrenDateResult.speaker_images;
                    session1.speakers = childrenDateResult.speakers;
                    session1.full = childrenDateResult.full;
                    sessionList.Add(session1);
                }
            }
            else
            {
                foreach (sessions_by_event_location_dateResult locationDateResult in (IEnumerable<sessions_by_event_location_dateResult>)this.db.sessions_by_event_location_date(new int?(event_id), location, new DateTime?(start)))
                {
                    Session session1 = new Session();
                    session1.active = locationDateResult.active.ToString();
                    session1.description = locationDateResult.description;
                    session1.end = Convert.ToDateTime((object)locationDateResult.session_end);
                    session1.event_id = event_id;
                    session1.event_type = locationDateResult.type;
                    Session session2 = session1;
                    int num = locationDateResult.attendees;
                    string str1 = num.ToString();
                    session2.goers = str1;
                    Session session3 = session1;
                    num = locationDateResult.id;
                    string str2 = num.ToString();
                    session3.id = str2;
                    session1.event_key = locationDateResult.event_key;
                    session1.internal_id = locationDateResult.id;
                    session1.name = locationDateResult.title;
                    session1.speakers = locationDateResult.speakers;
                    session1.start = Convert.ToDateTime((object)locationDateResult.session_start);
                    session1.event_start = Convert.ToDateTime(session1.start).ToShortTimeString();
                    session1.event_end = Convert.ToDateTime(session1.end).ToShortTimeString();
                    session1.venue = locationDateResult.venue;
                    session1.speaker_companies = locationDateResult.speaker_companies;
                    session1.speaker_images = locationDateResult.speaker_images;
                    session1.speakers = locationDateResult.speakers;
                    session1.full = locationDateResult.full;
                    sessionList.Add(session1);
                }
            }
            return sessionList;
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
                s.speaker_companies = item.speaker_companies;
                s.speaker_images = item.speaker_images;
                s.full = item.full;

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
                s.speaker_companies = item.speaker_companies;
                s.speaker_images = item.speaker_images;
                s.full = item.full;

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

        public List<Session> barcelona_design_summit()
        {
            List<Session> sessionList = new List<Session>();
            foreach (barcelona_design_summitResult designSummitResult in (IEnumerable<barcelona_design_summitResult>)this.db.barcelona_design_summit())
            {
                Session session1 = new Session();
                session1.description = designSummitResult.description;
                session1.end = Convert.ToDateTime((object)designSummitResult.session_end);
                session1.event_type = designSummitResult.type;
                Session session2 = session1;
                int num = designSummitResult.attendees;
                string str1 = num.ToString();
                session2.goers = str1;
                Session session3 = session1;
                num = designSummitResult.id;
                string str2 = num.ToString();
                session3.id = str2;
                session1.internal_id = designSummitResult.id;
                session1.name = designSummitResult.title;
                session1.speakers = designSummitResult.speakers;
                session1.start = Convert.ToDateTime((object)designSummitResult.session_start);
                session1.venue = designSummitResult.venue;
                session1.event_start = session1.start.ToShortTimeString();
                sessionList.Add(session1);
            }
            return sessionList;
        }

        public List<Session> barcelona_working_group()
        {
            List<Session> sessionList = new List<Session>();
            foreach (barcelona_working_groupResult workingGroupResult in (IEnumerable<barcelona_working_groupResult>)this.db.barcelona_working_group())
            {
                Session session1 = new Session();
                session1.description = workingGroupResult.description;
                session1.end = Convert.ToDateTime((object)workingGroupResult.session_end);
                session1.event_type = workingGroupResult.type;
                Session session2 = session1;
                int num = workingGroupResult.attendees;
                string str1 = num.ToString();
                session2.goers = str1;
                Session session3 = session1;
                num = workingGroupResult.id;
                string str2 = num.ToString();
                session3.id = str2;
                session1.internal_id = workingGroupResult.id;
                session1.name = workingGroupResult.title;
                session1.speakers = workingGroupResult.speakers;
                session1.start = Convert.ToDateTime((object)workingGroupResult.session_start);
                session1.venue = workingGroupResult.venue;
                session1.event_start = session1.start.ToShortTimeString();
                sessionList.Add(session1);
            }
            return sessionList;
        }

        public List<Session> summit()
        {
            List<Session> _sessions = new List<Session>();

            //var result = from sess in db.sessions
            //             where sess.event_id == 1001 && Convert.ToDateTime(sess.session_start).Date == DateTime.Now.Date
            //             orderby sess.session_start, sess.title
            //             select sess;

            foreach (var item in db.design_summit_austin())
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
                s.speaker_companies = item.speaker_companies;
                s.speaker_images = item.speaker_images;
                s.full = item.full;

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

        public Session openstackAdd(Session s, Int32 event_id)
        {
            Regex rgx = new Regex(@"[0-9 :-]");

            db.session_mod(s.event_key, true, s.name, s.start, s.end, s.event_type, s.description,
                !string.IsNullOrEmpty(s.seats) ? Convert.ToInt32(s.seats) : 0,
                !string.IsNullOrEmpty(s.goers) ? Convert.ToInt32(s.goers) : 0, Convert.ToInt32(s.venue_id), s.venue, s.speakers,
                s.event_id);

            return s;
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

                    if (s.end > DateTime.Now.AddYears(-100))
                    {
                        se.session_end = s.end;
                    }
                    else
                    {

                        se.session_end = DateTime.TryParse(s.event_end, out session_end)
                            ? (DateTime?) session_end
                            : null;

                        if (se.session_end != null)
                        {
                            se.session_end = Convert.ToDateTime(se.session_end) < DateTime.Now ? null : se.session_end;
                        }

                    }

                    DateTime session_start;

                    if (s.start > DateTime.Now.AddYears(-100))
                    {
                        se.session_start = s.start;
                    }
                    else
                    {
                        se.session_start = DateTime.TryParse(s.event_start, out session_start)
                            ? (DateTime?) session_start
                            : null;

                        if (se.session_start != null)
                        {
                            se.session_start = Convert.ToDateTime(se.session_start) < DateTime.Now
                                ? null
                                : se.session_start;
                        }
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

                if (s.end > DateTime.Now.AddYears(-100))
                {
                    se.session_end = s.end;
                }
                else
                {

                    se.session_end = DateTime.TryParse(s.event_end, out session_end) ? (DateTime?) session_end : null;
                }

                DateTime session_start;

                if (s.start > DateTime.Now.AddYears(-100))
                {
                    se.session_start = s.start;
                }
                else
                {
                    se.session_start = DateTime.TryParse(s.event_start, out session_start)
                        ? (DateTime?) session_start
                        : null;
                }

                se.speakers = s.speakers;
                se.title = s.name;

                se.type = !string.IsNullOrEmpty(s.event_type) ? s.event_type.Replace("[", "").Replace("]", "") : string.Empty;

                
                se.venue = s.venue;
                se.venue_id = s.venue_id;
                se.speaker_companies = s.speaker_companies;
                se.speaker_images = s.speaker_images;
                se.full = s.full;

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

        public Session single(int id)
        {
            Session session1 = new Session();
            Table<session> sessions = this.db.sessions;
            Expression<Func<session, bool>> predicate = (Expression<Func<session, bool>>)(sess => sess.id == id);
            foreach (session session2 in (IEnumerable<session>)sessions.Where<session>(predicate))
            {
                session1.internal_id = session2.id;
                session1.description = session2.description;
                if (session2.session_end.HasValue)
                    session1.end = Convert.ToDateTime((object)session2.session_end);
                session1.event_id = Convert.ToInt32(session1.event_id);
                session1.event_key = session2.event_key;
                session1.event_type = session2.type;
                session1.name = session2.title;
                session1.speakers = session2.speakers;
                session1.speaker_images = session2.speaker_images;
                session1.speaker_companies = session2.speaker_companies;
                session1.full = session2.full;
                if (session2.session_start.HasValue)
                    session1.start = Convert.ToDateTime((object)session2.session_start);
                session1.venue = session2.venue;
                session1.venue_id = session2.venue_id;
            }
            return session1;
        }

        public Session current(int event_id, string location, DateTime start)
        {
            Session session = new Session();
            foreach (session_get_currentResult getCurrentResult in (IEnumerable<session_get_currentResult>)this.db.session_get_current(new int?(event_id), location, new DateTime?(start)))
            {
                session.description = getCurrentResult.description;
                session.end = Convert.ToDateTime((object)getCurrentResult.session_end);
                session.event_type = getCurrentResult.type;
                session.goers = getCurrentResult.attendees.ToString();
                session.internal_id = getCurrentResult.id;
                session.name = getCurrentResult.title;
                session.speakers = getCurrentResult.speakers;
                session.start = Convert.ToDateTime((object)getCurrentResult.session_start);
                session.venue = getCurrentResult.venue;
                session.event_id = Convert.ToInt32((object)getCurrentResult.event_id);
                session.speaker_images = getCurrentResult.speaker_images;
                session.speaker_companies = getCurrentResult.speaker_companies;
                session.full = getCurrentResult.full;
            }
            return session;
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
                s.speaker_companies = item.speaker_companies;
                s.speaker_images = item.speaker_images;
                s.full = item.full;
            }

            if (s.internal_id == 0)
            {
                s.event_start = "No Session";
                s.name = "There is no session upcoming in this location";
            }

            return s;
        }

        public Boolean remove(string id)
        {
            db.sessions_remove(id);

            return true;
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

        public string speaker_companies { get; set; }
        public string speaker_images { get; set; }
        public bool full { get; set; }
    }

    #region openstackAPI

    public class osessions
    {
        private functions _functions = new functions();
        private locations _locations = new locations();
        private speakers _speakers = new speakers();
        private osettings _settings = new osettings();

        public OpenStack refresh(Int32 id, string page)
        {
            // https://testresource-server.openstack.org/api/v1/summits/6/events&per_page=100&page=1

            var client = new RestClient("https://openstackid-resources.openstack.org/api/v1/");

            var request = new RestRequest("summits/" + id + "/events/published");

            request.AddParameter("access_token",_settings.auth_key());
            request.AddParameter("token_type", "Bearer");
            request.AddParameter("per_page", "100");
            request.AddParameter("page", page);

            IRestResponse response = client.Execute(request);

            var mySessions = new JavaScriptSerializer().Deserialize<OpenStack>(response.Content);

            return mySessions;
        }

        public Session parse(OpenStackSession s, Event e, List<EventType> types)
        {
            Session se = new Session();

            se.description = s.description;
            se.end = s.end_date != null ? (DateTime) Convert.ToDateTime(_functions.ConvertUnixTimeStamp(s.end_date.ToString())).AddSeconds(e.offset) : DateTime.Now.AddYears(-50);

            se.event_id = e.id;
            se.event_key = s.id.ToString();
            se.event_type = types.Single(x => x.event_type_id == s.type_id).title;
            se.internal_id = s.id;
            se.name = s.title;
            se.start = s.start_date != null ? (DateTime) Convert.ToDateTime(_functions.ConvertUnixTimeStamp(s.start_date.ToString())).AddSeconds(e.offset) : DateTime.Now.AddYears(-50);
            se.venue_id = s.location_id != null ? s.location_id.ToString() : null;
            se.venue = s.location_id != null ? _locations.name_by_reference(s.location_id.ToString(), e.id) : "NOT SET";

            List<string> speakers = new List<string>();

            try
            {
                if (s.speakers.Any())
                {
                    foreach (int sp in s.speakers)
                    {
                        speakers.Add(_speakers.name_by_open_id(sp, e.id));
                    }

                    se.speakers = string.Join(", ", speakers);
                }
            }
            catch (ArgumentNullException ex)
            {
                se.speakers = "NOT SET";
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
        public List<Int32> speakers { get; set; }
        public Int32 type_id { get; set; }
    }

    #endregion
}
