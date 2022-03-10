# Vision
{{app_name}}'s vision is to automate and digitize business process and workflow of {{app_for}} by providing first class and complete end to end software solution.

## Business And Company 💼

**{{app_for}} as a business** at it's core - home delivers {{product}} to Customer. Customer subscribes to get the products delivered at their door step and pay for the product as well as provided service at agreed interval (weekly, monthly, quaterly...)

**{{app_for}} as a company** focuses on successful implementation of core business operations, thereby ensures gaining value for company. Company has various stake holders who play important roles in different areas of bussiness operation and workflow. 

{{app_name}} identifies these roles & their responsibilities across bussiness processes and develops a standard bussiness workflow through software automation.

## Roles And Responsibilties 👥

|Sr No.|Role|Responsibility|
|:-|:-|:-|
|1.|Owner|<ol><li>Owns the milk and product distribution company.</li><li>Invests in company. Creates financial budget & sales forecast and make sure company meets them.</li><li>Defines workflow and processes for daily business operations.</li><li>Sets strategies and have plans for future vision, mission and growth of company.</li><li>View sales statistics and financial reports.</li><li>May involve in businees marketing and campaigns.</li><li>May involve in customer support to improve overall customer service experience.</li><li>Final descision maker with all the rights.</li></ol>|
|2.|Customer|<ol><li>Consumer of {{product}}.</li><li>Subscribes to get {{product}} delivered at door step on quota basis.</li><li>Take deliveries of {{product}} from delivery squad.</li><li>Makes payment for consumed {{product}} as well as services.</li><li>Source of income for {{app_for}} and company.</li><ol>|
|3.|Administrator|<ol><li>Handles complete business operations as per defined workflow and process.</li><li>Records and manage catalog of {{product}}.</li><li>Records and manage profiles of Customer.</li><li>Records and map quantified {{product}} to Customer as per request.</li><li>Records and manage profiles of delivery squad</li><li>Pay salary to delivery squad.</li><li>Records and assign {{product}} to delivery squad for deliveries to Customer.</li><li>Records and manage Customer subscriptions.</li><li>Generates invoices based on records and send it to Customer(through delivery squad) for payment.</li><li>Collects invoice payment from Customer and records it.</li><li>Generates sales statistics and financial reports from records.</li><ol>|
|4.|Delivery Squad|<ol><li>Handles and ensures daily delivery of {{product}} at door step of Customer.</li><li>Records the delivery details - Customer number, {{product}} detail, Quantity and Date Time.</li><li>Handovers invoice received from administrator to Customer.</li><li>Can collect invoice payment from Customer and records it.</li><li>Take salary payment from administrator.</li><ol>|

## Business Processes 🛠️

To run the business successfully and effeciently, {{app_for}} carry out number of core business processes and operations.

### 1. Product Creation Process

```mermaid
flowchart TB
    start([Start]) --> 
    product-details[/Administrator get's {{product}} details from manufacturer. \n Name, Brand, Available In Quantity And Price./] -->
    check-product{Administrator checks \n if product already exists \n in product catalog.}
    check-product --> |no|create-product[Administrator creates new product \n in catalog with provided details]
    check-product --> |yes|update-product[Administrator updates existing product \n in catalog with provided details]
    create-product --> 
    product-created[/Product details are recorded in catalog and ready to \n get map to end customer for delivery/]
    update-product --> product-created -->
    finish([Finish])
```
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

### 2. Customer Enrollment Process

```mermaid
flowchart TB
    start([Start]) -->
    customer-request[Customer requests for enrollment and subscription] --> 
    customer-details[/Customer provide details. Fullname, Address, Phone Number, \n Email Address, Profile Pic, {{product}}, \n Subscription Period and Quantity/] --> 
    check-customer{Administrator checks \n if customer details \n already exists in records}
    check-customer --> |no|create-customer[Administrator creates new customer record \n in journal with provided details]
    check-customer --> |yes|update-customer[Administrator updates existing customer record \n in journal with provided details]
    create-customer --> 
    customer-enrolled[/Customer details are recorded in journal and ready to \n get map to delivery squad for delivery of {{product}}/]
    update-customer --> 
    customer-enrolled -->
    finish([Finish])
```

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

### 3. Delivery Process

```mermaid
flowchart TB
    start([Start]) -->
    customer-subscription[/Delivery squad gets customer details from journal. \n Fullname, Address, Phone Number, {{product}}, \n Subscription and Quantity/] --> 
    check-customer-subscription{Delivery squad checks \n if customer subscription is active} -->
    |no|finish
    check-customer-subscription --> 
    |yes|check-customer-product-quantity{Delivery squad checks \n if {{product}} \n to be delivered \n is available  \n with required quantity} --> 
    |yes|deliver-product
    check-customer-product-quantity --> |no|check-customer-other-product-quantity{Delivery squad \n connects with customer \n to check if \n some other {{product}} \n can be delivered \n with required quantity} -->
    |no|enter-no-delivery
    check-customer-other-product-quantity --> 
    |yes|deliver-product[Delivery squad delivers \n {{product}} to customer] -->
    enter-delivery-details[Delivery squad enters delivery details in delivery journal. \n Customer number, {{product}}, Quantity, Date Time] --> finish
    enter-no-delivery[Delivery squad cancels delivery \n in delivery journal. \n Customer number, Date Time] --> finish

    finish([Finish])
```

|Name Of Process|Delivery Process|
|:--|:--|
|**Process Owner**|Delivery Squad|
|**Description**|Delivery Squad delivers {{product}} to customer|
|**Actors**|<ol><li>Customer</li><li>Delivery Squad</li><li>Customer Journal</li><li>Delivery Journal</li><ol>|
|**Process Input**|Customer details - Fullname, Address, Phone Number, {{product}}, Subscription and Quantity.|
|**Process Output**|Delivery details are recorded in delivery journal.|
|**Process Flow**|<ol><li>The process initiates by delivery squad getting customer details from customer journal. Fullname, Address, Phone Number, Email Address, Profile Pic, {{product}}, Subscription and Quantity. (Input)</li><li>Delivery squad checks for customer subscription and availabilitiy of ordered {{product}}.</li><li>Delivery squad delivers {{product}} at door step of customer.</li><li>The process ends with delivery squad entering delivery details in delivery journal. Customer details - Customer Number, {{product}}, Quantity and Date Time. (Output)</li><ol>|
|**Process Boundary**|<ul><li>The starting boundary of process is defined by delivery squad getting customer details from journal.</li><li>The ending boundary of process is defined by delivery details getting recorded in delivery journal.</li></ul>|
|**Exceptions To Normal Process Flow**|Delivery squad cancels the delivery if customer subscription is elapsed OR {{product}} is unavailable.|
|**Control Points and Measurements**|In step-2, <ul><li>Delivery squad checks if customer subscription already exists.</li><li>If subscription is elapsed then delivery squad ends the process.</li><li>If active subscription exists then delivery squad checks for availablity of {{product}} for which customer has enrolled.</li><li>If opted {{product}} is available then delivery squads delivers it.</li><li>If opted {{product}} is unavailable then delivery squad checks with customer for delivery of some other {{product}}.</li><li>If customer agrees then delivery squad delivers it.</li><li>If customer denies then delivery squad cancels the delivery.</li></ul>|