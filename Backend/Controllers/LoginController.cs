using CandidateSoW.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CandidateSoW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration configuration;
        public LoginController(IConfiguration iConfig)
        {
            configuration = iConfig;
        }
        // GET: api/<LoginController>
        [HttpGet]
        public string LoginGet(string EmailId, string LoginPassword)
        {
            string dbConn = configuration.GetSection("ConnectionStrings").GetSection("DbConnection").Value;
            Db dop = new Db(dbConn);
            try
            {
                string msg = string.Empty;
                LoginModel ln = new LoginModel();
                DataSet ds = dop.loginget(EmailId, LoginPassword);

                if (ds.Tables[0].Rows.Count>0)
                {
                    ln.LoginName = ds.Tables[0].Rows[0]["LoginName"].ToString();
                    ln.EmailId = ds.Tables[0].Rows[0]["EmailId"].ToString();
                    ln.RoleName = ds.Tables[0].Rows[0]["RoleName"].ToString();
                    ln.ScreenNames = ds.Tables[0].Rows[0]["ScreenNames"].ToString();
                    ln.Status = ds.Tables[0].Rows[0]["Status"].ToString();
                    ln.PermissionName = ds.Tables[0].Rows[0]["PermissionName"].ToString();
                    ln.FailureAttempts = (int)ds.Tables[0].Rows[0]["FailureAttempts"];
                    ln.Islock = (bool)ds.Tables[0].Rows[0]["Islock"];
                    return JsonConvert.SerializeObject(ln);
                }
                else
                {
                    msg = "error";
                    return msg;
                }

            }
            catch (Exception E)
            {
                return "Entered credentials is invalid! Please check";
            }
        }
        [HttpPut("{EmailId}")]
        public string Put(string EmailId, LoginModel lm)
        {
            string dbConn = configuration.GetSection("ConnectionStrings").GetSection("DbConnection").Value;
            Db dop = new Db(dbConn);
            string msg = string.Empty;
            try
            {
                msg = dop.updateLoginTable(EmailId, lm);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return msg;
        }

    }

}
