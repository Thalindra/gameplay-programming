using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transmitter : MonoBehaviour
{
    public bool bolActivated = false;
    protected GameObject objPlayer;
    protected GameObject objHook;
    protected void FindPlayer()
    {
        objPlayer = GameObject.FindWithTag("Player");
    }

    protected void FindHook()
    {
        objHook = GameObject.FindWithTag("Hook");
    }
}
