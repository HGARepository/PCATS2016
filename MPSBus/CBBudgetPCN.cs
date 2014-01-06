using System;
using System.Collections.Generic;
using System.Text;

using System.IO;
using System.Xml.Serialization;

using System.Data;
using System.Data.SqlClient;

namespace RSMPS
{
    public class CBBudgetPCN : COBudgetPCN
    {
        public CBBudgetPCN()
        {
            base.Clear();
        }

        public static int TotalHours(int pcnID)
        {
            CDbBudgetPCN dbDt = new CDbBudgetPCN();

            return dbDt.GetBudgetPCNHours(pcnID);
        }

        public static decimal TotalDollars(int pcnID)
        {
            CDbBudgetPCN dbDt = new CDbBudgetPCN();

            return dbDt.GetBudgetPCNDollars(pcnID);
        }

        public void Load(int iID)
        {
            CDbBudgetPCN dbDt = new CDbBudgetPCN();
            string tmpDat;

            tmpDat = dbDt.GetByID(iID);

            Clear();
            if (tmpDat.Length > 0)
                LoadVals(tmpDat);

            dbDt = null;
        }

        public void LoadWithData(int iID)
        {
            CDbBudgetPCN dbDt = new CDbBudgetPCN();
            string tmpDat;

            tmpDat = dbDt.GetByID(iID);

            Clear();
            if (tmpDat.Length > 0)
                LoadVals(tmpDat);

            dbDt = null;

            base.PCNData = new dsPCN();

            SqlDataReader dr;
            DataRow d;

            dr = CBBudgetPCNHour.GetListByPCN(iID);

            while (dr.Read())
            {
                d = base.PCNData.Tables["PCNHours"].NewRow();

                d["ID"] = dr["ID"];
                d["PCNID"] = dr["PCNID"];
                d["Code"] = dr["Code"];
                d["WBS"] = dr["WBS"];
                d["Description"] = dr["Description"];
                d["Quantity"] = dr["Quantity"];
                d["HoursPerItem"] = dr["HoursPerItem"];
                d["Rate"] = dr["Rate"];
                d["SubtotalHrs"] = dr["SubtotalHrs"];
                d["SubtotalDlrs"] = dr["SubtotalDlrs"];

                base.PCNData.Tables["PCNHours"].Rows.Add(d);
            }

            dr.Close();

            dr = CBBudgetPCNExpense.GetListByPCN(iID);

            while (dr.Read())
            {
                d = base.PCNData.Tables["PCNExpenses"].NewRow();

                d["ID"] = dr["ID"];
                d["PCNID"] = dr["PCNID"];
                d["Code"] = dr["Code"];
                d["Description"] = dr["Description"];
                d["DlrsPerItem"] = dr["DlrsPerItem"];
                d["NumItems"] = dr["NumItems"];
                d["MUPerc"] = dr["MUPerc"];
                d["MarkUp"] = dr["MarkUp"];
                d["TotalCost"] = dr["TotalCost"];

                base.PCNData.Tables["PCNExpenses"].Rows.Add(d);
            }

            dr.Close();
        }

        public void LoadVals(string strXml)
        {
            XmlSerializer s;
            StringReader sr;
            COBudgetPCN o;

            s = new XmlSerializer(typeof(COBudgetPCN));
            sr = new System.IO.StringReader(strXml);

            o = new COBudgetPCN();
            o = (COBudgetPCN)s.Deserialize(sr);

            base.LoadFromObj(o);

            o = null;
            sr.Close();
            sr = null;
            s = null;
        }

        public int Save()
        {
            CDbBudgetPCN dbDt = new CDbBudgetPCN();
            string tmpDat;
            int retVal;

            tmpDat = GetDataString();
            if (base.ID > 0)
            {
                dbDt.SavePrev(tmpDat);
                retVal = base.ID;
            }
            else
            {
                retVal = dbDt.SaveNew(tmpDat);
                base.ID = retVal;
            }

            dbDt = null;

            return retVal;
        }

