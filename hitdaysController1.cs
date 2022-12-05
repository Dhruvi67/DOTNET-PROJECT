using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using p1.Models;
using System;

using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace p1.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class hitdaysController1 : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public hitdaysController1(IConfiguration Configuration)
        {
            _configuration = Configuration;
        }

        [Route("api/invoice")]
        [HttpPost]
        public JsonResult Getinvoice(Date input)
        {
            string sd = input.sd.ToString("yyyy-MM-dd");
            string ed = input.ed.ToString("yyyy-MM-dd");
            //string sd = DateTime.ParseExact(input.sd, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
            //DateTime ed = DateTime.ParseExact(input.ed.ToString(), "yyyy-MM-dd", CultureInfo.InvariantCulture);
            string query = @"SELECT COUNT('id') as dueinvoice from dbo.fts_invoice where amount_due> 0 and invoice_date  between '"+sd+"' and '"+ed+"' ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("connection");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    
                   
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
            //return Json(new { res = response }, JsonRequestBehavior.AllowGet);

        }

        [Route("api/salechart")]
        [HttpPost]
        public IActionResult Getchart(Date input)
        {
            string sd = input.sd.ToString("yyyy-MM-dd");
            string ed = input.ed.ToString("yyyy-MM-dd");
            string query = @"SELECT DATEPART(HOUR,invoice_date ) as time,SUM(invoice_total) as amount from dbo.fts_invoice WHERE CAST(invoice_date AS DATE) BETWEEN '"+sd+"' AND '"+ed+"' GROUP BY DATEPART(HOUR,invoice_date )  order by DATEPART(HOUR,invoice_date ) ASC";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("connection");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
           

        }


        [Route("api/totalsale")]
        [HttpPost]
        public JsonResult Getsales(Date input)
        {
            string sd = input.sd.ToString("yyyy-MM-dd");
            string ed = input.ed.ToString("yyyy-MM-dd");
            string query = @"SELECT SUM(invoice_total) AS totalsales FROM dbo.fts_invoice where invoice_date between '"+sd+"' and '"+ed+"'" ;
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("connection");
            SqlDataReader myReader;
            
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    
                 
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);

        }

        [Route("api/discount")]
        [HttpPost]
        public JsonResult Getdiscount(Date input)
        {
            string sd = input.sd.ToString("yyyy-MM-dd");
            string ed = input.ed.ToString("yyyy-MM-dd");
            string query = @"SELECT FORMAT(sum(total_discount),'.##') as totaldiscount from dbo.fts_invoice where invoice_date  between '" + sd + "' and '" + ed + "'";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("connection");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }

        [Route("api/dueamount")]
        [HttpPost]
        public JsonResult Getamount(Date input)
        {
            string sd = input.sd.ToString("yyyy-MM-dd");
            string ed = input.ed.ToString("yyyy-MM-dd");
            string query = @"select SUM(amount_due) as dueamount from dbo.fts_invoice where invoice_date between '"+sd+"' and '"+ed+"'";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("connection");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }

        [Route("api/totaltax")]
        [HttpPost]
        public JsonResult Gettax(Date input)
        {
            string sd = input.sd.ToString("yyyy-MM-dd");
            string ed = input.ed.ToString("yyyy-MM-dd");
            string query = @"SELECT FORMAT(SUM(total_tax_amount),'.##') as totaltax FROM dbo.fts_invoice where invoice_date between '" + sd + "' and '" + ed + "'";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("connection");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }

        //[Route("api/totalsale")]
        //[HttpPost]
        //public JsonResult Getsales(string sd,string ed)
        //{
            
            
 
        //    string query = @"SELECT SUM(invoice_total) AS totalsales FROM dbo.fts_invoice WHERE invoice_date BETWEEN ? and  ? and is_paid=1";
        //    DataTable table = new DataTable();
        //    string sqlDataSource = _configuration.GetConnectionString("connection");
        //    SqlDataReader myReader;
        //    using (SqlConnection myCon = new SqlConnection(sqlDataSource))
        //    {
        //        myCon.Open();
        //        using (SqlCommand myCommand = new SqlCommand(query, myCon))
        //        {
        //            //object p = myCommand.Parameters.Add('@sd', sd);
        //            //object p1 = myCommand.Parameters.Add('@ed', ed);
        //            myReader = myCommand.ExecuteReader();
        //            table.Load(myReader);
        //            myReader.Close();
        //            myCon.Close();
        //        }
        //    }
        //    return new JsonResult(table);
        //}

        //purchase
        [Route("api/totalpurchase")]
        [HttpGet]
        public JsonResult Getpurchase()
        {
            string query = @"SELECT FORMAT(SUM(total_amount),'.##') as totalpurchase from dbo.fts_purchase_order_invoice_item_details";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("connection");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }

        [Route("api/pdueamount")]
        [HttpGet]
        public JsonResult Getpurchasedueamount()
        {
            string query = @"SELECT Format(SUM(amount_due),'.##') as pdueamount FROM dbo.fts_purchase_order_invoice";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("connection");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }


        [Route("api/totalptax")]
        [HttpGet]
        public JsonResult Getpurchase_tax()
        {
            string query = @"SELECT FORMAT(SUM(total_tax),'.##') as totalptax FROM dbo.fts_purchase_order_invoice";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("connection");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }


        [Route("api/pdueinvoice")]
        [HttpGet]
        public JsonResult Getpurchasedueinvoice()
        {
            string query = @"SELECT COUNT(id) as pdueinvoice FROM dbo.fts_purchase_order_invoice WHERE amount_due>0";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("connection");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }

        [Route("api/totalpdiscount")]
        [HttpGet]
        public JsonResult Getpurchasediscount()
        {
            string query = @"SELECT SUM(discount) as totalpdiscount FROM dbo.fts_purchase_order_invoice";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("connection");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }

    }
}
