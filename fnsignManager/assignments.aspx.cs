using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using schedInterface;

namespace fnsignManager
{
    public partial class assignments : System.Web.UI.Page
    {
        private schedInterface.terminals _terminals = new terminals();

        protected void Page_Load(object sender, EventArgs e)
        {
            permissions();

            string online = "Online";

            foreach (Terminal t in _terminals.all_by_event(Convert.ToInt32(Session["event_id"].ToString())))
            {
                online = t.online ? "Online" : "Offline";

                ph_tags.Controls.Add(new LiteralControl("<tr><td data-title=\"Title\">" + t.title.ToUpper() + "</td><td data-title=\"Online\" class=\"hidden-xs hidden-sm\">" + online + "</td><td data-title=\"Location\" class=\"hidden-xs hidden-sm\">" + t.location_title + "</td><td data-title=\"Template\" class=\"hidden-xs hidden-sm\">" + t.template_title + "</td><td data-title=\"Actions\"><a href=\"/assignments/edit/" + t.id + "\"><i class=\"fa fa-edit\"></i></a> <a href=\"/assignments/delete/" + t.id + "\"><i class=\"fa fa-trash-o\"></i></a></td></tr>"));
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