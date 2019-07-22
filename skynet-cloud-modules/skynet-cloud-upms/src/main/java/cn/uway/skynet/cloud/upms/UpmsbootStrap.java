package cn.uway.skynet.cloud.upms;

import cn.uway.skynet.cloud.common.security.annotation.EnableSkynetCloudFeignClients;
import cn.uway.skynet.cloud.common.security.annotation.EnableSkynetCloudResourceServer;
import org.mybatis.spring.annotation.MapperScan;
import org.springframework.boot.SpringApplication;
import org.springframework.cloud.client.SpringCloudApplication;
import org.springframework.context.annotation.ComponentScan;


@SpringCloudApplication
@MapperScan("cn.uway.skynet.cloud.upms.mapper")
@ComponentScan("cn.uway.skynet.cloud")
@EnableSkynetCloudFeignClients
@EnableSkynetCloudResourceServer
public class UpmsbootStrap {
    public static void main(String[] args) {
        //org.springframework.cloud.openfeign.FeignClientProperties xx1;
        //DynamicServerListLoadBalancer
        //FeignContext
        SpringApplication.run(UpmsbootStrap.class, args);
    }
}

