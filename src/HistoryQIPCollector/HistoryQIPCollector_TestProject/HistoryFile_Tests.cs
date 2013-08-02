using HistoryQIPCollector;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using HistoryQIPCollector_TestProject.Properties;
using System.IO;
using System.Collections.Generic;
using HistoryQIPCollector_TestProject.Tools;

namespace HistoryQIPCollector_TestProject
{


    [TestClass()]
    public class HistoryFile_Tests
    {
        private string fileName;

        private TestContext testContextInstance;

        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        #region Дополнительные атрибуты теста

        // 
        //При написании тестов можно использовать следующие дополнительные атрибуты:
        //
        //ClassInitialize используется для выполнения кода до запуска первого теста в классе
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext a_testContext)
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
        [TestInitialize()]
        public void MyTestInitialize()
        {
            // Сохраняем историю
            fileName = Path.Combine(TestContext.TestDir, "2933684.txt");
            TextWriter _tw = new StreamWriter(fileName, false, HistoryFile.FileEncoding);
            _tw.Write(Resources._2933684);
            _tw.Close();
        }

        //
        //TestCleanup используется для выполнения кода после завершения каждого теста
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //

        #endregion


        [TestMethod()]
        public void Read()
        {
            var _result = HistoryFile.Read(fileName);
            Assert.AreEqual(2933684, _result.InterlocutorIcqNumber);
            Assert.AreEqual(4, _result.Records.Count);
            Assert.AreEqual(new HistoryRecord
                {
                    Date = new DateTime(2010, 04, 22, 10, 23, 52),
                    Message = "госы 27го если ты не сдал",
                    Direction = MessageDirection.Incoming,
                    Nik = "Женя Тарасов"
                }, _result.Records[0]);
            Assert.AreEqual(new HistoryRecord
                {
                    Date = new DateTime(2010, 04, 22, 10, 28, 03),
                    Message = "ок)спс)Сдал)А кто ещё с тобой сдает?",
                    Direction = MessageDirection.Outgoing,
                    Nik = "S_H_U_R_I_K"
                }, _result.Records[1]);
            Assert.AreEqual(new HistoryRecord
                {
                    Date = new DateTime(2013, 03, 19, 12, 26, 21),
                    Message = "до скольки тренажерка работает?",
                    Direction = MessageDirection.Outgoing,
                    Nik = "abataloff"
                }, _result.Records[2]);
            Assert.AreEqual(new HistoryRecord
                {
                    Date = new DateTime(2013, 03, 19, 12, 26, 26),
                    Message = "до 9-10",
                    Direction = MessageDirection.Incoming,
                    Nik = "Женя Тарасов"
                }, _result.Records[3]);
        }

        [TestMethod]
        public void Write()
        {
            var _historyFile = new HistoryFile
                {
                    InterlocutorIcqNumber = 2933684,
                    Records = new List<HistoryRecord>()
                        {
                            new HistoryRecord
                                {
                                    Date = new DateTime(2010, 04, 22, 10, 23, 52),
                                    Message = "госы 27го если ты не сдал",
                                    Direction = MessageDirection.Incoming,
                                    Nik = "Женя Тарасов"
                                },
                            new HistoryRecord
                                {
                                    Date = new DateTime(2010, 04, 22, 10, 28, 03),
                                    Message = "ок)спс)Сдал)А кто ещё с тобой сдает?",
                                    Direction = MessageDirection.Outgoing,
                                    Nik = "S_H_U_R_I_K"
                                },
                            new HistoryRecord
                                {
                                    Date = new DateTime(2013, 03, 19, 12, 26, 21),
                                    Message = "до скольки тренажерка работает?",
                                    Direction = MessageDirection.Outgoing,
                                    Nik = "abataloff"
                                },
                            new HistoryRecord
                                {
                                    Date = new DateTime(2013, 03, 19, 12, 26, 26),
                                    Message = "до 9-10",
                                    Direction = MessageDirection.Incoming,
                                    Nik = "Женя Тарасов"
                                },
                        },

                };
            var _actualDi = new DirectoryInfo(Path.Combine(TestContext.TestDir, "actual"));
            if (_actualDi.Exists)
                _actualDi.Delete(true);
            _actualDi.Create();

            var _actualFileName = Path.Combine(_actualDi.FullName, _historyFile.InterlocutorIcqNumber + HistoryFile.FILE_EXTENTION);
            _historyFile.Write(_actualDi.FullName);

            Assert.IsTrue(File.Exists(_actualFileName));
            Assert.IsTrue(Comparator.File(_actualFileName, fileName));
        }
    }
}
