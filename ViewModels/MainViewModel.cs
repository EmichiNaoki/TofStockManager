﻿namespace Tofree.StockManager.ViewModels
{

    using System;
    using System.IO;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Security.Cryptography.X509Certificates;
    using System.Windows.Documents;
    using Tofree.StockManager.Models;

    using Microsoft.Extensions.Configuration;
    using Microsoft.Data.SqlClient;
    using System.Diagnostics;
    using Microsoft.VisualBasic.CompilerServices;

    using Microsoft.EntityFrameworkCore;


    using Microsoft.Win32;

    using System.Collections;

    using static System.Console;
    using System.Windows.Input;
    using System.Windows;

    ///<summary
    ///MainViewウインドウに対するデータコンテキストを表します
    ///</summary>
    internal class MainViewModel : NotificationObject
    {

        public MainViewModel()
        {











            //自宅PCかどうか確認して
            string machineName = Environment.MachineName;
            if (machineName == "NAOTAB")
            {
                for (int i = 0; i < 10; i++)
                {
                    var product = new Product()
                    {
                        ProductNo = "sam-ple-" + i,
                        ProductName = "これはサンプルです" + i,
                        StockQTY = i * 10,
                        ModDate = DateTime.Today.ToString("yyyy/MM/dd"),
                        ModTime = DateTime.Now.ToString("HH:mm:ss")
                    };

                    ProductsList.Add(product);
                }
                return;
                
            }

                
            DBManager dBManager = new DBManager();               
            var sqlstr = @"select * from TEST.T_STOCK;";
            try
            {
                SqlDataReader dr = dBManager.ExecuteQuery(sqlstr);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        var product = new Product()
                        {
                                ProductNo = dr["CODE"].ToString(),
                                ProductName = dr["Name"].ToString(),
                                StockQTY = (int)dr["QTY"],
                                ModDate = DateTime.Today.ToString("yyyy/MM/dd"),
                                ModTime = DateTime.Now.ToString("HH:mm:ss"),
                            };

                            ProductsList.Add(product);
                        }
                    }


                    dr.Close();


                }
                catch (Exception exc)
                {
                    Debug.WriteLine(exc.Message);
                }
                finally
                {

                    dBManager.Close();
                }


            



            


            //var db = new AppDbContext();
            //var http = new System.Net.Http.HttpClient();

            ////フィードを適当なサイトから取得
            //foreach (var targetUrl in
            //    new[] {
            //        "http://www3.asahi.com/rss/index.rdf",
            //        "http://rss.rssad.jp/rss/codezine/new/20/index.xml",
            //    })

            //{
            //    //フィードxmlをDL & Parse
            //    //xmlは名前空間で面倒が生じないよう名前空間情報を除染
            //    var rssTxt = http.GetStringAsync(targetUrl).Result;
            //    var rss = System.Xml.Linq.XElement.Parse(rssTxt);
            //    foreach (var item in rss.Descendants())
            //        item.Name = item.Name.LocalName;


            //    //フィードの記事をModelオブジェクトへ移し替える
            //    var articles = rss
            //        .Descendants("item")
            //        .Select(item =>
            //            new Article()
            //            {
            //                Title = item.Element("title").Value,
            //                LinkUrl = item.Element("link").Value,
            //                Description = item.Element("description").Value,
            //                ChannelTitle = rss.Element("channel").Element("title").Value,
            //            });

            //    //DBに未追加の記事をDBへ保存する
            //    foreach (var item in articles)
            //    {
            //        if (db.Article.Any(_ => _.LinkUrl == item.LinkUrl))
            //            continue;

            //        Console.WriteLine(item.Title);
            //        db.Article.Add(item);
            //    }
            //    //DBへの保存を確定
            //    db.SaveChanges();
            //    Console.WriteLine("終了");
            //    Console.Read();

            //}




        }
        private DelegateCommand _listViewItem_MouseDoubleClick;

        ///<summary>
        ///LISTVIEWをダブルクリックしたときの動作
        /// </summary>
        private DelegateCommand ListViewItem_MouseDoubleClick
        {
            get
            {
                return this._listViewItem_MouseDoubleClick ?? (this._listViewItem_MouseDoubleClick = new DelegateCommand(
                    _ =>
                    {
                        System.Diagnostics.Debug.WriteLine("daburukurikku ");
                    }));
            }
                
        
            // sender がダブルクリックされた項目
            //var targetItem = sender;

            // データバインディングを使っているなら、
            // DataContext からデータを取得できる
            //Person p = (Person)targetItem.DataContext;
        }




        private DelegateCommand _lastastNameCM_Click;
        public DelegateCommand LastNameCM_Click
        {
            get
            {
                return this._lastastNameCM_Click ?? (this._lastastNameCM_Click = new DelegateCommand(
                    _ =>
                    {
                        System.Diagnostics.Debug.WriteLine("LastNameCM_Click");
                    }));

            }
        
            

           
        }




        private DelegateCommand _addProductsListCommand;
        ///<summary>
        ///追加コマンドを取得します。
        /// </summary>
        public DelegateCommand AddProductsListCommand
        {
            get
            {
                return this._addProductsListCommand ?? (this._addProductsListCommand = new DelegateCommand(
                    _ =>
                    {
                        this._count++;
                        var product = new Product()
                        {
                            ProductNo = "sam-ple-" + this._count,
                            ProductName = "これはサンプルです" + this._count,
                            StockQTY = _count * 10,
                            ModDate = DateTime.Today.ToString("yyyy/MM/dd"),
                            ModTime = DateTime.Now.ToString("HH:mm:ss")
                        };
                        this.ProductsList.Add(product);

                        System.Diagnostics.Debug.WriteLine(product.ProductName + "を追加しました。");
                    }));
            }
        }




        #region LINQ for Entities

        //エンティティクラス
        public class Article
        {
        public int Id { get; set; }
        public string LinkUrl { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ChannelTitle { get; set; }
    }

    //コンテキストクラス
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            //ここでは接続先のDB名はhellocoredbとする
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=hellocoredb;");
        }
        public DbSet<Article> Article { get; set; }
    }






        #endregion LINQ for Entities




        public ObservableCollection<Product> _productsList = new ObservableCollection<Product>();
        ///<summary
        ///在庫情報のコレクションを取得します。
        ///</summary>
        public ObservableCollection<Product> ProductsList

        {
            get { return this._productsList; }
            private set { SetProperty(ref this._productsList, value); }
        }

        






        public ObservableCollection<Person> _people = new ObservableCollection<Person>();        
        ///<summary
        ///人物情報のコレクションを取得します。
        ///</summary>
        public ObservableCollection<Person> People
        {
            get { return this._people; }
            private set { SetProperty(ref this._people, value); }
        }

        private int _count;

        private DelegateCommand _addComand;
        ///<summary>
        ///追加コマンドを取得します。
        /// </summary>
        public DelegateCommand AddCommand
        {
            get
            {
                return this._addComand ?? (this._addComand = new DelegateCommand(
                    _ =>
                    {
                        this._count++;
                        var person = new Person()
                        {
                            FamilyName = "田中",
                            FirstName = this._count + "太郎",
                            Age = this._count,
                        };
                        this.People.Add(person);
                        this.DeleteCommand.RaiseCanExecuteChanged();

                        System.Diagnostics.Debug.WriteLine(person.FullName + "を追加しました。");
                    }));
            }
        }


        private DelegateCommand _deleteCommand;
        ///<summary>
        ///削除コマンドを取得します。
        /// </summary>
        public DelegateCommand DeleteCommand
        {
            get
            {
                return this._deleteCommand ?? (this._deleteCommand = new DelegateCommand(
                    p =>
                    {
                        this.People.Remove(p as Person);
                        this.DeleteCommand.RaiseCanExecuteChanged();
                    } ));
            }
        }


 

        #region ファイルを開く
        private DelegateCommand _openFileCommand;
        ///<summary>
        ///ファイルを開くコマンドを取得します。
        /// </summary>
        public DelegateCommand OpenFileCommand
        {
            get
            {
                return this._openFileCommand ?? (this._openFileCommand = new DelegateCommand
                    (
                    _ =>
                    {
                        ///System.Diagnostics.Debug.WriteLine("ファイルを開きます。");

                        this.DialogCallback = OnDialogCallback;

                    }));
            }
        }





        private Action<bool, string> _dialogCallback;
        ///<summary>
        ///ダイアログに対するコールバックを取得します。
        ///</summary>
        public Action<bool, string> DialogCallback
        {
            get { return this._dialogCallback; }
            private set { SetProperty(ref this._dialogCallback, value); }
        }



        ///<summary>
        ///ダイアログに対するコールバック処理を行います。
        ///</summary>
        ///<param name="isOK">ダイアログの結果を指定します。</param>
        ///<param name="filePath">ファイルのフルパスを指定します。</param>
        private void OnDialogCallback(bool isOK, string filepath)
        {
            this.DialogCallback = null;
            System.Diagnostics.Debug.WriteLine("コールバック処理を行います。");
            System.Diagnostics.Debug.WriteLine("isOK =" + isOK + ", filepath = " + filepath);
        }




        #endregion ファイルを開く


        #region アプリケーションを終了する

        public Func<bool> ClosingCallback
        {
            get { return OnExit; }
        }



        private DelegateCommand _exitCommand;
        ///<summary>
        ///アプリケーション終了コマンドを取得します。
        ///</summary>
        public DelegateCommand ExitCommand
        {
            get
            {
                return this._exitCommand ?? (this._exitCommand = new DelegateCommand(
                    _ =>
                    {
                        OnExit();
                    }));
            }
        }

        ///<summary>
        ///アプリケーションを終了します。
        ///</summary>

        private bool OnExit()
        {
            if (this._counter < 3)
            {
                this._counter++;
                return false;
            }
            App.Current.Shutdown();
            return true;
        }

        private int _counter;
        #endregion アプリケーションを終了する

        #region バージョン情報を表示する
        private VersionViewModel _versionViewModel = new VersionViewModel();
        ///<summary>
        ///VersionViewウインドウに対するデータコンテキストを取得します
        ///</summary>
        public VersionViewModel VersionViewModel
        {
            get { return this._versionViewModel; }
        }


        private DelegateCommand _versionDialogCommand;
        ///<summary>
        ///バージョン情報表示コマンドを取得します。
        ///</summary>
        public DelegateCommand VersionDialogCommand
        {
            get
            {
                return this._versionDialogCommand ?? (this._versionDialogCommand = new DelegateCommand(
                    _ =>
                    {
                        this.VersionDialogCallback = OnVersionDialog;
                    }));

            }
        }


        private Action<bool> _versionDialogCallback;
        ///<summary>
        ///バージョン情報コールバックを取得します。
        /// </summary>
        public Action<bool> VersionDialogCallback
        {
            get { return this._versionDialogCallback; }
            private set { SetProperty(ref this._versionDialogCallback, value); }
        }


        ///<summary>
        ///バージョン情報表示コールバック処理を行います。
        /// </summary>
        /// <param name="result"></param>
        private void OnVersionDialog(bool result)
        {
            this.VersionDialogCallback = null;
            System.Diagnostics.Debug.WriteLine(result);
        }

        #endregion バージョン情報を取得する




    }
}