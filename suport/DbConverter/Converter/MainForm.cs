using System;
using System.Data.OracleClient;
using System.Data.SQLite;
using System.Reflection;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;
using System.IO;
using DbAccess;

namespace Converter
{
    public partial class MainForm : Form
    {
        #region Constructor
        public MainForm()
        {
            InitializeComponent();
        }
        #endregion

        #region Event Handler
        private void btnBrowseSQLitePath_Click(object sender, EventArgs e)
        {
            DialogResult res = saveFileDialog1.ShowDialog(this);
            if (res == DialogResult.Cancel)
                return;

            string fpath = saveFileDialog1.FileName;
            txtSQLitePath.Text = fpath;
            pbrProgress.Value = 0;
            lblMessage.Text = string.Empty;
        }

        private void cboDatabases_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSensitivity();
            pbrProgress.Value = 0;
            lblMessage.Text = string.Empty;
        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            try
            {
                string constr = GetSqlServerConnectionString();

                using (OracleConnection conn = new OracleConnection(constr))
                {
                    conn.Open();

                    // Get the names of all DBs in the database server.
                    string sql = "select t1.table_name, t2.comments, t1.num_rows, t3.timestamp from user_tables t1 "
                                +" left join user_tab_comments t2  on  t1.table_name = t2.table_name "
                                +" left join user_objects t3 on t1.table_name = t3.object_name  and t3.object_type = 'TABLE'"
                                +" order by num_rows  ";
                    OracleCommand query = new OracleCommand(sql, conn);
                    using (OracleDataReader reader = query.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            dataGridView1.Rows.Add(true, reader[0], reader[1], reader[2], reader[3]);
                        }
                    } // using
                } // using


                string sqliteConnString = OracleToSQLite.CreateSQLiteConnectionString(txtSQLitePath.Text, null);
                using (SQLiteConnection conn = new SQLiteConnection(sqliteConnString))
                {
                    conn.Open();
                    // Get the names of all DBs in the database server.
                    string sql = "select name from sqlite_master where type= 'table'; ";
                    SQLiteCommand query = new SQLiteCommand(sql, conn);
                    using (SQLiteDataReader reader = query.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            foreach (DataGridViewRow row in dataGridView1.Rows)
                            {
                                if (row.Cells[1].Value.ToString() == reader[0].ToString())
                                {
                                    row.Cells[5].Value = "Existed";
                                    row.Cells[0].Value = false;
                                }
                            }
                        }
                    } // using
                }

                pbrProgress.Value = 0;
                lblMessage.Text = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this,
                    ex.Message,
                    "Failed To Connect",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            } // catch
        }

        private void txtSQLitePath_TextChanged(object sender, EventArgs e)
        {
            UpdateSensitivity();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            UpdateSensitivity();

            string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            this.Text = "Oracle To SQLite DB Converter (" + version + ")";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            OracleToSQLite.CancelConversion();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (OracleToSQLite.IsActive)
            {
                OracleToSQLite.CancelConversion();
                _shouldExit = true;
                e.Cancel = true;
            }
            else
                e.Cancel = false;
        }

        private void cbxEncrypt_CheckedChanged(object sender, EventArgs e)
        {
            UpdateSensitivity();
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            UpdateSensitivity();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            string sqlConnString = GetSqlServerConnectionString();

            bool createViews = cbxCreateViews.Checked;
        	
            string sqlitePath = txtSQLitePath.Text.Trim();
            this.Cursor = Cursors.WaitCursor;
            SqlConversionHandler handler = new SqlConversionHandler(delegate(bool done,
                bool success, int percent, string msg) {
                    Invoke(new MethodInvoker(delegate() {
                        UpdateSensitivity();
                        lblMessage.Text = msg;
                        pbrProgress.Value = percent;

                        if (done)
                        {
                            btnStart.Enabled = true;
                            this.Cursor = Cursors.Default;
                            UpdateSensitivity();

                            if (success)
                            {
                                MessageBox.Show(this,
                                    msg,
                                    "Conversion Finished",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                                pbrProgress.Value = 0;
                                lblMessage.Text = string.Empty;
                            }
                            else
                            {
                                if (!_shouldExit)
                                {
                                    MessageBox.Show(this,
                                        msg,
                                        "Conversion Failed",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);
                                    pbrProgress.Value = 0;
                                    lblMessage.Text = string.Empty;
                                }
                                else
                                    Application.Exit();
                            }
                        }
                    }));
            });
//            SqlTableSelectionHandler selectionHandler = new SqlTableSelectionHandler(delegate(List<string> schema, List<int> numRows)
//            {
//                return IncludedTables;
//            });

            FailedViewDefinitionHandler viewFailureHandler = new FailedViewDefinitionHandler(delegate(ViewSchema vs)
            {
                string updated = null;
                Invoke(new MethodInvoker(delegate
                {
                    ViewFailureDialog dlg = new ViewFailureDialog();
                    dlg.View = vs;
                    DialogResult res = dlg.ShowDialog(this);
                    if (res == DialogResult.OK)
                        updated = dlg.ViewSQL;
                    else
                        updated = null;
                }));

                return updated;
            });

            string password = txtPassword.Text.Trim();
            if (!cbxEncrypt.Checked) password = null;

            List<string> includedTables = new List<string>();
            List<string> existedTables = new List<string>();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                bool include = (bool)row.Cells[0].Value;
                if (include)
                {
                    includedTables.Add((string)row.Cells[1].Value);
                    if ((string)row.Cells[5].Value == "Existed") existedTables.Add((string)row.Cells[1].Value);
                }
            } // foreach

            OracleToSQLite.ConvertOracleToSQLiteDatabase(sqlConnString, sqlitePath, password, handler,
                includedTables, existedTables, viewFailureHandler, cbxTriggers.Checked, createViews);
        }

        #endregion

        #region Private Methods
        private void UpdateSensitivity()
        {
            if (txtSQLitePath.Text.Trim().Length > 0 &&
                (!cbxEncrypt.Checked || txtPassword.Text.Trim().Length > 0))
                btnStart.Enabled = true && !OracleToSQLite.IsActive;
            else
                btnStart.Enabled = false;

            btnCancel.Visible = OracleToSQLite.IsActive;
            txtSQLitePath.Enabled = !OracleToSQLite.IsActive;
            btnBrowseSQLitePath.Enabled = !OracleToSQLite.IsActive;
            cbxEncrypt.Enabled = !OracleToSQLite.IsActive;
            txtPassword.Enabled = cbxEncrypt.Checked && cbxEncrypt.Enabled;
            cbxCreateViews.Enabled = !OracleToSQLite.IsActive;
            cbxTriggers.Enabled = !OracleToSQLite.IsActive;
        }

        private string GetSqlServerConnectionString()
        {
            string res =
                String.Format(
                    @"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST={0})(PORT={1})))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME={2})));User Id={3};Password={4}",
                    txtIp.Text, txtPort.Text, txtSid.Text, txtUserOracle.Text, txtPassOracle.Text);
            return res;
        }
        #endregion

        #region Private Variables
        private bool _shouldExit = false;
        #endregion        

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                // Check the [V] for this row.
                row.Cells[0].Value = true;
            } // foreach
        }

        private void btnDeselectAll_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                // Check the [V] for this row.
                row.Cells[0].Value = false;
            } // foreach
        }
    }
}