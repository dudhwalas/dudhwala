# Technical
{{app_name}}'s technical software solution design comprises of **Software Architecture**, **Solution Design**, **Infrastructure**, **DevOps**, **Info Sec**, **APLM Process** and other technical aspects of the system.

## Software Architecture 🗜️
The software architecture of {{app_name}} system describes elements that makes the sytem, their interaction, relation with other elements and properties of both. It describes the system by number of architectural views which shows different aspect of the system that addresses different concerns.

### 1. Context View
The Context view of {{app_name}} system defines the relationships, dependencies, and interactions between the system and its environment—the people, external systems and entities with which it interacts. It defines what {{app_name}} does and does not do; where the boundaries are between it and the outside world; and how it interacts with other systems, organizations, and people across these boundaries.

[filename](diagram/context_view.drawio ':include :type=code')

### 2. Subdomain Services Definition
[filename](diagram/subdomain_service_view.drawio ':include :type=code')

### 3. High Level Service APIs
|Service|Operations|Collaborators|
|:--|:--|:--|
|**Product**|addProduct|<center>**--**</center>|
||modifyProduct|**Subscription Service**<br>- updateProductDetails|
||deactivateProduct|**Subscription Service**<br>- verifyProductSubscription|
|**Customer**|enrollCustomer|<center>**--**</center>|
||modifyCustomer|**Subscription Service**<br>- updateConsumerDetails|
||deactivateCustomer|**Subscription Service**<br>- verifyConsumerSubscription|
|**Delivery Squad**|recruitDeliverySquadMember|<center>**--**</center>|
||modifyDeliverySquadMember|**Subscription Service**<br>- updateDeliverySquadMemberDetails|
||deactivateDeliverySquadMember|**Subscription Service**<br>- verifyDeliverySquadMemberInSubscription|
|**Subscription**|createSubscription|<center>**--**</center>|
||modifySubscription|<center>**--**</center>|
||removeSubscription|<center>**--**</center>|
|**Delivery**|deliver|<center>**--**</center>|
||modifyDelivery|<center>**--**</center>|
|**Invoice**|prepareInvoice|**Delivery Service**<br>- fetchDeliveryDetails<br>**Product Service**<br>- fetchProductAmountDetails|
||shareInvoice|<center>**--**</center>|
|**Payment**|recordPayment|**Invoice Service**<br>- updateInvoiceStatus|

### 4. Architecturally Significant Requirements (ASR) - Quality Attributes
|Quality Attribute|Measurable Metric|Benchmark|
|:--|:--|:--|
|**Performance**|Latency of API response|Max <= 5 sec</br>Avg <=2 sec|
|**Performance**|Timeout of API response|Max <= 15 sec|
|**Performance**|API request post size|Max <= 2 MB|
|**Concurrent**|Concurrent users|Max = 100 users</br>Performance benchmark report|
|**High Availability**|SLA</br>Failure detection|Min = 99% SLA</br>Alert on failure within 30 sec|
|**Portability**|Docker containers|Platform agnostic. Run on any platform supporting Docker|
|**Portability**|Container orchestration - k8s|Platform agnostic. Run on any platform supporting k8s|
|**Backup**|Database backup|Weekly full backup|
|**Usability**|Browser Support|Google Chrome, Safari|
|**Logging**|API request/response</br>Exception|UI tool to access logs|
|**Auditablity**|Audit of operations that changes application state|UI tool to access audit logs|
|**Testability**|Unit Test</br>Functional Test</br>|Test reports|
|**Deployability**|CI-CD|DevOps pipeline|

