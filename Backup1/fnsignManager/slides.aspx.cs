using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using schedInterface;

namespace fnsignManager
{
    public partial class slides : System.Web.UI.Page
    {
        public Int32 deck_id;

        public string total_media = "0";
        public string facebook_media = "0";
        public string instagram_media = "0";
        public string twitter_media = "0";
        public string unapproved_media = "0";
        public string all_media = "0";

        private schedInterface.decks _decks = new schedInterface.decks();
        private schedInterface.slides _slides = new schedInterface.slides();

        protected void Page_Load(object sender, EventArgs e)
        {
            permissions();

            deck_id = Convert.ToInt32(Page.RouteData.Values["id"]);

            foreach (Slide s in _slides.by_deck(Convert.ToInt32(Page.RouteData.Values["id"])))
            {
                ph_tags.Controls.Add(new LiteralControl("<tr><td><img src=\"/uploads/" + s.source + "\" width=\"95%\" /></td><td><a href=\"/slides/delete/" + s.id + "\"><i class=\"fa fa-trash-o\"></i></a></td></tr>"));
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