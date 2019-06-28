package cn.uway.skynet.cloud.feign.fallback;

import cn.uway.skynet.cloud.common.core.util.R;
import cn.uway.skynet.cloud.feign.RemoteMemberService;
import cn.uway.skynet.cloud.feign.dto.IntegrationConsumeSettingDTO;
import cn.uway.skynet.cloud.feign.dto.MemberDTO;
import cn.uway.skynet.cloud.feign.dto.UserInfo;
import lombok.Setter;
import lombok.extern.slf4j.Slf4j;
import org.springframework.stereotype.Component;

import java.util.Map;

@Slf4j
@Component
public class RemoteMemberServiceFallbackImpl implements RemoteMemberService {
    @Setter
    private Throwable cause;



    @Override
    public R<MemberDTO> member(Long id) {
        log.error("feign 查询用户信息失败:{}", id, cause);
        return null;
    }

    @Override
    public R<Map<Long, MemberDTO>> query(Long[] ids) {
        log.error("feign 批量查询用户信息失败:{}", ids.length, cause);
        return null;
    }

    @Override
    public R<UserInfo> info(String username, String from) {
        return null;
    }

    @Override
    public R<UserInfo> mobile(String mobile, String from) {
        log.error("feign 查询用户信息失败:{}", mobile, cause);
        return null;
    }

    @Override
    public R<IntegrationConsumeSettingDTO> get(Long Id) {
        log.error("feign 查询积分消费设定失败:{}", Id, cause);
        return null;
    }
}
