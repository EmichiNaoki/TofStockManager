using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;

using System.Windows;

using System.Windows.Input;

namespace Tofree.StockManager.Views.Behaviors
{
    internal class OpenProductDetail : DelegateCommand
    {









        #region Callback 添付プロパティ
        ///<summary>
        ///Action&lt;bool, string&gt; 型のCallback添付プロパティを定義します。
        ///</summary>
        public static readonly DependencyProperty CallbackProperty
            = DependencyProperty.RegisterAttached(
                "Callback",                                                 /*名称*/
                typeof(Action<bool, string>),                               /*添付プロパティの型*/
                typeof(CommonDialogBehavior),                               /*この添付プロパティを保有するクラス”CommonDialogBehavior”の型*/
                new PropertyMetadata(null, OnCallbackPropertyChanged)       /*この添付プロパティのメタ情報、つまり初期値*/
             );


    


        ///<summary>
        ///Callback添付プロパティを取得します。
        ///</summary>
        ///<param name="target" >対象とするDependencyObjectを指定します。つまりは実行したウインドウの要素</param>
        ///<returns>取得した値を返します。</returns>
        public static Action<bool, string> GetCallback(DependencyObject target)
        {
            return (Action<bool, string>)target.GetValue(CallbackProperty);
        }


        ///<summary>
        ///Callback添付プロパティを設定します。
        ///</summary>
        ///<param name="target" >対象とするDependencyObjectを指定します。</param>
        ///<param name="value" >設定する値を指定します。</param>
        public static void SetCallback(DependencyObject target, Action<bool, string> value)
        {
            target.SetValue(CallbackProperty, value);

        }
        #endregion Callback 添付プロパティ







        #region Title 添付プロパティ
        ///<summary>
        ///string型のTitle添付プロパティを定義します。
        ///</summary>
        public static readonly DependencyProperty TitleProperty
            = DependencyProperty.RegisterAttached(
                "Title",                                                /*名称*/
                typeof(string),                                         /*添付プロパティの型*/
                typeof(CommonDialogBehavior),                           /*この添付プロパティを保有するクラス”CommonDialogBehavior”の型*/
                new PropertyMetadata("ファイルを開く")                  /*この添付プロパティのメタ情報、つまり初期値*/
             );

        ///<summary>
        ///Title添付プロパティを取得します。
        ///</summary>
        ///<param name="target" >対象とするDependencyObjectを指定します。つまりは実行したウインドウの要素</param>
        ///<returns>取得した値を返します。</returns>
        public static string GetTitle(DependencyObject target)
        {
            return (string)target.GetValue(TitleProperty);
        }


        ///<summary>
        ///Title添付プロパティを設定します。
        ///</summary>
        ///<param name="target" >対象とするDependencyObjectを指定します。</param>
        ///<param name="value" >設定する値を指定します。</param>
        public static void SetTitle(DependencyObject target, string value)
        {
            target.SetValue(TitleProperty, value);
        }
        #endregion Title 添付プロパティ









        #region Filter 添付プロパティ
        ///<summary>
        ///string型のFilter添付プロパティを定義します。
        ///</summary>
        public static readonly DependencyProperty FilterProperty
            = DependencyProperty.RegisterAttached(
                "Filter",                                                /*名称*/
                typeof(string),                                         /*添付プロパティの型*/
                typeof(CommonDialogBehavior),                           /*この添付プロパティを保有するクラス”CommonDialogBehavior”の型*/
                new PropertyMetadata("すべてのファイル（*.*）|*.*")                  /*この添付プロパティのメタ情報、つまり初期値*/
             );

        ///<summary>
        ///Filter添付プロパティを取得します。
        ///</summary>
        ///<param name="target" >対象とするDependencyObjectを指定します。つまりは実行したウインドウの要素</param>
        ///<returns>取得した値を返します。</returns>
        public static string GetFilter(DependencyObject target)
        {
            return (string)target.GetValue(FilterProperty);
        }


        ///<summary>
        ///Filter添付プロパティを設定します。
        ///</summary>
        ///<param name="target" >対象とするDependencyObjectを指定します。</param>
        ///<param name="value" >設定する値を指定します。</param>
        public static void SetFilter(DependencyObject target, string value)
        {
            target.SetValue(FilterProperty, value);
        }
        #endregion Filter 添付プロパティ
















        #region Multiselect 添付プロパティ
        ///<summary>
        ///string型のMultiselect添付プロパティを定義します。
        ///</summary>
        public static readonly DependencyProperty MultiselectProperty
            = DependencyProperty.RegisterAttached(
                "Multiselect",                                                /*名称*/
                typeof(bool),                                         /*添付プロパティの型*/
                typeof(CommonDialogBehavior),                           /*この添付プロパティを保有するクラス”CommonDialogBehavior”の型*/
                new PropertyMetadata(true)                  /*この添付プロパティのメタ情報、つまり初期値*/
             );

        ///<summary>
        ///Multiselect添付プロパティを取得します。
        ///</summary>
        ///<param name="target" >対象とするDependencyObjectを指定します。つまりは実行したウインドウの要素</param>
        ///<returns>取得した値を返します。</returns>
        public static bool GetMultiselect(DependencyObject target)
        {
            return (bool)target.GetValue(MultiselectProperty);
        }


        ///<summary>
        ///Filter添付プロパティを設定します。
        ///</summary>
        ///<param name="target" >対象とするDependencyObjectを指定します。</param>
        ///<param name="value" >設定する値を指定します。</param>
        public static void SetMultiselect(DependencyObject target, bool value)
        {
            target.SetValue(FilterProperty, value);
        }
        #endregion Multiselect 添付プロパティ









        ///<summary>
        ///Call back 添付プロパティ変更イベントハンドラ
        ///</summary>
        ///<param name="sender" >イベント発行元</param>
        ///<param name="e" >イベント引数</param>
        private static void OnCallbackPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            /*コールバックメソッド*/
            /*sender（「開く」メニュー要素）*/
            var Callback = GetCallback(sender);
            if (Callback != null)
            {
                var dlg = new OpenFileDialog()
                {
                    Title = GetTitle(sender),
                    Filter = GetFilter(sender),
                    Multiselect = GetMultiselect(sender),

                };
                var owner = Window.GetWindow(sender);
                var result = dlg.ShowDialog(owner);     /*ownerを指定するとそのウインドウのそばにダイアログが表示される*/
                Callback(result.Value, dlg.FileName);

            }
        }



    }

}

