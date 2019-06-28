package cn.uway.skynet.cloud.feign;

import cn.uway.skynet.cloud.common.core.util.R;
import cn.uway.skynet.cloud.feign.dto.UserInfo;
import cn.uway.skynet.cloud.feign.factory.RemoteUserServiceFallbackFactory;
import cn.uway.skynet.cloud.common.core.constant.SecurityConstants;
import cn.uway.skynet.cloud.common.core.constant.ServiceNameConstants;
import com.alibaba.fastjson.JSONObject;
import org.springframework.cloud.openfeign.FeignClient;
import org.springframework.web.bind.annotation.*;


@FeignClient(value = ServiceNameConstants.UPMS_SERVICE, fallbackFactory = RemoteUserServiceFallbackFactory.class,contextId = "remoteUserService")
public interface RemoteUserService {
    /**
     * 通过用户名查询用户、角色信息
     *
     * @param username 用户名
     * @param from     调用标志
     * @return R
     */
    @GetMapping("/user/info/{username}")
    R<UserInfo> info(@PathVariable("username") String username
            , @RequestHeader(SecurityConstants.FROM) String from);

    /**
     * 通过社交账号查询用户、角色信息
     *
     * @param inStr appid@code
     * @return
     */
    @GetMapping("/user/social/info/{inStr}")
    R<UserInfo> social(@PathVariable("inStr") String inStr);

//    /**
//     * 保存日志
//     *
//     * @param sysLog 日志实体
//     * @param from   内部调用标志
//     * @return succes、false
//     */
//    @PostMapping("/log")
//    R<Boolean> saveLog(@RequestBody JSONObject sysLog, @RequestHeader(SecurityConstants.FROM) String from);
}