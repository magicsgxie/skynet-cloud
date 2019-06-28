package cn.uway.skynet.cloud.feign.factory;


//import cn.uway.skynet.cloud.feign.RemoteIntegerationConsumeSettingService;
//import cn.uway.skynet.cloud.feign.fallback.RemoteIntegerationConsumeSettingServiceImpl;
//import feign.hystrix.FallbackFactory;
//import org.springframework.stereotype.Component;
//
//
//@Component
//public class RemoteIntegerationConsumeSettingServiceFallbackFactory  implements FallbackFactory<RemoteIntegerationConsumeSettingService> {
//    @Override
//    public RemoteIntegerationConsumeSettingService create(Throwable throwable) {
//        RemoteIntegerationConsumeSettingServiceImpl remoteLogServiceFallback = new RemoteIntegerationConsumeSettingServiceImpl();
//        remoteLogServiceFallback.setCause(throwable);
//        return remoteLogServiceFallback;
//    }
//}
