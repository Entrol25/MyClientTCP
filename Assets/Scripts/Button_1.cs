using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_1 : MonoBehaviour
{
    public void SetMyMouseDown()
    {
        //Debug.Log("SetMyMouseDown()");
        ClientSocket.SetExit();
    }
}
