using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using Server_V1.DHCK_ServerDataSet2TableAdapters;

namespace Server_V1
{
    public partial class DHCKSQLServerDatabase : Form
    {
        public static int comboBoxIndex;
        public static DataSet SearchDstable = new DataSet();
        public DHCKSQLServerDatabase()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
        }

        private void DHCKSQLServerDatabase_Load(object sender, EventArgs e)
        {
            // TODO: 这行代码将数据加载到表“dHCK_ServerForSearch.报警状态记录表”中。您可以根据需要移动或删除它。
            //this.报警状态记录表TableAdapter.Fill(this.dHCK_ServerForSearch.报警状态记录表);
            // TODO: 这行代码将数据加载到表“dHCK_ServerDataSet2.报警状态记录表”中。您可以根据需要移动或删除它。
            this.TableAdapter.Fill(this.dHCK_ServerDataSet2.报警状态记录表);

            dataGridView.RowsDefaultCellStyle.ForeColor = Color.Black;

        }

        public void InsertItm()
        {
            SqlServerDatabase sqlDb = new SqlServerDatabase();
            sqlDb.SetToDefaultDatabase();
            sqlDb.ConnectToDatabase();
            //Parameter
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AlarmInfo tmp = new AlarmInfo();
            tmp.AlarmLevel = textBox1.Text;
            tmp.AlarmDate = dateTimePicker1.Value;
            tmp.AlarmLocation = textBox3.Text;
            tmp.AlarmType = textBox4.Text;
            tmp.ProcessState = textBox5.Text;
            tmp.Staff = textBox6.Text;
            tmp.ProcessDate = dateTimePicker2.Value;
            tmp.ProcessInfo = textBox8.Text;
            SqlServerDatabase sqlServer = new SqlServerDatabase();
            try
            {
                int tmp1 = sqlServer.CommitButton(tmp);
                if (tmp1==0)
                {
                    TableAdapter.Fill(this.dHCK_ServerDataSet2.报警状态记录表);
                    dataGridView.Refresh();
                }
                else if (tmp1 == 1)
                {
                    dataGridView1.DataSource = SearchDstable;
                    dataGridView1.DataMember = "testTable";
                }
                else
                {
                    dataGridView1.DataSource = SearchDstable;
                    dataGridView1.DataMember = "testTable";
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
            

        }

        

        private void fillBy报警级别ToolStripButton_Click_1(object sender, EventArgs e)
        {
            try
            {
                this.TableAdapter.FillBy报警级别(this.dHCK_ServerDataSet2.报警状态记录表);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxIndex = comboBox1.SelectedIndex;
        }
        public class SqlServerDatabase : IConnectToDb<AlarmInfo>
        {
            private static String DatabaseName = "DHCK_Server";
            private string connectionString_preload = "server=GODRICSLAPTOP\\SQLEXPRESS;database=" + DatabaseName + ";uid=sa;pwd=gxq654321";
            //private string connectionString_preload = "server=DHCKSERVER\\DHCK_EXPRESS;database=" + DatabaseName + ";uid=sa;pwd=gxq654321";

            private static string _connectionString = "server=GODRICSLAPTOP\\SQLEXPRESS;database=" + DatabaseName + ";uid=sa;pwd=gxq654321";
            private SqlConnection SqlConn = new SqlConnection(_connectionString);

            private class SqlServerDbConn
            {
                //public SqlConnection sqlConnection;
                //public SqlCommand sqlCommand;
                public SqlConnection sqlConnection;
                public SqlCommand sqlCommand;

                public SqlServerDbConn()
                {

                }
                public SqlServerDbConn(SqlConnection theConnection, SqlCommand theCommand)
                {
                    sqlConnection = theConnection;
                    sqlCommand = theCommand;
                }

            }

            private SqlServerDbConn GetConnInfo(string sqlCommand)
            {
                SqlServerDbConn tmp = new SqlServerDbConn();
                tmp.sqlConnection = SqlConn;
                tmp.sqlCommand = new SqlCommand(sqlCommand, tmp.sqlConnection);
                return tmp;
            }
            public SqlServerDatabase()
            {

            }
            public SqlServerDatabase(string connectionString)
            {
                _connectionString = connectionString;
            }

            public void SetToDefaultDatabase()
            {
                _connectionString = connectionString_preload;
            }
            public void OpenDatabase()
            {
                try
                {
                    SqlConn = new SqlConnection(_connectionString);

                    SqlConn.Open();

                    MessageBox.Show(@"Connected to SqlServer.", @"Success");
                    return;

                }
                catch (Exception e)
                {
                    MessageBox.Show(@"Cannot connect to SqlServer" + e.ToString(), @"Error");
                    return;
                }
            }
            public void ConnectToDatabase()
            {
                try
                {
                    using (SqlConnection SqlConn = new SqlConnection(_connectionString))
                    {
                        SqlConn.Open();
                        MessageBox.Show(@"Connected to SqlServer.", @"Success");
                        return;
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(@"Cannot connect to SqlServer" + e.ToString(), @"Error");
                    return;
                }
            }

            public int CommitButton(AlarmInfo classValue)
            {
                if (DHCKSQLServerDatabase.comboBoxIndex == 0)
                {
                    string sqlStr = "INSERT INTO 报警状态记录表 VALUES(@报警级别,@报警时间,@报警地点,@报警类型,@处理状态,@处理人,@处理时间,@处理说明)";
                    //string sqlStr = "INSERT INTO 报警状态记录表 VALUES(@报警级别,@报警地点,@报警类型,@处理状态,@处理人,@处理说明)";
                    //SqlConn = new SqlConnection(connectionString_preload);
                    //SqlCommand sqlCommand = new SqlCommand(sqlStr,SqlConn);
                    SqlServerDbConn sqlServerConnInfo = new SqlServerDbConn(SqlConn, GetConnInfo(sqlStr).sqlCommand);
                    SqlCommand sqlCommand = sqlServerConnInfo.sqlCommand;
                    sqlCommand.Parameters.Add("@报警级别", SqlDbType.Char, 8).Value = classValue.AlarmLevel;
                    sqlCommand.Parameters.Add("@报警时间", SqlDbType.DateTime, 8).Value = classValue.AlarmDate;
                    sqlCommand.Parameters.Add("@报警地点", SqlDbType.Char, 8).Value = classValue.AlarmLocation;
                    sqlCommand.Parameters.Add("@报警类型", SqlDbType.Char, 8).Value = classValue.AlarmType;
                    sqlCommand.Parameters.Add("@处理状态", SqlDbType.Char, 8).Value = classValue.ProcessState;
                    sqlCommand.Parameters.Add("@处理人", SqlDbType.Char, 8).Value = classValue.Staff;
                    sqlCommand.Parameters.Add("@处理时间", SqlDbType.DateTime, 8).Value = classValue.ProcessDate;
                    sqlCommand.Parameters.Add("@处理说明", SqlDbType.Char, 8).Value = classValue.ProcessInfo;
                    SqlConn.Open();
                    sqlCommand.ExecuteNonQuery();
                    SqlConn.Close();
                    return 0;
                }
                else if (DHCKSQLServerDatabase.comboBoxIndex == 1)
                {
                    //string sqlStr = "SELECT * FROM 报警状态记录表 WHERE 报警地点 LIKE '%" + classValue.AlarmLocation + "%'";
                    string sqlStr = "SELECT * FROM 报警状态记录表 ";
                    if (classValue.AlarmLevel!="")
                    {
                        sqlStr += "WHERE 报警级别 LIKE '%" + classValue.AlarmLevel + "%'";
                    }

                    //if (classValue.AlarmDate.ToString() != "")
                    //{
                    //    sqlStr += "AND 报警时间 LIKE '%" + classValue.AlarmDate.ToString() + "%' ";
                    //}
                    if (classValue.AlarmLocation!="")
                    {
                        if (sqlStr[sqlStr.Length-1]==' ')
                        {
                            sqlStr += "WHERE ";
                        }
                        else
                        {
                            sqlStr += "AND ";
                        }
                        sqlStr += " 报警地点 LIKE '%" + classValue.AlarmLocation + "%'";
                    }

                    if (classValue.AlarmType!="")
                    {
                        if (sqlStr[sqlStr.Length - 1] == ' ')
                        {
                            sqlStr += "WHERE ";
                        }
                        else
                        {
                            sqlStr += "AND ";
                        }
                        sqlStr += " 报警类型 LIKE '%" + classValue.AlarmType + "%'";
                    }
                    if (classValue.ProcessState != "")
                    {
                        if (sqlStr[sqlStr.Length - 1] == ' ')
                        {
                            sqlStr += "WHERE ";
                        }
                        else
                        {
                            sqlStr += "AND ";
                        }
                        sqlStr += " 处理状态 LIKE '%" + classValue.ProcessState + "%'";
                    }
                    if (classValue.Staff != "")
                    {
                        if (sqlStr[sqlStr.Length - 1] == ' ')
                        {
                            sqlStr += "WHERE ";
                        }
                        else
                        {
                            sqlStr += "AND ";
                        }
                        sqlStr += " 处理人 LIKE '%" + classValue.Staff + "%'";
                    }
                    if (classValue.ProcessInfo != "")
                    {
                        if (sqlStr[sqlStr.Length - 1] == ' ')
                        {
                            sqlStr += "WHERE ";
                        }
                        else
                        {
                            sqlStr += "AND ";
                        }
                        sqlStr += " 处理说明 LIKE '%" + classValue.ProcessInfo + "%'";
                    }
                    SqlConn.Open();
                    SqlCommand sc = new SqlCommand(sqlStr, SqlConn);
                    SqlDataAdapter adapter = new SqlDataAdapter(sc);
                    DataSet dstable = new DataSet();
                    adapter.Fill(dstable, "testTable");
                    SearchDstable = dstable;
                    SqlConn.Close();
                    SqlConn.Dispose();
                    return 1;
                }
                else
                {
                    string sqlStr = "SELECT * FROM 报警状态记录表 ";
                    if (classValue.AlarmLevel != "")
                    {
                        sqlStr += "WHERE 报警级别 LIKE '%" + classValue.AlarmLevel + "%'";
                    }

                    //if (classValue.AlarmDate.ToString() != "")
                    //{
                    //    sqlStr += "AND 报警时间 LIKE '%" + classValue.AlarmDate.ToString() + "%' ";
                    //}
                    if (classValue.AlarmLocation != "")
                    {
                        if (sqlStr[sqlStr.Length - 1] == ' ')
                        {
                            sqlStr += "WHERE ";
                        }
                        else
                        {
                            sqlStr += "OR ";
                        }
                        sqlStr += " 报警地点 LIKE '%" + classValue.AlarmLocation + "%'";
                    }

                    if (classValue.AlarmType != "")
                    {
                        if (sqlStr[sqlStr.Length - 1] == ' ')
                        {
                            sqlStr += "WHERE ";
                        }
                        else
                        {
                            sqlStr += "OR ";
                        }
                        sqlStr += " 报警类型 LIKE '%" + classValue.AlarmType + "%'";
                    }
                    if (classValue.ProcessState != "")
                    {
                        if (sqlStr[sqlStr.Length - 1] == ' ')
                        {
                            sqlStr += "WHERE ";
                        }
                        else
                        {
                            sqlStr += "OR ";
                        }
                        sqlStr += " 处理状态 LIKE '%" + classValue.ProcessState + "%'";
                    }
                    if (classValue.Staff != "")
                    {
                        if (sqlStr[sqlStr.Length - 1] == ' ')
                        {
                            sqlStr += "WHERE ";
                        }
                        else
                        {
                            sqlStr += "OR ";
                        }
                        sqlStr += " 处理人 LIKE '%" + classValue.Staff + "%'";
                    }
                    if (classValue.ProcessInfo != "")
                    {
                        if (sqlStr[sqlStr.Length - 1] == ' ')
                        {
                            sqlStr += "WHERE ";
                        }
                        else
                        {
                            sqlStr += "OR ";
                        }
                        sqlStr += " 处理说明 LIKE '%" + classValue.ProcessInfo + "%'";
                    }
                    SqlConn.Open();
                    SqlCommand sc = new SqlCommand(sqlStr, SqlConn);
                    SqlDataAdapter adapter = new SqlDataAdapter(sc);
                    DataSet dstable = new DataSet();
                    adapter.Fill(dstable, "testTable");
                    SearchDstable = dstable;
                    SqlConn.Close();
                    SqlConn.Dispose();
                    return 2;
                }
            }
        }

        public class AlarmInfo
        {
            public string AlarmLevel { get; set; }
            public DateTime AlarmDate { get; set; }
            public String AlarmLocation { get; set; }
            public String AlarmType { get; set; }
            public string ProcessState { get; set; }
            public string Staff { get; set; }
            public DateTime ProcessDate { get; set; }
            public String ProcessInfo { get; set; }
        }
        public interface IConnectToDb<T> where T : class
        {
            int CommitButton(T classValue);
        }
    }
    
}
