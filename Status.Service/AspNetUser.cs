using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace Status.Service
{
    public class AspNetUser
    {
        private readonly IHttpContextAccessor _accessor;

        public AspNetUser(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }


        //public int Id => Convert.ToInt32(ClaimsPrincipal.Current.FindFirst("Id").Value); //       ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes. FindFirst( "Id").Value);
        //public int Id => Convert.ToInt32(((ClaimsIdentity)_accessor.HttpContext.User.Identity).FindFirst("Id").Value);
        //public int Id => int.Parse(_accessor.HttpContext.User.FindFirst("id").Value);
        //public int Id => Convert.ToInt32(GetClaimsIdentity().Where(c => c.Type == "Id").FirstOrDefault());
        //        public Guid Id => Guid.Parse(_accessor.HttpContext.User.FindFirst("id").Value);
        //        public Guid Id => Guid.Parse(((ClaimsIdentity)_accessor.HttpContext.User.Identity).FindFirst(ClaimTypes.Sid).ToString());
        //public Guid Id => Guid.Parse(GetClaimsIdentity().Where(c => c.Type == "Sid").FirstOrDefault().ToString());

        public Guid Id => Guid.Parse(_accessor.HttpContext.User.FindFirst(ClaimTypes.Sid).Value);
        public string Name => _accessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value; // _accessor.HttpContext.User.Identity.Name;
        public string Email => _accessor.HttpContext.User.FindFirst(ClaimTypes.Email).Value; // _accessor.HttpContext.User.Identity.Name;

        public IEnumerable<Claim> GetClaimsIdentity()
        {
            return _accessor.HttpContext.User.Claims;
        }

        public bool IsAuthenticated()
        {
            return _accessor.HttpContext.User.Identity.IsAuthenticated;
        }
        
        public async Task Authenticate(Guid id, string Email, string Nome = "")
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Sid, id.ToString()),
                new Claim(ClaimTypes.Name, Nome),
                new Claim(ClaimTypes.Email, Email)
            };
            var identity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme,
                "name", ""
            );
            var principal = new ClaimsPrincipal(identity);
            await _accessor.HttpContext.SignInAsync(principal, new AuthenticationProperties()
            {
                IsPersistent = true
            });

            return;
        }

        /// <summary>  
        /// Get the cookie  
        /// </summary>  
        /// <param name="key">Key </param>  
        /// <returns>string value</returns> 
        public string GetCookie(string key)
        {
            //return Request.Cookies[Key];
            return _accessor.HttpContext.Request.Cookies[key];
        }

        /// <summary>  
        /// set the cookie  
        /// </summary>  
        /// <param name="key">key (unique indentifier)</param>  
        /// <param name="value">value to store in cookie object</param>  
        /// <param name="expireTime">expiration time</param>  
        public void SetCookie(string key, string value, int? expireTimeMinutes = 10)
        {
            CookieOptions option = new CookieOptions();
            if (expireTimeMinutes.HasValue)
                option.Expires = DateTime.Now.AddMinutes(expireTimeMinutes.Value);
            else
                option.Expires = DateTime.Now.AddMinutes(10);
            _accessor.HttpContext.Response.Cookies.Append(key, value, option);
        }

        /// <summary>  
        /// Delete the key  
        /// </summary>  
        /// <param name="key">Key</param>  
        public void RemoveCookie(string key)
        {
            _accessor.HttpContext.Response.Cookies.Delete(key);
        }
    }
}

