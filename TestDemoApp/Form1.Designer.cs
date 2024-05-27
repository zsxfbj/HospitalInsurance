namespace HospitalInsurance.TestDemoApp
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.lbActionName = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbInput = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbResult = new System.Windows.Forms.TextBox();
            this.btnGetPerson = new System.Windows.Forms.Button();
            this.btnDivide = new System.Windows.Forms.Button();
            this.btnTrade = new System.Windows.Forms.Button();
            this.btnRefundment = new System.Windows.Forms.Button();
            this.btnCommitTradeState = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "操作方法：";
            // 
            // lbActionName
            // 
            this.lbActionName.AutoSize = true;
            this.lbActionName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbActionName.Location = new System.Drawing.Point(112, 26);
            this.lbActionName.Name = "lbActionName";
            this.lbActionName.Size = new System.Drawing.Size(0, 15);
            this.lbActionName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "请求内容：";
            // 
            // tbInput
            // 
            this.tbInput.Location = new System.Drawing.Point(115, 68);
            this.tbInput.Multiline = true;
            this.tbInput.Name = "tbInput";
            this.tbInput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbInput.Size = new System.Drawing.Size(630, 161);
            this.tbInput.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 264);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "响应内容：";
            // 
            // tbResult
            // 
            this.tbResult.Location = new System.Drawing.Point(115, 261);
            this.tbResult.Multiline = true;
            this.tbResult.Name = "tbResult";
            this.tbResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbResult.Size = new System.Drawing.Size(630, 439);
            this.tbResult.TabIndex = 5;
            // 
            // btnGetPerson
            // 
            this.btnGetPerson.Location = new System.Drawing.Point(770, 92);
            this.btnGetPerson.Name = "btnGetPerson";
            this.btnGetPerson.Size = new System.Drawing.Size(162, 23);
            this.btnGetPerson.TabIndex = 6;
            this.btnGetPerson.Text = "GetPersonInfo_Web";
            this.btnGetPerson.UseVisualStyleBackColor = true;
            this.btnGetPerson.Click += new System.EventHandler(this.btnGetPerson_Click);
            // 
            // btnDivide
            // 
            this.btnDivide.Location = new System.Drawing.Point(770, 142);
            this.btnDivide.Name = "btnDivide";
            this.btnDivide.Size = new System.Drawing.Size(162, 23);
            this.btnDivide.TabIndex = 7;
            this.btnDivide.Text = "Divide_Web";
            this.btnDivide.UseVisualStyleBackColor = true;
            this.btnDivide.Click += new System.EventHandler(this.btnDivide_Click);
            // 
            // btnTrade
            // 
            this.btnTrade.Location = new System.Drawing.Point(770, 195);
            this.btnTrade.Name = "btnTrade";
            this.btnTrade.Size = new System.Drawing.Size(162, 23);
            this.btnTrade.TabIndex = 8;
            this.btnTrade.Text = "Trade_Web";
            this.btnTrade.UseVisualStyleBackColor = true;
            this.btnTrade.Click += new System.EventHandler(this.btnTrade_Click);
            // 
            // btnRefundment
            // 
            this.btnRefundment.Location = new System.Drawing.Point(770, 244);
            this.btnRefundment.Name = "btnRefundment";
            this.btnRefundment.Size = new System.Drawing.Size(162, 23);
            this.btnRefundment.TabIndex = 9;
            this.btnRefundment.Text = "Refundment_Web";
            this.btnRefundment.UseVisualStyleBackColor = true;
            this.btnRefundment.Click += new System.EventHandler(this.btnRefundment_Click);
            // 
            // btnCommitTradeState
            // 
            this.btnCommitTradeState.Location = new System.Drawing.Point(770, 297);
            this.btnCommitTradeState.Name = "btnCommitTradeState";
            this.btnCommitTradeState.Size = new System.Drawing.Size(162, 26);
            this.btnCommitTradeState.TabIndex = 10;
            this.btnCommitTradeState.Text = "CommitTradeState";
            this.btnCommitTradeState.UseVisualStyleBackColor = true;
            this.btnCommitTradeState.Click += new System.EventHandler(this.btnCommitTradeState_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(963, 712);
            this.Controls.Add(this.btnCommitTradeState);
            this.Controls.Add(this.btnRefundment);
            this.Controls.Add(this.btnTrade);
            this.Controls.Add(this.btnDivide);
            this.Controls.Add(this.btnGetPerson);
            this.Controls.Add(this.tbResult);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbInput);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbActionName);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "接口调式程序";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbActionName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbInput;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbResult;
        private System.Windows.Forms.Button btnGetPerson;
        private System.Windows.Forms.Button btnDivide;
        private System.Windows.Forms.Button btnTrade;
        private System.Windows.Forms.Button btnRefundment;
        private System.Windows.Forms.Button btnCommitTradeState;
    }
}

