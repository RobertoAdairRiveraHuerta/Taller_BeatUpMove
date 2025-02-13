using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonObject : MonoBehaviour
{
    public static SingletonObject instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
