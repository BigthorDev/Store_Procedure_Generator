using SP_DAO_Generator.Data;
using SP_DAO_Generator.DbAccess;
using SP_DAO_Generator.Models;
using SP_DAO_Generator.Utils;
using System.Configuration;

namespace SP_DAO_Generator
{
    public partial class Form1 : Form
    {
        private IDataAccess _dataAccess;
        private ISchemaDAO _schemaDAO;
        public Form1()
        {
            _dataAccess = new SqlDataAccess();
            _schemaDAO = new SchemaDAO(_dataAccess);
            InitializeComponent();
            
        }

        private async void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                if (StringUtil.IsNullOrEmpty(txtBasePath.Text))
                    throw new Exception("Base path is required");

                await CheckMainDirectories();
                if (listTables.SelectedItems.Count > 0)
                {
                    foreach (var selectedTable in listTables.SelectedItems)
                    {
                        string tableName = selectedTable as string;

                        if (chkModels.Checked)
                        {
                            if (StringUtil.IsNullOrEmpty(txtNamespace.Text))
                                throw new Exception("You must add a namespace");

                            ModelGenerator modelGenerator = new ModelGenerator(_dataAccess);
                            await modelGenerator.Generate(tableName, txtBasePath.Text, txtNamespace.Text);
                        }

                        if (chkSPs.Checked)
                        {
                            SpGenerator spGenerator = new SpGenerator(_dataAccess,tableName, txtBasePath.Text);
                            spGenerator.Generate();
                        }

                        if (chkDAOs.Checked)
                        {
                            if (StringUtil.IsNullOrEmpty(txtNamespace.Text))
                                throw new Exception("You must add a namespace");
                        }
                            
                    }
                }
                else
                    throw new Exception("No Table(s) selected");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtBasePath.Text = ConfigUtil.ReadSetting("basePath");
            TableColumnModel[] tables = _schemaDAO.GetTables().ToArray();
            this.listTables.Items.Clear();
            foreach (var table in tables)
            { 
                listTables.Items.Add(table.TableName);
            }
        }

        private async Task CheckMainDirectories()
        {
            string[] dirs = ConfigUtil.ReadSetting("ComponentDirectories").Split(',');

            if (dirs.Length == 0)
                throw new Exception("Please set up your component directories in the App.Config file");

            if(!Directory.Exists(txtBasePath.Text))
                Directory.CreateDirectory(txtBasePath.Text);

            foreach (string dir in dirs)
            {
                string folderPath = txtBasePath.Text + '/' + dir;
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);
            }

        }
    }
}