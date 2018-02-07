using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace DXInfo.Print
{
    public class PrintMiddlePrimitiveText: IPrintPrimitive
    {
        private String Text;
        private Font Font;
        public PrintMiddlePrimitiveText(String buf, Font font)
        {
            Text = buf;
            Font = font;
        }
        public float CalculateHeight(Graphics graphics, float xPos, float width)
        {            
            return Font.GetHeight(graphics);
        }
        public void Draw(float xPos, float yPos, float rPos, float width, Graphics graphics)
        {
            SizeF sf = graphics.MeasureString(Text, Font);
            float newxPos = (width - sf.Width - xPos - rPos) / 2;
            graphics.DrawString(Text, Font, Brushes.Black, newxPos, yPos);
        }
    }
}
