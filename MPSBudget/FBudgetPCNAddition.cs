using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Data.SqlClient;

namespace RSMPS
{
    public partial class FBudgetPCNAddition : Form
    {
        private CBProject moProj;
        private CBBudget moBudg;
        private CBBudgetPCN moPCN;
        private dsAccts mdsAccnts;
        private dsAccts mdsExpensAccts;

        public event RevSol.ItemValueChangedHandler OnPCNChanged;

        public void StartNewPCN(int projID)
        {
            ClearForm();

            moProj = new CBProject();
            moProj.Load(projID);
            moPCN.PCNNumber = moPCN.GetNextPCNNumber(projID);
            moPCN.ProjectID = projID;

            lblProjectNumber.Text = moProj.Number;
            lblProjectTitle.Text = moProj.Description;

            this.Text = "PCN: Job-" + moProj.Number + " PCN-" + moPCN.PCNNumber;

            SetPCNSecurityLevel();
        }

        public void EditPreviousPCN(int pcnID)
        {
            ClearForm();

            moProj = new CBProject();
            moBudg = new CBBudget();

            moPCN.LoadWithData(pcnID);
            moProj.Load(moPCN.ProjectID);
            moBudg.LoadByProject(moProj.ID);

            LoadFromPCN();

            this.Text = "PCN: Job-" + moProj.Number + " PCN-" + moPCN.PCNNumber;

            SetPCNSecurityLevel();
        }

        public FBudgetPCNAddition()
        {
            InitializeComponent();
        }

        public void ClearForm()
        {
            moPCN = new CBBudgetPCN();
            moPCN.Clear();

            txtInitiatedBy.Text = "";
            lblProjectNumber.Text = "";
            lblProjectTitle.Text = "";
            txtPCNTitle.Text = "";
            dtpDateInitiated.Value = DateTime.Now;
            txtRequestedBy.Text = "";
            dtpDateRequested.Value = DateTime.Now;
            txtDescription.Text = "";
            chkDesignError.Checked = false;
            chkVendorError.Checked = false;
            chkEstimatingError.Checked = false;
            chkContractorError.Checked = false;
            chkScheduleDelay.Checked = false;
            chkScopeAdd.Checked = false;
            chkScopeDel.Checked = false;
            chkDesignChange.Checked = false;
            chkOther.Checked = false;
            txtOtherReason.Text = "";
            txtOtherReason.Enabled = false;
            txtEstimatedHrs.Text = "";
            txtEstimatedDollars.Text = "";
            txtEstimTIC.Text = "";
            txtEstimateAccuracy.Text = "";
            txtScheduleImpact.Text = "";
            chkApproved.Checked = false;
            chkDisapproved.Checked = false;
            chkPrepareControlEstimate.Checked = false;
            txtProjMngr.Text = "";
            lblDateApproved.Text = "";

            tdbgHours.SetDataBinding(moPCN.PCNData, "PCNHours", true);
            tdbgExpenses.SetDataBinding(moPCN.PCNData, "PCNExpenses", true);

            SqlDataReader dr = CBActivityCode.GetListForBudget();
            DataRow d;
            mdsAccnts = new dsAccts();
            mdsExpensAccts = new dsAccts();

            while (dr.Read())
            {
                d = mdsAccnts.Tables["Accounts"].NewRow();
                d["Code"] = dr["Code"];
                d["Description"] = dr["Description"];

                mdsAccnts.Tables["Accounts"].Rows.Add(d);
            }

            dr.Close();

            tdbdActivities.SetDataBinding(mdsAccnts, "Accounts", true);

            dr = CBProjectBudget.GetExpenseGroupByDiscipline(0);

            while (dr.Read())
            {
                d = mdsExpensAccts.Tables["Accounts"].NewRow();
                d["Code"] = dr["Code"];
                d["Description"] = dr["Description"];

                mdsExpensAccts.Tables["Accounts"].Rows.Add(d);
            }

            dr.Close();

            tdbdExpenseAccts.SetDataBinding(mdsExpensAccts, "Accounts", true);
        }

