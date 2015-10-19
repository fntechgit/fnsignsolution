using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using schedInterface;

namespace fnsignManager
{
    public partial class dashboard : System.Web.UI.Page
    {
        public string total_media = "0";
        public string facebook_media = "0";
        public string instagram_media = "0";
        public string twitter_media = "0";
        public string unapproved_media = "0";
        public string all_media = "0";

        public string all_sessions = "0";
        public string locations = "0";
        public string tweets = "0";
        public string terminals = "0";

        private schedInterface.permissions _permissions = new schedInterface.permissions();
        private schedInterface.sessions _sessions = new schedInterface.sessions();
        private schedInterface.locations _locations = new schedInterface.locations();
        private schedInterface.mediaManager _media = new mediaManager();
        private schedInterface.terminals _terminals = new terminals();

        protected void Page_Load(object sender, EventArgs e)
        {
            permissions(); ;

            Int32 event_id = Convert.ToInt32(Session["event_id"].ToString());

            all_sessions = string.Format("{0:N0}", _sessions.by_event(event_id).Count());
            locations = string.Format("{0:N0}", _locations.by_event(event_id).Count());
            tweets = string.Format("{0:N0}", _media.get_all(event_id).Count());
            terminals = _terminals.by_event(event_id).Count().ToString();
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