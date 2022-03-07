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

### 1. Customer Enrollment Process

```mermaid
flowchart TB
    start([Start]) -->
    customer-request[Customer requests for enrollment and subscription] --> 
    customer-details[/Customer provide details. Fullname, Address, Phone Number,\nEmail Address, Profile Pic, {{product}},\nSubscription Period and Quantity/] --> 
    check-customer{Administrator checks\nif customer details\nalready exists in records}
    check-customer --> |no|create-customer[Administrator creates new customer record\nin journal with provided details]
    check-customer --> |yes|update-customer[Administrator updates existing customer record\nin journal with provided details]
    create-customer --> 
    customer-enrolled[/Customer details are recorded in journal and ready to\nget map to delivery squad for delivery of {{product}}/]
    update-customer --> 
    customer-enrolled -->
    finish([Finish])
```

|Name Of Process|Customer Enrollment Process|
|:--|:--|
|**Process Owner**|Administrator|
|**Description**|Administrator enrolls new customer with active subscription for delivery of {{product}}.|
|**Actors**|<ol><li>Customer</li><li>Administrator</li><li>Customer Journal</li><ol>|
|**Process Input**|Customer details - Fullname, Address, Phone Number, Email Address, Profile Pic, Subscription Period and Quantity are the inputs for this process.|
|**Process Output**|Customer details are recorded in customer journal and ready to get map to delivery squad for delivery of {{product}}.|
|**Process Flow**|<ol><li>The process starts from customer requesting for enrollment and activate subscription to get delivery of {{product}}.</li><li>Administrator asks customer to provide necessary details. Fullname, Address, Phone Number,Email Address, Profile Pic, {{product}}, Subscription Period and Quantity. (Input)</li><li>Upon getting customer details, administrator records customer and subscription details in customer journal.</li><li>The process ends with customer getting enrolled and ready to get map to delivery squad for {{product}} delivery. (Output)</li><ol>|
|**Process Boundary**|The starting boundary of process is defined by administrator asking customer to provide necessary details with subscription.<br>The ending boundary of process is defined by customer getting enrolled with details recorded in customer journal and ready to get map to delivery squad.|
|**Exceptions To Normal Process Flow**|In step-2, if customer doesn't provide all necessary details then administrator re-requests for mandatory details and the Customer Enrollment Process will begin again.|
|**Control Points and Measurements**|In step-3, administrator checks if customer details already exists in journal. If administrator finds customer details then administrator updates existing customer record OR else creates new customer record in customer journal.|

### 2. {{product}} - Catalog Management Process

```mermaid
flowchart TB
    start([Start]) -->
    finish([Finish])
```
