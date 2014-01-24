using System;
using System.Data.OracleClient;
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
            try
            {
                string constr = GetSqlServerConnectionString();

                using (OracleConnection conn = new OracleConnection(constr))
                {
                    conn.Open();

                    // Get the names of all DBs in the database server.
                    OracleCommand query = new OracleCommand(@"select table_name, num_rows from user_tables order by num_rows", conn);
                    using (OracleDataReader reader = query.ExecuteReader())
                    {
                        cboDatabases.Items.Clear();
                        while (reader.Read())
                            cboDatabases.Items.Add(reader[0] +"("+ reader[1]+")");
                        if (cboDatabases.Items.Count > 0)
                        {
                            cboDatabases.Enabled = true;
                            cboDatabases.SelectedIndex = 0;
                        }
                    } // using
                } // using

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
            this.Text = "SQL Server To SQLite DB Converter (" + version + ")";
        }

		private void txtSqlAddress_TextChanged(object sender, EventArgs e)
        {
            UpdateSensitivity();
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
            SqlTableSelectionHandler selectionHandler = new SqlTableSelectionHandler(delegate(List<string> schema, List<int> numRows)
            {
                List<string> updated = null;
                Invoke(new MethodInvoker(delegate
                {
                    // Allow the user to select which tables to include by showing him the 
                    // table selection dialog.
                    TableSelectionDialog dlg = new TableSelectionDialog();
                    DialogResult res = dlg.ShowTables(schema, numRows,  this);
                    if (res == DialogResult.OK)
                        updated = dlg.IncludedTables;
                }));
                return updated;
            });

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
            if (!cbxEncrypt.Checked)
                password = null;
            OracleToSQLite.ConvertOracleToSQLiteDatabase(sqlConnString, sqlitePath, password, handler, 
                selectionHandler, viewFailureHandler, cbxTriggers.Checked, createViews);
        }

        #endregion

        #region Private Methods
        private void UpdateSensitivity()
        {
            if (txtSQLitePath.Text.Trim().Length > 0 && cboDatabases.Enabled &&
                (!cbxEncrypt.Checked || txtPassword.Text.Trim().Length > 0))
                btnStart.Enabled = true && !OracleToSQLite.IsActive;
            else
                btnStart.Enabled = false;

            btnCancel.Visible = OracleToSQLite.IsActive;
            txtSQLitePath.Enabled = !OracleToSQLite.IsActive;
            btnBrowseSQLitePath.Enabled = !OracleToSQLite.IsActive;
            cbxEncrypt.Enabled = !OracleToSQLite.IsActive;
            cboDatabases.Enabled = cboDatabases.Items.Count > 0 && !OracleToSQLite.IsActive;
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
    }
}