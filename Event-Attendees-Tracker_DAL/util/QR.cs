using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Event_Attendees_Tracker_DAL.util
{
    public class QR
    {
        /// <summary>
        /// This method will generate a QR Code in jpeg format at location as ~/Resources/TempImages/QR.jpeg
        /// </summary>
        /// <param name="QRString"></param>
        public static void GenerateQR(string QRString)
        {
            QRCodeGenerator qRCodeGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qRCodeGenerator.CreateQrCode(QRString, QRCodeGenerator.ECCLevel.Q);

            System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();

            imgBarCode.Height = 250;
            imgBarCode.Width = 250;
            QRCode qRCode = new QRCode(qrCodeData);

            string data = qRCode.ToString();
            Bitmap bitMap = qRCode.GetGraphic(5);

            bitMap.Save(HttpContext.Current.Server.MapPath("~/Images/qr.jpeg"));
        }
    }
}