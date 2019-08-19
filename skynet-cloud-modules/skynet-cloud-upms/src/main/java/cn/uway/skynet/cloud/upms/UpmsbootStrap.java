package cn.uway.skynet.cloud.upms;

import cn.uway.skynet.cloud.common.security.annotation.EnableSkynetCloudFeignClients;
import cn.uway.skynet.cloud.common.security.annotation.EnableSkynetCloudResourceServer;
import org.mybatis.spring.annotation.MapperScan;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.cloud.client.SpringCloudApplication;
import org.springframework.cloud.client.discovery.EnableDiscoveryClient;
import org.springframework.context.annotation.ComponentScan;


@SpringBootApplication
@EnableSkynetCloudFeignClients
@EnableSkynetCloudResourceServer
@ComponentScan(basePackages = {"cn.uway.skynet.cloud"})
@EnableDiscoveryClient
public class UpmsbootStrap {
    public static void main(String[] args) {
        SpringApplication.run(UpmsbootStrap.class, args);
    }
}

