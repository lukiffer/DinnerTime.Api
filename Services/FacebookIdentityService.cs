using System;
using DinnerTime.Api.Models;

namespace DinnerTime.Api.Services
{
    public class FacebookIdentityService
    {
        readonly HttpClient _httpClient;

        public FacebookIdentityService()
        {
            _httpClient = new HttpClient();
        }

        public string GetUserId(string accessToken)
        {
            var response = _httpClient.Get<FacebookMeResponse>(
                String.Format("https://graph.facebook.com/v2.8/me?access_token={0}", accessToken));

            if (response.Error != null)
            {
                return null;
            }

            return response.Id;
        } 

        public bool IsAdmin(string userId)
        {
            // TODO: Query database to get list of admins.
            return userId == "10154602372316805";
        }
    }
}
