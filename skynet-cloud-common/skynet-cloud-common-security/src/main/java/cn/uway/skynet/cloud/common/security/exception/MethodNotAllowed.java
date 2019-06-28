package cn.uway.skynet.cloud.common.security.exception;

import cn.uway.skynet.cloud.common.security.component.SkynetCloudAuth2ExceptionSerializer;
import com.fasterxml.jackson.databind.annotation.JsonSerialize;
import org.springframework.http.HttpStatus;

@JsonSerialize(using = SkynetCloudAuth2ExceptionSerializer.class)
public class MethodNotAllowed extends SkynetCloudAuth2Exception {
    public MethodNotAllowed(String msg, Throwable t) {
        super(msg);
    }

    @Override
    public String getOAuth2ErrorCode() {
        return "method_not_allowed";
    }

    @Override
    public int getHttpErrorCode() {
        return HttpStatus.METHOD_NOT_ALLOWED.value();
    }
}
