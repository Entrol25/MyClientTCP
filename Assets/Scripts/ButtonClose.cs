//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class ButtonClose : MonoBehaviour
{
	[SerializeField] ClientSocket clientSocket;

	private void OnMouseDown()
	{
		//Debug.Log("class ButtonClose");
		//ClientSocket.SendMessage("1:0x");// выход. Unity
		// SendMessage("1:0x");// выход. Unity
		clientSocket.Button_Exit();
	}
	//public void SetMyMouseDown()
	//{ 
	//    //Debug.Log("SetMyMouseDown()");
	//    ClientSocket.SetExit();
	//}
}
