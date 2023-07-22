using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetProduct : MonoBehaviour, IProduct
{
    [SerializeField]
    private string productName = "Target";

    public string ProductName { get => productName; set => productName = value; }

    public void Initialize()
    {
        gameObject.name = productName;
    }
}
