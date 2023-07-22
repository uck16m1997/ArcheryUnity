using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ArrowProduct : MonoBehaviour, IProduct
{
    [SerializeField]
    private string productName = "Arrow";
    [SerializeField]
    private float duration = 5f;
    public string ProductName { get => productName; set => productName = value; }

    public void Initialize()
    {
        gameObject.name = productName;
        Destroy(gameObject, duration);
    }
}