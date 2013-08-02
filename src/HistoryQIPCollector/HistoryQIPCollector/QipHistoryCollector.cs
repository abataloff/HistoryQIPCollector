using System;
using System.Collections.Generic;
using NLog;

namespace HistoryQIPCollector
{
    class QipHistoryCollector
    {
        private static readonly Logger log = LogManager.GetCurrentClassLogger();

        private QipHistoryCollectorSettings settings;

        public QipHistoryCollector(QipHistoryCollectorSettings a_settings)
        {
            settings = a_settings;
        }

        internal HistoryFolder Collect(List<HistoryFolder> a_sources)
        {
            if (a_sources == null)
                throw new ArgumentNullException("a_sources");

            if (a_sources.Count == 0)
                return null;

            if (a_sources.Count == 1)
                return a_sources[0];
            
            // Целевой номер Icq первый
            var _targetIcqNumber = a_sources[0].OwnerIcqNumber;
            log.Trace("Целевой номер Icq - {0}", _targetIcqNumber);
            var _result = new HistoryFolder{OwnerIcqNumber = _targetIcqNumber};
            
            // Поочередно добавляем файлы, если Icq совпадает с целевым
            foreach (var _hf in a_sources)
            {
                if (_hf.OwnerIcqNumber == _targetIcqNumber)
                {
                    addFilesToFolder(_hf.Files, _result);
                }
                else log.Warn("История Icq - {0} не была добавлена",_hf.OwnerIcqNumber);
            }

            // Сортируем
            foreach (var _file in _result.Files.Values)
            {
                _file.Records.Sort((a_x, a_y ) =>
                    {
                        if (a_x.Date != a_y.Date)
                            return (a_x.Date > a_y.Date) ? 1 : -1;
                        return 0;
                    });
            }

            // Нормальизуем записи для каждого файла, т.е. удаляем дубликаты и объединяем одномоментные сообщения с разным текстом
            foreach (var _file in _result.Files.Values)
            {
                normalize(_file);
            }

            return _result;
        }

        private void normalize(HistoryFile a_file)
        {
            var _records = a_file.Records;
            if (_records.Count > 0)
            {
                var _result = new List<HistoryRecord>();
                // Предыдущая запись
                var _prev = _records[0];
                for (var _i = 1; _i < _records.Count; _i++)
                {
                    var _curRecord = _records[_i];
                    // Если даты записей одинаковые и при этом текст разный  (такое конечно не вероятно)
                    if (_prev.Date == _curRecord.Date && !_prev.Message.Equals(_curRecord.Message))
                    {
                        // объединяем тексты
                        _prev.Message += "\n" + _curRecord.Message;
                        log.Trace("Были обнаружены одномоментные записи с разным текстом. В файле {0} с датой {1}", a_file.InterlocutorIcqNumber,
                                  _curRecord.Date);
                    }
                    _result.Add(_prev);
                    _prev = _curRecord;
                }
                _records.Clear();
                _records.AddRange(_result);
            }
        }

        private static void addFilesToFolder(Dictionary<int, HistoryFile> a_addFiles, HistoryFolder a_historyFolder)
        {
            foreach (var _file in a_addFiles.Keys)
            {
                var _number = _file;
                if (a_historyFolder.Files.ContainsKey(_number))
                {
                    a_historyFolder.Files[_number].Records.AddRange(a_addFiles[_number].Records);
                }
                else
                {
                    a_historyFolder.Files.Add(_number, a_addFiles[_number]);
                }
            }
        }
    }
}
