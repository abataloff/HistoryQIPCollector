using HistoryQIPCollector.Properties;

namespace HistoryQIPCollector
{
    partial class Main
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.v_btnAddSourceDir = new System.Windows.Forms.Button();
            this.v_dgvSourceDirectories = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.v_btnChooseOutDirectory = new System.Windows.Forms.Button();
            this.v_tbOutDirectory = new System.Windows.Forms.TextBox();
            this.v_btnStart = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.v_dgvSourceDirectories)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.v_btnAddSourceDir);
            this.groupBox1.Controls.Add(this.v_dgvSourceDirectories);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(622, 295);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Источники";
            // 
            // v_btnAddSourceDir
            // 
            this.v_btnAddSourceDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.v_btnAddSourceDir.Location = new System.Drawing.Point(539, 268);
            this.v_btnAddSourceDir.Name = "v_btnAddSourceDir";
            this.v_btnAddSourceDir.Size = new System.Drawing.Size(77, 21);
            this.v_btnAddSourceDir.TabIndex = 1;
            this.v_btnAddSourceDir.Text = "Добавить";
            this.v_btnAddSourceDir.UseVisualStyleBackColor = true;
            this.v_btnAddSourceDir.Click += new System.EventHandler(this.v_btnAddSourceDir_Click);
            // 
            // v_dgvSourceDirectories
            // 
            this.v_dgvSourceDirectories.AllowUserToAddRows = false;
            this.v_dgvSourceDirectories.AllowUserToResizeColumns = false;
            this.v_dgvSourceDirectories.AllowUserToResizeRows = false;
            this.v_dgvSourceDirectories.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.v_dgvSourceDirectories.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.v_dgvSourceDirectories.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.v_dgvSourceDirectories.ColumnHeadersVisible = false;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.v_dgvSourceDirectories.DefaultCellStyle = dataGridViewCellStyle2;
            this.v_dgvSourceDirectories.Location = new System.Drawing.Point(6, 19);
            this.v_dgvSourceDirectories.Name = "v_dgvSourceDirectories";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.v_dgvSourceDirectories.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.v_dgvSourceDirectories.RowHeadersVisible = false;
            this.v_dgvSourceDirectories.Size = new System.Drawing.Size(610, 243);
            this.v_dgvSourceDirectories.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.v_btnChooseOutDirectory);
            this.groupBox2.Controls.Add(this.v_tbOutDirectory);
            this.groupBox2.Location = new System.Drawing.Point(12, 313);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(619, 50);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Выходная директория";
            // 
            // v_btnChooseOutDirectory
            // 
            this.v_btnChooseOutDirectory.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.v_btnChooseOutDirectory.Location = new System.Drawing.Point(590, 17);
            this.v_btnChooseOutDirectory.Name = "v_btnChooseOutDirectory";
            this.v_btnChooseOutDirectory.Size = new System.Drawing.Size(23, 23);
            this.v_btnChooseOutDirectory.TabIndex = 1;
            this.v_btnChooseOutDirectory.Text = "...";
            this.v_btnChooseOutDirectory.UseVisualStyleBackColor = true;
            this.v_btnChooseOutDirectory.Click += new System.EventHandler(this.v_btnChooseOutDirectory_Click);
            // 
            // v_tbOutDirectory
            // 
            this.v_tbOutDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.v_tbOutDirectory.Location = new System.Drawing.Point(6, 19);
            this.v_tbOutDirectory.Name = "v_tbOutDirectory";
            this.v_tbOutDirectory.Size = new System.Drawing.Size(578, 20);
            this.v_tbOutDirectory.TabIndex = 0;
            // 
            // v_btnStart
            // 
            this.v_btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.v_btnStart.Location = new System.Drawing.Point(559, 449);
            this.v_btnStart.Name = "v_btnStart";
            this.v_btnStart.Size = new System.Drawing.Size(75, 23);
            this.v_btnStart.TabIndex = 2;
            this.v_btnStart.Text = "Старт";
            this.v_btnStart.UseVisualStyleBackColor = true;
            this.v_btnStart.Click += new System.EventHandler(this.v_btnStart_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Location = new System.Drawing.Point(12, 372);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(622, 71);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Настройки";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(646, 484);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.v_btnStart);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "Main";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Сборщик пользовательской истори QIP";
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.v_dgvSourceDirectories)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView v_dgvSourceDirectories;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button v_btnChooseOutDirectory;
        private System.Windows.Forms.TextBox v_tbOutDirectory;
        private System.Windows.Forms.Button v_btnAddSourceDir;
        private System.Windows.Forms.Button v_btnStart;
        private System.Windows.Forms.GroupBox groupBox3;
    }
}

