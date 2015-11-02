using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using schedInterface;

namespace fnsignDisplay
{
    /// <summary>
    /// Summary description for display1
    /// </summary>
    [WebService(Namespace = "http://fndisplay.fntech.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]

    public class displaySvc : System.Web.Services.WebService
    {
        private schedInterface.sessions _sessions = new sessions();
        private schedInterface.terminals _terminals = new terminals();
        private schedInterface.templates _templates = new templates();
        private schedInterface.overlays _overlays = new schedInterface.overlays();
        private schedInterface.settings _settings = new settings();
        private schedInterface.messages _messages = new messages();
        schedInterface.mediaManager _media = new mediaManager();

        [WebMethod(Description = "Get Current Session", EnableSession = true)]
        public Session current(string location)
        {
            Int32 event_id = Convert.ToInt32(Context.Session["event_id"]);

            Session s = _sessions.current(event_id, location);

            if (s.internal_id == 0)
            {
                s.name = "No Current Session";
                s.event_type = "Currently there is no session in this room";
                s.event_start = "Time";
                s.event_id = event_id;
            }
            else
            {
                s.event_start = s.start.ToShortTimeString();
                s.event_end = s.end.ToShortTimeString();
            }

            if (Context.Session["event_id"] == null)
            {
                s.event_id = 0;
            }

            return s;
        }

        [WebMethod(Description = "Login From Cookie", EnableSession = true)]
        public Boolean loginAgain(string event_id)
        {
            Context.Session["event_id"] = event_id;

            return true;
        }

        [WebMethod(Description = "Get Next Session", EnableSession = true)]
        public Session next(string location)
        {
            Int32 event_id = Convert.ToInt32(Context.Session["event_id"]);

            Session current = this.current(location);

            if (current.internal_id > 0)
            {
                return _sessions.next(event_id, location, current.end);
            }
            else
            {
                return _sessions.next(event_id, location, DateTime.Now);
            }
        }

        [WebMethod(Description = "Check for New Template", EnableSession = true)]
        public string template(Int32 terminal)
        {
            Terminal t = _terminals.single(Convert.ToInt32(Context.Session["event_id"]), terminal);

            Template temp = _templates.single(Convert.ToInt32(t.template_id));

            _terminals.online(terminal);

            return _settings.site_url() + "/uploads/" + temp.bgimage;
        }

        [WebMethod(Description = "Get Random Media", EnableSession = true)]
        public Media random()
        {
            return _media.random_by_event(Convert.ToInt32(Context.Session["event_id"]));
        }

        [WebMethod(Description = "Check for Announcement", EnableSession = true)]
        public Message get_message(Int32 template_id, Int32 terminal_id)
        {
            Int32 event_id = Convert.ToInt32(Context.Session["event_id"]);

            return _messages.random(event_id, template_id, terminal_id);
        }

    }
}
