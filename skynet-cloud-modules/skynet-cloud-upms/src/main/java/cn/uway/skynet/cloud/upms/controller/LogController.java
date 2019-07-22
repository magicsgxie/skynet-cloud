/*
 *  Copyright (c) 2019-2020, schealth365 (magic.s.g.xie@126.com).
 *  <p>
 *  Licensed under the GNU Lesser General Public License 3.0 (the "License");
 *  you may not use this file except in compliance with the License.
 *  You may obtain a copy of the License at
 *  <p>
 * https://www.schealth365.cn
 *  <p>
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

package cn.uway.skynet.cloud.upms.controller;


import cn.hutool.core.bean.BeanUtil;
import cn.uway.skynet.cloud.feign.dto.LogDTO;
import cn.uway.skynet.cloud.upms.vo.PreLogVo;
import com.alibaba.fastjson.JSONObject;
import com.baomidou.mybatisplus.core.toolkit.BeanUtils;
import com.baomidou.mybatisplus.core.toolkit.Wrappers;
import com.baomidou.mybatisplus.extension.plugins.pagination.Page;
import cn.uway.skynet.cloud.upms.entity.SysLog;
import cn.uway.skynet.cloud.upms.service.SysLogService;
import cn.uway.skynet.cloud.common.core.util.R;
import cn.uway.skynet.cloud.common.security.annotation.Inner;
import lombok.AllArgsConstructor;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.web.bind.annotation.*;

import javax.validation.Valid;
import java.util.List;

/**
 * <p>
 * 日志表 前端控制器
 * </p>
 *
 * @author magic.s.g.xie
 * @since 2019/2/1
 */
@RestController
@AllArgsConstructor
@RequestMapping("/log")
public class LogController {
	private final SysLogService sysLogService;

	/**
	 * 简单分页查询
	 *
	 * @param page   分页对象
	 * @param sysLog 系统日志
	 * @return
	 *
	 */
	@GetMapping("/page")
	public R getLogPage(Page page, SysLog sysLog) {
		return new R<>(sysLogService.page(page, Wrappers.query(sysLog)));
	}

	/**
	 * 删除日志
	 *
	 * @param id ID
	 * @return success/false
	 */
	@DeleteMapping("/{id}")
	@PreAuthorize("@pms.hasPermission('sys_log_del')")
	public R removeById(@PathVariable Long id) {
		return new R<>(sysLogService.removeById(id));
	}

//	/**
//	 * 插入日志
//	 *
//	 * @param sysLog 日志实体
//	 * @return success/false
//	 */
//	@Inner
//	@PostMapping
//	public R save(@Valid @RequestBody SysLog sysLog) {
//		return new R<>(sysLogService.save(sysLog));
//	}

	/**
	 * 插入日志
	 *
	 * @param logDTO 日志实体
	 * @return success/false
	 */
	@Inner
	@PostMapping
	public R add(@Valid @RequestBody LogDTO logDTO) {
		SysLog sysLog = new SysLog();
		BeanUtil.copyProperties(logDTO, sysLog);
		return new R<>(sysLogService.save(sysLog));
	}


	/**
	 * 批量插入前端异常日志
	 *
	 * @param preLogVoList 日志实体
	 * @return success/false
	 */
	@PostMapping("/logs")
	public R saveBatchLogs(@RequestBody List<PreLogVo> preLogVoList) {
		return new R<>(sysLogService.saveBatchLogs(preLogVoList));
	}
}
