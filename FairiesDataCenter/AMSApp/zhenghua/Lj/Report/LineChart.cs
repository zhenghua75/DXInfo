using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;


namespace AMSApp.zhenghua.Lj.Report
{
	/// <summary>
	/// 
	/// </summary>
	public class LineChart
	{
		public Bitmap b ;
		public string Title = "" ;
		public ArrayList chartValues = new ArrayList ( ) ;
		public float Xorigin = 0 , Yorigin = 0 ;
		public float ScaleX , ScaleY ;
		public float Xdivs = 2 , Ydivs = 2 ;
		public System.Drawing.Pen myPen;
		public string strXTitle = "";
		public string strYTitle = "";

		private int Width , Height ;

		private Graphics g ;
	
		struct datapoint 
		{
			public float x ;
			public float y ;
			public bool valid ;
		}

		public LineChart ( int myWidth , int myHeight ) 
		{
			Width = myWidth ; Height = myHeight ;
			ScaleX = myWidth ; ScaleY = myHeight ;
			b = new Bitmap ( myWidth , myHeight ) ;
			g = Graphics.FromImage ( b ) ;
			myPen = new Pen ( Color.Blue , 2 ) ;			
		}

		public void SetPen(System.Drawing.Pen newPen)
		{
			this.myPen = newPen;
		}

		public void AddValue ( int x , int y ) 
		{
			datapoint myPoint ;
			myPoint.x = x ;
			myPoint.y = y ;
			myPoint.valid = true ;
			chartValues.Add ( myPoint ) ;
		}

		public void AddValue ( int x , float y ) 
		{
			datapoint myPoint ;
			myPoint.x = x ;
			myPoint.y = y ;
			myPoint.valid = true ;
			chartValues.Add ( myPoint ) ;
		}

		public void ClearOldPoint()
		{
			this.chartValues.Clear();
		}

		public void DrawCoordinate () 
		{
			int i ;
			float x , y ;
			string myLabel ;
			Pen myGridLinePen = new Pen(Color.Black,1);
			
			Brush blackBrush = new SolidBrush ( Color.Black ) ;
			Brush blueBrush = new SolidBrush(Color.Blue);
			Font axesFont = new Font ( "arial" , 10 ) ;

			//首先要创建图片的大小
						
			g.FillRectangle ( new SolidBrush ( Color.LightSkyBlue) , 0 , 0 , Width , Height ) ;
			int ChartInset = 50 ;
			int ChartWidth = Width - ( 2 * ChartInset ) ;
			int ChartHeight = Height - ( 2 * ChartInset ) ;

			g.DrawString ( strYTitle, new Font ( "arial" , 8 ) , blueBrush ,10 ,25 ) ;
			g.DrawString ( strXTitle ,new Font ( "arial" , 8 ) , blueBrush ,Width-ChartInset,Height -ChartInset ) ;
			
			g.DrawRectangle ( new Pen ( Color.Black , 1 ) , ChartInset , ChartInset , ChartWidth , ChartHeight ) ;
			//写出图片上面的图片内容文字
			g.DrawString ( Title , new Font ( "arial" , 14 ) , blackBrush , Width / 4 , 10 ) ;
			//沿X坐标写入X标签
			for ( i = 0 ; i <= Xdivs ; i++ ) 
			{
				x = ChartInset + ( i * ChartWidth ) / Xdivs ;
				y = ChartHeight + ChartInset ;
				myLabel = ( Xorigin + ( ScaleX * i / Xdivs ) ).ToString ( ) ;
				g.DrawString ( myLabel , axesFont , blackBrush , x - 4 , y + 10 ) ;
				g.DrawLine ( myPen , x , y + 2 , x , y - 2 ) ;
			}
			//沿Y坐标写入Y标签
			for ( i = 0 ; i <= Ydivs ; i++ )
			{
				x = ChartInset ;
				y = ChartHeight + ChartInset - ( i * ChartHeight / Ydivs ) ;
				myLabel = ( Yorigin + ( ScaleY * i / Ydivs ) ).ToString ( ) ;
				g.DrawString ( myLabel , axesFont , blackBrush , 5 , y - 6 ) ;
				g.DrawLine ( myPen , x + 2 , y , x - 2 , y ) ;
				g.DrawLine( myGridLinePen,x,y,Width-ChartInset,y);
			}		
			g.RotateTransform(180 ) ;
			g.TranslateTransform( 0 , - Height ) ;
			g.TranslateTransform ( - ChartInset , ChartInset ) ;
			g.ScaleTransform ( - 1 , 1 ) ;
		}

		public void DrawLine(Pen pen,bool bLineLable)
		{
			//画出图表中的数据
			datapoint prevPoint = new datapoint ( ) ;
			float x , y , x0 , y0 ;
			int ChartInset = 50 ;
			int ChartWidth = Width - ( 2 * ChartInset ) ;
			int ChartHeight = Height - ( 2 * ChartInset ) ;
			prevPoint.valid = false ;
			Brush penBrush = new SolidBrush ( pen.Color ) ;
			foreach ( datapoint myPoint in chartValues ) 
			{				
				x0 = ChartWidth * ( prevPoint.x - Xorigin ) / ScaleX ;
				y0 = ChartHeight * ( prevPoint.y - Yorigin ) / ScaleY ;
				x = ChartWidth * ( myPoint.x - Xorigin ) / ScaleX ;
				y = ChartHeight * ( myPoint.y - Yorigin ) / ScaleY ;
				if ( prevPoint.valid == true ) 
				{
					g.DrawLine ( pen , x0 , y0 , x , y ) ;
					g.FillEllipse ( penBrush , x0 - 2 , y0 - 2 , 4 , 4 ) ;					
				}
				g.FillEllipse ( penBrush , x - 2 , y - 2 , 4 , 4 ) ;
//				if(bLineLable)
//				{
//					RectangleF recF = new RectangleF(x-10,y+10,40,15);					
//					g.DrawString(myPoint.y.ToString(),new Font ( "arial" , 10 ),new SolidBrush ( Color.BlueViolet),recF);				
//				}
				prevPoint = myPoint ;
			}
		}

		~LineChart ( ) 
		{
			g.Dispose ( ) ;
			b.Dispose ( ) ;
		}

		public void SaveChart(string strFileName,System.Drawing.Imaging.ImageFormat imageFormat)
		{
			b.Save(strFileName,imageFormat);
		}
	}
}
