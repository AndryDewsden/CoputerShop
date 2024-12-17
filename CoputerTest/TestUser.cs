using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Windows;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CoputerShop;
using CoputerShop.ApplicationData;
using CoputerShop.Pages;

namespace CoputerTest.Tests
{
    [TestClass()]
    public class TestUser
    {
        [TestMethod]
        public void Check_Autorisation()
        {
            //1
            string testpassword = "1";
            string testlogin = "1";
            bool expected = true;

            //2
            bool actual = Autorisation.ValidateAuto(testlogin, testpassword);

            //3
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Check_Registration()
        {
            //1
            string test_name = "test_user";
            string test_password = "123";
            string test_login = "123";
            bool expected = true;

            //2
            bool actual = Registration.ValidateRegistration(test_name, test_login, test_password);

            //3
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Check_Product()
        {
            //1
            string test_product_name = "Монитор";
            int test_product_creator = 1;
            int test_product_type = 2;
            bool expected = true;

            //2
            bool actual = Product.ValidateProduct(test_product_name, test_product_type, test_product_creator);

            //3
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Check_User()
        {
            //1
            string test_user_name = "andrey";
            string test_user_login = "admin";
            int test_user_role = 3;
            bool expected = true;

            //2
            bool actual = User.ValidateUser(test_user_name, test_user_login, test_user_role);

            //3
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Check_AddRed()
        {
            //1
            string test_product = "name";
            int product_type = 1;
            bool expected = true;

            //2
            bool actual = AddRedact.ValidateAddRed(test_product, product_type);

            //3
            Assert.AreEqual(expected, actual);
        }
    }
}
