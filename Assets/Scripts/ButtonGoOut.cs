//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class ButtonGoOut : MonoBehaviour
{
	[SerializeField] ClientSocket clientSocket;

	private void OnMouseDown()
	{
		clientSocket.SendMessage("2:1");// запрос на выход в Accaunt
	}
}
