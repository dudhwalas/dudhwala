### Platform Services 🧠 <!-- {docsify-ignore} -->

**Platform services** are core business microservices - product service, customer service, delivery squad service, subscription service, delivery service, invoice service, payment service and other backend services. The backend services can expose API using REST over Http Or Grpc services. 

1.  ### Product Catalog Service

[filename](diagram/product_service_domain_model.drawio ':include :type=code')

#### Sequence View
[filename](diagram/product_sequence_view.drawio ':include :type=code')

#### Data Model
[filename](diagram/product_erd.drawio ':include :type=code')

#### API - Service
|Service|Operation|Service Endpoint|HTTP Method|
|:--|:--|:--|:--|
|**api.{{app_name}}.com/v1/brand**|CreateBrand|/v1/brand|POST|
||ListBrands|/v1/brand|GET|
||GetBrand|/v1/brand/*|GET|
||UpdateBrand|/v1/{brand.name=brand/*}|PATCH|
|**api.{{app_name}}.com/v1/{parent=brand/\*}/product**|AddProduct|/v1/{parent=brand/*}/product|POST|
||ListProducts|/v1/{parent=brand/\*}/product|GET|
||GetProduct|/v1/{name=brand/\*/product/*}|GET|
||UpdateProduct|/v1/{product.name=brand/\*/product/*}|PATCH|

```
syntax = "proto3";

service Catalog {
    rpc GetBrand (GetBrandRequest) returns (Brand) {
        option (google.api.http) = {
            get: "/v1/{name=brand/*}"
        };
    };

    rpc ListBrands(ListBrandsRequest) returns (ListBrandsResponse) {
        option (google.api.http) = {
            get: "/v1/brand"
        };
    };

    rpc CreateBrand(CreateBrandRequest) returns (Brand) {
        option (googe.api.http) = {
            post: "/v1/brand"
            body: brand
        };
    };

    rpc UpdateBrand(UpdateBrandRequest) returns (Brand) {
        option (google.api.http) = {
            patch: "v1/{brand.name=brand/*}"
            body: brand
        };
    };

    rpc GetProduct(GetProductRequest) returns (Product) {
        option (google.api.http) = {
            get: "/v1/{name=brand/*/product/*}"
        };
    };

    rpc ListProducts(ListProductsRequest) returns (ListProductsResponse) {
        option (google.api.http) = {
            get: "/v1/{parent=brand/*}/product"
        };
    };

    rpc CreateProduct(CreateProductRequest) returns (Product) {
        option (googe.api.http) = {
            post: "/v1/{parent=brand/*}/product}"
            body: product
        };
    };

    rpc UpdateProduct(UpdateProductRequest) returns (Product) {
        option (google.api.http) = {
            patch: "v1/{product.name=brand/*/product/*}"
            body: product
        }
    };
}


message ListBrandsRequest {
    int32 page_size = 1;
    string page_token = 2;
    string filter = 3;
}

message ListBrandsResponse {
    repeated Brand brand = 1;
    string next_page_token = 2;
}

message GetBrandRequest {
    string name = 1;
    … other value types
}

message CreateBrandRequest {
    Brand brand = 1;
    … other value types
}

message UpdateBrandRequest {
    Brand brand = 1;
    FieldMask update_mask = 2;
    … other value types
}

message ListProductsRequest {
    string parent = 1;
    int32 page_size = 2;
    string page_token = 3;
    string filter = 4;
}

message ListProductsResponse {
    repeated Product product = 1;
    string next_page_token = 2;
}

message GetProductRequest {
    string name = 1;
    … other value types
}

message CreateProductRequest {
    string parent = 1;
    Product product = 2;
    … other value types
}

message UpdateProductRequest {
    Product product = 1;
    FieldMask update_mask = 2;
    … other value types
}

message Brand {
    string name = 1;
    string realm_id = 2;
    string display_name = 3;
    string image = 4;
    bool status = 5;
    … other value types
}

message Product {
    string name = 1;
    string realm_id = 2;
    string display_name = 3;
    string image = 4;
    string price = 5;
    string quantity = 6;
    string brand_id = 7;
    bool status = 8;
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
||ListCustomers|/v1/customer|GET|
||GetCustomer|/v1/{name=customer/*}|GET|
||UpdateCustomer|/v1/{name=customer/*}|PATCH|
|**api.{{app_name}}.com/v1/{parent=customer/\*}/address**|AddCustomerAddress|/v1/{parent=customer/*}/address|POST|
||ListCustomerAddress|/v1/{parent=customer/*}/address|GET|
||GetCustomerAddress|/v1/{name=customer/\*/address/*}|GET|
||UpdateCustomerAddress|/v1/{customer_address.name=customer/\*/address/*}|PATCH|

```
syntax = "proto3";

service Customer {
    rpc ListCustomers(ListCustomersRequest) returns (ListCustomersResponse) {
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
            patch: "/v1/{customer.name=customer/*}"
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

message ListCustomersRequest {
    int32 page_size = 1;
    string page_token = 2;
    string filter = 3;
}

message ListCustomersResponse {
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
    string parent = 1;
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
|Service|Operation|Service Endpoint|HTTP Method|
|:--|:--|:--|:--|
|**api.{{app_name}}.com/v1/delivery_squad**|CreateDeliverySquad|/v1/delivery_squad|POST|
||ListDeliverySquad|/v1/delivery_squad|GET|
||GetDeliverySquad|/v1/{name=delivery_squad/*}|GET|
||UpdateDeliverySquad|/v1/{name=delivery_squad/*}|PATCH|
|**api.{{app_name}}.com/v1/{parent=delivery_squad/\*}/address**|AddDeliverySquadAddress|/v1/{parent=delivery_squad/*}/address|POST|
||ListDeliverySquadAddress|/v1/{parent=delivery_squad/*}/address|GET|
||GetDeliverySquadAddress|/v1/{name=delivery_squad/\*/address/*}|GET|
||UpdateDeliverySquadAddress|/v1/{delivery_squad_address.name=delivery_squad/\*/address/*}|PATCH|

```
syntax = "proto3";

service DeliverySquad {
    rpc ListDeliverySquad(ListDeliverySquadRequest) returns (ListDeliverySquadResponse) {
        option (google.api.http) = {
            get: "/v1/delivery_squad"
        };
    };

    rpc GetDeliverySquad (GetDeliverySquadRequest) returns (DeliverySquad) {
        option (google.api.http) = {
            get: "/v1/{name=delivery_squad/*}"
        };
    };

    rpc CreateDeliverySquad(CreateDeliverySquadRequest) returns (DeliverySquad)
    {
        option (google.api.http) = {
            post: "/v1/delivery_squad"
            body: "delivery_squad"
        };
    };

    rpc UpdateDeliverySquad(UpdateDeliverySquadRequest) returns (DeliverySquad)
    {
        option (google.api.http) = {
            patch: "/v1/{delivery_squad.name=delivery_squad/*}"
            body: "delivery_squad"
        };
    };

    rpc ListDeliverySquadAddress(ListDeliverySquadAddressRequest) returns (ListDeliverySquadAddressResponse) {
        option (google.api.http) = {
            get: "/v1/{parent=delivery_squad/*}/address"
        };
    };

    rpc GetDeliverySquadAddress(GetDeliverySquadAddressRequest) returns (DeliverySquadAddress) {
        option (google.api.http) = {
            get: "/v1/{name=delivery_squad/*/address/*}"
        };
    };

    rpc CreateDeliverySquadAddress(CreateDeliverySquadAddressRequest) returns (DeliverySquadAddress)
    {
        option (google.api.http) = {
            post: "/v1/{parent=delivery_squad/*}/address"
            body: "delivery_squad_address"
        };
    };

    rpc UpdateDeliverySquadAddress(UpdateDeliverySquadAddressRequest) returns (DeliverySquadAddress)
    {
        option (google.api.http) = {
            patch: "/v1/{delivery_squad_address.name=delivery_squad/*/address/*}"
            body: "delivery_squad_address"
        };
    };
}

message ListDeliverySquadRequest {
    int32 page_size = 1;
    string page_token = 2;
    string filter = 3;
}

message ListDeliverySquadResponse {
    repeated DeliverySquad delivery_squad = 1;
    string next_page_token = 2;
}

message GetDeliverySquadRequest {
    string name = 1;
    … other value types
}

message CreateDeliverySquadRequest {
    DeliverySquad delivery_squad = 1;
    … other value types
}

message UpdateDeliverySquadRequest {
    DeliverySquad delivery_squad = 1;
    FieldMask update_mask = 2;
    … other value types
}

message ListDeliverySquadAddressRequest {
    string parent = 1;
    int32 page_size = 2;
    string page_token = 3;
    string filter = 4;
}

message ListDeliverySquadAddressResponse {
    repeated DeliverySquadAddress delivery_squad_address = 1;
    string next_page_token = 2;
}

message GetDeliverySquadAddressRequest {
    string name = 1;
    … other value types
}

message CreateDeliverySquadAddressRequest {
    string parent = 1;
    DeliverySquadAddress delivery_squad_address = 2;
    … other value types
}

message UpdateDeliverySquadAddressRequest {
    DeliverySquadAddress delivery_squad_address = 1;
    FieldMask update_mask = 2;
    … other value types
}

message DeliverySquad {
    string name = 1;
    string realm_id = 2;
    string first_name = 3;
    string last_name = 4;
    bool status = 5;
    … other value types
}

message DeliverySquadAddress {
    string name = 1;
    string delivery_squad_id = 2;
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