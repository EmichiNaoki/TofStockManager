using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace Tofree.StockManager.Models
{
    internal class Calculator
    {
        ///<summary>
        ///被演算項を取得または設定します。
        ///</summary>
        public double Lhs { get; set; }



        ///<summary>
        ///演算項を取得または設定します。
        ///</summary>
        public double Rhs { get; set; }


        ///<summary>
        ///計算結果を取得または設定します。
        ///</summary>
        public double Result { get; set; }

        ///<summary>
        ///割り算を行います
        ///</summary>
        public void ExecuteDiv()
        {
            this.Result = this.Lhs / this.Rhs;
        }


    }
}
