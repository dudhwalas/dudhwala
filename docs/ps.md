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
||ListBrands|/v1/brand|GET
|||*TODO*/v1/brand?filter={query} Add-Filter-Search-Query|GET|
||GetBrand|/v1/brand/*|GET|
||UpdateBrand|/v1/{brand.name=brand/*}|PATCH|
|**api.{{app_name}}.com/v1/{parent=brand/\*}/product**|AddProduct|/v1/{parent=brand/*}/product|POST|
||ListProducts|/v1/{parent=brand/\*}/product|GET|
||GetProduct|/v1/{name=brand/\*/product/*}|GET|
||UpdateProduct|/v1/{product.name=brand/\*/product/*}|PATCH|

```

service BrandService {
	rpc ListBrand(ListBrandRequestDto) returns (ListBrandResponseDto) {
		option (google.api.http) = {
            get: "/v1/brand"
        };
	};

	rpc GetBrand(GetBrandRequestDto) returns (BrandDto) {
		option (google.api.http) = {
            get: "/v1/brand/{id}"
        };
	};

	rpc UpdateBrand(BrandDto) returns (BrandDto) {
		option (google.api.http) = {
            put: "/v1/brand",
			body: "*"
        };
	};

	rpc PatchBrand(UpdateBrandRequestDto) returns (BrandDto) {
		option (google.api.http) = {
            patch: "/v1/brand"
			body: "brand"
        };
	};
}

message ListBrandRequestDto {
	int32 page_token = 1;
	int32 page_size = 2;
	string sorting = 3;
}

message GetBrandRequestDto {
	string id = 1;
}

message BrandDto {
	string id = 1;
	string name = 2;
	string image = 3;
	int32 status = 4;
	string realm_Id = 5;
}

message ListBrandResponseDto {
	repeated BrandDto brands = 1;
	int32 next_page_token = 2;
	int64 total_size = 3;
}

message UpdateBrandRequestDto {
	BrandDto brand = 1;
	google.protobuf.FieldMask field_to_update = 2;
}

service ProductService {
	rpc ListProduct(ListProductRequestDto) returns (ListProductResponseDto) {
		option (google.api.http) = {
            get: "/v1/Product"
        };
	};

	rpc GetProduct(GetProductRequestDto) returns (ProductDto) {
		option (google.api.http) = {
            get: "/v1/Product/{id}"
        };
	};

	rpc UpdateProduct(ProductDto) returns (ProductDto) {
		option (google.api.http) = {
            put: "/v1/Product",
			body: "*"
        };
	};

	rpc PatchProduct(UpdateProductRequestDto) returns (ProductDto) {
		option (google.api.http) = {
            patch: "/v1/Product"
			body: "Product"
        };
	};
}

message ListProductRequestDto {
	int32 page_token = 1;
	int32 page_size = 2;
	string sorting = 3;
	string brandId = 4;
}

message GetProductRequestDto {
	string id = 1;
}

message ProductDto {
	string id = 1;
	string name = 2;
	string image = 3;
	int32 status = 4;
	string realm_Id = 5;
	string brand_Id = 6;
}

message ListProductResponseDto {
	repeated ProductDto Products = 1;
	int32 next_page_token = 2;
	int64 total_size = 3;
}

message UpdateProductRequestDto {
	ProductDto Product = 1;
	google.protobuf.FieldMask field_to_update = 2;
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
    repeated Customer customers = 1;
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
|Service|Operation|Service Endpoint|HTTP Method|
|:--|:--|:--|:--|
|**api.{{app_name}}.com/v1/subscription**|CreateSubscription|/v1/subscription|POST|
||GetSubscription|/v1/{name=subscription/*}|GET|
||ListSubscriptions|/v1/subscription}|GET|
||UpdateSubscription|/v1/{subscription.name=subscription/*}|PATCH|
||DeleteSubscription|/v1/{name=subscription/*}|DELETE|

```
syntax = "proto3";

service Subscription {
    rpc GetSubscription(GetSubscriptionRequest) returns (Subscription) {
        option (google.api.http) = {
            get: "/v1/{name=subscription/*}"
        };
    };

    rpc ListSubscriptions(ListSubscriptionsRequest) returns (ListSubscriptionsResponse) {
        option (google.api.http) = {
            get: "/v1/subscription"
        };
    };

    rpc CreateSubscription(AddSubscriptionRequest) returns (Subscription) {
        option (google.api.http) = {
            post: "v1/subscription"
            body: "subscription"
        };
    };

    rpc UpdateSubscription(UpdateSubscriptionRequest) returns (Subscription)
    {
        option (google.api.http) = {
            patch: "/v1/{subscription.name=subscription/*}"
            body: "subscription"
        };
    };

    rpc DeleteSubscription(DeleteSubscriptionRequest) returns (google.protobuf.Empty) {
        option (google.api.http) = {
            delete: "/v1/{name=subscription/*}"
        };
    };
}

message ListSubscriptionsRequest {
    int32 page_size = 1;
    string page_token = 2;
    string filter = 3;
}

message ListSubscriptionsResponse {
    repeated Subscription subscriptions = 1;
    string next_page_token = 2;
}

message GetSubscriptionRequest {
    string name = 1;
    … other value types
}

message CreateSubscriptionRequest {
    Subscription subscription = 1;
    … other value types
}

message UpdateSubscriptionRequest {
    Subscription subscription = 1;
    FieldMask update_mask = 2;
    … other value types
}

message DeleteSubscriptionRequest {
    string name = 1;
    … other value types
}

message Subscription {
    string name = 1;
    string realm_id = 2;
    string delivery_time = 3;
    Customer customer = 4;
    DelvierySquad delivery_squad = 5;
    repeated Product product = 6;
    bool status = 7;
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
|Service|Operation|Service Endpoint|HTTP Method|
|:--|:--|:--|:--|
|**api.{{app_name}}.com/v1/delivery**|CreateDelivery|/v1/delivery|POST|
||ListDeliveries|/v1/delivery|GET|
||GetDelivery|/v1/{name=delivery/*}|GET|
||UpdateDelivery|/v1/{delivery.name=delivery/*}|PATCH|
||DeleteDelivery|/v1/{name=delivery/*}|DELETE|

```
syntax = "proto3";

service Delivery {
    rpc GetDelivery(GetDeliveryRequest) returns (Delivery) {
        option (google.api.http) = {
            get: "/v1/{name=delivery/*}"
        };
    };

    rpc ListDeliveries(ListDeliveriesRequest) returns (ListDeliveriesResponse) {
        option (google.api.http) = {
            get: "/v1/delivery"
        };
    };

    rpc CreateDelivery(AddDeliveryRequest) returns (Delivery) {
        option (google.api.http) = {
            post: "v1/delivery"
            body: "delivery"
        };
    };

    rpc UpdateDelivery(UpdateDeliveryRequest) returns (Delivery)
    {
        option (google.api.http) = {
            patch: "/v1/{delivery.name=delivery/*}"
            body: "delivery"
        };
    };

    rpc DeleteDelivery(DeleteDeliveryRequest) returns (google.protobuf.Empty) {
        option (google.api.http) = {
            delete: "/v1/{name=delivery/*}"
        };
    };
}

message ListDeliveriesRequest {
    int32 page_size = 1;
    string page_token = 2;
    string filter = 3;
}

message ListDeliveriesResponse {
    repeated Delivery deliveries = 1;
    string next_page_token = 2;
}

message GetDeliveryRequest {
    string name = 1;
    … other value types
}

message CreateDeliveryRequest {
    Delivery delivery = 1;
    … other value types
}

message UpdateDeliveryRequest {
    Delivery delivery = 1;
    FieldMask update_mask = 2;
    … other value types
}

message DeleteDeliveryRequest {
    string name = 1;
    … other value types
}

message Delivery {
    string name = 1;
    string realm_id = 2;
    string delivery_time = 3;
    Customer customer = 4;
    DelvierySquad delivery_squad = 5;
    repeated Product product = 6;
    bool status = 7;
    string comment = 8;
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
|Service|Operation|Service Endpoint|HTTP Method|
|:--|:--|:--|:--|
|**api.{{app_name}}.com/v1/invoice**|CreateInvoice|/v1/invoice|POST|
||GetInvoice|/v1/{name=invoice/*}|GET|
||ListInvoices|/v1/invoice}|GET|
||ShareInvoice|/v1/{name=invoice/*}:share|POST|

```
syntax = "proto3";

service Invoice {

    rpc GetInvoice(GetPaymentRequest) returns (Invoice) {
        option (google.api.http) = {
            get= "/v1/{name=invoice/*}"
        }
    };

    rpc ListInvoices(ListInvoicesRequest) returns (ListInvoicesResponse) {
        option (google.api.http) = {
            get= "/v1/invoice"
        }
    };

    rpc CreateInvoice(CreateInvoiceRequest) returns (Invoice){
        option (google.api.http) = {
            post= "v1/invoice"
            body= "invoice"
        };
    };

    rpc ShareInvoice(ShareInvoiceRequest) returns (google.protobuf.Empty) {
        option (google.api.http) = {
            post= "v1/{name=invoice/*}:share"
            body= "*"
        };
    };
}

message GetInvoiceRequest {
    string name = 1;
    … other value types
}

message ListInvoicesRequest {
    int32 page_size = 1;
    string page_token = 2;
    string filter = 3;
    … other value types
}

message ListInvoicesResponse {
    repeated Invoice invoices = 1;
    … other value types
}

message CreateInvoiceRequest {
    Invoice invoice = 1;
    … other value types
}

message ShareInvoiceRequest {
    string name = 1;
    … other value types
}

message Invoice {
    string name = 1;
    string realm_id = 2;
    string invoice_time = 3;
    Customer customer = 4;
    repeated Product product = 5;
    string start_date = 6;
    string end_date = 7;
    bool status = 8;
    string amount = 9;
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
|Service|Operation|Service Endpoint|HTTP Method|
|:--|:--|:--|:--|
|**api.{{app_name}}.com/v1/payment**|CreatePayment|/v1/payment|POST|
||GetPayment|/v1/{name=payment/*}|GET|
||ListPayments|/v1/payment|GET|

```
syntax = "proto3";

service Payment {
    rpc GetPayment(GetPaymentRequest) returns (Payment) {
        option (google.api.http) = {
            get= "/v1/{name=payment/*}"
        }
    };

    rpc ListPayments(ListPaymentsRequest) returns (ListPaymentsResponse) {
        option (google.api.http) = {
            get= "/v1/payment"
        }
    };

    rpc CreatePayment(CreatePaymentRequest) returns (Payment) {
        option (google.api.http) = {
            post= "/v1/payment"
            body= "payment"
        }
    };
}

message GetPaymentRequest {
    string name = 1;
    … other value types
}

message ListPaymentsRequest {
    int32 page_size = 1;
    string page_token = 2;
    string filter = 3;
    … other value types
}

message ListPaymentsResponse {
    repeated Payment payments = 1;
    … other value types
}

message CreatePaymentRequest {
    Payment payment = 1;
    … other value types
}

message Payment {
    string name = 1;
    string realm_id = 2;
    string date = 3;
    string amount = 4;
    repeated Invoice invoice = 5;
    string comment = 6;
    bool status = 7;
    Customer customer = 8;
    … other value types
}
```