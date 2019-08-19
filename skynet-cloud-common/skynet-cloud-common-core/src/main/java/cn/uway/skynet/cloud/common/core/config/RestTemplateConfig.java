package cn.uway.skynet.cloud.common.core.config;


import cn.uway.skynet.cloud.common.core.util.ExceptionUtil;
import com.alibaba.cloud.sentinel.annotation.SentinelRestTemplate;
import org.springframework.cloud.client.loadbalancer.LoadBalanced;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.web.client.RestTemplate;

@Configuration
public class RestTemplateConfig {
    @Bean
    @LoadBalanced
    @SentinelRestTemplate(fallback = "fallback", fallbackClass = ExceptionUtil.class, blockHandler="handleException", blockHandlerClass=ExceptionUtil.class)
    public RestTemplate restTemplate() {
        return new RestTemplate();
    }
}