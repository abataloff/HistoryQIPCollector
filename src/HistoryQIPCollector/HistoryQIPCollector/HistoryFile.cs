using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Linq;
using NLog;

namespace HistoryQIPCollector
{
    internal class HistoryFile
    {
        public const string FILE_EXTENTION = ".txt";
        public const int END_RECORD_NEW_LINE_COUNT = 2;
        public const int END_FILE_NEW_LINE_COUNT = 0;

        public int InterlocutorIcqNumber { get; set; }

        private static readonly Logger log = LogManager.GetCurrentClassLogger();
        private static readonly int endRecordCharCount = END_RECORD_NEW_LINE_COUNT*Environment.NewLine.Length;
        private static readonly int endFileCharCount = END_FILE_NEW_LINE_COUNT*Environment.NewLine.Length;

        public static readonly Encoding FileEncoding = Encoding.GetEncoding(1251);

        public HistoryFile()
        {
            Records = new List<HistoryRecord>();
        }

        public List<HistoryRecord> Records { get; set; }

        public static HistoryFile Read(string a_fullNameFile)
        {
            var _fileName = Path.GetFileNameWithoutExtension(a_fullNameFile);
            int _interlocutorIcqNumber;

            if (!int.TryParse(_fileName, out _interlocutorIcqNumber))
                throw new Exception(string.Format("Имя файла должно быть числовым. Имя файла:{0}", _fileName));

            TextReader _tr = new StreamReader(a_fullNameFile, FileEncoding);
            var _text = _tr.ReadToEnd();
            _tr.Close();
            var _sr = new StringReader(_text);
            string _str;
            var _buf = new StringBuilder();
            var _records = new List<HistoryRecord>();
            HistoryRecord _historyRecord;
            // Читаем перую строку (это начало записи истории), добавляем её в буфер
            _buf.AppendLine(_sr.ReadLine());
            while ((_str = _sr.ReadLine()) != null)
            {
                // Если начало сообщения и в буфере что то есть
                if ((_str == HistoryRecord.INCOMING_HEAD_STRING || _str == HistoryRecord.OUTGOING_HEAD_STRING) &&
                    _buf.Length > 0)
                {
                    // Удаляем два переноса строк из буфера (конец сообщения)
                    _buf.Remove(_buf.Length - endRecordCharCount, endRecordCharCount);
                    // Пытаемся разобрать запись из буфера
                    var _success = tryParse(_buf.ToString(), out _historyRecord);
                    // Если попытка удалась
                    if (_success)
                    {
                        // добавляем запись в список
                        _records.Add(_historyRecord);
                    }

                    // Очищаем буфер
                    _buf.Clear();
                }

                _buf.AppendLine(_str);
            }

            // Удяляем послении переводы строк
            _buf.Remove(_buf.Length - (endRecordCharCount + endFileCharCount), endRecordCharCount + endFileCharCount);
            // Если запись разобралась
            if (tryParse(_buf.ToString(), out _historyRecord))
            {
                // добавляем в скписок
                _records.Add(_historyRecord);
            }

            var _recordCount = _records.Count;

            // Удаляем все пустые записи
            _records.RemoveAll(a_record => a_record == null);
            log.Trace("Число Null записей:{0}", _recordCount - _records.Count);

            return new HistoryFile
                {
                    InterlocutorIcqNumber = _interlocutorIcqNumber,
                    Records = _records,
                };
        }

        private static bool tryParse(string a_recordText, out HistoryRecord out_historyRecord)
        {
            var _retBool = false;
            out_historyRecord = null;
            try
            {
                out_historyRecord = HistoryRecord.Parse(a_recordText);
                _retBool = true;
            }
            catch (Exception _ex)
            {
                log.ErrorException(string.Format("Ошибка при разборе строки записи истории. Строка:{0}", a_recordText), _ex);
            }
            return _retBool;
        }

        public void Write(string a_historyPath)
        {
            var _fileName = Path.Combine(a_historyPath, InterlocutorIcqNumber.ToString(CultureInfo.InvariantCulture) + FILE_EXTENTION);
            TextWriter _tw = new StreamWriter(File.Create(_fileName), FileEncoding);
            foreach (var _record in Records)
            {
                _tw.Write(_record.ToString());
                for (var _i = 0; _i < END_RECORD_NEW_LINE_COUNT; _i++)
                    _tw.WriteLine();
            }
            _tw.Close();
        }

        public override bool Equals(object a_obj)
        {
            var _val = a_obj as HistoryFile;
            if (_val == null)
                return false;
            return InterlocutorIcqNumber.Equals(_val.InterlocutorIcqNumber) &&
                   Records.SequenceEqual(_val.Records);
        }
    }
}