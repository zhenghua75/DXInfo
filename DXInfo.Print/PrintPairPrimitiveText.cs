using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace DXInfo.Print
{
    public class PrintPairPrimitiveText : IPrintPrimitive
    {
        private string Text1;
        private string Text2;
        private string Separation;
        private Font Font1;
        private Font Font2;
        private float MaxLength;
        public PrintPairPrimitiveText(string buf1, string buf2, string separation,
            float maxLength, Font font1,Font font2)
        {
            Text1 = buf1;
            Text2 = buf2;
            Separation = separation;
            MaxLength = maxLength;
            Font1 = font1;
            Font2 = font2;
        }
        public float CalculateHeight(Graphics graphics, float xPos, float width)
        {            
            //return Font.GetHeight(graphics);
            float height1 = Font1.GetHeight(graphics);
            float height2 = Font2.GetHeight(graphics);
            return height1 > height2 ? height1 : height2;
        }
        public void Draw(float xPos, float yPos, float rPos, float width, Graphics graphics)
        {
            graphics.DrawString(Text1, Font1, Brushes.Black, xPos, yPos);
            graphics.DrawString(Separation + Text2, Font2, Brushes.Black, xPos+MaxLength, yPos);
        }
    }
    public class PrintAlignPairPrimitiveText : IPrintPrimitive
    {
        private string Text1;
        private string Text2;
        private Font Font;
        private float MaxLength2;
        public PrintAlignPairPrimitiveText(string buf1, string buf2, float maxLength2, Font font)
        {
            Text1 = buf1;
            Text2 = buf2;
            MaxLength2 = maxLength2;
            Font = font;
        }
        public float CalculateHeight(Graphics graphics, float xPos, float width)
        {
            return Font.GetHeight(graphics);
        }
        public void Draw(float xPos, float yPos, float rPos, float width, Graphics graphics)
        {
            graphics.DrawString(Text1, Font, Brushes.Black, xPos, yPos);
            graphics.DrawString(Text2, Font, Brushes.Black, width - xPos - MaxLength2 - rPos, yPos);
        }
    }
}