### 5. Architecture Core Principles
|Principle|Description|RAG
|:--|:--|:--|
|**Inteface Segregation**|<ul><li>Design of interfaces (i.e., service contracts/API contracts).</li><li>Support multiple client.</li></ul>**Tactics:**<ol><li>Backend For Frontend (BFF)</li><li>API Gateway</li><li>Gateway Aggregation</li><li>Gateway Offloading</li><li>Gateway Routing</li><ol>|G|
|**Deployability**|<ul><li>Configuring the runtime infrastructure, which includes containers, pods, clusters, persistence, security, and networking.</li><li>Expediting the commit+build+test+deploy process.</li><li>Monitoring the health of the microservices to quickly identify and remedy faults.</li></ul>**Tactics:**<ol><li>Containerization & container orchestration</li><li>Service mesh</li><li>API Gatewayt</li><li>Serverless architecture</li><li>Monitoring tools</li><li>Log consolidation tools</li><li>Tracing tools</li><li>DevOps</li><li>Blue Green deployment and canary releases</li><li>IaC</li><li>Continuous Delivery</li><li>Externalized configuration</li><ol>|G|
|**Loose coupling**|<ul><li>Asynchronous messaging.</li><li>Event Driven</li><li>Send to and receive messages from a queue/topic.</li><li>Publisher/Subscriber.</li></ul>**Tactics:**<ol><li>Message broker.</li><li>Database per micro-service</li><li>Saga</li><li>Distributed transaction</li><li>Compensating transaction</li><ol>|G|
|**Availability over consistency**|<ul><li>Minimum downtime - High availability.</li><li>Fault tolerance.</li><li>Resilient.</li></ul>**Tactics:**<ol><li>Service data replication</li><li>CQRS</li><li>Event Sourcing</li><li>Retry</li><li>Circuit breaker</li><li>Network timeouts</li><ol>|G|
|**Single Responsiblity**|<ul><li>Right grained micro-service. Not too fine - not to coarse</li><li>Cohesion.</li></ul>**Tactics:**<ol><li>Domain Driven Design - DDD</li><li>Scope of bounded context - BC</li><li>Domain events</li><ol>|G|

### 6. Conceptual Architecture
[filename](diagram/conceptual_view.drawio ':include :type=code')

#### Element Catalog

|Solution Component|Description
|:--|:--|
|**Users**|Admin and delivery squad member who can access {{app_name}} portal|
|**Channels**|Web browsers used to access {{app_name}} portal|
|**Auth Channels**|User authentication and authorization channel|
|**Identity Provider**|User Indentity and Access Management|
|**{{app_name}} Platform Service**|Core business microservices - product service, customer service, delivery squad service, subscription service, delivery service, invoice service, payment service and other backend services.|
|**Base Framework**|Set of microservices that form cross-cutting services commonly used across the {{app_name}} business services.|
|**Cloud Infra**|Cloud infrastructure where all services, data and web app will be hosted.|
|**Cloud Networking**|Networking services like Virtual Networks, Subnets.|
|**Cloud Compute**|Computing services like container orchestration, VM etc., from the Cloud provider.|
|**Cloud Storage**|File storage and data storage managed services from the cloud provider.|
|**Cloud API Gateway**|Public facing endpoint for the {{app_name}} APIs.|
|**Cloud Service n…**|Other cloud services identified in future.|
|**Data pipelines**|ETL data pipelines used to perform data migration from {{app_name}} Database to DW system.|
|**Staging data store**|{{app_name}} SQL database|
|**Enterprise Data Warehouse**|Data Warehouse for {{app_name}} data migration.|
|**Data Analytics Service**|Analysis Service to create data models for BI reports.|
|**Visualisation**|BI analytics and reports.|

### 7. Logical Architecture
[filename](diagram/logical_view.drawio ':include :type=code')

#### Element Catalog

|Solution Component|Technology|Licensed/Open Source
|:--|:--|:--|
|**Presentation Tier - Web**|HTML 5,<br>CSS,<br>Javascript|Open Source|
|**Frontend Framework,<br>Supporting Framework**|TBD<br>TBD|Open Source|
|**Presentation Tier Hosting - Web**|TBD|TBD|
|**Backend Application Tier**|TBD|TBD|
|**Backend Framework,<br>Supporting Framework**|TBD|TBD|
|**Backend Application Tier Hosting**|TBD|TBD|
|**API Gateway**|TBD|TBD|
|**Data Tier - RDBMS**|TBD|TBD|
|**Container**|Docker|Open Source|
|**Container Orchestration**|K8S|Open Source|
|**Container Registry**|Docker Hub|Open Source|
|**Service Mesh**|Istio|Open Source|
|**Message Queue**|TBD|TBD|
|**File Store**|TBD|TBD|
|**Cache**|Redis Cache|Open Source|
|**Data Center**|TBD|TBD|
|**Application Logging & Monitoring**|TBD|Open Source|
|**Infra Monitoring Tool**|TBD|TBD|
|**Application Deployment**|TBD|TBD|

### 8. Physical Architecture (TBD)

## Base Framework
Set of microservices that form cross-cutting services commonly used across the {{app_name}} business services.

