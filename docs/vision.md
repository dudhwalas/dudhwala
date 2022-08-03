# Vision
{{app_name}}'s vision is to automate and digitize business process & workflow of {{app_for}} by providing first class and complete end to end software solution.

## Business And Company 💼

**{{app_for}} as a business** at it's core - home delivers {{product}} to Customer. Customer subscribes to get the products delivered at their door step and pay for the product as well as provided service at agreed interval (weekly, monthly, quaterly...)

**{{app_for}} as a company** focuses on successful implementation of core business operations, thereby ensures gaining value for company. Company has various stake holders who play important roles in different areas of bussiness operation and workflow. 

{{app_name}} identifies these roles & their responsibilities across bussiness processes and develops a standard bussiness workflow through software automation.

# Discovery Phase 🔍
Roles & Responsibilities, Business Processes and Core Entities & Relationships are identified as a part of discovery phase for {{app_name}}.

## Roles & Responsibilties 👥

|Sr No.|Role|Responsibility|
|:-|:-|:-|
|1.|Owner|<ol><li>Owns the milk and product distribution company.</li><li>Invests in company. Creates financial budget & sales forecast and make sure company meets them.</li><li>Defines workflow and processes for daily business operations.</li><li>Sets strategies and have plans for future vision, mission and growth of company.</li><li>View sales statistics and financial reports.</li><li>May involve in businees marketing and campaigns.</li><li>May involve in customer support to improve overall customer service experience.</li><li>Final descision maker with all the rights.</li></ol>|
|2.|Customer|<ol><li>Consumer of {{product}}.</li><li>Subscribes to get {{product}} delivered at door step on quota basis.</li><li>Take deliveries of {{product}} from delivery squad.</li><li>Makes payment for consumed {{product}} as well as services.</li><li>Source of income for {{app_for}} and company.</li><ol>|
|3.|Administrator|<ol><li>Handles complete business operations as per defined workflow and process.</li><li>Records and manage catalog of {{product}}.</li><li>Records and manage profiles of Customer.</li><li>Records and map quantified {{product}} to Customer as per request.</li><li>Records and manage profiles of delivery squad</li><li>Pay salary to delivery squad.</li><li>Records and assign {{product}} to delivery squad for deliveries to Customer.</li><li>Records and manage Customer subscriptions.</li><li>Generates invoices based on records and send it to Customer(through delivery squad) for payment.</li><li>Collects invoice payment from Customer and records it.</li><li>Generates sales statistics and financial reports from records.</li><ol>|
|4.|Delivery Squad|<ol><li>Handles and ensures daily delivery of {{product}} at door step of Customer.</li><li>Records the delivery details - Customer number, {{product}} detail, Quantity and Date Time.</li><li>Handovers invoice received from administrator to Customer.</li><li>Can collect invoice payment from Customer and records it.</li><li>Take salary payment from administrator.</li><ol>|

## Business Processes 🛠️

To run the business successfully and effeciently, {{app_for}} carry out number of core business processes and operations.

### 1. Product Creation Process

[filename](diagram/product_creation_process.drawio ':include :type=code')

|Name Of Process|Product Creation Process|
|:--|:--|
|**Process Owner**|Administrator|
|**Description**|Administrator creates new {{product}} in product catalog.|
|**Actors**|<ol><li>Administrator</li><li>Manufacturer</li><li>Product Catalog</li><ol>|
|**Process Input**|{{product}} details - Name, Brand, Available In Quantity And Price.|
|**Process Output**|{{product}} details are recorded in product catalog and ready to get map to end customer for delivery.|
|**Process Flow**|<ol><li>The process starts when administrator requests {{product}} details from manufacturing company. {{product}} details - Name, Brand, Available In Quantity And Price. (Input)</li><li>Upon getting customer details, administrator records {{product}} details in product catalog.</li><li>The process ends with product details getting recorded in product catalog and ready to get map to end customer for delivery. (Output)</li><ol>|
|**Process Boundary**|<ul><li>The starting boundary of process is defined by administrator asking manufacturer company to provide necessary product details.</li><li>The ending boundary of process is defined by {{product}} getting recorded with details in product catalog and ready to get map to end customer for delivery.</li></ul>|
|**Exceptions To Normal Process Flow**|In step-2, if manufacturer doesn't provide all necessary product details then administrator re-requests for details and the Product Creation Process will begin again.|
|**Control Points and Measurements**|In step-3,<ul><li>Administrator checks if {{product}} details already exists in product catalog.</li><li>If administrator finds product details then administrator updates existing product OR else creates new product in product catalog.</li></ul>|

### 2. Delivery Squad Recruitment Process

