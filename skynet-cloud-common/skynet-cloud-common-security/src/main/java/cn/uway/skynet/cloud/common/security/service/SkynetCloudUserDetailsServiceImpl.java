package cn.uway.skynet.cloud.common.security.service;

import cn.uway.skynet.cloud.feign.RemoteUserService;

import cn.uway.skynet.cloud.common.core.constant.SecurityConstants;
import cn.uway.skynet.cloud.common.core.util.R;
import cn.uway.skynet.cloud.feign.dto.UserInfo;
import lombok.AllArgsConstructor;
import lombok.extern.slf4j.Slf4j;
import org.springframework.cache.Cache;
import org.springframework.cache.CacheManager;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.security.core.userdetails.UserDetailsService;
import org.springframework.security.core.userdetails.UsernameNotFoundException;
import org.springframework.stereotype.Service;


/**
 * 用户详细信息
 */
@Slf4j
@Service
@AllArgsConstructor
public class SkynetCloudUserDetailsServiceImpl extends  UserApdater implements  UserDetailsService {



    private final RemoteUserService remoteUserService;



    private final CacheManager cacheManager;

    /**
     * 用户密码登录
     *
     * @param username 用户名
     * @return
     * @throws UsernameNotFoundException
     */
    @Override
    public UserDetails loadUserByUsername(String username) throws UsernameNotFoundException {
        return getUserDetails(1, username);
    }


    private UserDetails getUserDetails(int type, String username) {

        Cache cache = cacheManager.getCache("user_details");
        if (cache != null && cache.get(username) != null) {
            return (UserDetailIpml) cache.get(username).get();
        }
        //DruidDataSource
        R<UserInfo> result = remoteUserService.info(username, SecurityConstants.FROM_IN);

        UserDetails userDetails = getUserDetails(result);
        cache.put(username, userDetails);
        return userDetails;
    }


}