### 1. Logging Service - Unified Logging
[filename](diagram/logging_framework.drawio ':include :type=code')

|Component|Description|Technology|
|:--|:--|:-|
|**Centralized Log Integration**|Collect, Filter, Tranform, Sink log data from variety of {{app_name}}'s source systems, application, database etc.|Fluentd 1.12.0-debian-1.0|
|**Centralized Log Data Store And Search**|Search and analytical engine that centrally stores data to search, index and analyze data.|Elasticsearch 8.1.2|
|**Extensible UI & Visualization**|Interactive user interface to query and visualize log data.|Kibana 8.1.2|

Reference - https://docs.fluentd.org/container-deployment/docker-compose

#### What To Log
The core aspects of {{app_name}} that should be monitored are functional correctness, performance, reliability, and security. Always log:
<ol>
<li>Application errors</li>
<li>Input and output validation failures</li>
<li>Authentication successes and failures</li>
<li>Authorization failures</li>
<li>Exceptions</li>
<li>Other higher-risk events, like data import and export</li>
<li>Opt-ins, like terms of service</li>
<li>Troubleshooting</li>
<li>Monitoring and performance improvement</li>
<li>Understanding user behavior</li>
<li>Security and auditing</li>
</ol>

#### Add context to the message content, such as:
<ol>
<li>What action was performed</li>
<li>Who performed the action</li>
<li>When a failure occurred</li>
<li>Where a failure occurred</li>
<li>Why a failure occurred</li>
<li>Remediation information when possible for WARN and ERROR messages</li>
<li>HTTP request ID - see HTTP Request IDs for more info.</li>
</ol>

#### Exclude sensitive information
Keep security and compliance requirements in mind:
<ol>
<li>Don’t emit sensitive Personal Identifiable Information (PII).</li>
<li>Don’t emit encryption keys or secrets to your logs.</li>
<li>Ensure that your company’s privacy policy covers your log data.</li>
<li>Ensure that your logging add-on provider meets your compliance needs.</li>
<li>Ensure that you meet data residency requirements.</li>
</ol>

#### Log levels:
|Level|Description|
|:-|:-|
|**INFO**|Informational messages that do not indicate any fault or error.|
|**WARN**|Indicates that there is a potential problem, but with no user experience impact.|
|**ERROR**|Indicates a serious problem, with some user experience impact.|
|**FATAL**|Indicates fatal errors, user experience is majorly impacted.|
|**DEBUG**|Used for debugging. The messaging targets specifically the app’s developers.|

#### Structured Log Format
Example
```
{
    "datetime": "2021-01-01T01:01:01-0700",
    "appid": "foobar.netportal_auth",
    "event": "AUTHN_login_success:joebob1",
    "level": "INFO",
    "description": "User joebob1 login successfully",
    "useragent": "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_14_6) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36",
    "source_ip": "165.225.50.94",
    "host_ip": "10.12.7.9",
    "hostname": "portalauth.foobar.com",
    "protocol": "https",
    "port": "440",
    "request_uri": "/api/v2/auth/",
    "request_method": "POST",
    "region": "AWS-US-WEST-2",
    "geo": "USA"
}

```

### 2. IAM Service - Auth
[filename](diagram/iam_auth_framework.drawio ':include :type=code')

|Component|Description|Technology|
|:--|:--|:-|
|**Identity And Access Management**|Identity Provider, Single-Sign On, Identity Brokering and Social Login, User Federation, Admin Console, Account Management Console, OpenID Connect, OAuth 2.0, and SAML, Authorization Services.|Keycloak 20.0.1|

Reference - https://www.keycloak.org/documentation

#### IAM Features
Provides user federation, strong authentication, user management, fine-grained authorization, and more.
<ol>
<li><b>Single-Sign On - </b> Users authenticate with IAM Server. Once logged-in, users don't have to login again to access a different application.</li>
<li><b>Identity Brokering and Social Login - </b> Enabling login with social networks is easy to add through the admin console.</li>
<li><b>User Federation - </b> Own identity provider using relational database.</li>
<li><b>Admin Console - </b> Through the admin console administrators can centrally manage all aspects of the IAM server.</li>
<li><b>Account Management Console - </b> Through the account management console users can manage their own accounts. They can update the profile, change passwords, and setup two-factor authentication.</li>
<li><b>Standard Protocols - </b> Based on standard protocols and provides support for OpenID Connect, OAuth 2.0, and SAML.</li>
<li><b>Authorization Services - </b> Allows you to manage permissions for all your services from the admin console and gives you the power to define exactly the policies you need.</li>
</ol>

