# postgres/Dockerfile
FROM postgres:15.1
COPY /dump/ /docker-entrypoint-initdb.d/
COPY /conf/ /etc/conf/