FROM anapsix/alpine-java:8_server-jre_unlimited

MAINTAINER xieshaoguangwww@126.com

RUN ln -sf /usr/share/zoneinfo/Asia/Shanghai /etc/localtime

RUN mkdir -p /pig-codegen

WORKDIR /skynet-cloud-codegen

EXPOSE 5003

ADD ./skynet-cloud-visual/skynet-cloud-codegen/target/skynet-cloud-codegen.jar ./

CMD java -Djava.security.egd=file:/dev/./urandom -jar skynet-cloud-codegen.jar