#### Access Control Matrix
|<sub>Role</sub>╲<sup>Feature</sup>|Tenant (Realm)|Role|Scope|Policy & Permission|User|Product|Customer|Delivery Squad|Subscription|Delivery|Invoice|Payment|
|:--|:--|:-|:-|:-|:-|:-|:-|:-|:-|:-|:-|:-|
|**Super Admin**|read-write|read-write|read-write|read-write|read-write|read-write|read-write|read-write|read-write|read-write|read-write|read-write|
|**Owner**|-|-|-|-|-|-|read-write|read-write|read-write|read-write|read-write|read-write|
|**Customer**|-|-|-|-|-|-|-|-|-|-|-|-|
|**Delivery Squad**|-|-|-|-|-|-|read|read|read|read-write|read|read-write|
|**Administrator**|-|-|-|-|-|-|read-write|read-write|read-write|read-write|read-write|read-write|

## {{app_name}} Platform Services
Core business microservices - product service, customer service, delivery squad service, subscription service, delivery service, invoice service, payment service and other backend services.

### 1. Product Catalog Service
[filename](diagram/product_service_domain_model.drawio ':include :type=code')

#### Sequence View
[filename](diagram/product_sequence_view.drawio ':include :type=code')

#### Data Model
[filename](diagram/product_erd.drawio ':include :type=code')

#### API - Service
|Service|Operation|Service Endpoint|
|:--|:--|:--|
|**{{app_name}}.v1.brand**|AddBrand|/{{app_name}}.v1.brand/addbrand|
||GetAllBrand|/{{app_name}}.v1.brand/getbrand|
||ModifyBrand|/{{app_name}}.v1.brand/modifybrand|
||DeactivateBrand|/{{app_name}}.v1.brand/deactivatebrand|
|**{{app_name}}.v1.product**|AddProduct|/{{app_name}}.v1.product/addproduct|
||GetAllProduct|/{{app_name}}.v1.product/getproduct|
||ModifyProduct|/{{app_name}}.v1.product/modifyproduct|
||DeactivateProduct|/{{app_name}}.v1.product/deactivateproduct|

```
syntax = "proto3";

service BrandService {
    rpc GetBrand (GetBrandRequest) returns (BrandResponse);

    rpc AddBrand(BrandRequest) returns (StatusResponse);

    rpc ModifyBrand(BrandRequest) returns (StatusResponse);

    rpc DeactivateBrand(DeactivateBrandRequest) returns (StatusResponse);
}

message GetBrandRequest {
    string filterParams = 1;
    … other value types
}

message BrandRequest {
    Brand brand = 1;
    … other value types
}

message DeactivateBrandRequest {
    string brandId = 1;
    … other value types
}

message BrandResponse {
    repeated Brand brand = 1;
    … other value types
}

message Brand {
    string id = 1;
    string realmId = 2;
    string name = 3;
    string image = 4;
    bool status = 5;
    … other value types
}

message StatusResponse {
    bool success = 1;
    string message = 2;
    … other value types
}
```

```
syntax = "proto3";

service ProductService {
    rpc GetProduct (GetProductRequest) returns (ProductResponse);

    rpc AddProduct(ProductRequest) returns (StatusResponse);

    rpc ModifyProduct(ProductRequest) returns (StatusResponse);

    rpc DeactivateProduct(DeactivateProductRequest) returns (StatusResponse);
}

message GetProductRequest {
    string filterParams = 1;
    … other value types
}

message ProductRequest {
    Product product = 1;
    … other value types
}

message DeactivateProductRequest {
    string productId = 1;
    … other value types
}

message ProductResponse {
    repeated Product product = 1;
    … other value types
}

message Product {
    string id = 1;
    string realmId = 2;
    string name = 3;
    string image = 4;
    string price = 5;
    string quantity = 6;
    string brandId = 7;
    bool status = 8;
    … other value types
}

message StatusResponse {
    bool success = 1;
    string message = 2;
    … other value types
}
```


### 2. Customer Service
[filename](diagram/customer_service_domain_model.drawio ':include :type=code')

