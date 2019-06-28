package cn.uway.skynet.cloud.common.data.datasources.annotation;

import java.lang.annotation.*;

/**
 * 多数据源注解
 * @author magic.s.g.xie
 * @email xiesg@uway.cn
 * @date 2019/1/16 22:16
 */
@Target(ElementType.METHOD)
@Retention(RetentionPolicy.RUNTIME)
@Documented
public @interface DataSource {
    String name() default "";
}
