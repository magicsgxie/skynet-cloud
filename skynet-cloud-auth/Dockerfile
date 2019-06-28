FROM anapsix/alpine-java:8_server-jre_unlimited

MAINTAINER xieshaoguangwww@126.com

RUN ln -sf /usr/share/zoneinfo/Asia/Shanghai /etc/localtime

RUN mkdir -p /skynet-cloud-auth

WORKDIR /skynet-cloud-auth

EXPOSE 3000

ADD ./skynet-cloud-auth/target/skynet-cloud-auth.jar ./

CMD java -Djava.security.egd=file:/dev/./urandom -jar skynet-cloud-auth.jar