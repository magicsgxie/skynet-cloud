package cn.uway.skynet.cloud.auth;

import cn.uway.skynet.cloud.common.security.annotation.EnableSkynetCloudFeignClients;
import com.alibaba.nacos.api.config.listener.Listener;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.boot.ApplicationArguments;
import org.springframework.boot.ApplicationRunner;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.boot.autoconfigure.jdbc.DataSourceAutoConfiguration;
import org.springframework.cloud.alibaba.nacos.NacosConfigProperties;
import org.springframework.cloud.client.SpringCloudApplication;
import org.springframework.cloud.client.discovery.EnableDiscoveryClient;
import org.springframework.context.ConfigurableApplicationContext;
import org.springframework.context.annotation.ComponentScan;
import org.springframework.security.oauth2.provider.endpoint.TokenEndpoint;
import org.springframework.stereotype.Component;

import java.io.IOException;
import java.io.StringReader;
import java.util.Properties;
import java.util.concurrent.Executor;
import java.util.concurrent.TimeUnit;

@SpringCloudApplication
@ComponentScan(basePackages = {"cn.uway.skynet.cloud"})
@EnableSkynetCloudFeignClients
public class AuthBootstrap {
    static Logger logger = LoggerFactory.getLogger(AuthBootstrap.class);
    public static void main(String[] args) {
        ConfigurableApplicationContext applicationContext = SpringApplication.run(AuthBootstrap.class, args);
        //TokenEndpoint
//        while (true) {
//            //当动态配置刷新时，会更新到 Enviroment中，因此这里每隔一秒中从Enviroment中获取配置
//            String userName = applicationContext.getEnvironment().getProperty("spring.datasource.name");
//            String userAge = applicationContext.getEnvironment().getProperty("spring.datasource.username");
//            logger.info("配置信息，名称：{}，年龄：{}", userName, userAge);
//            //TimeUnit.SECONDS.sleep(1);
//        }
    }

}

