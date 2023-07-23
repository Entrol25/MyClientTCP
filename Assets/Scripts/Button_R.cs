//using System.Collections;
//using System.Collections.Generic;
//using UnityEditor.VersionControl;
using UnityEngine;

public class Button_R : MonoBehaviour
{
	[SerializeField] Player player;

	[SerializeField] ClientSocketManager clientSocketManager;
	string message = "";

	private void OnMouseDown()
	{
		clientSocketManager.SetMove("R", true);
	}
	private void OnMouseUp()
	{//Debug.Log("-------");
	 //ClientSocketManager.SetMove("0", false);
		clientSocketManager.SetMove("0", false);
	}
	//private void OnMouseDown()
	//{
	//    //Debug.Log("+++++++");
	//    message = player.GetVectorRun("R");// vector из payer
	//    Debug.Log(message);// данная точка по vector3.x и скорость
	//    //ClientSocket.SetSendMessage(message);
	//    ClientSocketManager.SetRoom(message);
	//}
}
