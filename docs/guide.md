### Development Guide 📚 <!-- {docsify-ignore} -->

**Development Guide** provides the industry wide accepted standards, guidelines and best practices that must be considered while implementing, extending and maintaining {{app_name}} application / any enterprise grade application.

### Backend Service 
The backend services can expose API using REST over Http Or Grpc services. Here are few consideration for both the approaches.

|Feature|gRPC|REST HTTP APIs with JSON|
|:--|:--|:--|
|**Contract**|Required (.proto)|Optional (OpenAPI)|
|**Protocol**|HTTP/2|HTTP|
|**Payload**|Protobuf (small, binary)|JSON (large, human readable)|
|**Prescriptiveness**|Strict specification|Loose. Any HTTP is valid.|
|**Streaming**|Client, server, bi-directional|Client, server|
|**Browser support**|No (requires grpc-web)|Yes
|**Security**|Transport (TLS)|Transport (TLS)|
|**Client code-generation**|Yes|OpenAPI + third-party tooling|

### API Design Guidelines
1.  SHOULD be designed for intent.
1.  SHOULD be stateless.
1.  SHOULD be protected by authentication, authorization along with CORS, OWASP guidelines,secured communication protocol, validations of input objects and harmless (non-sensitive) response.
1.  SHOULD support bulk operations.
1.  SHOULD support pagination.
1.  SHOULD avoid creating single monolith API returning a large response messages.
1.  SHOULD break down API into multiple endpoints for easier consumption and extension.
1.  SHOULD not trust input messages/objects.
1.  SHOULD parse and validate input: length, range, format, type, accepted characters, injection, size, content-type.
1.  SHOULD be self descriptive.
1.  SHOULD have enough documentation.
1.  SHOULD support versioning.
1.  SHOULD be backward compatible.
1.  SHOULD trace and log all requests.
1.  SHOULD not return exception details.

### REST API Guidelines

Ref : <u> https://github.com/microsoft/api-guidelines/blob/vNext/Guidelines.md </u>

1.  SHOULD have consistent and well structured URL. An example of a URL containing a canonical identifier is:
`https://api.{{app_name}}.com/v1.0/user/7011042402/payment`

