using Newtonsoft.Json;
using SendGrid.Database;
using SendGrid.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace SendGrid.Controllers
{
    public class SendGridAPIController : ApiController
    {
       
        //Changed SendGridEntites with DemoSendGridEntities        
        DemoSendGridEntities _entity = new DemoSendGridEntities();

        [HttpGet]
        [Route("api/SendGrid")]
        public void SendGrid()
        {
            try
            {
                ///Uncomment Below line when using mobile API 
                //System.IO.StreamReader reader = new System.IO.StreamReader(HttpContext.Current.Request.InputStream);

                //Test Purpose 
                System.IO.StreamReader reader = new System.IO.StreamReader(HttpContext.Current.Server.MapPath("~/test.json"));

                string rawSendGridJSON = reader.ReadToEnd();
                List<SendGridModel> sendGridEvents = JsonConvert.DeserializeObject<List<SendGridModel>>(rawSendGridJSON);
                string count = sendGridEvents.Count.ToString();
                System.Diagnostics.Trace.TraceError(rawSendGridJSON);

                string path = HttpContext.Current.Server.MapPath("~/Document/SendGrid_" + DateTime.Now.Day + "_" + DateTime.Now.Month + "_" + DateTime.Now.Year + ".json");
                if (!File.Exists(path))
                {
                    using (StreamWriter sw = File.CreateText(path))
                    {
                        sw.WriteLine(rawSendGridJSON);
                    }
                }
                else
                {
                    using (StreamWriter sw = File.AppendText(path))
                    {
                        sw.WriteLine(rawSendGridJSON);
                    }
                }
                foreach (SendGridModel sendGridEvent in sendGridEvents)
                {
                    EmailDetail emailDetail = new EmailDetail();
                    emailDetail.category = sendGridEvent.category[0];
                    emailDetail.Email = sendGridEvent.Email;
                    //Changed from @event to C_event 
                    emailDetail.C_event = sendGridEvent.Event;
                    emailDetail.IP = sendGridEvent.ip;
                    emailDetail.Reason = sendGridEvent.reason;
                    emailDetail.response = sendGridEvent.response;
                    emailDetail.smtp_id = sendGridEvent.smtp_id;
                    emailDetail.Status = sendGridEvent.status;
                    emailDetail.TimeStamp = sendGridEvent.Timestamp;
                    emailDetail.Type = sendGridEvent.type;
                    emailDetail.URL = sendGridEvent.url;

                    _entity.EmailDetails.Add(emailDetail);
                    _entity.SaveChanges();
                    //System.Diagnostics.Trace.TraceError(sendGridEvent.Email);
                }
            }
            catch (Exception ex)
            {
                string path = HttpContext.Current.Server.MapPath("~/Document/Error_" + DateTime.Now.Day + "_" + DateTime.Now.Month + "_" + DateTime.Now.Year + ".text"); // @"c:\temp\MyTest.txt";
                if (!File.Exists(path))
                {
                    using (StreamWriter sw = File.CreateText(path))
                    {
                        sw.WriteLine(ex);
                    }
                }
                else
                {
                    using (StreamWriter sw = File.AppendText(path))
                    {   
                        sw.WriteLine(ex);
                    }
                }
            }
        }
    }
}
