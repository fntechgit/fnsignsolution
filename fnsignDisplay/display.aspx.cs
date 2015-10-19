using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using schedInterface;

namespace fnsignDisplay
{
    public partial class display : System.Web.UI.Page
    {
        private schedInterface.terminals _terminals = new terminals();
        private schedInterface.templates _templates = new templates();
        private schedInterface.overlays _overlays = new overlays();
        private schedInterface.sessions _sessions = new sessions();
        private schedInterface.locations _locations = new locations();

        public string bgcolor;
        public string font;
        public string font_color;
        public string bgimage;
        public string session_type;
        public string session_title;
        public string start_time;
        public string end_time;
        public string next_session;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["event_id"] != null)
            {

                Terminal t = _terminals.single(Convert.ToInt32(Session["event_id"]),
                    Convert.ToInt32(Page.RouteData.Values["id"]));

                if (t.template_id > 0)
                {
                    // fill the content
                    Template temp = _templates.single(Convert.ToInt32(t.template_id));

                    Overlay o = _overlays.single(Convert.ToInt32(temp.overlay));

                    Location l = _locations.single(Convert.ToInt32(t.location_id));

                    event_id.Value = t.event_id.ToString();
                    location_sched.Value = l.sched_id;
                    terminal_id.Value = Page.RouteData.Values["id"].ToString();

                    Session current = _sessions.current(Convert.ToInt32(Session["event_id"]), l.sched_id);

                    Session next = _sessions.next(Convert.ToInt32(Session["event_id"]), l.sched_id, current.end);

                    bgcolor = temp.bgcolor;
                    font = temp.overlay_font;
                    font_color = temp.overlay_font_color;
                    bgimage = temp.bgimage;

                    session_title = current.name;
                    session_type = current.event_type;
                    start_time = current.start.ToShortTimeString();
                    end_time = current.end.ToShortTimeString();

                    next_session = next.name;

                    // now render the content
                    Page.Header.Controls.Add(new LiteralControl(o.header));
                    footer.Controls.Add(new LiteralControl(o.footer));
                    footer.Controls.Add(
                        new LiteralControl("<script type=\"text/javascript\" src=\"/js/display.js\"></script>"));
                    body.Controls.Add(new LiteralControl(o.body));
                }
                else
                {
                    // display the waiting signal
                    pnl_no_template.Visible = true;

                    footer.Controls.Add(
                        new LiteralControl("<script type=\"text/javascript\" src=\"/js/notemplate.js\"></script>"));
                }
            }
            else
            {
                Response.Redirect("/details");
            }
        }
    }
}