#### Sequence View
[filename](diagram/customer_sequence_view.drawio ':include :type=code')

#### Data Model
[filename](diagram/customers_erd.drawio ':include :type=code')

#### API - Service
|Service|Operation|Service Endpoint|
|:--|:--|:--|
|**{{app_name}}.v1.customer**|EnrollCustomer|/{{app_name}}.v1.customer/enrollcustomer|
||GetCustomer|/{{app_name}}.v1.customer/getcustomer|
||ModifyCustomer|/{{app_name}}.v1.customer/modifycustomer|
||DeactivateCustomer|/{{app_name}}.v1.customer/deactivatecustomer|
||AddCustomerAddress|/{{app_name}}.v1.customer/addcustomeraddress|
||GetCustomerAddress|/{{app_name}}.v1.customer/getcustomeraddress|
||ModifyCustomerAddress|/{{app_name}}.v1.customer/modifycustomeraddress|
||DeactivateCustomerAddress|/{{app_name}}.v1.customer/deactivatecustomeraddress|

```
syntax = "proto3";

service CustomerService {
    rpc GetCustomer (GetCustomerRequest) returns (CustomerResponse);

    rpc EnrollCustomer(CustomerRequest) returns (StatusResponse);

    rpc ModifyCustomer(CustomerRequest) returns (StatusResponse);

    rpc DeactivateCustomer(DeactivateCustomerRequest) returns (StatusResponse);

    rpc GetCustomerAddress (GetCustomerAddressRequest) returns (CustomerAddressResponse);

    rpc AddCustomerAddress(CustomerAddressRequest) returns (CustomerAddressResponse);

    rpc ModifyCustomerAddress(CustomerAddressRequest) returns (StatusResponse);

    rpc DeactivateCustomerAddress(DeactivateCustomerAddressRequest) returns (StatusResponse);
}

message GetCustomerRequest {
    string filterParams = 1;
    … other value types
}

message CustomerRequest {
    Customer customer = 1;
    … other value types
}

message DeactivateCustomerRequest {
    string customerId = 1;
    … other value types
}

message CustomerResponse {
    repeated Customer customer = 1;
    … other value types
}

message Customer {
    string id = 1;
    string realmId = 2;
    string firstname = 3;
    string lastname = 4;
    bool status = 5;
    … other value types
}

message StatusResponse {
    bool success = 1;
    string message = 2;
    … other value types
}


message GetCustomerAddressRequest {
    string filterParams = 1;
    … other value types
}

message CustomerAddressRequest {
    CustomerAddress customerAddress = 1;
    … other value types
}

message DeactivateCustomerAddressRequest {
    string customerAddressId = 1;
    … other value types
}

message CustomerAddressResponse {
    repeated CustomerAddress customerAddress = 1;
    … other value types
}

message CustomerAddress {
    string id = 1;
    string customerId = 2;
    string line = 3;
    int pincode = 4;
    bool status = 5;
    … other value types
}
```

### 3. Delivery Squad Service
[filename](diagram/deliverysquad_service_domain_model.drawio ':include :type=code')

#### Sequence View
[filename](diagram/delivery_squad_sequence_view.drawio ':include :type=code')

#### Data Model
[filename](diagram/deliverysquad_erd.drawio ':include :type=code')

#### API - Service
|Service|Operation|Service Endpoint|
|:--|:--|:--|
|**{{app_name}}.v1.DeliverySquad**|RecruitDeliverySquad|/{{app_name}}.v1.deliverysquad/recruitdeliverysquad|
||GetDeliverySquad|/{{app_name}}.v1.deliverysquad/getdeliverysquad|
||ModifyDeliverySquad|/{{app_name}}.v1.deliverysquad/modifydeliverysquad|
||DeactivateDeliverySquad|/{{app_name}}.v1.deliverysquad/deactivatedeliverysquad|
||AddDeliverySquadAddress|/{{app_name}}.v1.deliverysquad/adddeliverysquadaddress|
||GetDeliverySquadAddress|/{{app_name}}.v1.deliverysquad/getdeliverysquadaddress|
||ModifyDeliverySquadAddress|/{{app_name}}.v1.deliverysquad/modifydeliverysquadaddress|
||DeactivateDeliverySquadAddress|/{{app_name}}.v1.deliverysquad/deactivatedeliverysquadaddress|

