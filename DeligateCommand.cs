using System;
using System.Collections.Generic;
using System.Text;

namespace Tofree.StockManager
{
    using System;
    using System.Windows.Input;

    internal class DelegateCommand : ICommand
    {


        #region private フィールド

        ///<summary>
        ///コマンド実行時の処理内容を保持します。
        ///</summary>
        private Action<object> _execute;

        ///<summary>
        ///コマンド実行時可能判別の処理内容を保持します。。
        ///</summary>
        private Func<object, bool> _canExecute;


        #endregion private フィールド



        #region コンストラクタ

        ///<summary>
        ///新しいインスタンスを生成します。
        ///</summary>
        ///<param name="execute">コマンド実行処理を指定します。</param>
        public DelegateCommand(Action<object> execute)
            :this(execute, null)                                //:下↓のコンストラクタを<execute, null>で起動する。
        {
        }

        ///<summary>
        ///新しいインスタンスを生成します。
        ///</summary>
        ///<param name="execute">コマンド実行処理を指定します。</param>
        ///<param name="canExecute">コマンド実行可能判別処理を指定します。</param>
        public DelegateCommand(Action<object> execute, Func<object, bool> canExecute)
        {
            this._execute = execute;
            this._canExecute = canExecute;
        }

        #endregion











        #region ICommandの実装


        ///<summary>
        ///コマンド実行時可能かどうかの判別処理をおこないます。
        ///ICommandクラスからの継承
        ///</summary>
        ///<param name="parameter">判別処理に対するパラメータを指定します。</param>
        ///<return>実行可能な場合にtrueをかえします。</return>>
        public bool CanExecute(object parameter)
        {
            return (this._canExecute != null) ? this._canExecute(parameter) : true;　//?はif IsNotNullの簡略系。この場合、_canExecuteがNullでないならば真となり、以降の処理を行う。

        }




        ///<summary>
        ///実行時可能かどうかの判別処理に関する状態が変更されたときに発生します。
        ///</summary>
        public event EventHandler CanExecuteChanged;


        ///<summary>
        ///CanExecuteChangedイベントを発行します。
        ///</summary>
        public void RaiseCanExecuteChanged()
        {
            var h = this.CanExecuteChanged;
            if (h != null) h(this, EventArgs.Empty);

        }



        ///<summary>
        ///コマンドが実行されたときの処理を行います。
        ///</summary>
        ///<param name="parameter">コマンドに対するパラメータを指定します。
        public void Execute(object parameter)
        {
            if (this._execute != null)
                this._execute(parameter);
        }

        #endregion ICommandの実装



    }
}
