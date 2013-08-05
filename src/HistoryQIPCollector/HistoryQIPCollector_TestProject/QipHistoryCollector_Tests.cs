using System;
using System.Collections.Generic;
using HistoryQIPCollector;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HistoryQIPCollector_TestProject
{
    [TestClass]
    public class QipHistoryCollector_Tests
    {
        [TestMethod]
        public void CollectTest()
        {
            var _folder0 = new HistoryFolder();
            var _folder1 = new HistoryFolder();
            var _folder2 = new HistoryFolder();

            // Целевой номер Icq 2

            #region Подготовка входных данных
            #region Папка0
            // Два файла по 3 сообщения от пользователей 1 и 3
            _folder0.OwnerIcqNumber = 2;
            _folder0.Files = new Dictionary<int, HistoryFile>()
                {
                    {
                        1,
                        new HistoryFile
                            {
                                InterlocutorIcqNumber = 1,
                                Records =
                                    new List<HistoryRecord>
                                        {
                                            new HistoryRecord
                                                {
                                                    Date = new DateTime(2001, 12, 12, 2, 12, 00),
                                                    Direction = MessageDirection.Incoming,
                                                    Message = "Folder 1 File 1 Record 1",
                                                    Nik = "Nik 1"
                                                },
                                                new HistoryRecord
                                                {
                                                    Date = new DateTime(2001, 12, 12, 2, 15, 00),
                                                    Direction = MessageDirection.Outgoing,
                                                    Message = "Folder 1 File 1 Record 2",
                                                    Nik = "Nik 2"
                                                },
                                                new HistoryRecord
                                                {
                                                    Date = new DateTime(2001, 12, 12, 2, 50, 00),
                                                    Direction = MessageDirection.Incoming,
                                                    Message = "Folder 1 File 1 Record 3",
                                                    Nik = "Nik 1"
                                                },
                                        }
                            }
                    },
                    {
                        3,
                        new HistoryFile
                            {
                                InterlocutorIcqNumber = 3,
                                Records =
                                    new List<HistoryRecord>
                                        {
                                            new HistoryRecord
                                                {
                                                    Date = new DateTime(2001, 1, 12, 2, 12, 00),
                                                    Direction = MessageDirection.Incoming,
                                                    Message = "Folder 1 File 2 Record 1",
                                                    Nik = "Nik 3"
                                                },
                                                new HistoryRecord
                                                {
                                                    Date = new DateTime(2001, 1, 12, 2, 15, 00),
                                                    Direction = MessageDirection.Outgoing,
                                                    Message = "Folder 1 File 2 Record 2",
                                                    Nik = "Nik 2"
                                                },
                                                new HistoryRecord
                                                {
                                                    Date = new DateTime(2001, 1, 12, 2, 50, 00),
                                                    Direction = MessageDirection.Incoming,
                                                    Message = "Folder 1 File 2 Record 3",
                                                    Nik = "Nik 3"
                                                },
                                        }
                            }
                    },
                };

            #endregion

            #region Папка1
            // История не должна обработаться, так как номер не целевой
            // Один файл с одним входящим сообещием
            _folder1.OwnerIcqNumber = 1;
            _folder1.Files = new Dictionary<int, HistoryFile>()
                {
                    {
                        2,
                        new HistoryFile
                            {
                                InterlocutorIcqNumber = 2,
                                Records =
                                    new List<HistoryRecord>
                                        {
                                                new HistoryRecord
                                                {
                                                    Date = new DateTime(2001, 12, 12, 2, 15, 00),
                                                    Direction = MessageDirection.Incoming,
                                                    Message = "Folder 2 File 1 Record 1",
                                                    Nik = "Nik 2"
                                                },
                                        }
                            }
                    },
                };

            #endregion

            #region Папка2
            // Три файла файла
            // Первый файл пользователя 1, одно сообщение общее, это третье из прошлой истории и два новых
            // Второй файл пользователя 3, у ниго одно сообщение дублируется, и одно дописывается
            // Третий файл пользователя 4 - новый относительно других
            _folder2.OwnerIcqNumber = 2;
            _folder2.Files = new Dictionary<int, HistoryFile>()
                {
                    {
                        1,
                        new HistoryFile
                            {
                                InterlocutorIcqNumber = 1,
                                Records =
                                    new List<HistoryRecord>
                                        {
                                            // Новое сообщение
                                            new HistoryRecord
                                                {
                                                    Date = new DateTime(2001, 12, 12, 2, 14, 00),
                                                    Direction = MessageDirection.Incoming,
                                                    Message = "Folder 3 File 1 Record 1",
                                                    Nik = "Nik 1"
                                                },
                                                //Дублируемое сообщение
                                                new HistoryRecord
                                                {
                                                    Date = new DateTime(2001, 12, 12, 2, 15, 00),
                                                    Direction = MessageDirection.Outgoing,
                                                    Message = "Folder 1 File 1 Record 2",
                                                    Nik = "Nik 2"
                                                },
                                                // Новое сообщение
                                                new HistoryRecord
                                                {
                                                    Date = new DateTime(2001, 12, 12, 2, 55, 00),
                                                    Direction = MessageDirection.Incoming,
                                                    Message = "Folder 3 File 1 Record 3",
                                                    Nik = "Nik 1"
                                                },
                                        }
                            }
                    },
                     {
                        3,
                        new HistoryFile
                            {
                                InterlocutorIcqNumber = 3,
                                Records =
                                    new List<HistoryRecord>
                                        {
                                            // Дублируемое сообешние
                                            new HistoryRecord
                                                {
                                                    Date = new DateTime(2001, 1, 12, 2, 12, 00),
                                                    Direction = MessageDirection.Incoming,
                                                    Message = "Folder 1 File 2 Record 1",
                                                    Nik = "Nik 3"
                                                },
                                                // Дублируемое но не полностью, изменен текст
                                                new HistoryRecord
                                                {
                                                    Date = new DateTime(2001, 1, 12, 2, 15, 00),
                                                    Direction = MessageDirection.Outgoing,
                                                    Message = "Folder 1 File 2 Record 2_new",
                                                    Nik = "Nik 2"
                                                },
                                        }
                            }
                    },
                    {
                        4,
                        new HistoryFile
                            {
                                InterlocutorIcqNumber = 4,
                                Records =
                                    new List<HistoryRecord>
                                        {
                                            new HistoryRecord
                                                {
                                                    Date = new DateTime(2001, 11, 12, 2, 15, 00),
                                                    Direction = MessageDirection.Incoming,
                                                    Message = "Folder 3 File 3 Record 1",
                                                    Nik = "Nik 4"
                                                },
                                                new HistoryRecord
                                                {
                                                    Date = new DateTime(2001, 11, 12, 2, 15, 00),
                                                    Direction = MessageDirection.Outgoing,
                                                    Message = "Folder 3 File 3 Record 2",
                                                    Nik = "Nik 2"
                                                },
                                                new HistoryRecord
                                                {
                                                    Date = new DateTime(2001, 11, 12, 2, 50, 00),
                                                    Direction = MessageDirection.Incoming,
                                                    Message = "Folder 3 File 3 Record 3",
                                                    Nik = "Nik 4"
                                                },
                                        }
                            }
                    },
                };

            #endregion
            #endregion

            var _expected = new HistoryFolder();

            #region Подготовка результата

            // Должна получиться одна папка с тремя папками
            _expected.OwnerIcqNumber = 2;
            _expected.Files = new Dictionary<int, HistoryFile>
                {
                    {
                        1,
                        new HistoryFile
                            {
                                InterlocutorIcqNumber = 1,
                                Records =
                                    new List<HistoryRecord>
                                        {
                                            new HistoryRecord
                                                {
                                                    Date = new DateTime(2001, 12, 12, 2, 12, 00),
                                                    Direction = MessageDirection.Incoming,
                                                    Message = "Folder 1 File 1 Record 1",
                                                    Nik = "Nik 1"
                                                },
                                                new HistoryRecord
                                                {
                                                    Date = new DateTime(2001, 12, 12, 2, 14, 00),
                                                    Direction = MessageDirection.Incoming,
                                                    Message = "Folder 3 File 1 Record 1",
                                                    Nik = "Nik 1"
                                                },
                                                new HistoryRecord
                                                {
                                                    Date = new DateTime(2001, 12, 12, 2, 15, 00),
                                                    Direction = MessageDirection.Outgoing,
                                                    Message = "Folder 1 File 1 Record 2",
                                                    Nik = "Nik 2"
                                                },
                                                new HistoryRecord
                                                {
                                                    Date = new DateTime(2001, 12, 12, 2, 50, 00),
                                                    Direction = MessageDirection.Incoming,
                                                    Message = "Folder 1 File 1 Record 3",
                                                    Nik = "Nik 1"
                                                },
                                                new HistoryRecord
                                                {
                                                    Date = new DateTime(2001, 12, 12, 2, 55, 00),
                                                    Direction = MessageDirection.Incoming,
                                                    Message = "Folder 3 File 1 Record 3",
                                                    Nik = "Nik 1"
                                                },
                                        }
                            }
                    },
                    {
                        3,
                        new HistoryFile
                            {
                                InterlocutorIcqNumber = 3,
                                Records =
                                    new List<HistoryRecord>
                                        {
                                            new HistoryRecord
                                                {
                                                    Date = new DateTime(2001, 1, 12, 2, 12, 00),
                                                    Direction = MessageDirection.Incoming,
                                                    Message = "Folder 1 File 2 Record 1",
                                                    Nik = "Nik 3"
                                                },
                                                new HistoryRecord
                                                {
                                                    Date = new DateTime(2001, 1, 12, 2, 15, 00),
                                                    Direction = MessageDirection.Outgoing,
                                                    Message = "Folder 1 File 2 Record 2_new\nFolder 1 File 2 Record 2",
                                                    Nik = "Nik 2"
                                                },
                                                new HistoryRecord
                                                {
                                                    Date = new DateTime(2001, 1, 12, 2, 50, 00),
                                                    Direction = MessageDirection.Incoming,
                                                    Message = "Folder 1 File 2 Record 3",
                                                    Nik = "Nik 3"
                                                },
                                        }
                            }
                    },
                    {
                        4,
                        new HistoryFile
                            {
                                InterlocutorIcqNumber = 4,
                                Records =
                                    new List<HistoryRecord>
                                        {
                                            new HistoryRecord
                                                {
                                                    Date = new DateTime(2001, 11, 12, 2, 12, 00),
                                                    Direction = MessageDirection.Incoming,
                                                    Message = "Folder 3 File 3 Record 1",
                                                    Nik = "Nik 4"
                                                },
                                                new HistoryRecord
                                                {
                                                    Date = new DateTime(2001, 11, 12, 2, 12, 00),
                                                    Direction = MessageDirection.Outgoing,
                                                    Message = "Folder 3 File 3 Record 2",
                                                    Nik = "Nik 2"
                                                },
                                                new HistoryRecord
                                                {
                                                    Date = new DateTime(2001, 11, 12, 2, 50, 00),
                                                    Direction = MessageDirection.Incoming,
                                                    Message = "Folder 3 File 3 Record 3",
                                                    Nik = "Nik 4"
                                                },
                                        }
                            }
                    },
                };

            #endregion

            var _target = new QipHistoryCollector(new QipHistoryCollectorSettings());
            var _actual = _target.Collect(new List<HistoryFolder> { _folder0, _folder1, _folder2 });
            Assert.AreEqual(_expected, _actual);
        }
    }
}
