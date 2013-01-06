using System;

namespace Photon.Web.Extensions
{
	public static class StringExtensions
	{
		public static String FormatWith(this string str, params object[] objs )
		{
			return string.Format(str, objs);
		}
	}
}
