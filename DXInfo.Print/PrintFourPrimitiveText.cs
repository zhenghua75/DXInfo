using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace DXInfo.Print
{
    public class PrintFourPrimitiveText : IPrintPrimitive
    {
        private string Text1;
        private string Text2;
        private string Text3;
        private string Text4;

        private Font Font;
        private float MaxLength1;
        private float MaxLength2;
        private float MaxLength3;

        public PrintFourPrimitiveText(string buf1, string buf2, string buf3,string buf4,
            float maxLength1, float maxLength2, float maxLength3, Font font)
        {
            Text1 = buf1;
            Text2 = buf2;
            Text3 = buf3;
            Text4 = buf4;

            MaxLength1 = maxLength1;
            MaxLength2 = maxLength2;
            MaxLength3 = maxLength3;
            Font = font;
        }
        public float CalculateHeight(Graphics graphics, float xPos, float width)
        {
            return Font.GetHeight(graphics);
        }
        public void Draw(float xPos, float yPos, float rPos, float width, Graphics graphics)
        {
            //名称、单价、数量、金额
            graphics.DrawString(Text1, Font, Brushes.Black, xPos, yPos);
            graphics.DrawString(Text2, Font, Brushes.Black, width - MaxLength1 - MaxLength2 - MaxLength3 - rPos, yPos);
            graphics.DrawString(Text3, Font, Brushes.Black, width - MaxLength2 - MaxLength3 - rPos, yPos);
            graphics.DrawString(Text4, Font, Brushes.Black, width - MaxLength3 - rPos, yPos);
        }
    }
}
