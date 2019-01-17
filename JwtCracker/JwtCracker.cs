using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Windows.Forms;

namespace JwtCracker
{
    public partial class frmJwtCracker : Form
    {
        public frmJwtCracker()
        {
            InitializeComponent();
        }

        private void btnDecodeJwt_Click(object sender, EventArgs e)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            var jwtInput = txtJwtIn.Text;

            //Check if readable token (string is in a JWT format)
            var readableToken = jwtHandler.CanReadToken(jwtInput);

            if (readableToken != true)
            {
                txtJwtOut.Text = "The token doesn't seem to be in a proper JWT format.";
            }
            if (readableToken == true)
            {
                var token = jwtHandler.ReadJwtToken(jwtInput);

                //Extract the headers of the JWT
                var headers = token.Header;
                var jwtHeader = "{";
                foreach (var h in headers)
                {
                    jwtHeader += '"' + h.Key + "\":\"" + h.Value + "\",";
                }
                jwtHeader += "}";
                txtJwtOut.Text = "Header:\r\n" + JToken.Parse(jwtHeader).ToString(Formatting.Indented);

                //Extract the payload of the JWT
                var claims = token.Claims;
                var jwtPayload = "{";
                foreach (Claim c in claims)
                {
                    if (c.Value.StartsWith("{"))
                        jwtPayload += '"' + c.Type + "\":" + c.Value + ",";
                    else
                        jwtPayload += '"' + c.Type + "\":\"" + c.Value + "\",";
                }
                jwtPayload += "}";
                txtJwtOut.Text += "\r\nPayload:\r\n" + JToken.Parse(jwtPayload).ToString(Formatting.Indented);
            }
        }
    }
}
