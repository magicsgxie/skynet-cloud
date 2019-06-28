package cn.uway.skynet.cloud.common.core.xss;

import cn.uway.skynet.cloud.common.core.exception.CheckedException;
import org.apache.commons.lang.StringUtils;

/**
 * SQL过滤
 * @author magic.s.g.xie
 * @email xiesg@uway.cn
 * @date 2019-01-01 16:16
 */
public class SQLFilter {

    /**
     * SQL注入过滤
     * @param str  待验证的字符串
     */
    public static String sqlInject(String str){
        if(StringUtils.isBlank(str)){
            return null;
        }
        //去掉'|"|;|\字符
        str = StringUtils.replace(str, "'", "");
        str = StringUtils.replace(str, "\"", "");
        str = StringUtils.replace(str, ";", "");
        str = StringUtils.replace(str, "\\", "");

        //转换成小写
        str = str.toLowerCase();

        //非法字符
        String[] keywords = {"master", "truncate", "insert", "select", "delete", "update", "declare", "alert", "drop"};

        //判断是否包含非法字符
        for(String keyword : keywords){
            if(str.indexOf(keyword) != -1 ){
                  if(keyword.equals("update") && str.indexOf(keyword+"d") == -1)
                    throw new CheckedException("包含非法字符");
                  else if(!keyword.equals("update"))
                      throw new CheckedException("包含非法字符");
            }
        }

        return str;
    }
}
