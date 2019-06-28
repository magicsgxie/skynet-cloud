package cn.uway.skynet.cloud.common.core.exception;

import lombok.NoArgsConstructor;

@NoArgsConstructor
public class UnloginException extends RuntimeException {
    private static final long serialVersionUID = 1L;

    public UnloginException(String message) {
        super(message);
    }

    public UnloginException(Throwable cause) {
        super(cause);
    }

    public UnloginException(String message, Throwable cause) {
        super(message, cause);
    }

    public UnloginException(String message, Throwable cause, boolean enableSuppression, boolean writableStackTrace) {
        super(message, cause, enableSuppression, writableStackTrace);
    }

}