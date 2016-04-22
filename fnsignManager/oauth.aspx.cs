using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RestSharp;

namespace fnsignManager
{
    public partial class oauth : System.Web.UI.Page
    {
        private schedInterface.osettings _settings = new schedInterface.osettings();
        protected void Page_Load(object sender, EventArgs e)
        {
            //var client = new RestClient("https://openstackid.org/oauth2");

            //var request = new RestRequest("auth");

            ////request.AddHeader("Authorization","Basic Base64-Encoded(" + Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes( _settings.client_id())) + ":" + Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(_settings.client_secret())) + ")");
            //request.AddHeader("Content-Type", "application/x-www-form-urlencoded");

            //request.Method = Method.POST;

            //request.AddParameter("response_type", "code");
            //request.AddParameter("scope", "https://openstackid-resources.openstack.org/summits/read");
            //request.AddParameter("client_id", _settings.client_id());
            //request.AddParameter("redirect_uri", "https://fnpix.fntech.com/login");
            //request.AddParameter("access_type", "offline");

            //IRestResponse response = client.Execute(request);

            //ph_response.Controls.Add(new LiteralControl("<h4>" + response.Content + "</h4>"));
        }
    }
}