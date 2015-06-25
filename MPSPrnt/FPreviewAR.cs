using RSMPS.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;



using GrapeCity.ActiveReports;



using DataDynamics.ActiveReports;

namespace RSMPS
{
    public partial class FPreviewAR : Form
    {
        private SectionReport rprt;

        public  string projNumber;
        public string reportType;
        
        public FPreviewAR()
        {
            InitializeComponent();
                                     
        }

        public void ViewReport(SectionReport ar)
        {
           reportType = ar.GetType().ToString();
            MessageBox.Show(reportType);

            rprt = ar;
            viewer1.Document = rprt.Document;
            rprt.Run();
        }

        public void ViewReportNoRun(SectionReport ar)
        {
            rprt = ar;
            viewer1.Document = rprt.Document;
        }

        public void ViewReportWithExcel(SectionReport ar)
        {
            rprt = ar;
            viewer1.Document = rprt.Document;
            rprt.Run();
        }

        public void ViewDrawingLogWithExcel(SectionReport ar)
        {
            rprt = ar;
            viewer1.Document = rprt.Document;
            rprt.Run();
        }
       
        private void SendForcastReportToExcel()
        {
        //    GrapeCity.ActiveReports.Export.Excel.Section.XlsExport xlExport;
        //    xlExport = new GrapeCity.ActiveReports.Export.Excel.Section.XlsExport();

        //    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
        //    {
        //        this.Cursor = Cursors.WaitCursor;

        //        xlExport.Export(viewer1.Document, saveFileDialog1.FileName);

        //        this.Cursor = Cursors.Default;
        //    }
        }
        private void SendForcastReportToExcelOld()
        {
        //     if (saveFileDialog1.ShowDialog() == DialogResult.OK)
        //    {
        //        this.Cursor = Cursors.WaitCursor;

        //        C1.C1Excel.C1XLBook book = new C1.C1Excel.C1XLBook();
        //        C1.C1Excel.XLSheet sheet = book.Sheets[0];
        //        int indx = 0;
        //        int ftc1000, ftc2000, ftc3000, ftc4000, ftc5000, ftc6000;
        //        int ftcTotVal;

        //        sheet[indx, 0].Value = "Job #";
        //        sheet[indx, 1].Value = "Client";
        //        sheet[indx, 2].Value = "Description";
        //        sheet[indx, 3].Value = "Admin/PM";
        //        sheet[indx, 4].Value = "Proc";
        //        sheet[indx, 5].Value = "M/P";
        //        sheet[indx, 6].Value = "CSA";
        //        sheet[indx, 7].Value = "E&I";
        //        sheet[indx, 8].Value = "Spclst";
        //        sheet[indx, 9].Value = "Total";
        //        sheet[indx, 10].Value = "Status";

        //        indx++;

        //        foreach (DataRow dr in ((DataSet)rprt.DataSource).Tables[0].Rows)
        //        {
                  
        //            ftc1000 = Convert.ToInt32(dr["ftc1000"]);
        //            ftc2000 = Convert.ToInt32(dr["ftc2000"]);
        //            ftc3000 = Convert.ToInt32(dr["ftc3000"]);
        //            ftc4000 = Convert.ToInt32(dr["ftc4000"]);
        //            ftc5000 = Convert.ToInt32(dr["ftc5000"]);
        //            ftc6000 = Convert.ToInt32(dr["ftc6000"]);
        //            ftcTotVal = ftc1000 + ftc2000 + ftc3000 + ftc4000 + ftc5000 + ftc6000;

        //            sheet[indx, 0].Value = dr["Number"].ToString();
        //            sheet[indx, 1].Value = dr["Customer"].ToString();
        //            sheet[indx, 2].Value = dr["Description"].ToString();
        //            sheet[indx, 3].Value = ftc1000;
        //            sheet[indx, 4].Value = ftc2000;
        //            sheet[indx, 5].Value = ftc3000;
        //            sheet[indx, 6].Value = ftc4000;
        //            sheet[indx, 7].Value = ftc5000;
        //            sheet[indx, 8].Value = ftc6000;
        //            sheet[indx, 9].Value = ftcTotVal;
        //            sheet[indx, 10].Value = dr["Status"].ToString();

        //            indx++;
        //        }

        //        book.Save(saveFileDialog1.FileName);

        //        this.Cursor = Cursors.Default;
        //    }
        }

