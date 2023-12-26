## Base Framework ⚙️ <!-- {docsify-ignore} -->

**Base framework** are set of microservices that form cross-cutting services commonly used across the {{app_name}} business services.

1.  ### Logging Service - Unified Logging

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
|**DEBUG**|Used for debugging. The messaging targets specifically the app’s developers.|
|**INFO**|Informational messages that do not indicate any fault or error.|
|**WARN**|Indicates that there is a potential problem, but with no user experience impact.|
|**ERROR**|Indicates a serious problem, with some user experience impact.|
|**FATAL**|Indicates fatal errors, user experience is majorly impacted.|

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

2.  ### IAM Service - Auth

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
|**Owner**|-|-|-|-|-|read-write|read-write|read-write|read-write|read-write|read-write|read-write|
|**Customer**|-|-|-|-|-|-|-|-|-|-|-|-|
|**Delivery Squad**|-|-|-|-|-|-|read|read|read|read-write|read|read-write|
|**Administrator**|-|-|-|-|-|read-write|read-write|read-write|read-write|read-write|read-write|read-write|