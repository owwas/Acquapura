﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Acquapura
{
	public static class UrlHelperExtensions
	{
		public static string ContentVersioned(this UrlHelper self, string contentPath)
		{
			var BuildVersion = ConfigurationManager.AppSettings["BuildVersion"];
			string versionedContentPath = contentPath + "?v=" + BuildVersion;
			return self.Content(versionedContentPath);
		}
	}
}