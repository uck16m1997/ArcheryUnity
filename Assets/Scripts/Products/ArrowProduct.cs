using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ArrowProduct : MonoBehaviour, IProduct
{
    [SerializeField]
    private string _productName = "Arrow";
    [SerializeField]
    private float _duration = 5f;
    public string ProductName { get => _productName; set => _productName = value; }

    public void Initialize()
    {
        gameObject.name = _productName;
        Destroy(gameObject, _duration);
    }
}