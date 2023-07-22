using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowFactory : BaseFactory
{
    [SerializeField]
    private ArrowProduct _arrowPrefab;

    public ArrowProduct ArrowPrefab
    {
        get => _arrowPrefab;
    }

    public static ArrowFactory Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public override GameObject GetProduct(Vector3 position, Quaternion rotation)
    {
        GameObject instance = Instantiate(_arrowPrefab.gameObject, position, rotation);

        ArrowProduct arrow = instance.GetComponent<ArrowProduct>();
        arrow.Initialize();

        return instance;
    }
}
