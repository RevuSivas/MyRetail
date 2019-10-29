using MyRetail.Rest.Entities;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace MyRetail.Rest.DAL
{
    public class RetailDl
    {
        public List<PriceEntity> getData()
        {
            string strCon = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

            using (var con = new SqlConnection(strCon))
            {
                try
                {
                    con.Open();
                    DataTable dt = new DataTable();
                    using (var adp = new SqlDataAdapter("Select * from retail.dbo.price", con))
                    {
                        adp.Fill(dt);
                        var prices = dt.AsEnumerable().Select(r => new PriceEntity()
                        {
                            ProductId = long.Parse(r["ProductId"].ToString()),
                            Value = decimal.Parse(r["value"].ToString()),
                            CurrencyCode = r["CurrencyCode"].ToString()
                        }).ToList();
                        return prices;
                    }
                }
                catch(SqlException ex)
                {
                    throw new System.Exception(ex.Message);
                }
            }
        }

        public void updateData(long productId, decimal price)
        {
            string strCon = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

            using (var con = new SqlConnection(strCon))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    con.Open();

                    cmd.CommandText = $"Update retail.dbo.price set value = {price} where productId = {productId}";
                    cmd.ExecuteNonQuery();
                }
                catch(SqlException ex)
                {
                    throw new System.Exception(ex.Message);
                }
            }
        }
    }
}
