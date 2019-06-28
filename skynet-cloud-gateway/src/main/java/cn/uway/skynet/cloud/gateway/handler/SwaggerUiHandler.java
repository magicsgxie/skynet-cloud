package cn.uway.skynet.cloud.gateway.handler;

import lombok.AllArgsConstructor;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.context.annotation.Configuration;
import org.springframework.http.HttpStatus;
import org.springframework.http.MediaType;
import org.springframework.stereotype.Component;
import org.springframework.web.reactive.function.BodyInserters;
import org.springframework.web.reactive.function.server.HandlerFunction;
import org.springframework.web.reactive.function.server.ServerRequest;
import org.springframework.web.reactive.function.server.ServerResponse;
import reactor.core.publisher.Mono;
import springfox.documentation.swagger.web.UiConfiguration;
import springfox.documentation.swagger.web.UiConfigurationBuilder;

import java.util.Optional;

@Component
public class SwaggerUiHandler implements HandlerFunction {

    @Autowired(required = false)
    private UiConfiguration uiConfiguration;

    @Override
    public Mono handle(ServerRequest serverRequest) {
        return ServerResponse.status(HttpStatus.OK)
                    .contentType(MediaType.APPLICATION_JSON_UTF8)
                    .body(BodyInserters.fromObject(
                            Optional.ofNullable(uiConfiguration)
                                    .orElse(UiConfigurationBuilder.builder().build())));
    }

}
