using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetProduct : MonoBehaviour, IProduct
{
    [SerializeField]
    private string _productName = "Target";

    public string ProductName { get => _productName; set => _productName = value; }

    public void Initialize()
    {
        gameObject.name = _productName;
    }
}