        private void SetPCNSecurityLevel()
        {
            RSLib.COSecurity sec = new RSLib.COSecurity();
            CBUser u = new CBUser();

            sec.InitAppSettings();
            u.Load(sec.UserID);

            if (u.IsAdministrator == true || u.IsEngineerAdmin == true || u.IsManager == true)
            {
            }
            else
            {
                tlbbPrint.Enabled = false;

                label10.Visible = false;
                txtEstimatedHrs.Visible = false;
                label15.Visible = false;
                txtEstimatedDollars.Visible = false;
                label2.Visible = false;
                txtEstimTIC.Visible = false;
                label1.Visible = false;
                txtEstimateAccuracy.Visible = false;

                tdbgHours.Splits[0].DisplayColumns[5].Visible = false;
                tdbgHours.Splits[0].DisplayColumns[7].Visible = false;
                /*
                tdbgExpenses.Splits[0].DisplayColumns[2].Visible = false;
                tdbgExpenses.Splits[0].DisplayColumns[4].Visible = false;
                tdbgExpenses.Splits[0].DisplayColumns[5].Visible = false;
                tdbgExpenses.Splits[0].DisplayColumns[6].Visible = false;
                */
            }
        }

        private void tlbbClose_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            this.Close();
        }

        private void tlbbClear_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            txtInitiatedBy.Text = "";
            lblProjectNumber.Text = "";
            lblProjectTitle.Text = "";
            txtPCNTitle.Text = "";
            dtpDateInitiated.Value = DateTime.Now;
            txtRequestedBy.Text = "";
            dtpDateRequested.Value = DateTime.Now;
            txtDescription.Text = "";
            chkDesignError.Checked = false;
            chkVendorError.Checked = false;
            chkEstimatingError.Checked = false;
            chkContractorError.Checked = false;
            chkScheduleDelay.Checked = false;
            chkScopeAdd.Checked = false;
            chkScopeDel.Checked = false;
            chkDesignChange.Checked = false;
            chkOther.Checked = false;
            txtOtherReason.Text = "";
            txtOtherReason.Enabled = false;
            txtEstimatedHrs.Text = "";
            txtEstimatedDollars.Text = "";
            txtEstimTIC.Text = "";
            txtEstimateAccuracy.Text = "";
            txtScheduleImpact.Text = "";
            chkApproved.Checked = false;
            chkDisapproved.Checked = false;
            chkPrepareControlEstimate.Checked = false;
            txtProjMngr.Text = "";
            lblDateApproved.Text = "";

