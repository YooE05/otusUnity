using System;
using System.Collections.Generic;
using UnityEngine;


public class Pool<T>
{

    private Queue<T> pool = new Queue<T>();
    private readonly List<T> activeItms = new();

    private readonly Func<T> preloadFunc;
    private readonly Action<T> getAction;
    private readonly Action<T> returnAction;

    public Pool(Func<T> preloadFunc, Action<T> getAction, Action<T> returnAction, int preloadCount)
    {
        this.preloadFunc = preloadFunc;
        this.getAction = getAction;
        this.returnAction = returnAction;

        if(preloadFunc==null)
        {
            Debug.LogError("Preload func is null");
            return;
        }

        for (int i = 0; i < preloadCount; i++)
        {
            Return(preloadFunc());
        }


    }

    public T Get()
    {
        T item = pool.Count > 0 ? pool.Dequeue() : preloadFunc();
        getAction(item);
        activeItms.Add(item);

        return item;
    }

    public void Return(T item)
    {
        returnAction(item);
        pool.Enqueue(item);
        activeItms.Remove(item);
    }

    public void ReturnAll()
    {
        foreach (T item in activeItms.ToArray())
        {
            Return(item);
        }
    }

    public List<T> GetActiveItms()
    {
        return activeItms;
    }
}
