using System.Data.SqlClient;

public class Database
{
    SqlConnection con =
        new SqlConnection(
        @"Data Source=(LocalDB)\MSSQLLocalDB;
        Initial Catalog=HastaneZiyaretDB;
        Integrated Security=True");

    public SqlConnection Baglanti()
    {
        if (con.State == System.Data.ConnectionState.Closed)
        {
            con.Open();
        }

        return con;
    }
}