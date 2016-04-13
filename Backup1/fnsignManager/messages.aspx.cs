using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using schedInterface;

namespace fnsignManager
{
    public partial class messages : System.Web.UI.Page
    {
        private schedInterface.messages _messages = new schedInterface.messages();

        protected void Page_Load(object sender, EventArgs e)
        {
            permissions();

            // get by event

            string timespan;

            foreach(Message m in _messages.all_by_event(Convert.ToInt32(Session["event_id"])))
            {
                if (m.entire)
                {
                    timespan = "Until Removed";
                }
                else
                {
                    if (m.start != null)
                    {
                        timespan = Convert.ToDateTime(m.start).ToShortDateString() + " " +
                                   Convert.ToDateTime(m.start).ToShortTimeString();

                        if (m.stop != null)
                        {
                            timespan += " - " + Convert.ToDateTime(m.stop).ToShortDateString() + " " + Convert.ToDateTime(m.stop).ToShortTimeString();
                        }
                    }
                    else
                    {
                        timespan = "Not Set";
                    }
                }

                ph_tags.Controls.Add(new LiteralControl("<tr><td data-title=\"Title\">" + m.title.ToUpper() + "</td><td data-title=\"Message\">" + m.message + "</td><td data-title=\"Timespan\">" + timespan + "</td><td data-title=\"Actions\"><a href=\"/announcements/edit/" + m.id + "\"><i class=\"fa fa-edit\"></i></a> <a href=\"/announcements/delete/" + m.id + "\"><i class=\"fa fa-trash-o\"></i></a></td></tr>"));
            }

            foreach (Message m in _messages.all_by_template())
            {
                if (m.entire)
                {
                    timespan = "Until Removed";
                }
                else
                {
                    if (m.start != null)
                    {
                        timespan = Convert.ToDateTime(m.start).ToShortDateString() + " " +
                                   Convert.ToDateTime(m.start).ToShortTimeString();

                        if (m.stop != null)
                        {
                            timespan += " - " + Convert.ToDateTime(m.stop).ToShortDateString() + " " + Convert.ToDateTime(m.stop).ToShortTimeString();
                        }
                    }
                    else
                    {
                        timespan = "Not Set";
                    }
                }

                ph_by_template.Controls.Add(new LiteralControl("<tr><td data-title=\"Title\">" + m.title.ToUpper() + "</td><td data-title=\"Template\">" + m.template_title + "</td><td data-title=\"Timespan\">" + timespan + "</td><td data-title=\"Actions\"><a href=\"/announcements/edit/" + m.id + "\"><i class=\"fa fa-edit\"></i></a> <a href=\"/announcements/delete/" + m.id + "\"><i class=\"fa fa-trash-o\"></i></a></td></tr>"));
            }

            foreach (Message m in _messages.all_by_terminal())
            {
                if (m.entire)
                {
                    timespan = "Until Removed";
                }
                else
                {
                    if (m.start != null)
                    {
                        timespan = Convert.ToDateTime(m.start).ToShortDateString() + " " +
                                   Convert.ToDateTime(m.start).ToShortTimeString();

                        if (m.stop != null)
                        {
                            timespan += " - " + Convert.ToDateTime(m.stop).ToShortDateString() + " " + Convert.ToDateTime(m.stop).ToShortTimeString();
                        }
                    }
                    else
                    {
                        timespan = "Not Set";
                    }
                }

                ph_by_terminal.Controls.Add(new LiteralControl("<tr><td data-title=\"Title\">" + m.title.ToUpper() + "</td><td data-title=\"Terminal\">" + m.terminal_title + "</td><td data-title=\"Timespan\">" + timespan + "</td><td data-title=\"Actions\"><a href=\"/announcements/edit/" + m.id + "\"><i class=\"fa fa-edit\"></i></a> <a href=\"/announcements/delete/" + m.id + "\"><i class=\"fa fa-trash-o\"></i></a></td></tr>"));
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