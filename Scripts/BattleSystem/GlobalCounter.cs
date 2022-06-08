using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalCounter : MonoBehaviour
{
    [HideInInspector] public int defeatCount;

    public void DefeatCounter()
    {
        defeatCount++;
    }
}
