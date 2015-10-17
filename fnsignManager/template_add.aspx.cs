using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using schedInterface;

namespace fnsignManager
{
    public partial class template_add : System.Web.UI.Page
    {
        public string add_edit = "Add";

        public string total_media = "0";
        public string facebook_media = "0";
        public string instagram_media = "0";
        public string twitter_media = "0";
        public string unapproved_media = "0";
        public string all_media = "0";

        private schedInterface.users _users = new schedInterface.users();

        private schedInterface.templates _templates = new schedInterface.templates();
        private schedInterface.overlays _overlays = new schedInterface.overlays();

        protected void Page_Load(object sender, EventArgs e)
        {
            permissions();

            if (Page.RouteData.Values["id"] != null)
            {
                add_edit = "Update";
                btn_process.Text = "Update";
            }
            else
            {
                btn_process.Text = "Add";
            }

            if (!Page.IsPostBack)
            {
                overlay_pattern.DataSource = _overlays.active();
                overlay_pattern.DataValueField = "id";
                overlay_pattern.DataTextField = "title";
                overlay_pattern.DataBind();

                ListItem i = new ListItem("Select Overlay", "0");

                overlay_pattern.Items.Insert(0, i);

                if (Page.RouteData.Values["id"] != null)
                {
                    // this is an update
                    Template t = _templates.single(Convert.ToInt32(Page.RouteData.Values["id"] as string));

                    title.Text = t.title;
                    bgcolor.Text = "#" + t.bgcolor;
                    overlay_pattern.SelectedValue = t.overlay.ToString();
                    overlay_font.Text = t.overlay_font;
                    overlay_font_color.Text = "#" + t.overlay_font_color;
                    current_image.ImageUrl = "/uploads/" + t.bgimage;

                    pnl_current_image.Visible = true;
                }
            }
        }

        protected void update(object sender, EventArgs e)
        {
            Template t = new Template();

            Boolean is_update = false;

            if (Page.RouteData.Values["id"] != null)
            {
                t = _templates.single(Convert.ToInt32(Page.RouteData.Values["id"] as string));

                is_update = true;
            }

            t.ad_interval = 0;
            t.bgcolor = bgcolor.Text.Replace("#", "");
            t.event_id = Convert.ToInt32(Session["event_id"].ToString());
            t.orientation = 1;
            t.overlay = Convert.ToInt32(overlay_pattern.SelectedValue);
            t.overlay_font = overlay_font.SelectedValue;
            t.overlay_font_color = overlay_font_color.Text.Replace("#", "");
            t.resolution = 1000;
            t.rotate_ads = false;
            t.title = title.Text;

            if (image.HasFile)
            {
                string path = Server.MapPath("~/uploads/");
                string extension = Path.GetExtension(image.FileName.ToString());
                string unique = Guid.NewGuid().ToString();

                image.SaveAs(path + unique + extension);

                t.bgimage = unique + extension;
            }
            else
            {
                if (!is_update)
                {
                    t.bgimage = null;
                }
            }

            if (is_update)
            {
                t = _templates.update(t);
            }
            else
            {
                t = _templates.add(t);
            }

            pnl_success.Visible = true;
            current_image.ImageUrl = "/uploads/" + t.bgimage;
            pnl_current_image.Visible = true;
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
                    security_level.Visible = false;
                    break;
                case "content":
                    security_level.Visible = false;
                    break;
            }
        }
    }
}