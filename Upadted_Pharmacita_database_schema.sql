
-- Create tables for the database schema

--Done
CREATE TABLE Customer (
    C_ID INT IDENTITY(1,1) PRIMARY KEY,
    C_NAME NVARCHAR(100),
    C_phone NVARCHAR(11),
    C_address NVARCHAR(255),
    PAYMENT_INFO NVARCHAR(255), -- credit card number
    E_age INT
	-- ORDER_HISTORY will be orders' IDs in orders table
);
--Done
CREATE TABLE Employee (
    E_ID INT IDENTITY(1,1) PRIMARY KEY,
    E_NAME NVARCHAR(100),
    E_phone NVARCHAR(11),
    E_address NVARCHAR(255),
    E_salary INT,
    E_role NVARCHAR(50),
    E_age INT,
    Hire_date DATE
);
--Done
CREATE TABLE Supplier (
    Supplier_ID INT IDENTITY(1,1) PRIMARY KEY,
    Sup_Name NVARCHAR(100),
    Sup_address NVARCHAR(255),
    DeliveryDay NVARCHAR(3), --SAT, SUN, MON, TUE, WED, THU, FRI
	BankName NVARCHAR(255),
    Accountnumber NVARCHAR(255),
    Payment_Terms NVARCHAR(3), -- Cash before shipment (CBS) / Cash on delivery (COD)
    --products supplied will be refernced with the product
);
--Done
CREATE TABLE Orders (
    O_ID INT IDENTITY(1,1) PRIMARY KEY,
    C_ID INT FOREIGN KEY REFERENCES Customer(C_ID),
	Order_Date Date
);
--Done
CREATE TABLE Products (
    Product_ID INT IDENTITY(1,1) PRIMARY KEY,
    Product_name NVARCHAR(100),
	Product_type NVARCHAR(100),
    Production_date DATE,
	Expiration_Date DATE,
    Quantity_IN_Stock INT,
    Product_Description NVARCHAR(255),
    Supplier_ID INT FOREIGN KEY REFERENCES Supplier(Supplier_ID)
);
--Done
CREATE TABLE LabEmployee (
    Lab_E_ID INT PRIMARY KEY FOREIGN KEY REFERENCES Employee(E_ID),
    License NVARCHAR(50) --License code 
);
--Done
CREATE TABLE LabTests (
    LT_ID INT IDENTITY(1,1) PRIMARY KEY,
    LT_name NVARCHAR(100),
    LT_category NVARCHAR(50), --blood, stool, urine
    LT_price INT,
    LT_description NVARCHAR(255),
    Result_time_frame NVARCHAR(50) --time for the result to come out
);
--Done
CREATE TABLE ProductsInOrder (
    O_ID INT FOREIGN KEY REFERENCES Orders(O_ID),
    Product_ID INT FOREIGN KEY REFERENCES Products(Product_ID),
    Quantity INT, -- Quantity of each product in the order
    PRIMARY KEY (O_ID, Product_ID)
);

-- Product categories
--Done
CREATE TABLE Skincare (
    Product_ID INT PRIMARY KEY FOREIGN KEY REFERENCES Products(Product_ID),
    SKC_Skintype NVARCHAR(50), --dry, normal, oily
    SKC_SPFLevel INT,
    SKC_brand NVARCHAR(50)
);
--Done
CREATE TABLE Haircare (
    Product_ID INT PRIMARY KEY FOREIGN KEY REFERENCES Products(Product_ID),
    HC_Hairtype NVARCHAR(6), --dry, normal, oily
    HC_SPFLevel INT,
    HC_brand NVARCHAR(50),
);
--Done
CREATE TABLE Perfumes (
    Product_ID INT PRIMARY KEY FOREIGN KEY REFERENCES Products(Product_ID),
    PER_Brand NVARCHAR(50),
    Gender_Suitability CHAR -- M, F, U
);
--Done
CREATE TABLE BabyCare (
    Product_ID INT PRIMARY KEY FOREIGN KEY REFERENCES ProductS(Product_ID),
	Age_Range  NVARCHAR(10) -- 2-3/ 4-12 months
);
--Done
CREATE TABLE Accessories (
    Product_ID INT PRIMARY KEY FOREIGN KEY REFERENCES Products(Product_ID),
    ACC_Brand NVARCHAR(50),
);
--Done
CREATE TABLE Restricted_MED (
    Product_ID INT PRIMARY KEY FOREIGN KEY REFERENCES Products(Product_ID),
    RC_category NVARCHAR(50), --Narcotic, Specialty, Hazardous, Controlled, Psychotropic 
    PRESCRIPTION_REQ NVARCHAR(50)
);
--Done
CREATE TABLE Medicine (
    Product_ID INT PRIMARY KEY FOREIGN KEY REFERENCES Products(Product_ID),
    MD_category NVARCHAR(50) --cream, spray, pills, drink
);
--Done
CREATE TABLE FirstAidKit (
    Product_ID INT PRIMARY KEY FOREIGN KEY REFERENCES Products(Product_ID),
    Kit_Manufacturer NVARCHAR(50),
    ContentList NVARCHAR(MAX)
);


-- Relations
--Done
CREATE TABLE Conduct (
    LT_ID INT FOREIGN KEY REFERENCES LabTests(LT_ID),
    Lab_E_ID INT FOREIGN KEY REFERENCES LabEmployee(Lab_E_ID),
    PRIMARY KEY (LT_ID, Lab_E_ID)
);
--Done
CREATE TABLE Supervise (
    supervisor_E_ID INT FOREIGN KEY REFERENCES Employee(E_ID),
    supervisee_E_ID INT FOREIGN KEY REFERENCES Employee(E_ID),
    PRIMARY KEY (supervisor_E_ID, supervisee_E_ID)
);
--Done
CREATE TABLE Buy (
    O_ID INT FOREIGN KEY REFERENCES Orders(O_ID),
    C_ID INT FOREIGN KEY REFERENCES Customer(C_ID),
    PRIMARY KEY (O_ID, C_ID)
);
--Done
CREATE TABLE Contain ( 
    Product_ID INT FOREIGN KEY REFERENCES Products(Product_ID),
    O_ID INT FOREIGN KEY REFERENCES Orders(O_ID),
    PRIMARY KEY (Product_ID, O_ID)
);
--Done
CREATE TABLE Supplies (
    Product_ID INT FOREIGN KEY REFERENCES Products(Product_ID),
    Supplier_ID INT FOREIGN KEY REFERENCES Supplier(Supplier_ID),
    PRIMARY KEY (Product_ID, Supplier_ID)
);
--Done
CREATE TABLE Make (
    LT_ID INT FOREIGN KEY REFERENCES LabTests(LT_ID),
    C_ID INT FOREIGN KEY REFERENCES Customer(C_ID),
    PRIMARY KEY (LT_ID, C_ID)
);