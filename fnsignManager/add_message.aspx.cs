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
    public partial class add_message : System.Web.UI.Page
    {
        public string add_edit = "Add";

        public string total_media = "0";
        public string facebook_media = "0";
        public string instagram_media = "0";
        public string twitter_media = "0";
        public string unapproved_media = "0";
        public string all_media = "0";

        private schedInterface.messages _messages = new schedInterface.messages();
        private schedInterface.terminals _terminals = new terminals();
        private schedInterface.templates _templates = new schedInterface.templates();

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

            Int32 event_id = Convert.ToInt32(Session["event_id"]);

            if (!Page.IsPostBack)
            {
                ddl_template.DataSource = _templates.by_event(event_id);
                ddl_template.DataValueField = "id";
                ddl_template.DataTextField = "title";
                ddl_template.DataBind();

                ListItem i = new ListItem("Select Template if Desired", "");

                ddl_template.Items.Insert(0, i);

                ddl_terminal.DataSource = _terminals.by_event(event_id);
                ddl_terminal.DataValueField = "id";
                ddl_terminal.DataTextField = "title";
                ddl_terminal.DataBind();

                ListItem i2 = new ListItem("Select Terminal if Desired", "");

                ddl_terminal.Items.Insert(0, i2);

                if (Page.RouteData.Values["id"] != null)
                {
                    // this is an update
                    Message m = _messages.single(Convert.ToInt32(Page.RouteData.Values["id"]));

                    title.Text = m.title;
                    message.Text = m.message;

                    if (m.start != null)
                    {
                        start.Text = Convert.ToDateTime(m.start).ToShortDateString();
                        start_time.Text = Convert.ToDateTime(m.start).ToShortTimeString();
                    }

                    if (m.stop != null)
                    {
                        end.Text = Convert.ToDateTime(m.stop).ToShortDateString();
                        end_time.Text = Convert.ToDateTime(m.stop).ToShortTimeString();
                    }

                    
                    active.Checked = m.entire;
                    

                    current_image.ImageUrl = "/uploads/" + m.pic;

                    pnl_current_image.Visible = true;
                }
            }
        }

        protected void update(object sender, EventArgs e)
        {
            Message m = new Message();

            Boolean is_update = false;

            if (Page.RouteData.Values["id"] != null)
            {
                m = _messages.single(Convert.ToInt32(Page.RouteData.Values["id"] as string));

                is_update = true;
            }

            m.title = title.Text;
            m.message = message.Text;

            Boolean is_event = true;

            if (ddl_template.SelectedIndex > 0)
            {
                m.template_id = Convert.ToInt32(ddl_template.SelectedValue);
                is_event = false;
            }
            else
            {
                m.template_id = null;
            }

            if (ddl_terminal.SelectedIndex > 0)
            {
                m.terminal_id = Convert.ToInt32(ddl_terminal.SelectedValue);
                is_event = false;
            }

            if (is_event)
            {
                m.event_id = Convert.ToInt32(Session["event_id"]);
            }

            m.entire = active.Checked;

            if (!m.entire)
            {
                if (!string.IsNullOrEmpty(start.Text) && !string.IsNullOrEmpty(start_time.Text))
                {
                    m.start = Convert.ToDateTime(start.Text + " " + start_time.Text);
                }
                else
                {
                    m.start = null;
                }

                if (!string.IsNullOrEmpty(end.Text) && !string.IsNullOrEmpty(end_time.Text))
                {
                    m.stop = Convert.ToDateTime(end.Text + " " + end_time.Text);
                }
                else
                {
                    m.stop = null;
                }
            }

            if (image.HasFile)
            {
                string path = Server.MapPath("~/uploads/");
                string extension = Path.GetExtension(image.FileName.ToString());
                string unique = Guid.NewGuid().ToString();

                image.SaveAs(path + unique + extension);

                m.pic = unique + extension;
            }
            else
            {
                if (!is_update)
                {
                    m.pic = null;
                }
            }

            if (is_update)
            {
                _messages.update(m);
            }
            else
            {
                m = _messages.create(m);
            }

            pnl_success.Visible = true;
            current_image.ImageUrl = "/uploads/" + m.pic;
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
                    break;
                case "content":
                    break;
            }
        }
    }
}