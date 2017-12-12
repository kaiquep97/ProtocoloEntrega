using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using iTextSharp.text;
using iTextSharp.text.pdf;
using ProtocoloDeEntrega.Droid;
using ProtocoloDeEntrega.Interface;
using ProtocoloDeEntrega.Model;

[assembly:Xamarin.Forms.Dependency(typeof(PDF_Android))]
namespace ProtocoloDeEntrega.Droid
{
    public class PDF_Android : ICreatePDF
    {
        public bool CreatePDF(Protocolo protocol)
        {
            try
            {
                string name = "ENTREGATESTE.pdf";
                var caminho = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.Path, name);

                using (var fs = new FileStream(caminho, FileMode.Create))
                {

                    var doc = new Document(PageSize.A4, 25, 25, 30, 30);
                    PdfWriter writer = PdfWriter.GetInstance(doc, fs);

                    doc.Open();

                    doc.Add(new Paragraph($"DATA: {protocol.Data}"));
                    doc.Add(new Paragraph($"HORA: {protocol.Hora}"));
                    doc.Add(new Paragraph($"LOCAL: {protocol.Local}"));
                    doc.Add(new Paragraph($"Nº ENTREGA: {protocol.Entrega}"));


                    Image img = Image.GetInstance(protocol.Assinatura);
                    doc.Add(img);
                    doc.Close();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }


        }
    }
}