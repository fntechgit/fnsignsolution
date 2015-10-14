using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using schedInterface;

namespace fnsignManager
{
    public partial class sessions : System.Web.UI.Page
    {
        public string total_media = "0";
        public string facebook_media = "0";
        public string instagram_media = "0";
        public string twitter_media = "0";
        public string unapproved_media = "0";
        public string all_media = "0";

        private schedInterface.sessions _sessions = new schedInterface.sessions();

        protected void Page_Load(object sender, EventArgs e)
        {
            permissions();

            string start = "N/A";
            string end = "N/A";

            foreach (Session t in _sessions.by_event(Convert.ToInt32(Session["event_id"].ToString())))
            {
                start = t.start == null ? "N/A" : Convert.ToDateTime(t.start).ToShortDateString() + " " + Convert.ToDateTime(t.start).ToShortTimeString();
                end = t.end == null ? "N/A" : Convert.ToDateTime(t.end).ToShortDateString() + " " + Convert.ToDateTime(t.end).ToShortTimeString();

                ph_tags.Controls.Add(new LiteralControl("<tr><td data-title=\"Title\">" + t.name.ToUpper() + "</td><td data-title=\"Venue\" class=\"hidden-xs hidden-sm\">" + t.venue + "</td><td data-title=\"Attendees\" class=\"hidden-xs hidden-sm\">" + t.goers + "</td><td data-title=\"Speakers\" class=\"hidden-xs hidden-sm\">" + t.speakers + "</td><td data-title=\"Type\">" + t.event_type + "</td><td data-title=\"Start\" class=\"text-right\">" + start + "</td><td data-title=\"End\" class=\"text-right\">" + end + "</td></tr>"));
            }
        }

        private void permissions()
        {
            if (string.IsNullOrEmpty(Session["event_id"] as string))
            {
                Response.Redirect("/login");
            }

            if (string.IsNullOrEmpty(Session["user_access"] as string))
            {
                Response.Redirect("/login");
            }
            else
            {
                check_levels(Session["user_access"] as string);
            }
        }

        private void check_levels(string user_level)
        {
            switch (user_level)
            {
                case "system":
                    event_link.Visible = true;
                    display_link.Visible = true;
                    user_link.Visible = true;
                    preference_link.Visible = true;
                    break;
                case "event":
                    display_link.Visible = true;
                    preference_link.Visible = true;
                    break;
                case "content":
                    break;
            }
        }
    }
}