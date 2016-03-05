using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using schedInterface;

namespace fnsignManager
{
    public partial class decks_add : System.Web.UI.Page
    {
        public string add_edit = "Add";

        public string total_media = "0";
        public string facebook_media = "0";
        public string instagram_media = "0";
        public string twitter_media = "0";
        public string unapproved_media = "0";
        public string all_media = "0";

        private schedInterface.decks _decks = new schedInterface.decks();

        protected void Page_Load(object sender, EventArgs e)
        {
            permissions();

            if (Page.RouteData.Values["id"] != null)
            {
                add_edit = "Edit";

                if (!Page.IsPostBack)
                {

                    Deck d = _decks.single(Convert.ToInt32(Page.RouteData.Values["id"]));

                    title.Text = d.title;
                    description.Text = d.description;

                    btn_process.Text = "Update";
                }
            }
        }

        protected void update(object sender, EventArgs e)
        {
            Deck d = new Deck();

            Boolean is_update = false;

            if (Page.RouteData.Values["id"] != null)
            {
                // update
                d = _decks.single(Convert.ToInt32(Page.RouteData.Values["id"]));

                is_update = true;
            }

            d.title = title.Text;
            d.description = description.Text;
            d.event_id = Convert.ToInt32(Session["event_id"]);

            if (is_update)
            {
                _decks.update(d);
            }
            else
            {
                _decks.create(d);
            }

            pnl_success.Visible = true;
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