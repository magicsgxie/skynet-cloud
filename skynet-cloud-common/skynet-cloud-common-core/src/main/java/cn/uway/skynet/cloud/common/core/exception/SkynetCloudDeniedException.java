package cn.uway.skynet.cloud.common.core.exception;

/**
 * Created by uway on 18-9-13.
 * 403 授权拒绝
 */
public class SkynetCloudDeniedException extends RuntimeException {

    private static final long serialVersionUID = 1L;

    public SkynetCloudDeniedException() {
    }

    public SkynetCloudDeniedException(String message) {
        super(message);
    }

    public SkynetCloudDeniedException(Throwable cause) {
        super(cause);
    }

    public SkynetCloudDeniedException(String message, Throwable cause) {
        super(message, cause);
    }

    public SkynetCloudDeniedException(String message, Throwable cause, boolean enableSuppression, boolean writableStackTrace) {
        super(message, cause, enableSuppression, writableStackTrace);
    }

}