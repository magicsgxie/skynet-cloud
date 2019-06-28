package cn.uway.skynet.cloud.common.security.component;

import cn.uway.skynet.cloud.common.security.service.SkynetCloudUserService;
import org.springframework.security.authentication.AuthenticationProvider;
import org.springframework.security.core.Authentication;
import org.springframework.security.core.AuthenticationException;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.security.core.userdetails.UsernameNotFoundException;

public class SkynetCloudMobileAuthenticationProvider implements AuthenticationProvider {
    private SkynetCloudUserService skynetCloudUserService;

    @Override
    public Authentication authenticate(Authentication authentication) throws AuthenticationException {
        SkynetCloudMobileAuthenticationToken mobileAuthenticationToken = (SkynetCloudMobileAuthenticationToken) authentication;
        UserDetails userDetails = skynetCloudUserService.findUserByMobile((String) mobileAuthenticationToken.getPrincipal());

        if (userDetails == null) {
            throw new UsernameNotFoundException("手机号码不存在:" + mobileAuthenticationToken.getPrincipal());
        }



        SkynetCloudMobileAuthenticationToken authenticationToken = new SkynetCloudMobileAuthenticationToken(userDetails, userDetails.getAuthorities());
        authenticationToken.setDetails(mobileAuthenticationToken.getDetails());
        return authenticationToken;
    }

    @Override
    public boolean supports(Class<?> authentication) {
        return SkynetCloudMobileAuthenticationToken.class.isAssignableFrom(authentication);
    }

}