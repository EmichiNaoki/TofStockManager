using System;
using System.Collections.Generic;
using System.Text;

namespace Tofree.StockManager.Views.Behaviors
{
    using System;
    using System.Windows;

    /// <summary>
    /// ダイアログを開くためのビヘイビアを表します。
    /// </summary>
    internal class OpenDialogBehavior
    {
        #region DataContext 添付プロパティ
        ///<summary>
        ///object型のDataContext添付プロパティを定義します。
        /// </summary>                           
        public static readonly DependencyProperty DataContextProperty
            = DependencyProperty.RegisterAttached(
                "DataContext",                                                /*名称*/
                typeof(object),                                         /*添付プロパティの型*/
                typeof(OpenDialogBehavior),                           /*この添付プロパティを保有するクラス”CommonDialogBehavior”の型*/
                new PropertyMetadata(null)                  /*この添付プロパティのメタ情報、つまり初期値*/
             );

        ///<summary>
        ///DataContext添付プロパティを取得します。
        ///</summary>
        ///<param name="target" >対象とするDependencyObjectを指定します。</param>
        ///<returns>取得した値を返します。</returns>
        public static object GetDataContext(DependencyObject target)
        {
            return (object)target.GetValue(DataContextProperty);
        }


        ///<summary>
        ///DataContext添付プロパティを設定します。
        ///</summary>
        ///<param name="target" >対象とするDependencyObjectを指定します。</param>
        ///<param name="value" >設定する値を指定します。</param>
        public static void SetDataContext(DependencyObject target, object value)
        {
            target.SetValue(DataContextProperty, value);
        }
        #endregion DataContext 添付プロパティ


        #region WindowType 添付プロパティ
        ///<summary>
        ///Type型のWindowType添付プロパティを定義します。
        /// </summary>       

        public static readonly DependencyProperty WindowTypeProperty
            = DependencyProperty.RegisterAttached(
                "WindowType",                                                /*名称*/
                typeof(Type),                                         /*添付プロパティの型*/
                typeof(OpenDialogBehavior),                           /*この添付プロパティを保有するクラス”CommonDialogBehavior”の型*/
                new PropertyMetadata(null)                  /*この添付プロパティのメタ情報、つまり初期値*/
             );

        ///<summary>
        ///WindowType添付プロパティを取得します。
        ///</summary>
        ///<param name="target" >対象とするDependencyObjectを指定します。</param>
        ///<returns>取得した値を返します。</returns>
        public static Type GetWindowType(DependencyObject target)
        {
            return (Type)target.GetValue(WindowTypeProperty);
        }


        ///<summary>
        ///WindowType添付プロパティを設定します。
        ///</summary>
        ///<param name="target" >対象とするDependencyObjectを指定します。</param>
        ///<param name="value" >設定する値を指定します。</param>
        public static void SetWindowType(DependencyObject target, Type value)
        {
            target.SetValue(WindowTypeProperty, value);
        }
        #endregion WindowType 添付プロパティ



        #region Callback 添付プロパティ
        ///<summary>
        ///Action&lt;bool&gt; 型のCallback添付プロパティを定義します。
        ///</summary>
        public static readonly DependencyProperty CallbackProperty
            = DependencyProperty.RegisterAttached(
                "Callback",                                                 /*名称*/
                typeof(Action<bool>),                               /*添付プロパティの型*/
                typeof(OpenDialogBehavior),                               /*この添付プロパティを保有するクラス”CommonDialogBehavior”の型*/
                new PropertyMetadata(null, OnCallbackPropertyChanged)       /*この添付プロパティのメタ情報、つまり初期値*/
             );


        ///<summary>
        ///Callback添付プロパティを取得します。
        ///</summary>
        ///<param name="target" >対象とするDependencyObjectを指定します。つまりは実行したウインドウの要素</param>
        ///<returns>取得した値を返します。</returns>
        public static Action<bool> GetCallback(DependencyObject target)
        {
            return (Action<bool>)target.GetValue(CallbackProperty);
        }


        ///<summary>
        ///Callback添付プロパティを設定します。
        ///</summary>
        ///<param name="target" >対象とするDependencyObjectを指定します。</param>
        ///<param name="value" >設定する値を指定します。</param>
        public static void SetCallback(DependencyObject target, Action<bool> value)
        {
            target.SetValue(CallbackProperty, value);

        }
        #endregion Callback 添付プロパティ



        ///<summary>
        ///Callback 添付プロパティ変更イベントハンドラ
        ///</summary>
        ///<param name="sender" >イベント発行元</param>
        ///<param name="e" >イベント引数</param>
        private static void OnCallbackPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            /*コールバックメソッド*/
            /*sender（「開く」メニュー要素）*/
            var callback = GetCallback(sender);
            if (callback != null)
            {
                var type = GetWindowType(sender);
                var obj = type.InvokeMember(null, System.Reflection.BindingFlags.CreateInstance, null, null, null);
                var child = obj as Window;
                if (child != null)
                {
                    child.DataContext = GetDataContext(sender);
                    var result = child.ShowDialog();
                    callback(result.Value);
                }



            }
        }




    }
}
