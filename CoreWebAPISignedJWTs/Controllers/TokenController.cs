using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace TokenWorthinessEvaluator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {        
        [HttpGet]
        public ActionResult<string> Get(int id)
        {
            return "Hello World!";
        }

        [HttpGet]
        [Route("Parse")]
        public ActionResult<IEnumerable<string>> Parse()
        {
            var token = string.Empty;

            //The token can be passed either via query string or headers
            if (HttpContext.Request.QueryString.Value.Contains("token"))
            {
                token = HttpContext.Request.Query["token"].ToString();
            }
            if (HttpContext.Request.Headers["Authorization"].ToString() != null)
            {
                token = HttpContext.Request.Headers["Authorization"];

                //Remove "Bearer " from string
                token = token.Substring(7);
            }
            //No token equals bad request
            else
            {
                return BadRequest("Missing something?");
            }

            //Let's try to treat it like a token
            var jwtHandler = new JwtSecurityTokenHandler();
            var jwtInput = token;

            //Check if readable token (string is in a JWT format)
            var readableToken = jwtHandler.CanReadToken(jwtInput);

            if (readableToken == true)
            {
                var jwtoken = jwtHandler.ReadJwtToken(jwtInput);

                var header = jwtoken.RawHeader;
                byte[] hData = Convert.FromBase64String(header);
                string hDecodedString = Encoding.UTF8.GetString(hData);

                //.NET needs some padding to Base64 decode
                var payload = jwtoken.RawPayload + "==";
                byte[] pData = Convert.FromBase64String(payload);
                string pDecodedString = Encoding.UTF8.GetString(pData);

                return Content("[" + hDecodedString + "," +
                   pDecodedString + "]",
                   "application/json");
            }
            if (readableToken != true)
            {
                //The token doesn't seem to be in a proper JWT format.
                //Assume it's a combo token and break it apart
                string decodedString = string.Empty;
                try
                {
                    byte[] data = Convert.FromBase64String(token + "=");
                    decodedString = Encoding.UTF8.GetString(data);
                }
                catch (Exception)
                {
                    //If this fails we'll just assume bogus input
                    return BadRequest("Not able to figure out this token");
                }

                //The tokens are separated with a comma
                var tokens = decodedString.Split(',');

                //Sort out the proxy token first
                var proxyToken = tokens[0];
                proxyToken = proxyToken.Substring(16);
                proxyToken = proxyToken.Substring(0, proxyToken.Length - 1);

                var pToken = jwtHandler.ReadJwtToken(proxyToken);

                var ptHeader = pToken.RawHeader;
                byte[] ptHeaderData = Convert.FromBase64String(ptHeader);
                string ptHDecodedString = Encoding.UTF8.GetString(ptHeaderData);

                var ptPayload = pToken.RawPayload;
                //.NET needs extra padding to do Base64 decode
                byte[] ptPayloadData = Convert.FromBase64String(ptPayload + "==");
                string ptTDecodedString = Encoding.UTF8.GetString(ptPayloadData);

                //Figure out the access token
                var accessToken = tokens[1];
                accessToken = accessToken.Substring(16);
                accessToken = accessToken.Substring(0, accessToken.Length - 2);

                var aToken = jwtHandler.ReadJwtToken(accessToken);

                var atHeader = aToken.RawHeader;
                byte[] atHeaderData = Convert.FromBase64String(atHeader);
                string atHDecodedString = Encoding.UTF8.GetString(atHeaderData);

                var atPayload = aToken.RawPayload;
                //.NET needs extra padding to do Base64 decode
                byte[] atPayloadData = Convert.FromBase64String(atPayload + "==");
                string atTDecodedString = Encoding.UTF8.GetString(atPayloadData);

                return Content("[" + ptHDecodedString + "," +
                    ptTDecodedString + "," +
                    atHDecodedString + "," +
                    atTDecodedString + "]",
                    "application/json");
            }

            return new string[] { "How did you end up here?" };
        }
        
        [HttpGet]
        [Authorize(Policy = "Certificate")]
        [Route("Validate")]
        public ActionResult<IEnumerable<string>> Validate()
        {
            var token = "{";
            foreach (var claim in User.Claims)
            {
                //Datetimes are already escaped
                if (claim.Type.ToString().Contains("time"))
                {
                    token += $"\"{claim.Type}\":{claim.Value},";
                }
                else
                {
                    //Let's not care about the authentication method here
                    //since that requires building an array to pass as valid json
                    if (claim.Type == ClaimTypes.AuthenticationMethod)
                    { }
                    else
                    {
                        token += $"\"{claim.Type}\":\"{claim.Value}\",";
                    }
                }
            }
            //Remove the surplus comma
            token = token.Substring(0, token.Length - 1);
            token += "}";

            return Content(token, "application/json");
        }

    }
}
