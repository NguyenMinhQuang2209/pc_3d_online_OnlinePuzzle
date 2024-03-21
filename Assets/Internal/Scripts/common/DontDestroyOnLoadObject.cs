using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoadObject : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

}