1.  #### Supported Methods

    Operations MUST use the proper HTTP methods whenever possible, and operation idempotency MUST be respected. HTTP methods are frequently referred to as the HTTP verbs. Below is a list of methods that REST services SHOULD support. Not all resources will support all methods, but all resources using the methods below MUST conform to their usage.

    |Method|Description|Is Idempotent|
    |:--|:--|:--|
    |**GET**|Return the current value of an object|True|
    |**PUT**|Replace an object, or create a named object, when applicable|True|
    |**DELETE**|Delete an object|True|
    |**POST**|Create a new object based on the data provided, or submit a command|False|
    |**HEAD**|Return metadata of an object for a GET response. Resources that support the GET method MAY support the HEAD method as well|True|
    |**PATCH**|Apply a partial update to an object|False|
    |**OPTIONS**|Get information about a request; see below for details.|True|

    1.  #### POST

        POST operations SHOULD support the Location response header to specify the location of any created resource that was not explicitly named, via the Location header.

        As an example, a service that allows creation of users, which will be named by the service:

        POST `http://api.{{app_name}}.com/v1.0/user`
        The response would be something like:

        201 Created
        Location: `http://api.{{app_name}}.com/v1.0/user/7011042402`

    1.   #### Creating resources via PATCH (UPSERT semantics)
    
        Services that allow callers to specify key values on create SHOULD support UPSERT semantics, and those that do MUST support creating resources using PATCH. Because PUT is defined as a complete replacement of the content, it is dangerous for clients to use PUT to modify data. Clients that do not understand (and hence ignore) properties on a resource are not likely to provide them on a PUT when trying to update a resource, hence such properties could be inadvertently removed. Services MAY optionally support PUT to update existing resources, but if they do they MUST use replacement semantics (that is, after the PUT, the resource's properties MUST match what was provided in the request, including deleting any server properties that were not provided).

        Under UPSERT semantics, a PATCH call to a nonexistent resource is handled by the server as a "create", and a PATCH call to an existing resource is handled as an "update". To ensure that an update request is not treated as a create or vice versa, the client MAY specify precondition HTTP headers in the request. The service MUST NOT treat a PATCH request as an insert if it contains an If-Match header and MUST NOT treat a PATCH request as an update if it contains an If-None-Match header with a value of "*".

        If a service does not support UPSERT, then a PATCH call against a resource that does not exist MUST result in an HTTP "409 Conflict" error.

1.  #### Standard request headers
    The table of request headers below SHOULD be used by REST API Guidelines services. Using these headers is not mandated, but if used they MUST be used consistently.

    All header values MUST follow the syntax rules set forth in the specification where the header field is defined.

    |Header|	Type|	Description|
    |:--|:--|:--|
    |**Authorization**|	String|	Authorization header for the request|
    |**Date**|	Date|	Timestamp of the request, based on the client's clock, in RFC 5322 date and time format. The server SHOULD NOT make any assumptions about the accuracy of the client's clock. This header MAY be included in the request, but MUST be in this format when supplied. Greenwich Mean Time (GMT) MUST be used as the time zone reference for this header when it is provided. For example: Wed, 24 Aug 2016 18:41:30 GMT. Note that GMT is exactly equal to UTC (Coordinated Universal Time) for this purpose.|
    |**Accept**|	Content type|	The requested content type for the response such as: application/xml, text/xml, application/json, text/javascript (for JSONP) Per the HTTP guidelines, this is just a hint and responses MAY have a different content type, such as a blob fetch where a successful response will just be the blob stream as the payload. For services following OData, the preference order specified in OData SHOULD be followed.|
    |**Accept-Encoding**|	Gzip, deflate|	REST endpoints SHOULD support GZIP and DEFLATE encoding, when applicable. For very large resources, services MAY ignore and return uncompressed data.|
    |**Accept-Language**|	"en", "es", etc.|	Specifies the preferred language for the response. Services are not required to support this, but if a service supports localization it MUST do so through the Accept-Language header.|
    |**Accept-Charset**|	Charset type like "UTF-8"|	Default is UTF-8, but services SHOULD be able to handle ISO-8859-1.|
    |**Content-Type**|	Content type|	Mime type of request body (PUT/POST/PATCH)|
    |**Prefer**|	return=minimal, return=representation|	If the return=minimal preference is specified, services SHOULD return an empty body in response to a successful insert or update. If return=representation is specified, services SHOULD return the created or updated resource in the response. Services SHOULD support this header if they have scenarios where clients would sometimes benefit from responses, but sometimes the response would impose too much of a hit on bandwidth.|
    |**If-Match, If-None-Match, If-Range**|	String|	Services that support updates to resources using optimistic concurrency control MUST support the If-Match header to do so. Services **MAY** also use other headers related to ETags as long as they follow the HTTP specification.|

1.  #### Standard response headers

    Services SHOULD return the following response headers, except where noted in the "required" column.

    |Response Header|	Required|	Description|
    |:--|:--|:--|
    |Date|	All responses|	Timestamp the response was processed, based on the server's clock, in RFC 5322 date and time format. This header MUST be included in the response. Greenwich Mean Time (GMT) MUST be used as the time zone reference for this header. For example: Wed, 24 Aug 2016 18:41:30 GMT. Note that GMT is exactly equal to UTC (Coordinated Universal Time) for this purpose.|
    |Content-Type|	All responses|	The content type|
    |Content-Encoding|	All responses|	GZIP or DEFLATE, as appropriate|
    |Preference-Applied|	When specified in request|	Whether a preference indicated in the Prefer request header was applied|
    |ETag|	When the requested resource has an entity tag|	The ETag response-header field provides the current value of the entity tag for the requested variant. Used with If-Match, If-None-Match and If-Range to implement optimistic concurrency control.|

1.  #### Custom headers

    Custom headers MUST NOT be required for the basic operation of a given API.

    Some of the guidelines in this document prescribe the use of nonstandard HTTP headers. In addition, some services MAY need to add extra functionality, which is exposed via HTTP headers. The following guidelines help maintain consistency across usage of custom headers.

1.  #### Specifying headers as query parameters

    Some headers pose challenges for some scenarios such as AJAX clients, especially when making cross-domain calls where adding headers MAY not be supported. As such, some headers MAY be accepted as Query Parameters in addition to headers, with the same naming as the header:

    Not all headers make sense as query parameters, including most standard HTTP headers.

    The criteria for considering when to accept headers as parameters are:

    1.  Any custom headers MUST be also accepted as parameters.
    1.  Required standard headers MAY be accepted as parameters.
    1.  Required headers with security sensitivity (e.g., Authorization header) MIGHT NOT be appropriate as parameters; the service owner SHOULD evaluate these on a case-by-case basis.
    
    The one exception to this rule is the Accept header. It's common practice to use a scheme with simple names instead of the full functionality described in the HTTP specification for Accept.

1.  #### PII parameters

    Consistent with their organization's privacy policy, clients SHOULD NOT transmit personally identifiable information (PII) parameters in the URL (as part of path or query string) because this information can be inadvertently exposed via client, network, and server logs and other mechanisms.

    Consequently, a service SHOULD accept PII parameters transmitted as headers.

    However, there are many scenarios where the above recommendations cannot be followed due to client or software limitations. To address these limitations, services SHOULD also accept these PII parameters as part of the URL consistent with the rest of these guidelines.

    Services that accept PII parameters -- whether in the URL or as headers -- SHOULD be compliant with privacy policy specified by their organization's engineering leadership. This will typically include recommending that clients prefer headers for transmission and implementations adhere to special precautions to ensure that logs and other service data collection are properly handled.

1.  #### Response formats

    For organizations to have a successful platform, they must serve data in formats developers are accustomed to using, and in consistent ways that allow developers to handle responses with common code.

    Web-based communication, especially when a mobile or other low-bandwidth client is involved, has moved quickly in the direction of JSON for a variety of reasons, including its tendency to be lighter weight and its ease of consumption with JavaScript-based clients.

    JSON property names SHOULD be camelCased.

    Services SHOULD provide JSON as the default encoding.

1.  #### Error condition responses

    For non-success conditions, developers SHOULD be able to write one piece of code that handles errors consistently across different REST API Guidelines services. This allows building of simple and reliable infrastructure to handle exceptions as a separate flow from successful responses.

    The error response MUST be a single JSON object. This object MUST have a name/value pair named "error". The value MUST be a JSON object.

    This object MUST contain name/value pairs with the names "code" and "message", and it MAY contain name/value pairs with the names "target", "details" and "innererror."

    The value for the "code" name/value pair is a language-independent string. Its value is a service-defined error code that SHOULD be human-readable. This code serves as a more specific indicator of the error than the HTTP error code specified in the response. Services SHOULD have a relatively small number (about 20) of possible values for "code", and all clients MUST be capable of handling all of them. Most services will require a much larger number of more specific error codes, which are not interesting to all clients. These error codes SHOULD be exposed in the "innererror" name/value pair as described below. Introducing a new value for "code" that is visible to existing clients is a breaking change and requires a version increase. Services can avoid breaking changes by adding new error codes to "innererror" instead.

    The value for the "message" name/value pair MUST be a human-readable representation of the error. It is intended as an aid to developers and is not suitable for exposure to end users. Services wanting to expose a suitable message for end users MUST do so through an annotation or custom property. Services SHOULD NOT localize "message" for the end user, because doing so might make the value unreadable to the app developer who may be logging the value, as well as make the value less searchable on the Internet.

    The value for the "target" name/value pair is the target of the particular error (e.g., the name of the property in error).

    The value for the "details" name/value pair MUST be an array of JSON objects that MUST contain name/value pairs for "code" and "message", and MAY contain a name/value pair for "target", as described above. The objects in the "details" array usually represent distinct, related errors that occurred during the request. See example below.

    The value for the "innererror" name/value pair MUST be an object. The contents of this object are service-defined. Services wanting to return more specific errors than the root-level code MUST do so by including a name/value pair for "code" and a nested "innererror". Each nested "innererror" object represents a higher level of detail than its parent. When evaluating errors, clients MUST traverse through all of the nested "innererrors" and choose the deepest one that they understand. This scheme allows services to introduce new error codes anywhere in the hierarchy without breaking backwards compatibility, so long as old error codes still appear. The service MAY return different levels of depth and detail to different callers. For example, in development environments, the deepest "innererror" MAY contain internal information that can help debug the service. To guard against potential security concerns around information disclosure, services SHOULD take care not to expose too much detail unintentionally. Error objects MAY also include custom server-defined name/value pairs that MAY be specific to the code. Error types with custom server-defined properties SHOULD be declared in the service's metadata document. See example below.

    Error responses MAY contain annotations in any of their JSON objects.

    We recommend that for any transient errors that may be retried, services SHOULD include a Retry-After HTTP header indicating the minimum number of seconds that clients SHOULD wait before attempting the operation again.

    #### ErrorResponse : Object

    |Property|	Type|	Required|	Description|
    |:--|:--|:--|:--|
    |error|	Error|✔|The error object.|

    #### Error : Object

    |Property|	Type|	Required|	Description|
    |:--|:--|:--|:--|
    |code|	String|	✔	|One of a server-defined set of error codes.|
    |message|	String|	✔	|A human-readable representation of the error.|
    |target|	String|		|The target of the error.|
    |details|	Error[]|		|An array of details about specific errors that led to this reported error.|
    |innererror|	InnerError|		|An object containing more specific information than the current object about the error.|

    #### InnerError : Object

    |Property|	Type|	Required|	Description|
    |:--|:--|:--|:--|
    |code	|String|		|A more specific error code than was provided by the containing error.|
    |innererror|	InnerError|		|An object containing more specific information than the current object about the error.|

    Example of "innererror":
    ```
        {
            "error": {
                "code": "BadArgument",
                "message": "Previous passwords may not be reused",
                "target": "password",
                "innererror": {
                "code": "PasswordError",
                "innererror": {
                    "code": "PasswordDoesNotMeetPolicy",
                    "minLength": "6",
                    "maxLength": "64",
                    "characterTypes": ["lowerCase","upperCase","number","symbol"],
                    "minDistinctCharacterTypes": "2",
                    "innererror": {
                    "code": "PasswordReuseNotAllowed"
                    }
                }
                }
            }
        }
    ```

    In this example, the most basic error code is "BadArgument", but for clients that are interested, there are more specific error codes in "innererror." The "PasswordReuseNotAllowed" code may have been added by the service at a later date, having previously only returned "PasswordDoesNotMeetPolicy." Existing clients do not break when the new error code is added, but new clients MAY take advantage of it. The "PasswordDoesNotMeetPolicy" error also includes additional name/value pairs that allow the client to determine the server's configuration, validate the user's input programmatically, or present the server's constraints to the user within the client's own localized messaging.

    Example of "details":
    ```
        {
            "error": {
                "code": "BadArgument",
                "message": "Multiple errors in ContactInfo data",
                "target": "ContactInfo",
                "details": [
                {
                    "code": "NullValue",
                    "target": "PhoneNumber",
                    "message": "Phone number must not be null"
                },
                {
                    "code": "NullValue",
                    "target": "LastName",
                    "message": "Last name must not be null"
                },
                {
                    "code": "MalformedValue",
                    "target": "Address",
                    "message": "Address is not valid"
                }
                ]
            }
        }
    ```

    In this example there were multiple problems with the request, with each individual error listed in "details."

1.  #### Collections
    1.  #### Item keys

        Services MAY support durable identifiers for each item in the collection, and that identifier SHOULD be represented in JSON as "id". These durable identifiers are often used as item keys.

        Collections that support durable identifiers MAY support delta queries.

    1.  #### Serialization

        Collections are represented in JSON using standard array notation.

    1.  #### Collection URL patterns

        Collections are located directly under the service root when they are top level, or as a segment under another resource when scoped to that resource.

        For example:

        GET https://api.{{app_name}}.com/v1.0/user
        Whenever possible, services MUST support the "/" pattern. For example:

        GET https://{serviceRoot}/{collection}/{id}

        Where:

        {serviceRoot} – the combination of host (site URL) + the root path to the service
        {collection} – the name of the collection, unabbreviated, pluralized
        {id} – the value of the unique id property. When using the "/" pattern this MUST be the raw string/number/guid value with no quoting but properly escaped to fit in a URL segment.

    1.  #### Nested collections and properties

        Collection items MAY contain other collections. For example, a user collection MAY contain user resources that have multiple addresses:

        GET https://api.{{app_name}}.com/v1.0/user/123/addresses
        
        ```
        {
        "value": [
            { "street": "LT Road", "city": "Mumbai" },
            { "street": "Gorai Road", "city": "Mumbai" }
        ]
        }
        ```

    1.  #### Big collections

        As data grows, so do collections. Planning for pagination is important for all services. Therefore, when multiple pages are available, the serialization payload MUST contain the opaque URL for the next page as appropriate. Refer to the paging guidance for more details.

        Clients MUST be resilient to collection data being either paged or nonpaged for any given request.

        ```
        {
        "value":[
            { "id": "Item 1","price": 99.95,"sizes": null},
            { … },
            { … },
            { "id": "Item 99","price": 59.99,"sizes": null}
        ],
        "@nextLink": "{opaqueUrl}"
        }
        ```
    
    1.  #### Changing collections

        POST requests are not idempotent. This means that two POST requests sent to a collection resource with exactly the same payload MAY lead to multiple items being created in that collection. This is often the case for insert operations on items with a server-side generated id.

        For example, the following request:

        POST https://api.{{app_name}}.com/v1.0/user

        Would lead to a response indicating the location of the new collection item:

        201 Created
        Location: https://api.{{app_name}}.com/v1.0/user/123
        
        And once executed again, would likely lead to another resource:

        201 Created
        Location: https://api.{{app_name}}.com/v1.0/user/124

        While a PUT request would require the indication of the collection item with the corresponding key instead:

        PUT https://api.{{app_name}}.com/v1.0/user/123
    
    1.  #### Sorting collections

        The results of a collection query MAY be sorted based on property values. The property is determined by the value of the $orderBy query parameter.

        The value of the $orderBy parameter contains a comma-separated list of expressions used to sort the items. A special case of such an expression is a property path terminating on a primitive property.

        The expression MAY include the suffix "asc" for ascending or "desc" for descending, separated from the property name by one or more spaces. If "asc" or "desc" is not specified, the service MUST order by the specified property in ascending order.

        NULL values MUST sort as "less than" non-NULL values.

        Items MUST be sorted by the result values of the first expression, and then items with the same value for the first expression are sorted by the result value of the second expression, and so on. The sort order is the inherent order for the type of the property.

        For example:

        GET `https://api.{{app_name}}.com/v1.0/user?$orderBy=name`

        Will return all people sorted by name in ascending order.

        For example:

        GET `https://api.{{app_name}}.com/v1.0/user?$orderBy=name desc`

        Will return all people sorted by name in descending order.

        Sub-sorts can be specified by a comma-separated list of property names with OPTIONAL direction qualifier.

        For example:

        GET `https://api.{{app_name}}.com/v1.0/user?$orderBy=name desc,hireDate`

        Will return all people sorted by name in descending order and a secondary sort order of hireDate in ascending order.

        Sorting MUST compose with filtering such that:

        GET `https://api.{{app_name}}.com/v1.0/user?$filter=name eq 'shri'&$orderBy=hireDate`

        Will return all people whose name is Shri sorted in ascending order by hireDate.

    1.  #### Interpreting a sorting expression

        Sorting parameters MUST be consistent across pages, as both client and server-side paging is fully compatible with sorting.

        If a service does not support sorting by a property named in a $orderBy expression, the service MUST respond with an error message as defined in the Responding to Unsupported Requests section.

    1.  #### Filtering

        The $filter querystring parameter allows clients to filter a collection of resources that are addressed by a request URL. The expression specified with $filter is evaluated for each resource in the collection, and only items where the expression evaluates to true are included in the response. Resources for which the expression evaluates to false or to null, or which reference properties that are unavailable due to permissions, are omitted from the response.

        Example: return all Products whose Price is less than $10.00

        GET https://api.{{app_name}}.com/v1.0/products?$filter=price lt 10.00

        The value of the $filter option is a Boolean expression.

    1.  #### Filter operations

        Services that support $filter SHOULD support the following minimal set of operations.

        |Operator|	Description|Example|
        |:--|:--|:--|
        |Comparison Operators|||		
        |eq|Equal|city eq 'Redmon||
        |ne|Not equal|city ne 'Londo||
        |gt|Greater than|price gt ||
        |ge|Greater than or equal|	price ge ||
        |lt|Less than|price lt ||
        |le|Less than or equal|	price le 1||
        |Logical Operators|||
        |and|Logical and|price le 200 and price gt 3.5|
        |or|Logical or|price le 3.5 or price gt 200|
        |not|Logical negation|not price le 3.5|
        |Grouping Operators||		
        |( )|	Precedence grouping	(priority eq 1 or city eq 'Redmond') and price gt 100|

    1.  #### Operator examples

        The following examples illustrate the use and semantics of each of the logical operators.

        Example: all products with a name equal to 'Milk'

        GET https://api.{{app_name}}.com/v1.0/products?$filter=name eq 'Milk'

        Example: all products with a name not equal to 'Milk'

        GET https://api.{{app_name}}.com/v1.0/products?$filter=name ne 'Milk'

        Example: all products with the name 'Milk' that also have a price less than 2.55:

        GET https://api.{{app_name}}.com/v1.0/products?$filter=name eq 'Milk' and price lt 2.55

        Example: all products that either have the name 'Milk' or have a price less than 2.55:

        GET https://api.{{app_name}}.com/v1.0/products?$filter=name eq 'Milk' or price lt 2.55

        Example: all products that have the name 'Milk' or 'Eggs' and have a price less than 2.55:

        GET https://api.{{app_name}}.com/v1.0/products?$filter=(name eq 'Milk' or name eq 'Eggs') and price lt 2.55

    1.  #### Operator precedence

        Services MUST use the following operator precedence for supported operators when evaluating $filter expressions. Operators are listed by category in order of precedence from highest to lowest. Operators in the same category have equal precedence:

        |Group|	Operator|	Description|
        |:--|:--|:--|
        |Grouping|	( )|	Precedence grouping|
        |Unary|	not|	Logical Negation|
        |Relational|	gt|	Greater Than|
        ||ge|	Greater than or Equal|
        ||lt|	Less Than|
        ||le|	Less than or Equal|
        |Equality|	eq|	Equal|
        ||ne|	Not Equal|
        |Conditional AND|	and|	Logical And|
        |Conditional OR|	or|	Logical Or|

    1.  #### Pagination

        RESTful APIs that return collections MAY return partial sets. Consumers of these services MUST expect partial result sets and correctly page through to retrieve an entire set.

        There are two forms of pagination that MAY be supported by RESTful APIs. Server-driven paging mitigates against denial-of-service attacks by forcibly paginating a request over multiple response payloads. Client-driven paging enables clients to request only the number of resources that it can use at a given time.

        Sorting and Filtering parameters MUST be consistent across pages, because both client- and server-side paging is fully compatible with both filtering and sorting.

    1.  #### Server-driven paging

        Paginated responses MUST indicate a partial result by including a continuation token in the response. The absence of a continuation token means that no additional pages are available.

        Clients MUST treat the continuation URL as opaque, which means that query options may not be changed while iterating over a set of partial results.

        Example:

        GET http://api.{{app_name}}.com/v1.0/user HTTP/1.1
        Accept: application/json

        HTTP/1.1 200 OK
        Content-Type: application/json

        {
        ...,
        "value": [...],
        "@nextLink": "{opaqueUrl}"
        }

    1.  #### Client-driven paging

        Clients MAY use $top and $skip query parameters to specify a number of results to return and an offset into the collection.

        The server SHOULD honor the values specified by the client; however, clients MUST be prepared to handle responses that contain a different page size or contain a continuation token.

        When both $top and $skip are given by a client, the server SHOULD first apply $skip and then $top on the collection.

        Note: If the server can't honor $top and/or $skip, the server MUST return an error to the client informing about it instead of just ignoring the query options. This will avoid the risk of the client making assumptions about the data returned.

        Example:

        GET http://api.{{app_name}}.com/v1.0/user?$top=5&$skip=2 HTTP/1.1
        Accept: application/json

        HTTP/1.1 200 OK
        Content-Type: application/json

        {
        ...,
        "value": [...]
        }

    1.  #### Additional considerations

        Stable order prerequisite: Both forms of paging depend on the collection of items having a stable order. The server MUST supplement any specified order criteria with additional sorts (typically by key) to ensure that items are always ordered consistently.

        Missing/repeated results: Even if the server enforces a consistent sort order, results MAY be missing or repeated based on creation or deletion of other resources. Clients MUST be prepared to deal with these discrepancies. The server SHOULD always encode the record ID of the last read record, helping the client in the process of managing repeated/missing results.

        Combining client- and server-driven paging: Note that client-driven paging does not preclude server-driven paging. If the page size requested by the client is larger than the default page size supported by the server, the expected response would be the number of results specified by the client, paginated as specified by the server paging settings.

        Page Size: Clients MAY request server-driven paging with a specific page size by specifying a $maxpagesize preference. The server SHOULD honor this preference if the specified page size is smaller than the server's default page size.

        Paginating embedded collections: It is possible for both client-driven paging and server-driven paging to be applied to embedded collections. If a server paginates an embedded collection, it MUST include additional continuation tokens as appropriate.

        Recordset count: Developers who want to know the full number of records across all pages, MAY include the query parameter $count=true to tell the server to include the count of items in the response.

    1.  #### Compound collection operations

        Filtering, Sorting and Pagination operations MAY all be performed against a given collection. When these operations are performed together, the evaluation order MUST be:

        1.  **Filtering.** This includes all range expressions performed as an AND operation.
        
        1.  **Sorting.** The potentially filtered list is sorted according to the sort criteria.
        
        1.  **Pagination.** The materialized paginated view is presented over the filtered, sorted list. This applies to both server-driven pagination and client-driven pagination.

    1.  #### Empty Results

        When a filter is performed on a collection and the result set is empty you MUST respond with a valid response body and a 200 response code. In this example the filters supplied by the client resulted in a empty result set. The response body is returned as normal and the value attribute is set to a empty collection. A client MAY be expecting metadata attributes like maxItems based on the format of your responses to similar calls which produced results. You SHOULD maintain consistency in your API whenever possible.

        GET https://api.{{app_name}}.com/v1.0/products?$filter=(name eq 'Milk' or name eq 'Eggs') and price lt 2.55
        Accept: application/json

        HTTP/1.1 200 OK
        Content-Type: application/json

        {
        ...,
        "maxItems": 0,
        "value": []
        }

1.  #### Delta queries

    Services MAY choose to support delta queries.

    1.  #### Delta links

    Delta links are opaque, service-generated links that the client uses to retrieve subsequent changes to a result.

    At a conceptual level delta links are based on a defining query that describes the set of results for which changes are being tracked. The delta link encodes the collection of entities for which changes are being tracked, along with a starting point from which to track changes.

    If the query contains a filter, the response MUST include only changes to entities matching the specified criteria. The key principles of the Delta Query are:

    Every item in the set MUST have a persistent identifier. That identifier SHOULD be represented as "id". This identifier is a service defined opaque string that MAY be used by the client to track object across calls.

    The delta MUST contain an entry for each entity that newly matches the specified criteria, and MUST contain a "@removed" entry for each entity that no longer matches the criteria.

    Re-evaluate the query and compare it to original set of results; every entry uniquely in the current set MUST be returned as an Add operation, and every entry uniquely in the original set MUST be returned as a "remove" operation.

    Each entity that previously did not match the criteria but matches it now MUST be returned as an "add"; conversely, each entity that previously matched the query but no longer does MUST be returned as a "@removed" entry.

    Entities that have changed MUST be included in the set using their standard representation.

    Services MAY add additional metadata to the "@removed" node, such as a reason for removal, or a "removed at" timestamp. We recommend teams coordinate with the REST API Guidelines to help maintain consistency.

    The delta link MUST NOT encode any client top or skip value.

    1.  #### Entity representation

    Added and updated entities are represented in the entity set using their standard representation. From the perspective of the set, there is no difference between an added or updated entity.

    Removed entities are represented using only their "id" and an "@removed" node. The presence of an "@removed" node MUST represent the removal of the entry from the set.

    1.  #### Obtaining a delta link

    A delta link is obtained by querying a collection or entity and appending a $delta query string parameter. For example:

    GET https://api.{{app_name}}.com/v1.0/user?$delta

    HTTP/1.1
    Accept: application/json

    HTTP/1.1 200 OK
    Content-Type: application/json

    {
    "value":[
        { "id": "1", "name": "Matt"},
        { "id": "2", "name": "Mark"},
        { "id": "3", "name": "John"}
    ],
    "@deltaLink": "{opaqueUrl}"
    }

    Note: If the collection is paginated the deltaLink will only be present on the final page but MUST reflect any changes to the data returned across all pages.

    1.  #### Contents of a delta link response

    Added/Updated entries MUST appear as regular JSON objects, with regular item properties. Returning the added/modified items in their regular representation allows the client to merge them into their existing "cache" using standard merge concepts based on the "id" field.

    Entries removed from the defined collection MUST be included in the response. Items removed from the set MUST be represented using only their "id" and an "@removed" node.

    1.  #### Using a delta link

    The client requests changes by invoking the GET method on the delta link. The client MUST use the delta URL as is -- in other words the client MUST NOT modify the URL in any way (e.g., parsing it and adding additional query string parameters). In this example:

    GET https://{opaqueUrl} HTTP/1.1
    Accept: application/json

    HTTP/1.1 200 OK
    Content-Type: application/json

    {
    "value":[
        { "id": "1", "name": "Mat"},
        { "id": "2", "name": "Marc"},
        { "id": "3", "@removed": {} },
        { "id": "4", "name": "Luc"}
    ],
    "@deltaLink": "{opaqueUrl}"
    }
    
    The results of a request against the delta link may span multiple pages but MUST be ordered by the service across all pages in such a way as to ensure a deterministic result when applied in order to the response that contained the delta link.

    If no changes have occurred, the response is an empty collection that contains a delta link for subsequent changes if requested. This delta link MAY be identical to the delta link resulting in the empty collection of changes.

    If the delta link is no longer valid, the service MUST respond with 410 Gone. The response SHOULD include a Location header that the client can use to retrieve a new baseline set of results.

1.  #### Versioning

    All APIs compliant with the REST API Guidelines MUST support explicit versioning. It's critical that clients can count on services to be stable over time, and it's critical that services can add features and make changes.

    1.  #### Versioning formats

    Services are versioned using a Major.Minor versioning scheme. Services MAY opt for a "Major" only version scheme in which case the ".0" is implied and all other rules in this section apply. Two options for specifying the version of a REST API request are supported:

    Embedded in the path of the request URL, at the end of the service root: https://api.{{app_name}}.com/v1.0/products
    As a query string parameter of the URL: https://api.{{app_name}}.com/products?api-version=1.0
   
    1.  #### When to version

    Services MUST increment their version number in response to any breaking API change. See the following section for a detailed discussion of what constitutes a breaking change. Services MAY increment their version number for nonbreaking changes as well, if desired.

    Use a new major version number to signal that support for existing clients will be deprecated in the future. When introducing a new major version, services MUST provide a clear upgrade path for existing clients and develop a plan for deprecation that is consistent with their business group's policies. Services SHOULD use a new minor version number for all other changes.

    Online documentation of versioned services MUST indicate the current support status of each previous API version and provide a path to the latest version.

    1.  #### Definition of a breaking change

    Changes to the contract of an API are considered a breaking change. Changes that impact the backwards compatibility of an API are a breaking change.

    Teams MAY define backwards compatibility as their business needs require.

    Clear examples of breaking changes:

        1.  Removing or renaming APIs or API parameters
        1.  Changes in behavior for an existing API
        1.  Changes in Error Codes and Fault Contracts
        1.  Anything that would violate the Principle of Least Astonishment
    
    Services MUST explicitly define their definition of a breaking change, especially with regard to adding new fields to JSON responses and adding new API arguments with default fields. Services that are co-located behind a DNS Endpoint with other services MUST be consistent in defining contract extensibility.

1.  #### Naming guidelines

    1.  #### Approach

    Naming policies should aid developers in discovering functionality without having to constantly refer to documentation. Use of common patterns and standard conventions greatly aids developers in correctly guessing common property names and meanings. Services SHOULD use verbose naming patterns and SHOULD NOT use abbreviations other than acronyms that are the dominant mode of expression in the domain being represented by the API, (e.g. Url).

    1.  #### Casing
    Acronyms SHOULD follow the casing conventions as though they were regular words (e.g. Url).

    All identifiers including namespaces, entityTypes, entitySets, properties, actions, functions and enumeration values SHOULD use lowerCamelCase.

    HTTP headers are the exception and SHOULD use standard HTTP convention of Capitalized-Hyphenated-Terms.

    1.  #### Names to avoid
    Certain names are so overloaded in API domains that they lose all meaning or clash with other common usages in domains that cannot be avoided when using REST APIs, such as OAUTH. Services SHOULD NOT use the following names:

        1.  #### Context
        2.  Scope
        3.  Resource

    1.  #### Forming compound names
    
    Services SHOULD avoid using articles such as 'a', 'the', 'of' unless needed to convey meaning.
    e.g. names such as aUser, theAccount, countOfBooks SHOULD NOT be used, rather user, account, bookCount SHOULD be preferred.
    
    Services SHOULD add a type to a property name when not doing so would cause ambiguity about how the data is represented or would cause the service not to use a common property name.
    
    When adding a type to a property name, services MUST add the type at the end, e.g. createdDateTime.
    
    1.  #### Identity properties
    
    Services MUST use string types for identity properties.
    
    Services MAY use the simple 'id' property to represent a local or legacy primary key value for a resource.
    
    Services SHOULD use the name of the relationship postfixed with 'Id' to represent a foreign key to another resource, e.g. subscriptionId.
    
    The content of this property SHOULD be the canonical ID of the referenced resource.
    
    1. Date and time properties

    For properties requiring both date and time, services MUST use the suffix 'DateTime'.
    
    For properties requiring only date information without specifying time, services MUST use the suffix 'Date', e.g. birthDate.
    
    For properties requiring only time information without specifying date, services MUST use the suffix 'Time', e.g. appointmentStartTime.
    
    1.  #### Name properties
    
    For the overall name of a resource typically shown to users, services MUST use the property name 'displayName'.
    
    Services MAY use other common naming properties, e.g. givenName, surname, signInName.
    
    1.  #### Collections and counts
    
    Services MUST name collections as plural nouns or plural noun phrases using correct English.
    
    Services MAY use simplified English for nouns that have plurals not in common verbal usage.
    e.g. schemas MAY be used instead of schemata.
    
    Services MUST name counts of resources with a noun or noun phrase suffixed with 'Count'.

### gRPC API Guidelines

Ref : <u> https://cloud.google.com/apis/design </u>

1.  #### Resource Oriented Design

    The Design Guide suggests taking the following steps when designing resource-oriented APIs (more details are covered in specific sections below):

    1.  Determine what types of resources an API provides.
    1.  Determine the relationships between resources.
    1.  Decide the resource name schemes based on types and relationships.
    1.  Decide the resource schemas.
    1.  Attach minimum set of methods to resources.

        1.  #### Resources

            A resource-oriented API is generally modeled as a resource hierarchy, where each node is either a simple resource or a collection resource. For convenience, they are often called a resource and a collection, respectively.

            A collection contains a list of resources of the same type. For example, a user has a collection of contacts.

            A resource has some state and zero or more sub-resources. Each sub-resource can be either a simple resource or a collection resource.

            For example, Gmail API has a collection of users, each user has a collection of messages, a collection of threads, a collection of labels, a profile resource, and several setting resources.

            While there is some conceptual alignment between storage systems and REST APIs, a service with a resource-oriented API is not necessarily a database, and has enormous flexibility in how it interprets resources and methods. 
            
            For example, creating a calendar event (resource) may create additional events for attendees, send email invitations to attendees, reserve conference rooms, and update video conference schedules.

        1.  #### Methods

            The key characteristic of a resource-oriented API is that it emphasizes resources (data model) over the methods performed on the resources (functionality). A typical resource-oriented API exposes a large number of resources with a small number of methods. The methods can be either the standard methods or custom methods. The standard methods are: List, Get, Create, Update, and Delete.

    Where API functionality naturally maps to one of the standard methods, that method should be used in the API design. For functionality that does not naturally map to one of the standard methods, custom methods may be used. Custom methods offer the same design freedom as traditional RPC APIs, which can be used to implement common programming patterns, such as database transactions or data analysis.

1.  #### Resource Name

    In resource-oriented APIs, resources are named entities, and resource names are their identifiers. Each resource must have its own unique resource name. The resource name is made up of the ID of the resource itself, the IDs of any parent resources, and its API service name. We'll look at resource IDs and how a resource name is constructed below.

    gRPC APIs should use scheme-less URIs for resource names. They generally follow the REST URL conventions and behave much like network file paths. They can be easily mapped to REST URLs: see the Standard Methods section for details.

    A collection is a special kind of resource that contains a list of sub-resources of identical type. For example, a directory is a collection of file resources. The resource ID for a collection is called collection ID.

    The resource name is organized hierarchically using collection IDs and resource IDs, separated by forward slashes. If a resource contains a sub-resource, the sub-resource's name is formed by specifying the parent resource name followed by the sub-resource's ID - again, separated by forward slashes.

    Example : An email service has a collection of users. Each user has a settings sub-resource, and the settings sub-resource has a number of other sub-resources, including customFrom:
    
    |API Service Name|Collection ID|Resource ID|Resource ID|Resource ID|
    |:--|:--|:--|:--|:--|
    |mail.googleapis.com|users|name@example.com|settings|customFrom|

    An API producer can choose any acceptable value for resource and collection IDs as long as they are unique within the resource hierarchy. You can find more guidelines for choosing appropriate resource and collection IDs below.

    By splitting the resource name, such as name.split("/")[n], one can obtain the individual collection IDs and resource IDs, assuming none of the segments contains any forward slash.

    1. #### Full Resource Name
    
    A scheme-less URI consisting of a DNS-compatible API service name and a resource path. The resource path is also known as relative resource name. For example:

    "//library.googleapis.com/shelves/shelf1/books/book2"

    The API service name is for clients to locate the API service endpoint; it may be a fake DNS name for internal-only services. If the API service name is obvious from the context, relative resource names are often used.

    1. #### Relative Resource Name
    A URI path (path-noscheme) without the leading "/". It identifies a resource within the API service. For example:

    "shelves/shelf1/books/book2"
    
    1. #### Resource ID

        A resource ID typically consists of one or more non-empty URI segments (segment-nz-nc) that identify the resource within its parent resource, see above examples. The non-trailing resource ID in a resource name must have exactly one URL segment, while the trailing resource ID in a resource name may have more than one URI segment. For example:
        
        |Collection ID|Resource ID|
        |:--|:--|
        |files|source/py/parser.py|

        API services should use URL-friendly resource IDs when feasible. Resource IDs must be clearly documented whether they are assigned by the client, the server, or either. For example, file names are typically assigned by clients, while email message IDs are typically assigned by servers.

    1. #### Collection ID

        A non-empty URI segment (segment-nz-nc) identifying the collection resource within its parent resource, see above examples.

        Because collection IDs often appear in the generated client libraries, they must conform to the following requirements:

        1.  Must be valid C/C++ identifiers.
        1.  Must be in plural form with lowerCamel case. If the term doesn't have suitable plural form, such as "evidence" and "weather", the singular form should be used.
        1.  Must use clear and concise English terms.
        1.  Overly general terms should be avoided or qualified. For example, rowValues is preferred to values. The following terms should be avoided without qualification:
            1.  elements
            1.  entries
            1.  instances
            1.  items
            1.  objects
            1.  resources
            1.  types
            1.  values
    
    1.  #### Resource Name vs URL

        While full resource names resemble normal URLs, they are not the same thing. A single resource can be exposed by different API versions, API protocols, or API network endpoints. The full resource name does not specify such information, so it must be mapped to a specific API version and API protocol for actual use.

        To use a full resource name via REST APIs, it must be converted to a REST URL by adding the HTTPS scheme before the service name, adding the API major version before the resource path, and URL-escaping the resource path. For example:

        // This is a calendar event resource name.

        "//calendar.googleapis.com/users/john smith/events/123"

        // This is the corresponding HTTP URL.

        "https://calendar.googleapis.com/v3/users/john%20smith/events/123"

    1.  #### Resource Name as String
    
        APIs must represent resource names using plain strings, unless backward compatibility is an issue. Resource names should be handled like normal file paths. When a resource name is passed between different components, it must be treated as an atomic value and must not have any data loss.

        For resource definitions, the first field should be a string field for the resource name, and it should be called name.

        Note: The following code examples use gRPC Transcoding syntax. Please follow the link to see the details.
        
        For example:

        ```
        service LibraryService {
        rpc GetBook(GetBookRequest) returns (Book) {
            option (google.api.http) = {
            get: "/v1/{name=shelves/*/books/*}"
            };
        };
        rpc CreateBook(CreateBookRequest) returns (Book) {
            option (google.api.http) = {
            post: "/v1/{parent=shelves/*}/books"
            body: "book"
            };
        };
        }

        message Book {
        // Resource name of the book. It must have the format of "shelves/*/books/*".
        // For example: "shelves/shelf1/books/book2".
        string name = 1;

        // ... other properties
        }

        message GetBookRequest {
        // Resource name of a book. For example: "shelves/shelf1/books/book2".
        string name = 1;
        }

        message CreateBookRequest {
        // Resource name of the parent resource where to create the book.
        // For example: "shelves/shelf1".
        string parent = 1;
        // The Book resource to be created. Client must not set the `Book.name` field.
        Book book = 2;
        }
        ```

        Note: For consistency of resource names, the leading forward slash must not be captured by any URL template variable. For example, URL template "/v1/{name=shelves/*/books/*}" must be used instead of "/v1{name=/shelves/*/books/*}".

1.  #### Standard methods

    This chapter defines the concept of standard methods, which are List, Get, Create, Update, and Delete. Standard methods reduce complexity and increase consistency.

    The following table describes how to map standard methods to HTTP methods:

    |Standard Method|HTTP Mapping|HTTP Request Body|HTTP Response Body|
    |:--|:--|:--|:--|
    |List|	GET <collection URL>|	N/A|	Resource* list|
    |Get|	GET <resource URL>|	N/A|Resource*|
    |Create|	POST <collection URL>|	Resource|	Resource*|
    |Update|	PUT or PATCH <resource URL>|	Resource|	Resource*|
    |Delete|	DELETE <resource URL>|	N/A|	google.protobuf.Empty**|

    *The resource returned from List, Get, Create, and Update methods may contain partial data if the methods support response field masks, which specify a subset of fields to be returned. In some cases, the API platform natively supports field masks for all methods.

    **The response returned from a Delete method that doesn't immediately remove the resource (such as updating a flag or creating a long-running delete operation) should contain either the long-running operation or the modified resource.

    A standard method may also return a long running operation for requests that do not complete within the time-span of the single API call.

    The following sections describe each of the standard methods in detail. The examples show the methods defined in .proto files with special annotations for the HTTP mappings. You can find many examples that use standard methods in the APIs repository.

    Note: The google.api.http annotation shown in the examples below uses the gRPC transcoding syntax to define URIs.

    1.  #### List

        The List method takes a collection name and zero or more parameters as input, and returns a list of resources that match the input.

        List is commonly used to search for resources. List is suited to data from a single collection that is bounded in size and not cached. For broader cases, the custom method Search should be used.

        A batch get (such as a method that takes multiple resource IDs and returns an object for each of those IDs) should be implemented as a custom BatchGet method, rather than a List. However, if you have an already-existing List method that provides the same functionality, you may reuse the List method for this purpose instead. If you are using a custom BatchGet method, it should be mapped to HTTP GET.

        Applicable common patterns: pagination, result ordering.

        Applicable naming conventions: filter field, results field

        #### HTTP mapping:
        
        1.  The List method must use an HTTP GET verb.

        1.  The request message field(s) receiving the name of the collection whose resources are being listed should map to the URL path. If the collection name maps to the URL path, the last segment of the URL template (the collection ID) must be literal.

        1.  All remaining request message fields shall map to the URL query parameters.

        1.  There is no request body; the API configuration must not declare a body clause.

        1.  The response body should contain a list of resources along with optional metadata.

        #### Example:

        ```
        // Lists books in a shelf.
        rpc ListBooks(ListBooksRequest) returns (ListBooksResponse) {
        // List method maps to HTTP GET.
        option (google.api.http) = {
            // The `parent` captures the parent resource name, such as "shelves/shelf1".
            get: "/v1/{parent=shelves/*}/books"
        };
        }

        message ListBooksRequest {
        // The parent resource name, for example, "shelves/shelf1".
        string parent = 1;

        // The maximum number of items to return.
        int32 page_size = 2;

        // The next_page_token value returned from a previous List request, if any.
        string page_token = 3;
        }

        message ListBooksResponse {
        // The field name should match the noun "books" in the method name.  There
        // will be a maximum number of items returned based on the page_size field
        // in the request.
        repeated Book books = 1;

        // Token to retrieve the next page of results, or empty if there are no
        // more results in the list.
        string next_page_token = 2;
        }
        ```

    1.  #### Get

        The Get method takes a resource name, zero or more parameters, and returns the specified resource.

        #### HTTP mapping:

        1.  The Get method must use an HTTP GET verb.

        1.  The request message field(s) receiving the resource name should map to the URL path.

        1.  All remaining request message fields shall map to the URL query parameters.

        1.  There is no request body; the API configuration must not declare a body clause.

        1.  The returned resource shall map to the entire response body.

        #### Example:
        ```
        // Gets a book.
        rpc GetBook(GetBookRequest) returns (Book) {
        // Get maps to HTTP GET. Resource name is mapped to the URL. No body.
        option (google.api.http) = {
            // Note the URL template variable which captures the multi-segment resource
            // name of the requested book, such as "shelves/shelf1/books/book2"
            get: "/v1/{name=shelves/*/books/*}"
        };
        }

        message GetBookRequest {
        // The field will contain name of the resource requested, for example:
        // "shelves/shelf1/books/book2"
        string name = 1;
        }
        ```
    1.  #### Create

        The Create method takes a parent resource name, a resource, and zero or more parameters. It creates a new resource under the specified parent, and returns the newly created resource.

        If an API supports creating resources, it should have a Create method for each type of resource that can be created.

        #### HTTP mapping:

        1.  The Create method must use an HTTP POST verb.

        1.  The request message should have a field parent that specifies the parent resource name where the resource is to be created.

        1.  The request message field containing the resource must map to the HTTP request body. If the google.api.http annotation is used for the Create method, the body: "<resource_field>" form must be used.

        1.  The request may contain a field named <resource>_id to allow callers to select a client assigned id. This field may be inside the resource.

        1.  All remaining request message fields shall map to the URL query parameters.

        1.  The returned resource shall map to the entire HTTP response body.

        1.  If the Create method supports client-assigned resource name and the resource already exists, the request should either fail with error code ALREADY_EXISTS or use a different server-assigned resource name and the documentation should be clear that the created resource name may be different from that passed in.

        1.  The Create method must take an input resource, so that when the resource schema changes, there is no need to update both request schema and resource schema. For resource fields that cannot be set by the clients, they must be documented as "Output only" fields.

        #### Example:
        ```
        // Creates a book in a shelf.
        rpc CreateBook(CreateBookRequest) returns (Book) {
        // Create maps to HTTP POST. URL path as the collection name.
        // HTTP request body contains the resource.
        option (google.api.http) = {
            // The `parent` captures the parent resource name, such as "shelves/1".
            post: "/v1/{parent=shelves/*}/books"
            body: "book"
        };
        }

        message CreateBookRequest {
        // The parent resource name where the book is to be created.
        string parent = 1;

        // The book id to use for this book.
        string book_id = 3;

        // The book resource to create.
        // The field name should match the Noun in the method name.
        Book book = 2;
        }

        rpc CreateShelf(CreateShelfRequest) returns (Shelf) {
        option (google.api.http) = {
            post: "/v1/shelves"
            body: "shelf"
        };
        }

        message CreateShelfRequest {
        Shelf shelf = 1;
        }
        ```
    
    1.  #### Update
        The Update method takes a request message containing a resource and zero or more parameters. It updates the specified resource and its properties, and returns the updated resource.

        Mutable resource properties should be mutable by the Update method, except the properties that contain the resource's name or parent. Any functionality to rename or move a resource must not happen in the Update method and instead shall be handled by a custom method.

        #### HTTP mapping:

        1. The standard Update method should support partial resource update, and use HTTP verb PATCH with a FieldMask field named update_mask. Output fields that are provided by the client as inputs should be ignored.

        1. An Update method that requires more advanced patching semantics, such as appending to a repeated field, should be made available by a custom method.

        1. If the Update method only supports full resource update, it must use HTTP verb PUT. However, full update is highly discouraged because it has backwards compatibility issues when adding new resource fields.

        1. The message field receiving the resource name must map to the URL path. The field may be in the resource message itself.

        1. The request message field containing the resource must map to the request body.

        1. All remaining request message fields must map to the URL query parameters.
        
        1. The response message must be the updated resource itself.
        
        1. If the API accepts client-assigned resource names, the server may allow the client to specify a non-existent resource name and create a new resource. Otherwise, the Update method should fail with non-existent resource name. The error code NOT_FOUND should be used if it is the only error condition.

        An API with an Update method that supports resource creation should also provide a Create method. Rationale is that it is not clear how to create resources if the Update method is the only way to do it.

        #### Example:

        ```
        // Updates a book.
        rpc UpdateBook(UpdateBookRequest) returns (Book) {
        // Update maps to HTTP PATCH. Resource name is mapped to a URL path.
        // Resource is contained in the HTTP request body.
        option (google.api.http) = {
            // Note the URL template variable which captures the resource name of the
            // book to update.
            patch: "/v1/{book.name=shelves/*/books/*}"
            body: "book"
        };
        }

        message UpdateBookRequest {
        // The book resource which replaces the resource on the server.
        Book book = 1;

        // The update mask applies to the resource. For the `FieldMask` definition,
        // see https://developers.google.com/protocol-buffers/docs/reference/google.protobuf#fieldmask
        FieldMask update_mask = 2;
        }
        ```

    1.  #### Delete

        The Delete method takes a resource name and zero or more parameters, and deletes or schedules for deletion the specified resource. The Delete method should return google.protobuf.Empty.

        An API should not rely on any information returned by a Delete method, as it cannot be invoked repeatedly.

        #### HTTP mapping:

        1.  The Delete method must use an HTTP DELETE verb.

        1.  The request message field(s) receiving the resource name should map to the URL path.

        1.  All remaining request message fields shall map to the URL query parameters.

        1.  There is no request body; the API configuration must not declare a body clause.

        1.  If the Delete method immediately removes the resource, it should return an empty response.

        1.  If the Delete method initiates a long-running operation, it should return the long-running operation.

        1.  If the Delete method only marks the resource as being deleted, it should return the updated resource.

        1.  Calls to the Delete method should be idempotent in effect, but do not need to yield the same response. Any number of Delete requests should result in a resource being (eventually) deleted, but only the first request should result in a success code. Subsequent requests should result in a google.rpc.Code.NOT_FOUND.

        #### Example:
        ```
        // Deletes a book.
        rpc DeleteBook(DeleteBookRequest) returns (google.protobuf.Empty) {
        // Delete maps to HTTP DELETE. Resource name maps to the URL path.
        // There is no request body.
        option (google.api.http) = {
            // Note the URL template variable capturing the multi-segment name of the
            // book resource to be deleted, such as "shelves/shelf1/books/book2"
            delete: "/v1/{name=shelves/*/books/*}"
        };
        }

        message DeleteBookRequest {
        // The resource name of the book to be deleted, for example:
        // "shelves/shelf1/books/book2"
        string name = 1;
        }
        ```

1.  #### Custom Methods

    Custom methods refer to API methods besides the 5 standard methods. They should only be used for functionality that cannot be easily expressed via standard methods. In general, API designers should choose standard methods over custom methods whenever feasible.
    
    Standard Methods have simpler and well-defined semantics that most developers are familiar with, so they are easier to use and less error prone. Another advantage of standard methods is the API platform has better understanding and support for standard methods, such as billing, error handling, logging, monitoring.

    A custom method can be associated with a resource, a collection, or a service. It may take an arbitrary request and return an arbitrary response, and also supports streaming request and response.

    Custom method names must follow method naming conventions.

    1.  #### HTTP mapping
        For custom methods, they should use the following generic HTTP mapping:

        https://service.name/v1/some/resource/name:customVerb

        The reason to use : instead of / to separate the custom verb from the resource name is to support arbitrary paths. For example, undelete a file can map to POST /files/a/long/file/name:undelete

        The following guidelines shall be applied when choosing the HTTP mapping:

        1. Custom methods should use HTTP POST verb since it has the most flexible semantics, except for methods serving as an alternative get or list which may use GET when possible. (See third bullet for specifics.)
        1. Custom methods should not use HTTP PATCH, but may use other HTTP verbs. In such cases, the methods must follow the standard HTTP semantics for that verb.
        1. Notably, custom methods using HTTP GET must be idempotent and have no side effects. For example custom methods that implement special views on the resource should use HTTP GET.
        1. The request message field(s) receiving the resource name of the resource or collection with which the custom method is associated should map to the URL path.
        1. The URL path must end with a suffix consisting of a colon followed by the custom verb.
        1. If the HTTP verb used for the custom method allows an HTTP request body (this applies to POST, PUT, PATCH, or a custom HTTP verb), the HTTP configuration of that custom method must use the body: "*" clause and all remaining request message fields shall map to the HTTP request body.
        1. If the HTTP verb used for the custom method does not accept an HTTP request body (GET, DELETE), the HTTP configuration of such method must not use the body clause at all, and all remaining request message fields shall map to the URL query parameters.
        1. WARNING: If a service implements multiple APIs, the API producer must carefully create the service configuration to avoid custom verb conflicts between APIs.

    ```
    // This is a service level custom method.
    rpc Watch(WatchRequest) returns (WatchResponse) {
    // Custom method maps to HTTP POST. All request parameters go into body.
    option (google.api.http) = {
        post: "/v1:watch"
        body: "*"
    };
    }

    // This is a collection level custom method.
    rpc ClearEvents(ClearEventsRequest) returns (ClearEventsResponse) {
    option (google.api.http) = {
        post: "/v3/events:clear"
        body: "*"
    };
    }

    // This is a resource level custom method.
    rpc CancelEvent(CancelEventRequest) returns (CancelEventResponse) {
    option (google.api.http) = {
        post: "/v3/{name=events/*}:cancel"
        body: "*"
    };
    }

    // This is a batch get custom method.
    rpc BatchGetEvents(BatchGetEventsRequest) returns (BatchGetEventsResponse) {
    // The batch get method maps to HTTP GET verb.
    option (google.api.http) = {
        get: "/v3/events:batchGet"
    };
    }
    ```

    1.  #### Use Cases

        Some additional scenarios where custom methods may be the right choice:

        1. Reboot a virtual machine. The design alternatives could be "create a reboot resource in collection of reboots" which feels disproportionately complex, or "virtual machine has a mutable state which the client can update from RUNNING to RESTARTING" which would open questions as to which other state transitions are possible. Moreover, reboot is a well-known concept that can translate well to a custom method which intuitively meets developer expectations.
        1. Send mail. Creating an email message should not necessarily send it (draft). Compared to the design alternative (move a message to an "Outbox" collection) custom method has the advantage of being more discoverable by the API user and models the concept more directly.
        1. Promote an employee. If implemented as a standard update, the client would have to replicate the corporate policies governing the promotion process to ensure the promotion happens to the correct level, within the same career ladder etc.
        1. Batch methods. For performance critical methods, it may be useful to provide custom batch methods to reduce per-request overhead. For example, accounts.locations.batchGet.

        A few examples where a standard method is a better fit than a custom method:

        1. Query resources with different query parameters (use standard list method with standard list filtering).
        Simple resource property change (use standard update method with field mask).
        1. Dismiss a notification (use standard delete method).
    
    1.  #### Common Custom Methods
    
    The curated list of commonly used or useful custom method names is below. API designers should consider these names before introducing their own to facilitate consistency across APIs.

    |Method Name|Custom verb|HTTP verb|Note|
    |:--|:--|:--|:--|
    |Cancel|	:cancel|	POST|	Cancel an outstanding operation, such as operations.cancel.|
    |BatchGet|	:batchGet|	GET|	Batch get of multiple resources. See details in the description of List.|
    |Move|	:move|	POST|	Move a resource from one parent to another, such as folders.move.|
    |Search|	:search|	GET|	Alternative to List for fetching data that does not adhere to List semantics, such as services.search.|
    |Undelete|	:undelete|	POST|	Restore a resource that was previously deleted, such as services.undelete. The recommended retention period is 30-day.|

1.  #### Standard Fields
    This section describes a set of standard message field definitions that should be used when similar concepts are needed. This will ensure the same concept has the same name and semantics across different APIs.

    |Name|Type|Description|
    |:--|:--|:--|
    |name	|string	|The name field should contain the relative resource name.|
    |parent	|string	|For resource definitions and List/Create requests, the parent field should contain the parent relative resource name.|
    |create_time	|Timestamp	|The creation timestamp of an entity.|
    |update_time	|Timestamp	|The last update timestamp of an entity. Note: update_time is updated when create/patch/delete operation is performed.|
    |delete_time	|Timestamp	|The deletion timestamp of an entity, only if it supports retention.|
    |expire_time	|Timestamp	|The expiration timestamp of an entity if it happens to expire.|
    |start_time	|Timestamp	|The timestamp marking the beginning of some time period.|
    |end_time	|Timestamp	|The timestamp marking the end of some time period or operation (regardless of its success).|
    |read_time	|Timestamp	|The timestamp at which a particular an entity should be read (if used in a request) or was read (if used in a response).|
    |time_zone	|string	|The time zone name. It should be an IANA TZ name, such as "America/Los_Angeles". For more information, see https://en.wikipedia.org/wiki/List_of_tz_database_time_zones.|
    |region_code	|string	|The Unicode country/region code (CLDR) of a location, such as "US" and "419". For more information, see http://www.unicode.org/reports/tr35/#unicode_region_subtag.|
    |language_code	|string	|The BCP-47 language code, such as "en-US" or "sr-Latn". For more information, see http://www.unicode.org/reports/tr35/#Unicode_locale_identifier.|
    |mime_type	|string	|An IANA published MIME type (also referred to as media type). For more information, see https://www.iana.org/assignments/media-types/media-types.xhtml.|
    |display_name	|string	|The display name of an entity.|
    |title	|string	|The official name of an entity, such as company name. It should be treated as the formal version of display_name.|
    |description	|string	|One or more paragraphs of text description of an entity.|
    |filter	|string	|The standard filter parameter for List methods. See AIP-160.|
    |query	|string	|The same as filter if being applied to a search method (ie :search)|
    |page_token	|string	|The pagination token in the List request.|
    |page_size	|int32	|The pagination size in the List request.|
    |total_size	|int32	|The total count of items in the list irrespective of pagination.|
    |next_page_token	|string	|The next pagination token in the List response. It should be used as page_token for the following request. An |empty value means no more result.|
    |order_by	|string	|Specifies the result ordering for List requests.|
    |progress_percent	|int32	|Specifies the progress of an action in percentage (0-100). The value -1 means the progress is unknown.|
    |request_id	|string	|A unique string id used for detecting duplicated requests.|
    |resume_token	|string	|An opaque token used for resuming a streaming request.|
    |labels	|map<string, string>	|Represents Cloud resource labels.|
    |show_deleted	|bool	|If a resource allows undelete behavior, the corresponding List method must have a show_deleted field so client can discover the deleted resources.|
    |update_mask	|FieldMask	|It is used for Update request message for performing partial update on a resource. This mask is relative to the resource, not to the request message.|
    |validate_only	|bool	|If true, it indicates that the given request should only be validated, not executed.|

1.  #### Errors
    APIs SHOULD use a simple protocol-agnostic error model, which allows us to offer a consistent experience across different APIs, different API protocols (such as gRPC), and different error contexts (such as asynchronous, batch, or workflow errors).

    1. #### Error Model
    
    The error model for APIs is logically defined by google.rpc.Status, an instance of which is returned to the client when an API error occurs. The following code snippet shows the overall design of the error model:

    ```
    package google.rpc;

    // The `Status` type defines a logical error model that is suitable for
    // different programming environments, including REST APIs and RPC APIs.
    message Status {
    // A simple error code that can be easily handled by the client. The
    // actual error code is defined by `google.rpc.Code`.
    int32 code = 1;

    // A developer-facing human-readable error message in English. It should
    // both explain the error and offer an actionable resolution to it.
    string message = 2;

    // Additional error information that the client code can use to handle
    // the error, such as retry info or a help link.
    repeated google.protobuf.Any details = 3;
    }
    ```

    Because most APIs use resource-oriented API design, the error handling follows the same design principle by using a small set of standard errors with a large number of resources. For example, instead of defining different kinds of "not found" errors, the server uses one standard google.rpc.Code.NOT_FOUND error code and tells the client which specific resource was not found. The smaller error space reduces the complexity of documentation, affords better idiomatic mappings in client libraries, and reduces client logic complexity while not restricting the inclusion of actionable information.

    Note: When using gRPC, errors are included in the headers, and total headers in responses are limited to 8 KB (8,192 bytes). Ensure that errors do not exceed 1-2 KB in size.

    1. #### Error Codes
    
    APIs must use the canonical error codes defined by google.rpc.Code. Individual APIs must avoid defining additional error codes, since developers are very unlikely to write logic to handle a large number of error codes. For reference, handling an average of three error codes per API call would mean most application logic would just be for error handling, which would not be a good developer experience.

    1. #### Error Messages
        The error message should help users understand and resolve the API error easily and quickly. In general, consider the following    guidelines when writing error messages:

        1. Do not assume the user is an expert user of your API. Users could be client developers, operations people, IT staff, or end-users of apps.
        1. Do not assume the user knows anything about your service implementation or is familiar with the context of the errors (such as log analysis).
        1. When possible, error messages should be constructed such that a technical user (but not necessarily a developer of your API) can respond to the error and correct it.
        1. Keep the error message brief. If needed, provide a link where a confused reader can ask questions, give feedback, or get more information that doesn't cleanly fit in an error message. Otherwise, use the details field to expand.
   
    Warning: Error messages are not part of the API surface. They are subject to changes without notice. Application code must not have a hard dependency on error messages.
   
    1. #### Error Details

        APIs define a set of standard error payloads for error details, which you can find in google/rpc/error_details.proto. These cover the most common needs for API errors, such as quota failure and invalid parameters. Like error codes, developers should use these standard payloads whenever possible.

        Additional error detail types should only be introduced if they can assist application code to handle the errors. If the error information can only be handled by humans, rely on the error message content and let developers handle it manually rather than introducing additional error detail types.

        Here are some example error_details payloads:

        1. ErrorInfo: Provides structured error information that is both stable and extensible.
        1. RetryInfo: Describes when clients can retry a failed request, may be returned on Code.UNAVAILABLE or Code.ABORTED
        1. QuotaFailure: Describes how a quota check failed, may be returned on Code.RESOURCE_EXHAUSTED
        1. BadRequest: Describes violations in a client request, may be returned on Code.INVALID_ARGUMENT
    
    1. #### Error Info
        
        ErrorInfo is a special kind of error payload. It provides stable and extensible error information that both humans and applications can depend on. Each ErrorInfo has three pieces of information: an error domain, an error reason, and a set of error metadata, such as this example. For more information, see the ErrorInfo definition.

        For APIs, the primary error domain is api.{{app_name}}.com, and the corresponding error reasons are defined by api.{{app_name}}.ErrorReason enum. For more information, see the api.{{app_name}}.ErrorReason definition.

    1. #### Error Localization
        The message field in google.rpc.Status is developer-facing and must be in English.

        If a user-facing error message is needed, use google.rpc.LocalizedMessage as your details field. While the message field in google.rpc.LocalizedMessage can be localized, ensure that the message field in google.rpc.Status is in English.

        By default, the API service should use the authenticated user’s locale or HTTP Accept-Language header or the language_code parameter in the request to determine the language for the localization.

    1. #### Error Mapping
    
        APIs are accessible in different programming environments. Each environment typically has its own way of error handling. The following sections explain how the error model is mapped in commonly used environments.

    1. #### HTTP Mapping

        While proto3 messages have native JSON encoding, API Platform uses a different error schema for JSON HTTP APIs for backward compatibility reasons.

    #### Schema:

    ```
    // This message defines the error schema for Google's JSON HTTP APIs.
    message Error {
    // Deprecated. This message is only used by error format v1.
    message ErrorProto {}
    // This message has the same semantics as `google.rpc.Status`. It uses HTTP
    // status code instead of gRPC status code. It has extra fields `status` and
    // `errors` for backward compatibility with [Google API Client
    // Libraries](https://developers.google.com/api-client-library).
    message Status {
        // The HTTP status code that corresponds to `google.rpc.Status.code`.
        int32 code = 1;
        // This corresponds to `google.rpc.Status.message`.
        string message = 2;
        // Deprecated. This field is only used by error format v1.
        repeated ErrorProto errors = 3;
        // This is the enum version for `google.rpc.Status.code`.
        google.rpc.Code status = 4;
        // This corresponds to `google.rpc.Status.details`.
        repeated google.protobuf.Any details = 5;
    }
    // The actual error payload. The nested message structure is for backward
    // compatibility with [Google API Client
    // Libraries](https://developers.google.com/api-client-library). It also
    // makes the error more readable to developers.
    Status error = 1;
    }
    ```

    #### Example (link):

    ```
    {
    "error": {
        "code": 400,
        "message": "API key not valid. Please pass a valid API key.",
        "status": "INVALID_ARGUMENT",
        "details": [
        {
            "@type": "type.googleapis.com/google.rpc.ErrorInfo",
            "reason": "API_KEY_INVALID",
            "domain": "googleapis.com",
            "metadata": {
            "service": "translate.googleapis.com"
            }
        }
        ]
    }
    }
    ```

    1. #### gRPC Mapping

    Different RPC protocols map the error model differently. For gRPC, the error model is natively supported by the generated code and the runtime library in each supported language. You can find out more in gRPC's API documentation. For example, see gRPC Java's io.grpc.Status.

    1. #### Client Library Mapping

    Google client libraries may choose to surface errors differently per language to be consistent with established idioms. For example, the google-cloud-go library will return an error that implements the same interface as google.rpc.Status, while google-cloud-java will raise an Exception.

    1. #### Handling Errors

        Below is a table containing all of the gRPC error codes defined in google.rpc.Code and a short description of their cause. To handle an error, you can check the description for the returned status code and modify your call accordingly.

        |HTTP|gRPC|Description|
        |:--|:--|:--|
        |200	|OK|	No error.|
        |400	|INVALID_ARGUMENT|	Client specified an invalid argument. Check error message and error details for more information.|
        |400	|FAILED_PRECONDITION|	Request can not be executed in the current system state, such as deleting a non-empty directory.|
        |400	|OUT_OF_RANGE|	Client specified an invalid range.|
        |401	|UNAUTHENTICATED|	Request not authenticated due to missing, invalid, or expired OAuth token.|
        |403	|PERMISSION_DENIED|	Client does not have sufficient permission. This can happen because the OAuth token does not have the right scopes, the client doesn't have permission, or the API has not been enabled.|
        |404	|NOT_FOUND|	A specified resource is not found.|
        |409	|ABORTED|	Concurrency conflict, such as read-modify-write conflict.|
        |409	|ALREADY_EXISTS|	The resource that a client tried to create already exists.|
        |429	|RESOURCE_EXHAUSTED|	Either out of resource quota or reaching rate limiting. The client should look for google.rpc.QuotaFailure error detail for more information.|
        |499	|CANCELLED|	Request cancelled by the client.|
        |500	|DATA_LOSS|	Unrecoverable data loss or data corruption. The client should report the error to the user.|
        |500	|UNKNOWN|	Unknown server error. Typically a server bug.|
        |500	|INTERNAL|	Internal server error. Typically a server bug.|
        |501	|NOT_IMPLEMENTED|	API method not implemented by the server.|
        |502	|N/A|	Network error occurred before reaching the server. Typically a network outage or misconfiguration.|
        |503	|UNAVAILABLE|	Service unavailable. Typically the server is down.|
        |504	|DEADLINE_EXCEEDED|	Request deadline exceeded. This will happen only if the caller sets a deadline that is shorter than the method's default deadline (i.e. requested deadline is not enough for the server to process the request) and the request did not finish within the deadline.|
    
    Warning: APIs may concurrently check multiple preconditions for an API request. Returning one error code does not imply other preconditions are satisfied. Application code must not depend on the ordering of precondition checks.

    1. #### Retrying Errors

        Clients may retry on 503 UNAVAILABLE errors with exponential backoff. The minimum delay should be 1s unless it is documented otherwise. The default retry repetition should be once unless it is documented otherwise.

        For 429 RESOURCE_EXHAUSTED errors, the client may retry at the higher level with minimum 30s delay. Such retries are only useful for long running background jobs.

        For all other errors, retry may not be applicable. First ensure your request is idempotent, and see google.rpc.RetryInfo for guidance.

    1. #### Propagating Errors
        If your API service depends on other services, you should not blindly propagate errors from those services to your clients. When translating errors, we suggest the following:

        1. Hide implementation details and confidential information.
        1. Adjust the party responsible for the error. For example, a server that receives an INVALID_ARGUMENT error from another service should propagate an INTERNAL to its own caller.
    
    1. #### Generating Errors
        If you are a server developer, you should generate errors with enough information to help client developers understand and resolve the problem. At the same time, you must be aware of the security and privacy of the user data, and avoid disclosing sensitive information in the error message and error details, since errors are often logged and may be accessible by others. For example, an error message like "Client IP address is not on allowlist 128.0.0.0/8" exposes information about the server-side policy, which may not be accessible to the user who has access to the logs.

        To generate proper errors, you first need to be familiar with google.rpc.Code to choose the most suitable error code for each error condition. A server application may check multiple error conditions in parallel, and return the first one.

        The following table lists each error code and an example of a good error message.

        |HTTP|gRPC|Example Error Message|
        |:--|:--|:--|
        |400|	INVALID_ARGUMENT|	Request field x.y.z is xxx, expected one of [yyy, zzz].|
        |400|	FAILED_PRECONDITION|	Resource xxx is a non-empty directory, so it cannot be deleted.|
        |400|	OUT_OF_RANGE|	Parameter 'age' is out of range [0, 125].|
        |401|	UNAUTHENTICATED|	Invalid authentication credentials.|
        |403|	PERMISSION_DENIED|	Permission 'xxx' denied on resource 'yyy'.|
        |404|	NOT_FOUND|	Resource 'xxx' not found.|
        |409|	ABORTED|	Couldn’t acquire lock on resource ‘xxx’.|
        |409|	ALREADY_EXISTS|	Resource 'xxx' already exists.|
        |429|	RESOURCE_EXHAUSTED|	Quota limit 'xxx' exceeded.|
        |499|	CANCELLED|	Request cancelled by the client.|
        |500|	DATA_LOSS|	See note.|
        |500|	UNKNOWN|	See note.|
        |500|	INTERNAL|	See note.|
        |501|	NOT_IMPLEMENTED|	Method 'xxx' not implemented.|
        |503|	UNAVAILABLE|	See note.|
        |504|	DEADLINE_EXCEEDED|	See note.|

        Note: Since the client cannot fix the server error, it is not useful to generate additional error details. To avoid leaking sensitive information under error conditions, it is recommended not to generate any error message and only generate google.rpc.DebugInfo error details. The DebugInfo is specially designed only for server-side logging, and must not be sent to client.
    
    1. #### Error Payloads
        The google.rpc package defines a set of standard error payloads, which are preferred to custom error payloads. The following table lists each error code and its matching standard error payload, if applicable. We recommend advanced applications look for these error payloads in google.rpc.Status when they handle errors.

    |HTTP|gRPC|Recommended Error Detail|
    |:--|:--|:--|
    |400|	INVALID_ARGUMENT|	google.rpc.BadRequest|
    |400|	FAILED_PRECONDITION	|google.rpc.PreconditionFailure|
    |400|	OUT_OF_RANGE|	google.rpc.BadRequest|
    |401|	UNAUTHENTICATED|	google.rpc.ErrorInfo|
    |403|	PERMISSION_DENIED|	google.rpc.ErrorInfo|
    |404|	NOT_FOUND|	google.rpc.ResourceInfo|
    |409|	ABORTED	|google.rpc.ErrorInfo|
    |409|	ALREADY_EXISTS|	google.rpc.ResourceInfo|
    |429|	RESOURCE_EXHAUSTED|	google.rpc.QuotaFailure|
    |499|	CANCELLED	||
    |500|	DATA_LOSS	|google.rpc.DebugInfo|
    |500|	UNKNOWN	|google.rpc.DebugInfo|
    |500|	INTERNAL|	google.rpc.DebugInfo|
    |501|	NOT_IMPLEMENTED	||
    |503|	UNAVAILABLE|	google.rpc.DebugInfo|
    |504|	DEADLINE_EXCEEDED|	google.rpc.DebugInfo|

1. #### Naming Conventions
    In order to provide consistent developer experience across many APIs and over a long period of time, all names used by an API should be: simple, intuitive, consistent.

    This includes names of interfaces, resources, collections, methods, and messages.

    Since many developers are not native English speakers, one goal of these naming conventions is to ensure that the majority of developers can easily understand an API. It does this by encouraging the use of a simple, consistent, and small vocabulary when naming methods and resources.
    1. #### Vocabulary
        1. Names used in APIs should be in correct American English. For example, license (instead of licence), color (instead of colour).
        1. Commonly accepted short forms or abbreviations of long words may be used for brevity. For example, API is preferred over Application Programming Interface.
        1. Use intuitive, familiar terminology where possible. For example, when describing removing (and destroying) a resource, delete is preferred over erase.
        1. Use the same name or term for the same concept, including for concepts shared across APIs.
        1. Avoid name overloading. Use different names for different concepts.
        1. Avoid overly general names that are ambiguous within the context of the API and the larger ecosystem of APIs. They can lead to misunderstanding of API concepts. Rather, choose specific names that accurately describe the API concept. This is particularly important for names that define first-order API elements, such as resources. There is no definitive list of names to avoid, as every name must be evaluated in the context of other names. Instance, info, and service are examples of names that have been problematic in the past. Names chosen should describe the API concept clearly (for example: instance of what?) and distinguish it from other relevant concepts (for example: does "alert" mean the rule, the signal, or the notification?).
        1. Carefully consider use of names that may conflict with keywords in common programming languages. Such names may be used but will likely trigger additional scrutiny during API review. Use them judiciously and sparingly.
    
    1. #### Product names
        Product names refer to the product marketing names of APIs, such as Google Calendar API. Product names must be consistently used by APIs, UIs, documentation, Terms of Service, billing statements, commercial contracts, etc. APIs must use product names approved by the product and marketing teams.

        The table below shows examples of all related API names and their consistency. See further below on this page for more details on the respective names and their conventions.

        |API Name|Example|
        |:--|:--|
        |Product Name|	Google Calendar API|
        |Service Name|	calendar.googleapis.com|
        |Package Name|	google.calendar.v3|
        |Interface Name|	google.calendar.v3.CalendarService|
        |Source Directory|	//google/calendar/v3|
        |API Name|	calendar|

    1. #### Service names
        Service names should be syntactically valid DNS names (as per RFC 1035) which can be resolved to one or more network addresses. The service names of public APIs follow the pattern: xxx.{{app_name}}apis.com. For example, the service name of the Google Calendar is calendar.googleapis.com.

        If an API is composed of several services they should be named in a way to help discoverability. One way to do this is for the Service Names to share a common prefix. For example the services build.googleapis.com and buildresults.googleapis.com are both services that are part of the Google Build API.

    1. #### Package names
        Package names declared in the API .proto files should be consistent with Product Names and Service Names. Package names should use singular component names to avoid mixed singular and plural component names. Package names must not use underscores. Package names for versioned APIs must end with the version. For example:

        // Google Calendar API
        package google.calendar.v3;
        
        An abstract API that isn't directly associated with a service, such as Google Watcher API, should use proto package names consistent with the Product name:

        // Google Watcher API
        package google.watcher.v1;
        Java package names specified in the API .proto files must match the proto package names with standard Java package name prefix (com., edu., net., etc). For example:

        package google.calendar.v3;

        // Specifies Java package name, using the standard prefix "com."
        option java_package = "com.google.calendar.v3";

    1. #### Collection IDs
        Collection IDs should use plural form and lowerCamelCase, and American English spelling and semantics. For example: events, children, or deletedEvents.

    1. #### Interface names
        To avoid confusion with Service Names such as pubsub.googleapis.com, the term interface name refers to the name used when defining a service in a .proto file:
        ```
        // Library is the interface name.
        service Library {
        rpc ListBooks(...) returns (...);
        rpc ...
        }
        ```
        You can think of the service name as a reference to the actual implementation of a set of APIs, while the interface name refers to the abstract definition of an API.

        An interface name should use an intuitive noun such as Calendar or Blob. The name should not conflict with any well-established concepts in programming languages and their runtime libraries (for example, File).

        In the rare case where an interface name would conflict with another name within the API, a suffix (for example Api or Service) should be used to disambiguate.

    1. #### Method names
        A service may, in its IDL specification, define one or more RPC methods that correspond to methods on collections and resources. The method names should follow the naming convention of VerbNoun in upper camel case, where the noun is typically the resource type.

        |Verb|Noun|Method name|Request message|Response message|
        |:--|:--|:--|:--|:--|
        |List|	Book|	ListBooks|	ListBooksRequest|	ListBooksResponse|
        |Get|	Book|	GetBook|	GetBookRequest|	Book|
        |Create|	Book|	CreateBook|	CreateBookRequest|	Book|
        |Update|	Book|	UpdateBook|	UpdateBookRequest|	Book|
        |Rename|	Book|	RenameBook|	RenameBookRequest|	RenameBookResponse|
        |Delete|	Book|	DeleteBook|	DeleteBookRequest|	google.protobuf.Empty|
    
        The verb portion of the method name should use the imperative mood, which is for orders or commands rather than the indicative mood which is for questions.

        For standard methods, the noun portion of the method name must be singular for all methods except List, and must be plural for List. For custom methods, the noun may be singular or plural as appropriate. Batch methods must use the plural noun.

        Note: The case above refers to the RPC name in protocol buffers; the HTTP/JSON URI suffixes use :lowerCamelCase.
        This is easily confused when the verb is asking a question about a sub-resource in the API, which is often expressed in the indicative mood. For example, ordering the API to create a book is clearly CreateBook (in the imperative mood), but asking the API about the state of the book's publisher might use the indicative mood, such as IsBookPublisherApproved or NeedsPublisherApproval. To remain in the imperative mood in situations like this, rely on commands such as "check" (CheckBookPublisherApproved) and "validate" (ValidateBookPublisher).

        Method names should not include prepositions (e.g. "For", "With", "At", "To"). Generally, method names with prepositions indicate that a new method is being used where a field should instead be added to an existing method, or the method should use a distinct verb.

        For example, if a CreateBook message already exists and you are considering adding CreateBookFromDictation, consider a TranscribeBook method instead.

    1. #### Message names
   
        Message names should be short and concise. Avoid unnecessary or redundant words. Adjectives can often be omitted if there is no corresponding message without the adjective. For example, the Shared in SharedProxySettings is unnecessary if there are no unshared proxy settings.

        Message names should not include prepositions (e.g. "With", "For"). Generally, message names with prepositions are better represented with optional fields on the message.

    1. #### Request and response messages
        The request and response messages for RPC methods should be named after the method names with the suffix Request and Response, respectively, unless the method request or response type is:

        1. an empty message (use google.protobuf.Empty),
        1. a resource type, or
        1. a resource representing an operation
        1. This typically applies to requests or responses used in standard methods Get, Create, Update, or Delete.

    1. #### Enum names
        Enum types must use UpperCamelCase names.

        Enum values must use CAPITALIZED_NAMES_WITH_UNDERSCORES. Each enum value must end with a semicolon, not a comma. The first value should be named ENUM_TYPE_UNSPECIFIED as it is returned when an enum value is not explicitly specified.
        ```
        enum FooBar {
        // The first value represents the default and must be == 0.
        FOO_BAR_UNSPECIFIED = 0;
        FIRST_VALUE = 1;
        SECOND_VALUE = 2;
        }
        ```

    1. #### Wrappers
    
        Messages encapsulating proto2 enum types where the 0 value has meaning other than UNSPECIFIED should be named with the suffix Value and have a single field named value.
        ```
        enum OldEnum {
        VALID = 0;
        OTHER_VALID = 1;
        }
        message OldEnumValue {
        OldEnum value = 1;
        }
        ```

    1. #### Field names

        Field definitions in the .proto files must use lower_case_underscore_separated_names. These names will be mapped to the native naming convention in generated code for each programming language.

        Field names should not include prepositions (e.g. "for", "during", "at"), for example:
        1. reason_for_error should instead be error_reason
        1. cpu_usage_at_time_of_failure should instead be failure_time_cpu_usage
        
        Field names should not use postpositive adjectives (modifiers placed after the noun), for example:
        1. items_collected should instead be collected_items
        1. objects_imported should instead be imported_objects
        
        #### Repeated field names
            Repeated fields in APIs must use proper plural forms. This matches the convention of existing APIs, and the common expectation of external developers.

    1. #### Time and Duration
        To represent a point in time independent of any time zone or calendar, google.protobuf.Timestamp should be used, and the field name should end with time, such as start_time and end_time.

        If the time refers to an activity, the field name should have the form of verb_time, such as create_time, update_time. Avoid using past tense for the verb, such as created_time or last_updated_time.

        To represent a span of time between two points in time independent of any calendar and concepts like "day" or "month", google.protobuf.Duration should be used.

        ```
        message FlightRecord {
        google.protobuf.Timestamp takeoff_time = 1;
        google.protobuf.Duration flight_duration = 2;
        }
        ```

        If you have to represent time-related fields using an integer type for legacy or compatibility reasons, including wall-clock time, duration, delay and latency, the field names must have the following form:

        `xxx_{time|duration|delay|latency}_{seconds|millis|micros|nanos}`

        ```
        message Email {
        int64 send_time_millis = 1;
        int64 receive_time_millis = 2;
        }
        ```

        If you have to represent timestamp using string type for legacy or compatibility reasons, the field names should not include any unit suffix. The string representation should use RFC 3339 format, e.g. "2014-07-30T10:43:17Z".

    1. #### Date and Time of Day
        For dates that are independent of time zone and time of day, google.type.Date should be used and it should have the suffix _date. If a date must be represented as a string, it should be in the ISO 8601 date format YYYY-MM-DD, e.g. 2014-07-30.

        For times of day that are independent of time zone and date, google.type.TimeOfDay should be used and should have the suffix _time. If a time of day must be represented as a string, it should be in the ISO 8601 24-hour time format HH:MM:SS[.FFF], e.g. 14:55:01.672.

        ```
        message StoreOpening {
        google.type.Date opening_date = 1;
        google.type.TimeOfDay opening_time = 2;
        }
        ```

    1. #### Quantities
        Quantities represented by an integer type must include the unit of measurement.

        `xxx_{bytes|width_pixels|meters}`

        If the quantity is a number of items, then the field should have the suffix _count, for example node_count.

    1. #### List filter field

        If an API supports filtering of resources returned by the List method, the field containing the filter expression should be named filter. For example:
        ```
        message ListBooksRequest {
        // The parent resource name.
        string parent = 1;

        // The filter expression.
        string filter = 2;
        }
        ```

    1. #### List response
        The name of the field in the List method's response message, which contains the list of resources must be a plural form of the resource name itself. For example, a method CalendarApi.ListEvents() must define a response message ListEventsResponse with a repeated field called events for the list of returned resources.

        ```
        service CalendarApi {
        rpc ListEvents(ListEventsRequest) returns (ListEventsResponse) {
            option (google.api.http) = {
            get: "/v3/{parent=calendars/*}/events";
            };
        }
        }

        message ListEventsRequest {
        string parent = 1;
        int32 page_size = 2;
        string page_token = 3;
        }

        message ListEventsResponse {
        repeated Event events = 1;
        string next_page_token = 2;
        }
        ```

    1. #### Camel case
        Except for field names and enum values, all definitions inside .proto files must use UpperCamelCase names, as defined by Google Java Style.

    1. #### Name abbreviation
        For well known name abbreviations among software developers, such as config and spec, the abbreviations should be used in API definitions instead of the full spelling. This will make the source code easy to read and write. In formal documentations, the full spelling should be used. Examples:

        1. config (configuration)
        1. id (identifier)
        1. spec (specification)
        1. stats (statistics)

1. #### Common design patterns
    1. #### Empty Responses
        The standard Delete method should return google.protobuf.Empty, unless it is performing a "soft" delete, in which case the method should return the resource with its state updated to indicate the deletion in progress.

        For custom methods, they should have their own XxxResponse messages even if they are empty, because it is very likely their functionality will grow over time and need to return additional data.

    1. #### Representing Ranges
        Fields that represent ranges should use half-open intervals with naming convention [start_xxx, end_xxx), such as [start_key, end_key) or [start_time, end_time). Half-open interval semantics is commonly used by C++ STL library and Java standard library. APIs should avoid using other ways of representing ranges, such as (index, count), or [first, last].

    1. #### Resource Labels
        In a resource-oriented API, the resource schema is defined by the API. To let the client attach small amount of simple metadata to the resources (for example, tagging a virtual machine resource as a database server), APIs should add a field map<string, string> labels to the resource definition:
        ```
        message Book {
        string name = 1;
        map<string, string> labels = 2;
        }
        ```

    1. #### Long Running Operations
        If an API method typically takes a long time to complete, it can be designed to return a Long Running Operation resource to the client, which the client can use to track the progress and receive the result. The Operation defines a standard interface to work with long running operations. Individual APIs must not define their own interfaces for long running operations to avoid inconsistency.

        The operation resource must be returned directly as the response message and any immediate consequence of the operation should be reflected in the API. For example, when creating a resource, that resource should appear in LIST and GET methods though the resource should indicate that it is not ready for use. When the operation is complete, the Operation.response field should contain the message that would have been returned directly, if the method was not long running.

        An operation can provide information about its progress using the Operation.metadata field. An API should define a message for this metadata even if the initial implementation does not populate the metadata field.

    1. #### List Pagination
        Listable collections should support pagination, even if results are typically small.

        Rationale: If an API does not support pagination from the start, supporting it later is troublesome because adding pagination breaks the API's behavior. Clients that are unaware that the API now uses pagination could incorrectly assume that they received a complete result, when in fact they only received the first page.

        To support pagination (returning list results in pages) in a List method, the API shall:

        1. define a string field page_token in the List method's request message. The client uses this field to request a specific page of the list results.
        1. define an int32 field page_size in the List method's request message. Clients use this field to specify the maximum number of results to be returned by the server. The server may further constrain the maximum number of results returned in a single page. If the page_size is 0, the server will decide the number of results to be returned.
        1. define a string field next_page_token in the List method's response message. This field represents the pagination token to retrieve the next page of results. If the value is "", it means no further results for the request.
        1. To retrieve the next page of results, client shall pass the value of response's next_page_token in the subsequent List method call (in the request message's page_token field):

        ```
        rpc ListBooks(ListBooksRequest) returns (ListBooksResponse);

        message ListBooksRequest {
        string parent = 1;
        int32 page_size = 2;
        string page_token = 3;
        }

        message ListBooksResponse {
        repeated Book books = 1;
        string next_page_token = 2;
        }
        ```

        When clients pass in query parameters in addition to a page token, the service must fail the request if the query parameters are not consistent with the page token.

        Page token contents should be a url-safe base64 encoded protocol buffer. This allows the contents to evolve without compatibility issues. If the page token contains potentially sensitive information, that information should be encrypted. Services must prevent tampering with page tokens from exposing unintended data through one of the following methods:

        require query parameters to be respecified on follow up requests.
        only reference server-side session state in the page token.
        encrypt and sign the query parameters in the page token and revalidate and reauthorize these parameters on every call.
        An implementation of pagination may also provide the total count of items in an int32 field named total_size.

    1. #### List Sub-Collections
        Sometimes, an API needs to let a client List/Search across sub- collections. For example, the Library API has a collection of shelves, and each shelf has a collection of books, and a client wants to search for a book across all shelves. In such cases, it is recommended to use standard List on the sub-collection and specify the wildcard collection id "-" for the parent collection(s). For the Library API example, we can use the following REST API request:
        `GET https://library.googleapis.com/v1/shelves/-/books?filter=xxx`
        Note: the reason to choose "-" instead of "*" is to avoid the need for URL escaping.
    
    1. #### Get Unique Resource From Sub-Collection
        Sometimes, a resource within a sub-collection has an identifier that is unique within its parent collection(s). In this case, it may be useful to allow a Get to retrieve that resource without knowing which parent collection contains it. In such cases, it is recommended to use a standard Get on the resource and specify the wildcard collection id "-" for all parent collections within which the resource is unique. For example, in the Library API, we can use the following REST API request, if the book is unique among all books on all shelves:
        `GET https://library.googleapis.com/v1/shelves/-/books/{id}`
    
        The resource name in the response to this call must use the canonical name of the resource, with actual parent collection identifiers instead of "-" for each parent collection. For example, the request above should return a resource with a name like shelves/shelf713/books/book8141, not shelves/-/books/book8141.

    1. #### Sorting Order
        If an API method lets client specify sorting order for list results, the request message should contain a field:
        `string order_by = ...;`
        
        The string value should follow SQL syntax: comma separated list of fields. For example: "foo,bar". The default sorting order is ascending. To specify descending order for a field, a suffix " desc" should be appended to the field name. For example: "foo desc,bar".

        Redundant space characters in the syntax are insignificant. "foo,bar desc" and "  foo ,  bar  desc  " are equivalent.

    1. #### Request Validation
        If an API method has side effects and there is a need to validate the request without causing such side effects, the request message should contain a field:
        `bool validate_only = ...;`

        If this field is set to true, the server must not execute any side effects and only perform implementation-specific validation consistent with the full request.

        If validation succeeds, google.rpc.Code.OK must be returned and any full request using the same request message should not return google.rpc.Code.INVALID_ARGUMENT. Note that the request may still fail due to other errors such as google.rpc.Code.ALREADY_EXISTS or because of race conditions.

    1. #### Request Duplication
        For network APIs, idempotent API methods are highly preferred, because they can be safely retried after network failures. However, some API methods cannot easily be idempotent, such as creating a resource, and there is a need to avoid unnecessary duplication. For such use cases, the request message should contain a unique ID, like a UUID, which the server will use to detect duplication and make sure the request is only processed once.

        // A unique request ID for server to detect duplicated requests.
        // This field **should** be named as `request_id`.
        `string request_id = ...;`

        If a duplicate request is detected, the server should return the response for the previously successful request, because the client most likely did not receive the previous response.

    1. #### Enum Default Value
        Every enum definition must start with a 0 valued entry, which shall be used when an enum value is not explicitly specified. APIs must document how 0 values are handled.

        The enum value 0 should be named as ENUM_TYPE_UNSPECIFIED. If there is a common default behavior, then it shall be used when an enum value is not explicitly specified. If there is no common default behavior, then the 0 value should be rejected with error INVALID_ARGUMENT when used.

        ```
        enum Isolation {
        // Not specified.
        ISOLATION_UNSPECIFIED = 0;
        // Reads from a snapshot. Collisions occur if all reads and writes cannot be
        // logically serialized with concurrent transactions.
        SERIALIZABLE = 1;
        // Reads from a snapshot. Collisions occur if concurrent transactions write
        // to the same rows.
        SNAPSHOT = 2;
        ...
        }
        ```

        // When unspecified, the server will use an isolation level of SNAPSHOT or
        // better.
        `Isolation level = 1;`

        An idiomatic name may be used for the 0 value. For example, google.rpc.Code.OK is the idiomatic way of specifying the absence of an error code. In this case, OK is semantically equivalent to UNSPECIFIED in the context of the enum type.

        In cases where an intrinsically sensible and safe default exists, that value may be used for the '0' value. For example, BASIC is the '0' value in the Resource View enum.

    1. #### Grammar Syntax

        In API designs, it is often necessary to define simple grammars for certain data formats, such as acceptable text input. To provide a consistent developer experience across APIs and reduce learning curve, API designers must use the following variant of Extended Backus-Naur Form (EBNF) syntax to define such grammars:

        ```
        Production  = name "=" [ Expression ] ";" ;
        Expression  = Alternative { "|" Alternative } ;
        Alternative = Term { Term } ;
        Term        = name | TOKEN | Group | Option | Repetition ;
        Group       = "(" Expression ")" ;
        Option      = "[" Expression "]" ;
        Repetition  = "{" Expression "}" ;
        ```

        Note: TOKEN represents terminal symbols defined outside the grammar.

    1. #### Integer Types
        
        In API designs, unsigned integer types such as uint32 and fixed32 should not be used because some important programming languages and systems don't support them well, such as Java, JavaScript and OpenAPI. And they are more likely to cause overflow errors. Another issue is that different APIs are very likely to use mismatched signed and unsigned types for the same thing.

        When signed integer types are used for things where the negative values are not meaningful, such as size or timeout, the value -1 (and only -1) may be used to indicate special meaning, such as end of file (EOF), infinite timeout, unlimited quota limit, or unknown age. Such usages must be clearly documented to avoid confusion. API producers should also document the behavior of the implicit default value 0 if it is not very obvious.

    1. #### Partial Response
        Sometimes an API client only needs a specific subset of data in the response message. To support such use cases, some API platforms provide native support for partial responses. API Platform supports it through response field mask.

        For any REST API call, there is an implicit system query parameter $fields, which is the JSON representation of a google.protobuf.FieldMask value. The response message will be filtered by the $fields before being sent back to the client. This logic is handled automatically for all API methods by the API Platform.

        `GET https://library.googleapis.com/v1/shelves?$fields=shelves.name`

        `GET https://library.googleapis.com/v1/shelves/123?$fields=name`

        Note: This logic always uses the response structure as the root for the field mask.

    1. #### Resource View
        To reduce network traffic, it is sometimes useful to allow the client to limit which parts of the resource the server should return in its responses, returning a view of the resource instead of the full resource representation. The resource view support in an API is implemented by adding a parameter to the method request which allows the client to specify which view of the resource it wants to receive in the response.
        
        The parameter:
        1. should be of an enum type
        1. must be named view
        1. Each value of the enumeration defines which parts of the resource (which fields) will be returned in the server's response. 
        1. Exactly what is returned for each view value is implementation-defined and should be specified in the API documentation.

        ```
        package google.example.library.v1;

        service Library {
        rpc ListBooks(ListBooksRequest) returns (ListBooksResponse) {
            option (google.api.http) = {
            get: "/v1/{name=shelves/*}/books"
            }
        };
        }

        enum BookView {
        // Not specified, equivalent to BASIC.
        BOOK_VIEW_UNSPECIFIED = 0;

        // Server responses only include author, title, ISBN and unique book ID.
        // The default value.
        BASIC = 1;

        // Full representation of the book is returned in server responses,
        // including contents of the book.
        FULL = 2;
        }

        message ListBooksRequest {
        string name = 1;

        // Specifies which parts of the book resource should be returned
        // in the response.
        BookView view = 2;
        }
        ```

        This construct will be mapped to URLs such as:

        `GET https://library.googleapis.com/v1/shelves/shelf1/books?view=BASIC`

        You can find out more about defining methods, requests, and responses in the Standard Methods chapter of this Design Guide.

    1. #### Output Fields
        APIs may want to distinguish between fields that are provided by the client as inputs and fields that are only returned by the server on output on a particular resource. For fields that are output only, the field attribute shall be annotated.

        Note that if output only fields are set in the request or included in a google.protobuf.FieldMask, the server must accept the request without error. The server must ignore the presence of output only fields and any indication of it. The reason for this recommendation is because clients often reuse resources returned by the server as another request input, e.g. a retrieved Book will be later reused in an UPDATE method. If output only fields are validated against, then this places extra work on the client to clear out output only fields.

        ```
        import "google/api/field_behavior.proto";

        message Book {
        string name = 1;
        Timestamp create_time = 2 [(google.api.field_behavior) = OUTPUT_ONLY];
        }
        ```

    1. #### Singleton Resources
        A singleton resource can be used when only a single instance of a resource exists within its parent resource (or within the API, if it has no parent).

        The standard Create and Delete methods must be omitted for singleton resources; the singleton is implicitly created or deleted when its parent is created or deleted (and implicitly exists if it has no parent). The resource must be accessed using the standard Get and Update methods, as well as any custom methods that are appropriate for your use case.

        For example, an API with User resources could expose per-user settings as a Settings singleton.

        ```
        rpc GetSettings(GetSettingsRequest) returns (Settings) {
        option (google.api.http) = {
            get: "/v1/{name=users/*/settings}"
        };
        }

        rpc UpdateSettings(UpdateSettingsRequest) returns (Settings) {
        option (google.api.http) = {
            patch: "/v1/{settings.name=users/*/settings}"
            body: "settings"
        };
        }

        [...]

        message Settings {
        string name = 1;
        // Settings fields omitted.
        }

        message GetSettingsRequest {
        string name = 1;
        }

        message UpdateSettingsRequest {
        Settings settings = 1;
        // Field mask to support partial updates.
        FieldMask update_mask = 2;
        }
        ```

    1. #### Streaming Half-Close
        For any bi-directional or client-streaming APIs, the server should rely on the client-initiated half-close, as provided by the RPC system, to complete the client-side stream. There is no need to define an explicit completion message.

        Any information that the client needs to send prior to the half-close must be defined as part of the request message.

    1. #### Domain-scoped names

        A domain-scoped name is an entity name that is prefixed by a DNS domain name to prevent name collisions. It is a useful design pattern when different organizations define their entity names in a decentralized manner. The syntax resembles a URI without a scheme.

        Domain-scoped names are widely used among APIs and Kubernetes APIs, such as:

        1. The Protobuf Any type representation: type.googleapis.com/google.protobuf.Any
        1. Stackdriver metric types: compute.googleapis.com/instance/cpu/utilization
        1. Label keys: cloud.googleapis.com/location
        1. Kubernetes API versions: networking.k8s.io/v1
        1. The kind field in the x-kubernetes-group-version-kind OpenAPI extension.

    1. #### Bool vs. Enum vs. String
        When designing an API method, it is very common to provide a set of choices for a specific feature, such as enabling tracing or disabling caching. The common way to achieve this is to introduce a request field of bool, enum, or string type. It is not always obvious what is the right type to use for a given use case. The recommended choice is as follows:

        Using bool type if we want to have a fixed design and intentionally don't want to extend the functionality. For example, bool enable_tracing or bool enable_pretty_print.

        Using an enum type if we want to have a flexible design but don't expect the design will change often. The rule of thumb is the enum definition will only change once a year or less often. For example, enum TlsVersion or enum HttpVersion.

        Using string type if we have an open ended design or the design can be changed frequently by an external standard. The supported values must be clearly documented. For example:

        1. string region_code as defined by Unicode regions.
        1. string language_code as defined by Unicode locales.
    
    1. #### Data Retention
        When designing an API service, data retention is a critical aspect of service reliability. It is common that user data is mistakenly deleted by software bugs or human errors. Without data retention and corresponding undelete functionality, a simple mistake can cause catastrophic business impact.

        In general, we recommend the following data retention policy for API services:

        1. For user metadata, user settings, and other important information, there should be 30-day data retention. For example, monitoring metrics, project metadata, and service definitions.

        1. For large-volume user content, there should be 7-day data retention. For example, binary blobs and database tables.

        1. For transient state or expensive storage, there should be 1-day data retention if feasible. For example, memcache instances and Redis servers.

        1. During the data retention window, the data can be undeleted without data loss. If it is expensive to offer data retention for free, a service can offer data retention as a paid option.

    1. #### Large Payloads
        Networked APIs often depend on multiple network layers for their data path. Most network layers have hard limits on the request and response size. 32MB is a commonly used limit in many systems.

        When designing an API method that handles payloads larger than 10MB, we should carefully choose the right strategy for usability and future growth. For APIs, we recommend to use either streaming or media upload/download to handle large payloads. With streaming, the server incrementally handles the large data synchronously. With media, the large data flows through a large storage system, and the server can handle the data asynchronously.

    1. #### Optional Primitive Fields
        Protocol Buffers v3 (proto3) supports optional primitive fields, which are semantically equivalent to nullable types in many programming languages. They can be used to distinguish empty values from unset values.

        In practice it is hard for developers to correctly handle optional fields. Most JSON HTTP client libraries, including Google API Client Libraries, cannot distinguish proto3 int32, google.protobuf.Int32Value, and optional int32. If an alternative design is equally clear and does not require an optional primitive, prefer that. If not using optional would add complexity or ambiguity, then use optional primitive fields. Wrapper types must not be used going forward. In general, API designers should use plain primitive types, such as int32, for simplicity and consistency.

