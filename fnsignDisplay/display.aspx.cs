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
        private schedInterface.overlays _overlays = new schedInterface.overlays();
        private schedInterface.sessions _sessions = new sessions();
        private schedInterface.locations _locations = new locations();
        private schedInterface.settings _settings = new settings();

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

                terminal_id.Value = t.id.ToString();

                if (t.template_id > 0)
                {
                    Template te = _templates.single(Convert.ToInt32(t.template_id));

                    Overlay o = _overlays.single(Convert.ToInt32(te.overlay));

                    Response.Redirect(_settings.display_url() + "/" + o.header + "/" + t.id);
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