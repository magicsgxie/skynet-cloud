package cn.uway.skynet.cloud.common.core.util;

import java.util.Date;

public class ObjectUtils {
    public static boolean isInteger(Object param) {
        return (param instanceof Integer);
    }

    public static boolean isFloat(Object param) {
        return (param instanceof Float);
    }

    public static boolean isString(Object param) {
        return (param instanceof String);
    }

    public static boolean isDouble(Object param) {
        return (param instanceof Double);
    }

    public static boolean isLong(Object param) {
        return (param instanceof Long);
    }

    public static boolean isBoolean(Object param) {
        return (param instanceof Boolean);
    }

    public static boolean isDate(Object param) {
        return (param instanceof Date);
    }

    public static boolean isArray(Object obj) {
        if(obj == null) {
            return false;
        }
        return obj.getClass().isArray();
    }
}
