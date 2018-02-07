using System;
using System.Web;
using System.Drawing.Imaging; 

namespace AMSApp.zhenghua.ImageDisplay
{
	public class CachedImageService : IHttpHandler
	{
		// Override the IsReusable property
		public bool IsReusable { get { return true; } }

		// Override the ProcessRequest method
		public void ProcessRequest(HttpContext context)
		{
			string storageKey = "";

			// Retrieve the DATA query string parameter
			if (context.Request["data"] == null)
			{
				WriteError();
				return;
			}
			else storageKey = context.Request["data"].ToString();

			// Grab data from the cache 
			object o = HttpContext.Current.Cache[storageKey];
			if (o == null)
			{
				WriteError();
				return;
			}

			if ((o as byte[]) != null)
			{
				WriteImageBytes((byte[]) o);
			}
			else
			{
				if ((o as System.Drawing.Image) != null)
					WriteImage((System.Drawing.Image) o);
			}
		}

		private void WriteImageBytes(byte[] img)
		{
			HttpContext.Current.Response.ContentType = "image/jpeg"; 
			HttpContext.Current.Response.OutputStream.Write(img, 0, img.Length);
		}

		private void WriteImage(System.Drawing.Image img)
		{
			HttpContext.Current.Response.ContentType = "image/jpeg"; 
			img.Save(HttpContext.Current.Response.OutputStream, ImageFormat.Jpeg);
		}

		private void WriteError()
		{
			HttpContext.Current.Response.Write("No image specified");
		}
	}
}