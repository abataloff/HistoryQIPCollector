using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace HistoryQIPCollector
{
    public enum MessageDirection
    {
        Incoming,
        Outgoing
    }

    public class HistoryRecord
    {
        public const string INCOMING_HEAD_STRING = "--------------------------------------<-";
        public const string OUTGOING_HEAD_STRING = "-------------------------------------->-";

        public string Nik { get; set; }
        public DateTime Date { get; set; }
        public string Message { get; set; }
        public MessageDirection Direction { get; set; }

        public static HistoryRecord Parse(string a_recordText)
        {
            if (string.IsNullOrEmpty(a_recordText))
                throw new ArgumentNullException("a_recordText");

            MessageDirection _direction;
            var _sr = new StringReader(a_recordText);
            var _directionString = _sr.ReadLine();
            switch (_directionString)
            {
                case INCOMING_HEAD_STRING:
                    _direction = MessageDirection.Incoming;
                    break;
                case OUTGOING_HEAD_STRING:
                    _direction = MessageDirection.Outgoing;
                    break;
                default:
                    throw new Exception(string.Format("Нераспознана строка {0}. Запись: {1}", _directionString, a_recordText));
            }
            var _nikAndDateString = _sr.ReadLine();
            Debug.Assert(_nikAndDateString != null, "_nikAndDateString != null");
            var _pos = _nikAndDateString.LastIndexOf('(');
            var _dateString = _nikAndDateString.Remove(0, _pos + 1);
            var _date = DateTime.Parse(_dateString.Remove(_dateString.Length - 1));
            string _nik = _nikAndDateString.Remove(_pos - 1);

            string _msg = _sr.ReadToEnd();

            return new HistoryRecord
                {
                    Nik = _nik,
                    Date = _date,
                    Direction = _direction,
                    Message = _msg
                };
        }

        public override bool Equals(object a_value)
        {
            var _val = a_value as HistoryRecord;
            if (_val != null)
            {
                return Date.Equals(_val.Date) &&
                       Nik.Equals(_val.Nik) &&
                       Message.Equals(_val.Message) &&
                       Direction.Equals(_val.Direction);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return (Date.GetHashCode() +
                    Nik.GetHashCode() +
                    Message.GetHashCode() +
                    Direction.GetHashCode()).GetHashCode();
        }

        public override string ToString()
        {
            var _sb = new StringBuilder();
            string _directionString;
            switch (Direction)
            {
                case MessageDirection.Incoming:
                    _directionString = INCOMING_HEAD_STRING;
                    break;
                case MessageDirection.Outgoing:
                    _directionString = OUTGOING_HEAD_STRING;
                    break;
                default:
                    throw new Exception(string.Format("Не реализована ветка {0} для switch.", Direction));
            }
            _sb.AppendLine(_directionString);
            //Nik (12:26:26 19/03/2013)
            var _dateText = Date.ToString("HH:mm:ss d/MM/yyyy");
            // Заменяем точки так как форматирование не даст косых
            _dateText = _dateText.Replace('.', '/');
            _sb.AppendLine(string.Format("{0} ({1})", Nik, _dateText));
            _sb.Append(Message);
            return _sb.ToString();
        }
    }
}
