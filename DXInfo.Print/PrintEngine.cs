using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Printing;

namespace DXInfo.Print
{
    public class PrintBatchStickerEngine : PrintDocument
    {
        private ArrayList _printObjects;
        private float Top = 10;
        private float Left = 10;
        private int Height;
        //private int PageHeight;
        private float Right = 0;
        public PrintBatchStickerEngine(string printerName, 
            float top, float left)
        {
            _printObjects = new ArrayList();
            this.PrinterSettings.PrinterName = printerName;
            this.Top = top;
            this.Left = left;
            //int dpi = this.DefaultPageSettings.PrinterResolution.Y;
            this.Height = this.DefaultPageSettings.PaperSize.Height + 13;
            
        }
        public void AddPrintObject(IPrintable printObject)
        {
            _printObjects.Add(printObject);
        }
        public void ClearPrintObject()
        {
            if (_printObjects != null && _printObjects.Count>0)
            {
                _printObjects.Clear();
            }
        }
        protected override void OnPrintPage(PrintPageEventArgs e)
        {
            float yPos = Top;// +5.9f;
            bool morePages = false;
            int elementsOnPage = 0;
            int printIndex = 0;
            
            while (printIndex < _printObjects.Count)
            {
                IPrintable printObject = (IPrintable)_printObjects[printIndex];
                PrintElement printElement = new PrintElement(printObject);
                printElement.Print(null);
                printElement.Draw(Left, yPos,Right, e.PageBounds.Width, e.Graphics);
                yPos += this.Height;
                printIndex++;
                elementsOnPage++;
            }
            e.HasMorePages = morePages;

        }
    }
    public class PrintEngine : PrintDocument
    {
        #region ×Ö¶Î
        public PrintElement Header;
        public PrintElement Footer;
        private ArrayList _printElements;
        private int _printIndex;
        private float Top;
        private float Left;
        private float Right;
        private Graphics g;
        #endregion

        public PrintEngine():this(null)
        {          
        }
        public PrintEngine(PrintElement header, PrintElement footer):this(header,footer,0,0,0)
        {
        }
        public PrintEngine(float top,float left,float right):this(null,top,left,right)
        {
        }
        public PrintEngine(string printerName):this(printerName,0,0,0)
        {
        }
        public PrintEngine(string printerName, float top, float left, float right):this(printerName,null,null,top,left,right)
        {
        }
        public PrintEngine(PrintElement header, PrintElement footer, float top, float left, float right)
            : this(null, header, footer, top, left, right)
        {
        }
        public PrintEngine(string printerName,PrintElement header, PrintElement footer, float top,float left,float right)
        {
            InitEngine(printerName: printerName, header: header, footer: footer, top: top, left: left, right: right);
        }
        
        private void InitEngine(string printerName=null,
            PrintElement header=null, PrintElement footer=null, float top=0, float left=0, float right=0)
        {
            if (!string.IsNullOrEmpty(printerName))
            {
                this.PrinterSettings.PrinterName = printerName;
            }
            Header = header;
            Footer = footer;
            _printIndex = 0;
            _printElements = new ArrayList();
            g = this.PrinterSettings.CreateMeasurementGraphics();
            Top = top;
            Left = left;
            Right = right;
        }
        
        public void AddPrintObject(IPrintable printObject)
        {
            PrintElement element = new PrintElement(printObject);
            element.Print(g);

            _printElements.Add(element);
            
        }
        public float CalculateHeight()
        {
            float width = this.DefaultPageSettings.PaperSize.Width;
            float headerHeight = 0;
            if (Header != null)
            {
                headerHeight = Header.CalculateHeight(g, Left, width);
            }
            float yPos = Top + headerHeight;
            int calPrintIndex = 0;
            while (calPrintIndex < _printElements.Count)
            {
                PrintElement element = (PrintElement)_printElements[_printIndex];
                float height = element.CalculateHeight(g, Left, width);
                yPos += height;
                calPrintIndex++;
            }
            float footerHeight = 0;
            if (Footer != null)
            {
                footerHeight = Footer.CalculateHeight(g, Left, width);
            }
            yPos += footerHeight;
            yPos += Top;
            return yPos;
        }
        // ShowPreview - show a print preview... 
        //public void ShowPreview()
        //{
        //    // now, show the print dialog... 
        //    PrintPreviewDialog dialog = new PrintPreviewDialog();
        //    dialog.Document = this;

        //    // show the dialog... 
        //    dialog.ShowDialog();
        //}
         // OnBeginPrint - called when printing starts 

        //protected override void OnBeginPrint(PrintEventArgs e)
        //{
                           
        //}

        // OnPrintPage - called when printing needs to be done... 
        protected override void OnPrintPage(PrintPageEventArgs e)
        {
            //foreach (IPrintable printObject in _printObjects)
            //{
            //    PrintElement element = new PrintElement(printObject);
            //    _printElements.Add(element); 
            //    element.Print(e.Graphics);
            //}

            float headerHeight = 0;
            if (Header != null)
            {
                headerHeight = Header.CalculateHeight(e.Graphics, Left, e.PageBounds.Width);
                Header.Draw(Left, Top, Right, e.PageBounds.Width, e.Graphics);
            }
            float yPos = Top + headerHeight;
            bool morePages = false;
            int elementsOnPage = 0;
            while (_printIndex < _printElements.Count)
            {
                PrintElement element = (PrintElement)_printElements[_printIndex];
                float height = element.CalculateHeight(e.Graphics, Left, e.PageBounds.Width);
                element.Draw(Left, yPos, Right, e.PageBounds.Width, e.Graphics);
                yPos += height;
                _printIndex++;
                elementsOnPage++;
            }
            float footerHeight = 0;
            if (Footer != null)
            {
                footerHeight = Footer.CalculateHeight(e.Graphics, Left, e.PageBounds.Width);
                Footer.Draw(Left, yPos, Right, e.PageBounds.Width, e.Graphics);
            }
            e.HasMorePages = morePages;
            yPos += footerHeight;

        }

         // ShowPageSettings - let's us change the page settings... 
        //public void ShowPageSettings()
        //{
        //    PageSetupDialog setup = new PageSetupDialog();
        //    PageSettings settings = DefaultPageSettings;
        //    setup.PageSettings = settings;

        //    // display the dialog and, 
        //    if (setup.ShowDialog() == DialogResult.OK)
        //        DefaultPageSettings = setup.PageSettings;
        //}
    }

}
