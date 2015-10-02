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
    }
}
