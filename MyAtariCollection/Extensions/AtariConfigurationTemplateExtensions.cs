using System;
using Microsoft.Extensions.Primitives;

namespace MyAtariCollection.Extensions
{
	public static class AtariConfigurationTemplateExtensions
	{

		public static string DisplayText(this AtariSystemType systemType)
		{
			string res = string.Empty;
			switch (systemType)
			{
				case AtariSystemType.ST:
					res = "ST";
					break;
				case AtariSystemType.MegaST:
					res = "Mega ST";
					break;
				case AtariSystemType.STE:
					res = "STE";
					break;
				case AtariSystemType.MegaSTE:
					res = "Mega STE";
					break;
				case AtariSystemType.TT:
					res = "TT";
					break;
				case AtariSystemType.Falcon:
					res = "Falcon";
					break;
			}

			return res;
		}

		
		
	}
}
