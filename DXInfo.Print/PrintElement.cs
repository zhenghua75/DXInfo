using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;

namespace DXInfo.Print
{    
    public class PrintElement
    {
        private ArrayList _printPrimitives = new ArrayList();
        private IPrintable _printObject;
        private string _separtion = ":";
        public PrintElement(IPrintable printObject)
        {
            _printObject = printObject;
        }
        public void AddText(String buf,Font font)
        {
            AddPrimitive(new PrintPrimitiveText(buf,font));
        }
        public void AddImage(string fileName, String buf1, Font font1, String buf2, Font font2)
        {
            AddPrimitive(new PrintPrimitiveImage(fileName,buf1,font1,buf2,font2));
        }
        //public void AddTwoFontText(String buf1,string buf2, Font font1,Font font2)
        //{
        //    AddPrimitive(new PrintTwoFontText(buf1,buf2, font1,font2));
        //}
        public void AddBreakText(String buf, Font font)
        {
            AddPrimitive(new PrintBreakPrimitiveText(buf, font));
        }
        public void AddPrimitive(IPrintPrimitive primitive)
        {
            _printPrimitives.Add(primitive);
        }

        //public void AddData(String dataName, String dataValue,Font font)
        //{
        //    Dictionary<string, string> pairs = new Dictionary<string, string>();
        //    AddText(dataName + ": " + dataValue,font);
        //}

        public void AddHorizontalRule()
        {
            AddPrimitive(new PrintPrimitiveRule());
        }
        public void AddWhiteLine()
        {
            AddPrimitive(new PrintPrimitiveWhiteLine());
        }
        public void AddMiddleText(string buf, Font font)
        {
            AddPrimitive(new PrintMiddlePrimitiveText(buf, font));
        }
        public void AddPairText(string buf1,string buf2,float maxLenght, Font font1,Font font2)
        {
            AddPrimitive(new PrintPairPrimitiveText(buf1, buf2, _separtion, maxLenght, font1,font2));
        }
        public void AddAlignPairText(string buf1, string buf2, float maxLenght2, Font font)
        {
            AddPrimitive(new PrintAlignPairPrimitiveText(buf1, buf2, maxLenght2, font));
        }
        public void AddTripleText(string buf1, string buf2,string buf3,float maxLength1,float maxLength2, Font font1,Font font2,Font font3)
        {
            AddPrimitive(new PrintTriplePrimitiveText(buf1, buf2, buf3,maxLength1,maxLength2, font1,font2,font3));
        }
        public void AddFourText(string buf1, string buf2, string buf3,string buf4,
            float maxLenght1, float maxLength2, float maxLength3, Font font)
        {
            AddPrimitive(new PrintFourPrimitiveText(buf1, buf2, buf3,buf4, maxLenght1, maxLength2,maxLength3, font));
        }
        public float CalculateHeight(Graphics graphics, float xPos,float width)
        {
            float height = 0;
            foreach (IPrintPrimitive primitive in _printPrimitives)
            {
                height += primitive.CalculateHeight(graphics, xPos,width);
            }
            return height;
        }

        public void Draw(float xPos, float yPos, float rPos, float width, Graphics graphics)
        {
            //float height = CalculateHeight(graphics, xPos, width);
            foreach (IPrintPrimitive primitive in _printPrimitives)
            {
                primitive.Draw(xPos, yPos,rPos,width, graphics);
                yPos += primitive.CalculateHeight(graphics,xPos,width);
            }
        }

        public void Print(Graphics graphics)
        {
            _printObject.Print(this,graphics);
        }
    }
}
