using Microsoft.VisualStudio.TestTools.UnitTesting;
using Data;
namespace DataTest
{
    [TestClass]
    public class DBRestTest
    {
        DBRest datos = new DBRest();


        [TestMethod]
        public void TestMethod1()
        {
            Assert.AreEqual(1, 1);
            DBRest datos = new DBRest();
            datos.insertarUsuario();
        }
    }
}
