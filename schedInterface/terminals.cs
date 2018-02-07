using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

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
                t.rotate = item.rotate;
                t.deck = item.deck;
                t.group_by_location = item.group_by_location;
                t.group_by_start = item.group_by_start;
                t.template_id_announce = item.template_id_announce;
                t.template_id_end = item.template_id_end;
                t.notified = item.notified;
                t.notified_date = item.notified_date;

                _terminals.Add(t);
            }

            return _terminals;
        }

        public List<Terminal> all_by_event(Int32 id)
        {
            List<Terminal> _terminals = new List<Terminal>();

            foreach (var item in db.terminal_assignments_by_event(id))
            {
                Terminal t = new Terminal();

                t.event_id = id;
                t.id = item.id;
                t.location_id = item.location_id;
                t.location_title = item.location_title;
                t.online = item.online;
                t.template_id = item.template_id;
                t.deck = item.deck;
                t.rotate = item.rotate;
                t.template_title = t.template_id > 0 ? item.template_title : @"Unassigned";
                t.title = item.title;
                t.template_id_announce = item.template_id_announce;
                t.template_id_end = item.template_id_end;
                t.group_by_location = item.group_by_location;
                t.group_by_start = item.group_by_start;
                t.all_sessions = item.all_sessions;
                t.notified = item.notified;
                t.notified_date = item.notified_date;

                _terminals.Add(t);
            }

            return _terminals;
        }

        public Terminal single(Int32 event_id, Int32 id)
        {
            return all_by_event(event_id).Single(x => x.id == id);
        }

        public Terminal add(Terminal t)
        {
            terminal te = new terminal();

            te.event_id = t.event_id;
            te.location_id = t.location_id;
            te.online = true;
            te.template_id = t.template_id;
            te.title = t.title;
            te.last_online = DateTime.Now;
            te.deck = t.deck;
            te.rotate = t.rotate;
            te.group_by_location = t.group_by_location;
            te.group_by_start = t.group_by_start;
            te.all_sessions = t.all_sessions;
            te.template_id_announce = t.template_id_announce;
            te.template_id_end = t.template_id_end;
            te.notified = false;
            te.notified_date = new DateTime?();
            
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
            te.rotate = t.rotate;
            te.deck = t.deck;
            te.template_id_announce = t.template_id_announce;
            te.template_id_end = t.template_id_end;
            te.group_by_location = t.group_by_location;
            te.group_by_start = t.group_by_start;
            te.all_sessions = t.all_sessions;
            te.notified = t.notified;
            te.notified_date = t.notified_date;

            db.SubmitChanges();

            return t;
        }

        public Boolean delete(Int32 id)
        {
            terminal t = db.terminals.Single(x => x.id == id);

            db.terminals.DeleteOnSubmit(t);

            db.SubmitChanges();

            return true;
        }

        public List<Terminal> offline_terminals()
        {
            List<Terminal> _terminals = new List<Terminal>();

            var result = from terms in db.terminals
                where terms.last_online < DateTime.Now.AddMinutes(-10)
                             select terms;

            foreach (var item in result)
            {
                Terminal t = new Terminal();

                t.event_id = item.event_id;
                t.id = item.id;
                t.last_online = item.last_online;
                t.location_id = item.location_id;
                t.online = item.online;
                t.template_id = item.template_id;
                t.title = item.title;
                t.notified = item.notified;
                t.notified_date = item.notified_date;

                _terminals.Add(t);
            }

            return _terminals;
        }

        public bool notify(int id)
        {
            terminal terminal = this.db.terminals.Single<terminal>((Expression<Func<terminal, bool>>)(x => x.id == id));
            terminal.notified = true;
            terminal.notified_date = new DateTime?(DateTime.Now);
            this.db.SubmitChanges();
            return true;
        }

        public Boolean offline(Int32 id)
        {
            terminal t = db.terminals.Single(x => x.id == id);

            t.online = false;

            db.SubmitChanges();

            return true;
        }

        public List<Terminal> offline_to_notify()
        {
            return this.offline_terminals().Where<Terminal>((Func<Terminal, bool>)(x => !x.notified)).ToList<Terminal>();
        }

        public bool online(int id)
        {
            terminal terminal = this.db.terminals.Single<terminal>((Expression<Func<terminal, bool>>)(x => x.id == id));
            terminal.online = true;
            terminal.last_online = DateTime.Now;
            terminal.notified = false;
            terminal.notified_date = new DateTime?();
            this.db.SubmitChanges();
            return true;
        }
    }

    public class Terminal
    {
        public Int32 id { get; set; }
        public string title { get; set; }
        public Int32? location_id { get; set; }
        public string location_title { get; set; }
        public Int32? template_id { get; set; }
        public string template_title { get; set; }
        public Boolean online { get; set; }
        public Int32 event_id { get; set; }
        public DateTime last_online { get; set; }
        public Int32? deck { get; set; }
        public Int32? rotate { get; set; }
        public Int32? template_id_announce { get; set; }
        public Int32? template_id_end { get; set; }
        public bool all_sessions { get; set; }
        public bool group_by_location { get; set; }
        public bool group_by_start { get; set; }
        public bool notified { get; set; }
        public DateTime? notified_date { get; set; }
    }
}
