CREATE TABLE [dbo].[UBER_EATS_DEFAULT]
(
 
 [Store_Name]	                                      varchar(150)
,[Store_ID]                                           varchar(150)
,[Order_ID]                                           varchar(150)
,[Order_Date_Or_Refund_Date]                          date
,[Order_Accept_Time]                                  varchar(150)
,[Food_Sales_excluding_tax]                           decimal(16,6)
,[Tax_On_Food_Sales]                                  decimal(16,6)
,[Food_sales_including_tax]                           decimal(16,6)
,[Adjustments_excluding_tax]                          decimal(16,6)
,[Tax_on_Adjustments]                                 decimal(16,6)
,[Promo_Spend_On_Food]                                decimal(16,6)
,[Total_Sales_After_Adjustments_including_tax]        decimal(16,6)
,[Uber_Service_Fee]                                   decimal(16,6)
,[Dispatch_Fee]                                       decimal(16,6)
,[Tax_On_Dispatch_Fee]                                decimal(16,6)
,[Misc_Payment_Description]                           varchar(150)
,[Miscellaneous_Payments]                             decimal(16,6)
,[Payout]                                             decimal(16,6)
,[Payout_Date]                                        date
,[Order_Status]                                       varchar(150)
	 

  
,[T_Filename]						 varchar(255)
,[T_Created_TS]						 datetime2(0)

) ON [PRIMARY]

GO

ALTER TABLE [dbo].[UBER_EATS_DEFAULT] ADD  CONSTRAINT [DF_dbo_UBER_EATS_DEFAULT_T_Created_TS]  DEFAULT (getdate()) FOR [T_Created_TS]
GO
ALTER TABLE [dbo].[UBER_EATS_DEFAULT] ADD  CONSTRAINT [DF_dbo_UBER_EATS_DEFAULT_Store_ID]  DEFAULT ('N/A') FOR [Store_ID]
GO

CREATE CLUSTERED INDEX idx_cs_uber_eats_default on  [dbo].[UBER_EATS_DEFAULT] (Order_Date_Or_Refund_Date,Store_ID)
WITH (DATA_COMPRESSION = PAGE)
 