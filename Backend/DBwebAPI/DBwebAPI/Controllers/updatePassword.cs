using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using DBwebAPI.Models;

namespace DBwebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ForgetPasswordController : ControllerBase
    {
        public class UserAccountRequest // Make the class public
        {
            public string UserAccount { get; set; }
        }
        public class PasswordUpdateRequest
        {
            public string UserAccount { get; set; }
            public string UserSecAns { get; set; }
            public string NewPassword { get; set; }
        }
        [HttpPost]
        public async Task<string> GetUserSecQue([FromBody] UserAccountRequest json)
        {
            // Extract parameters
            string account = json.UserAccount;

            Console.WriteLine("GET GetUserSecQue");
            // Check if the ORACLE connection is successful
            ORACLEconn ORACLEConnectTry = new ORACLEconn();
            if (!ORACLEConnectTry.getConn())
            {
                Console.WriteLine("Fail_Access");
                return "Fail_Access"; // Failed to connect to the database
            }

            try
            {
                SqlSugarClient sqlORM = ORACLEConnectTry.sqlORM;
                // Query the database to find the userSecQue based on the userAccount
                List<string> userSecQues = await sqlORM.Queryable<Usr>()
                    .Where(u => u.userAccount == account)
                    .Select(u => u.userSecQue)
                    .ToListAsync();

                if (userSecQues.Count == 0)
                {
                    Console.WriteLine("WrongAccount");
                    return "WrongAccount"; // User account not found
                }
                Console.WriteLine(userSecQues.FirstOrDefault());
                return userSecQues.FirstOrDefault();
            }
            catch (Exception)
            {
                Console.WriteLine("UNKNOWN");
                return "UNKNOWN"; // Internal server error
            }
        }

        [HttpPost]
        public async Task<string> UpdatePassword([FromBody] PasswordUpdateRequest json)
        {
            // Extract parameters
            string account = json.UserAccount;
            string userSecAns = json.UserSecAns;
            string newPassword = json.NewPassword;
            Console.WriteLine("UpdatePassword");
            // Check if the ORACLE connection is successful
            ORACLEconn ORACLEConnectTry = new ORACLEconn();
            if (!ORACLEConnectTry.getConn())
            {
                Console.WriteLine("Fail_Access");
                return "Fail_Access"; // Failed to connect to the database
            }

            try
            {
                SqlSugarClient sqlORM = ORACLEConnectTry.sqlORM;
                // Query the database to find the user account and security answer
                var user = await sqlORM.Queryable<Usr>()
                    .Where(u => u.userAccount == account)
                    .Select(u => new { u.userSecAns, u.userPassword })
                    .FirstAsync();

                if (user == null)
                {
                    Console.WriteLine("wrongAccount");
                    return "wrongAccount"; // User account not found
                }

                if (user.userSecAns != userSecAns)
                {
                    Console.WriteLine("wrongAns");
                    return "wrongAns"; // Security answer does not match
                }

                // Update the user password
                var updateResult = await sqlORM.Updateable<Usr>()
                    .SetColumns(u => new Usr { userPassword = newPassword })
                    .Where(u => u.userAccount == account)
                    .ExecuteCommandAsync();

                if (updateResult > 0)
                {
                    Console.WriteLine("Success");
                    return "Success"; // Password updated successfully
                }
                else
                {
                    Console.WriteLine("updateFailed");
                    return "updateFailed"; // Failed to update password
                }
            }
            catch (Exception)
            {
                Console.WriteLine("UNKNOWN");
                return "UNKNOWN"; // Internal server error
            }
        }
    }
}
