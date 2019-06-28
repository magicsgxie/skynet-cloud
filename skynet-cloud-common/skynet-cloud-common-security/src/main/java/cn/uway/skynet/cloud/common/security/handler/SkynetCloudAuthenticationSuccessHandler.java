package cn.uway.skynet.cloud.common.security.handler;

import cn.hutool.core.map.MapUtil;
import cn.hutool.core.util.CharsetUtil;
import cn.uway.skynet.cloud.common.core.constant.CommonConstants;
import cn.uway.skynet.cloud.common.security.utils.AuthUtils;
import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.ObjectMapper;
import lombok.extern.slf4j.Slf4j;
import org.springframework.http.HttpHeaders;
import org.springframework.security.authentication.BadCredentialsException;
import org.springframework.security.core.Authentication;
import org.springframework.security.crypto.password.PasswordEncoder;
import org.springframework.security.oauth2.common.OAuth2AccessToken;
import org.springframework.security.oauth2.common.exceptions.InvalidClientException;
import org.springframework.security.oauth2.common.exceptions.UnapprovedClientAuthenticationException;
import org.springframework.security.oauth2.provider.*;
import org.springframework.security.oauth2.provider.request.DefaultOAuth2RequestValidator;
import org.springframework.security.oauth2.provider.token.AuthorizationServerTokenServices;
import org.springframework.security.web.authentication.AuthenticationSuccessHandler;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;
import java.io.PrintWriter;

@Slf4j
public  abstract class SkynetCloudAuthenticationSuccessHandler implements AuthenticationSuccessHandler {
    private static final String BASIC_ = "Basic ";

    protected abstract ClientDetails loadClientByClientId(String clientId);

    protected abstract boolean validateClientSecret(String orginal,String clientSecret);

    protected abstract OAuth2AccessToken createAccessToken(OAuth2Authentication oAuth2Authentication);

    protected abstract String writeValueAsString(OAuth2AccessToken oAuth2AccessToken) throws JsonProcessingException;

    protected void onAuthenticationSuccessProcess(HttpServletRequest request, HttpServletResponse response, Authentication authentication) {
        String header = request.getHeader(HttpHeaders.AUTHORIZATION);

        if (header == null || !header.startsWith(BASIC_)) {
            throw new UnapprovedClientAuthenticationException("请求头中client信息为空");
        }

        try {
            String[] tokens = AuthUtils.extractAndDecodeHeader(header);
            assert tokens.length == 2;
            String clientId = tokens[0];

            ClientDetails clientDetails = loadClientByClientId(clientId);

//            //校验secret
//            if (!passwordEncoder.matches(tokens[1], clientDetails.getClientSecret())) {
//                throw new InvalidClientException("Given client ID does not match authenticated client");
//
//            }
            if(!validateClientSecret(tokens[1], clientDetails.getClientSecret())) {
                throw new InvalidClientException("Given client ID does not match authenticated client");
            }

            TokenRequest tokenRequest = new TokenRequest(MapUtil.newHashMap(), clientId, clientDetails.getScope(), "mobile");

            //校验scope
            new DefaultOAuth2RequestValidator().validateScope(tokenRequest, clientDetails);
            OAuth2Request oAuth2Request = tokenRequest.createOAuth2Request(clientDetails);
            OAuth2Authentication oAuth2Authentication = new OAuth2Authentication(oAuth2Request, authentication);
            //OAuth2AccessToken oAuth2AccessToken = defaultAuthorizationServerTokenServices.createAccessToken(oAuth2Authentication);
            OAuth2AccessToken oAuth2AccessToken =createAccessToken(oAuth2Authentication);
            log.info("获取token 成功：{}", oAuth2AccessToken.getValue());

            response.setCharacterEncoding(CharsetUtil.UTF_8);
            response.setContentType(CommonConstants.CONTENT_TYPE);
            PrintWriter printWriter = response.getWriter();
            //printWriter.append(objectMapper.writeValueAsString(oAuth2AccessToken));
            printWriter.append(writeValueAsString(oAuth2AccessToken));
        } catch (IOException e) {
            throw new BadCredentialsException(
                    "Failed to decode basic authentication token");
        }
    }
}
