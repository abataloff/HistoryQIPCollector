using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HistoryQIPCollector
{
    class QipHistoryCollector
    {
        private QipHistoryCollectorSettings _settings;

        public QipHistoryCollector(QipHistoryCollectorSettings _settings)
        {
            // TODO: Complete member initialization
            this._settings = _settings;
        }

        internal HistoryFolder Collect(List<HistoryFolder> _sources)
        {
            throw new NotImplementedException();
        }
    }
}
