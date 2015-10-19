using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using schedInterface;

namespace fnsignManager
{
    public partial class edit_assignment : System.Web.UI.Page
    {
        private schedInterface.terminals _terminals = new terminals();
        private schedInterface.templates _templates = new schedInterface.templates();

        protected void Page_Load(object sender, EventArgs e)
        {
            permissions();

            if (!Page.IsPostBack)
            {
                ddl_template.DataSource = _templates.by_event(Convert.ToInt32(Session["event_id"].ToString()));
                ddl_template.DataValueField = "id";
                ddl_template.DataTextField = "title";
                ddl_template.DataBind();

                ListItem i = new ListItem("Select Template", "0");

                ddl_template.Items.Insert(0, i);

                Terminal t = _terminals.single(Convert.ToInt32(Session["event_id"].ToString()), Convert.ToInt32(Page.RouteData.Values["id"] as string));

                title.Text = t.title;

                if (t.template_id > 0)
                {
                    ddl_template.SelectedValue = t.template_id.ToString();
                }
                else
                {
                    ddl_template.SelectedIndex = 0;
                }
            }
        }

        protected void update(object sender, EventArgs e)
        {
            Terminal t = _terminals.single(Convert.ToInt32(Session["event_id"].ToString()), Convert.ToInt32(Page.RouteData.Values["id"] as string));

            if (ddl_template.SelectedIndex > 0)
            {
                t.template_id = Convert.ToInt32(ddl_template.SelectedValue);
            }
            else
            {
                t.template_id = null;
            }

            _terminals.update(t);

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
                    security_level.Visible = false;
                    break;
                case "content":
                    security_level.Visible = false;
                    break;
            }
        }
    }
}