using SellTables.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SellTables.ViewModels
{
    public class RegisterChapterViewModel
    {
        public Creative Creative { get; set; }
        [AllowHtml]
        public string Text { get; set; }
        public string Tags { get; set; }
    }
}