using System;
using DataDynamics.ActiveReports;
using DataDynamics.ActiveReports.Document;

using System.Data;

namespace RSMPS
{
    public delegate void HandleTotalValues(decimal value1, decimal value2, decimal value3);

	public class rprtBudgetDetail : DataDynamics.ActiveReports.ActiveReport3
	{
        int miExpNumItems;
        decimal mdExpMuDlrs;
        decimal mdExpTotDlrs;

        string fltrVal = "";
        private Label label15;
        private Line Line4;
        private Line line5;
        private TextBox txtTotalHours;
        private TextBox txtTotalLoadedRate;
        private Label label8;
        private Label label9;
        private TextBox textBox22;
        private TextBox textBox23;
        private TextBox textBox24;
        decimal currentExpenseValue = 0;

        public void SetTitles(string customer, string desc, string number, string revision, string wbs)
        {
            lblCustomerLocation.Text = customer;
            lblJobDescription.Text = desc;

            if (wbs.Length > 0)
                lblJobNumber.Text = number + " - WBS: " + wbs;
            else
                lblJobNumber.Text = number;

            lblRevision.Text = revision;
        }

		public rprtBudgetDetail()
		{
			InitializeComponent();
		}

		private void GroupFooter1_Format(object sender, System.EventArgs eArgs)
		{
            lblDeptTotals.Text = "Total " + txtDiscipline.Value.ToString() + " (" + txtDepartment.Value.ToString() + ")";

            rprtBudgetDetailExps exp = new rprtBudgetDetailExps();

            exp.OnReportTotaled += new RevSol.PassMultiDataStrings(exp_OnReportTotaled);
            exp.DataSource = ((DataSet)this.DataSource).Tables["Table1"].Select(fltrVal);
            exp.SetTitle(txtDiscipline.Value.ToString(), txtDepartment.Value.ToString());
            exp.OnTotalRun += new HandleTotalValues(exp_OnTotalRun);

            this.subRprtExpenses.Report = exp;
		}

        void exp_OnReportTotaled(string[] vals, int count)
        {
            miExpNumItems += Convert.ToInt32(vals[0]);
            mdExpMuDlrs += Convert.ToDecimal(vals[1]);
            mdExpTotDlrs += Convert.ToDecimal(vals[2]);
        }

        void exp_OnTotalRun(decimal value1, decimal value2, decimal value3)
        {
            currentExpenseValue = value1;
        }

		private void GroupHeader1_Format(object sender, System.EventArgs eArgs)
		{
            currentExpenseValue = 0;

            if (txtDiscipline.Text.Length < 1)
            {
                GroupHeader1.Visible = false;
                GroupHeader2.Visible = false;
                GroupHeader3.Visible = false;
                GroupHeader4.Visible = false;
            }
            else
            {
                GroupHeader1.Visible = true;
                GroupHeader2.Visible = true;
                GroupHeader3.Visible = true;
                GroupHeader4.Visible = true;
            }

            if (txtDepartment.Value == null)
                fltrVal = "DeptGroup = 1000";
            else
                fltrVal = "DeptGroup = " + txtDepartment.Value.ToString();
		}

		private void rprtBudgetDetail_ReportStart(object sender, System.EventArgs eArgs)
		{
            lblDateTime.Text = DateTime.Now.ToShortDateString();

            miExpNumItems = 0;
            mdExpMuDlrs = 0;
            mdExpTotDlrs = 0;

            RSLib.COSecurity sec = new RSLib.COSecurity();
            CBUser u = new CBUser();

            sec.InitAppSettings();
            u.Load(sec.UserID);

            if (u.IsAdministrator == false && u.IsEngineerAdmin == false && u.IsManager == false)
            {
                TextBox5.Visible = false;
                TextBox6.Visible = false;
                TextBox12.Visible = false;
                TextBox10.Visible = false;
                TextBox18.Visible = false;
                TextBox17.Visible = false;
            }
		}

		private void ReportFooter_Format(object sender, System.EventArgs eArgs)
		{
            //txtEngQty.Value = 0;
            //txtEngTotalHrs.Value = 0;

            if (Convert.ToInt32(txtEngTotalHrs.Value) != 0)
                txtEngLdRt.Value = Convert.ToDecimal(txtEngTotLdDlrs.Value) / Convert.ToDecimal(txtEngTotalHrs.Value);
            else
                txtEngLdRt.Value = 0;

            //txtEngTotLdDlrs.Value = 0;

            txtNonEngTotHrs.Value = 0;
            if (Convert.ToDecimal(txtEngTotalHrs.Value) != 0)
            {
                txtNonEngLdRt.Value = mdExpTotDlrs / Convert.ToDecimal(txtEngTotalHrs.Value);
            }
            else
            {
                txtNonEngLdRt.Value = 0;
            }
            txtNonEngTotLdDlrs.Value = mdExpTotDlrs;

            txtTotalHours.Value = Convert.ToDecimal(txtEngTotalHrs.Value);
            if (Convert.ToDecimal(txtEngTotalHrs.Value) != 0)
            {
                txtTotalLoadedRate.Value = (Convert.ToDecimal(txtEngTotLdDlrs.Value) + mdExpTotDlrs) / Convert.ToDecimal(txtEngTotalHrs.Value);
            }
            else
            {
                txtTotalLoadedRate.Value = 0;
            }
            txtTotalLoadedDlrs.Value = Convert.ToDecimal(txtEngTotLdDlrs.Value) + mdExpTotDlrs;
		}

