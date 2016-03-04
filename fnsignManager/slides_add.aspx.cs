using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using schedInterface;
using System.IO;

namespace fnsignManager
{
    public partial class slides_add : System.Web.UI.Page
    {
        public Deck d;

        public string total_media = "0";
        public string facebook_media = "0";
        public string instagram_media = "0";
        public string twitter_media = "0";
        public string unapproved_media = "0";
        public string all_media = "0";

        private schedInterface.slides _slides = new schedInterface.slides();
        private schedInterface.decks _decks = new schedInterface.decks();

        protected void Page_Load(object sender, EventArgs e)
        {
            permissions();

            d = _decks.single(Convert.ToInt32(Page.RouteData.Values["id"]));
        }

        protected void update(object sender, EventArgs e)
        {
            Slide s = new Slide();

            s.deck_id = d.id;

            if (slideImg.HasFile)
            {
                string path = Server.MapPath("~/uploads/");
                string extension = Path.GetExtension(slideImg.FileName.ToString());
                string unique = "DECK_" + d.id + "_" + Guid.NewGuid().ToString();

                slideImg.SaveAs(path + unique + extension);

                s.source = unique + extension;

                _slides.create(s);

                pnl_success.Visible = true;
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