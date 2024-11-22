using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _23DH110809_MyStore.Models;
using PagedList.Mvc;
using WebBanHang.Models;

namespace WebBanHang.Models.ViewModel
{
    public class ProductSearchVM
    {
        public string searchTerm { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public string SortOrder { get; set; }
        public int PageNumber { get; set; }
        public int PagedList { get; set; } = 10;
        public PagedList.IPagedList<Product> Products { get; set; }
    }
}