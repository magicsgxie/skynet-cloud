/*
 *  Copyright (c) 2019-2020, uway (magic.s.g.xie@126.com).
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

package cn.uway.skynet.cloud.common.data.datasources.config;

import cn.uway.skynet.cloud.common.data.datascope.DataScopeInterceptor;
import com.baomidou.mybatisplus.core.injector.ISqlInjector;
import com.baomidou.mybatisplus.extension.injector.LogicSqlInjector;
import com.baomidou.mybatisplus.extension.plugins.PaginationInterceptor;
import org.mybatis.spring.annotation.MapperScan;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

/**
 * @author magic.s.g.xie
 * @date 2019/2/1
 */
@Configuration
@MapperScan("cn.uway.skynet.cloud.**.mapper")
public class MybatisPlusConfigurer {
	/**
	 * 分页插件
	 *
	 * @return PaginationInterceptor
	 */
	@Bean
	public PaginationInterceptor paginationInterceptor() {
		return new PaginationInterceptor();
	}

	/**
	 * 数据权限插件
	 *
	 * @return DataScopeInterceptor
	 */
	@Bean
	public DataScopeInterceptor dataScopeInterceptor() {
		return new DataScopeInterceptor();
	}

	/**
	 * 逻辑删除
	 *
	 * @return
	 */
	@Bean
	public ISqlInjector sqlInjector() {
		return new LogicSqlInjector();
	}
}
