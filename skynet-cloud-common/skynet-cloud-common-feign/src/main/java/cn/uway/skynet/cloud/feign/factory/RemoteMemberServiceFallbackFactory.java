package cn.uway.skynet.cloud.feign.factory;

import cn.uway.skynet.cloud.common.core.util.R;
import cn.uway.skynet.cloud.feign.RemoteMemberService;
import cn.uway.skynet.cloud.feign.dto.IntegrationConsumeSettingDTO;
import cn.uway.skynet.cloud.feign.fallback.RemoteMemberServiceFallbackImpl;
import feign.hystrix.FallbackFactory;
import org.springframework.stereotype.Component;

@Component
public class RemoteMemberServiceFallbackFactory implements FallbackFactory<RemoteMemberService> {

    @Override
    public RemoteMemberService create(Throwable throwable) {
        RemoteMemberServiceFallbackImpl remoteMemberServiceFallback = new RemoteMemberServiceFallbackImpl();
        remoteMemberServiceFallback.setCause(throwable);
        return remoteMemberServiceFallback;
    }

}