        public int SaveWithData()
        {
            int retVal = Save();

            CBBudgetPCNHour hr;
            CBBudgetPCNExpense exp;

            foreach (DataRow dr in base.PCNData.PCNHours.Rows)
            {
                hr = new CBBudgetPCNHour();

                hr.ID = Convert.ToInt32(dr["ID"]);
                hr.PCNID = retVal;
                hr.Code = dr["Code"].ToString();
                hr.WBS = dr["WBS"].ToString();
                hr.Description = dr["Description"].ToString();
                hr.Quantity = Convert.ToInt32(dr["Quantity"]);
                hr.HoursPerItem = Convert.ToInt32(dr["HoursPerItem"]);
                hr.Rate = Convert.ToDecimal(dr["Rate"]);
                hr.SubtotalHrs = Convert.ToInt32(dr["SubtotalHrs"]);
                hr.SubtotalDlrs = Convert.ToDecimal(dr["SubtotalDlrs"]);

                hr.Save();

                if (Convert.ToInt32(dr["ID"]) < 1)
                {
                    dr["ID"] = hr.ID;
                    dr["PCNID"] = retVal;
                }
            }

            foreach (DataRow dr in base.PCNData.PCNHoursDeleted.Rows)
            {
                CBBudgetPCNHour.Delete(Convert.ToInt32(dr["ID"]));
            }

            foreach (DataRow dr in base.PCNData.PCNExpenses.Rows)
            {
                exp = new CBBudgetPCNExpense();

                exp.ID = Convert.ToInt32(dr["ID"]);
                exp.PCNID = retVal;
                exp.Code = dr["Code"].ToString();
                exp.Description = dr["Description"].ToString();
                exp.DlrsPerItem = Convert.ToDecimal(dr["DlrsPerItem"]);
                exp.NumItems = Convert.ToInt32(dr["NumItems"]);
                exp.MUPerc = Convert.ToDecimal(dr["MUPerc"]);
                exp.MarkUp = Convert.ToDecimal(dr["MarkUp"]);
                exp.TotalCost = Convert.ToDecimal(dr["TotalCost"]);

                exp.Save();

                if (Convert.ToInt32(dr["ID"]) < 1)
                {
                    dr["ID"] = exp.ID;
                    dr["PCNID"] = retVal;
                }
            }

            foreach (DataRow dr in base.PCNData.PCNExpensesDeleted.Rows)
            {
                CBBudgetPCNExpense.Delete(Convert.ToInt32(dr["ID"]));
            }

            return retVal;
        }

        public static void Delete(int iID)
        {
            CDbBudgetPCN dbDt = new CDbBudgetPCN();

            dbDt.Delete(iID);
        }

        public string GetDataString()
        {
            string tmpStr;
            COBudgetPCN o;
            XmlSerializer s;
            StringWriter sw;

            o = new COBudgetPCN();
            s = new XmlSerializer(typeof(COBudgetPCN));
            sw = new StringWriter();

            base.Copy(o);
            s.Serialize(sw, o);

            tmpStr = sw.ToString();

            o = null;
            s = null;
            sw = null;

            return tmpStr;
        }

        public static SqlDataReader GetList()
        {
            CDbBudgetPCN dbDt = new CDbBudgetPCN();

            return dbDt.GetList();
        }

        public static SqlDataReader GetListByProject(int projID)
        {
            CDbBudgetPCN dbDt = new CDbBudgetPCN();

            return dbDt.GetListByProject(projID);
        }

        public string GetNextPCNNumber(int projID)
        {
            CDbBudgetPCN dbDt = new CDbBudgetPCN();
            int currCount;
            string newNum;

            currCount = dbDt.GetCountByProject(projID);
            currCount++;

            newNum = Convert.ToDecimal(currCount).ToString("00");

            return newNum;
        }

        public static DataSet GetBudgetPCNInfoForReport(int pcnID)
        {
            CDbBudgetPCN dbDt = new CDbBudgetPCN();

            return dbDt.GetBudgetPCNInfoForReport(pcnID);
        }

        public static DataSet GetPCNLogByProjID(int projID)
        {
            CDbBudgetPCN db = new CDbBudgetPCN();

            return db.GetPCNLogByProjID(projID);
        }

        public static void SetClientDate(int pcnID, DateTime clientSubmitted, DateTime clientReceived, string comments)
        {
            CDbBudgetPCN db = new CDbBudgetPCN();

            db.SetClientDate(pcnID, clientSubmitted, clientReceived, comments);
        }

        public static void GetPCILogTotalByProjID(int projID, ref int hours, ref decimal estTIC, ref int mhAdd, ref decimal dlrAdd, ref decimal trend)
        {
            CDbBudgetPCN db = new CDbBudgetPCN();

            db.GetPCILogTotalByProjID(projID, ref hours, ref estTIC, ref mhAdd, ref dlrAdd, ref trend);
        }

        public static void SetCurrentStatus(int pcnID, int statusID)
        {
            CDbBudgetPCN db = new CDbBudgetPCN();

            db.SetCurrentStatus(pcnID, statusID);
        }

        public static int GetBudgetsWithPCN(int pcnID)
        {
            CDbBudgetPCN db = new CDbBudgetPCN();

            return db.GetBudgetsWithPCN(pcnID);
        }
    }
}
