package cn.uway.skynet.cloud.common.core.config;

import cn.uway.skynet.cloud.common.core.util.DocketUtils;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import springfox.documentation.spring.web.plugins.Docket;
import springfox.documentation.swagger2.annotations.EnableSwagger2;

@Configuration
@EnableSwagger2
public class SwaggerConfig {

    @Bean
    public Docket createRestApi() {
        return DocketUtils.createRestApi();
    }



}
