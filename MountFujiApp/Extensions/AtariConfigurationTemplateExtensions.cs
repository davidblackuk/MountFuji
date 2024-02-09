/*
   Mount Fuji - A front end for the Hatari Emulator
   Copyright (C) 2024  David Black

   This program is free software: you can redistribute it and/or modify
   it under the terms of the GNU General Public License as published by
   the Free Software Foundation, either version 3 of the License, or
   (at your option) any later version.

   This program is distributed in the hope that it will be useful,
   but WITHOUT ANY WARRANTY; without even the implied warranty of
   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
   GNU General Public License for more details.

   You should have received a copy of the GNU General Public License
   along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/

namespace MountFuji.Extensions
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
