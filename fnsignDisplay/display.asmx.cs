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
        schedInterface.mediaManager _media = new mediaManager();

        [WebMethod(Description = "Get Current Session", EnableSession = true)]
        public Session current(string location)
        {
            Int32 event_id = Convert.ToInt32(Context.Session["event_id"]);

            Session s = _sessions.current(event_id, location);

            s.event_start = s.start.ToShortTimeString();
            s.event_end = s.end.ToShortTimeString();

            return s;
        }

        [WebMethod(Description = "Get Next Session", EnableSession = true)]
        public Session next(string location)
        {
            Int32 event_id = Convert.ToInt32(Context.Session["event_id"]);

            Session current = this.current(location);

            return _sessions.next(event_id, location, current.end);
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

    }
}
