using System;
using System.Web;
using System.Web.UI;
using System.Drawing;
using System.Web.Caching;
using System.Web.UI.WebControls;
using System.ComponentModel;

namespace AMSApp.zhenghua.ImageDisplay
{
	[DefaultProperty("ImageFile")]
	[ToolboxData("<{0}:DynamicImage runat=server></{0}:DynamicImage>")]
	public class DynamicImage : System.Web.UI.WebControls.Image
	{
		private const string IgsBaseUrl = "cachedimageservice.axd?data={0}";

		// ****************************************************************************
		// Ctor
		// Wire up events
		public DynamicImage()
		{
			m_imageBytes = null;
			m_image = null;

			this.PreRender += new EventHandler(DynamicImage_PreRender); 			
		}
		// ****************************************************************************


		// ****************************************************************************
		// PRIVATE members
		private System.Drawing.Image m_image;
		private byte[] m_imageBytes;
		// ****************************************************************************


		// ****************************************************************************
		// PROPERTY: StorageKey 
		// DESCRIPT: ...
		public string StorageKey
		{
			get {return Convert.ToString(ViewState["StorageKey"]);}
			set {ViewState["StorageKey"] = value;}
		}
		// ****************************************************************************


		// ****************************************************************************
		// PROPERTY: ImageUrl (override)
		// DESCRIPT: Returns the actual URL served to the browser 
		public override string ImageUrl
		{
			get {return GetImageUrl();}
			set {throw new NotSupportedException();}
		}
		// ****************************************************************************


		// ****************************************************************************
		// PROPERTY: GetImageUrl 
		// DESCRIPT: Calculates and returns the actual URL served to the browser 
		private string GetImageUrl()
		{
			string url = "";

			// Check ImageFile 
			if (ImageFile.Length >0)
			{
				url = ImageFile;
			}
			else // Check ImageBytes and Image
			{
				if (ImageBytes != null || Image != null)
				{
					if (StorageKey.Length == 0)
					{
						Guid g = Guid.NewGuid(); 
						StorageKey = g.ToString();  
					}
					string strUrl = GetCachedImageUrl();
					ImageFile = strUrl;
					return strUrl;
				}
			}

			return url;
		}
		// ****************************************************************************

		
		// ****************************************************************************
		// PROPERTY: ImageFile
		// DESCRIPT: URL to the file to display (when set defaults to Image)
		public string ImageFile
		{
			get {return Convert.ToString(ViewState["ImageFile"]);}
			set {ViewState["ImageFile"] = value;}
		}
		// ****************************************************************************


		// ****************************************************************************
		// PROPERTY: ImageBytes
		// DESCRIPT: Gets and sets the image using an array of bytes
		[Browsable(false)]
		public byte[] ImageBytes
		{
			get {return m_imageBytes;}
			set {m_imageBytes = value;}
		}
		// ****************************************************************************


		// ****************************************************************************
		// PROPERTY: Image 
		// DESCRIPT: Gets and sets the image using a GDI+ object
		[Browsable(false)]
		public System.Drawing.Image Image 
		{
			get {return m_image;}
			set 
			{
				if (StorageKey.Length > 0)
				{					
					if (Page.Cache[StorageKey] != null)
					{
						Page.Cache.Remove(StorageKey);					
					}
					StorageKey = "";
					ImageFile = "";
				}
				m_image = value;
			}
		}
		// ****************************************************************************


		// ****************************************************************************
		// HANDLER:  PreRender 
		// DESCRIPT: Fire before the control renders its markup 
		private void DynamicImage_PreRender(object sender, EventArgs e)
		{
			if (ImageUrl.Length == 0)
				return;

			// Cache bytes or image
			if (ImageBytes != null)
				StoreImageBytes();
			else
				if (Image != null)
				StoreImage();

			return;
		}
		// ****************************************************************************


		// ****************************************************************************
		// PROPERTY: GetCachedImageUrl 
		// DESCRIPT: Return the URL of the cached image
		private string GetCachedImageUrl()
		{
			return String.Format(IgsBaseUrl, StorageKey);
		}
		// ****************************************************************************


		// ****************************************************************************
		// METHOD:   StoreImage 
		// DESCRIPT: Caches the images as a GDI+ object 
		private void StoreImage()
		{
			StoreData(m_image);
		}
		// ****************************************************************************


		// ****************************************************************************
		// METHOD:   StoreImageBytes 
		// DESCRIPT: Caches the bytes of the image 
		private void StoreImageBytes()
		{
			StoreData(m_imageBytes);
		}
		// ****************************************************************************


		// ****************************************************************************
		// METHOD:   StoreData
		// DESCRIPT: Helper method to store data to the cache
		private void StoreData(object data)
		{
			if (Page.Cache[StorageKey] == null)
			{
				Page.Cache.Add(StorageKey, 
					data, 
					null, 
					Cache.NoAbsoluteExpiration, 
					TimeSpan.FromMinutes(5), 
					CacheItemPriority.High, 
					null);
			}
		}
		// ****************************************************************************
	}
}