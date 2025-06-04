# Pharmacy Management System Project Report

## 1. Analysis Section
### 1.1 Project Overview
The Pharmacy Management System aims to streamline the administration of pharmacy operations by providing a centralized interface to manage customers, orders, products, suppliers, employees, and lab tests. The system is designed to ensure efficient inventory management, customer order tracking, employee supervision, and supplier coordination.

### 1.2 Objectives
- Automate pharmacy operations and reduce manual errors.
- Provide a robust database for storing product, customer, and order information.
- Integrate lab tests and employee roles for comprehensive management.
- Track supplier information and delivery timelines.

### 1.3 Scope
- Customer Management (Orders, Payments, and History)
- Employee Supervision and Lab Operations
- Product Categorization and Inventory
- Supplier Relations and Delivery Tracking
- Lab Test Management and Conducting

## 2. Design Section
### 2.1 Database Schema
The schema consists of multiple tables representing the core entities of the pharmacy:
- **Customer** - Stores customer details and payment information.
- **Employee** - Maintains employee records.
- **Orders** - Tracks customer orders.
- **Products** - Contains all product details, including stock and supplier information.
- **Supplier** - Manages supplier details and delivery schedules.
- **LabEmployee** - Specialized employees who conduct lab tests.
- **LabTests** - Tracks tests conducted and results.

### 2.2 ER Diagram
The ER diagram shows entity relationships including:
- One-to-Many relationship between **Customer** and **Orders**.
- Many-to-Many relationship between **Orders** and **Products** through **ProductsInOrder**.
- One-to-Many relationship between **Supplier** and **Products**.
- Many-to-Many relationship between **LabTests** and **LabEmployee** via **Conduct**.
- Supervisory relationship between employees through **Supervise**.

### 2.3 Functional Modules
- **Order Management Module** - Handles customer orders and order fulfillment.
- **Product Management Module** - Manages inventory, categorization, and expiry dates.
- **Lab Test Module** - Tracks lab test information, results, and lab employees.
- **Supplier Module** - Monitors suppliers and their delivery timelines.

## 3. Implementation Section
### 3.1 Technologies
- **Backend** - ASP.NET (C#)
- **Frontend** - HTML/CSS with JavaScript for interactivity
- **Database** - Microsoft SQL Server Management Studio

### 3.2 Database Tables
- **Customer Table** - Manages customer data.
- **Orders Table** - Tracks orders and links to products.
- **Products Table** - Contains product details.
- **Supplier Table** - Holds supplier information.
- **LabTests Table** - Stores lab test data.

### 3.3 Key Code Snippets
Example of table creation (Customer):
```sql
CREATE TABLE Customer (
    C_ID INT IDENTITY(1,1) PRIMARY KEY,
    C_NAME NVARCHAR(100),
    C_phone NVARCHAR(11),
    C_address NVARCHAR(255),
    PAYMENT_INFO NVARCHAR(255),
    E_age INT
);
```


## 4. Workload Distribution
| Team Member             | Responsibility                                                               |
|-------------------------|------------------------------------------------------------------------------|
| Database Schema Design  |  Abdelrahman Abdelrassoul, Youssef Islam, Ahmed Mohammady, Abdelrahman Sayed |
| Frontend Development    |  Youssef Islam, Ahmed Mohammady                                              |
| Backend Logic           |  Abdelrahman Abdelrassoul, Abdelrahman Sayed                                 |   
| Integration & Testing   |  Abdelrahman Abdelrassoul                                                    |         


## 5. Conclusion
The pharmacy management system provides an efficient solution to manage the various operations of a pharmacy. The combination of robust database design and web-based admin interface ensures streamlined workflow and improved productivity.

