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

            foreach (Template t in _templates.all_by_event(Convert.ToInt32(Session["event_id"].ToString())))
            {
                ph_tags.Controls.Add(new LiteralControl("<tr><td data-title=\"Title\">" + t.title.ToUpper() + "</td><td data-title=\"Bgcolor\" class=\"hidden-xs hidden-sm\">" + t.bgcolor + "</td><td data-title=\"Font\" class=\"hidden-xs hidden-sm\">" + t.overlay_font + "</td><td data-title=\"Font Color\" class=\"hidden-xs hidden-sm\">" + t.overlay_font_color + "</td><td data-title=\"Overlay\">" + t.overlay_title + "</td><td data-title=\"Actions\"><a href=\"/templates/edit/" + t.id + "\"><i class=\"fa fa-edit\"></i></a> <a href=\"/templates/delete/" + t.id + "\"><i class=\"fa fa-trash-o\"></i></a></td></tr>"));
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