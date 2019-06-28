package cn.uway.skynet.cloud.common.security.service;

import cn.uway.skynet.cloud.feign.dto.UserInfo;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.security.core.userdetails.UsernameNotFoundException;

public interface SkynetCloudUserService {
    UserDetails loadUserByUsername(String username) throws UsernameNotFoundException;

    UserDetails findUserByMobile(String username) throws UsernameNotFoundException;

    UserDetails loadUserByOpenId(String openId) throws UsernameNotFoundException;


}
