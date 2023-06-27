using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_0 : MonoBehaviour
{
    //private void OnMouseDown()
    //{
    //    Debug.Log("OnMouseDown()");
    //}

    public void SetMyMouseDown()
    {
        //Debug.Log("SetMyMouseDown()");
        ClientSocket.SetSendMessage(" * : 3,025f ");
    }
}
