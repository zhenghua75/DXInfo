using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace DXInfo.Print
{
    public class PrintPrimitiveImage : IPrintPrimitive
    {
        private String Text1;
        private Font Font1;
        private String Text2;
        private Font Font2;

        private Image image;
        public PrintPrimitiveImage(string fileName, String buf1, Font font1, String buf2, Font font2)
        {
            image = Image.FromFile(fileName);
            Text1 = buf1;
            Font1 = font1;
            Text2 = buf2;
            Font2 = font2;
        }
        public float CalculateHeight(Graphics graphics, float xPos, float width)
        {
            //return 30;//image.Height;//Font.GetHeight(graphics);
            return Font2.GetHeight(graphics);
        }
        public void Draw(float xPos, float yPos, float rPos, float width, Graphics graphics)
        {
            SizeF sf = graphics.MeasureString("名称预留八", new Font("宋体", 8, FontStyle.Bold));
            //SizeF sf1 = graphics.MeasureString("高度", Font2);
            SizeF sf1 = graphics.MeasureString("11/111", Font1);

            //graphics.DrawImage(image, xPos, yPos);
            graphics.DrawImage(image, xPos, yPos);//, sf.Width, sf1.Height);

            graphics.DrawString(Text1, Font1, Brushes.Black, xPos + sf.Width, yPos+10);//,StringFormat.GenericTypographic);
            graphics.DrawString(Text2, Font2, Brushes.Black, xPos + sf.Width + sf1.Width, yPos);
        }
    }
}
