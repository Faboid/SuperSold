﻿namespace SuperSold.Data.Extensions;

public static class TaskExtensions {

    public static Task<T> AsTask<T>(this T obj) => Task.FromResult(obj); 

}

