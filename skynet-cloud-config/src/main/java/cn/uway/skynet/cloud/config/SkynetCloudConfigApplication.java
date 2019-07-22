package cn.uway.skynet.cloud.config;

import org.springframework.boot.SpringApplication;
import org.springframework.cloud.client.SpringCloudApplication;
import org.springframework.cloud.config.server.EnableConfigServer;

@EnableConfigServer
@SpringCloudApplication
public class SkynetCloudConfigApplication {
    public static void main(String[] args) {
        SpringApplication.run(SkynetCloudConfigApplication.class, args);
    }
}
