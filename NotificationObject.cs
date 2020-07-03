using System;
using System.Collections.Generic;
using System.Text;

namespace Tofree.StockManager
{

    using System.ComponentModel;
    using System.Runtime.CompilerServices;


    internal abstract class NotificationObject : INotifyPropertyChanged
    {




        #region INotifyPropertyChangedの実装
        ///<summary>
        ///プロパティに変更があった場合に発生します。
        ///</summary>

        public event PropertyChangedEventHandler PropertyChanged;

        ///<summary>
        ///PropertyChangedイベントを発行する。
        ///</summary>
        ///<param name="propertyName"> プロパティ値に変更があったプロパティ名を指定します。</param>
        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            var h = this.PropertyChanged;
            if (h != null) h(this, new PropertyChangedEventArgs(propertyName));
        }

        ///<summary>
        ///プロパティ値を変更するヘルパです。
        ///</summary>
        ///<typeparam name="T">プロパティの型を表します。</typeparam>
        ///<param name="target"> 変更するプロパティの実態をref参照します</param>
        ///<param name="value"></param>
        ///<param name="propertyName"> プロパティ名を指定します。</param>
        ///<returns>プロパティ値に変更があった場合にtrueを返します。</returns>
        protected bool SetProperty<T>(ref T target, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(target, value))
                return false;
            target = value;
            RaisePropertyChanged(propertyName);
            return true;

        }




        #endregion INotifyPropertyChangedの実装

    }
}
