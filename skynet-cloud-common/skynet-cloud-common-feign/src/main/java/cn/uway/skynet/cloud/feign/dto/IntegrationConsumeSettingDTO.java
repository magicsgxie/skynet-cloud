package cn.uway.skynet.cloud.feign.dto;

import lombok.Data;

@Data
public class IntegrationConsumeSettingDTO {
    private Integer couponStatus;

    private Integer useUnit;

    private Integer maxPercentPerOrder;
}
