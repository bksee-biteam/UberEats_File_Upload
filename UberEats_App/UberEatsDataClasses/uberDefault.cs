using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UberEats_Upload.DataClasses
{
    class uberDefault:UberEatsData
    {

        public uberDefault():base()
        {


            table.TableName = "STAGING_UBER_EATS_DEFAULT";

            table.Columns.Add("Store_Name"	, typeof(string));
            table.Columns.Add("Store_ID", typeof(string));
            table.Columns.Add("Order_ID", typeof(string));
            table.Columns.Add("Order_Date_OR_Refund_date", typeof(string));
            table.Columns.Add("Order_Accept_Time", typeof(string));
            table.Columns.Add("Food_Sales_excluding_tax", typeof(string));
            table.Columns.Add("Tax_on_Food_Sales", typeof(string));
            table.Columns.Add("Food_sales_including_tax", typeof(string));
            table.Columns.Add("Adjustments_excluding_tax", typeof(string));
            table.Columns.Add("Tax_on_Adjustments", typeof(string));
            table.Columns.Add("Promo_Spend_on_food", typeof(string));
            table.Columns.Add("Total_Sales_after_Adjustments_including_tax", typeof(string));
            table.Columns.Add("Uber_Service_Fee", typeof(string));
            table.Columns.Add("Dispatch_Fee", typeof(string));
            table.Columns.Add("Tax_on_Dispatch_Fee", typeof(string));
            table.Columns.Add("Misc_Payment_Description", typeof(string));
            table.Columns.Add("Miscellaneous_Payments", typeof(string));
            table.Columns.Add("Payout", typeof(string));
            table.Columns.Add("Payout_Date", typeof(string));
            table.Columns.Add("Order_Status", typeof(string));


            table.Columns.Add("T_File_Name", typeof(string));

        } 

    }
}
