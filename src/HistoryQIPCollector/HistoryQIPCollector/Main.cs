using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using NLog;

namespace HistoryQIPCollector
{
    public partial class Main : Form
    {
        private const string SOURCE_DIRECTORY_COLUMN_NAME = "Dir";

        private static Logger log = LogManager.GetCurrentClassLogger();

        public Main()
        {
            InitializeComponent();

            // Инициализация таблицы Источников
            v_dgvSourceDirectories.Columns.Add(SOURCE_DIRECTORY_COLUMN_NAME, "");
        }

        private void v_btnChooseOutDirectory_Click(object a_sender, EventArgs a_e)
        {
            var _fbd = new FolderBrowserDialog();
            if (_fbd.ShowDialog() == DialogResult.OK)
            {
                v_tbOutDirectory.Text = _fbd.SelectedPath;
            }
        }

        private void v_btnAddSourceDir_Click(object a_sender, EventArgs a_e)
        {
            var _fbd = new FolderBrowserDialog();
            if (_fbd.ShowDialog() == DialogResult.OK)
            {
                var _addedRowNumber = v_dgvSourceDirectories.Rows.Add();
                var _row = v_dgvSourceDirectories.Rows[_addedRowNumber];
                _row.Cells[SOURCE_DIRECTORY_COLUMN_NAME].Value = _fbd.SelectedPath;
                
            }
        }

        private void v_btnStart_Click(object a_sender, EventArgs a_e)
        {
            // Набор входных историй
            var _sourceDirs =
                (from DataGridViewRow _row in v_dgvSourceDirectories.Rows select _row.Cells[SOURCE_DIRECTORY_COLUMN_NAME].Value.ToString()).ToList();

            // Путь для выходной истории
            var _resultDir = v_tbOutDirectory.Text;

            // Если данные не заданы
            if (_sourceDirs.Count == 0 || string.IsNullOrEmpty(_resultDir))
            {
                // выводим соответствующие сообщение
                MessageBox.Show(@"Проверьте корректность введеных данных");
            }
            else
            {
                // иначе, начинаем объединять истории
                var _settings = new QipHistoryCollectorSettings();
                var _collector = new QipHistoryCollector(_settings);
                var _sources = new List<HistoryFolder>();
                foreach (var _dir in _sourceDirs)
                {
                    try
                    {
                        log.Trace("Добавление директрии {0}", _dir);
                        _sources.Add(HistoryFolder.Read(_dir));
                    }
                    catch (Exception _ex)
                    {
                        log.ErrorException(string.Format("Ошибка при чтении директрии истории {0}", _dir), _ex);
                    }
                }
                var _result = _collector.Collect(_sources);
                _result.Write(_resultDir);
            }
        }
    }
}
