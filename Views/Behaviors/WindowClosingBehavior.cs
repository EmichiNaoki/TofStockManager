namespace Tofree.StockManager.Views.Behaviors
{
    using System;
    using System.ComponentModel;
    using System.Windows;


    internal class WindowClosingBehavior
    {
        #region Callback 添付プロパティ
        ///<summary>
        ///Func&lt;bool&gt; 型のCallback添付プロパティを定義します。
        ///</summary>
        public static readonly DependencyProperty CallbackProperty
            = DependencyProperty.RegisterAttached(
                "Callback",                                                 /*名称*/
                typeof(Func<bool>),                                         /*添付プロパティの型*/
                typeof(WindowClosingBehavior),                              /*この添付プロパティを保有するクラス”CommonDialogBehavior”の型*/
                new PropertyMetadata(null, OnIsEnabledPropertyChanged)      /*この添付プロパティのメタ情報、つまり初期値*/
             );


        ///<summary>
        ///Callback添付プロパティを取得します。
        ///</summary>
        ///<param name="target" >対象とするDependencyObjectを指定します。つまりは実行したウインドウの要素</param>
        ///<returns>取得した値を返します。</returns>
        public static Func<bool> GetCallback(DependencyObject target)
        {
            return (Func<bool>)target.GetValue(CallbackProperty);
        }


        ///<summary>
        ///Callback添付プロパティを設定します。
        ///</summary>
        ///<param name="target" >対象とするDependencyObjectを指定します。</param>
        ///<param name="value" >設定する値を指定します。</param>
        public static void SetCallback(DependencyObject target, Func<bool> value)
        {
            target.SetValue(CallbackProperty, value);

        }
        #endregion Callback 添付プロパティ


        /// <summary>
        /// .Callback添付プロパティ変更イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private static void OnIsEnabledPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var w = sender as Window;
            if (w != null)
            {
                var callback = GetCallback(w);
                if ((callback != null) && (e.OldValue == null))
                {
                    w.Closing += OnClosing;
                }
                else if (callback == null)
                {
                    w.Closing -= OnClosing;
                }
            }
        }

        /// <summary>
        /// Closingイベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private static void OnClosing(Object sender, CancelEventArgs e)
        {

            var callback = GetCallback(sender as DependencyObject);
            if (callback != null)
            {
                //コールバック処理がfalseの時キャンセルする
                e.Cancel = !callback();
            }
        }
    }
}
