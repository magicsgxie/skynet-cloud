package cn.uway.skynet.cloud.auth;

import cn.uway.skynet.cloud.common.security.annotation.EnableSkynetCloudFeignClients;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.boot.SpringApplication;
import org.springframework.cloud.client.SpringCloudApplication;
import org.springframework.context.ConfigurableApplicationContext;
import org.springframework.context.annotation.ComponentScan;

@SpringCloudApplication
@ComponentScan(basePackages = {"cn.uway.skynet.cloud"})
@EnableSkynetCloudFeignClients
public class AuthBootstrap {
    static Logger logger = LoggerFactory.getLogger(AuthBootstrap.class);
    public static void main(String[] args) {
        ConfigurableApplicationContext applicationContext = SpringApplication.run(AuthBootstrap.class, args);
    }

}

