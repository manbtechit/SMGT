﻿using SalesApp.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesApp.Model
{

    public class MenuMasterMenuItem
    {
        public MenuMasterMenuItem()
        {
          //  TargetType = typeof(MenuMasterDetail);
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public Type TargetType { get; set; }
    }
}