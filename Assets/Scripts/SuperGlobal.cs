using UnityEngine;
using System.Collections;

public class SuperGlobal : MonoBehaviour
{

    public static SuperGlobal that;

    public static bool isDemo = false;

    void Awake()
    {
        that = this;
        DontDestroyOnLoad(that);
    }

    void Update()
    { }
}
