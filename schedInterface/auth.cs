using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;

namespace schedInterface
{
    public class auth
    {
        public string login(string conference_url, string api_key)
        {
            var client = new RestClient(conference_url + "/api");

            var request = new RestRequest("auth/login");

            request.AddParameter("api_key", api_key);
            request.AddParameter("username", "justin.tabb@ovri.de");
            request.AddParameter("password", "test");

            IRestResponse response = client.Execute(request);

            return response.Content;
        }

        public string get_token(string client_id, string secret)
        {
            var client = new RestClient("https://openstackid.org/oauth2");

            var request = new RestRequest("auth");

            request.AddParameter("client_id", client_id);
            request.AddParameter("scope", "summit");

            IRestResponse response = client.Execute(request);

            return response.Content;
        }
    }
}