1. #### Inline API documentation
    This section provides guidelines for adding inline documentation to your API. Most APIs will also have overviews, tutorials, and higher-level reference documentation, which are outside the scope of this Design Guide

    1. #### Comment format in proto files
        Add comments to your .proto file using the usual Protocol Buffers // comment format.
        ```
        // Creates a shelf in the library, and returns the new Shelf.
        rpc CreateShelf(CreateShelfRequest) returns (Shelf) {
        option (google.api.http) = { post: "/v1/shelves" body: "shelf" };
        }
        ```

    1. #### Comments in service configuration
        As an alternative to adding documentation comments to your .proto file, you can add inline documentation to your API in its YAML service configuration file. Documentation in this file will take precedence over documentation in your .proto if the same element is documented in both files.
        ```
        documentation:
        summary: Gets and lists social activities
        overview: A simple example service that lets you get and list possible social activities
        rules:
        - selector: google.social.Social.GetActivity
            description: Gets a social activity. If the activity does not exist, returns Code.NOT_FOUND.
        ...
        ```

        You may need to use this approach if you have multiple services that use the same .proto files and want to provide service-specific documentation. YAML documentation rules also let you add a more detailed overview to your API description. However, in general adding documentation comments to your .proto is preferred.

        As with .proto comments, you can use Markdown to provide additional formatting in your YAML file comments.

    1. #### API description
        The API description is a phrase starting with an active verb that describes what you can do with the API. In your .proto file, an API description is added as a comment to the corresponding service, as in the following example:

        ```
        // Manages books and shelves in a simple digital library.
        service LibraryService {
        ...
        }
        ```
        Here are some more example API descriptions:

        1. Shares updates, photos, videos, and more with your friends around the world.
        1. Accesses a cloud-hosted machine learning service that makes it easy to build smart apps that respond to streams of data.
    
    1. #### Resource description
        A resource description is a partial sentence that describes what the resource represents. If you need to add more detail, use additional sentences. In your .proto file, a resource description is added as a comment to the corresponding message type, as in the following example:

        ```
        // A book resource in the Library API.
        message Book {
        ...
        }
        ```
        Here are some example resource descriptions:
        1. A task on the user's to-do list. Each task has a unique priority.
        1. An event on the user's calendar.
    
    1. #### Field and parameter descriptions
        A noun phrase that describes a field or parameter definition, as shown in the following examples:
        1. The number of topics in this series.
        1. The accuracy of the latitude and longitude coordinates, in meters. Must be non-negative.
        1. Flag governing whether attachment URL values are returned for submission resources in this series. The default value for series.insert is true.
        1. The container for voting information. Present only when voting information is recorded.
        1. Not currently used or deprecated.
        
        Field and parameter descriptions should describe what values are valid and invalid. Remember that engineers will do their best to break any service, and will not be able to read the underlying code to clarify any unclear information.

        For strings, the description should describe the syntax and permissible characters, as well as any required encoding. For example:

        1. 1-255 characters in the set [A-a0-9]
        1. A valid URL path string starting with / that follows the RFC 2332 conventions. Max length is 500 characters.
        The description should specify any default value or behavior, but may omit describing default values that are effectively null.

        If the field value is required, input only, output only, it should be documented at the start of the field description. By default, all fields and parameters are optional. For example:

        ```
        message Table {
        // Required. The resource name of the table.
        string name = 1;

        // Input only. Whether to validate table creation without actually doing it.
        bool validate_only = 2;

        // Output only. The timestamp when the table was created. Assigned by
        // the server.
        google.protobuf.Timestamp create_time = 3;

        // The display name of the table.
        string display_name = 4;
        }
        ```
        Note: Field descriptions should provide an example value whenever it is feasible and helpful.

    1. #### Method description
        A method description is a sentence indicating what effect the method has and what resource it operates on. It usually starts with a third-person, present tense verb, i.e., in English, a verb ending in "s". If you need to add more details, use additional sentences. Here are some examples:
        1. Lists calendar events for the authenticated user.
        1. Updates a calendar event with the data included in the request.
        1. Deletes a location record from the authenticated user's location history.
        1. Creates or updates a location record in the authenticated user's location history using the data included in the request. If a location resource already exists with the same timestamp value, the data provided overwrites the existing data.
    
    1. #### Checklist for all descriptions
        Make sure each description is brief but complete and can be understood by users who don't have additional information about the API. In most cases, there's more to say than just restating the obvious; for example, the description of the series.insert method shouldn't just say "Inserts a series." — while your naming should be informative, most readers are reading your descriptions because they want more information than the names themselves provide. If you're not sure what else to say in a description, try answering all of the following questions that are relevant:

        1. What is it?
        1. What does it do if it succeeds? What does it do if it fails? What can cause it to fail, and how?
        1. Is it idempotent?
        1. What are the units? (Examples: meters, degrees, pixels.)
        1. What range of values does it accept? Is the range inclusive or exclusive?
        1. What are the side effects?
        1. How do you use it?
        1. What are common errors that may break it?
        1. Is it always present? (For example: "Container for voting information. Present only when voting information is recorded.")
        1. Does it have a default setting?
    
    1. #### Conventions
        This section lists some usage conventions for textual descriptions and documentation. For example, use "ID" (all caps), rather than "Id" or "id" when talking about identifiers. Use "JSON" rather than "Json" or "json" when referring to that data format. Show all field/parameter names in code font. Put literal string values in code font and quotes.

        1. ID
        1. JSON
        1. RPC
        1. REST
        1. property_name or "string_literal"
        1. true / false

    1. #### Requirement levels
        To set expectations or state requirement levels, use the terms: must, must not, required, shall, shall not, should, should not, recommended, may, and optional.

        The meanings are defined in RFC 2119. You might want to include the statement from the RFC abstract in your API documentation.

        Determine which term meets your requirements while giving developers flexibility. Don't use absolute terms like must when other options technically work in your API.

    1. Language style
        As in our naming conventions, we recommend using a simple, consistent vocabulary and style when writing doc comments. Comments should be understandable by readers who don't speak English natively, so avoid jargon, slang, complex metaphors, pop culture references, or anything else that won't easily translate. Use a friendly, professional style that speaks directly to the developers reading your comments, and keep things as concise as possible. Remember, most readers want to find out how to do something with your API, not read your documentation!

