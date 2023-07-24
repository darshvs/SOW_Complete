using CandidateSoW.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CandidateSoW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private IConfiguration configuration;
        public SecurityController(IConfiguration iConfig)
        {
            configuration = iConfig;
        }

        string msg = string.Empty;

        [HttpGet]
        public List<SecurityQuestionModel> Get()
        {
            string dbConn = configuration.GetSection("ConnectionStrings").GetSection("DbConnection").Value;
            Db dop = new Db(dbConn);
            SecurityQuestionModel st = new SecurityQuestionModel();
            st.Type = "get";
            DataSet ds = dop.GetSecurityQuestions(st, msg);
            List<SecurityQuestionModel> list = new List<SecurityQuestionModel>();
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        list.Add(new SecurityQuestionModel
                        {
                            QuestionId = Convert.ToInt32(dr["QuestionId"]),
                            Question = dr["Question"].ToString(),
                        });
                    }
                }
            }
            return list;
        }

        [HttpGet("{id}")]
        public List<SecurityQuestionModel> Get(int id)
        {
            string dbConn = configuration.GetSection("ConnectionStrings").GetSection("DbConnection").Value;
            Db dop = new Db(dbConn);
            SecurityQuestionModel st = new SecurityQuestionModel();
            st.Type = "getid";
            st.QuestionId = id;
            DataSet ds = dop.GetSecurityQuestions(st, msg);
            List<SecurityQuestionModel> list = new List<SecurityQuestionModel>();
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        list.Add(new SecurityQuestionModel
                        {
                            QuestionId = Convert.ToInt32(dr["QuestionId"]),
                            Question = dr["Question"].ToString(),
                        });
                    }
                }
            }
            return list;
        }

        [HttpPost]
        public string Post([FromBody] SecurityAnswerModel st)
        {
            string dbConn = configuration.GetSection("ConnectionStrings").GetSection("DbConnection").Value;
            Db dop = new Db(dbConn);
            string msg = string.Empty;
            try
            {
                msg = dop.SecurityAnswertable(st);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            return JsonConvert.SerializeObject(msg);
        }

        [HttpPut("{id}")]
        public string Put(int id, [FromBody] SecurityAnswerModel st)
        {
            string dbConn = configuration.GetSection("ConnectionStrings").GetSection("DbConnection").Value;
            Db dop = new Db(dbConn);
            string msg = string.Empty;
            try
            {
                st.Type = "update";
                st.AnswerId = id;
                msg = dop.SecurityAnswertable(st);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            return JsonConvert.SerializeObject(msg);
        }

        [HttpGet]
        [Route("ValidateSecurityQnA")]
        public string ValidateSecuityQnA(string LoginName, int QuestionId, string Answer)
        {
            string dbConn = configuration.GetSection("ConnectionStrings").GetSection("DbConnection").Value;
            Db dop = new Db(dbConn);
            string msg = string.Empty;
            try
            {
                msg = dop.ValidateSecurityQnA(LoginName, QuestionId, Answer);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return JsonConvert.SerializeObject(msg);
        }

        [HttpGet]
        [Route("SecurityAnswer")]
        public List<SecurityAnswerModel> GetAnswer(int loginid)
        {
            string dbConn = configuration.GetSection("ConnectionStrings").GetSection("DbConnection").Value;
            Db dop = new Db(dbConn);
            SecurityAnswerModel st = new SecurityAnswerModel();
            st.Type = "get";
            st.LoginId = loginid;
            DataSet ds = dop.GetSecurityAnswers(st, msg);
            List<SecurityAnswerModel> list = new List<SecurityAnswerModel>();
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        list.Add(new SecurityAnswerModel
                        {
                            AnswerId = Convert.ToInt32(dr["AnswerId"]),
                            LoginId = Convert.ToInt32(dr["LoginId"]),
                            QuestionId = Convert.ToInt32(dr["QuestionId"]),
                            Answer = dr["Answer"].ToString()
                        });
                    }
                }
            }
            return list;
        }
    }
}
