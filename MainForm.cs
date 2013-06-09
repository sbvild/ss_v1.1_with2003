/*
 * Created by SharpDevelop.
 * User: s4
 * Date: 2013/06/07
 * Time: 21:28
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data;

namespace ss
{
    /// <summary>
    /// Description of MainForm.
    /// </summary>
    public class MainForm : System.Windows.Forms.Form
    {
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGrid dataGrid1;
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.MenuItem menuItem1;

        private Server[] _serverList1;
        private Server[] _serverList2;
        private SearchWord _searchWord;
        public MainForm()
        {
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            InitializeComponent();

            //Util.ResotreSettings(ref _serverList);


            Util.ResotreSettings(ref _serverList1, Server.PATH1);
            Util.ResotreSettings(ref _serverList2, Server.PATH2);
            Util.ResotreSettings(ref _searchWord);
            //Util.SaveSettings(_serverList1, Server.PATH1);
            //Util.SaveSettings(_serverList2, Server.PATH2);
            //
            // TODO: Add constructor code after the InitializeComponent() call.
            //
        }

        [STAThread]
        public static void Main(string[] args)
        {
            Application.Run(new MainForm());
        }

        #region Windows Forms Designer generated code
        /// <summary>
        /// This method is required for Windows Forms designer support.
        /// Do not change the method contents inside the source code editor. The Forms designer might
        /// not be able to load this method if it was changed manually.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.button2 = new System.Windows.Forms.Button();
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.dataGrid1 = new System.Windows.Forms.DataGrid();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 0;
            this.menuItem1.Text = "設定";
            this.menuItem1.Select += new System.EventHandler(this.MenuItem1Select);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(88, 200);
            this.button2.Name = "button2";
            this.button2.TabIndex = 1;
            this.button2.Text = "青LAN";
            this.button2.Click += new System.EventHandler(this.Button1Click);
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
						this.menuItem1});
            // 
            // dataGrid1
            // 
            this.dataGrid1.CaptionForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.dataGrid1.CaptionVisible = false;
            this.dataGrid1.DataMember = "";
            this.dataGrid1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.dataGrid1.Location = new System.Drawing.Point(0, 0);
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.Size = new System.Drawing.Size(456, 192);
            this.dataGrid1.TabIndex = 0;
            this.dataGrid1.CurrentCellChanged += new System.EventHandler(this.DataGrid1CurrentCellChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(8, 200);
            this.button1.Name = "button1";
            this.button1.TabIndex = 1;
            this.button1.Text = "白LAN";
            this.button1.Click += new System.EventHandler(this.Button1Click);
            // 
            // MainForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
            this.ClientSize = new System.Drawing.Size(444, 250);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGrid1);
            this.Controls.Add(this.button2);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(460, 288);
            this.Menu = this.mainMenu1;
            this.MinimumSize = new System.Drawing.Size(460, 288);
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "朝チェッカー";
            this.Load += new System.EventHandler(this.MainFormLoad);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
            this.ResumeLayout(false);
        }
        #endregion
        void MainFormLoad(object sender, System.EventArgs e)
        {

            DataTable dt = new DataTable("hoge");
            dt.Columns.Add("Server");
            dt.Columns.Add("StartUp");
            dt.Columns.Add("Error");
            dt.Columns.Add("WF");
            dt.Columns.Add("etc.", typeof(bool));
            dt.Columns.Add("errorDetail", typeof(string[]));

            this.dataGrid1.DataSource = dt;
            
            DataGridTableStyle ts = new DataGridTableStyle();
            ts.MappingName = "hoge";



            dataGrid1.TableStyles.Add(ts);
            dataGrid1.TableStyles["hoge"].GridColumnStyles["StartUp"].Width = 100;
            ts.RowHeadersVisible = false;
            dataGrid1.TableStyles["hoge"].GridColumnStyles["errorDetail"].Width = 0;
            DataGridTextBoxColumn cs;
            foreach (DataColumn dc in dt.Columns)
            {
                //
                if (dc.DataType == typeof(bool))
                {
                    System.Windows.Forms.DataGridBoolColumn bc = (DataGridBoolColumn)ts.GridColumnStyles[dc.ColumnName];
                    bc.NullValue = true;
                    continue;
                }


                cs = (DataGridTextBoxColumn)ts.GridColumnStyles[dc.ColumnName];
                //(Null)を変更する
                cs.NullText = "";
            }




            foreach (Server sss in this._serverList1)
            {
                //
                DataRow n = dt.NewRow();
                n["Server"] = sss.name;
                n["etc."] = false;
                dt.Rows.Add(n);
            }
            foreach (Server sss in this._serverList2)
            {
                //
                DataRow n = dt.NewRow();
                n["Server"] = sss.name;
                n["etc."] = false;
                dt.Rows.Add(n);
            }
        }

        void Button1Click(object sender, System.EventArgs e)
        {
            Server[] sList;
            if (((Button)sender).Text == "白LAN")
                sList = this._serverList1;
            else
                sList = this._serverList2;

            DataTable dt = (DataTable)this.dataGrid1.DataSource;
            System.Collections.ArrayList ret;
            SearchWord searchword = this._searchWord;
            foreach (Server server in sList)
            {

                DataRow h = dt.Select(String.Format("[Server] = '{0}'", server.name))[0];

                h["Server"] = server.name;
                h["StartUp"] = server.StartUpCheck();
                string[] errorlist = server.StartUpErrorCheck(searchword);
                h["errorDetail"] = errorlist;
                h["Error"] = errorlist.Length;

                h["WF"] = server.WfServiceLogCheck();

            }
            


        }



        void DataGrid1CurrentCellChanged(object sender, System.EventArgs e)
        {
            DataGridCell cs = this.dataGrid1.CurrentCell;
            DataTable t = (DataTable)this.dataGrid1.DataSource;

            if (t == null)
                return;

            switch (cs.ColumnNumber)
            {
                case 2:
                    //MessageBox.Show(t.Rows[cs.RowNumber][cs.ColumnNumber].GetType().ToString());

                    string s = t.Rows[cs.RowNumber][cs.ColumnNumber].ToString();

                    if (s == "0" || s == "")
                        break;

                    Form1 form1 = new Form1();
                    form1.errorDetail = (string[])t.Rows[cs.RowNumber]["errorDetail"];
                    form1.ShowDialog(this);

                    break;

            }
        }

        void MenuItem1Select(object sender, System.EventArgs e)
        {

        }

    }
}
