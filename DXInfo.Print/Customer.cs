using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace DXInfo.Print
{
    public class Customer : IPrintable
    {
        public int Id;
        public String FirstName;
        public String LastName;
        public String Company;
        public String Email;
        public String Phone;
        // Print... 
        public void Print(PrintElement element,Graphics graphics)
        {
            // tell the engine to draw a header... Arial
            Font font = new Font("Arial", 10);
            Font headFont = new Font("Arial", 12, FontStyle.Bold);
            SizeF sf = graphics.MeasureString("Customer ID", font);
            float maxLength = sf.Width;
            element.AddMiddleText("Customer", headFont);
            // now, draw the data... 
            element.AddPairText("Customer ID", Id.ToString(), maxLength, font,font);
            element.AddPairText("Name", FirstName + " " + LastName, maxLength, font,font);
            element.AddPairText("Company", Company, maxLength, font,font);
            element.AddPairText("E-mail", Email, maxLength, font,font);
            element.AddPairText("Phone", Phone, maxLength, font,font);
            // finally, add a blank line... 
            //element.AddText("", font);
        }
    }
}