1. #### Protocol Buffers v3
    How to use Protocol Buffers with API design. To simplify developer experience and improve runtime efficiency, gRPC APIs should use Protocol Buffers version 3 (proto3) for API definition.

    Protocol Buffers is a simple language-neutral and platform-neutral Interface Definition Language (IDL) for defining data structure schemas and programming interfaces. It supports both binary and text wire formats, and works with many different wire protocols on different platforms.

    Proto3 is the latest version of Protocol Buffers and includes the following changes from proto2:

    1. Field presence, also known as hasField, is removed by default for primitive fields. An unset primitive field has a language-defined default value.
    1. Presence of a message field is still available, which can be tested using the compiler generated hasField method, or compare to null, or the sentinel value defined by the implementation.
    1. Beginning in protobuf v3.14, primitive fields can distinguish between the default value and unset value by using the optional keyword, although this is generally discouraged.
    1. User-defined default value for fields is no longer available.
    1. Enum definitions must start with enum value zero.
    1. Required fields are no longer available.
    1. Extensions are no longer available. Use google.protobuf.Any instead.
    1. Special exception is granted for google/protobuf/descriptor.proto for backward and runtime compatibility reasons.
    1. Group syntax is removed.

    The reason for removing these features is to make API designs simpler, more stable, and more performant. For example, it is often necessary to filter some fields before logging a message, such as removing sensitive information. This would not be possible if the fields are required.

    See Protocol Buffers for more information.

