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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.getPersonPage = new System.Windows.Forms.TabPage();
            this.btnSubmitGetPerson = new System.Windows.Forms.Button();
            this.tbGetPersonResp = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbGetPersonReq = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.LogViewPage = new System.Windows.Forms.TabPage();
            this.btnLogPageReq = new System.Windows.Forms.Button();
            this.tbLogPageResp = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbLogPageReq = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbGatewayUrl = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.getPersonPage.SuspendLayout();
            this.LogViewPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.getPersonPage);
            this.tabControl1.Controls.Add(this.LogViewPage);
            this.tabControl1.Location = new System.Drawing.Point(-1, 49);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1257, 558);
            this.tabControl1.TabIndex = 0;
            // 
            // getPersonPage
            // 
            this.getPersonPage.Controls.Add(this.btnSubmitGetPerson);
            this.getPersonPage.Controls.Add(this.tbGetPersonResp);
            this.getPersonPage.Controls.Add(this.label3);
            this.getPersonPage.Controls.Add(this.tbGetPersonReq);
            this.getPersonPage.Controls.Add(this.label1);
            this.getPersonPage.Location = new System.Drawing.Point(4, 25);
            this.getPersonPage.Name = "getPersonPage";
            this.getPersonPage.Padding = new System.Windows.Forms.Padding(3);
            this.getPersonPage.Size = new System.Drawing.Size(1249, 529);
            this.getPersonPage.TabIndex = 0;
            this.getPersonPage.Text = "获取用户信息";
            this.getPersonPage.UseVisualStyleBackColor = true;
            // 
            // btnSubmitGetPerson
            // 
            this.btnSubmitGetPerson.Location = new System.Drawing.Point(1146, 17);
            this.btnSubmitGetPerson.Name = "btnSubmitGetPerson";
            this.btnSubmitGetPerson.Size = new System.Drawing.Size(97, 60);
            this.btnSubmitGetPerson.TabIndex = 4;
            this.btnSubmitGetPerson.Text = "提交获取用户信息";
            this.btnSubmitGetPerson.UseVisualStyleBackColor = true;
            this.btnSubmitGetPerson.Click += new System.EventHandler(this.btnSubmitGetPerson_Click);
            // 
            // tbGetPersonResp
            // 
            this.tbGetPersonResp.Location = new System.Drawing.Point(110, 142);
            this.tbGetPersonResp.Multiline = true;
            this.tbGetPersonResp.Name = "tbGetPersonResp";
            this.tbGetPersonResp.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbGetPersonResp.Size = new System.Drawing.Size(972, 299);
            this.tbGetPersonResp.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 142);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "响应内容：";
            // 
            // tbGetPersonReq
            // 
            this.tbGetPersonReq.Location = new System.Drawing.Point(110, 17);
            this.tbGetPersonReq.Multiline = true;
            this.tbGetPersonReq.Name = "tbGetPersonReq";
            this.tbGetPersonReq.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbGetPersonReq.Size = new System.Drawing.Size(972, 114);
            this.tbGetPersonReq.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "请求内容：";
            // 
            // LogViewPage
            // 
            this.LogViewPage.Controls.Add(this.btnLogPageReq);
            this.LogViewPage.Controls.Add(this.tbLogPageResp);
            this.LogViewPage.Controls.Add(this.label5);
            this.LogViewPage.Controls.Add(this.tbLogPageReq);
            this.LogViewPage.Controls.Add(this.label4);
            this.LogViewPage.Location = new System.Drawing.Point(4, 25);
            this.LogViewPage.Name = "LogViewPage";
            this.LogViewPage.Padding = new System.Windows.Forms.Padding(3);
            this.LogViewPage.Size = new System.Drawing.Size(1249, 529);
            this.LogViewPage.TabIndex = 1;
            this.LogViewPage.Text = "日志查看";
            this.LogViewPage.UseVisualStyleBackColor = true;
            // 
            // btnLogPageReq
            // 
            this.btnLogPageReq.Location = new System.Drawing.Point(1119, 34);
            this.btnLogPageReq.Name = "btnLogPageReq";
            this.btnLogPageReq.Size = new System.Drawing.Size(93, 52);
            this.btnLogPageReq.TabIndex = 4;
            this.btnLogPageReq.Text = "提  交";
            this.btnLogPageReq.UseVisualStyleBackColor = true;
            this.btnLogPageReq.Click += new System.EventHandler(this.btnLogPageReq_Click);
            // 
            // tbLogPageResp
            // 
            this.tbLogPageResp.Location = new System.Drawing.Point(97, 132);
            this.tbLogPageResp.Multiline = true;
            this.tbLogPageResp.Name = "tbLogPageResp";
            this.tbLogPageResp.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbLogPageResp.Size = new System.Drawing.Size(985, 374);
            this.tbLogPageResp.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 135);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 15);
            this.label5.TabIndex = 2;
            this.label5.Text = "响应内容：";
            // 
            // tbLogPageReq
            // 
            this.tbLogPageReq.Location = new System.Drawing.Point(97, 12);
            this.tbLogPageReq.Multiline = true;
            this.tbLogPageReq.Name = "tbLogPageReq";
            this.tbLogPageReq.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbLogPageReq.Size = new System.Drawing.Size(985, 106);
            this.tbLogPageReq.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 15);
            this.label4.TabIndex = 0;
            this.label4.Text = "请求内容：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "接口地址：";
            // 
            // tbGatewayUrl
            // 
            this.tbGatewayUrl.Location = new System.Drawing.Point(100, 12);
            this.tbGatewayUrl.Name = "tbGatewayUrl";
            this.tbGatewayUrl.Size = new System.Drawing.Size(985, 25);
            this.tbGatewayUrl.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1268, 619);
            this.Controls.Add(this.tbGatewayUrl);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "接口调式程序";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.getPersonPage.ResumeLayout(false);
            this.getPersonPage.PerformLayout();
            this.LogViewPage.ResumeLayout(false);
            this.LogViewPage.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage getPersonPage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage LogViewPage;
        private System.Windows.Forms.TextBox tbGetPersonReq;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbGatewayUrl;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbGetPersonResp;
        private System.Windows.Forms.Button btnSubmitGetPerson;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbLogPageResp;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbLogPageReq;
        private System.Windows.Forms.Button btnLogPageReq;
    }
}

