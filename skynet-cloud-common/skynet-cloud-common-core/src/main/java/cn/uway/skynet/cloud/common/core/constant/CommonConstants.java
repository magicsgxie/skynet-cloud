package cn.uway.skynet.cloud.common.core.constant;

public interface CommonConstants {
    /**
     * 删除
     */
    String STATUS_DEL = "1";
    /**
     * 正常
     */
    String STATUS_NORMAL = "0";

    /**
     * 锁定
     */
    String STATUS_LOCK = "9";

    /**
     * 菜单
     */
    String MENU = "0";

    /**
     * 编码
     */
    String UTF8 = "UTF-8";

    /**
     * JSON 资源
     */
    String CONTENT_TYPE = "application/json; charset=utf-8";

    /**
     * 前端工程名
     */
    String FRONT_END_PROJECT = "pig-ui";

    /**
     * 后端工程名
     */
    String BACK_END_PROJECT = "pig";

    /**
     * 成功标记
     */
    Integer SUCCESS = 0;
    /**
     * 失败标记
     */
    Integer FAIL = 1;

    /**
     * 验证码前缀
     */
    String DEFAULT_CODE_KEY = "DEFAULT_CODE_KEY_";

    /**
     * 验证码前缀
     */
    String REGISTRY_CODE_KEY = "DEFAULT_CODE_KEY_";

    /**
     * 当前页码
     */
   String PAGE = "page";
    /**
     * 每页显示记录数
     */
    String LIMIT = "limit";
    /**
     * 排序字段
     */
    String ORDER_FIELD = "sidx";
    /**
     * 排序方式
     */
    String ORDER = "order";
    /**
     *  升序
     */
    String ASC = "asc";

    /**
     * 内部用户
     */
    int INNER_USER =1;

    /**
     * 外部用户
     */
    int REGISTRY_USER=2;

    String SECURITY_USERNAME="username";

    String SECURITY_PASSWORD = "password";
}
