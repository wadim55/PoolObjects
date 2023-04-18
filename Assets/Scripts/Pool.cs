using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class Pool<T> where T: MonoBehaviour
{
    public T prefab { get;  }
    public bool autoPlus { get; set; } //флаг, надо ли добавить еще одну ячейку хранения в пул
    public Transform container { get;  }

    private List<T> poolList;

    public Pool(T prefab, int Count)
    {
        this.prefab = prefab;
    }

   
    
    public Pool(T prefab, int Count, Transform container)
    {
        this.prefab = prefab;
        this.container = container;
        CreatePool(Count);
    }

   
    
    private void CreatePool(int count)
    { 
        poolList = new List<T>();
        for (var i = 0; i < count; i++)
        {
            this.CreateObject();
        }
    }

  
    
    private T CreateObject(bool isActiveByDefault = false)
    {
        var createObject = Object.Instantiate(prefab, this.container);
        createObject.gameObject.SetActive(isActiveByDefault);
        this.poolList.Add(createObject);
        return createObject;
    }

    public bool HasFreeElement(out T element)
    {
        foreach (var mono in poolList)
        {
            if (mono.gameObject.activeInHierarchy) continue;
            element = mono;
            mono.gameObject.SetActive(true);
            return true;
        }
        element = null;
        return false;
    }

    public T GetFreeElement()
    {
        if (this.HasFreeElement(out var element)) return element;
        if (this.autoPlus) return this.CreateObject(true);
        throw new Exception($"нельзя добавить элемент {typeof(T)}");

    }
    
}
