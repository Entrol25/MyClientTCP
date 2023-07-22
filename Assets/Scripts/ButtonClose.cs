//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class ButtonClose : MonoBehaviour
{
	private void OnMouseDown()
	{
		ClientSocket.SendMessage("1:0x");// выход. Unity
	}
	//public void SetMyMouseDown()
	//{ 
	//    //Debug.Log("SetMyMouseDown()");
	//    ClientSocket.SetExit();
	//}
}
