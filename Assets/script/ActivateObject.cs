using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateObject : MonoBehaviour
{
    public GameObject obj;

    public void Activate()
    {
        obj.SetActive(!obj.activeSelf);
    }
}
