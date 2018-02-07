using System;
using System.Drawing;
namespace DXInfo.Print
{
    /// <summary>
    /// ´òÓ¡ÎÄ±¾
    /// </summary>
    public class PrintPrimitiveText : IPrintPrimitive
    {
        private String Text;
        private Font Font;
        public PrintPrimitiveText(String buf,Font font)
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
            //SizeF sf = graphics.MeasureString(Text, Font);
            //StringFormat sf = new StringFormat();
            //sf.FormatFlags = StringFormatFlags.NoClip;
             
            graphics.DrawString(Text, Font,Brushes.Black, xPos, yPos);//,StringFormat.GenericTypographic);
        }
        //public void DrawSpacedText(Graphics g, Font font, Brush brush, PointF point, string text, int desiredWidth)
        //{
        //    //Calculate spacing
        //    float widthNeeded = 0;
        //    foreach (char c in text)
        //    {
        //        widthNeeded += g.MeasureString(c.ToString(), font).Width;
        //    }
        //    float spacing = (desiredWidth - widthNeeded) / (text.Length - 1);

        //    //draw text
        //    float indent = 0;
        //    foreach (char c in text)
        //    {
        //        g.DrawString(c.ToString(), font, brush, new PointF(point.X + indent, point.Y));
        //        indent += g.MeasureString(c.ToString(), font).Width + spacing;
        //    }
        //}
        //public void DrawSpacedText(Graphics g, Font font, Brush brush, PointF point, string text, int desiredWidth)
        //{
        //    //Calculate spacing
        //    float widthNeeded = 0;
        //    Region[] regions = g.MeasureCharacterRanges(text, font, new RectangleF(point, new SizeF(desiredWidth, font.Height + 10)), StringFormat.GenericDefault);
        //    float[] widths = new float[regions.Length];
        //    for (int i = 0; i < widths.Length; i++)
        //    {
        //        widths[i] = regions[i].GetBounds(g).Width;
        //        widthNeeded += widths[i];
        //    }
        //    float spacing = (desiredWidth - widthNeeded) / (text.Length - 1);

        //    //draw text
        //    float indent = 0;
        //    int index = 0;
        //    foreach (char c in text)
        //    {
        //        g.DrawString(c.ToString(), font, brush, new PointF(point.X + indent, point.Y));
        //        indent += widths[index] + spacing;
        //        index++;
        //    }
        //}


    }
}
