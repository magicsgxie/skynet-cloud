package cn.uway.skynet.cloud.gateway;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.cloud.client.discovery.EnableDiscoveryClient;
import org.springframework.context.annotation.ComponentScan;

@SpringBootApplication
@ComponentScan(basePackages = {"cn.uway.skynet.cloud.common.core.config", "cn.uway.skynet.cloud.gateway"})
@EnableDiscoveryClient
public class GateWaybootStrap {
    public static void main(String[] args) {
        //HandlerMapping
        //Swagger2DocumentationConfiguration
        //Swagger2DocumentationConfiguration
        SpringApplication.run(GateWaybootStrap.class, args);
    }
}
