package cn.uway.skynet.cloud.common.security.handler;


import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.ObjectMapper;
import lombok.Builder;

import org.springframework.security.core.Authentication;
import org.springframework.security.crypto.password.PasswordEncoder;
import org.springframework.security.oauth2.common.OAuth2AccessToken;
import org.springframework.security.oauth2.provider.ClientDetails;
import org.springframework.security.oauth2.provider.ClientDetailsService;
import org.springframework.security.oauth2.provider.OAuth2Authentication;
import org.springframework.security.oauth2.provider.token.AuthorizationServerTokenServices;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;


/**
 * 手机号登录成功，返回oauth token
 */

@Builder
public class MobileLoginSuccessHandler extends SkynetCloudAuthenticationSuccessHandler {
    private ObjectMapper objectMapper;
    private PasswordEncoder passwordEncoder;
    private ClientDetailsService clientDetailsService;
    private AuthorizationServerTokenServices defaultAuthorizationServerTokenServices;

    /**
     * Called when a user has been successfully authenticated.
     * 调用spring security oauth API 生成 oAuth2AccessToken
     *
     * @param request        the request which caused the successful authentication
     * @param response       the response
     * @param authentication the <tt>Authentication</tt> object which was created during
     */
    @Override
    public void onAuthenticationSuccess(HttpServletRequest request, HttpServletResponse response, Authentication authentication) {
        this.onAuthenticationSuccessProcess(request, response, authentication);
    }

    @Override
    protected ClientDetails loadClientByClientId(String clientId) {
        return clientDetailsService.loadClientByClientId(clientId);
    }

    @Override
    protected boolean validateClientSecret(String orginal, String clientSecret) {
        return passwordEncoder.matches(orginal, clientSecret);
    }

    @Override
    protected OAuth2AccessToken createAccessToken(OAuth2Authentication oAuth2Authentication) {
        return defaultAuthorizationServerTokenServices.createAccessToken(oAuth2Authentication);
    }

    @Override
    protected String writeValueAsString(OAuth2AccessToken oAuth2AccessToken) throws JsonProcessingException {
        return objectMapper.writeValueAsString(oAuth2AccessToken);
    }
}