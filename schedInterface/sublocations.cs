using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Linq.Expressions;

namespace schedInterface
{
    public class sublocations
    {
        private schedDataContext db = new schedDataContext();

        public bool delete(int id)
        {
            this.db.sublocations.DeleteOnSubmit(this.db.sublocations.Single<sublocation>((Expression<Func<sublocation, bool>>)(x => x.id == id)));
            this.db.SubmitChanges();
            return true;
        }

        public SubLocation add(SubLocation s)
        {
            sublocation entity = new sublocation();
            entity.child = s.child;
            entity.parent = s.parent;
            this.db.sublocations.InsertOnSubmit(entity);
            this.db.SubmitChanges();
            s.id = entity.id;
            return s;
        }

        public List<SubLocation> children(int id)
        {
            List<SubLocation> subLocationList = new List<SubLocation>();
            Table<sublocation> sublocations = this.db.sublocations;
            Expression<Func<sublocation, bool>> predicate = (Expression<Func<sublocation, bool>>)(lcs => lcs.parent == id);
            foreach (sublocation sublocation in (IEnumerable<sublocation>)sublocations.Where<sublocation>(predicate))
                subLocationList.Add(new SubLocation()
                {
                    child = sublocation.child,
                    parent = id,
                    id = sublocation.id
                });
            return subLocationList;
        }

        public List<SubLocation> children_display(int id)
        {
            List<SubLocation> subLocationList = new List<SubLocation>();
            foreach (sublocations_by_locationResult byLocationResult in (IEnumerable<sublocations_by_locationResult>)this.db.sublocations_by_location(new int?(id)))
                subLocationList.Add(new SubLocation()
                {
                    child_title = byLocationResult.title,
                    parent = id,
                    id = Convert.ToInt32((object)byLocationResult.id)
                });
            return subLocationList;
        }
    }

    public class SubLocation
    {
        public int id { get; set; }

        public int parent { get; set; }

        public int child { get; set; }

        public string parent_title { get; set; }

        public string child_title { get; set; }
    }
}
