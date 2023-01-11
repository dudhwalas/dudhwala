### Platform Services 🧠 <!-- {docsify-ignore} -->

**Platform services** are core business microservices - product service, customer service, delivery squad service, subscription service, delivery service, invoice service, payment service and other backend services. The backend services can expose API using REST over Http Or Grpc services. 

1.  ### Product Catalog Service

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

2.  ### Customer Service

[filename](diagram/customer_service_domain_model.drawio ':include :type=code')

#### Sequence View
[filename](diagram/customer_sequence_view.drawio ':include :type=code')

#### Data Model
[filename](diagram/customers_erd.drawio ':include :type=code')

#### API - Service
|Service|Operation|Service Endpoint|HTTP Method|
|:--|:--|:--|:--|
|**api.{{app_name}}.com/v1/customer**|CreateCustomer|/v1/customer}|POST|
||ListCustomer|/v1/customer|GET|
||GetCustomer|/v1/customer/*|GET|
||UpdateCustomer|/v1/customer/*|PATCH|
||DeactivateCustomer|/v1/customer/*|PATCH|
||AddCustomerAddress|/v1/{parent=customer/*}/address|POST|
||ListCustomerAddress|/v1/{parent=customer/*}/address|GET|
||GetCustomerAddress|/v1/{parent=customer/\*}/address/*|GET|
||ModifyCustomerAddress|/v1/{parent=customer/\*}/address/*|PATCH|
||DeactivateCustomerAddress|/v1/{parent=customer/\*}/address/*|PATCH|

```
syntax = "proto3";

service CustomerService {
    rpc ListCustomer(ListCustomerRequest) returns (ListCustomerResponse) {
        option (google.api.http) = {
            get: "/v1/customer"
        };
    };

    rpc GetCustomer (GetCustomerRequest) returns (Customer) {
        option (google.api.http) = {
            get: "/v1/{name=customer/*}"
        };
    };

    rpc CreateCustomer(CreateCustomerRequest) returns (Customer)
    {
        option (google.api.http) = {
        post: "/v1/customer"
        body: "customer"
        };
    };

    rpc UpdateCustomer(UpdateCustomerRequest) returns (Customer)
    {
        option (google.api.http) = {
        patch: "/v1/customer"
        body: "customer"
        };
    };

    rpc ListCustomerAddress(ListCustomerAddressRequest) returns (ListCustomerAddressResponse) {
        option (google.api.http) = {
            get: "/v1/{parent=customer/*}/address"
        };
    };

    rpc GetCustomerAddress(GetCustomerAddressRequest) returns (CustomerAddress) {
        option (google.api.http) = {
            get: "/v1/{name=customer/*/address/*}"
        };
    };

    rpc CreateCustomerAddress(CreateCustomerAddressRequest) returns (CustomerAddress)
    {
        option (google.api.http) = {
        post: "/v1/{parent=customer/*}/address"
        body: "customer_address"
        };
    };

    rpc UpdateCustomerAddress(UpdateCustomerAddressRequest) returns (CustomerAddress)
    {
        option (google.api.http) = {
        patch: "/v1/{customer_address.name=customer/*/address/*}"
        body: "customer_address"
        };
    };
}

message ListCustomerRequest {
    int32 page_size = 1;
    string page_token = 2;
    string filter = 3;
}

message ListCustomerResponse {
    repeated Customer customer = 1;
    string next_page_token = 2;
}

message GetCustomerRequest {
    string name = 1;
    … other value types
}

message CreateCustomerRequest {
    Customer customer = 1;
    … other value types
}

message UpdateCustomerRequest {
    Customer customer = 1;
    FieldMask update_mask = 2;
    … other value types
}

message ListCustomerAddressRequest {
    string parent = 1;
    int32 page_size = 2;
    string page_token = 3;
    string filter = 4;
}

message ListCustomerAddressResponse {
    repeated CustomerAddress customer_address = 1;
    string next_page_token = 2;
}

message GetCustomerAddressRequest {
    string name = 1;
    … other value types
}

message CreateCustomerAddressRequest {
    string name = 1;
    CustomerAddress customer_address = 2;
    … other value types
}

message UpdateCustomerAddressRequest {
    CustomerAddress customer_address = 1;
    FieldMask update_mask = 2;
    … other value types
}

message Customer {
    string name = 1;
    string realm_id = 2;
    string first_name = 3;
    string last_name = 4;
    bool status = 5;
    … other value types
}

message CustomerAddress {
    string name = 1;
    string customer_id = 2;
    string line = 3;
    int pincode = 4;
    bool status = 5;
    … other value types
}
```

3.  ### Delivery Squad Service
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

4.  ### Subscription Service
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

5.  ### Delivery Service
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

6.  ### Invoice Service
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

7.  ### Payment Service
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