```
syntax = "proto3";

service DeliverySquadService {
    rpc GetDeliverySquad (GetDeliverySquadRequest) returns (DeliverySquadResponse);

    rpc RecruitDeliverySquad(DeliverySquadRequest) returns (StatusResponse);

    rpc ModifyDeliverySquad(DeliverySquadRequest) returns (StatusResponse);

    rpc DeactivateDeliverySquad(DeactivateDeliverySquadRequest) returns (StatusResponse);

    rpc GetDeliverySquadAddress (GetDeliverySquadAddressRequest) returns (DeliverySquadAddressResponse);

    rpc AddDeliverySquadAddress(DeliverySquadAddressRequest) returns (DeliverySquadAddressResponse);

    rpc ModifyDeliverySquadAddress(DeliverySquadAddressRequest) returns (StatusResponse);

    rpc DeactivateDeliverySquadAddress(DeactivateDeliverySquadAddressRequest) returns (StatusResponse);
}

message GetDeliverySquadRequest {
    string filterParams = 1;
    … other value types
}

message DeliverySquadRequest {
    DeliverySquad deliverySquad = 1;
    … other value types
}

message DeactivateDeliverySquadRequest {
    string deliverySquadId = 1;
    … other value types
}

message DeliverySquadResponse {
    repeated DeliverySquad deliverySquad = 1;
    … other value types
}

message DeliverySquad {
    string id = 1;
    string realmId = 2;
    string firstname = 3;
    string lastname = 4;
    bool status = 5;
    … other value types
}

message StatusResponse {
    bool success = 1;
    string message = 2;
    … other value types
}


message GetDeliverySquadAddressRequest {
    string filterParams = 1;
    … other value types
}

message DeliverySquadAddressRequest {
    DeliverySquadAddress deliverySquadAddress = 1;
    … other value types
}

message DeactivateDeliverySquadAddressRequest {
    string deliverySquadAddressId = 1;
    … other value types
}

message DeliverySquadAddressResponse {
    repeated DeliverySquadAddress deliverySquadAddress = 1;
    … other value types
}

message DeliverySquadAddress {
    string id = 1;
    string deliverySquadId = 2;
    string line = 3;
    int pincode = 4;
    bool status = 5;
    … other value types
}
```

### 4. Subscription Service
[filename](diagram/subscription_service_domain_model.drawio ':include :type=code')

#### Sequence View
[filename](diagram/subscription_sequence_view.drawio ':include :type=code')

#### Data Model
[filename](diagram/subscription_erd.drawio ':include :type=code')

#### API - Service
|Service|Operation|Service Endpoint|
|:--|:--|:--|
|**{{app_name}}.v1.Subscription**|AddSubscription|/{{app_name}}.v1.subscription/addsubscription|
||GetSubscription|/{{app_name}}.v1.subscription/getsubscription|
||ModifySubscription|/{{app_name}}.v1.subscription/modifysubscription|
||RemoveSubscription|/{{app_name}}.v1.subscription/removesubscription|

```
syntax = "proto3";

service SubscriptionService {
    rpc GetSubscription(GetSubscriptionRequest) returns (SubscriptionResponse);

    rpc AddSubscription(SubscriptionRequest) returns (StatusResponse);

    rpc ModifySubscription(SubscriptionRequest) returns (StatusResponse);

    rpc RemoveSubscription(RemoveSubscriptionRequest) returns (StatusResponse);
}

message GetSubscriptionRequest {
    string filterParams = 1;
    … other value types
}

message SubscriptionRequest {
    Subscription subscription = 1;
    … other value types
}

message RemoveSubscriptionRequest {
    string subscriptionId = 1;
    … other value types
}

message SubscriptionResponse {
    repeated Subscription subscription = 1;
    … other value types
}

message Subscription {
    string id = 1;
    string realmId = 2;
    string deliveryTime = 3;
    Customer customer = 4;
    DelvierySquad deliverySquad = 5;
    repeated Product product = 6;
    bool status = 7;
    … other value types
}

message StatusResponse {
    bool success = 1;
    string message = 2;
    … other value types
}
```

### 5. Delivery Service
[filename](diagram/delivery_service_domain_model.drawio ':include :type=code')

#### Sequence View
[filename](diagram/delivery_sequence_view.drawio ':include :type=code')

#### Data Model
[filename](diagram/delivery_erd.drawio ':include :type=code')

