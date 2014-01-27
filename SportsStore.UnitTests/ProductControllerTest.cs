using SportsStore.WebUI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using SportsStore.Domain.Abstract;
using System.Web.Mvc;

namespace SportsStore.UnitTests
{
    
    
    /// <summary>
    ///ProductControllerTest のテスト クラスです。すべての
    ///ProductControllerTest 単体テストをここに含めます
    ///</summary>
    [TestClass()]
    public class ProductControllerTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///現在のテストの実行についての情報および機能を
        ///提供するテスト コンテキストを取得または設定します。
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region 追加のテスト属性
        // 
        //テストを作成するときに、次の追加属性を使用することができます:
        //
        //クラスの最初のテストを実行する前にコードを実行するには、ClassInitialize を使用
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //クラスのすべてのテストを実行した後にコードを実行するには、ClassCleanup を使用
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //各テストを実行する前にコードを実行するには、TestInitialize を使用
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //各テストを実行した後にコードを実行するには、TestCleanup を使用
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///List のテスト
        ///</summary>
        // TODO: UrlToTest 属性が ASP.NET ページへの URL を指定していることを確認します (たとえば、
        // http://.../Default.aspx)。これはページ、Web サービス、または WCF サービスのいずれをテストする
        //場合でも、Web サーバー上で単体テストを実行するために必要です。
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\Users\\sari\\Documents\\Visual Studio 2010\\Projects\\SportsStore\\SportsStore.WebUI", "/")]
        [UrlToTest("http://localhost:50776/")]
        public void ListTest()
        {
            IProductRepository productRepository = null; // TODO: 適切な値に初期化してください
            ProductController target = new ProductController(productRepository); // TODO: 適切な値に初期化してください
            int page = 0; // TODO: 適切な値に初期化してください
            ViewResult expected = null; // TODO: 適切な値に初期化してください
            ViewResult actual;
            actual = target.List(page);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("このテストメソッドの正確性を確認します。");
        }
    }
}
