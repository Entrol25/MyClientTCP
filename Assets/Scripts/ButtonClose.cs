//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class ButtonClose : MonoBehaviour
{
    private void OnMouseDown()
    {
        ClientSocket.SetExit();
    }
    //public void SetMyMouseDown()
    //{ 
    //    //Debug.Log("SetMyMouseDown()");
    //    ClientSocket.SetExit();
    //}
}
