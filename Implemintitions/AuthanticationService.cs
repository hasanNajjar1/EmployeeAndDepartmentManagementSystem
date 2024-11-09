using EmployeeAndDepartmentManagementSystem.Context;
using EmployeeAndDepartmentManagementSystem.DTOs.Authantication.Request;
using EmployeeAndDepartmentManagementSystem.Helper;
using EmployeeAndDepartmentManagementSystem.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace EmployeeAndDepartmentManagementSystem.Implementation
{
    public class AuthanticationService : IAuthanticationService
    {
        private readonly EaDMContext _context;
        public AuthanticationService(EaDMContext context)
        {
            _context = context;
        }

        public async Task<string> Login(LoginDTO input)
        {
            if (input != null)
            {
                if(!string.IsNullOrEmpty(input.Email) && !string.IsNullOrEmpty(input.Password))
                {
                    input.Email = EncryptionHelper.GenerateSHA384String(input.Email);
                    input.Password = EncryptionHelper.GenerateSHA384String(input.Password);
                    var authUser= await (from p in _context.Employees 
                                         join li in _context.LookupItems
                                         on p.PositionTypeId equals li.Id
                                  where p.Email == input.Email && p.Password == input.Password
                                  select new
                                  {
                                      EmployeeId = p.Id.ToString(),
                                      Role = li.Name.ToString(),
                                  }).FirstOrDefaultAsync();
                    return authUser != null ? await TokenHelper.GenerateToken(authUser.EmployeeId,authUser.Role) : "Authantication Failed";
                    //if ()
                    //    return ;
                    //else return false;
                }
                else
                {
                    throw new Exception("Email and Password Are Required");
                }
            }
            else
            {

                throw new Exception("Email and Password Are Required");

            }
        }

        
    }
}