		#region ActiveReports Designer generated code
		private DataDynamics.ActiveReports.ReportHeader ReportHeader = null;
		private DataDynamics.ActiveReports.PageHeader PageHeader = null;
		private DataDynamics.ActiveReports.Picture Picture = null;
		private DataDynamics.ActiveReports.Label lblCustomerLocation = null;
		private DataDynamics.ActiveReports.Label lblJobDescription = null;
		private DataDynamics.ActiveReports.Label lblJobNumber = null;
		private DataDynamics.ActiveReports.Label lblRevision = null;
		private DataDynamics.ActiveReports.GroupHeader GroupHeader1 = null;
		private DataDynamics.ActiveReports.Shape Shape1 = null;
		private DataDynamics.ActiveReports.TextBox txtDepartment = null;
		private DataDynamics.ActiveReports.Line Line = null;
		private DataDynamics.ActiveReports.Label Label1 = null;
		private DataDynamics.ActiveReports.Label Label2 = null;
		private DataDynamics.ActiveReports.Label Label3 = null;
		private DataDynamics.ActiveReports.Label Label4 = null;
		private DataDynamics.ActiveReports.Label Label5 = null;
		private DataDynamics.ActiveReports.Label Label6 = null;
		private DataDynamics.ActiveReports.TextBox txtDiscipline = null;
		private DataDynamics.ActiveReports.GroupHeader GroupHeader2 = null;
		private DataDynamics.ActiveReports.Shape Shape2 = null;
		private DataDynamics.ActiveReports.TextBox TextBox7 = null;
		private DataDynamics.ActiveReports.Line Line1 = null;
		private DataDynamics.ActiveReports.TextBox TextBox11 = null;
		private DataDynamics.ActiveReports.TextBox TextBox12 = null;
		private DataDynamics.ActiveReports.TextBox TextBox19 = null;
		private DataDynamics.ActiveReports.GroupHeader GroupHeader3 = null;
		private DataDynamics.ActiveReports.Shape Shape3 = null;
		private DataDynamics.ActiveReports.TextBox TextBox9 = null;
		private DataDynamics.ActiveReports.Line Line2 = null;
		private DataDynamics.ActiveReports.TextBox TextBox8 = null;
		private DataDynamics.ActiveReports.TextBox TextBox10 = null;
		private DataDynamics.ActiveReports.TextBox TextBox20 = null;
		private DataDynamics.ActiveReports.GroupHeader GroupHeader4 = null;
		private DataDynamics.ActiveReports.Shape Shape4 = null;
		private DataDynamics.ActiveReports.TextBox TextBox13 = null;
		private DataDynamics.ActiveReports.TextBox TextBox14 = null;
		private DataDynamics.ActiveReports.TextBox TextBox18 = null;
		private DataDynamics.ActiveReports.TextBox TextBox21 = null;
		private DataDynamics.ActiveReports.Detail Detail = null;
		private DataDynamics.ActiveReports.TextBox TextBox = null;
		private DataDynamics.ActiveReports.TextBox TextBox1 = null;
		private DataDynamics.ActiveReports.TextBox TextBox2 = null;
		private DataDynamics.ActiveReports.TextBox TextBox3 = null;
		private DataDynamics.ActiveReports.TextBox TextBox4 = null;
		private DataDynamics.ActiveReports.TextBox TextBox5 = null;
		private DataDynamics.ActiveReports.TextBox TextBox6 = null;
		private DataDynamics.ActiveReports.GroupFooter GroupFooter4 = null;
		private DataDynamics.ActiveReports.GroupFooter GroupFooter3 = null;
		private DataDynamics.ActiveReports.GroupFooter GroupFooter2 = null;
		private DataDynamics.ActiveReports.GroupFooter GroupFooter1 = null;
		private DataDynamics.ActiveReports.Shape Shape = null;
		private DataDynamics.ActiveReports.SubReport subRprtExpenses = null;
		private DataDynamics.ActiveReports.TextBox TextBox15 = null;
		private DataDynamics.ActiveReports.TextBox TextBox16 = null;
		private DataDynamics.ActiveReports.TextBox TextBox17 = null;
		private DataDynamics.ActiveReports.Label lblDeptTotals = null;
		private DataDynamics.ActiveReports.PageFooter PageFooter = null;
		private DataDynamics.ActiveReports.Label lblDateTime = null;
		private DataDynamics.ActiveReports.ReportFooter ReportFooter = null;
		private DataDynamics.ActiveReports.Shape Shape5 = null;
		private DataDynamics.ActiveReports.Label Label = null;
        private DataDynamics.ActiveReports.Label Label7 = null;
		private DataDynamics.ActiveReports.Label Label10 = null;
        private DataDynamics.ActiveReports.Label Label11 = null;
        private DataDynamics.ActiveReports.Label Label13 = null;
		private DataDynamics.ActiveReports.TextBox txtEngTotalHrs = null;
        private DataDynamics.ActiveReports.TextBox txtEngLdRt = null;
        private DataDynamics.ActiveReports.TextBox txtEngTotLdDlrs = null;
		private DataDynamics.ActiveReports.TextBox txtNonEngTotHrs = null;
        private DataDynamics.ActiveReports.TextBox txtNonEngLdRt = null;
		private DataDynamics.ActiveReports.TextBox txtNonEngTotLdDlrs = null;
        private DataDynamics.ActiveReports.Line Line3 = null;
		private DataDynamics.ActiveReports.Line Line8 = null;
		private DataDynamics.ActiveReports.Line Line9 = null;
		private DataDynamics.ActiveReports.Line Line10 = null;
		private DataDynamics.ActiveReports.Shape Shape6 = null;
        private DataDynamics.ActiveReports.Label Label14 = null;
		private DataDynamics.ActiveReports.TextBox txtTotalLoadedDlrs = null;
		private DataDynamics.ActiveReports.Line Line11 = null;
		private DataDynamics.ActiveReports.Line Line12 = null;
		public void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(rprtBudgetDetail));
            this.Detail = new DataDynamics.ActiveReports.Detail();
            this.TextBox = new DataDynamics.ActiveReports.TextBox();
            this.TextBox1 = new DataDynamics.ActiveReports.TextBox();
            this.TextBox2 = new DataDynamics.ActiveReports.TextBox();
            this.TextBox3 = new DataDynamics.ActiveReports.TextBox();
            this.TextBox4 = new DataDynamics.ActiveReports.TextBox();
            this.TextBox5 = new DataDynamics.ActiveReports.TextBox();
            this.TextBox6 = new DataDynamics.ActiveReports.TextBox();
            this.textBox24 = new DataDynamics.ActiveReports.TextBox();
            this.ReportHeader = new DataDynamics.ActiveReports.ReportHeader();
            this.ReportFooter = new DataDynamics.ActiveReports.ReportFooter();
            this.Shape5 = new DataDynamics.ActiveReports.Shape();
            this.Label = new DataDynamics.ActiveReports.Label();
            this.Label7 = new DataDynamics.ActiveReports.Label();
            this.Label10 = new DataDynamics.ActiveReports.Label();
            this.Label11 = new DataDynamics.ActiveReports.Label();
            this.Label13 = new DataDynamics.ActiveReports.Label();
            this.txtEngTotalHrs = new DataDynamics.ActiveReports.TextBox();
            this.txtEngLdRt = new DataDynamics.ActiveReports.TextBox();
            this.txtEngTotLdDlrs = new DataDynamics.ActiveReports.TextBox();
            this.txtNonEngTotHrs = new DataDynamics.ActiveReports.TextBox();
            this.txtNonEngLdRt = new DataDynamics.ActiveReports.TextBox();
            this.txtNonEngTotLdDlrs = new DataDynamics.ActiveReports.TextBox();
            this.Line3 = new DataDynamics.ActiveReports.Line();
            this.Line4 = new DataDynamics.ActiveReports.Line();
            this.Line8 = new DataDynamics.ActiveReports.Line();
            this.Line9 = new DataDynamics.ActiveReports.Line();
            this.Line10 = new DataDynamics.ActiveReports.Line();
            this.Shape6 = new DataDynamics.ActiveReports.Shape();
            this.Label14 = new DataDynamics.ActiveReports.Label();
            this.txtTotalLoadedDlrs = new DataDynamics.ActiveReports.TextBox();
            this.Line11 = new DataDynamics.ActiveReports.Line();
            this.Line12 = new DataDynamics.ActiveReports.Line();
            this.line5 = new DataDynamics.ActiveReports.Line();
            this.txtTotalHours = new DataDynamics.ActiveReports.TextBox();
            this.txtTotalLoadedRate = new DataDynamics.ActiveReports.TextBox();
            this.PageHeader = new DataDynamics.ActiveReports.PageHeader();
            this.Picture = new DataDynamics.ActiveReports.Picture();
            this.lblCustomerLocation = new DataDynamics.ActiveReports.Label();
            this.lblJobDescription = new DataDynamics.ActiveReports.Label();
            this.lblJobNumber = new DataDynamics.ActiveReports.Label();
            this.lblRevision = new DataDynamics.ActiveReports.Label();
            this.label15 = new DataDynamics.ActiveReports.Label();
            this.PageFooter = new DataDynamics.ActiveReports.PageFooter();
            this.lblDateTime = new DataDynamics.ActiveReports.Label();
            this.label8 = new DataDynamics.ActiveReports.Label();
            this.label9 = new DataDynamics.ActiveReports.Label();
            this.textBox22 = new DataDynamics.ActiveReports.TextBox();
            this.textBox23 = new DataDynamics.ActiveReports.TextBox();
            this.GroupHeader1 = new DataDynamics.ActiveReports.GroupHeader();
            this.Shape1 = new DataDynamics.ActiveReports.Shape();
            this.txtDepartment = new DataDynamics.ActiveReports.TextBox();
            this.Line = new DataDynamics.ActiveReports.Line();
            this.Label1 = new DataDynamics.ActiveReports.Label();
            this.Label2 = new DataDynamics.ActiveReports.Label();
            this.Label3 = new DataDynamics.ActiveReports.Label();
            this.Label4 = new DataDynamics.ActiveReports.Label();
            this.Label5 = new DataDynamics.ActiveReports.Label();
            this.Label6 = new DataDynamics.ActiveReports.Label();
            this.txtDiscipline = new DataDynamics.ActiveReports.TextBox();
            this.GroupFooter1 = new DataDynamics.ActiveReports.GroupFooter();
            this.Shape = new DataDynamics.ActiveReports.Shape();
            this.subRprtExpenses = new DataDynamics.ActiveReports.SubReport();
            this.TextBox15 = new DataDynamics.ActiveReports.TextBox();
            this.TextBox16 = new DataDynamics.ActiveReports.TextBox();
            this.TextBox17 = new DataDynamics.ActiveReports.TextBox();
            this.lblDeptTotals = new DataDynamics.ActiveReports.Label();
            this.GroupHeader2 = new DataDynamics.ActiveReports.GroupHeader();
            this.Shape2 = new DataDynamics.ActiveReports.Shape();
            this.TextBox7 = new DataDynamics.ActiveReports.TextBox();
            this.Line1 = new DataDynamics.ActiveReports.Line();
            this.TextBox11 = new DataDynamics.ActiveReports.TextBox();
            this.TextBox12 = new DataDynamics.ActiveReports.TextBox();
            this.TextBox19 = new DataDynamics.ActiveReports.TextBox();
            this.GroupFooter2 = new DataDynamics.ActiveReports.GroupFooter();
            this.GroupHeader3 = new DataDynamics.ActiveReports.GroupHeader();
            this.Shape3 = new DataDynamics.ActiveReports.Shape();
            this.TextBox9 = new DataDynamics.ActiveReports.TextBox();
            this.Line2 = new DataDynamics.ActiveReports.Line();
            this.TextBox8 = new DataDynamics.ActiveReports.TextBox();
            this.TextBox10 = new DataDynamics.ActiveReports.TextBox();
            this.TextBox20 = new DataDynamics.ActiveReports.TextBox();
            this.GroupFooter3 = new DataDynamics.ActiveReports.GroupFooter();
            this.GroupHeader4 = new DataDynamics.ActiveReports.GroupHeader();
            this.Shape4 = new DataDynamics.ActiveReports.Shape();
            this.TextBox13 = new DataDynamics.ActiveReports.TextBox();
            this.TextBox14 = new DataDynamics.ActiveReports.TextBox();
            this.TextBox18 = new DataDynamics.ActiveReports.TextBox();
            this.TextBox21 = new DataDynamics.ActiveReports.TextBox();
            this.GroupFooter4 = new DataDynamics.ActiveReports.GroupFooter();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox24)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEngTotalHrs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEngLdRt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEngTotLdDlrs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNonEngTotHrs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNonEngLdRt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNonEngTotLdDlrs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalLoadedDlrs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalHours)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalLoadedRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Picture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCustomerLocation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblJobDescription)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblJobNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblRevision)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label15)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDateTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox22)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox23)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDepartment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDiscipline)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox15)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox16)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox17)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDeptTotals)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox19)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox20)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox18)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox21)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.ColumnSpacing = 0F;
            this.Detail.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.TextBox,
            this.TextBox1,
            this.TextBox2,
            this.TextBox3,
            this.TextBox4,
            this.TextBox5,
            this.TextBox6,
            this.textBox24});
            this.Detail.Height = 0.175F;
            this.Detail.Name = "Detail";
            this.Detail.Format += new System.EventHandler(this.Detail_Format);
            // 
            // TextBox
            // 
            this.TextBox.Border.BottomColor = System.Drawing.Color.Black;
            this.TextBox.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.TextBox.Border.LeftColor = System.Drawing.Color.Black;
            this.TextBox.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.TextBox.Border.RightColor = System.Drawing.Color.Black;
            this.TextBox.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.TextBox.Border.TopColor = System.Drawing.Color.Black;
            this.TextBox.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.TextBox.DataField = "Activity";
            this.TextBox.Height = 0.175F;
            this.TextBox.Left = 0.75F;
            this.TextBox.Name = "TextBox";
            this.TextBox.Style = "font-size: 9pt; ";
            this.TextBox.Text = "TextBox";
            this.TextBox.Top = 0F;
            this.TextBox.Width = 0.5F;
            // 
            // TextBox1
            // 
            this.TextBox1.Border.BottomColor = System.Drawing.Color.Black;
            this.TextBox1.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.TextBox1.Border.LeftColor = System.Drawing.Color.Black;
            this.TextBox1.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.TextBox1.Border.RightColor = System.Drawing.Color.Black;
            this.TextBox1.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.TextBox1.Border.TopColor = System.Drawing.Color.Black;
            this.TextBox1.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.TextBox1.DataField = "Description";
            this.TextBox1.Height = 0.175F;
            this.TextBox1.Left = 1.53125F;
            this.TextBox1.Name = "TextBox1";
            this.TextBox1.Style = "font-size: 9pt; ";
            this.TextBox1.Text = "TextBox";
            this.TextBox1.Top = 0F;
            this.TextBox1.Width = 3.03125F;
            // 
            // TextBox2
            // 
            this.TextBox2.Border.BottomColor = System.Drawing.Color.Black;
            this.TextBox2.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.TextBox2.Border.LeftColor = System.Drawing.Color.Black;
            this.TextBox2.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.TextBox2.Border.RightColor = System.Drawing.Color.Black;
            this.TextBox2.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.TextBox2.Border.TopColor = System.Drawing.Color.Black;
            this.TextBox2.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.TextBox2.DataField = "Quantity";
            this.TextBox2.Height = 0.175F;
            this.TextBox2.Left = 4.5625F;
            this.TextBox2.Name = "TextBox2";
            this.TextBox2.OutputFormat = resources.GetString("TextBox2.OutputFormat");
            this.TextBox2.Style = "text-align: right; font-size: 9pt; ";
            this.TextBox2.Text = "TextBox";
            this.TextBox2.Top = 0F;
            this.TextBox2.Width = 0.4375F;
            // 
            // TextBox3
            // 
            this.TextBox3.Border.BottomColor = System.Drawing.Color.Black;
            this.TextBox3.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.TextBox3.Border.LeftColor = System.Drawing.Color.Black;
            this.TextBox3.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.TextBox3.Border.RightColor = System.Drawing.Color.Black;
            this.TextBox3.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.TextBox3.Border.TopColor = System.Drawing.Color.Black;
            this.TextBox3.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.TextBox3.DataField = "HoursEach";
            this.TextBox3.Height = 0.175F;
            this.TextBox3.Left = 5.0625F;
            this.TextBox3.Name = "TextBox3";
            this.TextBox3.OutputFormat = resources.GetString("TextBox3.OutputFormat");
            this.TextBox3.Style = "text-align: right; font-size: 9pt; ";
            this.TextBox3.Text = "TextBox";
            this.TextBox3.Top = 0F;
            this.TextBox3.Width = 0.4375F;
            // 
            // TextBox4
            // 
            this.TextBox4.Border.BottomColor = System.Drawing.Color.Black;
            this.TextBox4.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.TextBox4.Border.LeftColor = System.Drawing.Color.Black;
            this.TextBox4.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.TextBox4.Border.RightColor = System.Drawing.Color.Black;
            this.TextBox4.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.TextBox4.Border.TopColor = System.Drawing.Color.Black;
            this.TextBox4.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.TextBox4.DataField = "TotalHours";
            this.TextBox4.Height = 0.175F;
            this.TextBox4.Left = 5.5625F;
            this.TextBox4.Name = "TextBox4";
            this.TextBox4.OutputFormat = resources.GetString("TextBox4.OutputFormat");
            this.TextBox4.Style = "text-align: right; font-size: 9pt; ";
            this.TextBox4.Text = "TextBox";
            this.TextBox4.Top = 0F;
            this.TextBox4.Width = 0.4375F;
            // 
            // TextBox5
            // 
            this.TextBox5.Border.BottomColor = System.Drawing.Color.Black;
            this.TextBox5.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.TextBox5.Border.LeftColor = System.Drawing.Color.Black;
            this.TextBox5.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.TextBox5.Border.RightColor = System.Drawing.Color.Black;
            this.TextBox5.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.TextBox5.Border.TopColor = System.Drawing.Color.Black;
            this.TextBox5.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.TextBox5.DataField = "LoadedRate";
            this.TextBox5.Height = 0.175F;
            this.TextBox5.Left = 6.0625F;
            this.TextBox5.Name = "TextBox5";
            this.TextBox5.OutputFormat = resources.GetString("TextBox5.OutputFormat");
            this.TextBox5.Style = "text-align: right; font-size: 9pt; ";
            this.TextBox5.Text = "$000.00";
            this.TextBox5.Top = 0F;
            this.TextBox5.Width = 0.5F;
            // 
            // TextBox6
            // 
            this.TextBox6.Border.BottomColor = System.Drawing.Color.Black;
            this.TextBox6.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.TextBox6.Border.LeftColor = System.Drawing.Color.Black;
            this.TextBox6.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.TextBox6.Border.RightColor = System.Drawing.Color.Black;
            this.TextBox6.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.TextBox6.Border.TopColor = System.Drawing.Color.Black;
            this.TextBox6.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.TextBox6.DataField = "TotalDollars";
            this.TextBox6.Height = 0.175F;
            this.TextBox6.Left = 6.625F;
            this.TextBox6.Name = "TextBox6";
            this.TextBox6.OutputFormat = resources.GetString("TextBox6.OutputFormat");
            this.TextBox6.Style = "text-align: right; font-size: 9pt; ";
            this.TextBox6.Text = "TextBox";
            this.TextBox6.Top = 0F;
            this.TextBox6.Width = 0.875F;
            // 
            // textBox24
            // 
            this.textBox24.Border.BottomColor = System.Drawing.Color.Black;
            this.textBox24.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.textBox24.Border.LeftColor = System.Drawing.Color.Black;
            this.textBox24.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.textBox24.Border.RightColor = System.Drawing.Color.Black;
            this.textBox24.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.textBox24.Border.TopColor = System.Drawing.Color.Black;
            this.textBox24.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.textBox24.DataField = "WBS";
            this.textBox24.Height = 0.175F;
            this.textBox24.Left = 1.28125F;
            this.textBox24.Name = "textBox24";
            this.textBox24.Style = "font-size: 9pt; ";
            this.textBox24.Text = "00";
            this.textBox24.Top = 0F;
            this.textBox24.Width = 0.2083333F;
            // 
            // ReportHeader
            // 
            this.ReportHeader.Height = 0F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.Shape5,
            this.Label,
            this.Label7,
            this.Label10,
            this.Label11,
            this.Label13,
            this.txtEngTotalHrs,
            this.txtEngLdRt,
            this.txtEngTotLdDlrs,
            this.txtNonEngTotHrs,
            this.txtNonEngLdRt,
            this.txtNonEngTotLdDlrs,
            this.Line3,
            this.Line4,
            this.Line8,
            this.Line9,
            this.Line10,
            this.Shape6,
            this.Label14,
            this.txtTotalLoadedDlrs,
            this.Line11,
            this.Line12,
            this.line5,
            this.txtTotalHours,
            this.txtTotalLoadedRate});
            this.ReportFooter.Height = 1.197917F;
            this.ReportFooter.KeepTogether = true;
            this.ReportFooter.Name = "ReportFooter";
            this.ReportFooter.Format += new System.EventHandler(this.ReportFooter_Format);
            // 
            // Shape5
            // 
            this.Shape5.Border.BottomColor = System.Drawing.Color.Black;
            this.Shape5.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Shape5.Border.LeftColor = System.Drawing.Color.Black;
            this.Shape5.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Shape5.Border.RightColor = System.Drawing.Color.Black;
            this.Shape5.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Shape5.Border.TopColor = System.Drawing.Color.Black;
            this.Shape5.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Shape5.Height = 0.75F;
            this.Shape5.Left = 0F;
            this.Shape5.LineWeight = 3F;
            this.Shape5.Name = "Shape5";
            this.Shape5.RoundingRadius = 9.999999F;
            this.Shape5.Top = 0F;
            this.Shape5.Width = 7.5F;
            // 
            // Label
            // 
            this.Label.Border.BottomColor = System.Drawing.Color.Black;
            this.Label.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Label.Border.LeftColor = System.Drawing.Color.Black;
            this.Label.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Label.Border.RightColor = System.Drawing.Color.Black;
            this.Label.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Label.Border.TopColor = System.Drawing.Color.Black;
            this.Label.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Label.Height = 0.1875F;
            this.Label.HyperLink = null;
            this.Label.Left = 3.625F;
            this.Label.Name = "Label";
            this.Label.Style = "font-weight: bold; font-size: 9pt; ";
            this.Label.Text = "Total Engineering Above";
            this.Label.Top = 0.375F;
            this.Label.Width = 1.5625F;
            // 
            // Label7
            // 
            this.Label7.Border.BottomColor = System.Drawing.Color.Black;
            this.Label7.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Label7.Border.LeftColor = System.Drawing.Color.Black;
            this.Label7.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Label7.Border.RightColor = System.Drawing.Color.Black;
            this.Label7.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Label7.Border.TopColor = System.Drawing.Color.Black;
            this.Label7.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Label7.Height = 0.2F;
            this.Label7.HyperLink = null;
            this.Label7.Left = 3.625F;
            this.Label7.Name = "Label7";
            this.Label7.Style = "font-weight: bold; font-size: 9pt; ";
            this.Label7.Text = "Total Expenses";
            this.Label7.Top = 0.5625F;
            this.Label7.Width = 1.5625F;
            // 
            // Label10
            // 
            this.Label10.Border.BottomColor = System.Drawing.Color.Black;
            this.Label10.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Label10.Border.LeftColor = System.Drawing.Color.Black;
            this.Label10.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Label10.Border.RightColor = System.Drawing.Color.Black;
            this.Label10.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Label10.Border.TopColor = System.Drawing.Color.Black;
            this.Label10.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Label10.Height = 0.375F;
            this.Label10.HyperLink = null;
            this.Label10.Left = 5.25F;
            this.Label10.Name = "Label10";
            this.Label10.Style = "text-align: center; font-weight: bold; font-size: 9pt; vertical-align: bottom; ";
            this.Label10.Text = "Total Hours";
            this.Label10.Top = 0F;
            this.Label10.Width = 0.5625F;
            // 
            // Label11
            // 
            this.Label11.Border.BottomColor = System.Drawing.Color.Black;
            this.Label11.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Label11.Border.LeftColor = System.Drawing.Color.Black;
            this.Label11.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Label11.Border.RightColor = System.Drawing.Color.Black;
            this.Label11.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Label11.Border.TopColor = System.Drawing.Color.Black;
            this.Label11.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Label11.Height = 0.375F;
            this.Label11.HyperLink = null;
            this.Label11.Left = 6.0625F;
            this.Label11.Name = "Label11";
            this.Label11.Style = "text-align: center; font-weight: bold; font-size: 9pt; vertical-align: bottom; ";
            this.Label11.Text = "Loaded Rate";
            this.Label11.Top = 0F;
            this.Label11.Width = 0.5F;
            // 
            // Label13
            // 
            this.Label13.Border.BottomColor = System.Drawing.Color.Black;
            this.Label13.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Label13.Border.LeftColor = System.Drawing.Color.Black;
            this.Label13.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Label13.Border.RightColor = System.Drawing.Color.Black;
            this.Label13.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Label13.Border.TopColor = System.Drawing.Color.Black;
            this.Label13.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Label13.Height = 0.375F;
            this.Label13.HyperLink = null;
            this.Label13.Left = 6.66F;
            this.Label13.Name = "Label13";
            this.Label13.Style = "text-align: center; font-weight: bold; font-size: 9pt; vertical-align: bottom; ";
            this.Label13.Text = "Total Loaded Dollars";
            this.Label13.Top = 0F;
            this.Label13.Width = 0.8125F;
            // 
            // txtEngTotalHrs
            // 
            this.txtEngTotalHrs.Border.BottomColor = System.Drawing.Color.Black;
            this.txtEngTotalHrs.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.txtEngTotalHrs.Border.LeftColor = System.Drawing.Color.Black;
            this.txtEngTotalHrs.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.txtEngTotalHrs.Border.RightColor = System.Drawing.Color.Black;
            this.txtEngTotalHrs.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.txtEngTotalHrs.Border.TopColor = System.Drawing.Color.Black;
            this.txtEngTotalHrs.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.txtEngTotalHrs.DataField = "TotalHours";
            this.txtEngTotalHrs.Height = 0.2F;
            this.txtEngTotalHrs.Left = 5.25F;
            this.txtEngTotalHrs.Name = "txtEngTotalHrs";
            this.txtEngTotalHrs.OutputFormat = resources.GetString("txtEngTotalHrs.OutputFormat");
            this.txtEngTotalHrs.Style = "text-align: right; font-size: 9pt; ";
            this.txtEngTotalHrs.SummaryRunning = DataDynamics.ActiveReports.SummaryRunning.All;
            this.txtEngTotalHrs.SummaryType = DataDynamics.ActiveReports.SummaryType.GrandTotal;
            this.txtEngTotalHrs.Text = "TextBox22";
            this.txtEngTotalHrs.Top = 0.375F;
            this.txtEngTotalHrs.Width = 0.5625F;
            // 
            // txtEngLdRt
            // 
            this.txtEngLdRt.Border.BottomColor = System.Drawing.Color.Black;
            this.txtEngLdRt.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.txtEngLdRt.Border.LeftColor = System.Drawing.Color.Black;
            this.txtEngLdRt.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.txtEngLdRt.Border.RightColor = System.Drawing.Color.Black;
            this.txtEngLdRt.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.txtEngLdRt.Border.TopColor = System.Drawing.Color.Black;
            this.txtEngLdRt.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.txtEngLdRt.Height = 0.2F;
            this.txtEngLdRt.Left = 5.9375F;
            this.txtEngLdRt.Name = "txtEngLdRt";
            this.txtEngLdRt.OutputFormat = resources.GetString("txtEngLdRt.OutputFormat");
            this.txtEngLdRt.Style = "text-align: right; font-size: 9pt; ";
            this.txtEngLdRt.Text = "TextBox22";
            this.txtEngLdRt.Top = 0.375F;
            this.txtEngLdRt.Width = 0.5625F;
            // 
            // txtEngTotLdDlrs
            // 
            this.txtEngTotLdDlrs.Border.BottomColor = System.Drawing.Color.Black;
            this.txtEngTotLdDlrs.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.txtEngTotLdDlrs.Border.LeftColor = System.Drawing.Color.Black;
            this.txtEngTotLdDlrs.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.txtEngTotLdDlrs.Border.RightColor = System.Drawing.Color.Black;
            this.txtEngTotLdDlrs.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.txtEngTotLdDlrs.Border.TopColor = System.Drawing.Color.Black;
            this.txtEngTotLdDlrs.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.txtEngTotLdDlrs.DataField = "TotalDollars";
            this.txtEngTotLdDlrs.Height = 0.2F;
            this.txtEngTotLdDlrs.Left = 6.58F;
            this.txtEngTotLdDlrs.Name = "txtEngTotLdDlrs";
            this.txtEngTotLdDlrs.OutputFormat = resources.GetString("txtEngTotLdDlrs.OutputFormat");
            this.txtEngTotLdDlrs.Style = "text-align: right; font-size: 9pt; ";
            this.txtEngTotLdDlrs.SummaryRunning = DataDynamics.ActiveReports.SummaryRunning.All;
            this.txtEngTotLdDlrs.SummaryType = DataDynamics.ActiveReports.SummaryType.GrandTotal;
            this.txtEngTotLdDlrs.Text = "$0,000,000.00";
            this.txtEngTotLdDlrs.Top = 0.375F;
            this.txtEngTotLdDlrs.Width = 0.875F;
            // 
            // txtNonEngTotHrs
            // 
            this.txtNonEngTotHrs.Border.BottomColor = System.Drawing.Color.Black;
            this.txtNonEngTotHrs.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.txtNonEngTotHrs.Border.LeftColor = System.Drawing.Color.Black;
            this.txtNonEngTotHrs.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.txtNonEngTotHrs.Border.RightColor = System.Drawing.Color.Black;
            this.txtNonEngTotHrs.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.txtNonEngTotHrs.Border.TopColor = System.Drawing.Color.Black;
            this.txtNonEngTotHrs.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.txtNonEngTotHrs.Height = 0.2F;
            this.txtNonEngTotHrs.Left = 5.25F;
            this.txtNonEngTotHrs.Name = "txtNonEngTotHrs";
            this.txtNonEngTotHrs.OutputFormat = resources.GetString("txtNonEngTotHrs.OutputFormat");
            this.txtNonEngTotHrs.Style = "text-align: right; font-size: 9pt; ";
            this.txtNonEngTotHrs.Text = "TextBox22";
            this.txtNonEngTotHrs.Top = 0.5625F;
            this.txtNonEngTotHrs.Width = 0.5625F;
            // 
            // txtNonEngLdRt
            // 
            this.txtNonEngLdRt.Border.BottomColor = System.Drawing.Color.Black;
            this.txtNonEngLdRt.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.txtNonEngLdRt.Border.LeftColor = System.Drawing.Color.Black;
            this.txtNonEngLdRt.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.txtNonEngLdRt.Border.RightColor = System.Drawing.Color.Black;
            this.txtNonEngLdRt.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.txtNonEngLdRt.Border.TopColor = System.Drawing.Color.Black;
            this.txtNonEngLdRt.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.txtNonEngLdRt.Height = 0.2F;
            this.txtNonEngLdRt.Left = 5.9375F;
            this.txtNonEngLdRt.Name = "txtNonEngLdRt";
            this.txtNonEngLdRt.OutputFormat = resources.GetString("txtNonEngLdRt.OutputFormat");
            this.txtNonEngLdRt.Style = "text-align: right; font-size: 9pt; ";
            this.txtNonEngLdRt.Text = "TextBox22";
            this.txtNonEngLdRt.Top = 0.5625F;
            this.txtNonEngLdRt.Width = 0.5625F;
            // 
            // txtNonEngTotLdDlrs
            // 
            this.txtNonEngTotLdDlrs.Border.BottomColor = System.Drawing.Color.Black;
            this.txtNonEngTotLdDlrs.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.txtNonEngTotLdDlrs.Border.LeftColor = System.Drawing.Color.Black;
            this.txtNonEngTotLdDlrs.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.txtNonEngTotLdDlrs.Border.RightColor = System.Drawing.Color.Black;
            this.txtNonEngTotLdDlrs.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.txtNonEngTotLdDlrs.Border.TopColor = System.Drawing.Color.Black;
            this.txtNonEngTotLdDlrs.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.txtNonEngTotLdDlrs.Height = 0.2F;
            this.txtNonEngTotLdDlrs.Left = 6.58F;
            this.txtNonEngTotLdDlrs.Name = "txtNonEngTotLdDlrs";
            this.txtNonEngTotLdDlrs.OutputFormat = resources.GetString("txtNonEngTotLdDlrs.OutputFormat");
            this.txtNonEngTotLdDlrs.Style = "text-align: right; font-size: 9pt; ";
            this.txtNonEngTotLdDlrs.Text = "TextBox22";
            this.txtNonEngTotLdDlrs.Top = 0.5625F;
            this.txtNonEngTotLdDlrs.Width = 0.875F;
            // 
            // Line3
            // 
            this.Line3.Border.BottomColor = System.Drawing.Color.Black;
            this.Line3.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Line3.Border.LeftColor = System.Drawing.Color.Black;
            this.Line3.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Line3.Border.RightColor = System.Drawing.Color.Black;
            this.Line3.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Line3.Border.TopColor = System.Drawing.Color.Black;
            this.Line3.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Line3.Height = 0F;
            this.Line3.Left = 0F;
            this.Line3.LineWeight = 1F;
            this.Line3.Name = "Line3";
            this.Line3.Top = 0.375F;
            this.Line3.Width = 7.5F;
            this.Line3.X1 = 0F;
            this.Line3.X2 = 7.5F;
            this.Line3.Y1 = 0.375F;
            this.Line3.Y2 = 0.375F;
            // 
            // Line4
            // 
            this.Line4.Border.BottomColor = System.Drawing.Color.Black;
            this.Line4.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Line4.Border.LeftColor = System.Drawing.Color.Black;
            this.Line4.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Line4.Border.RightColor = System.Drawing.Color.Black;
            this.Line4.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Line4.Border.TopColor = System.Drawing.Color.Black;
            this.Line4.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Line4.Height = 0F;
            this.Line4.Left = 0F;
            this.Line4.LineWeight = 1F;
            this.Line4.Name = "Line4";
            this.Line4.Top = 0.5625F;
            this.Line4.Width = 7.5F;
            this.Line4.X1 = 0F;
            this.Line4.X2 = 7.5F;
            this.Line4.Y1 = 0.5625F;
            this.Line4.Y2 = 0.5625F;
            // 
            // Line8
            // 
            this.Line8.Border.BottomColor = System.Drawing.Color.Black;
            this.Line8.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Line8.Border.LeftColor = System.Drawing.Color.Black;
            this.Line8.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Line8.Border.RightColor = System.Drawing.Color.Black;
            this.Line8.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Line8.Border.TopColor = System.Drawing.Color.Black;
            this.Line8.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Line8.Height = 0.75F;
            this.Line8.Left = 5.1875F;
            this.Line8.LineWeight = 1F;
            this.Line8.Name = "Line8";
            this.Line8.Top = 0F;
            this.Line8.Width = 0F;
            this.Line8.X1 = 5.1875F;
            this.Line8.X2 = 5.1875F;
            this.Line8.Y1 = 0F;
            this.Line8.Y2 = 0.75F;
            // 
            // Line9
            // 
            this.Line9.Border.BottomColor = System.Drawing.Color.Black;
            this.Line9.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Line9.Border.LeftColor = System.Drawing.Color.Black;
            this.Line9.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Line9.Border.RightColor = System.Drawing.Color.Black;
            this.Line9.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Line9.Border.TopColor = System.Drawing.Color.Black;
            this.Line9.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Line9.Height = 0.75F;
            this.Line9.Left = 5.875F;
            this.Line9.LineWeight = 1F;
            this.Line9.Name = "Line9";
            this.Line9.Top = 0F;
            this.Line9.Width = 0F;
            this.Line9.X1 = 5.875F;
            this.Line9.X2 = 5.875F;
            this.Line9.Y1 = 0F;
            this.Line9.Y2 = 0.75F;
            // 
            // Line10
            // 
            this.Line10.Border.BottomColor = System.Drawing.Color.Black;
            this.Line10.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Line10.Border.LeftColor = System.Drawing.Color.Black;
            this.Line10.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Line10.Border.RightColor = System.Drawing.Color.Black;
            this.Line10.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Line10.Border.TopColor = System.Drawing.Color.Black;
            this.Line10.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Line10.Height = 0.75F;
            this.Line10.Left = 6.625F;
            this.Line10.LineWeight = 1F;
            this.Line10.Name = "Line10";
            this.Line10.Top = 0F;
            this.Line10.Width = 0F;
            this.Line10.X1 = 6.625F;
            this.Line10.X2 = 6.625F;
            this.Line10.Y1 = 0F;
            this.Line10.Y2 = 0.75F;
            // 
            // Shape6
            // 
            this.Shape6.Border.BottomColor = System.Drawing.Color.Black;
            this.Shape6.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Shape6.Border.LeftColor = System.Drawing.Color.Black;
            this.Shape6.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Shape6.Border.RightColor = System.Drawing.Color.Black;
            this.Shape6.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Shape6.Border.TopColor = System.Drawing.Color.Black;
            this.Shape6.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Shape6.Height = 0.3125F;
            this.Shape6.Left = 3.5625F;
            this.Shape6.LineWeight = 3F;
            this.Shape6.Name = "Shape6";
            this.Shape6.RoundingRadius = 9.999999F;
            this.Shape6.Top = 0.8125F;
            this.Shape6.Width = 3.9375F;
            // 
            // Label14
            // 
            this.Label14.Border.BottomColor = System.Drawing.Color.Black;
            this.Label14.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Label14.Border.LeftColor = System.Drawing.Color.Black;
            this.Label14.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Label14.Border.RightColor = System.Drawing.Color.Black;
            this.Label14.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Label14.Border.TopColor = System.Drawing.Color.Black;
            this.Label14.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Label14.Height = 0.2F;
            this.Label14.HyperLink = null;
            this.Label14.Left = 3.625F;
            this.Label14.Name = "Label14";
            this.Label14.Style = "font-weight: bold; font-size: 9pt; ";
            this.Label14.Text = "Grand Total";
            this.Label14.Top = 0.875F;
            this.Label14.Width = 0.8125F;
            // 
            // txtTotalLoadedDlrs
            // 
            this.txtTotalLoadedDlrs.Border.BottomColor = System.Drawing.Color.Black;
            this.txtTotalLoadedDlrs.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.txtTotalLoadedDlrs.Border.LeftColor = System.Drawing.Color.Black;
            this.txtTotalLoadedDlrs.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.txtTotalLoadedDlrs.Border.RightColor = System.Drawing.Color.Black;
            this.txtTotalLoadedDlrs.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.txtTotalLoadedDlrs.Border.TopColor = System.Drawing.Color.Black;
            this.txtTotalLoadedDlrs.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.txtTotalLoadedDlrs.Height = 0.2F;
            this.txtTotalLoadedDlrs.Left = 6.58F;
            this.txtTotalLoadedDlrs.Name = "txtTotalLoadedDlrs";
            this.txtTotalLoadedDlrs.OutputFormat = resources.GetString("txtTotalLoadedDlrs.OutputFormat");
            this.txtTotalLoadedDlrs.Style = "text-align: right; font-size: 9pt; ";
            this.txtTotalLoadedDlrs.Text = "TextBox23";
            this.txtTotalLoadedDlrs.Top = 0.875F;
            this.txtTotalLoadedDlrs.Width = 0.875F;
            // 
            // Line11
            // 
            this.Line11.Border.BottomColor = System.Drawing.Color.Black;
            this.Line11.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Line11.Border.LeftColor = System.Drawing.Color.Black;
            this.Line11.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Line11.Border.RightColor = System.Drawing.Color.Black;
            this.Line11.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Line11.Border.TopColor = System.Drawing.Color.Black;
            this.Line11.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Line11.Height = 0.3125F;
            this.Line11.Left = 5.1875F;
            this.Line11.LineWeight = 1F;
            this.Line11.Name = "Line11";
            this.Line11.Top = 0.8125F;
            this.Line11.Width = 0F;
            this.Line11.X1 = 5.1875F;
            this.Line11.X2 = 5.1875F;
            this.Line11.Y1 = 0.8125F;
            this.Line11.Y2 = 1.125F;
            // 
            // Line12
            // 
            this.Line12.Border.BottomColor = System.Drawing.Color.Black;
            this.Line12.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Line12.Border.LeftColor = System.Drawing.Color.Black;
            this.Line12.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Line12.Border.RightColor = System.Drawing.Color.Black;
            this.Line12.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Line12.Border.TopColor = System.Drawing.Color.Black;
            this.Line12.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Line12.Height = 0.3125F;
            this.Line12.Left = 6.625F;
            this.Line12.LineWeight = 1F;
            this.Line12.Name = "Line12";
            this.Line12.Top = 0.8125F;
            this.Line12.Width = 0F;
            this.Line12.X1 = 6.625F;
            this.Line12.X2 = 6.625F;
            this.Line12.Y1 = 0.8125F;
            this.Line12.Y2 = 1.125F;
            // 
            // line5
            // 
            this.line5.Border.BottomColor = System.Drawing.Color.Black;
            this.line5.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.line5.Border.LeftColor = System.Drawing.Color.Black;
            this.line5.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.line5.Border.RightColor = System.Drawing.Color.Black;
            this.line5.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.line5.Border.TopColor = System.Drawing.Color.Black;
            this.line5.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.line5.Height = 0.3125F;
            this.line5.Left = 5.875F;
            this.line5.LineWeight = 1F;
            this.line5.Name = "line5";
            this.line5.Top = 0.8125F;
            this.line5.Width = 0F;
            this.line5.X1 = 5.875F;
            this.line5.X2 = 5.875F;
            this.line5.Y1 = 0.8125F;
            this.line5.Y2 = 1.125F;
            // 
            // txtTotalHours
            // 
            this.txtTotalHours.Border.BottomColor = System.Drawing.Color.Black;
            this.txtTotalHours.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.txtTotalHours.Border.LeftColor = System.Drawing.Color.Black;
            this.txtTotalHours.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.txtTotalHours.Border.RightColor = System.Drawing.Color.Black;
            this.txtTotalHours.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.txtTotalHours.Border.TopColor = System.Drawing.Color.Black;
            this.txtTotalHours.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.txtTotalHours.Height = 0.1875F;
            this.txtTotalHours.Left = 5.1875F;
            this.txtTotalHours.Name = "txtTotalHours";
            this.txtTotalHours.OutputFormat = resources.GetString("txtTotalHours.OutputFormat");
            this.txtTotalHours.Style = "text-align: right; font-size: 9pt; ";
            this.txtTotalHours.Text = "textBox22";
            this.txtTotalHours.Top = 0.875F;
            this.txtTotalHours.Width = 0.625F;
            // 
            // txtTotalLoadedRate
            // 
            this.txtTotalLoadedRate.Border.BottomColor = System.Drawing.Color.Black;
            this.txtTotalLoadedRate.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.txtTotalLoadedRate.Border.LeftColor = System.Drawing.Color.Black;
            this.txtTotalLoadedRate.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.txtTotalLoadedRate.Border.RightColor = System.Drawing.Color.Black;
            this.txtTotalLoadedRate.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.txtTotalLoadedRate.Border.TopColor = System.Drawing.Color.Black;
            this.txtTotalLoadedRate.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.txtTotalLoadedRate.Height = 0.1875F;
            this.txtTotalLoadedRate.Left = 5.875F;
            this.txtTotalLoadedRate.Name = "txtTotalLoadedRate";
            this.txtTotalLoadedRate.OutputFormat = resources.GetString("txtTotalLoadedRate.OutputFormat");
            this.txtTotalLoadedRate.Style = "text-align: right; font-size: 9pt; ";
            this.txtTotalLoadedRate.Text = "textBox23";
            this.txtTotalLoadedRate.Top = 0.875F;
            this.txtTotalLoadedRate.Width = 0.625F;
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.Picture,
            this.lblCustomerLocation,
            this.lblJobDescription,
            this.lblJobNumber,
            this.lblRevision,
            this.label15});
            this.PageHeader.Height = 1.0625F;
            this.PageHeader.Name = "PageHeader";
            // 
            // Picture
            // 
            this.Picture.Border.BottomColor = System.Drawing.Color.Black;
            this.Picture.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Picture.Border.LeftColor = System.Drawing.Color.Black;
            this.Picture.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Picture.Border.RightColor = System.Drawing.Color.Black;
            this.Picture.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Picture.Border.TopColor = System.Drawing.Color.Black;
            this.Picture.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Picture.Height = 0.8F;
            this.Picture.Image = ((System.Drawing.Image)(resources.GetObject("Picture.Image")));
            this.Picture.ImageData = ((System.IO.Stream)(resources.GetObject("Picture.ImageData")));
            this.Picture.Left = 0F;
            this.Picture.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.Picture.LineWeight = 0F;
            this.Picture.Name = "Picture";
            this.Picture.SizeMode = DataDynamics.ActiveReports.SizeModes.Zoom;
            this.Picture.Top = 0F;
            this.Picture.Width = 0.8F;
            // 
            // lblCustomerLocation
            // 
            this.lblCustomerLocation.Border.BottomColor = System.Drawing.Color.Black;
            this.lblCustomerLocation.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.lblCustomerLocation.Border.LeftColor = System.Drawing.Color.Black;
            this.lblCustomerLocation.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.lblCustomerLocation.Border.RightColor = System.Drawing.Color.Black;
            this.lblCustomerLocation.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.lblCustomerLocation.Border.TopColor = System.Drawing.Color.Black;
            this.lblCustomerLocation.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.lblCustomerLocation.Height = 0.25F;
            this.lblCustomerLocation.HyperLink = null;
            this.lblCustomerLocation.Left = 1.1875F;
            this.lblCustomerLocation.Name = "lblCustomerLocation";
            this.lblCustomerLocation.Style = "ddo-char-set: 1; text-align: center; font-weight: bold; font-size: 10pt; ";
            this.lblCustomerLocation.Text = "Customer/Location";
            this.lblCustomerLocation.Top = 0.25F;
            this.lblCustomerLocation.Width = 5F;
            // 
            // lblJobDescription
            // 
            this.lblJobDescription.Border.BottomColor = System.Drawing.Color.Black;
            this.lblJobDescription.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.lblJobDescription.Border.LeftColor = System.Drawing.Color.Black;
            this.lblJobDescription.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.lblJobDescription.Border.RightColor = System.Drawing.Color.Black;
            this.lblJobDescription.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.lblJobDescription.Border.TopColor = System.Drawing.Color.Black;
            this.lblJobDescription.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.lblJobDescription.Height = 0.25F;
            this.lblJobDescription.HyperLink = null;
            this.lblJobDescription.Left = 1.1875F;
            this.lblJobDescription.Name = "lblJobDescription";
            this.lblJobDescription.Style = "ddo-char-set: 1; text-align: center; font-weight: bold; font-size: 10pt; ";
            this.lblJobDescription.Text = "JobDescription";
            this.lblJobDescription.Top = 0.4375F;
            this.lblJobDescription.Width = 5F;
            // 
            // lblJobNumber
            // 
            this.lblJobNumber.Border.BottomColor = System.Drawing.Color.Black;
            this.lblJobNumber.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.lblJobNumber.Border.LeftColor = System.Drawing.Color.Black;
            this.lblJobNumber.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.lblJobNumber.Border.RightColor = System.Drawing.Color.Black;
            this.lblJobNumber.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.lblJobNumber.Border.TopColor = System.Drawing.Color.Black;
            this.lblJobNumber.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.lblJobNumber.Height = 0.25F;
            this.lblJobNumber.HyperLink = null;
            this.lblJobNumber.Left = 1.1875F;
            this.lblJobNumber.Name = "lblJobNumber";
            this.lblJobNumber.Style = "ddo-char-set: 1; text-align: center; font-weight: bold; font-size: 10pt; ";
            this.lblJobNumber.Text = "JobNumber";
            this.lblJobNumber.Top = 0.625F;
            this.lblJobNumber.Width = 5F;
            // 
            // lblRevision
            // 
            this.lblRevision.Border.BottomColor = System.Drawing.Color.Black;
            this.lblRevision.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.lblRevision.Border.LeftColor = System.Drawing.Color.Black;
            this.lblRevision.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.lblRevision.Border.RightColor = System.Drawing.Color.Black;
            this.lblRevision.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.lblRevision.Border.TopColor = System.Drawing.Color.Black;
            this.lblRevision.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.lblRevision.Height = 0.25F;
            this.lblRevision.HyperLink = null;
            this.lblRevision.Left = 1.1875F;
            this.lblRevision.Name = "lblRevision";
            this.lblRevision.Style = "ddo-char-set: 1; text-align: center; font-weight: bold; font-size: 10pt; ";
            this.lblRevision.Text = "Revision";
            this.lblRevision.Top = 0.8125F;
            this.lblRevision.Width = 5F;
            // 
            // label15
            // 
            this.label15.Border.BottomColor = System.Drawing.Color.Black;
            this.label15.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.label15.Border.LeftColor = System.Drawing.Color.Black;
            this.label15.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.label15.Border.RightColor = System.Drawing.Color.Black;
            this.label15.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.label15.Border.TopColor = System.Drawing.Color.Black;
            this.label15.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.label15.Height = 0.25F;
            this.label15.HyperLink = null;
            this.label15.Left = 2.3125F;
            this.label15.Name = "label15";
            this.label15.Style = "text-align: center; font-weight: bold; font-size: 12pt; ";
            this.label15.Text = "Engineering Estimate Details";
            this.label15.Top = 0F;
            this.label15.Width = 2.75F;
            // 
            // PageFooter
            // 
            this.PageFooter.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.lblDateTime,
            this.label8,
            this.label9,
            this.textBox22,
            this.textBox23});
            this.PageFooter.Height = 0.25F;
            this.PageFooter.Name = "PageFooter";
            // 
            // lblDateTime
            // 
            this.lblDateTime.Border.BottomColor = System.Drawing.Color.Black;
            this.lblDateTime.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.lblDateTime.Border.LeftColor = System.Drawing.Color.Black;
            this.lblDateTime.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.lblDateTime.Border.RightColor = System.Drawing.Color.Black;
            this.lblDateTime.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.lblDateTime.Border.TopColor = System.Drawing.Color.Black;
            this.lblDateTime.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.lblDateTime.Height = 0.2F;
            this.lblDateTime.HyperLink = null;
            this.lblDateTime.Left = 4.25F;
            this.lblDateTime.Name = "lblDateTime";
            this.lblDateTime.Style = "ddo-char-set: 1; text-align: right; font-size: 8pt; ";
            this.lblDateTime.Text = "Label";
            this.lblDateTime.Top = 0.0625F;
            this.lblDateTime.Width = 3.1875F;
            // 
            // label8
            // 
            this.label8.Border.BottomColor = System.Drawing.Color.Black;
            this.label8.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.label8.Border.LeftColor = System.Drawing.Color.Black;
            this.label8.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.label8.Border.RightColor = System.Drawing.Color.Black;
            this.label8.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.label8.Border.TopColor = System.Drawing.Color.Black;
            this.label8.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.label8.Height = 0.2F;
            this.label8.HyperLink = null;
            this.label8.Left = 0F;
            this.label8.Name = "label8";
            this.label8.Style = "text-align: right; font-size: 8.25pt; ";
            this.label8.Text = "Page";
            this.label8.Top = 0.0625F;
            this.label8.Width = 0.4375F;
            // 
            // label9
            // 
            this.label9.Border.BottomColor = System.Drawing.Color.Black;
            this.label9.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.label9.Border.LeftColor = System.Drawing.Color.Black;
            this.label9.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.label9.Border.RightColor = System.Drawing.Color.Black;
            this.label9.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.label9.Border.TopColor = System.Drawing.Color.Black;
            this.label9.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.label9.Height = 0.2F;
            this.label9.HyperLink = null;
            this.label9.Left = 0.6875F;
            this.label9.Name = "label9";
            this.label9.Style = "text-align: center; font-size: 8.25pt; ";
            this.label9.Text = "of";
            this.label9.Top = 0.0625F;
            this.label9.Width = 0.1875F;
            // 
            // textBox22
            // 
            this.textBox22.Border.BottomColor = System.Drawing.Color.Black;
            this.textBox22.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.textBox22.Border.LeftColor = System.Drawing.Color.Black;
            this.textBox22.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.textBox22.Border.RightColor = System.Drawing.Color.Black;
            this.textBox22.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.textBox22.Border.TopColor = System.Drawing.Color.Black;
            this.textBox22.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.textBox22.Height = 0.2F;
            this.textBox22.Left = 0.4375F;
            this.textBox22.Name = "textBox22";
            this.textBox22.Style = "text-align: center; font-size: 8.25pt; ";
            this.textBox22.SummaryFunc = DataDynamics.ActiveReports.SummaryFunc.Count;
            this.textBox22.SummaryRunning = DataDynamics.ActiveReports.SummaryRunning.All;
            this.textBox22.SummaryType = DataDynamics.ActiveReports.SummaryType.PageCount;
            this.textBox22.Text = "TextBox8";
            this.textBox22.Top = 0.0625F;
            this.textBox22.Width = 0.25F;
            // 
            // textBox23
            // 
            this.textBox23.Border.BottomColor = System.Drawing.Color.Black;
            this.textBox23.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.textBox23.Border.LeftColor = System.Drawing.Color.Black;
            this.textBox23.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.textBox23.Border.RightColor = System.Drawing.Color.Black;
            this.textBox23.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.textBox23.Border.TopColor = System.Drawing.Color.Black;
            this.textBox23.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.textBox23.Height = 0.2F;
            this.textBox23.Left = 0.875F;
            this.textBox23.Name = "textBox23";
            this.textBox23.Style = "font-size: 8.25pt; ";
            this.textBox23.SummaryFunc = DataDynamics.ActiveReports.SummaryFunc.Count;
            this.textBox23.SummaryType = DataDynamics.ActiveReports.SummaryType.PageCount;
            this.textBox23.Text = "TextBox9";
            this.textBox23.Top = 0.0625F;
            this.textBox23.Width = 0.25F;
            // 
            // GroupHeader1
            // 
            this.GroupHeader1.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.Shape1,
            this.txtDepartment,
            this.Line,
            this.Label1,
            this.Label2,
            this.Label3,
            this.Label4,
            this.Label5,
            this.Label6,
            this.txtDiscipline});
            this.GroupHeader1.DataField = "DeptGroup";
            this.GroupHeader1.Height = 0.3958333F;
            this.GroupHeader1.Name = "GroupHeader1";
            this.GroupHeader1.Format += new System.EventHandler(this.GroupHeader1_Format);
            // 
            // Shape1
            // 
            this.Shape1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.Shape1.Border.BottomColor = System.Drawing.Color.Black;
            this.Shape1.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Shape1.Border.LeftColor = System.Drawing.Color.Black;
            this.Shape1.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Shape1.Border.RightColor = System.Drawing.Color.Black;
            this.Shape1.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Shape1.Border.TopColor = System.Drawing.Color.Black;
            this.Shape1.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Shape1.Height = 0.375F;
            this.Shape1.Left = 0F;
            this.Shape1.Name = "Shape1";
            this.Shape1.RoundingRadius = 9.999999F;
            this.Shape1.Top = 0F;
            this.Shape1.Width = 7.5F;
            // 
            // txtDepartment
            // 
            this.txtDepartment.Border.BottomColor = System.Drawing.Color.Black;
            this.txtDepartment.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.txtDepartment.Border.LeftColor = System.Drawing.Color.Black;
            this.txtDepartment.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.txtDepartment.Border.RightColor = System.Drawing.Color.Black;
            this.txtDepartment.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.txtDepartment.Border.TopColor = System.Drawing.Color.Black;
            this.txtDepartment.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.txtDepartment.DataField = "DeptGroup";
            this.txtDepartment.Height = 0.2F;
            this.txtDepartment.Left = 0.05F;
            this.txtDepartment.Name = "txtDepartment";
            this.txtDepartment.Style = "ddo-char-set: 1; font-weight: bold; font-size: 9pt; ";
            this.txtDepartment.Text = "DeptGroup";
            this.txtDepartment.Top = 0.1875F;
            this.txtDepartment.Width = 0.875F;
            // 
            // Line
            // 
            this.Line.Border.BottomColor = System.Drawing.Color.Black;
            this.Line.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Line.Border.LeftColor = System.Drawing.Color.Black;
            this.Line.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Line.Border.RightColor = System.Drawing.Color.Black;
            this.Line.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Line.Border.TopColor = System.Drawing.Color.Black;
            this.Line.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Line.Height = 0F;
            this.Line.Left = 0F;
            this.Line.LineWeight = 3F;
            this.Line.Name = "Line";
            this.Line.Top = 0.375F;
            this.Line.Width = 7.5F;
            this.Line.X1 = 0F;
            this.Line.X2 = 7.5F;
            this.Line.Y1 = 0.375F;
            this.Line.Y2 = 0.375F;
            // 
            // Label1
            // 
            this.Label1.Border.BottomColor = System.Drawing.Color.Black;
            this.Label1.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Label1.Border.LeftColor = System.Drawing.Color.Black;
            this.Label1.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Label1.Border.RightColor = System.Drawing.Color.Black;
            this.Label1.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Label1.Border.TopColor = System.Drawing.Color.Black;
            this.Label1.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Label1.Height = 0.2F;
            this.Label1.HyperLink = null;
            this.Label1.Left = 4.4375F;
            this.Label1.Name = "Label1";
            this.Label1.Style = "ddo-char-set: 1; text-align: center; font-weight: bold; font-size: 8pt; ";
            this.Label1.Text = "Quantity";
            this.Label1.Top = 0.1574999F;
            this.Label1.Width = 0.625F;
            // 
            // Label2
            // 
            this.Label2.Border.BottomColor = System.Drawing.Color.Black;
            this.Label2.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Label2.Border.LeftColor = System.Drawing.Color.Black;
            this.Label2.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Label2.Border.RightColor = System.Drawing.Color.Black;
            this.Label2.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Label2.Border.TopColor = System.Drawing.Color.Black;
            this.Label2.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Label2.Height = 0.3374999F;
            this.Label2.HyperLink = null;
            this.Label2.Left = 5.0625F;
            this.Label2.Name = "Label2";
            this.Label2.Style = "ddo-char-set: 1; font-weight: bold; font-size: 8pt; ";
            this.Label2.Text = "Hours Wk/Ea";
            this.Label2.Top = 0.0200001F;
            this.Label2.Width = 0.4375F;
            // 
            // Label3
            // 
            this.Label3.Border.BottomColor = System.Drawing.Color.Black;
            this.Label3.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Label3.Border.LeftColor = System.Drawing.Color.Black;
            this.Label3.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Label3.Border.RightColor = System.Drawing.Color.Black;
            this.Label3.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Label3.Border.TopColor = System.Drawing.Color.Black;
            this.Label3.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Label3.Height = 0.3374999F;
            this.Label3.HyperLink = null;
            this.Label3.Left = 5.5625F;
            this.Label3.Name = "Label3";
            this.Label3.Style = "ddo-char-set: 1; text-align: center; font-weight: bold; font-size: 8pt; ";
            this.Label3.Text = "Total Hours";
            this.Label3.Top = 0.0200001F;
            this.Label3.Width = 0.4375F;
            // 
            // Label4
            // 
            this.Label4.Border.BottomColor = System.Drawing.Color.Black;
            this.Label4.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Label4.Border.LeftColor = System.Drawing.Color.Black;
            this.Label4.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Label4.Border.RightColor = System.Drawing.Color.Black;
            this.Label4.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Label4.Border.TopColor = System.Drawing.Color.Black;
            this.Label4.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Label4.Height = 0.3374999F;
            this.Label4.HyperLink = null;
            this.Label4.Left = 6.0625F;
            this.Label4.Name = "Label4";
            this.Label4.Style = "ddo-char-set: 1; text-align: center; font-weight: bold; font-size: 8pt; ";
            this.Label4.Text = "Loaded Rate";
            this.Label4.Top = 0.02000015F;
            this.Label4.Width = 0.5F;
            // 
            // Label5
            // 
            this.Label5.Border.BottomColor = System.Drawing.Color.Black;
            this.Label5.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Label5.Border.LeftColor = System.Drawing.Color.Black;
            this.Label5.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Label5.Border.RightColor = System.Drawing.Color.Black;
            this.Label5.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Label5.Border.TopColor = System.Drawing.Color.Black;
            this.Label5.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Label5.Height = 0.17F;
            this.Label5.HyperLink = null;
            this.Label5.Left = 6.625F;
            this.Label5.Name = "Label5";
            this.Label5.Style = "ddo-char-set: 1; text-align: center; font-weight: bold; font-size: 8pt; ";
            this.Label5.Text = " Total";
            this.Label5.Top = 0F;
            this.Label5.Width = 0.875F;
            // 
            // Label6
            // 
            this.Label6.Border.BottomColor = System.Drawing.Color.Black;
            this.Label6.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Label6.Border.LeftColor = System.Drawing.Color.Black;
            this.Label6.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Label6.Border.RightColor = System.Drawing.Color.Black;
            this.Label6.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Label6.Border.TopColor = System.Drawing.Color.Black;
            this.Label6.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Label6.Height = 0.17F;
            this.Label6.HyperLink = null;
            this.Label6.Left = 6.625F;
            this.Label6.Name = "Label6";
            this.Label6.Style = "ddo-char-set: 1; font-weight: bold; font-size: 8pt; ";
            this.Label6.Text = "Loaded Dollars";
            this.Label6.Top = 0.1875F;
            this.Label6.Width = 0.875F;
            // 
            // txtDiscipline
            // 
            this.txtDiscipline.Border.BottomColor = System.Drawing.Color.Black;
            this.txtDiscipline.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.txtDiscipline.Border.LeftColor = System.Drawing.Color.Black;
            this.txtDiscipline.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.txtDiscipline.Border.RightColor = System.Drawing.Color.Black;
            this.txtDiscipline.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.txtDiscipline.Border.TopColor = System.Drawing.Color.Black;
            this.txtDiscipline.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.txtDiscipline.DataField = "Discipline";
            this.txtDiscipline.Height = 0.2F;
            this.txtDiscipline.Left = 1F;
            this.txtDiscipline.Name = "txtDiscipline";
            this.txtDiscipline.Style = "ddo-char-set: 1; font-weight: bold; font-size: 9pt; ";
            this.txtDiscipline.Text = "Discipline";
            this.txtDiscipline.Top = 0.1875F;
            this.txtDiscipline.Width = 3.3125F;
            // 
            // GroupFooter1
            // 
            this.GroupFooter1.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.Shape,
            this.subRprtExpenses,
            this.TextBox15,
            this.TextBox16,
            this.TextBox17,
            this.lblDeptTotals});
            this.GroupFooter1.Height = 0.75F;
            this.GroupFooter1.Name = "GroupFooter1";
            this.GroupFooter1.Format += new System.EventHandler(this.GroupFooter1_Format);
            // 
            // Shape
            // 
            this.Shape.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.Shape.Border.BottomColor = System.Drawing.Color.Black;
            this.Shape.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Shape.Border.LeftColor = System.Drawing.Color.Black;
            this.Shape.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Shape.Border.RightColor = System.Drawing.Color.Black;
            this.Shape.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Shape.Border.TopColor = System.Drawing.Color.Black;
            this.Shape.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Shape.Height = 0.1875F;
            this.Shape.Left = 0F;
            this.Shape.Name = "Shape";
            this.Shape.RoundingRadius = 9.999999F;
            this.Shape.Top = 0.0625F;
            this.Shape.Width = 7.5F;
            // 
            // subRprtExpenses
            // 
            this.subRprtExpenses.Border.BottomColor = System.Drawing.Color.Black;
            this.subRprtExpenses.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.subRprtExpenses.Border.LeftColor = System.Drawing.Color.Black;
            this.subRprtExpenses.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.subRprtExpenses.Border.RightColor = System.Drawing.Color.Black;
            this.subRprtExpenses.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.subRprtExpenses.Border.TopColor = System.Drawing.Color.Black;
            this.subRprtExpenses.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.subRprtExpenses.CloseBorder = false;
            this.subRprtExpenses.Height = 0.25F;
            this.subRprtExpenses.Left = 0.4375F;
            this.subRprtExpenses.Name = "subRprtExpenses";
            this.subRprtExpenses.Report = null;
            this.subRprtExpenses.Top = 0.375F;
            this.subRprtExpenses.Width = 7F;
            // 
            // TextBox15
            // 
            this.TextBox15.Border.BottomColor = System.Drawing.Color.Black;
            this.TextBox15.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.TextBox15.Border.LeftColor = System.Drawing.Color.Black;
            this.TextBox15.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.TextBox15.Border.RightColor = System.Drawing.Color.Black;
            this.TextBox15.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.TextBox15.Border.TopColor = System.Drawing.Color.Black;
            this.TextBox15.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.TextBox15.DataField = "DeptGroup";
            this.TextBox15.Height = 0.2F;
            this.TextBox15.Left = 0F;
            this.TextBox15.Name = "TextBox15";
            this.TextBox15.Style = "ddo-char-set: 1; font-weight: bold; font-size: 9pt; ";
            this.TextBox15.Text = "DeptGroup";
            this.TextBox15.Top = 0.0625F;
            this.TextBox15.Width = 0.875F;
            // 
            // TextBox16
            // 
            this.TextBox16.Border.BottomColor = System.Drawing.Color.Black;
            this.TextBox16.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.TextBox16.Border.LeftColor = System.Drawing.Color.Black;
            this.TextBox16.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.TextBox16.Border.RightColor = System.Drawing.Color.Black;
            this.TextBox16.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.TextBox16.Border.TopColor = System.Drawing.Color.Black;
            this.TextBox16.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.TextBox16.DataField = "TotalHours";
            this.TextBox16.Height = 0.175F;
            this.TextBox16.Left = 5.5625F;
            this.TextBox16.Name = "TextBox16";
            this.TextBox16.OutputFormat = resources.GetString("TextBox16.OutputFormat");
            this.TextBox16.Style = "text-align: right; font-size: 9pt; ";
            this.TextBox16.SummaryGroup = "GroupHeader1";
            this.TextBox16.SummaryRunning = DataDynamics.ActiveReports.SummaryRunning.Group;
            this.TextBox16.SummaryType = DataDynamics.ActiveReports.SummaryType.SubTotal;
            this.TextBox16.Text = "TextBox";
            this.TextBox16.Top = 0.0625F;
            this.TextBox16.Width = 0.4375F;
            // 
            // TextBox17
            // 
            this.TextBox17.Border.BottomColor = System.Drawing.Color.Black;
            this.TextBox17.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.TextBox17.Border.LeftColor = System.Drawing.Color.Black;
            this.TextBox17.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.TextBox17.Border.RightColor = System.Drawing.Color.Black;
            this.TextBox17.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.TextBox17.Border.TopColor = System.Drawing.Color.Black;
            this.TextBox17.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.TextBox17.DataField = "TotalDollars";
            this.TextBox17.Height = 0.175F;
            this.TextBox17.Left = 6.625F;
            this.TextBox17.Name = "TextBox17";
            this.TextBox17.OutputFormat = resources.GetString("TextBox17.OutputFormat");
            this.TextBox17.Style = "text-align: right; font-size: 9pt; ";
            this.TextBox17.SummaryGroup = "GroupHeader1";
            this.TextBox17.SummaryRunning = DataDynamics.ActiveReports.SummaryRunning.Group;
            this.TextBox17.SummaryType = DataDynamics.ActiveReports.SummaryType.SubTotal;
            this.TextBox17.Text = "TextBox";
            this.TextBox17.Top = 0.0625F;
            this.TextBox17.Width = 0.875F;
            // 
            // lblDeptTotals
            // 
            this.lblDeptTotals.Border.BottomColor = System.Drawing.Color.Black;
            this.lblDeptTotals.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.lblDeptTotals.Border.LeftColor = System.Drawing.Color.Black;
            this.lblDeptTotals.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.lblDeptTotals.Border.RightColor = System.Drawing.Color.Black;
            this.lblDeptTotals.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.lblDeptTotals.Border.TopColor = System.Drawing.Color.Black;
            this.lblDeptTotals.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.lblDeptTotals.Height = 0.2F;
            this.lblDeptTotals.HyperLink = null;
            this.lblDeptTotals.Left = 2.4375F;
            this.lblDeptTotals.Name = "lblDeptTotals";
            this.lblDeptTotals.Style = "text-align: right; font-weight: bold; font-size: 9pt; ";
            this.lblDeptTotals.Text = "Total XXXX";
            this.lblDeptTotals.Top = 0.0625F;
            this.lblDeptTotals.Width = 2.875F;
            // 
            // GroupHeader2
            // 
            this.GroupHeader2.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.Shape2,
            this.TextBox7,
            this.Line1,
            this.TextBox11,
            this.TextBox12,
            this.TextBox19});
            this.GroupHeader2.DataField = "Task";
            this.GroupHeader2.Height = 0.2076389F;
            this.GroupHeader2.KeepTogether = true;
            this.GroupHeader2.Name = "GroupHeader2";
            // 
            // Shape2
            // 
            this.Shape2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.Shape2.Border.BottomColor = System.Drawing.Color.Black;
            this.Shape2.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Shape2.Border.LeftColor = System.Drawing.Color.Black;
            this.Shape2.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Shape2.Border.RightColor = System.Drawing.Color.Black;
            this.Shape2.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Shape2.Border.TopColor = System.Drawing.Color.Black;
            this.Shape2.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Shape2.Height = 0.1875F;
            this.Shape2.Left = 0.1875F;
            this.Shape2.Name = "Shape2";
            this.Shape2.RoundingRadius = 9.999999F;
            this.Shape2.Top = 0F;
            this.Shape2.Width = 7.3125F;
            // 
            // TextBox7
            // 
            this.TextBox7.Border.BottomColor = System.Drawing.Color.Black;
            this.TextBox7.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.TextBox7.Border.LeftColor = System.Drawing.Color.Black;
            this.TextBox7.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.TextBox7.Border.RightColor = System.Drawing.Color.Black;
            this.TextBox7.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.TextBox7.Border.TopColor = System.Drawing.Color.Black;
            this.TextBox7.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.TextBox7.DataField = "Task";
            this.TextBox7.Height = 0.2F;
            this.TextBox7.Left = 0.25F;
            this.TextBox7.Name = "TextBox7";
            this.TextBox7.Style = "font-size: 9pt; ";
            this.TextBox7.Text = "Task";
            this.TextBox7.Top = 0F;
            this.TextBox7.Width = 1F;
            // 
            // Line1
            // 
            this.Line1.Border.BottomColor = System.Drawing.Color.Black;
            this.Line1.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Line1.Border.LeftColor = System.Drawing.Color.Black;
            this.Line1.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Line1.Border.RightColor = System.Drawing.Color.Black;
            this.Line1.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Line1.Border.TopColor = System.Drawing.Color.Black;
            this.Line1.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Line1.Height = 0F;
            this.Line1.Left = 0.1875F;
            this.Line1.LineWeight = 1F;
            this.Line1.Name = "Line1";
            this.Line1.Top = 0.1875F;
            this.Line1.Width = 7.3125F;
            this.Line1.X1 = 0.1875F;
            this.Line1.X2 = 7.5F;
            this.Line1.Y1 = 0.1875F;
            this.Line1.Y2 = 0.1875F;
            // 
            // TextBox11
            // 
            this.TextBox11.Border.BottomColor = System.Drawing.Color.Black;
            this.TextBox11.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.TextBox11.Border.LeftColor = System.Drawing.Color.Black;
            this.TextBox11.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.TextBox11.Border.RightColor = System.Drawing.Color.Black;
            this.TextBox11.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.TextBox11.Border.TopColor = System.Drawing.Color.Black;
            this.TextBox11.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.TextBox11.DataField = "TotalHours";
            this.TextBox11.Height = 0.175F;
            this.TextBox11.Left = 5.5625F;
            this.TextBox11.Name = "TextBox11";
            this.TextBox11.OutputFormat = resources.GetString("TextBox11.OutputFormat");
            this.TextBox11.Style = "text-align: right; font-size: 9pt; ";
            this.TextBox11.SummaryGroup = "GroupHeader2";
            this.TextBox11.SummaryType = DataDynamics.ActiveReports.SummaryType.SubTotal;
            this.TextBox11.Text = "TextBox";
            this.TextBox11.Top = 0F;
            this.TextBox11.Width = 0.4375F;
            // 
            // TextBox12
            // 
            this.TextBox12.Border.BottomColor = System.Drawing.Color.Black;
            this.TextBox12.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.TextBox12.Border.LeftColor = System.Drawing.Color.Black;
            this.TextBox12.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.TextBox12.Border.RightColor = System.Drawing.Color.Black;
            this.TextBox12.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.TextBox12.Border.TopColor = System.Drawing.Color.Black;
            this.TextBox12.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.TextBox12.DataField = "TotalDollars";
            this.TextBox12.Height = 0.175F;
            this.TextBox12.Left = 6.6F;
            this.TextBox12.Name = "TextBox12";
            this.TextBox12.OutputFormat = resources.GetString("TextBox12.OutputFormat");
            this.TextBox12.Style = "text-align: right; font-size: 9pt; ";
            this.TextBox12.SummaryGroup = "GroupHeader2";
            this.TextBox12.SummaryType = DataDynamics.ActiveReports.SummaryType.SubTotal;
            this.TextBox12.Text = "TextBox";
            this.TextBox12.Top = 0F;
            this.TextBox12.Width = 0.875F;
            // 
            // TextBox19
            // 
            this.TextBox19.Border.BottomColor = System.Drawing.Color.Black;
            this.TextBox19.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.TextBox19.Border.LeftColor = System.Drawing.Color.Black;
            this.TextBox19.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.TextBox19.Border.RightColor = System.Drawing.Color.Black;
            this.TextBox19.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.TextBox19.Border.TopColor = System.Drawing.Color.Black;
            this.TextBox19.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.TextBox19.DataField = "TaskDescription";
            this.TextBox19.Height = 0.2F;
            this.TextBox19.Left = 1.270833F;
            this.TextBox19.Name = "TextBox19";
            this.TextBox19.Style = "font-size: 9pt; ";
            this.TextBox19.Text = "TextBox19";
            this.TextBox19.Top = 0F;
            this.TextBox19.Width = 3.0625F;
            // 
            // GroupFooter2
            // 
            this.GroupFooter2.Height = 0F;
            this.GroupFooter2.Name = "GroupFooter2";
            // 
            // GroupHeader3
            // 
            this.GroupHeader3.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.Shape3,
            this.TextBox9,
            this.Line2,
            this.TextBox8,
            this.TextBox10,
            this.TextBox20});
            this.GroupHeader3.DataField = "Category";
            this.GroupHeader3.Height = 0.2076389F;
            this.GroupHeader3.KeepTogether = true;
            this.GroupHeader3.Name = "GroupHeader3";
            // 
            // Shape3
            // 
            this.Shape3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.Shape3.Border.BottomColor = System.Drawing.Color.Black;
            this.Shape3.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Shape3.Border.LeftColor = System.Drawing.Color.Black;
            this.Shape3.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Shape3.Border.RightColor = System.Drawing.Color.Black;
            this.Shape3.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Shape3.Border.TopColor = System.Drawing.Color.Black;
            this.Shape3.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Shape3.Height = 0.1875F;
            this.Shape3.Left = 0.4375F;
            this.Shape3.Name = "Shape3";
            this.Shape3.RoundingRadius = 9.999999F;
            this.Shape3.Top = 0F;
            this.Shape3.Width = 7.0625F;
            // 
            // TextBox9
            // 
            this.TextBox9.Border.BottomColor = System.Drawing.Color.Black;
            this.TextBox9.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.TextBox9.Border.LeftColor = System.Drawing.Color.Black;
            this.TextBox9.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.TextBox9.Border.RightColor = System.Drawing.Color.Black;
            this.TextBox9.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.TextBox9.Border.TopColor = System.Drawing.Color.Black;
            this.TextBox9.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.TextBox9.DataField = "Category";
            this.TextBox9.Height = 0.2F;
            this.TextBox9.Left = 0.5F;
            this.TextBox9.Name = "TextBox9";
            this.TextBox9.Style = "font-size: 9pt; ";
            this.TextBox9.Text = "Category";
            this.TextBox9.Top = 0F;
            this.TextBox9.Width = 1F;
            // 
            // Line2
            // 
            this.Line2.Border.BottomColor = System.Drawing.Color.Black;
            this.Line2.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Line2.Border.LeftColor = System.Drawing.Color.Black;
            this.Line2.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Line2.Border.RightColor = System.Drawing.Color.Black;
            this.Line2.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Line2.Border.TopColor = System.Drawing.Color.Black;
            this.Line2.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Line2.Height = 0F;
            this.Line2.Left = 0.4375F;
            this.Line2.LineWeight = 1F;
            this.Line2.Name = "Line2";
            this.Line2.Top = 0.1875F;
            this.Line2.Width = 7.0625F;
            this.Line2.X1 = 0.4375F;
            this.Line2.X2 = 7.5F;
            this.Line2.Y1 = 0.1875F;
            this.Line2.Y2 = 0.1875F;
            // 
            // TextBox8
            // 
            this.TextBox8.Border.BottomColor = System.Drawing.Color.Black;
            this.TextBox8.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.TextBox8.Border.LeftColor = System.Drawing.Color.Black;
            this.TextBox8.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.TextBox8.Border.RightColor = System.Drawing.Color.Black;
            this.TextBox8.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.TextBox8.Border.TopColor = System.Drawing.Color.Black;
            this.TextBox8.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.TextBox8.DataField = "TotalHours";
            this.TextBox8.Height = 0.175F;
            this.TextBox8.Left = 5.5625F;
            this.TextBox8.Name = "TextBox8";
            this.TextBox8.OutputFormat = resources.GetString("TextBox8.OutputFormat");
            this.TextBox8.Style = "text-align: right; font-size: 9pt; ";
            this.TextBox8.SummaryGroup = "GroupHeader3";
            this.TextBox8.SummaryType = DataDynamics.ActiveReports.SummaryType.SubTotal;
            this.TextBox8.Text = "TextBox";
            this.TextBox8.Top = 0F;
            this.TextBox8.Width = 0.4375F;
            // 
            // TextBox10
            // 
            this.TextBox10.Border.BottomColor = System.Drawing.Color.Black;
            this.TextBox10.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.TextBox10.Border.LeftColor = System.Drawing.Color.Black;
            this.TextBox10.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.TextBox10.Border.RightColor = System.Drawing.Color.Black;
            this.TextBox10.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.TextBox10.Border.TopColor = System.Drawing.Color.Black;
            this.TextBox10.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.TextBox10.DataField = "TotalDollars";
            this.TextBox10.Height = 0.175F;
            this.TextBox10.Left = 6.6F;
            this.TextBox10.Name = "TextBox10";
            this.TextBox10.OutputFormat = resources.GetString("TextBox10.OutputFormat");
            this.TextBox10.Style = "text-align: right; font-size: 9pt; ";
            this.TextBox10.SummaryGroup = "GroupHeader3";
            this.TextBox10.SummaryType = DataDynamics.ActiveReports.SummaryType.SubTotal;
            this.TextBox10.Text = "TextBox";
            this.TextBox10.Top = 0F;
            this.TextBox10.Width = 0.875F;
            // 
            // TextBox20
            // 
            this.TextBox20.Border.BottomColor = System.Drawing.Color.Black;
            this.TextBox20.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.TextBox20.Border.LeftColor = System.Drawing.Color.Black;
            this.TextBox20.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.TextBox20.Border.RightColor = System.Drawing.Color.Black;
            this.TextBox20.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.TextBox20.Border.TopColor = System.Drawing.Color.Black;
            this.TextBox20.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.TextBox20.DataField = "CategoryDescription";
            this.TextBox20.Height = 0.2F;
            this.TextBox20.Left = 1.520833F;
            this.TextBox20.Name = "TextBox20";
            this.TextBox20.Style = "font-size: 9pt; ";
            this.TextBox20.Text = "TextBox19";
            this.TextBox20.Top = 0F;
            this.TextBox20.Width = 3.0625F;
            // 
            // GroupFooter3
            // 
            this.GroupFooter3.Height = 0F;
            this.GroupFooter3.Name = "GroupFooter3";
            // 
            // GroupHeader4
            // 
            this.GroupHeader4.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.Shape4,
            this.TextBox13,
            this.TextBox14,
            this.TextBox18,
            this.TextBox21});
            this.GroupHeader4.DataField = "Activity";
            this.GroupHeader4.Height = 0.2F;
            this.GroupHeader4.KeepTogether = true;
            this.GroupHeader4.Name = "GroupHeader4";
            // 
            // Shape4
            // 
            this.Shape4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.Shape4.Border.BottomColor = System.Drawing.Color.Black;
            this.Shape4.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Shape4.Border.LeftColor = System.Drawing.Color.Black;
            this.Shape4.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Shape4.Border.RightColor = System.Drawing.Color.Black;
            this.Shape4.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Shape4.Border.TopColor = System.Drawing.Color.Black;
            this.Shape4.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.Shape4.Height = 0.1875F;
            this.Shape4.Left = 0.5625F;
            this.Shape4.Name = "Shape4";
            this.Shape4.RoundingRadius = 9.999999F;
            this.Shape4.Top = 0F;
            this.Shape4.Width = 6.9375F;
            // 
            // TextBox13
            // 
            this.TextBox13.Border.BottomColor = System.Drawing.Color.Black;
            this.TextBox13.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.TextBox13.Border.LeftColor = System.Drawing.Color.Black;
            this.TextBox13.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.TextBox13.Border.RightColor = System.Drawing.Color.Black;
            this.TextBox13.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.TextBox13.Border.TopColor = System.Drawing.Color.Black;
            this.TextBox13.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.TextBox13.DataField = "Activity";
            this.TextBox13.Height = 0.2F;
            this.TextBox13.Left = 0.625F;
            this.TextBox13.Name = "TextBox13";
            this.TextBox13.Style = "font-size: 9pt; ";
            this.TextBox13.Text = "Activity";
            this.TextBox13.Top = 0F;
            this.TextBox13.Width = 1F;
            // 
            // TextBox14
            // 
            this.TextBox14.Border.BottomColor = System.Drawing.Color.Black;
            this.TextBox14.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.TextBox14.Border.LeftColor = System.Drawing.Color.Black;
            this.TextBox14.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.TextBox14.Border.RightColor = System.Drawing.Color.Black;
            this.TextBox14.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.TextBox14.Border.TopColor = System.Drawing.Color.Black;
            this.TextBox14.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.TextBox14.DataField = "TotalHours";
            this.TextBox14.Height = 0.175F;
            this.TextBox14.Left = 5.572917F;
            this.TextBox14.Name = "TextBox14";
            this.TextBox14.OutputFormat = resources.GetString("TextBox14.OutputFormat");
            this.TextBox14.Style = "text-align: right; font-size: 9pt; ";
            this.TextBox14.SummaryGroup = "GroupHeader4";
            this.TextBox14.SummaryType = DataDynamics.ActiveReports.SummaryType.SubTotal;
            this.TextBox14.Text = "TextBox";
            this.TextBox14.Top = 0F;
            this.TextBox14.Width = 0.4375F;
            // 
            // TextBox18
            // 
            this.TextBox18.Border.BottomColor = System.Drawing.Color.Black;
            this.TextBox18.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.TextBox18.Border.LeftColor = System.Drawing.Color.Black;
            this.TextBox18.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.TextBox18.Border.RightColor = System.Drawing.Color.Black;
            this.TextBox18.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.TextBox18.Border.TopColor = System.Drawing.Color.Black;
            this.TextBox18.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.TextBox18.DataField = "TotalDollars";
            this.TextBox18.Height = 0.175F;
            this.TextBox18.Left = 6.610417F;
            this.TextBox18.Name = "TextBox18";
            this.TextBox18.OutputFormat = resources.GetString("TextBox18.OutputFormat");
            this.TextBox18.Style = "text-align: right; font-size: 9pt; ";
            this.TextBox18.SummaryGroup = "GroupHeader4";
            this.TextBox18.SummaryType = DataDynamics.ActiveReports.SummaryType.SubTotal;
            this.TextBox18.Text = "TextBox";
            this.TextBox18.Top = 0F;
            this.TextBox18.Width = 0.875F;
            // 
            // TextBox21
            // 
            this.TextBox21.Border.BottomColor = System.Drawing.Color.Black;
            this.TextBox21.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.TextBox21.Border.LeftColor = System.Drawing.Color.Black;
            this.TextBox21.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.TextBox21.Border.RightColor = System.Drawing.Color.Black;
            this.TextBox21.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.TextBox21.Border.TopColor = System.Drawing.Color.Black;
            this.TextBox21.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
            this.TextBox21.DataField = "ActivityDescription";
            this.TextBox21.Height = 0.2F;
            this.TextBox21.Left = 1.625F;
            this.TextBox21.Name = "TextBox21";
            this.TextBox21.Style = "font-size: 9pt; ";
            this.TextBox21.Text = "TextBox19";
            this.TextBox21.Top = 0F;
            this.TextBox21.Width = 3.0625F;
            // 
            // GroupFooter4
            // 
            this.GroupFooter4.Height = 0F;
            this.GroupFooter4.Name = "GroupFooter4";
            // 
            // rprtBudgetDetail
            // 
            this.MasterReport = false;
            this.PageSettings.Margins.Bottom = 0.3F;
            this.PageSettings.Margins.Left = 0.5F;
            this.PageSettings.Margins.Right = 0.2F;
            this.PageSettings.Margins.Top = 0.5F;
            this.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Portrait;
            this.PageSettings.PaperHeight = 11F;
            this.PageSettings.PaperWidth = 8.5F;
            this.PrintWidth = 7.510417F;
            this.Sections.Add(this.ReportHeader);
            this.Sections.Add(this.PageHeader);
            this.Sections.Add(this.GroupHeader1);
            this.Sections.Add(this.GroupHeader2);
            this.Sections.Add(this.GroupHeader3);
            this.Sections.Add(this.GroupHeader4);
            this.Sections.Add(this.Detail);
            this.Sections.Add(this.GroupFooter4);
            this.Sections.Add(this.GroupFooter3);
            this.Sections.Add(this.GroupFooter2);
            this.Sections.Add(this.GroupFooter1);
            this.Sections.Add(this.PageFooter);
            this.Sections.Add(this.ReportFooter);
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule(resources.GetString("$this.StyleSheet"), "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-family: inherit; font-style: inherit; font-variant: inherit; font-weight: bo" +
                        "ld; font-size: 16pt; font-size-adjust: inherit; font-stretch: inherit; ", "Heading1", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-family: Times New Roman; font-style: italic; font-variant: inherit; font-wei" +
                        "ght: bold; font-size: 14pt; font-size-adjust: inherit; font-stretch: inherit; ", "Heading2", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-family: inherit; font-style: inherit; font-variant: inherit; font-weight: bo" +
                        "ld; font-size: 13pt; font-size-adjust: inherit; font-stretch: inherit; ", "Heading3", "Normal"));
            this.ReportStart += new System.EventHandler(this.rprtBudgetDetail_ReportStart);
            ((System.ComponentModel.ISupportInitialize)(this.TextBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox24)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEngTotalHrs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEngLdRt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEngTotLdDlrs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNonEngTotHrs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNonEngLdRt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNonEngTotLdDlrs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalLoadedDlrs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalHours)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalLoadedRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Picture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCustomerLocation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblJobDescription)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblJobNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblRevision)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label15)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDateTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox22)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox23)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDepartment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDiscipline)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox15)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox16)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox17)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDeptTotals)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox19)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox20)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox18)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox21)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

		 }

		#endregion

        private void Detail_Format(object sender, EventArgs e)
        {
        }
	}
}
