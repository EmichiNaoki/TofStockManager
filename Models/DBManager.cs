using System;
using System.Collections.Generic;
using System.Text;


using System.Linq;

using System.Threading.Tasks;

using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;




using Microsoft.EntityFrameworkCore;


namespace Tofree.StockManager.Models
{


    /// <summary>
    /// データベース操作を管理するクラス
    /// </summary>
    internal class DBManager
    {


        private SqlConnection sqlConnection;
        private SqlTransaction sqlTransaction;

        /// <summary>
        /// コンストラクタ（DB接続）
        /// </summary>
        public DBManager()
        {
            // 接続文字列をjsconfig.jsonから取得
            string constr = Config.configuration.GetConnectionString("TOFDEV");


            // SqlConnection の新しいインスタンスを生成 (接続文字列を指定)
            this.sqlConnection = new SqlConnection(constr);

            // データベース接続を開く
            this.sqlConnection.Open();
        }

        /// <summary>
        /// DB切断
        /// </summary>
        public void Close()
        {
            this.sqlConnection.Close();
            this.sqlConnection.Dispose();
        }

        /// <summary>
        /// トランザクション開始
        /// </summary>
        public void BeginTran()
        {
            this.sqlTransaction = this.sqlConnection.BeginTransaction();
        }

        /// <summary>
        /// トランザクション　コミット
        /// </summary>
        public void CommitTran()
        {
            if (this.sqlTransaction.Connection != null)
            {
                this.sqlTransaction.Commit();
                this.sqlTransaction.Dispose();
            }
        }

        /// <summary>
        /// トランザクション　ロールバック
        /// </summary>
        public void RollBack()
        {
            if (this.sqlTransaction.Connection != null)
            {
                this.sqlTransaction.Rollback();
                this.sqlTransaction.Dispose();
            }
        }

        /// <summary>
        /// クエリー実行(OUTPUT項目あり)
        /// <para name="query">SQL文</para>
        /// <para name="paramDict">SQLパラメータ</para>
        /// </summary>
        public SqlDataReader ExecuteQuery(string query, Dictionary<string, Object> paramDict) //SQLパラメータとは、
        {
            SqlCommand sqlCom = new SqlCommand();

            //クエリー送信先、トランザクションの指定
            sqlCom.Connection = this.sqlConnection;
            sqlCom.Transaction = this.sqlTransaction;

            sqlCom.CommandText = query;
            foreach (KeyValuePair<string, Object> item in paramDict)
            {
                sqlCom.Parameters.Add(new SqlParameter(item.Key, item.Value));
            }

            // SQLを実行
            SqlDataReader reader = sqlCom.ExecuteReader();

            return reader;
        }





        /// <summary>
        /// クエリー実行(OUTPUT項目あり)
        /// <para name="query">SQL文</para>
        /// </summary>
        public SqlDataReader ExecuteQuery(string query)
        {
            return this.ExecuteQuery(query, new Dictionary<string, Object>());
        }






        /// <summary>
        /// クエリー実行(OUTPUT項目なし)
        /// <para name="query">SQL文</para>
        /// <para name="paramDict">SQLパラメータ</para>
        /// </summary>
        public void ExecuteNonQuery(string query, Dictionary<string, Object> paramDict)
        {
            SqlCommand sqlCom = new SqlCommand();

            //クエリー送信先、トランザクションの指定
            sqlCom.Connection = this.sqlConnection;
            sqlCom.Transaction = this.sqlTransaction;

            sqlCom.CommandText = query;
            foreach (KeyValuePair<string, Object> item in paramDict)
            {
                sqlCom.Parameters.Add(new SqlParameter(item.Key, item.Value));
            }

            // SQLを実行
            sqlCom.ExecuteNonQuery();
        }
    }

    public class Program
    {
        public Program(string[] args)
        {
            //登録すれば必要な場面(Win上で実行した時)で使用される。
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        }
    }
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

            // 接続文字列をjsconfig.jsonから取得
            string constr = Config.configuration.GetConnectionString("TOFDEV");
            //ここでは接続先のDB名はhellocoredbとする
            optionsBuilder.UseSqlServer(constr);
        }

        public DbSet<Article> Article { get; set; }
    }


}