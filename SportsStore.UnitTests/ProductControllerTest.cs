using System;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using Moq;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Controllers;
using System.Collections;
using System.Collections.Generic;
using SportsStore.WebUI.Models;

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


        [TestMethod]
        public void Can_Paginate()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]{
                new Product{ProductID = 1, Name ="p1"},
                new Product{ProductID = 2, Name ="p2"},
                new Product{ProductID = 3, Name ="p3"},
                new Product{ProductID = 4, Name ="p4"},
                new Product{ProductID = 5, Name ="p5"}
            }.AsQueryable());

            ProductController target = new ProductController(mock.Object);
            target.PageSize = 3;

            ProductsListViewModel result = (ProductsListViewModel)target.List(null, 2).Model;
            Product[] actual = result.Products.ToArray();

            Assert.AreEqual(actual.Length, 2);
            Assert.AreEqual(actual[0].Name, "p4");
            Assert.AreEqual(actual[1].Name, "p5");
        }

        [TestMethod]
        public void Can_Send_Pagination_View_Model()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]{
                new Product{ProductID = 1, Name ="p1"},
                new Product{ProductID = 2, Name ="p2"},
                new Product{ProductID = 3, Name ="p3"},
                new Product{ProductID = 4, Name ="p4"},
                new Product{ProductID = 5, Name ="p5"}
            }.AsQueryable());

            ProductController target = new ProductController(mock.Object);
            target.PageSize = 3;

            ProductsListViewModel result = (ProductsListViewModel)target.List(null, 2).Model;
            PagingInfo actual = result.PagingInfo;

            Assert.AreEqual(actual.CurrentPage, 2);
            Assert.AreEqual(actual.ItemsPerPage, 3);
            Assert.AreEqual(actual.TotalItems, 5);
            Assert.AreEqual(actual.TotalPages, 2);
        }

        [TestMethod]
        public void Can_Filter_Products()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]{
                new Product{ProductID = 1, Name ="p1", Category="Cat1"},
                new Product{ProductID = 2, Name ="p2", Category="Cat2"},
                new Product{ProductID = 3, Name ="p3", Category="Cat2"},
                new Product{ProductID = 4, Name ="p4", Category="Cat1"},
                new Product{ProductID = 5, Name ="p5", Category="Cat3"}
            }.AsQueryable());

            ProductController target = new ProductController(mock.Object);
            target.PageSize = 3;

            ProductsListViewModel result = (ProductsListViewModel)target.List("Cat1", 1).Model;
            Product[] actual = result.Products.ToArray();

            Assert.AreEqual(result.CurrentCategory, "Cat1");
            Assert.AreEqual(actual.Length, 2);
            Assert.AreEqual(actual[0].Name, "p1");
            Assert.AreEqual(actual[0].Category, "Cat1");
            Assert.AreEqual(actual[1].Name, "p4");
            Assert.AreEqual(actual[1].Category, "Cat1");
        }
    }
}
