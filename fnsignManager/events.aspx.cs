using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using schedInterface;

namespace fnsignManager
{
    public partial class events : System.Web.UI.Page
    {
        public string total_media = "0";
        public string facebook_media = "0";
        public string instagram_media = "0";
        public string twitter_media = "0";
        public string unapproved_media = "0";
        public string all_media = "0";

        private schedInterface.events _events = new schedInterface.events();

        protected void Page_Load(object sender, EventArgs e)
        {
            permissions();

            string start = "N/A";
            string end = "N/A";

            foreach (Event ev in _events.select_list())
            {
                start = ev.event_start == null ? "N/A" : Convert.ToDateTime(ev.event_start).ToShortDateString() + " " + Convert.ToDateTime(ev.event_start).ToShortTimeString();
                end = ev.event_end == null ? "N/A" : Convert.ToDateTime(ev.event_end).ToShortDateString() + " " + Convert.ToDateTime(ev.event_end).ToShortTimeString();

                ph_tags.Controls.Add(new LiteralControl("<tr><td data-title=\"Title\">" + ev.title + "</td><td data-title=\"URL\" class=\"hidden-xs hidden-sm\">" + ev.url + "</td><td data-title=\"API Key\" class=\"hidden-xs hidden-sm\">" + ev.api_key + "</td><td data-title=\"Last Update\" class=\"hidden-xs hidden-sm\">" + ev.last_update.ToShortDateString() + " " + ev.last_update.ToShortDateString() + "</td><td data-title=\"Interval\" class=\"hidden-xs hidden-sm\">" + ev.interval + "</td><td data-title=\"Start\" class=\"text-right\">" + start + "</td><td data-title=\"End\" class=\"text-right\">" + end + "</td><td data-title=\"Actions\"><a href=\"/events/edit/" + ev.id + "\"><i class=\"fa fa-edit\"></i></a> <a href=\"/events/delete/" + ev.id + "\"><i class=\"fa fa-trash-o\"></i></a></td></tr>"));
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