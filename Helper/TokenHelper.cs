using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmployeeAndDepartmentManagementSystem.Helper
{
    public static class TokenHelper
    {
        public async static Task<string> GenerateToken(string personId, string role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes("LongSecrectStringForModulekodestartppopopopsdfjnshbvhueFGDKJSFBYJDSAGVYKDSGKFUYDGASYGFskc vhHJVCBYHVSKDGHASVBCL");
            var tokenDescriptior = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                        new Claim("PersonId",personId),
                        new Claim(ClaimTypes.Role,role)
                }),
                Expires = DateTime.Now.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey)
                , SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptior);
            return tokenHandler.WriteToken(token);
        }
        public async static Task<bool> ValidateToken(string tokenString, string role)
        {
            String toke = "Bearer " + tokenString;
            var jwtEncodedString = toke.Substring(7);
            var token = new JwtSecurityToken(jwtEncodedString: jwtEncodedString);
            var roleString = (token.Claims.First(c => c.Type == "role").Value.ToString());
            if (token.ValidTo > DateTime.UtcNow && roleString.Equals(role,StringComparison.OrdinalIgnoreCase))
            {

                return true;
            }
            return false;
        }
    }
}
