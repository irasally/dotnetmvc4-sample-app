using System;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using Moq;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Controllers;

namespace SportsStore.UnitTests
{
    
    
    /// <summary>
    ///AdminControllerTest のテスト クラスです。すべての
    ///AdminControllerTest 単体テストをここに含めます
    ///</summary>
    [TestClass()]
    public class AdminControllerTest
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
        public void Index_Contains_All_Products()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]{
                new Product{ProductID = 1, Name = "p1" },
                new Product{ProductID = 2, Name = "p2" },
                new Product{ProductID = 3, Name = "p3" },
            }.AsQueryable());

            AdminController target = new AdminController(mock.Object);

            Product[] actual = ((IEnumerable<Product>)target.Index().ViewData.Model).ToArray();

            Assert.AreEqual(actual.Length, 3);
            Assert.AreEqual(actual[0].Name, "p1");
            Assert.AreEqual(actual[1].Name, "p2");
            Assert.AreEqual(actual[2].Name, "p3");
        }

        [TestMethod]
        public void Can_Edit_Product()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]{
                new Product{ProductID = 1, Name = "p1" },
                new Product{ProductID = 2, Name = "p2" },
                new Product{ProductID = 3, Name = "p3" },
            }.AsQueryable());

            AdminController target = new AdminController(mock.Object);

            Product actual = target.Edit(2).ViewData.Model as Product;
            Assert.AreEqual(actual.ProductID, 2);
            Assert.AreEqual(actual.Name, "p2");
        }

        [TestMethod]
        public void Cannot_Edit_Notexistent_Product()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]{
                new Product{ProductID = 1, Name = "p1" },
                new Product{ProductID = 2, Name = "p2" },
                new Product{ProductID = 3, Name = "p3" },
            }.AsQueryable());

            AdminController target = new AdminController(mock.Object);

            Product actual = target.Edit(9).ViewData.Model as Product;

            Assert.IsNull(actual);
        }

        [TestMethod]
        public void Can_Save_Valid_Changes()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            Product product = new Product { Name = "Test" };
            AdminController target = new AdminController(mock.Object);
            
            ActionResult actual = target.Edit(product);

            mock.Verify(m => m.SaveProduct(product));
            Assert.IsNotInstanceOfType(actual, typeof(ViewResult));
        }

        [TestMethod]
        public void Cannot_Save_Invalid_Changes()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            Product product = new Product { Name = "Test" };
            AdminController target = new AdminController(mock.Object);
            target.ModelState.AddModelError("error", "error");

            ActionResult actual = target.Edit(product);

            mock.Verify(m => m.SaveProduct(It.IsAny<Product>()), Times.Never());
            Assert.IsInstanceOfType(actual, typeof(ViewResult));
        }

        [TestMethod]
        public void Can_Delete_Valid_Products()
        {
            Product product = new Product { ProductID = 2, Name = "Test" };
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns( new Product[] {
                new Product{ProductID = 1, Name = "p1" },
                product,
                new Product{ProductID = 3, Name = "p3" }
            }.AsQueryable());

            AdminController target = new AdminController(mock.Object);

            target.Delete(product.ProductID);

            mock.Verify(m => m.DeleteProduct(product));
        }

        [TestMethod]
        public void Cannot_Delete_Invalid_Products()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product{ProductID = 1, Name = "p1" },
                new Product{ProductID = 3, Name = "p2" },
                new Product{ProductID = 3, Name = "p3" }
            }.AsQueryable());

            AdminController target = new AdminController(mock.Object);
            
            target.Delete(999);

            mock.Verify(m => m.DeleteProduct(It.IsAny<Product>()), Times.Never());
        }
    }
}
