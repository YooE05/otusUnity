using System;
using System.Collections.Generic;
using UnityEngine;


public class Pool<T>
{

    private Queue<T> _pool = new Queue<T>();
    private readonly List<T> _activeItms = new();

    private readonly Func<T> _preloadFunc;
    private readonly Action<T> _getAction;
    private readonly Action<T> _returnAction;

    public Pool(Func<T> preloadFunc, Action<T> getAction, Action<T> returnAction, int preloadCount)
    {
        _preloadFunc = preloadFunc;
        _getAction = getAction;
        _returnAction = returnAction;

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
        T item = _pool.Count > 0 ? _pool.Dequeue() : _preloadFunc();
        _getAction(item);
        _activeItms.Add(item);

        return item;
    }

    public void Return(T item)
    {
        _returnAction(item);
        _pool.Enqueue(item);
        _activeItms.Remove(item);
    }

    public void ReturnAll()
    {
        foreach (T item in _activeItms.ToArray())
        {
            Return(item);
        }
    }

    public List<T> GetActiveItms()
    {
        return _activeItms;
    }
}
