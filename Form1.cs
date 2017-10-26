using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;

namespace DrSignalUpdate
{
    public partial class Form1 : Form
    {

        public Dictionary<string, Dictionary<string, Dictionary<string, XQSignal>>> dicXQSignal = new Dictionary<string, Dictionary<string, Dictionary<string, XQSignal>>>();
        public DateTime dtStart;
        Microsoft.Office.Interop.Excel.Application xlapp;

        public Form1()
        {
            InitializeComponent();
            GetSignalPath();
            dtStart = DateTime.Now;
            tmAutoRun.Start();
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            tmAutoRun.Stop();
            GetAllSignals();
            UpdateXQList();
            GenerateTodayResult();
        }

        private void GetSignalPath()
        {
            string path = Application.StartupPath + @"\param.txt";
            if (File.Exists(path))
            {
                StreamReader sr = new StreamReader(path, System.Text.Encoding.GetEncoding("big5"));
                string tmp = sr.ReadLine();
                if (tmp.Trim() != "")
                    txbPath.Text = tmp.Trim();
                sr.Close();
            }
            else
            {
                txbPath.Text = @"D:\博士訊號";
                btnUpdatePath_Click(null, null);
            }
        }

        private void SetSignalPath()
        {
            string path = Application.StartupPath + @"\param.txt";
            StreamWriter sw = new StreamWriter(path);
            sw.WriteLine(txbPath.Text.Trim());
            sw.Close();
        }
        //取得所有選股策略
        private void GetAllSignals()
        {
            
            lbSignals.Items.Clear();
            dicXQSignal.Clear();
            if (!Directory.Exists(txbPath.Text.Trim()))
                return;
            string[] dirs = Directory.GetDirectories(txbPath.Text.Trim());
            System.Collections.ArrayList dirlist = new System.Collections.ArrayList();/*用來儲存只有目錄名的集合*/

            foreach (string item in dirs)
            {
                if (item.Trim() == "RTS") //RTS送單訊號
                    continue;
                dirlist.Add(Path.GetFileNameWithoutExtension(item));//走訪每個元素只取得目錄名稱(不含路徑)並加入dirlist集合中
            }

            foreach (var item in dirlist)
            {//顯示目錄名稱(不含路徑)
                string strategy = item.ToString();
                lbSignals.Items.Add(strategy);
                if (!dicXQSignal.ContainsKey(strategy))
                    dicXQSignal.Add(strategy, new Dictionary<string, Dictionary<string, XQSignal>>());
            }
        }

       
        //更新XQ訊號
        private void UpdateXQList()
        {
            foreach (var item in lbSignals.Items)
            {//顯示目錄名稱(不含路徑)
                string dir = txbPath.Text.Trim() + @"\" + item.ToString();
                string strategy = item.ToString();
                string[] files = Directory.GetFileSystemEntries(dir, "*.csv");
                
                foreach (string f in files)
                {
                    string fn = f.Replace(dir + @"\", "");
                    if (fn.Substring(0, 2) != strategy)
                        continue;
                    string sDate = fn.Substring(2, fn.Length - 2).Replace(".csv", "");
                    DateTime d;
                    if (!DateTime.TryParseExact(sDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture,System.Globalization.DateTimeStyles.None, out d))
                    {
                        continue;
                    }
                    StreamReader sr = new StreamReader(f, Encoding.Default);
                    string line;
                    int n = 0;
                    int t = 0;
                    while ((line = sr.ReadLine()) != null)
                    {
                        n++;
                        if (n <= 3) continue;
                        string[] tmp = line.ToString().Split(',');
                        if (tmp.Length < 30)
                            continue;
                        XQSignal xq = new XQSignal();
                        xq.ProdCode = tmp[1].Replace(".TW", "");
                        xq.ProdCode = xq.ProdCode.Replace(".US", "");
                        if (tmp.Length == 31 && tmp[30].Trim()!="" && !int.TryParse(tmp[3], out t))
                        {
                            xq.BuyTarget = int.Parse(tmp[7]);
                            xq.SellTarget = int.Parse(tmp[8]);
                        }
                        else
                        {
                            xq.BuyTarget = int.Parse(tmp[6]);
                            xq.SellTarget = int.Parse(tmp[7]);
                            
                        }
                        xq.StategyName = strategy;
                        xq.CalculateDate = d;
                        if (!dicXQSignal.ContainsKey(xq.StategyName))
                            dicXQSignal.Add(xq.StategyName, new Dictionary<string, Dictionary<string, XQSignal>>());
                        if (!dicXQSignal[xq.StategyName].ContainsKey(xq.CalculateDate.ToString("yyyyMMdd")))
                            dicXQSignal[xq.StategyName].Add(xq.CalculateDate.ToString("yyyyMMdd"), new Dictionary<string, XQSignal>());
                        if (!dicXQSignal[xq.StategyName][xq.CalculateDate.ToString("yyyyMMdd")].ContainsKey(xq.ProdCode))
                            dicXQSignal[xq.StategyName][xq.CalculateDate.ToString("yyyyMMdd")].Add(xq.ProdCode, xq);
                        else
                            dicXQSignal[xq.StategyName][xq.CalculateDate.ToString("yyyyMMdd")][xq.ProdCode] = xq;
                     }
                    sr.Close();
                }
            }
        }

        //產生當天結果
        private void GenerateTodayResult()
        {
            lblMessage.Text = "完成:";
            xlapp = new Excel.Application();
            xlapp.DisplayAlerts = false;
            foreach (var item in lbSignals.Items)
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                string path = txbPath.Text.Trim() + @"\" + item.ToString() + @"\MonitorIndex.csv";
                string strategy = item.ToString();
                if (!File.Exists(path)) continue;
                StreamReader sr = new StreamReader(path, Encoding.Default);
                string line;
                int n = 0;
                sr.ReadLine(); //標題
                dic.Clear();
                while ((line = sr.ReadLine()) != null)
                {
                    string[] tmp = line.Split(',');
                    dic.Add(tmp[1].ToString(), tmp[0].ToString());
                }
                sr.Close();
                List<int> lstDate = new List<int>();
                foreach (string s in dicXQSignal[strategy].Keys.ToArray())
                {
                    lstDate.Add(int.Parse(s));
                }
                lstDate.Sort();
                lstDate.Reverse();
                string savepath = txbPath.Text.Trim() + @"\" + item.ToString() + @"\" + item.ToString() + "Record.xlsx";

                Excel.Workbook wb = xlapp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
                Excel.Worksheet ws = (Excel.Worksheet)wb.Worksheets[1];
                Excel.Range rng = ws.UsedRange;
                rng.Cells[1, 1].Value2 = "名稱";
                rng.Cells[1, 2].Value2 = "代碼";
                int colidx = 3;
                foreach (int i in lstDate)
                {
                    rng.Cells[1, colidx].Value2 = i.ToString() + "買/賣";
                    colidx++;
                }
                
                int rowidx = 2;
                foreach (string s in dic.Keys.ToArray())
                {
                    rng.Cells[rowidx, 1].Value2 = dic[s];
                    rng.Cells[rowidx, 2].Value2 = s;
                    colidx = 3;
                    foreach (int i in lstDate)
                    {
                        string sdt = i.ToString();
                        if (dicXQSignal.ContainsKey(strategy) && dicXQSignal[strategy].ContainsKey(sdt) && dicXQSignal[strategy][sdt].ContainsKey(s))
                        {
                            if (dicXQSignal[strategy][sdt][s].BuyTarget > 0)
                            {
                                rng.Cells[rowidx, colidx].Value2 = dicXQSignal[strategy][sdt][s].BuyTarget.ToString();
                                rng.Cells[rowidx, colidx].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Green);
                            }
                            else if (dicXQSignal[strategy][sdt][s].SellTarget < 0)
                            {
                                rng.Cells[rowidx, colidx].Value2 = dicXQSignal[strategy][sdt][s].SellTarget.ToString();
                                rng.Cells[rowidx, colidx].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                            }
                            else
                                rng.Cells[rowidx, colidx].Value2 = "0";
                            colidx++;
                        }
                        else
                        {
                            rng.Cells[rowidx, colidx].Value2 = "0";
                            colidx++;
                        }
                    }
                    rowidx++;
                }
                ws.Cells.EntireColumn.AutoFit();
                ws.Cells.EntireRow.AutoFit();
                if (System.IO.File.Exists(savepath))
                    System.IO.File.Delete(savepath);
                wb.SaveAs(savepath, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                wb.Close(false, Type.Missing, Type.Missing);
                Marshal.ReleaseComObject(ws);
                Marshal.ReleaseComObject(wb);
                if (lblMessage.Text != "完成:")
                    lblMessage.Text += ",";
                lblMessage.Text += item;
                lblMessage.Invalidate();
            }
            xlapp.Visible = false;
            xlapp.Quit();
            Marshal.ReleaseComObject(xlapp);
            if (!ckbAutoClose.Checked)
                MessageBox.Show("XQ訊號輸出完成");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!ckbAutoRun.Checked)
                return;
            DateTime t = DateTime.Now;
            int i = t.CompareTo(dtStart.AddSeconds(1));
            if (t.CompareTo(dtStart.AddSeconds(1)) > 0)
            {
                tmAutoRun.Stop();
                this.Cursor = Cursors.WaitCursor;
                btnUpdate_Click(null, null);
                this.Cursor = Cursors.Default;
                if (ckbAutoClose.Checked)
                    this.Close();
            }
        }

        private void btnUpdatePath_Click(object sender, EventArgs e)
        {
            if (txbPath.Text.Trim() == "") return;
            StreamWriter sw = new StreamWriter(Application.StartupPath+@"\param.txt",false, System.Text.Encoding.GetEncoding("big5"));
            sw.WriteLine(txbPath.Text.Trim());
            sw.Close();
            if (!ckbAutoClose.Checked)
                MessageBox.Show("已更換路徑為"+txbPath.Text.Trim());
        }
    }

    public class XQSignal
    {
        public string StategyName;
        public string ProdCode;
        public DateTime CalculateDate;
        public int BuyTarget;
        public int SellTarget;
        public XQSignal()
        {
            StategyName = ProdCode = "";
            BuyTarget = SellTarget = 0;
            CalculateDate = new DateTime(2000, 1, 1);
        }
        
    }
}
