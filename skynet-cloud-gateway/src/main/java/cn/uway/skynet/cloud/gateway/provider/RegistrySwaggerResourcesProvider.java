package cn.uway.skynet.cloud.gateway.provider;

import cn.uway.skynet.cloud.common.core.constant.ServiceNameConstants;
import lombok.AllArgsConstructor;
import org.springframework.cloud.gateway.config.GatewayProperties;
import org.springframework.cloud.gateway.route.RouteLocator;
import org.springframework.cloud.gateway.support.NameUtils;
import org.springframework.context.annotation.Primary;
import org.springframework.stereotype.Component;

import springfox.documentation.swagger.web.SwaggerResource;
import springfox.documentation.swagger.web.SwaggerResourcesProvider;

import java.util.ArrayList;
import java.util.List;

@Component
@Primary
//@AllArgsConstructor
public class RegistrySwaggerResourcesProvider implements SwaggerResourcesProvider {
    public static final String API_URI = "/v2/api-docs";
    private final RouteLocator routeLocator;
    private final GatewayProperties gatewayProperties;

    public  RegistrySwaggerResourcesProvider(RouteLocator routeLocator,GatewayProperties gatewayProperties ) {
        this.routeLocator = routeLocator;
        this.gatewayProperties = gatewayProperties;
    }


    @Override
    public List<SwaggerResource> get() {
        List<SwaggerResource> resources = new ArrayList<>();
        List<String> routes = new ArrayList<>();
        routeLocator.getRoutes().subscribe(route -> {
            routes.add(route.getId());
        });
        gatewayProperties.getRoutes().stream().filter(routeDefinition ->
                routes.contains(routeDefinition.getId())
        )
                .forEach(routeDefinition -> {
                    routeDefinition.getPredicates().stream()
                            .filter(predicateDefinition -> ("Path").equalsIgnoreCase(predicateDefinition.getName()))
                            .filter(predicateDefinition ->
                                    !(ServiceNameConstants.AUTH_SERVICE)
                                            .equalsIgnoreCase(routeDefinition.getId()))
//                            .filter(predicateDefinition ->
//                                    !(ServiceNameConstants.UMPS_SERVICE)
//                                            .equalsIgnoreCase(routeDefinition.getId()))
                            .forEach(predicateDefinition -> {
                                        resources.add(swaggerResource(routeDefinition.getId(),
                                                predicateDefinition.getArgs().get(NameUtils.GENERATED_NAME_PREFIX + "0")
                                                        .replace("/**", API_URI)));
                                    }
                            );
                });
        return resources;
    }

    private SwaggerResource swaggerResource(String name, String location) {
        SwaggerResource swaggerResource = new SwaggerResource();
        swaggerResource.setName(name);
        swaggerResource.setLocation(location);
        swaggerResource.setSwaggerVersion("2.0");
        return swaggerResource;
    }
}
