using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    [SerializeField] float timeTillDestroy = 3f;

    private void Start()
    {
        Destroy(gameObject, timeTillDestroy);
    }
}
