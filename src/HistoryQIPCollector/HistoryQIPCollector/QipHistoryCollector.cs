using System;
using System.Collections.Generic;
using System.Linq;
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
            // Добавляем в результаты файлы первого источника
            foreach (var _file in a_sources[0].Files.Values)
                _result.Files.Add(_file.InterlocutorIcqNumber, _file);

            // Поочередно (кроме первого источника) добавляем файлы, если Icq совпадает с целевым
            foreach (var _hf in a_sources.GetRange(1,a_sources.Count-1))
            {
                if (_hf.OwnerIcqNumber == _targetIcqNumber)
                {
                    addFilesToFolder(_result, _hf.Files);
                }
                else log.Warn("История Icq - {0} не была добавлена",_hf.OwnerIcqNumber);
            }

            return _result;
        }

        private static void addFilesToFolder(HistoryFolder a_historyFolder, Dictionary<int, HistoryFile> a_addFiles)
        {
            foreach (var _file in a_addFiles.Keys)
            {
                var _number = _file;
                var _addFile = a_addFiles[_number];
                // Если история с пользователем _number, есть
                if (a_historyFolder.Files.ContainsKey(_number))
                {
                    // добавляем к нейновые записи (при добавление учитывает хранологию и дублирующие сообщения)
                    var _historyFile = a_historyFolder.Files[_number];
                    //a_historyFolder.Files[_number].Records.AddRange(a_addFiles[_number].Records);
                    addRecordsToFile(_historyFile, _addFile.Records);
                }
                else
                {
                    // иначе это просто новая история, добавляем её
                    a_historyFolder.Files.Add(_number, _addFile);
                }
            }
        }

        /// <summary>
        /// Добавляет записи в файл, при этом учитывает хронологию и дублирующие сообещния.
        /// </summary>
        /// <param name="a_historyFile">Файл в который будут добавляться записи. Записи в файле считаются нормализованными (нет дупликатов и сортированы по времени).</param>
        /// <param name="a_addRecords">Добавляемые записи (должны быть нормализованные).</param>
        private static void addRecordsToFile(HistoryFile a_historyFile, List<HistoryRecord> a_addRecords)
        {
            // ВАЖНО! Считаем что записи в файле и добавляемые записи уже нормализованы

            // Если есть добавляемый записи
            if (a_addRecords.Count > 0)
            {
                var _historyFileRecords = a_historyFile.Records;
                // Если в файле уже есть записи и последняя запись в файле позже первой добавляемой
                if (_historyFileRecords.Count > 0 && _historyFileRecords.Last().Date>= a_addRecords.First().Date)
                {
                    var _pos = 0;
                    // поочередно добавляем записи
                    foreach (var _addRecord in a_addRecords)
                    {
                        var _dateAddRecord = _addRecord.Date;
                        // ищем место куда её вставить (по хронологии)
                        _pos = _historyFileRecords.FindIndex(_pos, a_rec => a_rec.Date > _dateAddRecord);
                        // Если позиция есть (>=0, т.е. добавляется не в конец)
                        if (_pos >= 0)
                        {
                            if (_pos == 0)
                            {
                                _historyFileRecords.Insert(_pos, _addRecord);
                            }
                            else
                            {
                                // делаем [_pos-1], т.к. _pos>1
                                var _recordInFile = _historyFileRecords[_pos - 1];
                                // Если нет таких сообщений (берем только до _pos, т.к. после точно нет)
                                if(!_historyFileRecords.GetRange(0,_pos).Contains(_addRecord))
                                {
                                    // Если даты записей (вставляемой и до вставляймой в файле - [_pos]), ники и направленость одинаковые
                                    if (_recordInFile.Date == _addRecord.Date && _recordInFile.Direction == _addRecord.Direction &&
                                        _recordInFile.Nik == _addRecord.Nik)
                                    {
                                        // объединяем тексты
                                        _recordInFile.Message += "\n" + _addRecord.Message;
                                        log.Trace("Были обнаружены одномоментные записи с разным текстом. В файле {0} с датой {1}",
                                                  a_historyFile.InterlocutorIcqNumber,
                                                  _addRecord.Date);
                                    }
                                    else
                                    {
                                        // иначе добавляем запись
                                        _historyFileRecords.Insert(_pos, _addRecord);
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (!_historyFileRecords.Contains(_addRecord))
                                _historyFileRecords.Add(_addRecord);
                            _pos = 0;
                        }
                    }
                }
                else _historyFileRecords.AddRange(a_addRecords);
            }
        }
    }
}
