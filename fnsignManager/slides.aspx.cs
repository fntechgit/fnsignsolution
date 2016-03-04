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

        }
    }
}