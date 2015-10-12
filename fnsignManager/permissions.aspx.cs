using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using schedInterface;

namespace fnsignManager
{
    public partial class permissions : System.Web.UI.Page
    {
        public string total_media = "0";
        public string facebook_media = "0";
        public string instagram_media = "0";
        public string twitter_media = "0";
        public string unapproved_media = "0";
        public string all_media = "0";
        public string user_name = string.Empty;

        private schedInterface.users _users = new schedInterface.users();
        private schedInterface.permissions _permissions = new schedInterface.permissions();

        protected void Page_Load(object sender, EventArgs e)
        {
            checkpermissions();

            // render permissions for user
            User u = _users.get_by_id(Convert.ToInt32(Page.RouteData.Values["id"] as string));

            user_name = u.first_name + " " + u.last_name;

            add_permission_link.NavigateUrl = "/permissions/add/" + u.id;
            edit_user_link.NavigateUrl = "/users/edit/" + u.id;

            foreach (Permission p in _permissions.get_by_user(u.id))
            {
                ph_tags.Controls.Add(new LiteralControl("<tr><td data-title=\"Event\">" + p.event_name + "</td><td data-title=\"Event Date\" class=\"hidden-xs hidden-sm\">" + p.event_date.ToShortDateString() + "</td><td data-title=\"Assigned Date\" class=\"hidden-xs hidden-sm\">" + p.assigned_date.ToShortDateString() + "</td><td data-title=\"Assigned by\" class=\"hidden-xs hidden-sm\">" + p.assigned_by_name + "</td><td data-title=\"Level\" class=\"text-right\">" + p.security_level + "</td><td data-title=\"Actions\"><a href=\"/permissions/edit/" + p.user_id + "/" + p.id + "\"><i class=\"fa fa-edit\"></i></a> <a href=\"/permissions/delete/" + p.user_id + "/" + p.id + "\"><i class=\"fa fa-trash-o\"></i></a></td></tr>"));
            }
        }

        private void checkpermissions()
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