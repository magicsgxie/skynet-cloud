package cn.uway.skynet.cloud.common.data.utils;

import cn.uway.skynet.cloud.common.core.constant.CommonConstants;
import cn.uway.skynet.cloud.common.core.util.DateUtils;
import cn.uway.skynet.cloud.common.core.util.ObjectUtils;
import cn.uway.skynet.cloud.common.core.xss.SQLFilter;
import com.baomidou.mybatisplus.core.conditions.Wrapper;
import com.baomidou.mybatisplus.core.conditions.query.QueryWrapper;
import com.baomidou.mybatisplus.core.metadata.IPage;

import com.baomidou.mybatisplus.core.metadata.IPage;
import com.baomidou.mybatisplus.extension.plugins.pagination.Page;
import org.apache.commons.lang.StringUtils;

import java.util.Map;

/**
 * 查询参数
 */
public class Query {


    public static <T> IPage getPage(Map<String, Object> params) {
        return getPage(params, null, false);
    }

    public static <T> IPage getPage(Map<String, Object> params, String defaultOrderField, boolean isAsc) {
        //分页参数
        long curPage = 1;
        long limit = 10;

        if(params.get(CommonConstants.PAGE) != null){
            curPage = Long.parseLong((String)params.get(CommonConstants.PAGE));
        }
        if(params.get(CommonConstants.LIMIT) != null){
            limit = Long.parseLong((String)params.get(CommonConstants.LIMIT));
        }

        //分页对象
        Page<T> page = new Page<>(curPage, limit);

        //分页参数
        params.put(CommonConstants.PAGE, page);

        //排序字段
        //防止SQL注入（因为sidx、order是通过拼接SQL实现排序的，会有SQL注入风险）
        if(params.containsKey(CommonConstants.ORDER)) {
            String order = (String)params.get(CommonConstants.ORDER);
            isAsc = CommonConstants.ASC.equalsIgnoreCase(order);
        }
        if(params.containsKey(CommonConstants.ORDER_FIELD)) {
            defaultOrderField = getColumn(SQLFilter.sqlInject((String)params.get(CommonConstants.ORDER_FIELD)));
        }
        //默认排序
        if(isAsc) {
            page.setAsc(defaultOrderField);
        }else {
            page.setDesc(defaultOrderField);
        }

        return page;
    }

    public static  <T> QueryWrapper getWrapper(Map<String, Object> params) {
        QueryWrapper<T> wrapper = new QueryWrapper<>();
        for (Map.Entry<String, Object> item :params.entrySet()
        ){
            if(
                    (!item.getKey().equalsIgnoreCase("t"))
                            &&
                            (!item.getKey().equalsIgnoreCase(CommonConstants.PAGE))
                            && (!item.getKey().equalsIgnoreCase(CommonConstants.LIMIT))
                            && (!item.getKey().equalsIgnoreCase(CommonConstants.ORDER_FIELD))
                            && (!item.getKey().equalsIgnoreCase(CommonConstants.ORDER))
            ) {
                if(item.getValue() != null && ObjectUtils.isString(item.getValue()) && item.getValue().toString().contains("~"))
                {
                    String[] arrays = item.getValue().toString().trim().split("~");
                    wrapper.le(getColumn(item.getKey()), DateUtils.stringToDate(arrays[1], DateUtils.DATE_TIME_PATTERN));
                    wrapper.ge(getColumn(item.getKey()), DateUtils.stringToDate(arrays[0], DateUtils.DATE_TIME_PATTERN));
                }
                else if(item.getValue() != null && ObjectUtils.isString(item.getValue())) {
                    wrapper.like(getColumn(item.getKey()), item.getValue().toString().trim());
                }
                else  if(item.getValue() != null && (ObjectUtils.isInteger(item.getValue())
                        || ObjectUtils.isBoolean(item.getValue())
                        || ObjectUtils.isLong(item.getValue()))
                ){
                    wrapper.eq(getColumn(item.getKey()), item.getValue());
                }
            }
        }

        return wrapper;
    }

    private static String getColumn(String attrName) {
        return com.baomidou.mybatisplus.core.toolkit.StringUtils.camelToUnderline(attrName);
    }

}
