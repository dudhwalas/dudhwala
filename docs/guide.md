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

The following is a selective list of standard guidelines for developers to understand the do’s and don’ts when designing and implementing APIs for {{app_name}}.

1.  All the services should be stateless.
1.  The services should follow gRPC best practices.

• The services should be protected by authentication, authorization, CORS, OWASP guidelines,
secured communication protocol, validations of input objects and harmless (non-sensitive)
response.
• The services should be self descriptive.
• The services documentation should have enough information for external clients to consume it
without much challenges.
• The services should be backward compatible.
• Avoid creating single monolith service endpoint returning a large response messages.
• Break down service into multiple endpoints for easier consumption and extension.
• Do not trust input messages/objects.
• Parse and Validate input: length, range, format, type, accepted characters, injection, size,
content-type.
• Trace and Log all requests.
• Do not return exception details. 
