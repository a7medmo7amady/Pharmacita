SELECT TOP (1000) [C_ID]
      ,[C_NAME]
      ,[C_phone]
      ,[C_address]
      ,[PAYMENT_INFO]
      ,[C_age]
  FROM [Pharmacita].[dbo].[Customer]


SELECT TOP (1000) [E_ID]
      ,[E_NAME]
      ,[E_phone]
      ,[E_address]
      ,[E_salary]
      ,[E_role]
      ,[E_age]
      ,[Hire_date]
      ,[E_department]
  FROM [Pharmacita].[dbo].[Employee]

  SELECT TOP (1000) [Product_ID]
      ,[Product_name]
      ,[Product_type]
      ,[Production_date]
      ,[Expiration_Date]
      ,[Quantity_IN_Stock]
      ,[Product_Description]
      ,[Supplier_ID]
      ,[Product_price]
  FROM [Pharmacita].[dbo].[Products]

  SELECT TOP (1000) [Supplier_ID]
      ,[Sup_Name]
      ,[Sup_address]
      ,[DeliveryDay]
      ,[BankName]
      ,[Accountnumber]
      ,[Payment_Terms]
  FROM [Pharmacita].[dbo].[Supplier]

  SELECT TOP (1000) [LT_ID]
      ,[LT_name]
      ,[LT_category]
      ,[LT_price]
      ,[LT_description]
      ,[Result_time_frame]
  FROM [Pharmacita].[dbo].[LabTests]

SELECT TOP (1000) [O_ID]
      ,[C_ID]
      ,[Order_Date]
      ,[P_ID]
  FROM [Pharmacita].[dbo].[Orders]

  SELECT TOP (1000) [admin_username]
      ,[admin_password]
  FROM [Pharmacita].[dbo].[Adminauth]

  SELECT TOP (1000) [customer_username]
      ,[customer_password]
  FROM [Pharmacita].[dbo].[CustomerAuth]

  DELETE FROM Customer;
  DELETE FROM Supplier;
    DELETE FROM Orders;




