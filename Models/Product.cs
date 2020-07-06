using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Tofree.StockManager.Models
{

    /// <summary>
    /// 商品情報
    /// </summary>

    internal class Product
    {


        /// <summary>
        /// 商品番号
        /// </summary>
        public string ProductNo { get; set; }

        /// <summary>
        /// 商品名
        /// </summary>
        public string ProductName { get; set; }


        /// <summary>
        /// 在庫数
        /// </summary>
        public int StockQTY { get; set; }


        private DateTime _modDate { get; set; }
        /// <summary>
        /// 更新日
        /// </summary>
        public string ModDate
        {
            get { return this._modDate.ToString("yyyy/MM/dd"); }
            set { _modDate = DateTime.Parse(value); }
        }



        private DateTime _modTime { get; set; }
        /// <summary>
        /// 更新時間
        /// </summary>
        public string ModTime
        {
            get { return this._modTime.ToString("HH:mm:ss"); }
            set { _modTime = DateTime.Parse(value); }
        }
    }
}
