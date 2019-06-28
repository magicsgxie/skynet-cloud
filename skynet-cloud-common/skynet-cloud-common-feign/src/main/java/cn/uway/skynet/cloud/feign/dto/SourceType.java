package cn.uway.skynet.cloud.feign.dto;

import lombok.Getter;

/**
 * 0->购物；1->管理员修改 2-社区发贴 3-社区评论 4-帖子收藏 5-关注 6-粉丝 7-商品评论, 8-分享,
 */

@Getter
public enum SourceType {
    POST(2,"社区发贴"),
    POST_COMMENT(3, "社区评论"),
    POST_COLLECTION(6, "帖子收藏"),
    FOLLOW(5, "关注"),
    FANS(6, "粉丝"),
    ADMIN_UPDATE(1, "管理员修改"),
    SHOP(0, "购物"),
    PRODUCT_COMMENT(7, "商品评论"),
    SHARE(8, "分享");

    private int value;
    private String text;

    SourceType(Integer value, String text) {
        this.value = value;
        this.text = text;
    }




}
