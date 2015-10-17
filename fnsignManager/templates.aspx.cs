using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using schedInterface;

namespace fnsignManager
{
    public partial class templates : System.Web.UI.Page
    {
        private schedInterface.templates _templates = new schedInterface.templates();

        protected void Page_Load(object sender, EventArgs e)
        {
            permissions();

            foreach (Template t in _templates.by_event(Convert.ToInt32(Session["event_id"].ToString())))
            {
                ph_tags.Controls.Add(new LiteralControl("<tr><td data-title=\"Title\">" + t.title.ToUpper() + "</td><td data-title=\"Venue\" class=\"hidden-xs hidden-sm\">" + t.venue + "</td><td data-title=\"Attendees\" class=\"hidden-xs hidden-sm\">" + t.goers + "</td><td data-title=\"Speakers\" class=\"hidden-xs hidden-sm\">" + t.speakers + "</td><td data-title=\"Type\">" + t.event_type + "</td><td data-title=\"Start\" class=\"text-right\">" + start + "</td><td data-title=\"End\" class=\"text-right\">" + end + "</td></tr>"));
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