package cn.uway.skynet.cloud.common.security.exception;

import cn.uway.skynet.cloud.common.security.component.SkynetCloudAuth2ExceptionSerializer;
import com.fasterxml.jackson.databind.annotation.JsonSerialize;

@JsonSerialize(using = SkynetCloudAuth2ExceptionSerializer.class)
public class InvalidException extends SkynetCloudAuth2Exception {

    public InvalidException(String msg, Throwable t) {
        super(msg);
    }

    @Override
    public String getOAuth2ErrorCode() {
        return "invalid_exception";
    }

    @Override
    public int getHttpErrorCode() {
        return 426;
    }

}
