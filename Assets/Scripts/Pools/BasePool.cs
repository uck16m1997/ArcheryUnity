using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public abstract class BasePool : MonoBehaviour
{

    private IObjectPool<IProduct> objectPool;

    [SerializeField]
    private bool collectionCheck = true;
    [SerializeField]
    private int defaultCapacity = 1;
    [SerializeField]
    private int maxSize = 20;

    private void Awake()
    {
        objectPool = new ObjectPool<IProduct>(CreateProduct,
        OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject,
        collectionCheck, defaultCapacity, maxSize);
    }

    public abstract IProduct CreateProduct();
    public abstract void OnReleaseToPool(IProduct product);
    public abstract void OnGetFromPool(IProduct product);
    public abstract void OnDestroyPooledObject(IProduct product);

}