1. #### Versioning
    Sometimes it is necessary to make backwards-incompatible (or "breaking") changes to an API. These kinds of changes can cause issues or breakage for code that has dependencies on the original functionality.

    APIs SHOULD use a versioning scheme to prevent breaking changes. Additionally, APIs make some functionality only available under certain stability levels, such as alpha and beta components.

    1. #### Guidance
        All API interfaces must provide a major version number, which is encoded at the end of the protobuf package, and included as the first part of the URI path for REST APIs. If an API introduces a breaking change, such as removing or renaming a field, it must increment its API version number to ensure that existing user code does not suddenly break.

        Note: The use of the term "major version number" above is taken from semantic versioning. However, unlike in traditional semantic versioning, APIs must not expose minor or patch version numbers. For example, APIs use v1, not v1.0, v1.1, or v1.4.2. From a user's perspective, major versions are updated in place, and users receive new functionality without migration.
        A new major version of an API must not depend on a previous major version of the same API. An API may depend on other APIs, with an expectation that the caller understands the dependency and stability risk associated with those APIs. In this scenario, a stable API version must only depend on stable versions of other APIs.

        Different versions of the same API must be able to work at the same time within a single client application for a reasonable transition period. This time period allows the client to transition smoothly to the newer version. An older version must go through a reasonable, well-communicated deprecation period before being shut down.

        For releases which have alpha or beta stability, APIs must append the stability level after the major version number in the protobuf package and URI path using one of these strategies:

        1. Channel-based versioning (recommended)
        1. Release-based versioning
        1. Visibility-based versioning
    
    1. #### Channel-based versioning
        A stability channel is a long-lived release at a given stability level that receives in-place updates. There is no more than one channel per stability level for a major version. Under this strategy, there are up to three channels available: alpha, beta, and stable.

        The alpha and beta channel must have their stability level appended to the version, but the stable channel must not have the stability level appended. For example, v1 is an acceptable version for the stable channel, but v1beta or v1alpha are not. Similarly, v1beta or v1alpha are acceptable versions for the respective beta and alpha channel, but v1 is not acceptable for either. Each of these channels receives new features and updates "in-place".

        The beta channel's functionality must be a superset of the stable channel's functionality, and the alpha channel's functionality must be a superset of the beta channel's functionality.

    1. #### Deprecating API functionality
        API elements (fields, messages, RPCs) may be marked deprecated in any channel to indicate that they should no longer be used:

        ```
        // Represents a scroll. Books are preferred over scrolls.
        message Scroll {
        option deprecated = true;

        // ...
        }
        ```

        Deprecated API functionality must not graduate from alpha to beta, nor beta to stable. In other words, functionality must not arrive "pre-deprecated" in any channel.

        The beta channel's functionality may be removed after it has been deprecated for a sufficient period; we recommend 180 days. For functionality that exists only in the alpha channel, deprecation is optional, and functionality may be removed without notice. If functionality is deprecated in an API's alpha channel before removal, the API should apply the same annotation, and may use any timeframe it wishes.

    1. #### Release-based versioning
        An individual release is an alpha or beta release that is expected to be available for a limited time period before its functionality is incorporated into the stable channel, after which the individual release will be shut down. When using release-based versioning strategy, an API may have any number of individual releases at each stability level.

        Note: Both the channel-based and release-based strategies update the stable version in-place. There is a single stable channel, rather than individual stable releases, even when using the release-based strategy.

        Alpha and beta releases must have their stability level appended to the version, followed by an incrementing release number. For example, v1beta1 or v1alpha5. APIs should document the chronological order of these versions in their documentation (such as comments).

        Each alpha or beta release may be updated in place with backwards-compatible changes. For beta releases, backwards-incompatible updates should be made by incrementing the release number and publishing a new release with the change. For example, if the current version is v1beta1, then v1beta2 is released next.

        Alpha and beta releases should be shut down after their functionality reaches the stable channel. An alpha release may be shut down at any time, while a beta release should allow users a reasonable transition period; we recommend 180 days.

    1. #### Visibility-based versioning
        API visibility is an advanced feature provided by API infrastructure. It allows API producers to expose multiple external API views from one internal API surface, and each view is associated with an API visibility label, such as:

        ```
        import "google/api/visibility.proto";

        message Resource {
        string name = 1;

        // Preview. Do not use this feature for production.
        string display_name = 2
            [(google.api.field_visibility).restriction = "PREVIEW"];
        }
        ```
        A visibility label is a case-sensitive string that can be used to tag any API element. By convention, visibility labels should always use UPPER case. An implicit PUBLIC label is applied to all API elements unless an explicit visibility label is applied as in the example above.

        Each visibility label is an allow-list. API producers need to grant visibility labels to API consumers for them to use API features associated with the labels. In other words, an API visibility label is like an ACL'ed API version.

        Multiple visibility labels may be applied to an element by using a comma-separated string (e.g. "PREVIEW,TRUSTED_TESTER"). When multiple visibility labels are used, then the client needs only one of the visibility labels (logical OR).

        A single API request can only use one visibility label. By default, the visibility label granted to the API consumer is used. A client can send requests with an explicit visibility label as follows:

        ```
        GET /v1/projects/my-project/topics HTTP/1.1
        Host: pubsub.googleapis.com
        Authorization: Bearer y29....
        X-Goog-Visibilities: PREVIEW
        ```
        API producers can use API visibility for API versioning, such as INTERNAL and PREVIEW. A new API feature starts with the INTERNAL label, then moves to the PREVIEW label. When the feature is stable and becomes generally available, all API visibility labels are removed from the API definition.

        In general, API visibility is easier to implement than API versioning for incremental changes, but it depends on sophisticated API infrastructure support. Google Cloud APIs often use API visibility for Preview features.

