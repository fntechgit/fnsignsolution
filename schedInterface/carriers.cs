using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Linq.Expressions;

namespace schedInterface
{
    public class carriers
    {
        private schedDataContext db = new schedDataContext();

        public Carrier create(Carrier c)
        {
            carrier entity = new carrier();
            bool flag = false;
            if (c.id > 0)
            {
                flag = true;
                entity = this.db.carriers.Single<carrier>((Expression<Func<carrier, bool>>)(x => x.id == c.id));
            }
            entity.mms = c.mms;
            entity.sms = c.title;
            entity.title = c.title;
            if (!flag)
                this.db.carriers.InsertOnSubmit(entity);
            this.db.SubmitChanges();
            c.id = entity.id;
            return c;
        }

        public Carrier single(int id)
        {
            Carrier carrier1 = new Carrier();
            Table<carrier> carriers = this.db.carriers;
            Expression<Func<carrier, bool>> predicate = (Expression<Func<carrier, bool>>)(crs => crs.id == id);
            foreach (carrier carrier2 in (IEnumerable<carrier>)carriers.Where<carrier>(predicate))
            {
                carrier1.id = carrier2.id;
                carrier1.mms = carrier2.mms;
                carrier1.sms = carrier2.sms;
                carrier1.title = carrier2.title;
            }
            return carrier1;
        }

        public List<Carrier> all()
        {
            List<Carrier> carrierList = new List<Carrier>();
            Table<carrier> carriers = this.db.carriers;
            Expression<Func<carrier, string>> keySelector = (Expression<Func<carrier, string>>)(crs => crs.title);
            foreach (carrier carrier in (IEnumerable<carrier>)carriers.OrderBy<carrier, string>(keySelector))
                carrierList.Add(new Carrier()
                {
                    id = carrier.id,
                    mms = carrier.mms,
                    sms = carrier.sms,
                    title = carrier.title
                });
            return carrierList;
        }

        public bool remove(int id)
        {
            this.db.carriers.DeleteAllOnSubmit<carrier>((IEnumerable<carrier>)this.db.carriers.Where<carrier>((Expression<Func<carrier, bool>>)(crs => crs.id == id)));
            this.db.SubmitChanges();
            return true;
        }
    }

    public class Carrier
    {
        public int id { get; set; }

        public string title { get; set; }

        public string sms { get; set; }

        public string mms { get; set; }
    }
}
