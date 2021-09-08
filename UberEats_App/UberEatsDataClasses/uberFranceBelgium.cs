using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UberEats_Upload.DataClasses
{
    class uberFranceBelgium : UberEatsData
    {

        public uberFranceBelgium() : base()
        {


            table.TableName = "STAGING_UBER_EATS_FRANCE_BELGIUM";

            table.Columns.Add("Order_ID", typeof(string));
            table.Columns.Add("Workflow_ID", typeof(string));
            table.Columns.Add("Store_Name", typeof(string));
            table.Columns.Add("Store_ID", typeof(string));
            table.Columns.Add("Order_Date_Or_Refund_Date", typeof(string));
            table.Columns.Add("Order_Accept_Time", typeof(string));
            table.Columns.Add("Dining_Mode", typeof(string));
            table.Columns.Add("Order_Channel", typeof(string));
            table.Columns.Add("Food_Sales_excl_VAT", typeof(string));
            table.Columns.Add("VAT1_on_Food_Sales", typeof(string));
            table.Columns.Add("VAT2_on_Food_Sales", typeof(string));
            table.Columns.Add("VAT3_on_Food_Sales", typeof(string));
            table.Columns.Add("Food_Sales_incl_VAT", typeof(string));
            table.Columns.Add("Discount_onFood_excl_VAT", typeof(string));
            table.Columns.Add("VAT_1_on_Discount_on_Food", typeof(string));
            table.Columns.Add("VAT_2_on_Discount_on_Food", typeof(string));
            table.Columns.Add("VAT_2_on_Discount_on_Food_", typeof(string));
            table.Columns.Add("Discount_on_Food_incl_VAT", typeof(string));
            table.Columns.Add("Marketing_Service_Fee_Adjustment", typeof(string));
            table.Columns.Add("Price_Adjustments_excluding_VAT", typeof(string));
            table.Columns.Add("Payout_from_3rd_party", typeof(string));
            table.Columns.Add("3rd_party_payor", typeof(string));
            table.Columns.Add("VAT_on_Price_Adjustments", typeof(string));
            table.Columns.Add("Price_Adjustments_Including_VAT", typeof(string));
            table.Columns.Add("Delivery_Fee_excl_VAT", typeof(string));
            table.Columns.Add("VAT1_on_Delivery_Fee", typeof(string));
            table.Columns.Add("VAT2_on_Delivery_Fee", typeof(string));
            table.Columns.Add("VAT3_on_Delivery_Fee", typeof(string));
            table.Columns.Add("Delivery_Fee_incl_VAT", typeof(string));

            table.Columns.Add("Special_Offer_On_Delivery_excl_VAT", typeof(string));
            table.Columns.Add("VAT_On Special Offer_On_Delivery", typeof(string));
            table.Columns.Add("Special_Offer_On_Delivery_incl_VAT", typeof(string));

            table.Columns.Add("Total_Order_incl_VAT", typeof(string));
            table.Columns.Add("Restaurant_to_Customer_Invoice_Link", typeof(string));
            table.Columns.Add("Cost_of_Delivery_excl_VAT", typeof(string));
            table.Columns.Add("VAT_on_cost_of_delivery", typeof(string));
            table.Columns.Add("Cost_of_Delivery_incl_VAT", typeof(string));
            table.Columns.Add("Courier_to_Restaurant_Invoice_Link", typeof(string));
            table.Columns.Add("Uber_Service_Fees_before_Discount_ex_VAT", typeof(string));
            table.Columns.Add("Service_Fee_Discount_ex_VAT", typeof(string));
            table.Columns.Add("Uber_Service_Fee_after_Discount_ex_VAT", typeof(string));
            table.Columns.Add("VAT_on_Uber_Service_Fees_after_Discount", typeof(string));
            table.Columns.Add("Uber_Service_Fee_after_Discount_incl_VAT", typeof(string));
            table.Columns.Add("Uber_to_Restaurant_Invoice_Link", typeof(string));
            table.Columns.Add("VAT_Adjustment", typeof(string));
            table.Columns.Add("Profit_on_Delivery", typeof(string));
            table.Columns.Add("Gratuity", typeof(string));
            table.Columns.Add("Misc_Payment_Description", typeof(string));
            table.Columns.Add("Misc_Payments_incl_VAT", typeof(string));
            table.Columns.Add("Payout", typeof(string));
            table.Columns.Add("Payout_Date", typeof(string));
            table.Columns.Add("Order_Status", typeof(string));
            table.Columns.Add("Payout_reference_ID", typeof(string));

            table.Columns.Add("T_File_Name", typeof(string));

        }

    }
}
