


CREATE TABLE [dbo].[UBER_EATS_FRANCE_BELGIUM]
(
 
 [Order_ID]											 varchar(150)
,[Workflow_ID]                                       varchar(150)
,[Store_Name]                                        varchar(150)
,[Store_ID]                                          varchar(150)
,[Order_Date_OR_Refund_Date]                         date 
,[Order_Accept_Time]                                 varchar(150)
,[Dining_Mode]                                       varchar(150)
,[Order_Channel]                                     varchar(150)
,[Food_Sales_excl_VAT]                               decimal(16,6)
,[VAT1_on_Food_Sales]                                decimal(16,6)
,[VAT2_on_Food_Sales]                                decimal(16,6)
,[VAT3_on_Food_Sales]                                decimal(16,6)
,[Food_Sales_incl_VAT]                               decimal(16,6)
,[Discount_onFood_excl_VAT]                          decimal(16,6)
,[VAT_1_on_Discount_on_Food]                         decimal(16,6)
,[VAT_2_on_Discount_on_Food]                         decimal(16,6)
,[VAT_2_on_Discount_on_Food_]                        decimal(16,6)
,[Discount_on_Food_incl_VAT]                         decimal(16,6)
,[Marketing_Service_Fee_Adjustment]                  decimal(16,6)
,[Price_Adjustments_excluding_VAT]                   decimal(16,6)
,[Payout_from_3rd_party]                             decimal(16,6)
,[3rd_party_payor]                                   varchar(150)
,[VAT_on_Price_Adjustments]                          decimal(16,6)
,[Price_Adjustments_Including_VAT]                   decimal(16,6)
,[Delivery_Fee_excl_VAT]                             decimal(16,6)
,[VAT1_on_Delivery_Fee]                              decimal(16,6)
,[VAT2_on_Delivery_Fee]                              decimal(16,6)
,[VAT3_on_Delivery_Fee]                              decimal(16,6)
,[Delivery_Fee_incl_VAT]                             decimal(16,6)

,[Special_Offer_On_Delivery_excl_VAT]                decimal(16,6)
,[VAT_On Special Offer_On_Delivery]                  decimal(16,6)
,[Special_Offer_On_Delivery_incl_VAT]                decimal(16,6)


,[Total_Order_incl_VAT]                              decimal(16,6)
,[Restaurant_to_Customer_Invoice_Link]               varchar(150)
,[Cost_of_Delivery_excl_VAT]                         decimal(16,6)
,[VAT_on_cost_of_delivery]                           decimal(16,6)
,[Cost_of_Delivery_incl_VAT]                         decimal(16,6)
,[Courier_to_Restaurant_Invoice_Link]                varchar(150)
,[Uber_Service_Fees_before_Discount_ex_VAT]          decimal(16,6)
,[Service_Fee_Discount_ex_VAT]                       decimal(16,6)
,[Uber_Service_Fee_after_Discount_ex_VAT]            decimal(16,6)
,[VAT_on_Uber_Service_Fees_after_Discount]           varchar(150)
,[Uber_Service_Fee_after_Discount_incl_VAT]          varchar(150)
,[Uber_to_Restaurant_Invoice_Link]                   varchar(150)
,[VAT_Adjustment]                                    decimal(16,6)
,[Profit_on_Delivery]                                decimal(16,6)
,[Gratuity]                                          decimal(16,6)
,[Misc_Payment_Description]                          varchar(150)
,[Misc_Payments_incl_VAT]                            decimal(16,6)
,[Payout]                                            decimal(16,6)
,[Payout_Date]                                       date
,[Order_Status]                                      varchar(150)
	 

  
,[T_Filename]						 varchar(255)
,[T_Created_TS]						 datetime2(0)

) ON [PRIMARY]

GO

ALTER TABLE [dbo].[UBER_EATS_FRANCE_BELGIUM] ADD  CONSTRAINT [DF_dbo_UBER_FRANCE_BELGIUM_T_Created_TS]  DEFAULT (getdate()) FOR [T_Created_TS]
GO

ALTER TABLE [dbo].[UBER_EATS_FRANCE_BELGIUM] ADD  CONSTRAINT [DF_dbo_UBER_FRANCE_BELGIUM_Store_ID]  DEFAULT ('N/A') FOR [Store_ID]
GO



CREATE CLUSTERED INDEX idx_cs_uber_eats_france_belgium on  [dbo].[UBER_EATS_FRANCE_BELGIUM] (Order_Date_OR_Refund_Date,Store_ID)
WITH (DATA_COMPRESSION = PAGE)
 