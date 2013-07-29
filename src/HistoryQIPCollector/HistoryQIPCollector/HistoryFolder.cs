using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HistoryQIPCollector
{
    internal class HistoryFolder
    {
        public const string HISTORY_FOLDER_NAME = "History";
        private static readonly List<string> ignoredFiles = new List<string>() {"_botlog.txt", "_srvlog.txt"};

        public int OwnerIcqNumber { get; set; }
        public Dictionary<int, HistoryFile>  Files { get; set; }

        public static HistoryFolder Read(string a_userFolder)
        {
            var _userFolderName = (new DirectoryInfo(a_userFolder)).Name;
            int _ownerIcqNumber;

            if (!int.TryParse(_userFolderName, out _ownerIcqNumber))
                throw new Exception(string.Format("Имя папки должно быть числовым. Имя файла:{0}", _userFolderName));

            var _historyFolder = Path.Combine(a_userFolder, HISTORY_FOLDER_NAME);

            if (!Directory.Exists(_historyFolder))
                throw new DirectoryNotFoundException(string.Format("Не найдена директория {0}", _historyFolder));

            return new HistoryFolder
                {
                    OwnerIcqNumber = _ownerIcqNumber,
                    Files = (from _fileName in Directory.GetFiles(_historyFolder, "*" + HistoryFile.FILE_EXTENTION, SearchOption.TopDirectoryOnly)
                             where !ignoredFiles.Contains(_fileName)
                             select HistoryFile.Read(_fileName)).ToDictionary(a_historyFile => a_historyFile.InterlocutorIcqNumber),
                };
        }

        internal void Write(string _resultDir)
        {
            throw new NotImplementedException();
        }
    }
}
