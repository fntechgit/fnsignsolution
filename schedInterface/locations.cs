using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Script.Serialization;
using RestSharp;

namespace schedInterface
{
    public class locations
    {
        private schedDataContext db = new schedDataContext();

        public Location add(Location l)
        {
            // test to see if it already exists
            var result = from lcls in db.locations
                where lcls.sched_id==l.sched_id && lcls.event_id==l.event_id
                select lcls;

            if (result.Any())
            {
                // this is an update
                foreach (var lo in result)
                {
                    lo.event_id = l.event_id;
                    lo.title = l.title;
                    lo.sched_id = l.sched_id;

                    db.SubmitChanges();
                }
            }
            else
            {
                // this is an add
                location lo = new location();

                lo.event_id = l.event_id;
                lo.sched_id = l.sched_id;
                lo.title = l.title;

                db.locations.InsertOnSubmit(lo);

                db.SubmitChanges();

                l.id = lo.id;
            }

            return l;
        }

        public Location single(Int32 id)
        {
            Location l = new Location();

            location lo = db.locations.Single(x => x.id == id);

            l.id = id;
            l.event_id = Convert.ToInt32(lo.event_id);
            l.sched_id = lo.sched_id;
            l.title = lo.title;

            return l;
        }

        public List<Location> by_event(Int32 id)
        {
            List<Location> _locations = new List<Location>();

            var result = from lc in db.locations
                where lc.event_id == id
                orderby lc.title
                select lc;

            foreach (var item in result)
            {
                Location l = new Location();

                l.id = item.id;
                l.title = item.title;
                l.sched_id = item.sched_id;

                _locations.Add(l);
            }

            return _locations;
        }

        public string name_by_reference(string id, Int32 event_id)
        {
            var result = from lc in db.locations
                where lc.sched_id == id && lc.event_id==event_id
                select lc;

            string name = "";

            foreach (var item in result)
            {
                name = item.title;
            }

            return name;
        }


    }

    public class Location
    {
        public Int32 id { get; set; }
        public string sched_id { get; set; }
        public string title { get; set; }
        public Int32 event_id { get; set; }
    }

    #region openstackAPI

    public class olocations
    {
        public List<OpenStackLocation> get_by_event(Int32 id)
        {
            var client = new RestClient("https://testresource-server.openstack.org/api/v1/");

            var request = new RestRequest("summits/" + id + "/locations");

            request.AddParameter("access_token", "PAH7KdDOiWgZVWuQqYGpr3LCPHv-fj8RsO%7EeozlVBMeEDd8xezJHMx.4VH64T0MFTVV3k2KN");
            request.AddParameter("token_type", "Bearer");

            IRestResponse response = client.Execute(request);

            var mySessions = new JavaScriptSerializer().Deserialize<List<OpenStackLocation>>(response.Content);

            return mySessions;
        }
    }

    public class OpenStackLocation
    {
        public Int32 id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string class_name { get; set; }
        public string location_type { get; set; }
        public string address_1 { get; set; }
        public string address_2 { get; set; }
        public string zip_code { get; set; }
        public string city { get; set; }
        public string steate { get; set; }
        public string country { get; set; }
        public string lng { get; set; }
        public string lat { get; set; }
    }

    #endregion
}