[filename](diagram/recruit_delivery_squad_process.drawio ':include :type=code')

|Name Of Process|Delivery Squad Recruitment Process|
|:--|:--|
|**Process Owner**|Administrator|
|**Description**|Administrator recruits new delivery squad member for delivery of {{product}}.|
|**Actors**|<ol><li>Delivery Squad Member</li><li>Administrator</li><li>Delivery Squad Journal</li><ol>|
|**Process Input**|Delivery squad member details - Fullname, Address, Phone Number, Email Address, Profile Pic and Idenitity Proof. Salary per month is agreed by administrator.|
|**Process Output**|Delivery squad member details are recorded in delivery squad journal and ready to get map for delivery of {{product}}.|
|**Process Flow**|<ol><li>Administrator asks delivery squad member to provide necessary details. Details - Fullname, Address, Phone Number, Email Address, Profile Pic and Idenitity Proof. Salary per month is agreed by administrator. (Input)</li><li>Upon getting delivery squad member details, administrator records detail in delivery squad journal.</li><li>The process ends with customer getting recruited and ready to get map for delivery of {{product}}. (Output)</li><ol>|
|**Process Boundary**|<ul><li>The starting boundary of process is defined by administrator asking delivery squad member to provide necessary details.</li><li>The ending boundary of process is defined by delivery squad member getting recruited with details recorded in delivery squad journal and ready to get map for delivery of {{product}}.</li></ul>|
|**Exceptions To Normal Process Flow**|In step-1, if delivery squad member doesn't provide all necessary details then administrator re-requests for mandatory details and the Recruit Delivery Squad Process will begin again.|
|**Control Points and Measurements**|In step-2,<ul><li>Administrator checks if delivery squad member details already exists in delivery squad journal.</li><li>If administrator finds delivery squad member details then administrator updates existing delivery squad member record OR else creates new delivery squad member record in delivery squad journal.</li></ul>|

### 3. Customer Enrollment Process

[filename](diagram/enroll_customer_process.drawio ':include :type=code')

|Name Of Process|Customer Enrollment Process|
|:--|:--|
|**Process Owner**|Administrator|
|**Description**|Administrator enrolls new customer with active subscription for delivery of {{product}}.|
|**Actors**|<ol><li>Customer</li><li>Administrator</li><li>Customer Journal</li><ol>|
|**Process Input**|Customer details - Fullname, Address, Phone Number, Email Address, Profile Pic, {{product}}, Subscription Period and Quantity.|
|**Process Output**|Customer details are recorded in customer journal and ready to get map to delivery squad for delivery of {{product}}.|
|**Process Flow**|<ol><li>The process starts from customer requesting for enrollment and activate subscription to get delivery of {{product}}.</li><li>Administrator asks customer to provide necessary details. Customer details - Fullname, Address, Phone Number,Email Address, Profile Pic, {{product}}, Subscription Period and Quantity. (Input)</li><li>Upon getting customer details, administrator records customer and subscription details in customer journal.</li><li>The process ends with customer getting enrolled and ready to get map to delivery squad for {{product}} delivery. (Output)</li><ol>|
|**Process Boundary**|<ul><li>The starting boundary of process is defined by administrator asking customer to provide necessary details with subscription.</li><li>The ending boundary of process is defined by customer getting enrolled with details recorded in customer journal and ready to get map to delivery squad.</li></ul>|
|**Exceptions To Normal Process Flow**|In step-2, if customer doesn't provide all necessary details then administrator re-requests for mandatory details and the Customer Enrollment Process will begin again.|
|**Control Points and Measurements**|In step-3,<ul><li>Administrator checks if customer details already exists in customer journal.</li><li>If administrator finds customer details then administrator updates existing customer record OR else creates new customer record in customer journal.</li></ul>|

### 4. Delivery Process

[filename](diagram/delivery_process.drawio ':include :type=code')

