package cn.uway.skynet.cloud.common.security.service;

import cn.hutool.core.util.ArrayUtil;
import cn.hutool.core.util.StrUtil;
import cn.uway.skynet.cloud.common.core.constant.CommonConstants;
import cn.uway.skynet.cloud.common.core.constant.SecurityConstants;
import cn.uway.skynet.cloud.common.core.util.R;
import cn.uway.skynet.cloud.feign.dto.UserInfo;
import org.springframework.security.core.GrantedAuthority;
import org.springframework.security.core.authority.AuthorityUtils;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.security.core.userdetails.UsernameNotFoundException;

import java.util.Arrays;
import java.util.Collection;
import java.util.HashSet;
import java.util.Set;

public class UserApdater {
    /**
     * 构建userdetails
     *
     * @param result 用户信息
     * @return
     */
    protected UserDetails getUserDetails(R<UserInfo> result) {
        if (result == null || result.getData() == null) {
            throw new UsernameNotFoundException("用户不存在");
        }

        UserInfo info = result.getData();
        Set<String> dbAuthsSet = new HashSet<>();
        if (ArrayUtil.isNotEmpty(info.getRoles())) {
            // 获取角色
            Arrays.stream(info.getRoles()).forEach(role -> dbAuthsSet.add(SecurityConstants.ROLE + role));
            // 获取资源
            dbAuthsSet.addAll(Arrays.asList(info.getPermissions()));
        }
        Collection<? extends GrantedAuthority> authorities
                = AuthorityUtils.createAuthorityList(dbAuthsSet.toArray(new String[0]));


        // 构造security用户
        return new UserDetailIpml(info.getUserId(),
                info.getDeptId(), info.getUsername(), SecurityConstants.BCRYPT + info.getPassword(),
                StrUtil.equals(info.getLockFlag(), CommonConstants.STATUS_NORMAL), true, true, true, authorities);
    }


}
