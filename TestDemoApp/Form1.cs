using System;
using System.IO;

using System.Text;
using System.Windows.Forms;
using WebRegComLib;

namespace HospitalInsurance.TestDemoApp
{
    public partial class Form1 : Form
    {     
        private WebRegClass cd;

        public Form1()
        {
            InitializeComponent();

            cd = new WebRegClass();
        }

        private void btnGetPerson_Click(object sender, EventArgs e)
        {
            try
            {
                lbActionName.Text = "GetPersonInfo_Web";
                using (StreamReader sr = new StreamReader(Environment.CurrentDirectory + "/PersonInWeb.xml", Encoding.UTF8))
                {
                    tbInput.Text = sr.ReadToEnd();

                    cd.GetPersonInfo_Web(tbInput.Text, out string outXml);
                    tbResult.Text = outXml;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);                 
            }
        }


        private void btnDivide_Click(object sender, EventArgs e)
        {
            try
            {
                lbActionName.Text = "Divide_Web";
                using (StreamReader sr = new StreamReader(Environment.CurrentDirectory + "/DivideInWeb.xml", Encoding.UTF8))
                {
                    tbInput.Text = sr.ReadToEnd();
                    cd.Divide_Web(tbInput.Text, out string outXml);
                    tbResult.Text = outXml;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void btnTrade_Click(object sender, EventArgs e)
        {
            try
            {
                lbActionName.Text = "Trade_Web";
                cd.Trade_Web(out string outXml);
                tbResult.Text = outXml;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void btnRefundment_Click(object sender, EventArgs e)
        {
            try
            {
                lbActionName.Text = "Refundment_Web";
                using (StreamReader sr = new StreamReader(Environment.CurrentDirectory + "/RefundmentInWeb.xml", Encoding.UTF8))
                {
                    tbInput.Text = sr.ReadToEnd();
                    cd.Refundment_Web(tbInput.Text, out string outXml);
                    tbResult.Text = outXml;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }


        private void btnCommitTradeState_Click(object sender, EventArgs e)
        {
            try
            {
                lbActionName.Text = "CommitTradeState_Web";
                cd.CommitTradeState_Web(tbInput.Text, out string outXml);
                tbResult.Text = outXml;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }
    }
}
