package cn.uway.skynet.cloud.common.security.component;

import cn.uway.skynet.cloud.common.security.service.SkynetCloudUserService;
import org.springframework.security.authentication.AuthenticationProvider;
import org.springframework.security.core.Authentication;
import org.springframework.security.core.AuthenticationException;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.security.core.userdetails.UsernameNotFoundException;

public class SkynetCloudAppAuthenticationProvider implements AuthenticationProvider {

    private SkynetCloudUserService skynetCloudUserService;

    @Override
    public Authentication authenticate(Authentication authentication) throws AuthenticationException {
       SkynetCloudAppAuthenticationToken mobileAuthenticationToken = (SkynetCloudAppAuthenticationToken) authentication;
        UserDetails userDetails = skynetCloudUserService.loadUserByUsername((String) mobileAuthenticationToken.getPrincipal());

        if (userDetails == null) {
            throw new UsernameNotFoundException("用户名不存在:" + mobileAuthenticationToken.getPrincipal());
        }



        SkynetCloudAppAuthenticationToken authenticationToken = new SkynetCloudAppAuthenticationToken(userDetails, userDetails.getAuthorities());
        authenticationToken.setDetails(mobileAuthenticationToken.getDetails());
        return authenticationToken;
    }



    @Override
    public boolean supports(Class<?> authentication) {
        return SkynetCloudAppAuthenticationToken.class.isAssignableFrom(authentication);
    }
}