using System;
using System.Drawing;
namespace DXInfo.Print
{
	public interface IPrintPrimitive
	{
		float CalculateHeight(Graphics graphics,float xPos,float width);        
        void Draw(float xPos,float yPos,float rPos,float width, Graphics graphics);
	}
}
