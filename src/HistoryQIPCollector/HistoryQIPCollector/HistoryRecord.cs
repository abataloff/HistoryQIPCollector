using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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
            if(string.IsNullOrEmpty(a_recordText))
                throw new ArgumentNullException("a_recordText");

            var _nik = "";
            var _date = new DateTime();
            var _msg = "";
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
            var _pos = _nikAndDateString.LastIndexOf('(');
            var _dateString = _nikAndDateString.Remove(0, _pos+1);
            _date = DateTime.Parse(_dateString.Remove(_dateString.Length - 1));
            _nik = _nikAndDateString.Remove(_pos-1);

            _msg = _sr.ReadToEnd();

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
    }
}