            moPCN.PCNData.PCNHours.Clear();
            moPCN.PCNData.PCNExpenses.Clear();
        }

        private void tdbgHours_BeforeColUpdate(object sender, C1.Win.C1TrueDBGrid.BeforeColUpdateEventArgs e)
        {
            if (e.ColIndex != 0)
                return;

            string acct = tdbgHours.Columns[0].Value.ToString();
            string desc = IsValidAccount(acct);

            if (desc.Length < 1)
                e.Cancel = true;
            else
                tdbgHours.Columns[1].Value = desc;
        }

        private void tdbgHours_AfterColUpdate(object sender, C1.Win.C1TrueDBGrid.ColEventArgs e)
        {
            if (e.ColIndex < 3 || e.ColIndex > 5)
                return;

            decimal quantity, hrsPer, rate;

            quantity = RevSol.RSMath.IsDecimalEx(tdbgHours.Columns[2].Value);
            hrsPer = RevSol.RSMath.IsDecimalEx(tdbgHours.Columns[3].Value);
            rate = RevSol.RSMath.IsDecimalEx(tdbgHours.Columns[4].Value);

            decimal subHrs, subDlrs;

            subHrs = quantity * hrsPer;
            subDlrs = quantity * hrsPer * rate;

            tdbgHours.Columns[5].Value = subHrs;
            tdbgHours.Columns[6].Value = subDlrs;
        }

        private void tdbgHours_AfterUpdate(object sender, EventArgs e)
        {
            //20131218 - Added Code to trap missing Activity Code which causes system crash
            string acct = tdbgHours.Columns[0].Value.ToString();

            if (acct.Length < 1)
            {
                tlbbSave.Enabled = false;
                MessageBox.Show("Input an Activity Code", "Missing Activity Code", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else
                tlbbSave.Enabled = true;
                TotalHoursGrid();
                
                
            //tlbbSave.Enabled = true;
            
        }

        private void TotalHoursGrid()
        {
            decimal hours;
            decimal dollars;

            hours = 0;
            dollars = 0;

            foreach (DataRow dr in moPCN.PCNData.PCNHours.Rows)
            {
                hours += Convert.ToDecimal(dr["SubtotalHrs"]);
                dollars += Convert.ToDecimal(dr["SubtotalDlrs"]);
            }

            tdbgHours.Columns[5].FooterText = hours.ToString("#,##0");
            tdbgHours.Columns[6].FooterText = dollars.ToString("$#,##0.00");

            txtEstimatedHrs.Text = hours.ToString("#,##0");
            txtEstimatedDollars.Text = TotalDollars().ToString("#,##0.00");
        }

        private string IsValidAccount(string acct)
        {
            string retVal = "";
            string rowVal;

            foreach (DataRow d in mdsAccnts.Tables["Accounts"].Rows)
            {
                rowVal = d["Code"].ToString();

                if (rowVal == acct)
                {
                    retVal = d["Description"].ToString();
                    break;
                }
            }

            return retVal;
        }

        private void SaveCurrentPCN()
        {
            tdbgHours.UpdateData();
            tdbgExpenses.UpdateData();

            LoadScreenToObject();

            moPCN.SaveWithData();
        }

        private void LoadScreenToObject()
        {
            moPCN.PCNTitle = txtPCNTitle.Text;
            moPCN.DateInitiated = dtpDateInitiated.Value;
            moPCN.RequestedBy = txtRequestedBy.Text;
            moPCN.DateRequested = dtpDateRequested.Value;
            moPCN.DescOfChange = txtDescription.Text;
            moPCN.ReasonDesignError = chkDesignError.Checked;
            moPCN.ReasonVendorError = chkVendorError.Checked;
            moPCN.ReasonEstimatingError = chkEstimatingError.Checked;
            moPCN.ReasonContractorError = chkContractorError.Checked;
            moPCN.ReasonSchedule = chkScheduleDelay.Checked;
            moPCN.ReasonScopeAdd = chkScopeAdd.Checked;
            moPCN.ReasonScopeDel = chkScopeDel.Checked;
            moPCN.ReasonDesignChange = chkDesignChange.Checked;
            moPCN.ReasonOther = chkOther.Checked;
            moPCN.OtherReasonDesc = txtOtherReason.Text;
            moPCN.EstimatedEngrHrs = RevSol.RSMath.IsIntegerEx(txtEstimatedHrs.Text);
            moPCN.EstimatedEngrDlrs = RevSol.RSMath.IsDecimalEx(txtEstimatedDollars.Text);
            moPCN.EstimatedTICDlrs = RevSol.RSMath.IsDecimalEx(txtEstimTIC.Text);
            moPCN.EstimateAccuracy = txtEstimateAccuracy.Text;
            moPCN.ScheduleImpact = txtScheduleImpact.Text;
            moPCN.IsApproved = chkApproved.Checked;
            moPCN.IsDisapproved = chkDisapproved.Checked;
            moPCN.PrepareControlEstimate = chkPrepareControlEstimate.Checked;
            moPCN.Comments = rtbComments.Rtf;

            moPCN.DateApproved = GetApprovedDate();
        }

        private DateTime GetApprovedDate()
        {
            DateTime retVal;

            if (chkApproved.Checked == true)
            {
                retVal = Convert.ToDateTime(lblDateApproved.Text);
            }
            else if (chkDisapproved.Checked == true)
            {
                retVal = Convert.ToDateTime(lblDateApproved.Text);
            }
            else if (chkPrepareControlEstimate.Checked == true)
            {
                retVal = Convert.ToDateTime(lblDateApproved.Text);
            }
            else
            {
                retVal = RevSol.RSUtility.DefaultDate();
            }

            return retVal;
        }

        private void chkAppvProceed_CheckedChanged(object sender, EventArgs e)
        {
            if (chkApproved.Checked == true)
            {
                chkDisapproved.Checked = false;
                chkPrepareControlEstimate.Checked = false;

                lblDateApproved.Text = DateTime.Now.ToShortDateString();
            }
        }

        private void chkAppvTrack_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDisapproved.Checked == true)
            {
                chkApproved.Checked = false;
                chkPrepareControlEstimate.Checked = false;

                lblDateApproved.Text = DateTime.Now.ToShortDateString();
            }
        }

        private void chkNotAppvDNP_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPrepareControlEstimate.Checked == true)
            {
                chkApproved.Checked = false;
                chkDisapproved.Checked = false;

                lblDateApproved.Text = DateTime.Now.ToShortDateString();
            }
        }

        private decimal TotalHours()
        {
            decimal hours;

            hours = 0;

            foreach (DataRow dr in moPCN.PCNData.PCNHours.Rows)
            {
                hours += Convert.ToDecimal(dr["SubtotalHrs"]);
            }

            return hours;
        }

        private decimal TotalDollars()
        {
            decimal dollars;
            decimal exps;

            dollars = 0;
            exps = 0;

            foreach (DataRow dr in moPCN.PCNData.PCNHours.Rows)
            {
                dollars += Convert.ToDecimal(dr["SubtotalDlrs"]);
            }

            foreach (DataRow dr in moPCN.PCNData.PCNExpenses.Rows)
            {
                exps += Convert.ToDecimal(dr["TotalCost"]);
                dollars += Convert.ToDecimal(dr["TotalCost"]);
            }

            return dollars;
        }

        private void tlbbSave_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            SaveCurrentPCN();

            tlbbSave.Enabled = false;
        }

        private void FBudgetPCNAddition_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (tlbbSave.Enabled == true)
                SaveCurrentPCN();

            if (OnPCNChanged != null)
                OnPCNChanged(moPCN.ID, moPCN.PCNNumber);
        }

        private void tdbgExpenses_BeforeColUpdate(object sender, C1.Win.C1TrueDBGrid.BeforeColUpdateEventArgs e)
        {
            if (e.ColIndex != 0)
                return;

            string exps = tdbgExpenses.Columns[0].Value.ToString();
            string desc = IsValidExpense(exps);

            if (desc.Length < 1)
                e.Cancel = true;
            else
                tdbgExpenses.Columns[1].Value = desc;
        }

        private void tdbgExpenses_AfterColUpdate(object sender, C1.Win.C1TrueDBGrid.ColEventArgs e)
        {
            if (e.ColIndex < 2)
                return;

            decimal dllrPerItem, numItems, percMU, markup, total;

            dllrPerItem = RevSol.RSMath.IsDecimalEx(tdbgExpenses.Columns[2].Value);
            numItems = RevSol.RSMath.IsDecimalEx(tdbgExpenses.Columns[3].Value);
            percMU = RevSol.RSMath.IsDecimalEx(tdbgExpenses.Columns[4].Value);
            markup = RevSol.RSMath.IsDecimalEx(tdbgExpenses.Columns[5].Value);
            total = RevSol.RSMath.IsDecimalEx(tdbgExpenses.Columns[6].Value);

            markup = dllrPerItem * numItems * (percMU / 100.0m);
            total = (dllrPerItem * numItems) + markup;

            tdbgExpenses.Columns[5].Value = markup;
            tdbgExpenses.Columns[6].Value = total;
        }

        private void tdbgExpenses_AfterUpdate(object sender, EventArgs e)
        {
            TotalExpenseGrid();

            tlbbSave.Enabled = true;
        }

        private void TotalExpenseGrid()
        {
            decimal muAmnt;
            decimal dollars;

            muAmnt = 0;
            dollars = 0;

            foreach (DataRow dr in moPCN.PCNData.PCNExpenses.Rows)
            {
                muAmnt += Convert.ToDecimal(dr["MarkUp"]);
                dollars += Convert.ToDecimal(dr["TotalCost"]);
            }

            tdbgExpenses.Columns[5].FooterText = muAmnt.ToString("$#,##0.00");
            tdbgExpenses.Columns[6].FooterText = dollars.ToString("$#,##0.00");

            txtEstimatedDollars.Text = TotalDollars().ToString("#,##0.00");
        }

        private string IsValidExpense(string expense)
        {
            string retVal = "";
            string rowVal;

            foreach (DataRow d in mdsExpensAccts.Tables["Accounts"].Rows)
            {
                rowVal = d["Code"].ToString();

                if (rowVal == expense)
                {
                    retVal = d["Description"].ToString();
                    break;
                }
            }

            return retVal;
        }

        private void txtRequestedBy_TextChanged(object sender, EventArgs e)
        {
            tlbbSave.Enabled = true;
        }

        private void dtpRequestDate_ValueChanged(object sender, EventArgs e)
        {
            tlbbSave.Enabled = true;
        }

        private void LoadFromPCN()
        {
            CBEmployee emp = new CBEmployee();

            lblProjectNumber.Text = moProj.Number;
            lblProjectTitle.Text = moProj.Description;

            txtPCNTitle.Text = moPCN.PCNTitle;
            emp.Load(moPCN.InitiatedByID);
            txtInitiatedBy.Text = emp.Name;
            dtpDateInitiated.Value = moPCN.DateInitiated;
            txtRequestedBy.Text = moPCN.RequestedBy;
            dtpDateRequested.Value = moPCN.DateRequested;
            txtDescription.Text = moPCN.DescOfChange;
            chkDesignError.Checked = moPCN.ReasonDesignError;
            chkVendorError.Checked = moPCN.ReasonVendorError;
            chkEstimatingError.Checked = moPCN.ReasonEstimatingError;
            chkContractorError.Checked = moPCN.ReasonContractorError;
            chkScheduleDelay.Checked = moPCN.ReasonSchedule;
            chkScopeAdd.Checked = moPCN.ReasonScopeAdd;
            chkScopeDel.Checked = moPCN.ReasonScopeDel;
            chkDesignChange.Checked = moPCN.ReasonDesignChange;
            chkOther.Checked = moPCN.ReasonOther;
            txtOtherReason.Text = moPCN.OtherReasonDesc;
            txtEstimatedHrs.Text = moPCN.EstimatedEngrHrs.ToString();
            txtEstimatedDollars.Text = moPCN.EstimatedEngrDlrs.ToString("#,##0");
            txtEstimTIC.Text = moPCN.EstimatedTICDlrs.ToString("#,##0");
            txtEstimateAccuracy.Text = moPCN.EstimateAccuracy;
            txtScheduleImpact.Text = moPCN.ScheduleImpact;
            chkApproved.Checked = moPCN.IsApproved;
            chkDisapproved.Checked = moPCN.IsDisapproved;
            chkPrepareControlEstimate.Checked = moPCN.PrepareControlEstimate;

            if (moPCN.DateApproved == RevSol.RSUtility.DefaultDate())
                lblDateApproved.Text = "";
            else
                lblDateApproved.Text = moPCN.DateApproved.ToShortDateString();

            tdbgHours.SetDataBinding(moPCN.PCNData, "PCNHours", true);
            tdbgExpenses.SetDataBinding(moPCN.PCNData, "PCNExpenses", true);

            TotalHoursGrid();
            TotalExpenseGrid();

            rtbComments.Rtf = moPCN.Comments;
        }

        private void tlbbPrint_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            SaveCurrentPCN();

            tlbbSave.Enabled = false;

            CPBudget pBud = new CPBudget();

            pBud.PreviewPCN(moPCN.ID);
        }

        private void txtDescription_TextChanged(object sender, EventArgs e)
        {
            tlbbSave.Enabled = true;
        }

        private void txtEffectSchedule_TextChanged(object sender, EventArgs e)
        {
            tlbbSave.Enabled = true;
        }

        private void txtEffectCapital_TextChanged(object sender, EventArgs e)
        {
            tlbbSave.Enabled = true;
        }

        private void bttInitiatedBy_Click(object sender, EventArgs e)
        {
            FBudEmp_List el = new FBudEmp_List();

            el.IsSelectOnly = true;
            el.OnItemSelected += new RSLib.ListItemAction(el_OnItemSelected);
            el.ShowDialog();
        }

        void el_OnItemSelected(int itmID)
        {
            CBEmployee emp = new CBEmployee();

            emp.Load(itmID);

            txtInitiatedBy.Text = emp.Name;
            txtInitiatedBy.Tag = emp.ID;
            moPCN.InitiatedByID = itmID;

            SetAllowSave(true);
        }

        private void deleteLineInHours_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you wish to delete the current line?", "Delete Hours", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                DataRow dr = moPCN.PCNData.PCNHours.Rows[tdbgHours.Bookmark];
                DataRow dd = moPCN.PCNData.PCNHoursDeleted.NewRow();
                dd["ID"] = dr["ID"];
                moPCN.PCNData.PCNHoursDeleted.Rows.Add(dd);

                tdbgHours.Delete();
            }
        }

        private void tdbgHours_BeforeDelete(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
        {
            if (MessageBox.Show("Are you sure you wish to delete the current line?", "Delete Hours", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                DataRow dr = moPCN.PCNData.PCNHours.Rows[tdbgHours.Bookmark];
                DataRow dd = moPCN.PCNData.PCNHoursDeleted.NewRow();
                dd["ID"] = dr["ID"];
                moPCN.PCNData.PCNHoursDeleted.Rows.Add(dd);
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void cmnuHours_Opening(object sender, CancelEventArgs e)
        {
            if (tdbgHours.Bookmark < 0)
                deleteLineInHours.Enabled = false;
            else
                deleteLineInHours.Enabled = true;
        }

        private void tdbgExpenses_BeforeDelete(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
        {
            if (MessageBox.Show("Are you sure you wish to delete the current line?", "Delete Expenses", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                DataRow dr = moPCN.PCNData.PCNExpenses.Rows[tdbgExpenses.Bookmark];
                DataRow dd = moPCN.PCNData.PCNExpensesDeleted.NewRow();
                dd["ID"] = dr["ID"];
                moPCN.PCNData.PCNExpensesDeleted.Rows.Add(dd);
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void deleteLineInExpenses_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you wish to delete the current line?", "Delete Expenses", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                DataRow dr = moPCN.PCNData.PCNExpenses.Rows[tdbgExpenses.Bookmark];
                DataRow dd = moPCN.PCNData.PCNExpensesDeleted.NewRow();
                dd["ID"] = dr["ID"];
                moPCN.PCNData.PCNExpensesDeleted.Rows.Add(dd);

                tdbgExpenses.Delete();
            }
        }

        private void cmnuExpenses_Opening(object sender, CancelEventArgs e)
        {
            if (tdbgExpenses.Bookmark < 0)
                deleteLineInExpenses.Enabled = false;
            else
                deleteLineInExpenses.Enabled = true;
        }

        private void SetAllowSave(bool saveState)
        {
            tlbbSave.Enabled = saveState;
        }

        private void dtpDateInitiated_ValueChanged(object sender, EventArgs e)
        {
            SetAllowSave(true);
        }

        private void txtRequestedBy_TextChanged_1(object sender, EventArgs e)
        {
            SetAllowSave(true);
        }

        private void dtpDateRequested_ValueChanged(object sender, EventArgs e)
        {
            SetAllowSave(true);
        }

        private void txtPCNTitle_TextChanged(object sender, EventArgs e)
        {
            SetAllowSave(true);
        }

        private void txtDescription_TextChanged_1(object sender, EventArgs e)
        {
            SetAllowSave(true);
        }

        private void txtEstimTIC_TextChanged(object sender, EventArgs e)
        {
            SetAllowSave(true);
        }

        private void txtEstimateAccuracy_TextChanged(object sender, EventArgs e)
        {
            SetAllowSave(true);
        }

        private void txtScheduleImpact_TextChanged(object sender, EventArgs e)
        {
            SetAllowSave(true);
        }

        private void rdoDesignError_CheckedChanged(object sender, EventArgs e)
        {
            SetAllowSave(true);
        }

        private void rdoVendorError_CheckedChanged(object sender, EventArgs e)
        {
            SetAllowSave(true);
        }

        private void rdoEstimatingError_CheckedChanged(object sender, EventArgs e)
        {
            SetAllowSave(true);
        }

        private void rdoContractorError_CheckedChanged(object sender, EventArgs e)
        {
            SetAllowSave(true);
        }

        private void rdoScheduleDelay_CheckedChanged(object sender, EventArgs e)
        {
            SetAllowSave(true);
        }

        private void rdoScopeAdd_CheckedChanged(object sender, EventArgs e)
        {
            SetAllowSave(true);
        }

        private void rdoScopeDel_CheckedChanged(object sender, EventArgs e)
        {
            SetAllowSave(true);
        }

        private void rdoDesignChange_CheckedChanged(object sender, EventArgs e)
        {
            SetAllowSave(true);
        }

        private void rdoOther_CheckedChanged(object sender, EventArgs e)
        {
            if (chkOther.Checked == true)
            {
                txtOtherReason.Enabled = true;
            }
            else
            {
                txtOtherReason.Enabled = false;
                txtOtherReason.Text = "";
            }

            SetAllowSave(true);
        }

        private void txtOtherReason_TextChanged(object sender, EventArgs e)
        {
            SetAllowSave(true);
        }

        private void rtbComments_TextChanged(object sender, EventArgs e)
        {
            SetAllowSave(true);
        }

        private void tdbgHours_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                tdbgHours.UpdateData();
            }
        }

        private void tdbgExpenses_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                tdbgExpenses.UpdateData();
            }
        }
    }
}