package cn.uway.skynet.cloud.common.security.service;


import cn.uway.skynet.cloud.common.core.constant.SecurityConstants;
import cn.uway.skynet.cloud.common.core.util.R;
import cn.uway.skynet.cloud.feign.RemoteMemberService;
import cn.uway.skynet.cloud.feign.dto.UserInfo;
import org.springframework.cache.Cache;
import org.springframework.cache.CacheManager;
import lombok.AllArgsConstructor;
import lombok.extern.slf4j.Slf4j;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.security.core.userdetails.UsernameNotFoundException;
import org.springframework.stereotype.Service;

@Slf4j
@Service
@AllArgsConstructor
public class SkyNetCloudAppUserServiceImpl extends  UserApdater implements SkynetCloudUserService {

    private final RemoteMemberService remoteMemberService;

    private final CacheManager cacheManager;

    @Override
    public UserDetails loadUserByUsername(String membername) throws UsernameNotFoundException {
        Cache cache1 = cacheManager.getCache("member_details");
        if (cache1 != null && cache1.get(membername) != null) {
            return (UserDetailIpml) cache1.get(membername).get();
        }

        //DruidDataSource
        R<UserInfo> result = remoteMemberService.info(membername, SecurityConstants.FROM_IN);

        UserDetails memberDetails = getUserDetails(result);
        cache1.put(membername, memberDetails);
        return memberDetails;
    }

    @Override
    public UserDetails findUserByMobile(String mobile) throws UsernameNotFoundException {

        //DruidDataSource
        R<UserInfo> result = remoteMemberService.mobile(mobile, SecurityConstants.FROM_IN);
        if(result.getData() == null) {
            throw new UsernameNotFoundException("手机号码不存在："+ mobile);
        }
        Cache cache = cacheManager.getCache("member_details");
        if (cache != null && cache.get(result.getData().getUsername()) != null) {
            return (UserDetailIpml) cache.get(result.getData().getUsername()).get();
        }

        UserDetails userDetails = getUserDetails(result);
        cache.put(result.getData().getUsername(), userDetails);
        return userDetails;
    }

    @Override
    public UserDetails loadUserByOpenId(String openId) throws UsernameNotFoundException {
        return null;
    }
}