### SQL Design Guidelines

1. Normalize Your Data
1. Define Constraints to Maintain Data Integrity
1. Document Everything
1. Diagram your data consider data modelling
1. Plan for resource limitations
1. Use standardization 

####  Do

* Use consistent and descriptive identifiers and names.
* Make judicious use of white space and indentation to make code easier to read.
* Store [ISO 8601][iso-8601] compliant time and date information
  (`YYYY-MM-DD HH:MM:SS.SSSSS`).
* Try to only use standard SQL functions instead of vendor-specific functions for
  reasons of portability.
* Keep code succinct and devoid of redundant SQL—such as unnecessary quoting or
  parentheses or `WHERE` clauses that can otherwise be derived.
* Include comments in SQL code where necessary. Use the C style opening `/*` and
  closing `*/` where possible otherwise precede comments with `--` and finish
  them with a new line.

```sql
SELECT file_hash  -- stored ssdeep hash
  FROM file_system
 WHERE file_name = '.vimrc';
```
```sql
/* Updating the file record after writing to the file */
UPDATE file_system
   SET file_modified_date = '1980-02-22 13:19:01.00000',
       file_size = 209732
 WHERE file_name = '.vimrc';
```

####  Avoid

* CamelCase—it is difficult to scan quickly.
* Descriptive prefixes or Hungarian notation such as `sp_` or `tbl`.
* Plurals—use the more natural collective term where possible instead. For example
  `staff` instead of `employees` or `people` instead of `individuals`.
