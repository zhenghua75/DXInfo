using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace DXInfo.Print
{
    //public class PrintTwoFontText : IPrintPrimitive
    //{
    //    private String Text1;
    //    private string Text2;
    //    private Font Font1;
    //    private Font Font2;
    //    public PrintTwoFontText(String buf1, string buf2, Font font1, Font font2)
    //    {
    //        Text1 = buf1;
    //        Text2 = buf2;
    //        Font1 = font1;
    //        Font2 = font2;
    //    }
    //    public float CalculateHeight(Graphics graphics, float xPos, float width)
    //    {
    //        float height1 = Font1.GetHeight(graphics);
    //        float height2 = Font2.GetHeight(graphics);
    //        return height1 > height2 ? height1 : height2;
    //    }
    //    public void Draw(float xPos, float yPos, float rPos, float width, Graphics graphics)
    //    {
    //        SizeF sf = graphics.MeasureString(Text1, Font1);
    //        graphics.DrawString(Text1, Font1, Brushes.Black, xPos, yPos);
    //        graphics.DrawString(Text2, Font2, Brushes.Black, xPos + sf.Width, yPos);
    //    }
    //}
}
