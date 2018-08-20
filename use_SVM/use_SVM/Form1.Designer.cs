namespace use_SVM
{
    partial class MainForm
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
            this.btnSVM = new System.Windows.Forms.Button();
            this.lblAccuracy = new System.Windows.Forms.Label();
            this.btnTrain = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSVM
            // 
            this.btnSVM.Location = new System.Drawing.Point(56, 36);
            this.btnSVM.Name = "btnSVM";
            this.btnSVM.Size = new System.Drawing.Size(75, 23);
            this.btnSVM.TabIndex = 0;
            this.btnSVM.Text = "SVM";
            this.btnSVM.UseVisualStyleBackColor = true;
            this.btnSVM.Click += new System.EventHandler(this.btnSVM_Click);
            // 
            // lblAccuracy
            // 
            this.lblAccuracy.AutoSize = true;
            this.lblAccuracy.Location = new System.Drawing.Point(66, 9);
            this.lblAccuracy.Name = "lblAccuracy";
            this.lblAccuracy.Size = new System.Drawing.Size(53, 12);
            this.lblAccuracy.TabIndex = 1;
            this.lblAccuracy.Text = "準確率：";
            // 
            // btnTrain
            // 
            this.btnTrain.Location = new System.Drawing.Point(56, 87);
            this.btnTrain.Name = "btnTrain";
            this.btnTrain.Size = new System.Drawing.Size(75, 23);
            this.btnTrain.TabIndex = 2;
            this.btnTrain.Text = "TrainInfo";
            this.btnTrain.UseVisualStyleBackColor = true;
            this.btnTrain.Click += new System.EventHandler(this.btnTrain_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(191, 149);
            this.Controls.Add(this.btnTrain);
            this.Controls.Add(this.lblAccuracy);
            this.Controls.Add(this.btnSVM);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSVM;
        private System.Windows.Forms.Label lblAccuracy;
        private System.Windows.Forms.Button btnTrain;
    }
}