|Name Of Process|Delivery Process|
|:--|:--|
|**Process Owner**|Delivery Squad|
|**Description**|Delivery Squad delivers {{product}} to customer|
|**Actors**|<ol><li>Customer</li><li>Delivery Squad</li><li>Customer Journal</li><li>Delivery Journal</li><ol>|
|**Process Input**|Customer details - Fullname, Address, Phone Number, {{product}}, Subscription and Quantity.|
|**Process Output**|Delivery details are recorded in delivery journal.|
|**Process Flow**|<ol><li>The process initiates by delivery squad getting customer details from customer journal. Fullname, Address, Phone Number, Email Address, Profile Pic, {{product}}, Subscription and Quantity. (Input)</li><li>Delivery squad checks for customer subscription and availabilitiy of ordered {{product}}.</li><li>Delivery squad delivers {{product}} at door step of customer.</li><li>The process ends with delivery squad entering delivery details in delivery journal. Delivery details - Customer Number, {{product}}, Quantity, Price and Date Time. (Output)</li><ol>|
|**Process Boundary**|<ul><li>The starting boundary of process is defined by delivery squad getting customer details from journal.</li><li>The ending boundary of process is defined by delivery details getting recorded in delivery journal.</li></ul>|
|**Exceptions To Normal Process Flow**|Delivery squad cancels the delivery if customer subscription is elapsed OR {{product}} is unavailable.|
|**Control Points and Measurements**|In step-2, <ul><li>Delivery squad checks if customer subscription already exists.</li><li>If subscription is elapsed then delivery squad ends the process.</li><li>If active subscription exists then delivery squad checks for availablity of {{product}} for which customer has enrolled.</li><li>If opted {{product}} is available then delivery squads delivers it.</li><li>If opted {{product}} is unavailable then delivery squad checks with customer for delivery of some other {{product}}.</li><li>If customer agrees then delivery squad delivers it.</li><li>If customer denies then delivery squad cancels the delivery.</li></ul>|

### 5. Invoice Creation Process

[filename](diagram/invoice_process.drawio ':include :type=code')

|Name Of Process|Invoice Creation Process|
|:--|:--|
|**Process Owner**|Administrator|
|**Description**|Administrator prepares invoice receipt to share with customer for payment|
|**Actors**|<ol><li>Administrator</li><li>Customer</li><li>Delivery Squad</li><li>Customer Journal</li><li>Delivery Journal</li><li>Invoice receipt</li><ol>|
|**Process Input**|<ul><li>Customer details - Fullname, Address, Phone Number, {{product}}, Subscription and Quantity.</li><li>Delivery details - Customer Number, {{product}}, Quantity, Price and Date Time.</li><ul>|
|**Process Output**|Invoice receipt is shared with customer for payment.|
|**Process Flow**|<ol><li>The process initiates by administrator getting customer subscription details from customer journal. Customer details - Fullname, Address, Phone Number, {{product}}, Subscription and Quantity. (Input)</li><li>Administrator gets customer delivery details from delivery journal. Delivery details - Customer Number, {{product}}, Quantity, Price and Date Time.</li><li>Administrator calculates invoice amount based on customer subscription and delivery details.</li><li>Administrator prepares invoice receipt mentioning invoice amount with customer subscription & delivery details.</li><li>The process ends with delivery squad or administrator sharing invoice receipt with customer for payment. (Output)</li><ol>|
|**Process Boundary**|<ul><li>The starting boundary of process is defined by administrator getting customer subscription and delivery details from journal.</li><li>The ending boundary of process is defined by invoice receipt being shared with customer for payment.</li></ul>|
|**Exceptions To Normal Process Flow**|NA|
|**Control Points and Measurements**|NA|

### 6. Payment Process

[filename](diagram/payment_process.drawio ':include :type=code')

|Name Of Process|Payment Process|
|:--|:--|
|**Process Owner**|Customer|
|**Description**|Customer make payment for {{product}} against the invoice received|
|**Actors**|<ol><li>Administrator</li><li>Customer</li><li>Payment Mode</li><ol>|
|**Process Input**|<ul><li>Customer pays for {{product}} against the invoice.</li><ul>|
|**Process Output**|Administrator records entry in payment register and acknowledges the receipt of payment.|
|**Process Flow**|<ol><li>The process initiates by customer paying for {{product}} against the invoice received. (Input)</li><li>The process ends with administrator recording entry in payment register and sending acknowledgement to customer for received payment. (Output)</li><ol>|
|**Process Boundary**|<ul><li>The starting boundary of process is defined by customer paying for {{product}}.</li><li>The ending boundary of process is defined by administrator recording entry in payment register and sending acknowledgement to customer for received payment.</li></ul>|
|**Exceptions To Normal Process Flow**|If online mode of payment doesn't work due to technical errors, customer starts the Payment Process again|
|**Control Points and Measurements**|NA|

## Core Enitities & Relationships ↔️
[filename](diagram/er.drawio ':include :type=code')

## MVP Modules 🌱

Based on discovery phase - MVP (Minimum Viable Product) modules are identified for {{app_name}}.

|Sr No|Module Name|Business Process|
|:--|:--|:--|
|1.|{{product}} Management|Product Creation|
|2.|Delivery Squad Management|Delivery Squad Recruitment|
|3.|Customer Management|Customer Enrollment|
|4.|{{product}} Delivery Management|{{product}} Delivery|
|5.|Invoice Management|Invoice Creation|
|6.|Payment|Invoice Payment|