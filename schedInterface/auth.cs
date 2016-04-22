using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Extensions;

namespace schedInterface
{
    public class auth
    {

        private osettings _settings = new osettings();

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
            
            request.AddHeader("authorization",
                "Basic Base64-Encoded(" +Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(_settings.client_id())) + ":" + Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(_settings.client_secret())) + ")");
            request.AddHeader("content_type", "application/x-www-form-urlencoded");
            request.AddParameter("grant_type", "client_credentials");
            request.AddParameter("scope", "summits");

            IRestResponse response = client.Execute(request);

            return response.Content;
        }

        public string auth_justin(string client_id, string secret)
        {
            var client = new RestClient("https://openstackid.org/oauth2");

            var request = new RestRequest("token");

            //request.AddHeader("Authorization","Basic Base64-Encoded(" + Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes( _settings.client_id())) + ":" + Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(_settings.client_secret())) + ")");
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");

            request.Method = Method.POST;


            request.AddParameter("grant_type", "client_credentials");
            request.AddParameter("scope", "https://openstackid-resources.openstack.org/summits/read");
            request.AddParameter("client_id", client_id);
            request.AddParameter("client_secret", secret);

            IRestResponse response = client.Execute(request);

            return response.Content;
        }

        //public OAuthResponse token(string client_id, string client_secret)
        //{
        //    var client = new System.Net.Http.HttpClient { BaseAddress = new Uri("https://openstackid.org/oauth2")};
        //    var request = new HttpRequestMessage(HttpMethod.Post, new Uri(client.BaseAddress, "token"));
        //    //HttpClient client = new HttpClient();
        //    //var request = new HttpRequestMessage(HttpMethod.Post, "https://api.instagram.com/oauth/access_token");
        //    var myParameters = string.Format("client_id={0}&client_secret={1}&grant_type={2}&scope={3}&code={4}",
        //        client_id.UrlEncode(),
        //        client_secret.UrlEncode(),
        //        "client_credentials".UrlEncode(),
        //        "summits".UrlEncode(),
        //        "code".UrlEncode());
            
        //    request.Content = new StringContent(myParameters);

        //    //return client.ExecuteAsync<OAuthResponse>(request);

        //    OAuthResponse response = new OAuthResponse();

        //    client.ExecuteAsync<OAuthResponse>(request);

        //    return response;
        //}
    }

    public class OAuthResponse
    {
        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public UserInfo User { get; set; }
        /// <summary>
        /// Gets or sets the access_ token.
        /// </summary>
        /// <value>
        /// The access_ token.
        /// </value>
        [JsonProperty("Access_Token")]
        public string AccessToken { get; set; }
    }

    public class UserInfo
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public long Id { get; set; }
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        public string Username { get; set; }
        /// <summary>
        /// Gets or sets the full name.
        /// </summary>
        /// <value>
        /// The full name.
        /// </value>
        [JsonProperty("full_name")]
        public string FullName { get; set; }
        /// <summary>
        /// Gets or sets the profile picture.
        /// </summary>
        /// <value>
        /// The profile picture.
        /// </value>
        [JsonProperty("profile_picture")]
        public string ProfilePicture { get; set; }
    }
}
