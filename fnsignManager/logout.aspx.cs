﻿using System;

namespace fnsignManager
{
    public partial class logout : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("/login");
        }
    }
}