using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseFactory : MonoBehaviour
{
    public abstract GameObject GetProduct(Vector3 position, Quaternion rotation);


}