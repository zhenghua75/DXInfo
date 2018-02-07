using System;
using System.Drawing;

namespace DXInfo.Print
{
	public interface IPrintable
	{
        void Print(PrintElement element, Graphics graphics);
	}
}
