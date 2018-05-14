using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RemoveBox : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        BoxCollider[] lists = transform.GetComponentsInChildren<BoxCollider>();

        foreach (BoxCollider b in lists)
        {
            Destroy(b);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
