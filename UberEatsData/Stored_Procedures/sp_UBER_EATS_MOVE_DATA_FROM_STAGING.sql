

CREATE  PROCEDURE sp_UBER_EATS_MOVE_DATA_FROM_STAGING
WITH EXECUTE as 'dbo'
AS
BEGIN

BEGIN TRANSACTION;

		BEGIN TRY
		



		
				-------------UBER_EATS_DEFAULT-------------------
				
				
				
				--DELETE 
				
				DELETE T
				FROM [dbo].[UBER_EATS_DEFAULT] T INNER JOIN [dbo].[STAGING_UBER_EATS_DEFAULT]  S
				ON T.Store_ID = S.Store_ID
				AND (T.Order_Date_Or_Refund_Date = S.Order_Date_Or_Refund_Date OR (T.Payout_Date = S.Payout_Date  AND S.Order_Date_Or_Refund_Date IS NULL) )

				
				
				---INSERT
				
				
				INSERT INTO [dbo].[UBER_EATS_DEFAULT] WITH(TABLOCK)
				(Store_Name, Store_ID, Order_ID, Order_Date_Or_Refund_Date, Order_Accept_Time, Food_Sales_excluding_tax, Tax_On_Food_Sales, Food_sales_including_tax, Adjustments_excluding_tax, Tax_on_Adjustments, Promo_Spend_On_Food, Total_Sales_After_Adjustments_including_tax, Uber_Service_Fee, Dispatch_Fee, Tax_On_Dispatch_Fee, Misc_Payment_Description, Miscellaneous_Payments, Payout, Payout_Date, Order_Status, T_Filename, T_Created_TS)
				
				Select Store_Name, Store_ID, Order_ID, Order_Date_Or_Refund_Date, Order_Accept_Time, Food_Sales_excluding_tax, Tax_On_Food_Sales, Food_sales_including_tax, Adjustments_excluding_tax, Tax_on_Adjustments, Promo_Spend_On_Food, Total_Sales_After_Adjustments_including_tax, Uber_Service_Fee, Dispatch_Fee, Tax_On_Dispatch_Fee, Misc_Payment_Description, Miscellaneous_Payments, Payout, Payout_Date, Order_Status, T_Filename, T_Created_TS
				FROM [dbo].[STAGING_UBER_EATS_DEFAULT];
				
				
				
				
				-------------UBER_EATS_FRANCE_BELGIUM-------------------
				
				
				--DELETE 
				
				DELETE T
				FROM [dbo].[UBER_EATS_FRANCE_BELGIUM] T INNER JOIN  [dbo].[STAGING_UBER_EATS_FRANCE_BELGIUM]  S
				ON T.Store_ID = S.Store_ID
					AND (T.Order_Date_Or_Refund_Date = S.Order_Date_Or_Refund_Date OR (T.Payout_Date = S.Payout_Date  AND S.Order_Date_Or_Refund_Date IS NULL) )
			 
				
				
				---INSERT
				
				
	 			INSERT INTO [dbo].[UBER_EATS_FRANCE_BELGIUM] WITH(TABLOCK)
				(Order_ID,Workflow_ID,Store_Name,Store_ID,Order_Date_OR_Refund_Date,Order_Accept_Time,Dining_Mode,Order_Channel,Food_Sales_excl_VAT,VAT1_on_Food_Sales,VAT2_on_Food_Sales,VAT3_on_Food_Sales,Food_Sales_incl_VAT,Discount_onFood_excl_VAT,VAT_1_on_Discount_on_Food,VAT_2_on_Discount_on_Food,VAT_2_on_Discount_on_Food_,Discount_on_Food_incl_VAT,Marketing_Service_Fee_Adjustment,Price_Adjustments_excluding_VAT,Payout_from_3rd_party,[3rd_party_payor],VAT_on_Price_Adjustments,Price_Adjustments_Including_VAT,Delivery_Fee_excl_VAT,VAT1_on_Delivery_Fee,VAT2_on_Delivery_Fee,VAT3_on_Delivery_Fee,Delivery_Fee_incl_VAT, [Special_Offer_On_Delivery_excl_VAT],[VAT_On Special Offer_On_Delivery],[Special_Offer_On_Delivery_incl_VAT], 	Total_Order_incl_VAT,Restaurant_to_Customer_Invoice_Link,Cost_of_Delivery_excl_VAT,VAT_on_cost_of_delivery,Cost_of_Delivery_incl_VAT,Courier_to_Restaurant_Invoice_Link,Uber_Service_Fees_before_Discount_ex_VAT,Service_Fee_Discount_ex_VAT,Uber_Service_Fee_after_Discount_ex_VAT,VAT_on_Uber_Service_Fees_after_Discount,Uber_Service_Fee_after_Discount_incl_VAT,Uber_to_Restaurant_Invoice_Link,VAT_Adjustment,Profit_on_Delivery,Gratuity,Misc_Payment_Description,Misc_Payments_incl_VAT,Payout,Payout_Date,Order_Status,T_Filename,T_Created_TS)
				
				SELECT Order_ID,Workflow_ID,Store_Name,Store_ID,Order_Date_OR_Refund_Date,Order_Accept_Time,Dining_Mode,Order_Channel,Food_Sales_excl_VAT,VAT1_on_Food_Sales,VAT2_on_Food_Sales,VAT3_on_Food_Sales,Food_Sales_incl_VAT,Discount_onFood_excl_VAT,VAT_1_on_Discount_on_Food,VAT_2_on_Discount_on_Food,VAT_2_on_Discount_on_Food_,Discount_on_Food_incl_VAT,Marketing_Service_Fee_Adjustment,Price_Adjustments_excluding_VAT,Payout_from_3rd_party,3rd_party_payor,VAT_on_Price_Adjustments,Price_Adjustments_Including_VAT,Delivery_Fee_excl_VAT,VAT1_on_Delivery_Fee,VAT2_on_Delivery_Fee,VAT3_on_Delivery_Fee,Delivery_Fee_incl_VAT,[Special_Offer_On_Delivery_excl_VAT],[VAT_On Special Offer_On_Delivery],[Special_Offer_On_Delivery_incl_VAT] ,Total_Order_incl_VAT,'' AS Restaurant_to_Customer_Invoice_Link,Cost_of_Delivery_excl_VAT,VAT_on_cost_of_delivery,Cost_of_Delivery_incl_VAT,'' AS Courier_to_Restaurant_Invoice_Link,Uber_Service_Fees_before_Discount_ex_VAT,Service_Fee_Discount_ex_VAT,Uber_Service_Fee_after_Discount_ex_VAT,VAT_on_Uber_Service_Fees_after_Discount,Uber_Service_Fee_after_Discount_incl_VAT,'' AS Uber_to_Restaurant_Invoice_Link,VAT_Adjustment,Profit_on_Delivery,Gratuity,Misc_Payment_Description,Misc_Payments_incl_VAT,Payout,Payout_Date,Order_Status,T_Filename,T_Created_TS
				FROM [dbo].[STAGING_UBER_EATS_FRANCE_BELGIUM];
		
		
		COMMIT TRANSACTION;
		
		END TRY
		
		
		BEGIN CATCH
		
				IF @@TRANCOUNT > 0
				BEGIN
					ROLLBACK TRANSACTION;
				END
		
		
		END CATCH



END