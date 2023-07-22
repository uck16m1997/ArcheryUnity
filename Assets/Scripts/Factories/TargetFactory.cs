using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFactory : BaseFactory
{
    [SerializeField]
    private TargetProduct _targetPrefab;

    public TargetProduct TargetPrefab
    {
        get => _targetPrefab;
    }

    public static TargetFactory Instance;

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
        GameObject instance = Instantiate(_targetPrefab.gameObject, position, rotation);

        TargetProduct target = instance.GetComponent<TargetProduct>();
        target.Initialize();

        return instance;
    }
}