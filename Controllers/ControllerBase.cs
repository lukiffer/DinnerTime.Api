using Microsoft.AspNetCore.Mvc;
using DinnerTime.Api.Services;

namespace DinnerTime.Api.Controllers
{
    public class ControllerBase : Controller
    {
        protected readonly FacebookIdentityService _identityService;

        public ControllerBase()
        {
            _identityService = new FacebookIdentityService();
        }

        protected string AccessToken
        {
            get
            {
                try
                {
                    return Request.Headers["Authorization"].ToString().Replace("FB-GRAPH ", "");    
                }
                catch
                {
                    return null;
                }
            }
        }

        protected string GetUserId()
        {
            try
            {
                return _identityService.GetUserId(AccessToken);
            }
            catch
            {
                return null;
            }
        }
    }
}
