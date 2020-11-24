using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using MSSQL_VO1;

namespace MSSQL_DAC1
{
    public class ProductDB
    {
        public DataTable GetProductList() //데이터테이블로 리턴
        {
            string strConn = ConfigurationManager.ConnectionStrings["myDB"].ConnectionString;

            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(strConn))
            {
                string sql = @"select P.ProductID, P.ProductName, S.CompanyName, C.CategoryName, QuantityPerUnit
  from Products P inner join Suppliers S on P.SupplierID = S.SupplierID
        inner join Categories C on P.CategoryID = C.CategoryID";

                using (SqlDataAdapter da = new SqlDataAdapter(sql, conn))
                {                    
                    da.Fill(dt);
                }
            }

            return dt;
        }

        public DataTable GetCategory() //카테고리 정보 바인딩
        {
            string strConn = ConfigurationManager.ConnectionStrings["myDB"].ConnectionString;

            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(strConn))
            {
                string sql = @"select CategoryID, CategoryName from Categories";

                using (SqlDataAdapter da = new SqlDataAdapter(sql, conn))
                {
                    da.Fill(dt);
                }
            }

            return dt;
        }
        

        public List<ProductInfoVO> GetProductList2() //리스트VO로 리턴
        {
            string strConn = ConfigurationManager.ConnectionStrings["myDB"].ConnectionString;

            List<ProductInfoVO> list = new List<ProductInfoVO>();
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                string sql = @"select P.ProductID, P.ProductName, S.CompanyName, C.CategoryName, QuantityPerUnit
  from Products P inner join Suppliers S on P.SupplierID = S.SupplierID
        inner join Categories C on P.CategoryID = C.CategoryID";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    list = Helper.DataReaderMapToList<ProductInfoVO>(reader);
                    //while (reader.Read())
                    //{
                    //    ProductInfoVO vo = new ProductInfoVO
                    //    {
                    //        ProductID = Convert.ToInt32(reader["ProductID"]),
                    //        ProductName = reader[1].ToString(),
                    //        CategoryName = reader["CategoryName"].ToString(),
                    //        CompanyName = reader["CompanyName"].ToString(),
                    //        QuantityPerUnit = reader["QuantityPerUnit"].ToString()
                    //    };

                    //    list.Add(vo);
                    //}
                    conn.Close();
                }
            }

            return list;
        }

        public List<ComboItemVO> GetCodeList()
        {
            List<ComboItemVO> list = new List<ComboItemVO>();

            string strConn = ConfigurationManager.ConnectionStrings["myDB"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(strConn))
            {
                string sql = @"select Code, CodeName, Gubun from VW_GetCode";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    list = Helper.DataReaderMapToList<ComboItemVO>(reader);
                    //while (reader.Read())
                    //{
                    //    ComboItemVO vo = new ComboItemVO
                    //    {
                    //        //Code CodeName Gubun
                    //        Code = reader["Code"].ToString(),
                    //        CodeName = reader["CodeName"].ToString(),
                    //        Gubun = reader["Gubun"].ToString(),
                            
                    //    };

                    //    list.Add(vo);
                    //}
                    conn.Close();
                }
            }
            return list;
        }

        public List<RegionInfoVO> GetRegionList()
        {
            string strConn = ConfigurationManager.ConnectionStrings["myDB"].ConnectionString;

            List<RegionInfoVO> list = new List<RegionInfoVO>();
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                string sql = @"select  TerritoryID, TerritoryDescription, cast(T.RegionID as varchar) RegionID, RegionDescription RegionDesc
                                from Territories T inner join Region R on T.RegionID = R.RegionID";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    list = Helper.DataReaderMapToList<RegionInfoVO>(reader);                   
                    conn.Close();
                }
            }
            return list;
        }
    }
}
