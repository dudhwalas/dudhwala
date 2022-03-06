# Vision
{{app_name}}'s vision is to automate and digitize business process and workflow of {{app_for}} by providing first class and complete end to end software solution.

## Business And Company 💼

**{{app_for}} as a business** at it's core - home delivers {{product}} to end customer. Customer subscribes to get the products delivered at their door step and pay for the product as well as provided service at agreed interval (weekly, monthly, quaterly...)

**{{app_for}} as a company** focuses on successful implementation of core business operations, thereby ensures gaining value for company. Company has various stake holders who play important roles in different areas of bussiness operation and workflow. 

{{app_name}} identifies these roles & their responsibilities across bussiness processes and develops a standard bussiness workflow through software automation.

## Roles and Responsibilties 👥

|Sr No.|Role|Responsibility|
|:-|:-|:-|
|1.|Owner|<ol><li>Owns the milk and product distribution company.</li><li>Invests in company. Creates financial budget & sales forecast and make sure company meets them.</li><li>Defines workflow and processes for daily business operations.</li><li>Sets strategies and have plans for future vision, mission and growth of company.</li><li>View sales statistics and financial reports.</li><li>May involve in businees marketing and campaigns.</li><li>May involve in customer support to improve overall customer service experience.</li><li>Final descision maker with all the rights.</li></ol>|
|2.|End Customer|<ol><li>Consumer of {{product}}.</li><li>Subscribes to get {{product}} delivered at door step on quota basis.</li><li>Take deliveries of {{product}} from delivery squad.</li><li>Makes payment for consumed {{product}} as well as services.</li><li>Source of income for {{app_for}} and company.</li><ol>|
|3.|Administrator|<ol><li>Handles complete business operations as per defined workflow and process.</li><li>Records and manage catalog of {{product}}.</li><li>Records and manage profiles of end customer.</li><li>Records and map quantified {{product}} to end customer as per request.</li><li>Records and manage profiles of delivery squad</li><li>Pay salary to delivery squad.</li><li>Records and assign {{product}} to delivery squad for deliveries to end customer.</li><li>Records and manage end customer subscriptions.</li><li>Generates invoices based on records and send it to end customer(through delivery squad) for payment.</li><li>Collects invoice payment from end customer and records it.</li><li>Generates sales statistics and financial reports from records.</li><ol>|
|4.|Delivery Squad|<ol><li>Handles and ensures daily delivery of {{product}} at door step of end customer.</li><li>Records the delivery details - Customer number, {{product}} detail, Quantity and Date Time.</li><li>Handovers invoice received from administrator to end customer.</li><li>Can collect invoice payment from end customer and records it.</li><li>Take salary payment from administrator.</li><ol>|

## Business Processes 🛠️

To run the business successfully and effeciently, {{app_for}} carry out number of core business processes and operations.

### 1. End Customer Enrollment Process

```mermaid
flowchart TB
    start([Start]) -->
    customer-request[Customer requests for enrollment and subscription] --> 
    customer-details[/Customer provide details. Fullname, Address, Phone Number,\nEmail Address, Profile Pic, {{product}},\nSubscription Period and Quantity/] --> 
    check-customer{Administrator checks\nif customer details\nalready exists in records}
    check-customer --> |no|create-customer[Administrator creates new customer record\nin jouranal with provided details]
    check-customer --> |yes|update-customer[Administrator updates existing customer record\nin jouranal with provided details]
    create-customer --> 
    customer-enrolled[/Customer enrollment is recorded in journal and\nready to get map to delivery squad for {{product}} delivery/]
    update-customer --> 
    customer-enrolled -->
    finish([Finish])
```

|Name Of Process|End Customer Enrollment Process|
|:--|:--|
|**Process Owner**|Administrator|
|**Description**|Administrator enrolls new customer with subscription for delivery of {{product}}.|
|**Actors**|<ol><li>End customer</li><li>Administrator</li><li>Customer Record Journal</li><ol>|
|**Process Input**|End customer request for enrollment and activate subscription to get delivery of {{product}}. Customer details - Fullname, Address, Phone Number, Email Address, Profile Pic, Subscription Period and Quantity are the inputs for this process.|
|**Process Flow**|End customer request for enrollment and activate subscription to get delivery of {{product}}. Customer details - Fullname, Address, Phone Number, Email Address, Profile Pic, Subscription Period and Quantity are the inputs for this process.|
|**Process Output**|End customer request for enrollment and activate subscription to get delivery of {{product}}. Customer details - Fullname, Address, Phone Number, Email Address, Profile Pic, Subscription Period and Quantity are the inputs for this process.|
|**Process Boundary**|End customer request for enrollment and activate subscription to get delivery of {{product}}. Customer details - Fullname, Address, Phone Number, Email Address, Profile Pic, Subscription Period and Quantity are the inputs for this process.|
|**Exceptions To Normal Process Flow**|End customer request for enrollment and activate subscription to get delivery of {{product}}. Customer details - Fullname, Address, Phone Number, Email Address, Profile Pic, Subscription Period and Quantity are the inputs for this process.|
|**Control Points and Measurements**|End customer request for enrollment and activate subscription to get delivery of {{product}}. Customer details - Fullname, Address, Phone Number, Email Address, Profile Pic, Subscription Period and Quantity are the inputs for this process.|