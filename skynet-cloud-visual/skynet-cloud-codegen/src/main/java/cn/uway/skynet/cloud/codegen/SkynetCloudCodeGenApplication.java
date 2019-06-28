/*
 *  Copyright (c) 2019-2020, 冷冷 (wangiegie@gmail.com).
 *  <p>
 *  Licensed under the GNU Lesser General Public License 3.0 (the "License");
 *  you may not use this file except in compliance with the License.
 *  You may obtain a copy of the License at
 *  <p>
 * https://www.gnu.org/licenses/lgpl.html
 *  <p>
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

package cn.uway.skynet.cloud.codegen;

import cn.uway.skynet.cloud.common.security.annotation.EnableSkynetCloudFeignClients;
import cn.uway.skynet.cloud.common.security.annotation.EnableSkynetCloudResourceServer;
import org.mybatis.spring.annotation.MapperScan;
import org.springframework.boot.SpringApplication;
import org.springframework.cloud.client.SpringCloudApplication;

/**
 * @author magic.s.g.xie
 * @date 2019/2/1
 * 代码生成模块
 */
@EnableSkynetCloudFeignClients
@EnableSkynetCloudResourceServer
@MapperScan("cn.uway.skynet.cloud.codegen.mapper")
@SpringCloudApplication
public class SkynetCloudCodeGenApplication {

	public static void main(String[] args) {
		SpringApplication.run(SkynetCloudCodeGenApplication.class, args);
	}
}
