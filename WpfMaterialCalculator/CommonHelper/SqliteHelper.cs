using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SQLite;
using System.Configuration;

namespace WpfMaterialCalculator.CommonHelper
{
    /// <summary>
    /// 操作Sqlite数据库Helper工具类
    /// 先做最需要的，以后再完善
    /// </summary>
    public class SqliteHelper
    {
        private static readonly string conStr;
        static SqliteHelper()
        {
            conStr = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
        }

        private static SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(conStr);
        } 

        private static void PrepareCommand(SQLiteConnection conn,SQLiteCommand cmd,string cmdText, SQLiteParameter[] cmdParameters)
        {
            cmd.Parameters.Clear();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            cmd.CommandType = CommandType.Text;
            if (cmdParameters!=null)
            {
                foreach (var parm in cmdParameters)
                {
                    //这里使用未命名参数
                    cmd.Parameters.Add(parm);
                }

            }
        }

        public static SQLiteDataReader ExecuteReader(string cmdText,SQLiteParameter[] cmdParameters)
        {
            SQLiteCommand cmd = new SQLiteCommand();
            SQLiteConnection conn = GetConnection();
            PrepareCommand(conn, cmd, cmdText, cmdParameters);
            conn.Open();
            SQLiteDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            return dr;
        }

        public static int ExecuteNonQuery(string cmdText, SQLiteParameter[] cmdParameters)
        {
            SQLiteCommand cmd = new SQLiteCommand();
            SQLiteConnection conn = GetConnection();
            PrepareCommand(conn, cmd, cmdText, cmdParameters);
            conn.Open();
            int result = cmd.ExecuteNonQuery();
            conn.Close();
            return result;
        }




    }
}
