﻿namespace Movies.Cache;
public sealed class CacheSignal<T>
{
    private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);
    public Task Wait() => _semaphore.WaitAsync();
    public void Release() => _semaphore.Release();
}
