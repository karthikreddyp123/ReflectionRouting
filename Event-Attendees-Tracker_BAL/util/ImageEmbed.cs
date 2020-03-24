using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace Event_Attendees_Tracker_BAL.util
{
    class ImageEmbed
    {
        public static AlternateView imageEmbedder(string SourcePath, string MailBody, string SourceId)
        {
            var htmlView = AlternateView.CreateAlternateViewFromString(MailBody, null, "text/html");
            var resource = new LinkedResource(SourcePath) { ContentId = SourceId };
            resource.TransferEncoding = TransferEncoding.Base64;
            htmlView.LinkedResources.Add(resource);
            return htmlView;
        }
    }
}
