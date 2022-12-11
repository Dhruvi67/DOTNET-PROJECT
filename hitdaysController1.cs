using Dapper;
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

        [Route("api/totalinvoice")]
        [HttpPost]
        public JsonResult Gettotalinvoice(Date input)
        {
            string sd = input.sd.ToString("yyyy-MM-dd");
            string ed = input.ed.ToString("yyyy-MM-dd");
            string query = @"SELECT COUNT(id) as totalinvoice FROM dbo.fts_invoice WHERE invoice_date BETWEEN '"+sd+"' and  '"+ed+"'";
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


        [Route("api/totalproduct")]
        [HttpPost]
        public JsonResult Getproduct(Date input)
        {
            string sd = input.sd.ToString("yyyy-MM-dd");
            string ed = input.ed.ToString("yyyy-MM-dd");
            string query = @"SELECT COUNT(invoice_item_id)as totalproducts FROM dbo.fts_itemable WHERE created_date_time BETWEEN '"+sd+"' AND '"+ed+"'";
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


        [Route("api/invoicelist")]
        [HttpPost]
        public JsonResult Getinvoicelist(Date input)
        {
            string sd = input.sd.ToString("yyyy-MM-dd");
            string ed = input.ed.ToString("yyyy-MM-dd");
            string query = @"SELECT id,invoice_date,invoice_total,amount_received,amount_due FROM dbo.fts_invoice WHERE  invoice_date BETWEEN '"+sd+"' AND '"+ed+"' GROUP BY id,invoice_date,invoice_total,amount_received,amount_due HAVING COUNT(id)> 0 ";
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



        [Route("api/productlist")]
        [HttpPost]
        public JsonResult Getproductlist(Date input)
        {
            string sd = input.sd.ToString("yyyy-MM-dd");
            string ed = input.ed.ToString("yyyy-MM-dd");
            string query = @"SELECT invoice_item_id,description,discount,total FROM dbo.fts_itemable WHERE created_date_time BETWEEN '"+sd+"' and  '"+ed+"' GROUP BY invoice_item_id,description,discount,total HAVING COUNT(invoice_item_id)>0";
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


        //purchase
        [Route("api/totalpurchase")]
        [HttpPost]
        public JsonResult Getpurchase(Date input)
        {
            
                string sd = input.sd.ToString("yyyy-MM-dd");
                string ed = input.ed.ToString("yyyy-MM-dd");
                string query = @"SELECT FORMAT(SUM(total_amount),'.##') as totalpurchase from dbo.fts_purchase_order_invoice_item_details where created_date_time between '"+sd+"' and '"+ed+"' ";
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

        [Route("api/pinvoicelist")]
        [HttpPost]
        public JsonResult Getpinvoicelist(Date input)
        {

            string sd = input.sd.ToString("yyyy-MM-dd");
            string ed = input.ed.ToString("yyyy-MM-dd");
            string query = @"SELECT id,discount,total,amount_due,amount_paid FROM dbo.fts_purchase_order_invoice WHERE invoice_date BETWEEN '"+sd+"' and  '"+ed+"'  GROUP BY id,discount,total,amount_due,amount_paid HAVING COUNT(id)>0";
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


        [Route("api/pproductlist")]
        [HttpPost]
        public JsonResult Getpproductlist(Date input)
        {

            string sd = input.sd.ToString("yyyy-MM-dd");
            string ed = input.ed.ToString("yyyy-MM-dd");
            string query = @"SELECT product_id,discount_amount,total_amount,unit_price FROM dbo.fts_purchase_order_invoice_item_details WHERE created_date_time BETWEEN '"+sd+"' and  '"+ed+"' GROUP BY product_id,discount_amount,total_amount,unit_price HAVING COUNT(product_id)>0";
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

        [Route("api/totalpinvoice")]
        [HttpPost]
        public JsonResult Gettotalpinvoice(Date input)
        {
            string sd = input.sd.ToString("yyyy-MM-dd");
            string ed = input.ed.ToString("yyyy-MM-dd");
            string query = @"SELECT COUNT(id) as totalinvoice FROM dbo.fts_purchase_order_invoice WHERE invoice_date BETWEEN '"+sd+"' and  '"+ed+"'";
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

        [Route("api/pdueamount")]
        [HttpPost]
        public JsonResult Getpurchasedueamount(Date input)
        {
            string sd = input.sd.ToString("yyyy-MM-dd");
            string ed = input.ed.ToString("yyyy-MM-dd");
            string query = @"SELECT Format(SUM(amount_due),'.##') as pdueamount FROM dbo.fts_purchase_order_invoice where last_modified_date_time between '"+sd+"' and '"+ed+"'";
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

        [Route("api/totalpproduct")]
        [HttpPost]
        public JsonResult Gettotalpproduct(Date input)
        {
            string sd = input.sd.ToString("yyyy-MM-dd");
            string ed = input.ed.ToString("yyyy-MM-dd");
            string query = @"SELECT COUNT(product_id) as totalproducts FROM dbo.fts_purchase_order_invoice_item_details WHERE created_date_time BETWEEN '"+sd+"' and  '"+ed+"'";
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
        [HttpPost]
        public JsonResult Getpurchase_tax(Date input)
        {
            string sd = input.sd.ToString("yyyy-MM-dd");
            string ed = input.ed.ToString("yyyy-MM-dd");
            string query = @"SELECT FORMAT(SUM(total_tax),'.##') as totalptax FROM dbo.fts_purchase_order_invoice where last_modified_date_time between '"+sd+"' and '"+ed+"' ";
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
        [HttpPost]
        public JsonResult Getpurchasedueinvoice(Date input)
        {
            string sd = input.sd.ToString("yyyy-MM-dd");
            string ed = input.ed.ToString("yyyy-MM-dd");
            string query = @"SELECT COUNT(id) as pdueinvoice FROM dbo.fts_purchase_order_invoice WHERE amount_due>0 and last_modified_date_time between '" + sd + "' and '" + ed + "'";
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
        [HttpPost]
        public JsonResult Getpurchasediscount(Date input)
        {
            string sd = input.sd.ToString("yyyy-MM-dd");
            string ed = input.ed.ToString("yyyy-MM-dd");
            string query = @"SELECT SUM(discount) as totalpdiscount FROM dbo.fts_purchase_order_invoice where last_modified_date_time between '"+sd+"' and '"+ed+"'";
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


        [Route("api/purchasechart")]
        [HttpPost]
        public JsonResult Getpurchasechart(Date input)
        {
            string sd = input.sd.ToString("yyyy-MM-dd");
            string ed = input.ed.ToString("yyyy-MM-dd");
            string query = @"SELECT SUM(total_amount) as amount,DATEPART(HOUR,created_date_time) as time from dbo.fts_purchase_order_invoice_item_details where created_date_time BETWEEN '" + sd + "' AND '" + ed + "' GROUP BY DATEPART(HOUR,created_date_time ) ORDER BY SUM(total_amount) desc";
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

        //collection

        [Route("api/collectionchart")]
        [HttpPost]
        public async Task<JsonResult> GetcollectionchartAsync(Date input)
        {
            string sd = input.sd.ToString("yyyy-MM-dd");
            string ed = input.ed.ToString("yyyy-MM-dd");

           // string query = ;
            DataTable table = new DataTable();
            DataTable table1 = new DataTable();
            DataTable table2 = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("connection");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();

               await using (SqlCommand myCommand = new SqlCommand(@"SELECT DATEPART(HOUR,invoice_date) as time,SUM(amount_received) as cashamount FROM dbo.fts_invoice where payment_type=1 AND invoice_date BETWEEN '"+sd+"' AND '"+ed+"' GROUP BY DATEPART(HOUR,invoice_date) ORDER BY DATEPART(HOUR,invoice_date) asc", myCon))
                {
                    //myCommand.ExecuteNonQuery();
                    try
                    {
                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);
                        myReader.Close();
                        //return new JsonResult(table);
                        // return new JsonResult(table);
                        //myCon.Close();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error loading Category data from database. " + ex.Message);
                    }
                   // return new JsonResult(table);
                }

                string query1 = @"SELECT DATEPART(HOUR,invoice_date) as time,SUM(amount_received) as phnamount FROM dbo.fts_invoice where payment_type=13 AND invoice_date BETWEEN '"+sd+"' AND '"+ed+"' GROUP BY DATEPART(HOUR,invoice_date) ";
               await using (SqlCommand myCommand = new SqlCommand(query1, myCon))
                {
                    try
                    {
                        // myCommand1.ExecuteNonQuery();

                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);
                        myReader.Close();
                       // return new JsonResult(table1);
                        // return new JsonResult(table);
                        //myCon.Close();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error loading Category data from database. " + ex.Message);
                    }
                  
                }

                string query2 = @"SELECT DATEPART(HOUR,invoice_date) as time,SUM(amount_received) as creditamount FROM dbo.fts_invoice where payment_type=8 AND invoice_date BETWEEN '"+sd+"' AND '"+ed+"' GROUP BY DATEPART(HOUR,invoice_date) ";
               await using (SqlCommand myCommand = new SqlCommand(query2, myCon))
                {
                    try
                    {
                        // myCommand1.ExecuteNonQuery();

                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);
                        myReader.Close();
                        //return new JsonResult(table2);

                        // myCon.Close();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error loading Category data from database. " + ex.Message);
                    }
                 
                }
            }
            return new JsonResult(table);
           
           
        }


        [Route("api/Demo")]
        [HttpPost]
        public JsonResult Getdemo(Date input1)
        {
            string sd = input1.sd.ToString("yyyy-MM-dd");
            string ed = input1.ed.ToString("yyyy-MM-dd");
            string[] query= { "SELECT DATEPART(HOUR,invoice_date) as time,SUM(amount_received) as cashamount FROM dbo.fts_invoice where payment_type=1 AND invoice_date BETWEEN '" + sd + "' AND '" + ed + "' GROUP BY DATEPART(HOUR,invoice_date) ORDER BY DATEPART(HOUR,invoice_date) asc", "SELECT DATEPART(HOUR,invoice_date) as time,SUM(amount_received) as creditamount FROM dbo.fts_invoice where payment_type=8 AND invoice_date BETWEEN '" + sd + "' AND '" + ed + "' GROUP BY DATEPART(HOUR,invoice_date) ORDER BY DATEPART(HOUR,invoice_date) asc", "SELECT DATEPART(HOUR,invoice_date) as time,SUM(amount_received) as phnamount FROM dbo.fts_invoice where payment_type=13 AND invoice_date BETWEEN '" + sd + "' AND '" + ed + "' GROUP BY DATEPART(HOUR,invoice_date) ORDER BY DATEPART(HOUR,invoice_date) asc" };
       
           
            DataTable table = new DataTable();
            
            string sqlDataSource = _configuration.GetConnectionString("connection");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                
                for (var i = 0; i < 3; i++)
                {
                   
                    using (SqlCommand myCommand = new SqlCommand(query[i], myCon))
                    {
                        
                       
                        
                            myReader = myCommand.ExecuteReader();
                            table.Load(myReader);
                            myReader.Close();
                            
                        
                         
                     
                    }
                   
                }
                return new JsonResult(table);
            }
            
        }

                [Route("api/totalcash")]
        [HttpPost]
        public JsonResult Gettotalcash(Date input)
        {
            string sd = input.sd.ToString("yyyy-MM-dd");
            string ed = input.ed.ToString("yyyy-MM-dd");
            string query = @"SELECT FORMAT(SUM(amount_received),'.') as amount FROM dbo.fts_invoice WHERE payment_type=1 and invoice_date BETWEEN '"+sd+"' and  '"+ed+"' ";
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


        [Route("api/onlinepayment")]
        [HttpPost]
        public JsonResult Getonlinepayment(Date input)
        {
            string sd = input.sd.ToString("yyyy-MM-dd");
            string ed = input.ed.ToString("yyyy-MM-dd");
            string query = @"SELECT FORMAT(SUM(amount_received),'.') as amount FROM dbo.fts_invoice WHERE payment_type=10 or payment_type=6 or payment_type=12 or payment_type=13   and invoice_date BETWEEN '"+sd+"' and  '"+ed+"' ";
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


        [Route("api/cardpayment")]
        [HttpPost]
        public JsonResult Getcardpayment(Date input)
        {
            string sd = input.sd.ToString("yyyy-MM-dd");
            string ed = input.ed.ToString("yyyy-MM-dd");
            string query = @"SELECT FORMAT(SUM(amount_received),'.') as amount FROM dbo.fts_invoice WHERE   invoice_date BETWEEN '"+sd+"' and  '"+ed+"' and payment_type=8 OR payment_type=9";
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


        [Route("api/transferproduct")]
        [HttpPost]
        public JsonResult Gettransferproduct(Date input)
        {
            string sd = input.sd.ToString("yyyy-MM-dd");
            string ed = input.ed.ToString("yyyy-MM-dd");
            string query = @"SELECT COUNT(product_id) as transferproduct FROM dbo.fts_purchase_order_invoice_item_details where created_date_time between '"+sd+"' and '"+ed+"'";
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

        [Route("api/totalcollection")]
        [HttpPost]
        public JsonResult Gettotalcollection(Date input)
        {
            string sd = input.sd.ToString("yyyy-MM-dd");
            string ed = input.ed.ToString("yyyy-MM-dd");
            string query = @"SELECT FORMAT(SUM(amount_received),'.') as totalamount FROM dbo.fts_invoice WHERE invoice_date BETWEEN '"+sd+"' and  '"+ed+"'";
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

        [Route("api/busyhour")]
        [HttpPost]
        public JsonResult Getbusyhour(Date input)
        {
            string sd = input.sd.ToString("yyyy-MM-dd");
            string ed = input.ed.ToString("yyyy-MM-dd");
            string query = @"SELECT COUNT(id) as busyhour,DATEPART(HOUR,invoice_date) as time FROM dbo.fts_invoice where (invoice_date) BETWEEN '"+sd+"' AND '"+ed+"' GROUP BY DATEPART(HOUR,invoice_date) order by DATEPART(HOUR,invoice_date) asc";
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


        [Route("api/newcustomer")]
        [HttpPost]
        public JsonResult Getnewcustomer(Date input)
        {
            string sd = input.sd.ToString("yyyy-MM-dd");
            string ed = input.ed.ToString("yyyy-MM-dd");
            string query = @"select COUNT(userid) as newcustomer from dbo.fts_clients$ where created_date_time between '"+sd+"' and '"+ed+"'";
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