#### API - Service
|Service|Operation|Service Endpoint|
|:--|:--|:--|
|**{{app_name}}.v1.Delivery**|Deliver|/{{app_name}}.v1.delivery/deliver|
||GetDelivery|/{{app_name}}.v1.delivery/getdelivery|
||ModifyDelivery|/{{app_name}}.v1.delivery/modifydelivery|

```
syntax = "proto3";

service DeliveryService {
    rpc GetDelivery(GetDeliveryRequest) returns (DeliveryResponse);

    rpc Deliver(DeliveryRequest) returns (StatusResponse);

    rpc ModifyDelivery(DeliveryRequest) returns (StatusResponse);
}

message GetDeliveryRequest {
    string filterParams = 1;
    … other value types
}

message DeliveryRequest {
    Delivery delivery = 1;
    … other value types
}

message DeliveryResponse {
    repeated Delivery delivery = 1;
    … other value types
}

message Delivery {
    string id = 1;
    string realmId = 2;
    string deliveryTime = 3;
    Customer customer = 4;
    DelvierySquad deliverySquad = 5;
    repeated Product product = 6;
    bool status = 7;
    string comment = 8;
    … other value types
}

message StatusResponse {
    bool success = 1;
    string message = 2;
    … other value types
}
```

### 6. Invoice Service
[filename](diagram/invoice_service_domain_model.drawio ':include :type=code')

#### Sequence View
[filename](diagram/invoice_sequence_view.drawio ':include :type=code')

#### Data Model
[filename](diagram/invoice_erd.drawio ':include :type=code')

#### API - Service
|Service|Operation|Service Endpoint|
|:--|:--|:--|
|**{{app_name}}.v1.Invoice**|PrepareInvoice|/{{app_name}}.v1.invoice/prepareinvoice|
||ShareInvoice|/{{app_name}}.v1.invoice/shareinvoice|

```
syntax = "proto3";

service InvoiceService {
    rpc GetInvoice(GetInvoiceRequest) returns (InvoiceResponse);

    rpc PrepareInvoice(PrepareInvoiceRequest) returns (StatusResponse);

    rpc ShareInvoice(ShareInvoiceRequest) returns (StatusResponse);
}

message GetInvoiceRequest {
    string filterParams = 1;
    … other value types
}

message PrepareInvoiceRequest {
    string customerId = 1;
    google.protobuf.Timestamp startDate = 2;
    google.protobuf.Timestamp endDate = 3;
    … other value types
}

message ShareInvoiceRequest {
    string invoiceId = 1;
    … other value types
}

message InvoiceResponse {
    repeated Invoice invoice = 1;
    … other value types
}

message Invoice {
    string id = 1;
    string realmId = 2;
    string invoiceTime = 3;
    Customer customer = 4;
    repeated Product product = 5;
    string startDate = 6;
    string endDate = 7;
    bool status = 8;
    string amount = 9;
    … other value types
}

message StatusResponse {
    bool success = 1;
    string message = 2;
    … other value types
}
```

### 7. Payment Service
[filename](diagram/payment_service_domain_model.drawio ':include :type=code')

#### Sequence View
[filename](diagram/payment_sequence_view.drawio ':include :type=code')

#### Data Model
[filename](diagram/payment_erd.drawio ':include :type=code')

#### API - Service
|Service|Operation|Service Endpoint|
|:--|:--|:--|
|**{{app_name}}.v1.Payment**|GetPayment|/{{app_name}}.v1.payment/getpayment|
||RecordPayment|/{{app_name}}.v1.payment/recordpayment|

```
syntax = "proto3";

service PaymentService {
    rpc GetPayment(GetPaymentRequest) returns (PaymentResponse);

    rpc RecordPayment(RecordPaymentRequest) returns (StatusResponse);
}

message GetPaymentRequest {
    string filterParams = 1;
    … other value types
}

message RecordPaymentRequest {
    Payment payment = 1;
    … other value types
}

message PaymentResponse {
    repeated Payment payment = 1;
    … other value types
}

message Payment {
    string id = 1;
    string realmId = 2;
    string date = 3;
    string amount = 4;
    repeated Invoice invoice = 5;
    string comment = 6;
    bool status = 7;
    Customer customer = 8;
    … other value types
}

message StatusResponse {
    bool success = 1;
    string message = 2;
    … other value types
}
```