* Quoted identifiers—if you must use them then stick to SQL-92 double quotes for
  portability (you may need to configure your SQL server to support this depending
  on vendor).
* Object-oriented design principles should not be applied to SQL or database
  structures.

####  Naming conventions

####  General

* Ensure the name is unique and does not exist as a
  [reserved keyword][reserved-keywords].
* Keep the length to a maximum of 30 bytes—in practice this is 30 characters
  unless you are using a multi-byte character set.
* Names must begin with a letter and may not end with an underscore.
* Only use letters, numbers and underscores in names.
* Avoid the use of multiple consecutive underscores—these can be hard to read.
* Use underscores where you would naturally include a space in the name (first
  name becomes `first_name`).
* Avoid abbreviations and if you have to use them make sure they are commonly
  understood.

```sql
SELECT first_name
  FROM staff;
```

####  Tables

* Use a collective name or, less ideally, a plural form. For example (in order of
  preference) `staff` and `employees`.
* Do not prefix with `tbl` or any other such descriptive prefix or Hungarian
  notation.
* Never give a table the same name as one of its columns and vice versa.
* Avoid, where possible, concatenating two table names together to create the name
  of a relationship table. Rather than `cars_mechanics` prefer `services`.

####  Columns

* Always use the singular name.
* Where possible avoid simply using `id` as the primary identifier for the table.
* Do not add a column with the same name as its table and vice versa.
* Always use lowercase except where it may make sense not to such as proper nouns.

####  Aliasing or correlations

* Should relate in some way to the object or expression they are aliasing.
* As a rule of thumb the correlation name should be the first letter of each word
  in the object's name.
* If there is already a correlation with the same name then append a number.
* Always include the `AS` keyword—makes it easier to read as it is explicit.
* For computed data (`SUM()` or `AVG()`) use the name you would give it were it
  a column defined in the schema.

```sql
SELECT first_name AS fn
  FROM staff AS s1
  JOIN students AS s2
    ON s2.mentor_id = s1.staff_num;
```
```sql
SELECT SUM(s.monitor_tally) AS monitor_total
  FROM staff AS s;
```

####  Stored procedures

* The name must contain a verb.
* Do not prefix with `sp_` or any other such descriptive prefix or Hungarian
  notation.

####  Uniform suffixes

The following suffixes have a universal meaning ensuring the columns can be read
and understood easily from SQL code. Use the correct suffix where appropriate.

* `_id`—a unique identifier such as a column that is a primary key.
* `_status`—flag value or some other status of any type such as
  `publication_status`.
* `_total`—the total or sum of a collection of values.
* `_num`—denotes the field contains any kind of number.
* `_name`—signifies a name such as `first_name`.
* `_seq`—contains a contiguous sequence of values.
* `_date`—denotes a column that contains the date of something.
* `_tally`—a count.
* `_size`—the size of something such as a file size or clothing.
* `_addr`—an address for the record could be physical or intangible such as
  `ip_addr`.

#### Query syntax

####  Reserved words

Always use uppercase for the [reserved keywords][reserved-keywords]
like `SELECT` and `WHERE`.

It is best to avoid the abbreviated keywords and use the full length ones where
available (prefer `ABSOLUTE` to `ABS`).

Do not use database server specific keywords where an ANSI SQL keyword already
exists performing the same function. This helps to make the code more portable.

```sql
SELECT model_num
  FROM phones AS p
 WHERE p.release_date > '2014-09-30';
```

####  White space

To make the code easier to read it is important that the correct complement of
spacing is used. Do not crowd code or remove natural language spaces.

#### Spaces

Spaces should be used to line up the code so that the root keywords all end on
the same character boundary. This forms a river down the middle making it easy for
the readers eye to scan over the code and separate the keywords from the
implementation detail. Rivers are [bad in typography][rivers], but helpful here.

```sql
(SELECT f.species_name,
        AVG(f.height) AS average_height, AVG(f.diameter) AS average_diameter
   FROM flora AS f
  WHERE f.species_name = 'Banksia'
     OR f.species_name = 'Sheoak'
     OR f.species_name = 'Wattle'
  GROUP BY f.species_name, f.observation_date)

  UNION ALL

(SELECT b.species_name,
        AVG(b.height) AS average_height, AVG(b.diameter) AS average_diameter
   FROM botanic_garden_flora AS b
  WHERE b.species_name = 'Banksia'
     OR b.species_name = 'Sheoak'
     OR b.species_name = 'Wattle'
  GROUP BY b.species_name, b.observation_date);
```

Notice that `SELECT`, `FROM`, etc. are all right aligned while the actual column
names and implementation-specific details are left aligned.

Although not exhaustive always include spaces:

* before and after equals (`=`)
* after commas (`,`)
* surrounding apostrophes (`'`) where not within parentheses or with a trailing
  comma or semicolon.

```sql
SELECT a.title, a.release_date, a.recording_date
  FROM albums AS a
 WHERE a.title = 'Charcoal Lane'
    OR a.title = 'The New Danger';
```

#### Line spacing

Always include newlines/vertical space:

* before `AND` or `OR`
* after semicolons to separate queries for easier reading
* after each keyword definition
* after a comma when separating multiple columns into logical groups
* to separate code into related sections, which helps to ease the readability of
  large chunks of code.

Keeping all the keywords aligned to the righthand side and the values left aligned
creates a uniform gap down the middle of the query. It also makes it much easier to
to quickly scan over the query definition.

```sql
INSERT INTO albums (title, release_date, recording_date)
VALUES ('Charcoal Lane', '1990-01-01 01:01:01.00000', '1990-01-01 01:01:01.00000'),
       ('The New Danger', '2008-01-01 01:01:01.00000', '1990-01-01 01:01:01.00000');
```

```sql
UPDATE albums
   SET release_date = '1990-01-01 01:01:01.00000'
 WHERE title = 'The New Danger';
```

```sql
SELECT a.title,
       a.release_date, a.recording_date, a.production_date -- grouped dates together
  FROM albums AS a
 WHERE a.title = 'Charcoal Lane'
    OR a.title = 'The New Danger';
```

####  Indentation

To ensure that SQL is readable it is important that standards of indentation
are followed.

#### Joins

Joins should be indented to the other side of the river and grouped with a new
line where necessary.

```sql
SELECT r.last_name
  FROM riders AS r
       INNER JOIN bikes AS b
       ON r.bike_vin_num = b.vin_num
          AND b.engine_tally > 2

       INNER JOIN crew AS c
       ON r.crew_chief_last_name = c.last_name
          AND c.chief = 'Y';
```

#### Subqueries

Subqueries should also be aligned to the right side of the river and then laid
out using the same style as any other query. Sometimes it will make sense to have
the closing parenthesis on a new line at the same character position as its
opening partner—this is especially true where you have nested subqueries.

```sql
SELECT r.last_name,
       (SELECT MAX(YEAR(championship_date))
          FROM champions AS c
         WHERE c.last_name = r.last_name
           AND c.confirmed = 'Y') AS last_championship_year
  FROM riders AS r
 WHERE r.last_name IN
       (SELECT c.last_name
          FROM champions AS c
         WHERE YEAR(championship_date) > '2008'
           AND c.confirmed = 'Y');
```

####  Preferred formalisms

* Make use of `BETWEEN` where possible instead of combining multiple statements
  with `AND`.
* Similarly use `IN()` instead of multiple `OR` clauses.
* Where a value needs to be interpreted before leaving the database use the `CASE`
  expression. `CASE` statements can be nested to form more complex logical structures.
* Avoid the use of `UNION` clauses and temporary tables where possible. If the
  schema can be optimised to remove the reliance on these features then it most
  likely should be.

```sql
SELECT CASE postcode
       WHEN 'BN1' THEN 'Brighton'
       WHEN 'EH1' THEN 'Edinburgh'
       END AS city
  FROM office_locations
 WHERE country = 'United Kingdom'
   AND opening_time BETWEEN 8 AND 9
   AND postcode IN ('EH1', 'BN1', 'NN1', 'KW1');
```

#### Create syntax

When declaring schema information it is also important to maintain human-readable
code. To facilitate this ensure that the column definitions are ordered and
grouped together where it makes sense to do so.

Indent column definitions by four (4) spaces within the `CREATE` definition.

####  Choosing data types

* Where possible do not use vendor-specific data types—these are not portable and
  may not be available in older versions of the same vendor's software.
* Only use `REAL` or `FLOAT` types where it is strictly necessary for floating
  point mathematics otherwise prefer `NUMERIC` and `DECIMAL` at all times. Floating
  point rounding errors are a nuisance!

####  Specifying default values

* The default value must be the same type as the column—if a column is declared
  a `DECIMAL` do not provide an `INTEGER` default value.
* Default values must follow the data type declaration and come before any
  `NOT NULL` statement.

####  Constraints and keys

Constraints and their subset, keys, are a very important component of any
database definition. They can quickly become very difficult to read and reason
about though so it is important that a standard set of guidelines are followed.

#### Choosing keys

Deciding the column(s) that will form the keys in the definition should be a
carefully considered activity as it will effect performance and data integrity.

1. The key should be unique to some degree.
2. Consistency in terms of data type for the value across the schema and a lower
   likelihood of this changing in the future.
3. Can the value be validated against a standard format (such as one published by
   ISO)? Encouraging conformity to point 2.
4. Keeping the key as simple as possible whilst not being scared to use compound
   keys where necessary.

