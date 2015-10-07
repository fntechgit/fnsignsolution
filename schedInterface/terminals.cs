using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace schedInterface
{
    public class terminals
    {
        private schedDataContext db = new schedDataContext();

        public List<Terminal> by_event(Int32 id)
        {
            List<Terminal> _terminals = new List<Terminal>();

            var result = from trms in db.terminals
                where trms.event_id == id
                orderby trms.title
                select trms;

            foreach (var item in result)
            {
                Terminal t = new Terminal();

                t.event_id = item.event_id;
                t.id = item.id;
                t.location_id = item.location_id;
                t.online = item.online;
                t.template_id = item.template_id;
                t.title = item.title;

                _terminals.Add(t);
            }

            return _terminals;
        }

        public Terminal add(Terminal t)
        {
            terminal te = new terminal();

            te.event_id = t.event_id;
            te.location_id = t.location_id;
            te.online = true;
            te.template_id = t.template_id;
            te.title = t.title;
            
            db.terminals.InsertOnSubmit(te);

            db.SubmitChanges();

            t.id = te.id;

            return t;
        }

        public Terminal update(Terminal t)
        {
            terminal te = db.terminals.Single(x => x.id == t.id);

            te.event_id = t.event_id;
            te.location_id = t.location_id;
            te.online = t.online;
            te.template_id = t.template_id;
            te.title = t.title;

            db.SubmitChanges();

            return t;
        }
    }

    public class Terminal
    {
        public Int32 id { get; set; }
        public string title { get; set; }
        public Int32? location_id { get; set; }
        public Int32? template_id { get; set; }
        public Boolean online { get; set; }
        public Int32 event_id { get; set; }
    }
}
