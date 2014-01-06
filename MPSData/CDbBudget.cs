using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.SqlClient;
using System.Xml;
using System.Xml.Serialization;
using System.IO;


namespace RSMPS
{
    public class CDbBudget
    {
        private COBudget oVar;

        public string GetByID(int lID)
        {
            SqlDataReader dr;
            RSLib.CDbConnection cnn;
            SqlCommand cmd;
            SqlParameter prm;
            string tmpStr = "";

            cnn = new RSLib.CDbConnection();
            cmd = new SqlCommand("spBudget_ByID", cnn.GetConnection());
            cmd.CommandType = CommandType.StoredProcedure;


            prm = cmd.Parameters.Add("@ID", SqlDbType.Int);
            prm.Value = lID;

            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);


            while (dr.Read())
            {
                oVar = new COBudget();

                oVar.ID = Convert.ToInt32(dr["ID"]);
                oVar.ProjectID = Convert.ToInt32(dr["ProjectID"]);
                oVar.Revision = Convert.ToInt32(dr["Revision"]);
                oVar.PCNID = Convert.ToInt32(dr["PCNID"]);
                oVar.IsDefault = Convert.ToBoolean(dr["IsDefault"]);
                oVar.IsActive = Convert.ToBoolean(dr["IsActive"]);
                oVar.IsLocked = Convert.ToBoolean(dr["IsLocked"]);
                oVar.Description = dr["Description"].ToString();
                oVar.PreparedBy = dr["PreparedBy"].ToString();
                oVar.Contingency = Convert.ToDecimal(dr["Contingency"]);
                tmpStr = GetDataString();
            }

            dr = null;
            prm = null;
            cmd = null;
            cnn.CloseConnection();
            cnn = null;

            return tmpStr;
        }

        public string GetByProjectID(int projID)
        {
            SqlDataReader dr;
            RSLib.CDbConnection cnn;
            SqlCommand cmd;
            SqlParameter prm;
            string tmpStr = "";

            cnn = new RSLib.CDbConnection();
            cmd = new SqlCommand("spBudget_ByProjectID", cnn.GetConnection());
            cmd.CommandType = CommandType.StoredProcedure;


            prm = cmd.Parameters.Add("@ProjectID", SqlDbType.Int);
            prm.Value = projID;

            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);


            while (dr.Read())
            {
                oVar = new COBudget();

                oVar.ID = Convert.ToInt32(dr["ID"]);
                oVar.ProjectID = Convert.ToInt32(dr["ProjectID"]);
                oVar.Revision = Convert.ToInt32(dr["Revision"]);
                oVar.PCNID = Convert.ToInt32(dr["PCNID"]);
                oVar.IsDefault = Convert.ToBoolean(dr["IsDefault"]);
                oVar.IsActive = Convert.ToBoolean(dr["IsActive"]);
                oVar.IsLocked = Convert.ToBoolean(dr["IsLocked"]);
                oVar.Description = dr["Description"].ToString();
                oVar.PreparedBy = dr["PreparedBy"].ToString();
                oVar.Contingency = Convert.ToDecimal(dr["Contingency"]);

                tmpStr = GetDataString();
            }

            dr = null;
            prm = null;
            cmd = null;
            cnn.CloseConnection();
            cnn = null;

