using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasMover : MonoBehaviour
{
    [SerializeField] private GameObject Cameraa;
    void Update()
    {
        if (Cameraa != null)
        {
            transform.LookAt(Cameraa.transform.position);
            transform.rotation *= Quaternion.Euler(0, 180f, 0);
        } else
        {
            Cameraa = FindAnyObjectByType<Camera>().gameObject;
        }
    }
}
