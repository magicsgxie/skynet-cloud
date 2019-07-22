FROM anapsix/alpine-java:8_server-jre_unlimited

MAINTAINER xieshaoguangwww@126.com

RUN ln -sf /usr/share/zoneinfo/Asia/Shanghai /etc/localtime

RUN mkdir -p /skynet-cloud-upms

WORKDIR /skynet-cloud-upms

EXPOSE 3000

ADD ./skynet-cloud-upms/target/skynet-cloud-upms.jar ./

CMD java -Djava.security.egd=file:/dev/./urandom -jar skynet-cloud-upms.jar