using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web;
using System.Drawing.Text;
using System.Drawing.Printing;
using System.IO;


namespace IHF.BusinessLayer.Util
{
    public class PrintUtil
    {
        private static System.Drawing.Image dirfilename = null;
        private static Int32 canvaswidth = 0;
        private static Int32 canvasheight = 0;
        private string labelfont = "Arial";

        public void generate_trbar(Int32 I_trolley_id, string I_label, string I_barcode, string I_url)
        {
            string path = @"C:\trolley\";
            string filename = "trolley_barcode_" + I_trolley_id + ".jpg";
            string dirpath = path + filename;

            string TypeFaceName = "Code39HalfInch";//"code128Wide";
            string Fontttf = "Code39HalfInch.ttf"; //"Code128W.ttf";

            //The format of the image file
            ImageFormat format = ImageFormat.Png;


            PrivateFontCollection fnts = new PrivateFontCollection();
            fnts.AddFontFile(Fontttf);
            FontFamily fntfam = new FontFamily(TypeFaceName, fnts);
            Font fnt = new Font(fntfam, 48);


            string barCode = I_barcode; //"TL00000000015STD";
            string label = I_label;
            Int64 barwidth = barCode.Length * 50;

            string I_longdesc = "";
            string barcodefont = fntfam.Name.ToString();
            Int32 labelsize = 60;
            Int32 barcodetxtsize = 12;
            Int32 barcodesize = 48;
            Int32 descsize = 60;
            canvaswidth = 600;
            canvasheight = 400;
            float labelx = 40f;
            float labely = 25f;
            float barcodex = 55f;
            float barcodey = 170f;
            float barcodetxtx = 210f;
            float barcodetxty = 270f;
            float descx = 0f;
            float descy = 0f;

            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);

            }

            dirpath = createImage(labelsize, barcodetxtsize,
                                  barcodesize, I_label,
                                  I_barcode, fnt,
                                  labelx, labely,
                                  barcodex, barcodey,
                                  barcodetxtx, barcodetxty,
                                  I_longdesc,
                                  descx, descy,
                                  descsize, dirpath);


            

