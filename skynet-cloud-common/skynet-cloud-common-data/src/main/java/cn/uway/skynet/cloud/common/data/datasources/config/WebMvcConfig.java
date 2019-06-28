package cn.uway.skynet.cloud.common.data.datasources.config;

import cn.uway.skynet.cloud.common.data.datascope.SqlFilterArgumentResolver;
import org.springframework.context.annotation.Configuration;
import org.springframework.web.method.support.HandlerMethodArgumentResolver;
import org.springframework.web.servlet.config.annotation.WebMvcConfigurer;

import java.util.List;

/**
 * @author magic.s.g.xie
 * @date 2019-06-24
 * <p>
 * 注入自自定义SQL 过滤
 */
@Configuration
public class WebMvcConfig implements WebMvcConfigurer {
    @Override
    public void addArgumentResolvers(List<HandlerMethodArgumentResolver> argumentResolvers) {
        argumentResolvers.add(new SqlFilterArgumentResolver());
    }
}