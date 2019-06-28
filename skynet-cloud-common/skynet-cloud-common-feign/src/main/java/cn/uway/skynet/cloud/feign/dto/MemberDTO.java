package cn.uway.skynet.cloud.feign.dto;

import lombok.Data;

import java.io.Serializable;
import java.util.Date;

@Data
public class MemberDTO implements Serializable {
    private static final long serialVersionUID = 1L;

    /**
     *
     */
    private Long id;
    /**
     *会员等级，
     */
    private Long memberLevelId;
    /**
     * 昵称
     */
    private String nickname;

    /**
     * 头像
     */
    private String icon;
    /**
     * 性别：0->未知；1->男；2->女
     */
    private Integer gender;
    /**
     * 积分
     */
    private Integer integration;
    /**
     * 成长值
     */
    private Integer growth;
    /**
     * 剩余抽奖次数
     */
    private Integer luckeyCount;

    /**
     * 魅力分数
     */
    private Integer totalCharmValue;

    /**
     * 贡献分数
     */
    private Integer totalContributeValue;

    /**
     * 财富分数
     */
    private Integer totalWealthValue;

    /**
     * 粉丝数量
     */
    private Integer fansCount;

    /**
     * 关注人数
     */
    private Integer followCount;

    /**
     * 购买的消息数量
     */
    private Integer buyMsgCount;

    /**
     * 消费的消息数量
     */
    private Integer consumerMsgCount;

    /**
     * 是否隐藏粉丝
     */
    private String hiddenFans;

    /**
     * 是否退出榜单
     */
    private String exitBoard;

    /**
     * 是否隐藏年龄
     */
    private String hiddenAge;


}