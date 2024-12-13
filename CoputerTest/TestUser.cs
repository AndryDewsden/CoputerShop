using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using CoputerShop.ApplicationData;

namespace CoputerTest
{
    [TestClass]
    public class TestUser
    {
        [TestMethod]
        //Проверяем авторизацию
        public void CheckerUserAutorisation()
        {
            //Начальные данные
            Users testUser = new Users();
            testUser.user_name = "1";
            testUser.user_login = "1";
            testUser.user_password = "1";

            bool expected = true;

            //Действие
            bool actual = false;

            //Результат
            Assert.AreEqual(expected, actual);
        }

        Products testProductAdd = new Products();
        Products testProductRed = new Products();

        [TestMethod]
        //Проверяем добавление товара
        public void CheckerProductAdd()
        {
            //Начальные данные
            testProductAdd.product_name = "Добавляемый тестовый продукт";
            testProductAdd.product_creator_id = 1;
            testProductAdd.product_seller_id = 1;
            testProductAdd.product_type_id = 1;
            testProductAdd.product_status_id = 1;
            testProductAdd.product_wholesale_price = 1;
            testProductAdd.product_retail_price = 1;

            bool expected = true;

            //Действие
            bool actual = false;

            //Результат
            Assert.AreEqual(expected, actual);
        }

        //Проверяем редактироание товара
        public void CheckerProducrRed()
        {
            //Начальные данные
            testProductRed.product_name = "Редактируемый тестовый продук";
            testProductRed.product_creator_id = 1;
            testProductRed.product_seller_id = 1;
            testProductRed.product_type_id = 1;
            testProductRed.product_status_id = 1;
            testProductRed.product_wholesale_price = 1;
            testProductRed.product_retail_price = 1;

            bool expected = true;

            //Действие
            bool actual = false;

            //Результат
            Assert.AreEqual(expected, actual);
        }

        //Проверяем удаление товара
        public void CheckerProductDel()
        {
            //Начальные данные


            bool expected = true;

            //Действие
            bool actual = false;

            //Результат
            Assert.AreEqual(expected, actual);
        }

        //Проверяем регистрацию
        //Проверяем добавление товара в корзину
        //Проверяем редактирование товаров в коризне
        //Проверяем завершение заказа
        //Проверяем добавление пользователя через админ панель
        //Проверяем редактирование пользователя через админ панель
        //Проверяем удаление пользователя через админ панель
        //Проверяем редактирование заказа через админ панель
        //Проверяем удаление заказа через админ панель
    }
}