        private void SendDrawingLogToExcel()
        {
        //    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
        //    {
        //        this.Cursor = Cursors.WaitCursor;

        //        C1.C1Excel.C1XLBook book = new C1.C1Excel.C1XLBook();
        //        C1.C1Excel.XLSheet sheet = book.Sheets[0];
        //        int indx = 0;
        //        string hgaNumber, cadNumber, title, revision, issuedate, issuefor, transmittalnumber;
        //        //SSS 201131126 uncommented the comment
        //        //string cadNumber, title, revision, issuedate, issuefor, transmittalnumber;
        //        DataSet dsLoc;
        //        DataView dvLoc;

        //        sheet[indx, 0].Value = "CADNumber";
        //        sheet[indx, 1].Value = "Title";
        //        sheet[indx, 2].Value = "Revision";
        //        sheet[indx, 3].Value = "IssueDate";
        //        sheet[indx, 4].Value = "IssuedFor";
        //        sheet[indx, 5].Value = "TransmittalNumber";

        //        indx++;

        //        dsLoc = (dsDrawingLog)rprt.DataSource;
        //        dvLoc = new DataView(dsLoc.Tables["Revisions"]);
        //        dvLoc.Sort = "IssuedDate";

        //        foreach (DataRow dr in dsLoc.Tables["DrawingList"].Rows)
        //        {
        //            dvLoc.RowFilter = "DrawingID = " + dr["DrawingID"].ToString();

        //            if (dvLoc.Count > 0)
        //            {
        //                for (int i = 0; i < dvLoc.Count; i++)
        //                {
        //                    DataRowView d = dvLoc[i];

        //                    cadNumber = dr["CADNumber"].ToString();
        //                    title = dr["Title1"].ToString();
        //                    revision = d["RevisionNumber"].ToString();
        //                    issuedate = DateToOutput(Convert.ToDateTime(d["IssuedDate"]));
        //                    issuefor = d["IssuedFor"].ToString();
        //                    transmittalnumber = d["TransmittalNumber"].ToString();

        //                    sheet[indx, 0].Value = cadNumber;
        //                    sheet[indx, 1].Value = title;
        //                    sheet[indx, 2].Value = revision;
        //                    sheet[indx, 3].Value = issuedate;
        //                    sheet[indx, 4].Value = issuefor;
        //                    sheet[indx, 5].Value = transmittalnumber;

        //                    indx++;
        //                }
        //            }
        //            else
        //            {
        //                cadNumber = dr["CADNumber"].ToString();
        //                hgaNumber = dr["DrawingID"].ToString();
        //                title = dr["Title1"].ToString();
        //                revision = "";
        //                issuedate = "";
        //                issuefor = "";
        //                transmittalnumber = "";

        //                sheet[indx, 0].Value = cadNumber;
        //                sheet[indx, 1].Value = title;
        //                sheet[indx, 2].Value = revision;
        //                sheet[indx, 3].Value = issuedate;
        //                sheet[indx, 4].Value = issuefor;
        //                sheet[indx, 5].Value = transmittalnumber;

        //                indx++;
        //            }
        //        }

        //        book.Save(saveFileDialog1.FileName);

        //        this.Cursor = Cursors.Default;
        //    }
        }

        private string DateToOutput(DateTime dOut)
        {
            string retVal = "";

            if (dOut < new DateTime(2000, 1, 1))
                retVal = "";
            else
                retVal = dOut.ToShortDateString();

            return retVal;
        }
        const long pdfExportToolID = 42;
        private void FPreviewAR_Load(object sender, EventArgs e)
        {
           // var zoomOutButton = viewer1.TouchModeToolbar.ToolStrip.Items[8];
            //zoomOutButton.Visible = true;

        }

        private void c1Button1_Click(object sender, EventArgs e)
        {
            GrapeCity.ActiveReports.Export.Pdf.Section.PdfExport PDFEx = new GrapeCity.ActiveReports.Export.Pdf.Section.PdfExport();


            FPreviewAR pv = new FPreviewAR();
            rprtBudgetDetail rprt = new rprtBudgetDetail();
            DataSet ds;
            CBBudget bud = new CBBudget();
            CBProject proj = new CBProject();
            CBCustomer cust = new CBCustomer();
            CBLocation loc = new CBLocation();
            CBState state = new CBState();

            //bud.Load(budID);
            proj.Load(bud.ProjectID);
            cust.Load(proj.CustomerID);
            loc.Load(proj.LocationID);
            state.Load(loc.StateID);
           
            //MessageBox.Show(proj.Number);
            MessageBox.Show("**********************************************");
            MessageBox.Show(projNumber);

           // string fileName = "Proposal Budget- "  + projNumber;

           DateTime dt = DateTime.Now;
            string fileName ;
            if(reportType == "RSMPS.rprtBudgetDetail")

             fileName = "Proposal Budget- " + projNumber  +" " + dt.ToString("yyyMMdd hhmmss"); 
            else //if(reportType == "RSMPS.rprtPCIInformation")

                 fileName = "Budget PCN- " + projNumber  +" " + dt.ToString("yyyMMdd hhmmss"); 

            MessageBox.Show(fileName);

            SaveFileDialog sv1 = new SaveFileDialog();
            // sv1.InitialDirectory = "c:\\MZ\\";
            sv1.InitialDirectory = "v:\\HGA\\";

            sv1.FileName = fileName;
            sv1.Filter = "PDF Files | *.pdf";
            sv1.DefaultExt = "pdf";

           


            if (sv1.ShowDialog() == DialogResult.OK)
            {
                viewer1.Export(PDFEx, new System.IO.FileInfo(sv1.FileName));
                //PDFEx.Export(rprt.Document, sv1.FileName);


            }


        }
    }
}