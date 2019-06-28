package cn.uway.skynet.cloud.auth.dto;

import lombok.AllArgsConstructor;
import lombok.Data;

import java.util.Collections;
import java.util.List;

@Data
@AllArgsConstructor
public class Page<T> {
    private List<T> records;

    private long total;

    private long size;

    private long current;

    public Page() {
        this.records = Collections.emptyList();
        this.total = 0L;
        this.size = 10L;


    }

    public Page(long current, long size) {
        this.records = Collections.emptyList();
        this.current = current;
        this.size = size;

    }
}
