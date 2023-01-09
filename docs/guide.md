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

    Full Resource Name
    
    A scheme-less URI consisting of a DNS-compatible API service name and a resource path. The resource path is also known as relative resource name. For example:

    "//library.googleapis.com/shelves/shelf1/books/book2"

    The API service name is for clients to locate the API service endpoint; it may be a fake DNS name for internal-only services. If the API service name is obvious from the context, relative resource names are often used.

    Relative Resource Name
    A URI path (path-noscheme) without the leading "/". It identifies a resource within the API service. For example:

    "shelves/shelf1/books/book2"
    
    Resource ID
    A resource ID typically consists of one or more non-empty URI segments (segment-nz-nc) that identify the resource within its parent resource, see above examples. The non-trailing resource ID in a resource name must have exactly one URL segment, while the trailing resource ID in a resource name may have more than one URI segment. For example:

    Collection ID	Resource ID
    files	source/py/parser.py
    
    API services should use URL-friendly resource IDs when feasible. Resource IDs must be clearly documented whether they are assigned by the client, the server, or either. For example, file names are typically assigned by clients, while email message IDs are typically assigned by servers.

    Collection ID
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