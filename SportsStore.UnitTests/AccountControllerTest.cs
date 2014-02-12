using SportsStore.WebUI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using SportsStore.WebUI.Infrastructure.Abstract;
using System.Web.Mvc;
using Moq;
using SportsStore.WebUI.Models;

namespace SportsStore.UnitTests
{


    /// <summary>
    ///AccountControllerTest のテスト クラスです。すべての
    ///AccountControllerTest 単体テストをここに含めます
    ///</summary>
    [TestClass()]
    public class AccountControllerTest
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
        public void Can_Login_With_Valid_Credentials()
        {
            Mock<IAuthProvider> mock = new Mock<IAuthProvider>();
            mock.Setup(m => m.Authenticate("admin", "secret")).Returns(true);

            LogOnViewModel model = new LogOnViewModel
            {
                UserName = "admin",
                Password = "secret"
            };

            AccountController target = new AccountController(mock.Object);

            ActionResult actual = target.LogOn(model, "/MyURL");

            Assert.IsInstanceOfType(actual, typeof(RedirectResult));
            Assert.AreEqual(((RedirectResult)actual).Url, "/MyURL");
        }

        [TestMethod]
        public void Cannot_Login_With_Invalid_Credentials()
        {
            Mock<IAuthProvider> mock = new Mock<IAuthProvider>();
            mock.Setup(m => m.Authenticate("badUser", "badPass")).Returns(false);

            LogOnViewModel model = new LogOnViewModel
            {
                UserName = "badUser",
                Password = "badPass"
            };

            AccountController target = new AccountController(mock.Object);

            ActionResult actual = target.LogOn(model, "/MyURL");

            Assert.IsInstanceOfType(actual, typeof(ViewResult));
            Assert.AreEqual(((ViewResult)actual).ViewData.ModelState.IsValid, false);
        }
    }
}
