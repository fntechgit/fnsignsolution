using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

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
    }

    public class Location
    {
        public Int32 id { get; set; }
        public string sched_id { get; set; }
        public string title { get; set; }
        public Int32 event_id { get; set; }
    }
}
