using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Script.Serialization;
using System.IO;
using Newtonsoft.Json;
using System.Security.Cryptography;

namespace Common
{
    public class CommonClass
    {
        /// <summary>
        /// 创建字符串连接
        /// </summary>
        /// <returns></returns>
        public static SqlConnection Create() {
           
            string Constr = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;
          SqlConnection  connection = new SqlConnection(Constr);
            return connection;
        }
        /// <summary>
        /// 通用查询方法,此方法使用MYSQL
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        //public DataTable Sels(string sql) {
        //    try
        //    {
        //        using (MySqlConnection conn=new MySqlConnection ())
        //        {  
        //            using (MySqlCommand cmd=new MySqlCommand ())
        //            {
        //                cmd.Connection = conn;
        //                cmd.CommandText = sql;
        //                MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
        //                DataSet ds = new DataSet();
        //                sda.Fill(ds);
        //                return ds.Tables[0];
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}
 
        public  DataTable Selcets(string sql) {
            try
            {
                SqlConnection conn = Create();//打开字符串连接
                SqlCommand cmd = new SqlCommand();//执行命令
                cmd.Connection = conn;
                cmd.CommandText = sql;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);//数据适配器
                DataSet ds = new DataSet();//填充数据
                sda.Fill(ds);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 把表格转换成JSON数据
        /// 通过表格名字查找数组中的数据
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static string DataTableToJson(DataTable table) {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            using (JsonWriter jw = new JsonTextWriter(sw)) {
                JsonSerializer ser = new JsonSerializer();
                jw.WriteStartObject();
                jw.WritePropertyName("");
                //表格名字
                jw.WriteStartArray();
                try
                {
                    foreach (DataRow dr in table.Rows)
                    {
                        jw.WriteStartObject();  
                        foreach (DataColumn dc in table.Columns)
                        {
                            jw.WritePropertyName(dc.ColumnName);
                            ser.Serialize(jw, dr[dc].ToString());
                        }
                        jw.WriteEndObject();//输出
                    }
                    jw.WriteEndObject();
                    jw.WriteEndObject();
                }
                catch (Exception ex)
                {

                    string str = ex.Message;
                }
                sw.Close();
                jw.Close();

            }
            return sb.ToString();
        }
        /// <summary>
        /// 把集合序列化
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static string ToJosn(object json) {
            JavaScriptSerializer js = new JavaScriptSerializer();
            return js.Serialize(json);

        }
         /// <summary>
         /// 查询单条数据
         /// </summary>
         /// <param name="sql"></param>
         /// <returns></returns>
        public static Dictionary<string, object> Select(string sql) {
            SqlConnection conn = Create();
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count==0)
            {
                return null;
            }
            DataRow row = dt.Rows[0];
            Dictionary<string, object> di = new Dictionary<string, object>();
            foreach (DataColumn item in dt.Columns)
            {
                di.Add(item.ColumnName, row[item.ColumnName]);
            }
            return di;
        }

        /// <summary>
        /// 执行一条SQL 语句
        /// </summary>
        /// <param name="strsql"></param>
        /// <param name="MySqlParameter"></param>
        /// <returns></returns>
        public static int ExecutionSQL(string strSql, SqlConnection conn = null)
        {
            conn = conn == null ? Create() : conn;
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(strSql, conn);
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
            finally
            {
                conn.Close();
            }
        }
        /// <summary>
        /// 带参数
        /// </summary>
        /// <param name="strMySql"></param>
        /// <param name="MySqlParameter"></param>
        /// <returns></returns>
        public int ExecutionSqlPar(string strMySql, SqlParameter[] SqlParameter)
        {
            SqlConnection conn = Create();
            SqlCommand cmd = null;
            try
            {
                conn.Open();
                cmd = new SqlCommand();
                cmd.Parameters.AddRange(SqlParameter);
                cmd.CommandText = strMySql;
                cmd.Connection = conn;
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Parameters.Clear();
                conn.Close();
            }
        }

 
        public object ExecuteScalar(string strMySql, SqlParameter[] SqlParameter)
        {
            SqlConnection conn = Create();
            SqlCommand cmd = null;
            try
            {
                conn.Open();
                cmd = new SqlCommand();
                cmd.Parameters.AddRange(SqlParameter);
                cmd.CommandText = strMySql;
                cmd.Connection = conn;
                return cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Parameters.Clear();
                conn.Close();
            }
        }



        //对字符串进行MD5运算
        public static string MD5String(string str) {
            MD5 md5 = MD5.Create();
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(str);
            byte[] md5Buffer= md5.ComputeHash(buffer);
            StringBuilder sb = new StringBuilder();
            foreach (byte item in md5Buffer)
            {
               sb.Append(item.ToString("X2"));////ToString("X2") 为C#中的字符串格式控制符
            }
            return sb.ToString();
        }
    }
}
