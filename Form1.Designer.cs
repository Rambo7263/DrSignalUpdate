namespace DrSignalUpdate
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.lbSignals = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txbPath = new System.Windows.Forms.TextBox();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.ckbAutoRun = new System.Windows.Forms.CheckBox();
            this.tmAutoRun = new System.Windows.Forms.Timer(this.components);
            this.btnUpdatePath = new System.Windows.Forms.Button();
            this.ckbAutoClose = new System.Windows.Forms.CheckBox();
            this.lblRunSec = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "訊號";
            // 
            // lbSignals
            // 
            this.lbSignals.FormattingEnabled = true;
            this.lbSignals.ItemHeight = 12;
            this.lbSignals.Location = new System.Drawing.Point(59, 38);
            this.lbSignals.MultiColumn = true;
            this.lbSignals.Name = "lbSignals";
            this.lbSignals.Size = new System.Drawing.Size(140, 160);
            this.lbSignals.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "路徑:";
            // 
            // txbPath
            // 
            this.txbPath.Location = new System.Drawing.Point(59, 6);
            this.txbPath.Name = "txbPath";
            this.txbPath.Size = new System.Drawing.Size(140, 22);
            this.txbPath.TabIndex = 3;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(205, 38);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(98, 23);
            this.btnUpdate.TabIndex = 4;
            this.btnUpdate.Text = "更新資料";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // ckbAutoRun
            // 
            this.ckbAutoRun.AutoSize = true;
            this.ckbAutoRun.Checked = true;
            this.ckbAutoRun.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbAutoRun.Location = new System.Drawing.Point(208, 67);
            this.ckbAutoRun.Name = "ckbAutoRun";
            this.ckbAutoRun.Size = new System.Drawing.Size(72, 16);
            this.ckbAutoRun.TabIndex = 5;
            this.ckbAutoRun.Text = "自動執行";
            this.ckbAutoRun.UseVisualStyleBackColor = true;
            // 
            // tmAutoRun
            // 
            this.tmAutoRun.Interval = 1000;
            this.tmAutoRun.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btnUpdatePath
            // 
            this.btnUpdatePath.Location = new System.Drawing.Point(205, 5);
            this.btnUpdatePath.Name = "btnUpdatePath";
            this.btnUpdatePath.Size = new System.Drawing.Size(98, 23);
            this.btnUpdatePath.TabIndex = 6;
            this.btnUpdatePath.Text = "更換路徑";
            this.btnUpdatePath.UseVisualStyleBackColor = true;
            this.btnUpdatePath.Click += new System.EventHandler(this.btnUpdatePath_Click);
            // 
            // ckbAutoClose
            // 
            this.ckbAutoClose.AutoSize = true;
            this.ckbAutoClose.Checked = true;
            this.ckbAutoClose.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbAutoClose.Location = new System.Drawing.Point(208, 89);
            this.ckbAutoClose.Name = "ckbAutoClose";
            this.ckbAutoClose.Size = new System.Drawing.Size(72, 16);
            this.ckbAutoClose.TabIndex = 7;
            this.ckbAutoClose.Text = "自動關閉";
            this.ckbAutoClose.UseVisualStyleBackColor = true;
            // 
            // lblRunSec
            // 
            this.lblRunSec.AutoSize = true;
            this.lblRunSec.Location = new System.Drawing.Point(286, 67);
            this.lblRunSec.Name = "lblRunSec";
            this.lblRunSec.Size = new System.Drawing.Size(0, 12);
            this.lblRunSec.TabIndex = 8;
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(12, 216);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(65, 12);
            this.lblMessage.TabIndex = 9;
            this.lblMessage.Text = "**********";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(312, 239);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.lblRunSec);
            this.Controls.Add(this.ckbAutoClose);
            this.Controls.Add(this.btnUpdatePath);
            this.Controls.Add(this.ckbAutoRun);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.txbPath);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbSignals);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Dr.每日訊號更新";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lbSignals;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txbPath;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.CheckBox ckbAutoRun;
        private System.Windows.Forms.Timer tmAutoRun;
        private System.Windows.Forms.Button btnUpdatePath;
        private System.Windows.Forms.CheckBox ckbAutoClose;
        private System.Windows.Forms.Label lblRunSec;
        private System.Windows.Forms.Label lblMessage;
    }
}