            return tmpStr;
        }

        public int SaveNew(string strXml)
        {
            RSLib.CDbConnection cnn;
            SqlCommand cmd;
            SqlParameter prm;
            int retVal = 0;

            LoadVals(strXml);

            cnn = new RSLib.CDbConnection();
            cmd = new SqlCommand("spBudget_Insert", cnn.GetConnection());
            cmd.CommandType = CommandType.StoredProcedure;


            prm = cmd.Parameters.Add("@ID", SqlDbType.Int);
            prm.Direction = ParameterDirection.Output;

            prm = cmd.Parameters.Add("@ProjectID", SqlDbType.Int);
            prm.Value = oVar.ProjectID;
            prm = cmd.Parameters.Add("@Revision", SqlDbType.Int);
            prm.Value = oVar.Revision;
            prm = cmd.Parameters.Add("@PCNID", SqlDbType.Int);
            prm.Value = oVar.PCNID;
            prm = cmd.Parameters.Add("@IsDefault", SqlDbType.Bit);
            prm.Value = oVar.IsDefault;
            prm = cmd.Parameters.Add("@IsActive", SqlDbType.Bit);
            prm.Value = oVar.IsActive;
            prm = cmd.Parameters.Add("@IsLocked", SqlDbType.Bit);
            prm.Value = oVar.IsLocked;
            prm = cmd.Parameters.Add("@Description", SqlDbType.VarChar, 100);
            prm.Value = oVar.Description;
            prm = cmd.Parameters.Add("@PreparedBy", SqlDbType.VarChar, 50);
            prm.Value = oVar.PreparedBy;
            prm = cmd.Parameters.Add("@Contingency", SqlDbType.Money);
            prm.Value = oVar.Contingency;
            cmd.ExecuteNonQuery();

            retVal = Convert.ToInt32(cmd.Parameters["@ID"].Value);

            prm = null;
            cmd = null;
            cnn.CloseConnection();
            cnn = null;

            return retVal;
        }


        public int SavePrev(string strXml)
        {
            RSLib.CDbConnection cnn;
            SqlCommand cmd;
            SqlParameter prm;

            LoadVals(strXml);

            cnn = new RSLib.CDbConnection();
            cmd = new SqlCommand("spBudget_Update", cnn.GetConnection());
            cmd.CommandType = CommandType.StoredProcedure;


            prm = cmd.Parameters.Add("@ID", SqlDbType.Int);
            prm.Value = oVar.ID;
            prm = cmd.Parameters.Add("@ProjectID", SqlDbType.Int);
            prm.Value = oVar.ProjectID;
            prm = cmd.Parameters.Add("@Revision", SqlDbType.Int);
            prm.Value = oVar.Revision;
            prm = cmd.Parameters.Add("@PCNID", SqlDbType.Int);
            prm.Value = oVar.PCNID;
            prm = cmd.Parameters.Add("@IsDefault", SqlDbType.Bit);
            prm.Value = oVar.IsDefault;
            prm = cmd.Parameters.Add("@IsActive", SqlDbType.Bit);
            prm.Value = oVar.IsActive;
            prm = cmd.Parameters.Add("@IsLocked", SqlDbType.Bit);
            prm.Value = oVar.IsLocked;
            prm = cmd.Parameters.Add("@Description", SqlDbType.VarChar, 100);
            prm.Value = oVar.Description;
            prm = cmd.Parameters.Add("@PreparedBy", SqlDbType.VarChar, 50);
            prm.Value = oVar.PreparedBy;
            prm = cmd.Parameters.Add("@Contingency", SqlDbType.Money);
            prm.Value = oVar.Contingency;
            cmd.ExecuteNonQuery();

            prm = null;
            cmd = null;
            cnn.CloseConnection();
            cnn = null;

            return oVar.ID;
        }


        private string GetDataString()
        {
            string tmpStr;
            XmlSerializer s;
            StringWriter sw;

            s = new XmlSerializer(typeof(COBudget));
            sw = new StringWriter();

            s.Serialize(sw, oVar);
            tmpStr = sw.ToString();

            return tmpStr;
        }


        private void LoadVals(string strXml)
        {
            XmlSerializer s;
            StringReader sr;

            s = new XmlSerializer(typeof(COBudget));
            sr = new System.IO.StringReader(strXml);

            oVar = new COBudget();
            oVar = (COBudget)s.Deserialize(sr);

            sr.Close();
            sr = null;
            s = null;
        }


        public bool Delete(int lID)
        {
            bool retVal = false;

            RSLib.CDbConnection cnn;
            SqlCommand cmd;
            SqlParameter prm;

            cnn = new RSLib.CDbConnection();
            cmd = new SqlCommand("spBudget_Delete", cnn.GetConnection());
            cmd.CommandType = CommandType.StoredProcedure;


            prm = cmd.Parameters.Add("@ID", SqlDbType.Int);
            prm.Value = lID;

            try
            {
                cmd.ExecuteNonQuery();

                retVal = true;
            }
            catch
            {
                retVal = false;
            }

            prm = null;
            cmd = null;
            cnn.CloseConnection();
            cnn = null;

            return retVal;
        }


        public SqlDataReader GetList()
        {
            SqlDataReader dr;
            RSLib.CDbConnection cnn;
            SqlCommand cmd;

            cnn = new RSLib.CDbConnection();
            cmd = new SqlCommand("spBudget_ListAll", cnn.GetConnection());
            cmd.CommandType = CommandType.StoredProcedure;


            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            cmd = null;

            return dr;
        }

        public SqlDataReader GetListByProject(int projID)
        {
            SqlDataReader dr;
            RSLib.CDbConnection cnn;
            SqlCommand cmd;
            SqlParameter prm;

            cnn = new RSLib.CDbConnection();
            cmd = new SqlCommand("spBudget_ListByProject", cnn.GetConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            prm = cmd.Parameters.Add("@ProjectID", SqlDbType.Int);
            prm.Value = projID;

            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            cmd = null;

            return dr;
        }

        public int GetLastRevision(int projID)
        {
            SqlDataReader dr;
            RSLib.CDbConnection cnn;
            SqlCommand cmd;
            SqlParameter prm;
            int lastRev = 0;

            cnn = new RSLib.CDbConnection();
            cmd = new SqlCommand("spBudget_GetLastRevision", cnn.GetConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            prm = cmd.Parameters.Add("@ProjectID", SqlDbType.Int);
            prm.Value = projID;

            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                lastRev = Convert.ToInt32(dr["CurrentCount"]);
            }

            dr.Close();
            cnn.CloseConnection();
            cmd = null;

            return lastRev;
        }

        public void MakeBudgetActive(int budID)
        {
            RSLib.CDbConnection cnn;
            SqlCommand cmd;
            SqlParameter prm;

            cnn = new RSLib.CDbConnection();
            cmd = new SqlCommand("spBudget_MakeActive", cnn.GetConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            prm = cmd.Parameters.Add("@BudgetID", SqlDbType.Int);
            prm.Value = budID;

            cmd.ExecuteNonQuery();

            cnn.CloseConnection();
            cmd = null;
        }

        public void MakeBudgetDefault(int budID)
        {
            RSLib.CDbConnection cnn;
            SqlCommand cmd;
            SqlParameter prm;

            cnn = new RSLib.CDbConnection();
            cmd = new SqlCommand("spBudget_MakeDefault", cnn.GetConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            prm = cmd.Parameters.Add("@BudgetID", SqlDbType.Int);
            prm.Value = budID;

            cmd.ExecuteNonQuery();

            cnn.CloseConnection();
            cmd = null;
        }

        public void MakeBudgetLockedUnlocked(int budID, bool isLocked)
        {
            RSLib.CDbConnection cnn;
            SqlCommand cmd;
            SqlParameter prm;

            cnn = new RSLib.CDbConnection();
            cmd = new SqlCommand("spBudget_LockUnlock", cnn.GetConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            prm = cmd.Parameters.Add("@BudgetID", SqlDbType.Int);
            prm.Value = budID;
            prm = cmd.Parameters.Add("@Lock", SqlDbType.Bit);
            prm.Value = isLocked;

            cmd.ExecuteNonQuery();

            cnn.CloseConnection();
            cmd = null;
        }

        public DataSet GetBudgetSummaryForReport(int budgetID, string wbs)
        {
            RSLib.CDbConnection cnn;
            SqlCommand cmd;
            SqlParameter prm;
            SqlDataAdapter da;
            DataSet ds;

            cnn = new RSLib.CDbConnection();
            cmd = new SqlCommand("spRPRT_BudgetSummary", cnn.GetConnection());
            cmd.CommandType = CommandType.StoredProcedure;

            prm = cmd.Parameters.Add("@BudgetID", SqlDbType.Int);
            prm.Value = budgetID;
            prm = cmd.Parameters.Add("@WBS", SqlDbType.VarChar, 5);
            prm.Value = wbs;

            da = new SqlDataAdapter();
            da.SelectCommand = cmd;

            ds = new DataSet();
            da.Fill(ds);

            cnn.CloseConnection();

            return ds;
        }

        //public DataSet GetBudgetSummaryForPipelineReport(int budgetID, string wbs)
        //{
        //    RSLib.CDbConnection cnn;
        //    SqlCommand cmd;
        //    SqlParameter prm;
        //    SqlDataAdapter da;
        //    DataSet ds;

        //    cnn = new RSLib.CDbConnection();
        //    cmd = new SqlCommand("spRPRT_BudgetSummary_Pipeline", cnn.GetConnection());
        //    cmd.CommandType = CommandType.StoredProcedure;

        //    prm = cmd.Parameters.Add("@BudgetID", SqlDbType.Int);
        //    prm.Value = budgetID;
        //    prm = cmd.Parameters.Add("@WBS", SqlDbType.VarChar, 5);
        //    prm.Value = wbs;

        //    da = new SqlDataAdapter();
        //    da.SelectCommand = cmd;

        //    ds = new DataSet();
        //    da.Fill(ds);

        //    cnn.CloseConnection();

        //    return ds;
        //}


        public int GetTotalBudgetHours(int budgetID, string wbs)
        {
            SqlDataReader dr;
            RSLib.CDbConnection cnn;
            SqlCommand cmd;
            SqlParameter prm;
            int totalHours = 0;

            cnn = new RSLib.CDbConnection();
            cmd = new SqlCommand("spBudget_GetTotalHoursByID", cnn.GetConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            prm = cmd.Parameters.Add("@BudgetID", SqlDbType.Int);
            prm.Value = budgetID;
            prm = cmd.Parameters.Add("@WBS", SqlDbType.VarChar, 5);
            prm.Value = wbs;

            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                totalHours = Convert.ToInt32(dr["TotalHours"]);
            }

            dr.Close();
            cnn.CloseConnection();
            cmd = null;

            return totalHours;
        }


        public decimal GetTotalBudgetHourDollars(int budgetID, string wbs)
        {
            SqlDataReader dr;
            RSLib.CDbConnection cnn;
            SqlCommand cmd;
            SqlParameter prm;
            decimal totalDollars = 0;

            cnn = new RSLib.CDbConnection();
            cmd = new SqlCommand("spBudget_GetTotalHourDollarsByID", cnn.GetConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            prm = cmd.Parameters.Add("@BudgetID", SqlDbType.Int);
            prm.Value = budgetID;
            prm = cmd.Parameters.Add("@WBS", SqlDbType.VarChar, 5);
            prm.Value = wbs;

            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                totalDollars = Convert.ToDecimal(dr["TotalDollars"]);
            }

            dr.Close();
            cnn.CloseConnection();
            cmd = null;

            return totalDollars;
        }

        public decimal GetTotalBudgetExpenses(int budgetID)
        {
            SqlDataReader dr;
            RSLib.CDbConnection cnn;
            SqlCommand cmd;
            SqlParameter prm;
            decimal totalDollars = 0;

            cnn = new RSLib.CDbConnection();
            cmd = new SqlCommand("spBudget_GetTotalExpensesByID", cnn.GetConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            prm = cmd.Parameters.Add("@BudgetID", SqlDbType.Int);
            prm.Value = budgetID;

            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                totalDollars = Convert.ToDecimal(dr["TotalDollars"]);
            }

            dr.Close();
            cnn.CloseConnection();
            cmd = null;

            return totalDollars;
        }

        public decimal GetContingencyForBudget(int budgetID)
        {
            SqlDataReader dr;
            RSLib.CDbConnection cnn;
            SqlCommand cmd;
            SqlParameter prm;
            decimal cont = 0;

            cnn = new RSLib.CDbConnection();
            cmd = new SqlCommand("spBudget_GetContingencyByID", cnn.GetConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            prm = cmd.Parameters.Add("@BudgetID", SqlDbType.Int);
            prm.Value = budgetID;

            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                cont = Convert.ToDecimal(dr["Contingency"]);
            }

            dr.Close();
            cnn.CloseConnection();
            cmd = null;

            return cont;
        }

        public DataSet GetBudgetDetailsForReport(int budgetID, string wbs)
        {
            RSLib.CDbConnection cnn;
            SqlCommand cmd;
            SqlParameter prm;
            SqlDataAdapter da;
            DataSet ds;

            cnn = new RSLib.CDbConnection();
            cmd = new SqlCommand("spRPRT_BudgetDetail", cnn.GetConnection());
            cmd.CommandType = CommandType.StoredProcedure;

            prm = cmd.Parameters.Add("@BudgetID", SqlDbType.Int);
            prm.Value = budgetID;
            prm = cmd.Parameters.Add("@WBS", SqlDbType.VarChar, 5);
            prm.Value = wbs;

            da = new SqlDataAdapter();
            da.SelectCommand = cmd;

            ds = new DataSet();
            da.Fill(ds);

            cnn.CloseConnection();

            return ds;
        }

        public DataSet GetBudgetJobstatForReport(int budgetID, string wbs)
        {
            RSLib.CDbConnection cnn;
            SqlCommand cmd;
            SqlParameter prm;
            SqlDataAdapter da;
            DataSet ds;

            cnn = new RSLib.CDbConnection();
            cmd = new SqlCommand("spRPRT_BudgetJobstat", cnn.GetConnection());
            cmd.CommandType = CommandType.StoredProcedure;

            prm = cmd.Parameters.Add("@BudgetID", SqlDbType.Int);
            prm.Value = budgetID;
            prm = cmd.Parameters.Add("@WBS", SqlDbType.VarChar, 5);
            prm.Value = wbs;

            da = new SqlDataAdapter();
            da.SelectCommand = cmd;

            ds = new DataSet();
            da.Fill(ds);

            cnn.CloseConnection();

            return ds;
        }

        public DataSet GetBudgetAccountingEntryForReport(int budgetID, string wbs)
        {
            RSLib.CDbConnection cnn;
            SqlCommand cmd;
            SqlParameter prm;
            SqlDataAdapter da;
            DataSet ds;

            cnn = new RSLib.CDbConnection();
            cmd = new SqlCommand("spRPRT_BudgetAccountEntrys", cnn.GetConnection());
            cmd.CommandType = CommandType.StoredProcedure;

            prm = cmd.Parameters.Add("@BudgetID", SqlDbType.Int);
            prm.Value = budgetID;
            prm = cmd.Parameters.Add("@WBS", SqlDbType.VarChar, 5);
            prm.Value = wbs;

            da = new SqlDataAdapter();
            da.SelectCommand = cmd;

            ds = new DataSet();
            da.Fill(ds);

            cnn.CloseConnection();

            return ds;
        }

        public void CreateBudgetInJobStat(int budID)
        {
            RSLib.CDbConnection cnn;
            SqlCommand cmd;
            SqlParameter prm;

            cnn = new RSLib.CDbConnection();
            cmd = new SqlCommand("spJobStatUseBudgetToCreate", cnn.GetConnection());
            cmd.CommandTimeout = 180;
            cmd.CommandType = CommandType.StoredProcedure;
            prm = cmd.Parameters.Add("@UseBudgetID", SqlDbType.Int);
            prm.Value = budID;

            cmd.ExecuteNonQuery();

            cnn.CloseConnection();
            cmd = null;
        }

        public void CopyBudgetToProject(int budID, int newProjID, int rev)
        {
            RSLib.CDbConnection cnn;
            SqlCommand cmd;
            SqlParameter prm;

            cnn = new RSLib.CDbConnection();
            cmd = new SqlCommand("spBudget_CopyBudgetToProject", cnn.GetConnection());
            cmd.CommandType = CommandType.StoredProcedure;

            prm = cmd.Parameters.Add("@BudgetID", SqlDbType.Int);
            prm.Value = budID;
            prm = cmd.Parameters.Add("@ProjectToID", SqlDbType.Int);
            prm.Value = newProjID;
            prm = cmd.Parameters.Add("@Revision", SqlDbType.Int);
            prm.Value = rev;

            cmd.ExecuteNonQuery();

            cnn.CloseConnection();
            cmd = null;
        }
    }
}
