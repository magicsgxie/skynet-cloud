package cn.uway.skynet.cloud.feign;

import cn.uway.skynet.cloud.common.core.constant.SecurityConstants;
import cn.uway.skynet.cloud.common.core.constant.ServiceNameConstants;
import cn.uway.skynet.cloud.common.core.util.R;
import cn.uway.skynet.cloud.feign.dto.IntegrationConsumeSettingDTO;
import cn.uway.skynet.cloud.feign.dto.MemberDTO;
import cn.uway.skynet.cloud.feign.dto.UserInfo;
import cn.uway.skynet.cloud.feign.factory.RemoteMemberServiceFallbackFactory;
import com.alibaba.fastjson.JSONObject;
import org.springframework.cloud.openfeign.FeignClient;
import org.springframework.web.bind.annotation.*;

import java.util.Map;

@FeignClient(value = ServiceNameConstants.UMS_SERVICE, fallbackFactory = RemoteMemberServiceFallbackFactory.class)
public interface RemoteMemberService {
    /**
     * 通过用户名查询用户、角色信息
     *
     * @param id 用户名
     * @return R
     */
    @GetMapping("/member/{id}")
    R<MemberDTO> member(@PathVariable("id") Long id);

    @GetMapping(value = "/member/list" , produces = "application/json")
    R<Map<Long, MemberDTO>> query(@RequestParam(value = "ids[]") Long[] ids);

    /**
     * 通过用户名查询用户、角色信息
     *
     * @param username 用户名
     * @param from     调用标志
     * @return R
     */
    @GetMapping("/member/info/{username}")
    R<UserInfo> info(@PathVariable("username") String username
            , @RequestHeader(SecurityConstants.FROM) String from);

    /**
     * 通过用户名查询用户、角色信息
     *
     * @param mobile 用户名
     * @param from     调用标志
     * @return R
     */
    @GetMapping("/member/mobile/{mobile}")
    R<UserInfo> mobile(@PathVariable("mobile") String mobile
            , @RequestHeader(SecurityConstants.FROM) String from);

    @GetMapping("/integrationconsumesetting/{id}")
    R<IntegrationConsumeSettingDTO> get(@PathVariable(value = "id") Long Id);

}
