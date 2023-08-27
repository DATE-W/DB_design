
using Oracle.ManagedDataAccess.Client;
using SqlSugar;


namespace DBwebAPI
{
    public class DBconn
    {
        public static string constr = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=110.40.138.123)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=xe)));Persist Security Info=True;User ID=dbdesign;Password=TJUdb2023;";
        public SqlSugarScope sqlORM = null;
        public bool Conn()
        {
            try
            {
                OracleConnection con = new OracleConnection(constr);
                con.Open();
                sqlORM = new SqlSugarScope(new ConnectionConfig()
                {
                    ConnectionString = constr,
                    DbType = DbType.Oracle,
                    IsAutoCloseConnection = true
                });
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


    }

    //public class SQLSugar
    //{


    //    public static string constr = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=orcl)));Persist Security Info=True;User ID=c##test;Password=021120;";

    //    public SqlSugarClient Db = new SqlSugarClient(new ConnectionConfig()
    //    {
    //        ConnectionString = constr,
    //        DbType = DbType.Oracle,
    //        IsAutoCloseConnection = true
    //    });

    //}
}