It is a reasoned and considered balancing act to be performed at the definition
of a database. Should requirements evolve in the future it is possible to make
changes to the definitions to keep them up to date.

#### Defining constraints

Once the keys are decided it is possible to define them in the system using
constraints along with field value validation.

#### General

* Tables must have at least one key to be complete and useful.
* Constraints should be given a custom name excepting `UNIQUE`, `PRIMARY KEY`
  and `FOREIGN KEY` where the database vendor will generally supply sufficiently
  intelligible names automatically.

#### Layout and order

* Specify the primary key first right after the `CREATE TABLE` statement.
* Constraints should be defined directly beneath the column they correspond to.
  Indent the constraint so that it aligns to the right of the column name.
* If it is a multi-column constraint then consider putting it as close to both
  column definitions as possible and where this is difficult as a last resort
  include them at the end of the `CREATE TABLE` definition.
* If it is a table-level constraint that applies to the entire table then it
  should also appear at the end.
* Use alphabetical order where `ON DELETE` comes before `ON UPDATE`.
* If it make senses to do so align each aspect of the query on the same character
  position. For example all `NOT NULL` definitions could start at the same
  character position. This is not hard and fast, but it certainly makes the code
  much easier to scan and read.

#### Validation

* Use `LIKE` and `SIMILAR TO` constraints to ensure the integrity of strings
  where the format is known.
* Where the ultimate range of a numerical value is known it must be written as a
  range `CHECK()` to prevent incorrect values entering the database or the silent
  truncation of data too large to fit the column definition. In the least it
  should check that the value is greater than zero in most cases.
* `CHECK()` constraints should be kept in separate clauses to ease debugging.

#### Example

```sql
CREATE TABLE staff (
    PRIMARY KEY (staff_num),
    staff_num      INT(5)       NOT NULL,
    first_name     VARCHAR(100) NOT NULL,
    pens_in_drawer INT(2)       NOT NULL,
                   CONSTRAINT pens_in_drawer_range
                   CHECK(pens_in_drawer BETWEEN 1 AND 99)
);
```

#### Designs to avoid

* Object-oriented design principles do not effectively translate to relational
  database designs—avoid this pitfall.
* Placing the value in one column and the units in another column. The column
  should make the units self-evident to prevent the requirement to combine
  columns again later in the application. Use `CHECK()` to ensure valid data is
  inserted into the column.
* [Entity–Attribute–Value][eav] (EAV) tables—use a specialist product intended for
  handling such schema-less data instead.
* Splitting up data that should be in one table across many tables because of
  arbitrary concerns such as time-based archiving or location in a multinational
  organisation. Later queries must then work across multiple tables with `UNION`
  rather than just simply querying one table.

#### Reserved keyword reference

A list of ANSI SQL (92, 99 and 2003), MySQL 3 to 5.x, PostgreSQL 8.1, MS SQL Server 2000, MS ODBC and Oracle 10.2 reserved keywords.

```sql
A
ABORT
ABS
ABSOLUTE
ACCESS
ACTION
ADA
ADD
ADMIN
AFTER
AGGREGATE
ALIAS
ALL
ALLOCATE
ALSO
ALTER
ALWAYS
ANALYSE
ANALYZE
AND
ANY
ARE
ARRAY
AS
ASC
ASENSITIVE
ASSERTION
ASSIGNMENT
ASYMMETRIC
AT
ATOMIC
ATTRIBUTE
ATTRIBUTES
AUDIT
AUTHORIZATION
AUTO_INCREMENT
AVG
AVG_ROW_LENGTH
BACKUP
BACKWARD
BEFORE
BEGIN
BERNOULLI
BETWEEN
BIGINT
BINARY
BIT
BIT_LENGTH
BITVAR
BLOB
BOOL
BOOLEAN
BOTH
BREADTH
BREAK
BROWSE
BULK
BY
C
CACHE
CALL
CALLED
CARDINALITY
CASCADE
CASCADED
CASE
CAST
CATALOG
CATALOG_NAME
CEIL
CEILING
CHAIN
CHANGE
CHAR
CHAR_LENGTH
CHARACTER
CHARACTER_LENGTH
CHARACTER_SET_CATALOG
CHARACTER_SET_NAME
CHARACTER_SET_SCHEMA
CHARACTERISTICS
CHARACTERS
CHECK
CHECKED
CHECKPOINT
CHECKSUM
CLASS
CLASS_ORIGIN
CLOB
CLOSE
CLUSTER
CLUSTERED
COALESCE
COBOL
COLLATE
COLLATION
COLLATION_CATALOG
COLLATION_NAME
COLLATION_SCHEMA
COLLECT
COLUMN
COLUMN_NAME
COLUMNS
COMMAND_FUNCTION
COMMAND_FUNCTION_CODE
COMMENT
COMMIT
COMMITTED
COMPLETION
COMPRESS
COMPUTE
CONDITION
CONDITION_NUMBER
CONNECT
CONNECTION
CONNECTION_NAME
CONSTRAINT
CONSTRAINT_CATALOG
CONSTRAINT_NAME
CONSTRAINT_SCHEMA
CONSTRAINTS
CONSTRUCTOR
CONTAINS
CONTAINSTABLE
CONTINUE
CONVERSION
CONVERT
COPY
CORR
CORRESPONDING
COUNT
COVAR_POP
COVAR_SAMP
CREATE
CREATEDB
CREATEROLE
CREATEUSER
CROSS
CSV
CUBE
CUME_DIST
CURRENT
CURRENT_DATE
CURRENT_DEFAULT_TRANSFORM_GROUP
CURRENT_PATH
CURRENT_ROLE
CURRENT_TIME
CURRENT_TIMESTAMP
CURRENT_TRANSFORM_GROUP_FOR_TYPE
CURRENT_USER
CURSOR
CURSOR_NAME
CYCLE
DATA
DATABASE
DATABASES
DATE
DATETIME
DATETIME_INTERVAL_CODE
DATETIME_INTERVAL_PRECISION
DAY
DAY_HOUR
DAY_MICROSECOND
DAY_MINUTE
DAY_SECOND
DAYOFMONTH
DAYOFWEEK
DAYOFYEAR
DBCC
DEALLOCATE
DEC
DECIMAL
DECLARE
DEFAULT
DEFAULTS
DEFERRABLE
DEFERRED
DEFINED
DEFINER
DEGREE
DELAY_KEY_WRITE
DELAYED
DELETE
DELIMITER
DELIMITERS
DENSE_RANK
DENY
DEPTH
DEREF
DERIVED
DESC
DESCRIBE
DESCRIPTOR
DESTROY
DESTRUCTOR
DETERMINISTIC
DIAGNOSTICS
DICTIONARY
DISABLE
DISCONNECT
DISK
DISPATCH
DISTINCT
DISTINCTROW
DISTRIBUTED
DIV
DO
DOMAIN
DOUBLE
DROP
DUAL
DUMMY
DUMP
DYNAMIC
DYNAMIC_FUNCTION
DYNAMIC_FUNCTION_CODE
EACH
ELEMENT
ELSE
ELSEIF
ENABLE
ENCLOSED
ENCODING
ENCRYPTED
END
END-EXEC
ENUM
EQUALS
ERRLVL
ESCAPE
ESCAPED
EVERY
EXCEPT
EXCEPTION
EXCLUDE
EXCLUDING
EXCLUSIVE
EXEC
EXECUTE
EXISTING
EXISTS
EXIT
EXP
EXPLAIN
EXTERNAL
EXTRACT
FALSE
FETCH
FIELDS
FILE
FILLFACTOR
FILTER
FINAL
FIRST
FLOAT
FLOAT4
FLOAT8
FLOOR
FLUSH
FOLLOWING
FOR
FORCE
FOREIGN
FORTRAN
FORWARD
FOUND
FREE
FREETEXT
FREETEXTTABLE
FREEZE
FROM
FULL
FULLTEXT
FUNCTION
FUSION
G
GENERAL
GENERATED
GET
GLOBAL
GO
GOTO
GRANT
GRANTED
GRANTS
GREATEST
GROUP
GROUPING
HANDLER
HAVING
HEADER
HEAP
HIERARCHY
HIGH_PRIORITY
HOLD
HOLDLOCK
HOST
HOSTS
HOUR
HOUR_MICROSECOND
HOUR_MINUTE
HOUR_SECOND
IDENTIFIED
IDENTITY
IDENTITY_INSERT
IDENTITYCOL
IF
IGNORE
ILIKE
IMMEDIATE
IMMUTABLE
IMPLEMENTATION
IMPLICIT
IN
INCLUDE
INCLUDING
INCREMENT
INDEX
INDICATOR
INFILE
INFIX
INHERIT
INHERITS
INITIAL
INITIALIZE
INITIALLY
INNER
INOUT
INPUT
INSENSITIVE
INSERT
INSERT_ID
INSTANCE
INSTANTIABLE
INSTEAD
INT
INT1
INT2
INT3
INT4
INT8
INTEGER
INTERSECT
INTERSECTION
INTERVAL
INTO
INVOKER
IS
ISAM
ISNULL
ISOLATION
ITERATE
JOIN
K
KEY
KEY_MEMBER
KEY_TYPE
KEYS
KILL
LANCOMPILER
LANGUAGE
LARGE
LAST
LAST_INSERT_ID
LATERAL
LEADING
LEAST
LEAVE
LEFT
LENGTH
LESS
LEVEL
LIKE
LIMIT
LINENO
LINES
LISTEN
LN
LOAD
LOCAL
LOCALTIME
LOCALTIMESTAMP
LOCATION
LOCATOR
LOCK
LOGIN
LOGS
LONG
LONGBLOB
LONGTEXT
LOOP
LOW_PRIORITY
LOWER
M
MAP
MATCH
MATCHED
MAX
MAX_ROWS
MAXEXTENTS
MAXVALUE
MEDIUMBLOB
MEDIUMINT
MEDIUMTEXT
MEMBER
MERGE
MESSAGE_LENGTH
MESSAGE_OCTET_LENGTH
MESSAGE_TEXT
METHOD
MIDDLEINT
MIN
MIN_ROWS
MINUS
MINUTE
MINUTE_MICROSECOND
MINUTE_SECOND
MINVALUE
MLSLABEL
MOD
MODE
MODIFIES
MODIFY
MODULE
MONTH
MONTHNAME
MORE
MOVE
MULTISET
MUMPS
MYISAM
NAME
NAMES
NATIONAL
NATURAL
NCHAR
NCLOB
NESTING
NEW
NEXT
NO
NO_WRITE_TO_BINLOG
NOAUDIT
NOCHECK
NOCOMPRESS
NOCREATEDB
NOCREATEROLE
NOCREATEUSER
NOINHERIT
NOLOGIN
NONCLUSTERED
NONE
NORMALIZE
NORMALIZED
NOSUPERUSER
NOT
NOTHING
NOTIFY
NOTNULL
NOWAIT
NULL
NULLABLE
NULLIF
NULLS
NUMBER
NUMERIC
OBJECT
OCTET_LENGTH
OCTETS
OF
OFF
OFFLINE
OFFSET
OFFSETS
OIDS
OLD
ON
ONLINE
ONLY
OPEN
OPENDATASOURCE
OPENQUERY
OPENROWSET
OPENXML
OPERATION
OPERATOR
OPTIMIZE
OPTION
OPTIONALLY
OPTIONS
OR
ORDER
ORDERING
ORDINALITY
OTHERS
OUT
OUTER
OUTFILE
OUTPUT
OVER
OVERLAPS
OVERLAY
OVERRIDING
OWNER
PACK_KEYS
PAD
PARAMETER
PARAMETER_MODE
PARAMETER_NAME
PARAMETER_ORDINAL_POSITION
PARAMETER_SPECIFIC_CATALOG
PARAMETER_SPECIFIC_NAME
PARAMETER_SPECIFIC_SCHEMA
PARAMETERS
PARTIAL
PARTITION
PASCAL
PASSWORD
PATH
PCTFREE
PERCENT
PERCENT_RANK
PERCENTILE_CONT
PERCENTILE_DISC
PLACING
PLAN
PLI
POSITION
POSTFIX
POWER
PRECEDING
PRECISION
PREFIX
PREORDER
PREPARE
PREPARED
PRESERVE
PRIMARY
PRINT
PRIOR
PRIVILEGES
PROC
PROCEDURAL
PROCEDURE
PROCESS
PROCESSLIST
PUBLIC
PURGE
QUOTE
RAID0
RAISERROR
RANGE
RANK
RAW
READ
READS
READTEXT
REAL
RECHECK
RECONFIGURE
RECURSIVE
REF
REFERENCES
REFERENCING
REGEXP
REGR_AVGX
REGR_AVGY
REGR_COUNT
REGR_INTERCEPT
REGR_R2
REGR_SLOPE
REGR_SXX
REGR_SXY
REGR_SYY
REINDEX
RELATIVE
RELEASE
RELOAD
RENAME
REPEAT
REPEATABLE
REPLACE
REPLICATION
REQUIRE
RESET
RESIGNAL
RESOURCE
RESTART
RESTORE
RESTRICT
RESULT
RETURN
RETURNED_CARDINALITY
RETURNED_LENGTH
RETURNED_OCTET_LENGTH
RETURNED_SQLSTATE
RETURNS
REVOKE
RIGHT
RLIKE
ROLE
ROLLBACK
ROLLUP
ROUTINE
ROUTINE_CATALOG
ROUTINE_NAME
ROUTINE_SCHEMA
ROW
ROW_COUNT
ROW_NUMBER
ROWCOUNT
ROWGUIDCOL
ROWID
ROWNUM
ROWS
RULE
SAVE
SAVEPOINT
SCALE
SCHEMA
SCHEMA_NAME
SCHEMAS
SCOPE
SCOPE_CATALOG
SCOPE_NAME
SCOPE_SCHEMA
SCROLL
SEARCH
SECOND
SECOND_MICROSECOND
SECTION
SECURITY
SELECT
SELF
SENSITIVE
SEPARATOR
SEQUENCE
SERIALIZABLE
SERVER_NAME
SESSION
SESSION_USER
SET
SETOF
SETS
SETUSER
SHARE
SHOW
SHUTDOWN
SIGNAL
SIMILAR
SIMPLE
SIZE
SMALLINT
SOME
SONAME
SOURCE
SPACE
SPATIAL
SPECIFIC
SPECIFIC_NAME
SPECIFICTYPE
SQL
SQL_BIG_RESULT
SQL_BIG_SELECTS
SQL_BIG_TABLES
SQL_CALC_FOUND_ROWS
SQL_LOG_OFF
SQL_LOG_UPDATE
SQL_LOW_PRIORITY_UPDATES
SQL_SELECT_LIMIT
SQL_SMALL_RESULT
SQL_WARNINGS
SQLCA
SQLCODE
SQLERROR
SQLEXCEPTION
SQLSTATE
SQLWARNING
SQRT
SSL
STABLE
START
STARTING
STATE
STATEMENT
STATIC
STATISTICS
STATUS
STDDEV_POP
STDDEV_SAMP
STDIN
STDOUT
STORAGE
STRAIGHT_JOIN
STRICT
STRING
STRUCTURE
STYLE
SUBCLASS_ORIGIN
SUBLIST
SUBMULTISET
SUBSTRING
SUCCESSFUL
SUM
SUPERUSER
SYMMETRIC
SYNONYM
SYSDATE
SYSID
SYSTEM
SYSTEM_USER
TABLE
TABLE_NAME
TABLES
TABLESAMPLE
TABLESPACE
TEMP
TEMPLATE
TEMPORARY
TERMINATE
TERMINATED
TEXT
TEXTSIZE
THAN
THEN
TIES
TIME
TIMESTAMP
TIMEZONE_HOUR
TIMEZONE_MINUTE
TINYBLOB
TINYINT
TINYTEXT
TO
TOAST
TOP
TOP_LEVEL_COUNT
TRAILING
TRAN
TRANSACTION
TRANSACTION_ACTIVE
TRANSACTIONS_COMMITTED
TRANSACTIONS_ROLLED_BACK
TRANSFORM
TRANSFORMS
TRANSLATE
TRANSLATION
TREAT
TRIGGER
TRIGGER_CATALOG
TRIGGER_NAME
TRIGGER_SCHEMA
TRIM
TRUE
TRUNCATE
TRUSTED
TSEQUAL
TYPE
UESCAPE
UID
UNBOUNDED
UNCOMMITTED
UNDER
UNDO
UNENCRYPTED
UNION
UNIQUE
UNKNOWN
UNLISTEN
UNLOCK
UNNAMED
UNNEST
UNSIGNED
UNTIL
UPDATE
UPDATETEXT
UPPER
USAGE
USE
USER
USER_DEFINED_TYPE_CATALOG
USER_DEFINED_TYPE_CODE
USER_DEFINED_TYPE_NAME
USER_DEFINED_TYPE_SCHEMA
USING
UTC_DATE
UTC_TIME
UTC_TIMESTAMP
VACUUM
VALID
VALIDATE
VALIDATOR
VALUE
VALUES
VAR_POP
VAR_SAMP
VARBINARY
VARCHAR
VARCHAR2
VARCHARACTER
VARIABLE
VARIABLES
VARYING
VERBOSE
VIEW
VOLATILE
WAITFOR
WHEN
WHENEVER
WHERE
WHILE
WIDTH_BUCKET
WINDOW
WITH
WITHIN
WITHOUT
WORK
WRITE
WRITETEXT
X509
XOR
YEAR
YEAR_MONTH
ZEROFILL
ZONE
```

#### Column data types

These are some suggested column data types to use for maximum compatibility between database engines.

#### Character types:

* CHAR
* CLOB
* VARCHAR

#### Numeric types

* Exact numeric types
    * BIGINT
    * DECIMAL
    * DECFLOAT
    * INTEGER
    * NUMERIC
    * SMALLINT
* Approximate numeric types
    * DOUBLE PRECISION
    * FLOAT
    * REAL

#### Datetime types

* DATE
* TIME
* TIMESTAMP

#### Binary types:

* BINARY
* BLOB
* VARBINARY

#### Additional types

* BOOLEAN
* INTERVAL
* XML