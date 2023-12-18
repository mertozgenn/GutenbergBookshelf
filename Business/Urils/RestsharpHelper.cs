using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Urils
{
    public static class RestsharpHelper
    {
        public static T Get<T>(string baseUrl, string apiEndPoint, string? token = null)
        {
            var client = new RestClient(new RestClientOptions(baseUrl) { FollowRedirects = true });
            if (token != null)
            {
                client.Authenticator = new JwtAuthenticator(token);
            }
            var request = new RestRequest(apiEndPoint, Method.Get);
            var restResponse = client.Execute(request);
            if (restResponse.StatusCode == System.Net.HttpStatusCode.Found)
            {
                return Get<T>(restResponse.Headers.FirstOrDefault(h => h.Name == "Location").Value.ToString(), "", token);
            }
            if (!restResponse.IsSuccessful)
            {
                throw new Exception(restResponse.StatusCode + " - " + restResponse.ErrorMessage + " - " + restResponse.ErrorException.Message);
            }
            if (typeof(T) == typeof(string))
            {
                return (T)Convert.ChangeType(restResponse.Content, typeof(T));
            }
            var response = JsonConvert.DeserializeObject<T>(restResponse.Content);
            return response;
        }

        public static T Post<T>(string baseUrl, string apiEndPoint, object objectToPost, string token = null)
        {
            var client = new RestClient(baseUrl);
            if (token != null)
            {
                client.Authenticator = new JwtAuthenticator(token);
            }
            var request = new RestRequest(apiEndPoint, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddBody(objectToPost);
            var restResponse = client.Execute(request);
            if (!restResponse.IsSuccessful && restResponse.StatusCode != System.Net.HttpStatusCode.BadRequest)
            {
                throw new Exception(restResponse.StatusCode + " - " + restResponse.ErrorMessage + " - " + restResponse.ErrorException.Message);
            }
            var response = JsonConvert.DeserializeObject<T>(restResponse.Content);
            return response;
        }
    }
}
