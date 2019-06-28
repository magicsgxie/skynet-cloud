package cn.uway.skynet.cloud.common.security.exception;

import cn.uway.skynet.cloud.common.security.component.SkynetCloudAuth2ExceptionSerializer;
import com.fasterxml.jackson.databind.annotation.JsonSerialize;
import lombok.Getter;
import org.springframework.security.oauth2.common.exceptions.OAuth2Exception;
import com.fasterxml.jackson.databind.annotation.JsonSerialize;

@JsonSerialize(using = SkynetCloudAuth2ExceptionSerializer.class)
public class SkynetCloudAuth2Exception extends OAuth2Exception {
    @Getter
    private String errorCode;

    public SkynetCloudAuth2Exception(String msg) {
        super(msg);
    }

    public SkynetCloudAuth2Exception(String msg, String errorCode) {
        super(msg);
        this.errorCode = errorCode;
    }
}