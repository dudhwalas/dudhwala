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

