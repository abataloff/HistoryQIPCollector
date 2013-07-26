using HistoryQIPCollector;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace HistoryQIPCollector_TestProject
{


    [TestClass()]
    public class HistoryRecord_Tests
    {


        private TestContext testContextInstance;

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

        #region Дополнительные атрибуты теста
        // 
        //При написании тестов можно использовать следующие дополнительные атрибуты:
        //
        //ClassInitialize используется для выполнения кода до запуска первого теста в классе
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //ClassCleanup используется для выполнения кода после завершения работы всех тестов в классе
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //TestInitialize используется для выполнения кода перед запуском каждого теста
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //TestCleanup используется для выполнения кода после завершения каждого теста
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        [TestMethod]
        public void ParseIncomingMessage()
        {
            const string _TEXT = "--------------------------------------<-\n" +
                        "Женя Тарасов (10:23:52 22/04/2010)\n" +
                        "госы 27го если ты не сдал\n\n\n";
            var _expected = new HistoryRecord
                {
                    Date = new DateTime(2010,04,22,10,23,52),
                    Message = "госы 27го если ты не сдал\n\n\n",
                    Direction = MessageDirection.Incoming,
                    Nik = "Женя Тарасов"
                };
            var _actual = HistoryRecord.Parse(_TEXT);
            Assert.AreEqual(_expected, _actual);
        }

        [TestMethod]
        public void ParseOutgoingMessage()
        {
            const string _TEXT = "-------------------------------------->-\n" +
                        "S_H_U_R_I_K (10:28:03 22/04/2010)\n" +
                        "ок)спс)Сдал)А кто ещё с тобой сдает?";
            var _expected = new HistoryRecord
            {
                Date = new DateTime(2010, 04, 22, 10, 28, 03),
                Message = "ок)спс)Сдал)А кто ещё с тобой сдает?",
                Direction = MessageDirection.Outgoing,
                Nik = "S_H_U_R_I_K"
            };
            var _actual = HistoryRecord.Parse(_TEXT);
            Assert.AreEqual(_expected, _actual);
        }
    }
}
