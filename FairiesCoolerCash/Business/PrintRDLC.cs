using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Text;
using Microsoft.Reporting.WinForms;

namespace FairiesCoolerCash.Business
{
    public class PrintRDLC : IDisposable
    {
        private int m_currentPageIndex;
        private IList<Stream> m_streams;
        private PaperSize ps;
        private Margins ms;
        private bool isLandscape;
        private int Width;
        private int Height;
        private Stream CreateStream(string name, string fileNameExtension, Encoding encoding,
                                    string mimeType, bool willSeek)
        {
            Stream stream = new MemoryStream();
            m_streams.Add(stream);
            return stream;
        }
        private void Export(LocalReport report)
        {
            report.EnableExternalImages = true;
            ReportPageSettings rps = report.GetDefaultPageSettings();
            ps = rps.PaperSize;
            ms = rps.Margins;
            isLandscape = rps.IsLandscape;

            Width = ps.Width;
            Height = ps.Height;
            if (rps.IsLandscape)
            {
                Width = ps.Height;
                Height = ps.Width;
            }
            int totalPages = 1;
            Warning[] warnings;
            string deviceInfo;
            do
            {
                this.Dispose();
                m_streams = new List<Stream>();
                deviceInfo =
              @"<DeviceInfo>
                <OutputFormat>EMF</OutputFormat>
                <PageWidth>" + Width / 100.00 + "in</PageWidth>" +
                "<PageHeight>" + Height / 100.00 + "in</PageHeight>" +
                "<MarginTop>" + ms.Top / 100.00 + "in</MarginTop>" +
                "<MarginLeft>" + ms.Left / 100.00 + "in</MarginLeft>" +
                "<MarginRight>" + ms.Right / 100.00 + "in</MarginRight>" +
                "<MarginBottom>" + ms.Bottom / 100.00 + "in</MarginBottom>" +
            "</DeviceInfo>";
                report.Render("Image", deviceInfo, CreateStream,
               out warnings);
                foreach (Stream stream in m_streams)
                    stream.Position = 0;
                totalPages = m_streams.Count;
                if (totalPages > 1)
                {
                    Height += 29;
                }
            }
            while (totalPages > 1);
        }
        private void PrintPage(object sender, PrintPageEventArgs ev)
        {
            Metafile pageImage = new
               Metafile(m_streams[m_currentPageIndex]);

            Rectangle adjustedRect = new Rectangle(0, 0, Width, Height);

            ev.Graphics.DrawImage(pageImage, adjustedRect);
            m_currentPageIndex++;
            ev.HasMorePages = (m_currentPageIndex < m_streams.Count);
        }
        private void Print()
        {
            if (m_streams == null || m_streams.Count == 0)
                throw new Exception("Error: no stream to print.");
            PrintDocument printDoc = new PrintDocument();
            if (!printDoc.PrinterSettings.IsValid)
            {
                throw new Exception("Error: cannot find the default printer.");
            }
            else
            {
                ps.Height = Height;
                ps.Width = Width;
                printDoc.DefaultPageSettings.PaperSize = ps;
                printDoc.DefaultPageSettings.Margins = ms;
                printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
                m_currentPageIndex = 0;
                printDoc.Print();
            }
        }
        public void Run(LocalReport report)
        {
            try
            {
                Export(report);
                Print();
            }
            catch (Exception ex)
            {
                Helper.ShowErrorMsg(ex.Message);
                Helper.HandelException(ex);
            }
        }
        public void Dispose()
        {
            if (m_streams != null)
            {
                foreach (Stream stream in m_streams)
                    stream.Close();
                m_streams = null;
            }
        }
    }
}
