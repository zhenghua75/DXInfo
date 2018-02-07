using System;
namespace Trirand.Web.Mvc
{
	public class CustomFormatter : JQGridColumnFormatter
	{
		public string FormatFunction
		{
			get;
			set;
		}
		public string UnFormatFunction
		{
			get;
			set;
		}
	}
    /// <summary>
    /// ��ʽ�����֣�ȥ��ĩβ��
    /// </summary>
    public class DigitFormatter : CustomFormatter
    {
        public DigitFormatter()
        {
            FormatFunction = "FormatNumber";
            UnFormatFunction = "UnFormatNumber";
        }
    }
    public class RankingFormatter : CustomFormatter
    {
        public RankingFormatter()
        {
            FormatFunction = "FormatRanking";
            UnFormatFunction = "UnFormatRanking";
        }
    }
    public class ImageFormatter : CustomFormatter
    {
        public ImageFormatter()
        {
            FormatFunction = "formatImage";
            UnFormatFunction = "unformatImage";
        }
    }
}
