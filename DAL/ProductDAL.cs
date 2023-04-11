using Crudapplication.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using System.Data.SqlClient;

namespace Crudapplication.DAL
{

    public class ProductDAL
    {
        string conString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();

        public List<Product> GetAllProducts()
        {
            List<Product> productList = new List<Product>();
            string sqlquery = @"  select ProductID,ProductName,Price,Qty,Remark from tbl_ProductMaster";
            DataSet objDataSet = SqlHelper.ExecuteDataset(conString, CommandType.Text, sqlquery);
            DataTable dataTable = objDataSet.Tables[0];
            foreach (DataRow dr in dataTable.Rows)
            {
                productList.Add(new Product
                {
                    ProductID = Convert.ToInt32(dr["ProductID"]),
                    ProductName = dr["ProductName"].ToString(),
                    Price = Convert.ToDecimal(dr["Price"]),
                    Qty = Convert.ToInt32(dr["qty"]),
                    Remark = dr["Remark"].ToString()
                });
            }
            return productList;
        }

        public bool InsertProduct(Product product)
        {
            int id = 0;
            string strSql = @"insert into tbl_ProductMaster(ProductName,Price,Qty,Remark) values(@ProductName,@Price,@Qty,@Remark)";
            SqlParameter[] objparam = new SqlParameter[]
            {
                new SqlParameter("@ProductName",product.ProductName),
                new SqlParameter("@Price",product.Price),
                new SqlParameter("@Qty",product.Qty),
                new SqlParameter("@Remark",product.Remark),
            };
            id = SqlHelper.ExecuteNonQuery(conString, CommandType.Text, strSql, objparam);
            if (id > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Product> GetProductsByID(int ProductID)
        {
            List<Product> productList = new List<Product>();
            string sqlquery = @"  select ProductID,ProductName,Price,Qty,Remark from tbl_ProductMaster where ProductID=@ProductID";
            SqlParameter[] objparam = new SqlParameter[]
            {
                new SqlParameter("@ProductID", ProductID)
            };
            DataSet objDataSet = SqlHelper.ExecuteDataset(conString, CommandType.Text, sqlquery, objparam);
            DataTable dataTable = objDataSet.Tables[0];
            foreach (DataRow dr in dataTable.Rows)
            {
                productList.Add(new Product
                {
                    ProductID = Convert.ToInt32(dr["ProductID"]),
                    ProductName = dr["ProductName"].ToString(),
                    Price = Convert.ToDecimal(dr["Price"]),
                    Qty = Convert.ToInt32(dr["qty"]),
                    Remark = dr["Remark"].ToString()
                });
            }
            return productList;
        }

        public bool UpdateProduct(Product product)
        {
            int id = 0;
            string strSql = @"update tbl_ProductMaster set ProductName=@ProductName,Price=@Price,Qty=@Qty,Remark=@Remark where ProductID=@ProductID";
            SqlParameter[] objparam = new SqlParameter[]
             {
                new SqlParameter("@ProductID",product.ProductID),
                new SqlParameter("@ProductName",product.ProductName),
                new SqlParameter("@Price",product.Price),
                new SqlParameter("@Qty",product.Qty),
                new SqlParameter("@Remark",product.Remark),
             };
            id = SqlHelper.ExecuteNonQuery(conString, CommandType.Text, strSql, objparam);
            if (id > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteProduct(int productID)
        {
            int i = 0;
            string strSql = @"delete from tbl_ProductMaster where ProductId=@ProductId";
            SqlParameter[] objparam = new SqlParameter[] {
              new SqlParameter("@ProductID",productID)
            };
            int id=SqlHelper.ExecuteNonQuery(conString,CommandType.Text, strSql, objparam);
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}