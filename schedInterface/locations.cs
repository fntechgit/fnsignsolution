using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Script.Serialization;
using RestSharp;
using System.Linq.Expressions;

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

        public bool delete(int id)
        {
            this.db.locations.DeleteAllOnSubmit<location>((IEnumerable<location>)this.db.locations.Where<location>((Expression<Func<location, bool>>)(lcs => lcs.id == id)));
            this.db.SubmitChanges();
            return true;
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

        public int location_id_by_sched_id(string sched, int event_id)
        {
            IQueryable<location> queryable = this.db.locations.Where<location>((Expression<Func<location, bool>>)(lc => lc.sched_id == sched && lc.event_id == (int?)event_id));
            int num = 0;
            foreach (location location in (IEnumerable<location>)queryable)
                num = location.id;
            return num;
        }

        public string find_venue_reference_id(string title, int event_id)
        {
            IQueryable<location> source = this.db.locations.Where<location>((Expression<Func<location, bool>>)(lcs => lcs.event_id == (int?)event_id && lcs.title == title));
            string str = "";
            if (source.Any<location>())
            {
                foreach (location location in (IEnumerable<location>)source)
                    str = location.sched_id;
            }
            else
            {
                str = "IMPORT_" + Guid.NewGuid().ToString();
                new locations().add(new Location()
                {
                    event_id = event_id,
                    sched_id = str,
                    title = title
                });
            }
            return str;
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
        private osettings _settings = new osettings();

        public List<OpenStackLocation> get_by_event(Int32 id)
        {
            var client = new RestClient("https://openstackid-resources.openstack.org/api/v1/");

            var request = new RestRequest("summits/" + id + "/locations");

            request.AddParameter("access_token", _settings.auth_key());
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
