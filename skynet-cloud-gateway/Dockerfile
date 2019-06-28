FROM anapsix/alpine-java:8_server-jre_unlimited

MAINTAINER wangiegie@gmail.com

RUN ln -sf /usr/share/zoneinfo/Asia/Shanghai /etc/localtime

RUN mkdir -p /pig-gateway

WORKDIR /pig-gateway

EXPOSE 9999

ADD ./skynet-cloud-gateway/target/skynet-cloud-gateway.jar ./

CMD java -Djava.security.egd=file:/dev/./urandom -jar skynet-cloud-gateway.jar