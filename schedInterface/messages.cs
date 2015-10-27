using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace schedInterface
{
    public class messages
    {
        private schedDataContext db = new schedDataContext();

        public Message create(Message m)
        {
            message me = new message();

            me.entire = m.entire;
            me.event_id = m.event_id;
            me.message1 = m.message;
            me.pic = m.pic;
            me.start = m.start;
            me.stop = m.stop;
            me.template_id = m.template_id;
            me.terminal_id = m.terminal_id;
            me.title = m.title;

            db.messages.InsertOnSubmit(me);

            db.SubmitChanges();

            m.id = me.id;

            return m;
        }

        public Message update(Message m)
        {
            message me = db.messages.Single(x => x.id == m.id);

            me.entire = m.entire;
            me.event_id = m.event_id;
            me.message1 = m.message;
            me.pic = m.pic;
            me.start = m.start;
            me.stop = m.stop;
            me.template_id = m.template_id;
            me.terminal_id = m.terminal_id;
            me.title = m.title;

            db.SubmitChanges();

            return m;
        }

        public Boolean delete(Int32 id)
        {
            var result = from msg in db.messages
                where msg.id == id
                select msg;

            db.messages.DeleteAllOnSubmit(result);

            db.SubmitChanges();

            return true;
        }

        public List<Message> by_terminal(Int32 id)
        {
            List<Message> _messages = new List<Message>();

            var result = from msg in db.messages
                where msg.terminal_id == id
                orderby msg.title
                select msg;

            foreach (var item in result)
            {
                Message m = new Message();

                m.entire = item.entire;
                m.event_id = item.event_id;
                m.id = item.id;
                m.message = item.message1;
                m.pic = item.pic;
                m.start = item.start;
                m.stop = item.stop;
                m.template_id = item.template_id;
                m.terminal_id = item.terminal_id;
                m.title = item.title;

                _messages.Add(m);
            }

            return _messages;
        }

        public List<Message> by_template(Int32 id)
        {
            List<Message> _messages = new List<Message>();

            var result = from msg in db.messages
                         where msg.template_id==id
                         orderby msg.title
                         select msg;

            foreach (var item in result)
            {
                Message m = new Message();

                m.entire = item.entire;
                m.event_id = item.event_id;
                m.id = item.id;
                m.message = item.message1;
                m.pic = item.pic;
                m.start = item.start;
                m.stop = item.stop;
                m.template_id = item.template_id;
                m.terminal_id = item.terminal_id;
                m.title = item.title;

                _messages.Add(m);
            }

            return _messages;
        }

        public List<Message> by_event(Int32 id)
        {
            List<Message> _messages = new List<Message>();

            var result = from msg in db.messages
                         where msg.event_id == id
                         orderby msg.title
                         select msg;

            foreach (var item in result)
            {
                Message m = new Message();

                m.entire = item.entire;
                m.event_id = item.event_id;
                m.id = item.id;
                m.message = item.message1;
                m.pic = item.pic;
                m.start = item.start;
                m.stop = item.stop;
                m.template_id = item.template_id;
                m.terminal_id = item.terminal_id;
                m.title = item.title;

                _messages.Add(m);
            }

            return _messages;
        }

        public Message single(Int32 id)
        {
            Message m = new Message();

            var result = from msg in db.messages
                where msg.id == id
                select msg;

            foreach (var item in result)
            {
                m.entire = item.entire;
                m.event_id = item.event_id;
                m.id = item.id;
                m.message = item.message1;
                m.pic = item.pic;
                m.start = item.start;
                m.stop = item.stop;
                m.template_id = item.template_id;
                m.terminal_id = item.terminal_id;
                m.title = item.title;
            }

            return m;
        }

        public Message random(Int32? event_id, Int32? template_id, Int32? terminal_id)
        {
            Message m = new Message();

            settings _settings = new settings();

            foreach (var item in db.messages_get_random(event_id, template_id, terminal_id))
            {
                m.entire = item.entire;
                m.event_id = item.event_id;
                m.id = item.id;
                m.message = item.message;

                if (item.pic != null)
                {
                    m.pic = "<img src=\"" + _settings.site_url() + "/uploads/" + item.pic + "\" />";
                }

                m.start = item.start;
                m.stop = item.stop;
                m.template_id = item.template_id;
                m.terminal_id = item.terminal_id;
                m.title = item.title;
            }

            return m;
        }

        public List<Message> all_by_event(Int32 id)
        {
            List<Message> _messages = new List<Message>();

            foreach (var item in db.messages_by_event(id))
            {
                Message m = new Message();

                m.entire = item.entire;
                m.event_id = item.event_id;
                m.id = item.id;
                m.message = item.message;
                m.pic = item.pic;
                m.start = item.start;
                m.stop = item.stop;
                m.template_id = item.template_id;
                m.terminal_id = item.terminal_id;
                m.title = item.title;

                _messages.Add(m);
            }

            return _messages;
        }

        public List<Message> all_by_template()
        {
            List<Message> _messages = new List<Message>();

            foreach (var item in db.messages_by_template())
            {
                Message m = new Message();

                m.entire = item.entire;
                m.event_id = item.event_id;
                m.id = item.id;
                m.message = item.message;
                m.pic = item.pic;
                m.start = item.start;
                m.stop = item.stop;
                m.template_id = item.template_id;
                m.terminal_id = item.terminal_id;
                m.title = item.title;
                m.template_title = item.template_title;

                _messages.Add(m);
            }

            return _messages;
        }

        public List<Message> all_by_terminal()
        {
            List<Message> _messages = new List<Message>();

            foreach (var item in db.messages_by_terminal())
            {
                Message m = new Message();

                m.entire = item.entire;
                m.event_id = item.event_id;
                m.id = item.id;
                m.message = item.message;
                m.pic = item.pic;
                m.start = item.start;
                m.stop = item.stop;
                m.template_id = item.template_id;
                m.terminal_id = item.terminal_id;
                m.title = item.title;
                m.terminal_title = item.terminal_title;

                _messages.Add(m);
            }

            return _messages;
        }
    }

    public class Message
    {
        public Int32 id { get; set; }
        public Int32? event_id { get; set; }
        public Int32? template_id { get; set; }
        public Int32? terminal_id { get; set; }
        public DateTime? start { get; set; }
        public DateTime? stop { get; set; }
        public Boolean entire { get; set; }
        public string title { get; set; }
        public string message { get; set; }
        public string pic { get; set; }
        public string template_title { get; set; }
        public string terminal_title { get; set; }
    }
}
