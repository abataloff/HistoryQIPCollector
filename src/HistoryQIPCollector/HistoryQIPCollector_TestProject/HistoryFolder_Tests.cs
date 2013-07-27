using HistoryQIPCollector;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using HistoryQIPCollector_TestProject.Properties;

namespace HistoryQIPCollector_TestProject
{
    [TestClass]
    public class HistoryFolder_Tests
    {
        private string fileName0;
        private string fileName1;
        private string fileName2;
        private string userDirectory;

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
        [TestInitialize]
        public void MyTestInitialize()
        {

            userDirectory = Path.Combine(TestContext.TestDir, "230989500");
            var _diUd = new DirectoryInfo(userDirectory);
            if (_diUd.Exists)
                _diUd.Delete(true);
            _diUd.Create();

            var userHistoryDirectory = Path.Combine(userDirectory, HistoryFolder.HISTORY_FOLDER_NAME);
            Directory.CreateDirectory(userHistoryDirectory);

            // Сохраняем историю
            fileName0 = Path.Combine(userHistoryDirectory, "2933684.txt");
            TextWriter _tw0 = new StreamWriter(fileName0, false);
            _tw0.Write(Resources._2933684);
            _tw0.Close();

            fileName1 = Path.Combine(userHistoryDirectory, "12348765.txt");
            TextWriter _tw1 = new StreamWriter(fileName1, false);
            _tw1.Write(Resources._12348765);
            _tw1.Close();

            fileName2 = Path.Combine(userHistoryDirectory, "12341765.txt");
            TextWriter _tw2 = new StreamWriter(fileName2, false);
            _tw2.Write(Resources._12341765);
            _tw2.Close();
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
            var _result = HistoryFolder.Read(userDirectory);
            Assert.AreEqual(230989500, _result.OwnerIcqNumber);
            Assert.AreEqual(3, _result.Files.Count);

            var _file0 = _result.Files[12341765];
            Assert.AreEqual(12341765, _file0.InterlocutorIcqNumber);
            Assert.AreEqual(4, _file0.Records.Count);
            Assert.AreEqual(new HistoryRecord
            {
                Date = new DateTime(2010, 04, 22, 10, 23, 52),
                Message = "госы 27го если ты не сдал\r\n123\r\n456\r\n\r\n\r\n",
                Direction = MessageDirection.Incoming,
                Nik = "Женя Тарасов"
            }, _file0.Records[0]);
            Assert.AreEqual(new HistoryRecord
            {
                Date = new DateTime(2010, 04, 22, 10, 28, 03),
                Message = "ок)спс)Сдал)А кто ещё с тобой сдает?\r\n123\r\n\r\n\r\n\r\n124\r\n14",
                Direction = MessageDirection.Outgoing,
                Nik = "S_H_U_R_I_K"
            }, _file0.Records[1]);
            Assert.AreEqual(new HistoryRecord
            {
                Date = new DateTime(2013, 03, 19, 12, 26, 21),
                Message = "до скольки тренажерка работает?",
                Direction = MessageDirection.Outgoing,
                Nik = "abataloff"
            }, _file0.Records[2]);
            Assert.AreEqual(new HistoryRecord
            {
                Date = new DateTime(2013, 03, 19, 12, 26, 26),
                Message = "до 9-10\r\n\r\n\r\n\r\n",
                Direction = MessageDirection.Incoming,
                Nik = "Женя Тарасов"
            }, _file0.Records[3]);

            var _file1 = _result.Files[12348765];
            Assert.AreEqual(12348765, _file1.InterlocutorIcqNumber);
            Assert.AreEqual(4, _file1.Records.Count);
            Assert.AreEqual(new HistoryRecord
            {
                Date = new DateTime(2010, 04, 22, 10, 23, 52),
                Message = "госы 27го если ты не сдал\r\n123\r\n456\r\n\r\n\r\n",
                Direction = MessageDirection.Incoming,
                Nik = "Женя Тарасов"
            }, _file1.Records[0]);
            Assert.AreEqual(new HistoryRecord
            {
                Date = new DateTime(2010, 04, 22, 10, 28, 03),
                Message = "ок)спс)Сдал)А кто ещё с тобой сдает?",
                Direction = MessageDirection.Outgoing,
                Nik = "S_H_U_R_I_K"
            }, _file1.Records[1]);
            Assert.AreEqual(new HistoryRecord
            {
                Date = new DateTime(2013, 03, 19, 12, 26, 21),
                Message = "до скольки тренажерка работает?",
                Direction = MessageDirection.Outgoing,
                Nik = "abataloff"
            }, _file1.Records[2]);
            Assert.AreEqual(new HistoryRecord
            {
                Date = new DateTime(2013, 03, 19, 12, 26, 26),
                Message = "до 9-10\r\n\r\n\r\n\r\n",
                Direction = MessageDirection.Incoming,
                Nik = "Женя Тарасов"
            }, _file1.Records[3]);

            var _file2 = _result.Files[2933684];
            Assert.AreEqual(2933684, _file2.InterlocutorIcqNumber);
            Assert.AreEqual(4, _file2.Records.Count);
            Assert.AreEqual(new HistoryRecord
            {
                Date = new DateTime(2010, 04, 22, 10, 23, 52),
                Message = "госы 27го если ты не сдал",
                Direction = MessageDirection.Incoming,
                Nik = "Женя Тарасов"
            }, _file2.Records[0]);
            Assert.AreEqual(new HistoryRecord
            {
                Date = new DateTime(2010, 04, 22, 10, 28, 03),
                Message = "ок)спс)Сдал)А кто ещё с тобой сдает?",
                Direction = MessageDirection.Outgoing,
                Nik = "S_H_U_R_I_K"
            }, _file2.Records[1]);
            Assert.AreEqual(new HistoryRecord
            {
                Date = new DateTime(2013, 03, 19, 12, 26, 21),
                Message = "до скольки тренажерка работает?",
                Direction = MessageDirection.Outgoing,
                Nik = "abataloff"
            }, _file2.Records[2]);
            Assert.AreEqual(new HistoryRecord
            {
                Date = new DateTime(2013, 03, 19, 12, 26, 26),
                Message = "до 9-10",
                Direction = MessageDirection.Incoming,
                Nik = "Женя Тарасов"
            }, _file2.Records[3]);
        }
    }
}
