package cn.uway.skynet.cloud.eureka;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.cloud.netflix.eureka.server.EnableEurekaServer;

/**
 * @author magic.s.g.xie
 */
@EnableEurekaServer
@SpringBootApplication
public class SkynetCloudEurekaApplication {
    public static void main(String[] args) {
        SpringApplication.run(SkynetCloudEurekaApplication.class, args);
    }
}
