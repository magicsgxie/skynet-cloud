package cn.uway.skynet.cloud.common.security.annotation;

import cn.uway.skynet.cloud.common.security.component.SkynetCloudResourceServerAutoConfiguration;
import cn.uway.skynet.cloud.common.security.component.SkynetCloudSecurityBeanDefinitionRegistrar;
import org.springframework.context.annotation.Import;
import org.springframework.security.config.annotation.method.configuration.EnableGlobalMethodSecurity;
import org.springframework.security.oauth2.config.annotation.web.configuration.EnableResourceServer;

import java.lang.annotation.*;

@Documented
@Inherited
@EnableResourceServer
@Target({ElementType.TYPE})
@Retention(RetentionPolicy.RUNTIME)
@EnableGlobalMethodSecurity(prePostEnabled = true)
@Import({SkynetCloudResourceServerAutoConfiguration.class, SkynetCloudSecurityBeanDefinitionRegistrar.class})
public @interface EnableSkynetCloudResourceServer {
}
