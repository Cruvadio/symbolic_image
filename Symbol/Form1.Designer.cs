namespace Symbol
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
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
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtFormXFormula = new System.Windows.Forms.TextBox();
            this.txtFormYFormula = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbBoxActions = new System.Windows.Forms.ComboBox();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtFormXMin = new System.Windows.Forms.TextBox();
            this.txtFormYMax = new System.Windows.Forms.TextBox();
            this.txtFormYMin = new System.Windows.Forms.TextBox();
            this.txtFormXMax = new System.Windows.Forms.TextBox();
            this.txtFormAValue = new System.Windows.Forms.TextBox();
            this.txtFormBValue = new System.Windows.Forms.TextBox();
            this.txtFormDeltaValue = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textBoxCast = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtFormXFormula
            // 
            this.txtFormXFormula.Location = new System.Drawing.Point(80, 37);
            this.txtFormXFormula.Name = "txtFormXFormula";
            this.txtFormXFormula.Size = new System.Drawing.Size(132, 20);
            this.txtFormXFormula.TabIndex = 0;
            // 
            // txtFormYFormula
            // 
            this.txtFormYFormula.Location = new System.Drawing.Point(80, 92);
            this.txtFormYFormula.Name = "txtFormYFormula";
            this.txtFormYFormula.Size = new System.Drawing.Size(132, 20);
            this.txtFormYFormula.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "f(x,y) =";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(53, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "x =";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(50, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(24, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "y = ";
            // 
            // cmbBoxActions
            // 
            this.cmbBoxActions.FormattingEnabled = true;
            this.cmbBoxActions.Items.AddRange(new object[] {
            "Построить символический образ",
            "Выполнить топологическую сортировку"});
            this.cmbBoxActions.Location = new System.Drawing.Point(264, 37);
            this.cmbBoxActions.Name = "cmbBoxActions";
            this.cmbBoxActions.Size = new System.Drawing.Size(307, 21);
            this.cmbBoxActions.TabIndex = 5;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(130, 176);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(307, 23);
            this.btnSubmit.TabIndex = 6;
            this.btnSubmit.Text = "Начать обработку";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.submit_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(264, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Действия";
            // 
            // txtFormXMin
            // 
            this.txtFormXMin.Location = new System.Drawing.Point(264, 92);
            this.txtFormXMin.Name = "txtFormXMin";
            this.txtFormXMin.Size = new System.Drawing.Size(33, 20);
            this.txtFormXMin.TabIndex = 8;
            // 
            // txtFormYMax
            // 
            this.txtFormYMax.Location = new System.Drawing.Point(312, 118);
            this.txtFormYMax.Name = "txtFormYMax";
            this.txtFormYMax.Size = new System.Drawing.Size(33, 20);
            this.txtFormYMax.TabIndex = 9;
            // 
            // txtFormYMin
            // 
            this.txtFormYMin.Location = new System.Drawing.Point(264, 118);
            this.txtFormYMin.Name = "txtFormYMin";
            this.txtFormYMin.Size = new System.Drawing.Size(33, 20);
            this.txtFormYMin.TabIndex = 10;
            // 
            // txtFormXMax
            // 
            this.txtFormXMax.Location = new System.Drawing.Point(312, 92);
            this.txtFormXMax.Name = "txtFormXMax";
            this.txtFormXMax.Size = new System.Drawing.Size(33, 20);
            this.txtFormXMax.TabIndex = 11;
            // 
            // txtFormAValue
            // 
            this.txtFormAValue.Location = new System.Drawing.Point(376, 92);
            this.txtFormAValue.Name = "txtFormAValue";
            this.txtFormAValue.Size = new System.Drawing.Size(33, 20);
            this.txtFormAValue.TabIndex = 12;
            // 
            // txtFormBValue
            // 
            this.txtFormBValue.Location = new System.Drawing.Point(415, 92);
            this.txtFormBValue.Name = "txtFormBValue";
            this.txtFormBValue.Size = new System.Drawing.Size(33, 20);
            this.txtFormBValue.TabIndex = 13;
            // 
            // txtFormDeltaValue
            // 
            this.txtFormDeltaValue.Location = new System.Drawing.Point(538, 92);
            this.txtFormDeltaValue.Name = "txtFormDeltaValue";
            this.txtFormDeltaValue.Size = new System.Drawing.Size(33, 20);
            this.txtFormDeltaValue.TabIndex = 14;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(248, 75);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(115, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Область применения";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(386, 75);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(13, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "a";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(424, 75);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(13, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "b";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(237, 95);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(18, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = "x: ";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(237, 125);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(18, 13);
            this.label9.TabIndex = 19;
            this.label9.Text = "y: ";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(515, 76);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(56, 13);
            this.label10.TabIndex = 20;
            this.label10.Text = "Интервал";
            this.label10.Click += new System.EventHandler(this.label10_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(590, 24);
            this.menuStrip1.TabIndex = 21;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.helpToolStripMenuItem.Text = "Справка";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.aboutToolStripMenuItem.Text = "О приложении";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // textBoxCast
            // 
            this.textBoxCast.Location = new System.Drawing.Point(538, 125);
            this.textBoxCast.Name = "textBoxCast";
            this.textBoxCast.Size = new System.Drawing.Size(33, 20);
            this.textBoxCast.TabIndex = 22;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(388, 128);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(147, 13);
            this.label11.TabIndex = 23;
            this.label11.Text = "Количество точек в клетке:";
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Location = new System.Drawing.Point(15, 207);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(0, 13);
            this.lblTime.TabIndex = 24;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(590, 232);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.textBoxCast);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtFormDeltaValue);
            this.Controls.Add(this.txtFormBValue);
            this.Controls.Add(this.txtFormAValue);
            this.Controls.Add(this.txtFormXMax);
            this.Controls.Add(this.txtFormYMin);
            this.Controls.Add(this.txtFormYMax);
            this.Controls.Add(this.txtFormXMin);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.cmbBoxActions);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtFormYFormula);
            this.Controls.Add(this.txtFormXFormula);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtFormXFormula;
        private System.Windows.Forms.TextBox txtFormYFormula;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbBoxActions;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtFormXMin;
        private System.Windows.Forms.TextBox txtFormYMax;
        private System.Windows.Forms.TextBox txtFormYMin;
        private System.Windows.Forms.TextBox txtFormXMax;
        private System.Windows.Forms.TextBox txtFormAValue;
        private System.Windows.Forms.TextBox txtFormBValue;
        private System.Windows.Forms.TextBox txtFormDeltaValue;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.TextBox textBoxCast;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblTime;
    }
}

