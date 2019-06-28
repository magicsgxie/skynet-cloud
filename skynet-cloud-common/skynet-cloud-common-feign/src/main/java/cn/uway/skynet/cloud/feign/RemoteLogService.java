/*
 *  Copyright (c) 2019-2020, magic.s.g.xie (xieshaoguangwww@gmail.com).
 *  <p>
 *  Licensed under the GNU Lesser General Public License 3.0 (the "License");
 *  you may not use this file except in compliance with the License.
 *  You may obtain a copy of the License at
 *  <p>
 * https://www.uway.cn
 *  <p>
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

package cn.uway.skynet.cloud.feign;


import cn.uway.skynet.cloud.feign.dto.LogDTO;
import cn.uway.skynet.cloud.feign.factory.RemoteLogServiceFallbackFactory;
import cn.uway.skynet.cloud.common.core.constant.SecurityConstants;
import cn.uway.skynet.cloud.common.core.constant.ServiceNameConstants;
import cn.uway.skynet.cloud.common.core.util.R;
import com.alibaba.fastjson.JSONObject;
import org.springframework.cloud.openfeign.FeignClient;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestHeader;

/**
 * @author magic.s.g.xie
 * @date 2019/2/1
 */
@FeignClient(value = ServiceNameConstants.UPMS_SERVICE, fallbackFactory = RemoteLogServiceFallbackFactory.class, contextId = "remoteLogService")
public interface RemoteLogService {
	/**
	 * 保存日志
	 *
	 * @param sysLog 日志实体
	 * @param from   内部调用标志
	 * @return succes、false
	 */
	@PostMapping("/log")
	R<Boolean> saveLog(@RequestBody LogDTO sysLog, @RequestHeader(SecurityConstants.FROM) String from);
}
