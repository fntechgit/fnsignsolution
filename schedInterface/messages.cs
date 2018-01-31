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
            me.priority = m.priority;
            me.session_full = m.session_full;
            me.test = m.test;

            db.messages.InsertOnSubmit(me);

            db.SubmitChanges();

            m.id = me.id;

            return m;
        }

        public Message session_full(int id)
        {
            Message message = new Message();
            foreach (session_fullResult sessionFullResult in (IEnumerable<session_fullResult>)this.db.session_full(new int?(id)))
            {
                message.session_full = true;
                message.event_id = sessionFullResult.event_id;
                message.entire = true;
                message.id = sessionFullResult.id;
                message.message = sessionFullResult.message;
                message.pic = sessionFullResult.pic;
            }
            return message;
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
            me.priority = m.priority;
            me.session_full = m.session_full;
            me.test = m.test;

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
                m.priority = item.priority;
                m.session_full = item.session_full;
                m.test = item.test;

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
                m.priority = item.priority;
                m.session_full = item.session_full;
                m.test = item.test;

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
                m.priority = item.priority;
                m.session_full = item.session_full;
                m.test = item.test;

                _messages.Add(m);
            }

            return _messages;
        }

        public Message random(int? event_id, int? template_id, int? terminal_id, bool priority)
        {
            Message message = new Message();
            settings settings = new settings();
            foreach (messages_get_random_priorityResult randomPriorityResult in (IEnumerable<messages_get_random_priorityResult>)this.db.messages_get_random_priority(event_id, template_id, terminal_id, new bool?(priority)))
            {
                message.entire = randomPriorityResult.entire;
                message.event_id = randomPriorityResult.event_id;
                message.id = randomPriorityResult.id;
                message.message = randomPriorityResult.message;
                if (randomPriorityResult.pic != null)
                    message.pic = "<img src=\"" + settings.site_url() + "/uploads/" + randomPriorityResult.pic + "\" />";
                message.start = randomPriorityResult.start;
                message.stop = randomPriorityResult.stop;
                message.template_id = randomPriorityResult.template_id;
                message.terminal_id = randomPriorityResult.terminal_id;
                message.title = randomPriorityResult.title;
                message.priority = randomPriorityResult.priority;
                message.session_full = randomPriorityResult.session_full;
                message.test = randomPriorityResult.test;
            }
            return message;
        }

        public Message preview(int? event_id, int? template_id, int? terminal_id, bool priority)
        {
            Message message = new Message();
            settings settings = new settings();
            foreach (messages_get_random_priority_testResult priorityTestResult in (IEnumerable<messages_get_random_priority_testResult>)this.db.messages_get_random_priority_test(event_id, template_id, terminal_id, new bool?(priority)))
            {
                message.entire = priorityTestResult.entire;
                message.event_id = priorityTestResult.event_id;
                message.id = priorityTestResult.id;
                message.message = priorityTestResult.message;
                if (priorityTestResult.pic != null)
                    message.pic = "<img src=\"" + settings.site_url() + "/uploads/" + priorityTestResult.pic + "\" />";
                message.start = priorityTestResult.start;
                message.stop = priorityTestResult.stop;
                message.template_id = priorityTestResult.template_id;
                message.terminal_id = priorityTestResult.terminal_id;
                message.title = priorityTestResult.title;
                message.priority = priorityTestResult.priority;
                message.session_full = priorityTestResult.session_full;
                message.test = priorityTestResult.test;
            }
            return message;
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
                m.priority = item.priority;
                m.session_full = item.session_full;
                m.test = item.test;
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

                m.priority = item.priority;
                m.session_full = item.session_full;
                m.test = item.test;
            }

            return m;
        }

        public List<Message> all_by_event_test(int id)
        {
            List<Message> messageList = new List<Message>();
            foreach (messages_by_event_testResult byEventTestResult in (IEnumerable<messages_by_event_testResult>)this.db.messages_by_event_test(new int?(id)))
                messageList.Add(new Message()
                {
                    entire = byEventTestResult.entire,
                    event_id = byEventTestResult.event_id,
                    id = byEventTestResult.id,
                    message = byEventTestResult.message,
                    pic = byEventTestResult.pic,
                    start = byEventTestResult.start,
                    stop = byEventTestResult.stop,
                    template_id = byEventTestResult.template_id,
                    terminal_id = byEventTestResult.terminal_id,
                    title = byEventTestResult.title,
                    priority = byEventTestResult.priority,
                    session_full = byEventTestResult.session_full,
                    test = byEventTestResult.test
                });
            return messageList;
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

                m.priority = item.priority;
                m.session_full = item.session_full;
                m.test = item.test;

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
                m.priority = item.priority;
                m.session_full = item.session_full;
                m.test = item.test;

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
                m.priority = item.priority;
                m.session_full = item.session_full;
                m.test = item.test;

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

        public bool priority { get; set; }
        public bool session_full { get; set; }
        public bool test { get; set; }
    }
}
