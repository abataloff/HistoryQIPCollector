using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using NLog;

namespace HistoryQIPCollector
{
    internal class HistoryFile
    {
        public const string FILE_EXTENTION = ".txt";

        public int InterlocutorIcqNumber { get; set; }

        private static readonly Logger log = LogManager.GetCurrentClassLogger();
        private static readonly int AndRecordCharCount = 2*Environment.NewLine.Length;
        private static readonly int AndFileCharCount = 0*Environment.NewLine.Length;

        public List<HistoryRecord> Records { get; set; }

        public static HistoryFile Read(string a_fullNameFile)
        {
            var _fileName = Path.GetFileNameWithoutExtension(a_fullNameFile);
            var _interlocutorIcqNumber = 0;

            if (!int.TryParse(_fileName, out _interlocutorIcqNumber))
                throw new Exception(string.Format("Имя файла должно быть числовым. Имя файла:{0}", _fileName));

            TextReader _tr = new StreamReader(a_fullNameFile);
            var _text = _tr.ReadToEnd();
            _tr.Close();
            var _sr = new StringReader(_text);
            var _str = string.Empty;
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
                    _buf.Remove(_buf.Length - AndRecordCharCount, AndRecordCharCount);                  
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
            _buf.Remove(_buf.Length - (AndRecordCharCount + AndFileCharCount), AndRecordCharCount + AndFileCharCount);
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

        private static bool tryParse(string a_recordText,out HistoryRecord out_historyRecord)
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
    }
}
