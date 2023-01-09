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

<u> https://mathieu.fenniak.net/the-api-checklist/

### REST API Guidelines

<u> https://github.com/microsoft/api-guidelines/blob/vNext/Guidelines.md#3-introduction

### GRPC API Guidelines

<u> https://cloud.google.com/apis/design
