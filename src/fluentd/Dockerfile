# fluentd/Dockerfile
FROM fluent/fluentd:edge
USER root
RUN ["gem", "install", "fluent-plugin-elasticsearch", "--no-document", "--version", "5.2.4"]
USER fluent

COPY /conf/ /fluentd/etc/