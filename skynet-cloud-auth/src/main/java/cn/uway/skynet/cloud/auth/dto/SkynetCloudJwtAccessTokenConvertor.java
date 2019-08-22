package cn.uway.skynet.cloud.auth.dto;

import cn.uway.skynet.cloud.common.core.constant.SecurityConstants;
import org.springframework.security.oauth2.common.OAuth2AccessToken;
import org.springframework.security.oauth2.provider.OAuth2Authentication;
import org.springframework.security.oauth2.provider.token.store.JwtAccessTokenConverter;

import java.util.Map;

public class SkynetCloudJwtAccessTokenConvertor extends JwtAccessTokenConverter {
    @Override
    public Map<String, ?> convertAccessToken(OAuth2AccessToken token, OAuth2Authentication authentication) {
        Map<String, Object> representation = (Map<String, Object>)super.convertAccessToken(token, authentication);
        representation.put("license", SecurityConstants.PROJECT_LICENSE);
        return representation;
    }

    @Override
    public OAuth2Authentication extractAuthentication(Map<String, ?> map) {
        return super.extractAuthentication(map);
    }

    @Override
    public OAuth2AccessToken extractAccessToken(String value, Map<String, ?> map) {
        return super.extractAccessToken(value, map);
    }
}
