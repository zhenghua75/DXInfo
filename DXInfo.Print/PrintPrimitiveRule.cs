using System;
using System.Drawing;
namespace DXInfo.Print
{
	/// <summary>
	/// 打印线
	/// </summary>
    public class PrintPrimitiveRule : IPrintPrimitive
    {
        public float CalculateHeight(Graphics graphics, float xPos,float width)
        {
            return 5;
        }
        public void Draw(float xPos, float yPos, float rPos, float width, Graphics graphics)
        {
            Pen pen = new Pen(Brushes.Black, 1);
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            graphics.DrawLine(pen, xPos, yPos+2,width-2*xPos,yPos+2);
        }
    }
    public class PrintPrimitiveWhiteLine : IPrintPrimitive
    {
        public float CalculateHeight(Graphics graphics, float xPos, float width)
        {
            return 5;
        }
        public void Draw(float xPos, float yPos, float rPos, float width, Graphics graphics)
        {
            Pen pen = new Pen(Brushes.White, 1);
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            graphics.DrawLine(pen, xPos, yPos + 2, width - 2 * xPos, yPos + 2);
        }
    }
    //画表格
    /// <summary>
    /// 竖线
    /// </summary>
    public class PrintPrimitiveSX : IPrintPrimitive
    {
        private float height = 5;
        public float CalculateHeight(Graphics graphics, float xPos, float width)
        {
            this.height = 5;
            return 5;
        }
        public void Draw(float xPos, float yPos, float rPos, float width, Graphics graphics)
        {
            Pen pen = new Pen(Brushes.Black, 1);
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            graphics.DrawLine(pen, xPos, yPos, xPos, yPos+height);
        }
    }
    public class PrintPrimitiveHX : IPrintPrimitive
    {
        public float CalculateHeight(Graphics graphics, float xPos, float width)
        {
            return 5;
        }
        public void Draw(float xPos, float yPos, float rPos, float width, Graphics graphics)
        {
            Pen pen = new Pen(Brushes.Black, 1);
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            graphics.DrawLine(pen, xPos, yPos, xPos+width, yPos);
        }
    }
}
