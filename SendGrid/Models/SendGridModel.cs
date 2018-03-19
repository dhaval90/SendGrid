using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SendGrid.Models
{
    public class SendGridModel
    {
        public string Email { get; set; }
        public string Timestamp { get; set; }
        public int Uid { get; set; }
        public int Id { get; set; }
        public string sendgrid_event_id { get; set; }
        public string smtp_id { get; set; }
        public string sg_message_id { get; set; }        
        public string sendgrid_event { get; set; }
        public string type { get; set; }
        public IList<string> category { get; set; }
        public string reason { get; set; }
        public string status { get; set; }
        public string url { get; set; }
        public string useragent { get; set; }
        public string ip { get; set; }
        public string purchase { get; set; }
        public string response { get; set; }
        public string Event { get; set; }
    }
}