using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace DXInfo.Print
{

    public class PrintBreakPrimitiveText : IPrintPrimitive
    {
        private String Text;
        private Font Font;
        public PrintBreakPrimitiveText(String buf, Font font)
        {
            Text = buf;
            Font = font;
        }
        public float CalculateHeight(Graphics graphics, float xPos,float width)
        {
            StringFormat sfFmt = new StringFormat(StringFormatFlags.LineLimit);            
            SizeF sf = graphics.MeasureString(Text, Font, (int)(width-2*xPos), sfFmt);
            return sf.Height;
        }
        public void Draw(float xPos, float yPos, float rPos, float width, Graphics graphics)
        {
            StringFormat sfFmt = new StringFormat(StringFormatFlags.LineLimit);
            SizeF sf = graphics.MeasureString(Text, Font, (int)(width - 2 * xPos), sfFmt);
            RectangleF recfF = new RectangleF(xPos, yPos, width - 2 * xPos, sf.Height);
            graphics.DrawString(Text, Font,Brushes.Black, recfF);
        }
    }
}
