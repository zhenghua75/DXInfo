using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace DXInfo.Print
{
    public class PrintTriplePrimitiveText:IPrintPrimitive
    {
        private string Text1;
        private string Text2;
        private string Text3;
        private Font Font1;
        private Font Font2;
        private Font Font3;
        private float MaxLength1;
        private float MaxLength2;
        public PrintTriplePrimitiveText(string buf1, string buf2, string buf3, 
            float maxLength1,float maxLength2,
            Font font1,Font font2,Font font3)
        {
            Text1 = buf1;
            Text2 = buf2;
            Text3 = buf3;
            MaxLength1 = maxLength1;
            MaxLength2 = maxLength2;
            Font1 = font1;
            Font2 = font2;
            Font3 = font3;
        }
        public float CalculateHeight(Graphics graphics, float xPos, float width)
        {
            float height1 = Font1.GetHeight(graphics);
            float height2 = Font2.GetHeight(graphics);
            float height3 = Font3.GetHeight(graphics);
            float height = height1;
            if (height < height2) height = height2;
            if (height < height3) height = height3;
            return height;
        }
        public void Draw(float xPos, float yPos, float rPos, float width, Graphics graphics)
        {
            graphics.DrawString(Text1, Font1, Brushes.Black, xPos, yPos);
            graphics.DrawString(Text2, Font2, Brushes.Black, width - rPos - MaxLength2 - MaxLength1, yPos);
            graphics.DrawString(Text3, Font3, Brushes.Black, width - rPos - MaxLength2, yPos);
        }
    }
}
