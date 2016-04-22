using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web.Script.Serialization;
using RestSharp;

namespace schedInterface
{
    public class speakers
    {
        private schedDataContext db = new schedDataContext();
        private osettings _settings = new osettings();

        public OpenSpeaker refresh(Int32 id, string page)
        {
            var client = new RestClient("https://openstackid-resources.openstack.org/api/v1/");

            var request = new RestRequest("summits/" + id + "/speakers");

            request.AddParameter("access_token", _settings.auth_key());
            request.AddParameter("token_type", "Bearer");
            request.AddParameter("per_page", "100");
            request.AddParameter("page", page);

            IRestResponse response = client.Execute(request);

            var mySessions = new JavaScriptSerializer().Deserialize<OpenSpeaker>(response.Content);

            return mySessions;
        }

        public Speaker add(Speaker s)
        {
            db.speaker_mod(s.first_name, s.last_name, s.title, s.bio, s.irc, s.twitter, s.member_id, s.pic,
                s.id, s.event_id);

            s.openstack_id = s.id;

            //speaker sp = new speaker();

            //if (db.speakers.Where(x => x.openstack_id == s.openstack_id).Any())
            //{
            //    // need to update
            //    sp = db.speakers.Where(x => x.openstack_id == s.openstack_id && x.event_id == s.event_id).Single();

            //    sp.bio = s.bio;
            //    sp.event_id = s.event_id;
            //    sp.first_name = s.first_name;
            //    sp.irc = s.irc;
            //    sp.last_name = s.last_name;
            //    sp.member_id = s.member_id;
            //    sp.openstack_id = s.id;
            //    s.openstack_id = s.id;
            //    sp.pic = s.pic;
            //    sp.title = s.title;
            //    sp.twitter = s.twitter;
            //    sp.event_id = s.event_id;

            //    db.SubmitChanges();
            //}
            //else
            //{
            //    sp.bio = s.bio;
            //    sp.event_id = s.event_id;
            //    sp.first_name = s.first_name;
            //    sp.irc = s.irc;
            //    sp.last_name = s.last_name;
            //    sp.member_id = s.member_id;
            //    sp.openstack_id = s.id;
            //    s.openstack_id = s.id;
            //    sp.pic = s.pic;
            //    sp.title = s.title;
            //    sp.twitter = s.twitter;
            //    sp.event_id = s.event_id;

            //    db.speakers.InsertOnSubmit(sp);

            //    db.SubmitChanges();

            //    s.id = sp.id;
            //}

            return s;
        }

        public string name_by_open_id(Int32 id, Int32 event_id)
        {
            string name = "Unknown";

            var result = from ops in db.speakers
                where ops.openstack_id == id && ops.event_id == event_id
                select ops;

            foreach (var item in result)
            {
                name = item.first_name + " " + item.last_name;
            }

            return name;
        }
    }

    public class OpenSpeaker
    {
        public Int32 total { get; set; }
        public Int32 per_page { get; set; }
        public Int32 current_page { get; set; }
        public Int32 last_page { get; set; }
        public List<Speaker> data { get; set; }
    }

    public class Speaker
    {
        public Int32 id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string title { get; set; }
        public string bio { get; set; }
        public string irc { get; set; }
        public string twitter { get; set; }
        public Int32? member_id { get; set; }
        public string pic { get; set; }
        public Int32? openstack_id { get; set; }
        public Int32 event_id { get; set; }
    }
}
