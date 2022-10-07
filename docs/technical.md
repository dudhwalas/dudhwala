# Technical
{{app_name}}'s technical software solution design comprises of **Software Architecture**, **Solution Design**, **Infrastructure**, **DevOps**, **Info Sec**, **APLM Process** and other technical aspects of the system.

## Software Architecture 🗜️
The software architecture of {{app_name}} system describes elements that makes the sytem, their interaction, relation with other elements and properties of both. It describes the system by number of architectural views which shows different aspect of the system that addresses different concerns.

### 1. Context View
The Context view of {{app_name}} system defines the relationships, dependencies, and interactions between the system and its environment—the people, external systems and entities with which it interacts. It defines what {{app_name}} does and does not do; where the boundaries are between it and the outside world; and how it interacts with other systems, organizations, and people across these boundaries.

[filename](diagram/context_view.drawio ':include :type=code')

### 2. Subdomain Services Defination
[filename](diagram/subdomain_service_view.drawio ':include :type=code')

### 3. Architecturally Significant Requirements (ASR) - Quality Attributes
|Quality Attribute|Measurable Metric|Benchmark|
|:--|:--|:--|
|Performance|Latency of API response|Max <= 5 sec</br>Avg <=2 sec|
|Performance|Timeout of API response|Max <= 15 sec|
|Performance|API request post size|Max <= 2 MB|
|Concurrent|Concurrent users|Max = 100 users</br>Performance benchmark report|
|High Availability|SLA</br>Failure detection|Min = 99% SLA</br>Alert on failure within 30 sec|
|Portability|Docker containers|Platform agnostic. Run on any platform supporting Docker|
|Portability|Container orchestration - k8s|Platform agnostic. Run on any platform supporting k8s|
|Backup|Database backup|Weekly full backup|
|Usability|Browser Support|Google Chrome, Safari|
|Logging|API request/response</br>Exception|UI tool to access logs|
|Auditablity|Audit of operations that changes application state|UI tool to access audit logs|
|Testability|Unit Test</br>Functional Test</br>|Test reports|
|Deployability|CI-CD|DevOps pipeline|

### 4. Architecture Core Principles
|Principle|Description|RAG
|:--|:--|:--|
|Inteface Segregation|<ul><li>Design of interfaces (i.e., service contracts/API contracts).</li><li>Support multiple client.</li></ul>**Tactics:**<ol><li>Backend For Frontend (BFF)</li><li>API Gateway</li><li>Gateway Aggregation</li><li>Gateway Offloading</li><li>Gateway Routing</li><ol>|G|
|Deployability|<ul><li>Configuring the runtime infrastructure, which includes containers, pods, clusters, persistence, security, and networking.</li><li>Expediting the commit+build+test+deploy process.</li><li>Monitoring the health of the microservices to quickly identify and remedy faults.</li></ul>**Tactics:**<ol><li>Containerization & container orchestration</li><li>Service mesh</li><li>API Gatewayt</li><li>Serverless architecture</li><li>Monitoring tools</li><li>Log consolidation tools</li><li>Tracing tools</li><li>DevOps</li><li>Blue Green deployment and canary releases</li><li>IaC</li><li>Continuous Delivery</li><li>Externalized configuration</li><ol>|G|
|Loose coupling|<ul><li>Asynchronous messaging.</li><li>Event Driven</li><li>Send to and receive messages from a queue/topic.</li><li>Publisher/Subscriber.</li></ul>**Tactics:**<ol><li>Message broker.</li><li>Database per micro-service</li><li>Saga</li><li>Distributed transaction</li><li>Compensating transaction</li><ol>|G|
|Availability over consistency|<ul><li>Minimum downtime - High availability.</li><li>Fault tolerance.</li><li>Resilient.</li></ul>**Tactics:**<ol><li>Service data replication</li><li>CQRS</li><li>Event Sourcing</li><li>Retry</li><li>Circuit breaker</li><li>Network timeouts</li><ol>|G|
|Single Responsiblity|<ul><li>Right grained micro-service. Not too fine - not to coarse</li><li>Cohesion.</li></ul>**Tactics:**<ol><li>Domain Driven Design - DDD</li><li>Scope of bounded context - BC</li><li>Domain events</li><ol>|G|

### 5. Conceptual Architecture
[filename](diagram/conceptual_view.drawio ':include :type=code')
### 6. Logical Architecture
[filename](diagram/logical_view.drawio ':include :type=code')
### 7. Physical Architecture (TBD)