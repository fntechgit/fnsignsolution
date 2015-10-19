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

    }
}