            // printing
            dirfilename = System.Drawing.Image.FromFile(dirpath);
            PrintDocument pd = new PrintDocument();
            pd.PrinterSettings.PrinterName = I_url;
            pd.DefaultPageSettings.Landscape = true;
            pd.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);
            //pd.PrinterSettings.Copies = 4;
            
            pd.Print();


        }

        public void generate_trlocbar(Int32 I_trolleyloc_id, string I_label, string I_barcode)
        {
            string path = @"C:\trolleylocation\";
            string filename = "trolleyloc_barcode_" + I_trolleyloc_id + ".jpg";
            string dirpath = path + filename;


            string TypeFaceName = "Code39Wide";//"code128Wide";
            string Fontttf = "Code39Wide.ttf"; //"Code128W.ttf";

            //The format of the image file
            ImageFormat format = ImageFormat.Png;


            PrivateFontCollection fnts = new PrivateFontCollection();
            fnts.AddFontFile(Fontttf);
            FontFamily fntfam = new FontFamily(TypeFaceName, fnts);
            Font fnt = new Font(fntfam, 16);


            string barCode = I_barcode;
            string label = I_label;
            Int64 barwidth = barCode.Length * 40;

            string I_longdesc = "";
            string barcodefont = fntfam.Name.ToString();
            Int32 labelsize = 36;
            Int32 barcodetxtsize = 8;
            Int32 barcodesize = 16;
            Int32 descsize = 36; // no desc required;
            canvaswidth = 350;
            canvasheight = 80;
            //Int32 rectangleht = 100;
            float labelx = 2f;
            float labely = 10f;
            float barcodex = 140f;
            float barcodey = 10f;
            float barcodetxtx = 165f;
            float barcodetxty = 40f;
            float descx = 0f;
            float descy = 0f;

            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);

            }


            dirpath = createImage(labelsize, barcodetxtsize,
                                  barcodesize, I_label,
                                  I_barcode, fnt,
                                  labelx, labely,
                                  barcodex, barcodey,
                                  barcodetxtx, barcodetxty,
                                  I_longdesc,
                                  descx, descy,
                                  descsize, dirpath);

            // printing
            dirfilename = System.Drawing.Image.FromFile(dirpath);
            PrintDocument pd = new PrintDocument();
            pd.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);
            pd.Print();


        }

        public void generate_chutebar(Int32 I_chute_id, string I_barcode, string I_chute_longdesc)
        {
            string path = @"C:\chute\";
            string filename = "chute_barcode_" + I_chute_id + ".jpg";
            string dirpath = path + filename;

            string TypeFaceName = "Code39HalfInch";//"code128Wide";
            string Fontttf = "Code39HalfInch.ttf"; //"Code128W.ttf";

            //The format of the image file
            ImageFormat format = ImageFormat.Png;


            PrivateFontCollection fnts = new PrivateFontCollection();
            fnts.AddFontFile(Fontttf);
            FontFamily fntfam = new FontFamily(TypeFaceName, fnts);
            Font fnt = new Font(fntfam, 30);


            string barCode = I_barcode;
            string label = I_chute_id.ToString();
            Int64 barwidth = barCode.Length * 40;


            string barcodefont = fntfam.Name.ToString();
            Int32 labelsize = 72;
            Int32 barcodetxtsize = 12;
            Int32 barcodesize = 30;
            Int32 descsize = 40;
            canvaswidth = 400;
            canvasheight = 600;
            //Int32 rectangleht = 100;
            float labelx = 120f;
            float labely = 15f;
            float barcodex = 45f;
            float barcodey = 180f;
            float barcodetxtx = 110f;
            float barcodetxty = 250f;
            float descx = 30f;
            float descy = 300f;

            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }
            

            dirpath = createImage(labelsize, barcodetxtsize,
                                  barcodesize, label,
                                  I_barcode, fnt,
                                  labelx, labely,
                                  barcodex, barcodey,
                                  barcodetxtx, barcodetxty,
                                  I_chute_longdesc,
                                  descx, descy,
                                  descsize, dirpath);

            // printing
            dirfilename = System.Drawing.Image.FromFile(dirpath);
            PrintDocument pd = new PrintDocument();
            pd.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);
            pd.PrinterSettings.Copies = 1;
            pd.Print();


        }

        public void generate_overflowtotebar(Int32 I_overflow_id, string I_label, string I_barcode)
        {
            string path = @"C:\OverflowTote\";
            string filename = "overflowtote_barcode_" + I_overflow_id + ".jpg";
            string dirpath = path + filename;

            string TypeFaceName = "Code39Wide";//"code128Wide";
            string Fontttf = "Code39Wide.ttf"; //"Code128W.ttf";

            //The format of the image file
            ImageFormat format = ImageFormat.Png;


            PrivateFontCollection fnts = new PrivateFontCollection();
            fnts.AddFontFile(Fontttf);
            FontFamily fntfam = new FontFamily(TypeFaceName, fnts);
            Font fnt = new Font(fntfam, 18);


            string barCode = I_barcode;
            string label = I_label;
            Int64 barwidth = barCode.Length * 40;

            string I_longdesc = "";
            string barcodefont = fntfam.Name.ToString();
            Int32 labelsize = 36;
            Int32 barcodetxtsize = 8;
            Int32 barcodesize = 20;
            Int32 descsize = 36; // no desc required;
            canvaswidth = 350;
            canvasheight = 80;
            //Int32 rectangleht = 100;
            float labelx = 2f;
            float labely = 10f;
            float barcodex = 120f;
            float barcodey = 10f;
            float barcodetxtx = 165f;
            float barcodetxty = 40f;
            float descx = 0f;
            float descy = 0f;

            

            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);

            }


            dirpath = createImage(labelsize, barcodetxtsize,
                                  barcodesize, I_label,
                                  I_barcode, fnt,
                                  labelx, labely,
                                  barcodex, barcodey,
                                  barcodetxtx, barcodetxty,
                                  I_longdesc,
                                  descx, descy,
                                  descsize, dirpath);

            // printing
            dirfilename = System.Drawing.Image.FromFile(dirpath);
            PrintDocument pd = new PrintDocument();
            pd.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);
            pd.Print();


        }

        public void generate_workstationbar(Int32 I_workstation_id, string I_label, string I_barcode, string I_long_desc)
        {
            string path = @"C:\workstation\";
            string filename = "workstation_barcode_" + I_workstation_id + ".jpg";
            string dirpath = path + filename;


            string TypeFaceName = "Code39HalfInch";//"code128Wide";
            string Fontttf = "Code39HalfInch.ttf"; //"Code128W.ttf";

            //The format of the image file
            ImageFormat format = ImageFormat.Png;


            PrivateFontCollection fnts = new PrivateFontCollection();
            fnts.AddFontFile(Fontttf);
            FontFamily fntfam = new FontFamily(TypeFaceName, fnts);
            Font fnt = new Font(fntfam, 14);


            string barCode = I_barcode;
            string label = I_label;
            Int64 barwidth = barCode.Length * 40;

            string I_longdesc = I_long_desc;
            string barcodefont = fntfam.Name.ToString();
            Int32 labelsize = 24;
            Int32 barcodetxtsize = 8;
            Int32 barcodesize = 14;
            Int32 descsize = 12; // no desc required;
            canvaswidth = 140;
            canvasheight = 180;
            //Int32 rectangleht = 100;
            float labelx = 10f;
            float labely = 10f;
            float barcodex = 5f;
            float barcodey = 70f;
            float barcodetxtx = 20f;
            float barcodetxty = 100f;
            float descx = 5f;
            float descy = 140f;

            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);

            }


            dirpath = createImage(labelsize, barcodetxtsize,
                                  barcodesize, I_label,
                                  I_barcode, fnt,
                                  labelx, labely,
                                  barcodex, barcodey,
                                  barcodetxtx, barcodetxty,
                                  I_longdesc,
                                  descx, descy,
                                  descsize, dirpath);

            // printing
            dirfilename = System.Drawing.Image.FromFile(dirpath);
            PrintDocument pd = new PrintDocument();
            pd.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);
            pd.Print();


        }

        public void generate_devicebar(Int32 I_trolleyloc_id, string I_label, string I_barcode)
        {
            string path = @"C:\device\";
            string filename = "device_barcode_" + I_trolleyloc_id + ".jpg";
            string dirpath = path + filename;

            string TypeFaceName = "Code39HalfInch";//"code128Wide";
            string Fontttf = "Code39HalfInch.ttf"; //"Code128W.ttf";

            //The format of the image file
            ImageFormat format = ImageFormat.Png;


            PrivateFontCollection fnts = new PrivateFontCollection();
            fnts.AddFontFile(Fontttf);
            FontFamily fntfam = new FontFamily(TypeFaceName, fnts);
            Font fnt = new Font(fntfam, 14);


            string barCode = I_barcode;
            string label = I_label;
            Int64 barwidth = barCode.Length * 40;

            string I_longdesc = "";
            string barcodefont = fntfam.Name.ToString();
            Int32 labelsize = 24;
            Int32 barcodetxtsize = 8;
            Int32 barcodesize = 14;
            Int32 descsize = 12; // no desc required;
            canvaswidth = 140;
            canvasheight = 180;
            //Int32 rectangleht = 100;
            float labelx = 10f;
            float labely = 10f;
            float barcodex = 5f;
            float barcodey = 70f;
            float barcodetxtx = 20f;
            float barcodetxty = 100f;
            float descx = 0f;
            float descy = 0f;

            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);

            }


            dirpath = createImage(labelsize, barcodetxtsize,
                                  barcodesize, I_label,
                                  I_barcode, fnt,
                                  labelx, labely,
                                  barcodex, barcodey,
                                  barcodetxtx, barcodetxty,
                                  I_longdesc,
                                  descx, descy,
                                  descsize, dirpath);

            // printing
            dirfilename = System.Drawing.Image.FromFile(dirpath);
            PrintDocument pd = new PrintDocument();
            pd.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);
            pd.Print();


        }

        private string createImage(Int32 I_labelsize, Int32 I_barcodetxtsize,
                                   Int32 I_barcodesize, string I_label,
                                   string I_barcode, Font oFont2,
                                   float I_labelx, float I_labely,
                                   float I_barcodex, float I_barcodey,
                                   float I_barcodetxtx, float I_barcodetxty,
                                   string long_desc,
                                   float I_descx, float I_descy,
                                   Int32 I_descsize, string I_dirpath)
        {
            //string dirpath = null;   
            Bitmap oBitmap = new Bitmap(canvaswidth, canvasheight);
            Graphics oGraphics = Graphics.FromImage(oBitmap);

            Font oFont1 = new Font(labelfont, I_labelsize);
            //Font oFont2 = new Font(barcodefont, barcodesize);
            Font oFont3 = new Font(labelfont, I_barcodetxtsize);
            Font oFont4 = new Font(labelfont, I_descsize);


            SolidBrush oBrushWrite = new SolidBrush(Color.Black);
            SolidBrush oBrush = new SolidBrush(Color.White);


            oGraphics.FillRectangle(oBrush, 0, 0, canvaswidth, canvasheight);
            oGraphics.DrawString(I_label, oFont1, oBrushWrite, I_labelx, I_labely);
            oGraphics.DrawString("*" + I_barcode + "*", oFont2, oBrushWrite, I_barcodex, I_barcodey);
            //oGraphics.DrawString(I_barcode, oFont2, oBrushWrite, I_barcodex, I_barcodey);
            oGraphics.DrawString(I_barcode, oFont3, oBrushWrite, I_barcodetxtx, I_barcodetxty);
            if (long_desc.Length != 0)
                oGraphics.DrawString(long_desc, oFont4, oBrushWrite, I_descx, I_descy);

            //Response.ContentType = "image/jpeg";
            //oBitmap.Save(I_dirpath, ImageFormat.Jpeg);
            if (!File.Exists(I_dirpath))
            {
                oBitmap.Save(I_dirpath, ImageFormat.Jpeg);
            }

            return I_dirpath;
        }

        private void pd_PrintPage(object sender, PrintPageEventArgs ev)
        {
            Bitmap lbitmap = new Bitmap(canvaswidth, canvasheight);
            ev.Graphics.DrawImage(dirfilename, 0, 0, lbitmap.Width, lbitmap.Height);
            ev.HasMorePages = false;
            
        }
    }
}
