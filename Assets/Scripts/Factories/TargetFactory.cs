using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFactory : BaseFactory
{
    [SerializeField]
    private TargetProduct targetPrefab;

    public TargetProduct TargetPrefab
    {
        get => targetPrefab;
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
        GameObject instance = Instantiate(targetPrefab.gameObject, position, rotation);

        TargetProduct target = instance.GetComponent<TargetProduct>();
        target.Initialize();

        return instance;
    }
}