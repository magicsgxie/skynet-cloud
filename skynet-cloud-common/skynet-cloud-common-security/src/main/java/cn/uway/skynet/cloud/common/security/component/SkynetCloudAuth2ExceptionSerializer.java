package cn.uway.skynet.cloud.common.security.component;

import cn.uway.skynet.cloud.common.core.constant.CommonConstants;
import cn.uway.skynet.cloud.common.security.exception.SkynetCloudAuth2Exception;
import com.fasterxml.jackson.core.JsonGenerator;
import com.fasterxml.jackson.databind.SerializerProvider;
import com.fasterxml.jackson.databind.ser.std.StdSerializer;
import lombok.SneakyThrows;

/**
 * @author magic.s.g.xie
 * OAuth2 异常格式化
 */

public class SkynetCloudAuth2ExceptionSerializer extends StdSerializer<SkynetCloudAuth2Exception> {
    public SkynetCloudAuth2ExceptionSerializer() {
        super(SkynetCloudAuth2Exception.class);
    }

    @Override
    @SneakyThrows
    public void serialize(SkynetCloudAuth2Exception value, JsonGenerator gen, SerializerProvider provider) {
        gen.writeStartObject();
        gen.writeObjectField("code", CommonConstants.FAIL);
        gen.writeStringField("msg", value.getMessage());
        gen.writeStringField("data", value.getErrorCode());
        gen.writeEndObject();
    }
}