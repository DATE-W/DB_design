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
        public class CustomResponse
        {
            public string Ok { get; set; }
            public object Value { get; set; }
        }
        [HttpPost]
        public async Task<IActionResult> GetUserSecQue([FromBody] UserAccountRequest json)
        {
            // Extract parameters
            string account = json.UserAccount;

            Console.WriteLine("GET GetUserSecQue");
            // Check if the ORACLE connection is successful
            ORACLEconn ORACLEConnectTry = new ORACLEconn();

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
                    return Ok(new CustomResponse { Ok = "no", Value = "WrongAccount" }); // User account not found
                }
                Console.WriteLine(userSecQues.FirstOrDefault());
                return Ok(new CustomResponse { Ok = "yes", Value = userSecQues.FirstOrDefault() });
            }
            catch (Exception)
            {
                Console.WriteLine("UNKNOWN");
                return Ok(new CustomResponse { Ok = "no", Value = "UNKNOWN" }); // Internal server error
                }
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePassword([FromBody] PasswordUpdateRequest json)
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
                return Ok(new CustomResponse { Ok = "no", Value = "Fail_Access" });// Failed to connect to the database
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
                    Console.WriteLine("WrongAccount");
                    return Ok(new CustomResponse { Ok = "no", Value = "WrongAccount" }); // User account not found
                }

                if (user.userSecAns != userSecAns)
                {
                    Console.WriteLine("WrongAns");
                    return Ok(new CustomResponse { Ok = "no", Value = "WrongAns" }); // Security answer does not match
                }

                // Update the user password
                var updateResult = await sqlORM.Updateable<Usr>()
                    .SetColumns(u => new Usr { userPassword = newPassword })
                    .Where(u => u.userAccount == account)
                    .ExecuteCommandAsync();

                if (updateResult > 0)
                {
                    Console.WriteLine("Success");
                    return Ok(new CustomResponse { Ok = "yes", Value = "Success" }); // Password updated successfully
                }
                else
                {
                    Console.WriteLine("updateFailed");
                    return Ok(new CustomResponse { Ok = "no", Value = "updateFailed" }); // Failed to update password
                }
            }
            catch (Exception)
            {
                Console.WriteLine("UNKNOWN");
                return Ok(new CustomResponse { Ok = "no", Value = "UNKNOWN" }); // Internal server error
                }
        }
    }
}
