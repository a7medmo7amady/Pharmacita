USE Pharmacita;


--Customer
-- Create
INSERT INTO Customer (C_NAME, C_address)
VALUES ('Abdelrahman', 'Giza');
INSERT INTO Customer (C_NAME, C_address)
VALUES ('Youssef', 'Ashgar');
INSERT INTO Customer (C_NAME, C_address)
VALUES ('Mohamady', 'Haram');
-- Read
SELECT * FROM Customer;
SELECT * FROM Customer WHERE C_ID = 1;

-- Update
UPDATE Customer
SET C_address = 'Cairo'
WHERE C_ID = 1;

-- Delete
DELETE FROM Customer
WHERE C_ID = 2;


-----------------------------------------------------------------


--Orders
-- Create
INSERT INTO Orders (C_ID, Order_Date)
VALUES (1, '2024-12-06');

-- Read
SELECT * FROM Orders;
SELECT * FROM Orders WHERE C_ID = 1;

-- Update
UPDATE Orders
SET Order_Date = '2024-12-07'
WHERE O_ID = 1;

-- Delete
DELETE FROM Orders
WHERE O_ID = 1;


-----------------------------------------------------------------


--Products
-- Create
INSERT INTO Products (Product_Name, Product_Price, Product_type)
VALUES ('Shampoo', 10, 'Haircare');

-- Read
SELECT * FROM Products;
SELECT * FROM Products WHERE Price < 15;

-- Update
UPDATE Products
SET Price = 9
WHERE Product_ID = 1;

-- Delete
DELETE FROM Products
WHERE Product_ID = 1;


-----------------------------------------------------------------


--BASED ON PRODUCT TYPE IN THE FRONT END THE FORM OF THE ADDITION OF A PRODUCT THE FORM WILL BE DETERMINED


-----------------------------------------------------------------


--Skincare
-- Create
INSERT INTO Skincare (SKC_brand, SKC_SPFLevel)
VALUES ('Nivea', 30);

-- Read
SELECT * FROM Skincare;
SELECT * FROM Skincare WHERE Product_Name = 'Moisturizer';

-- Update
UPDATE Skincare
SET SKC_SPFLevel = 50
WHERE Product_ID = 1;

-- Delete
DELETE FROM Skincare
WHERE Product_ID = 1;


-----------------------------------------------------------------


--Haircare
-- Create
INSERT INTO Haircare (hC_brand, HC_SPFLevel)
VALUES ('Clear', 30);

-- Read
SELECT * FROM Haircare;
SELECT * FROM Haircare WHERE Brand = 'Clear';

-- Update
UPDATE Haircare
SET Brand = 'Head & Shoulders'
WHERE Product_ID = 1;

-- Delete
DELETE FROM Haircare
WHERE Product_ID = 1;


-----------------------------------------------------------------


--Perfumes
-- Create
INSERT INTO Perfumes (PER_Brand, Gender_Suitability)
VALUES ('Creed ', 'M');

-- Read
SELECT * FROM Perfumes;
SELECT * FROM Perfumes WHERE PER_Brand = 'Creed';

-- Update
UPDATE Perfumes
SET PER_Brand = 'Versace'
WHERE Product_ID = 1;

-- Delete
DELETE FROM Perfumes
WHERE Product_ID = 1;


-----------------------------------------------------------------


--Babycare
-- Create
INSERT INTO BabyCare (Brand)
VALUES ('Molfix', '2-6');

-- Read
SELECT * FROM BabyCare;
SELECT * FROM BabyCare WHERE Brand = 'Molfix';

-- Update
UPDATE BabyCare
SET Brand = 'Babyjoy'
WHERE Product_ID = 1;

-- Delete
DELETE FROM BabyCare
WHERE Product_ID = 1;


-----------------------------------------------------------------


--Accessories
-- Create
INSERT INTO Accessories (ACC_Brand)
VALUES ('Gucci');

-- Read
SELECT * FROM Accessories;
SELECT * FROM Accessories WHERE ACC_Brand = 'Gucci';

-- Update
UPDATE Accessories
SET ACC_Brand = 'Zara'
WHERE Product_ID = 1;

-- Delete
DELETE FROM Accessories
WHERE Product_ID = 1;


-----------------------------------------------------------------


--Restricted_MED
-- Create
INSERT INTO Restricted_MED (RC_category, PRESCRIPTION_REQ)
VALUES ('Narcotic', 'Doctor');

-- Read
SELECT * FROM Restricted_MED;
SELECT * FROM Restricted_MED WHERE RC_category = 'Narcotic';

-- Update
UPDATE Restricted_MED
SET RC_category = 'Specialty' 
WHERE Product_ID = 1;

-- Delete
DELETE FROM Restricted_MED
WHERE Product_ID = 1;


-----------------------------------------------------------------


--Restricted_MED
-- Create
INSERT INTO Medicine (MD_category)
VALUES ('pills');

-- Read
SELECT * FROM Medicine;
SELECT * FROM Medicine WHERE MD_category = 'pills';

-- Update
UPDATE Medicine
SET MD_category = 'drink'
WHERE Product_ID = 1;

-- Delete
DELETE FROM Medicine
WHERE Product_ID = 1;


-----------------------------------------------------------------


--FirstAidKit
-- Create
INSERT INTO FirstAidKit (Kit_Manufacturer, ContentList)
VALUES ('EL-Salam','siccors/Bandaids/cotton/sanitizer');

-- Read
SELECT * FROM FirstAidKit;
SELECT * FROM FirstAidKit WHERE Kit_Manufacturer = 'EL-Salam';

-- Update
UPDATE FirstAidKit
SET Kit_Manufacturer = 'EL-Nile'
WHERE Product_ID = 1;

-- Delete
DELETE FROM FirstAidKit
WHERE Product_ID = 1;


-----------------------------------------------------------------


--ProductsInOrder 
-- Create
INSERT INTO Order_Product (O_ID, Product_ID, Quantity)
VALUES (1, 1, 2);

-- Read
SELECT * FROM ProductsInOrder WHERE O_ID = 1;

-- Update
UPDATE Order_Product
SET Quantity = 3
WHERE O_ID = 1 AND Product_ID = 1;

-- Delete
DELETE FROM ProductsInOrder
WHERE O_ID = 1;


-----------------------------------------------------------------


--LabEmployee  
-- Create
INSERT INTO LabEmployee (E_ID, License)
VALUES (1, '213564952');

-- Read
SELECT * FROM LabEmployee;
SELECT * FROM LabEmployee WHERE License = '213564952';

-- Update
UPDATE LabEmployee
SET License = '236519735'
WHERE E_ID = 1;

-- Delete
DELETE FROM LabEmployee
WHERE E_ID = 1;


-----------------------------------------------------------------


--LabTests  
-- Create
INSERT INTO LabTests (LT_ID, Test_Name)
VALUES (1, 'Blood');

-- Read
SELECT * FROM LabTests;
SELECT * FROM LabTests WHERE LT_ID = 1;

-- Update
UPDATE LabTests
SET Test_Name = 'Urine'
WHERE LT_ID = 1;

-- Delete
DELETE FROM LabTests
WHERE LT_ID = 1;


-----------------------------------------------------------------


--Employee  
-- Create
INSERT INTO Employee (E_ID, E_Name, E_Role)
VALUES (1, 'Youssef', 'Lab Employee');

-- Read
SELECT * FROM Employee;
SELECT * FROM Employee WHERE E_Role = 'Lab Employee';

-- Update
UPDATE Employee
SET E_Role = 'Pharmacist'
WHERE E_ID = 1;

-- Delete
DELETE FROM Employee
WHERE E_ID = 1;