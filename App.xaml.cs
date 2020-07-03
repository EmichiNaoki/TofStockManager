using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Tofree.StockManager
{

    using System.Windows;
    using Tofree.StockManager.ViewModels;
    using Tofree.StockManager.Views;

    /// <summary>
    /// App.xamlの相互作用ロジック
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);


            //ウインドウをインスタンス化します。
            var w = new MainView();


            //ウインドウに対するViewModelをインスタンス化します
            var vm = new MainViewModel();


            //ウインドウに対するViewModelをデータコンテキストに指定します。
            w.DataContext = vm;


            //ウインドウを表示します
            w.Show();


        }
    }
}
