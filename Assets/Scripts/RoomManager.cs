//using System;
//using System.Collections;
//using System.Collections.Generic;
//using TMPro;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
	[SerializeField] Player player;

	string tcp_Message = "";

	int countUpdate = 0;

	void Start()
	{
		//StartCoroutine(FPS());// FPS !!!!!!!!!!!!!!!!
	}
	void FixedUpdate()
	{
		//countUpdate++;// FPS !!!!!!!!!!!!!!!!!!!!!!!!
	}
	//------------- FPS --------------------------
	//void FPSLoop()
	//{
	//    StartCoroutine(FPS());
	//}
	//IEnumerator FPS()// StartCoroutine 
	//{
	//    countUpdate = 0;
	//    yield return new WaitForSeconds(1);
	//    fpsMeshPro.text = "fps " + countUpdate;
	//    //Debug.Log(countUpdate);
	//    FPSLoop();
	//}
	//------------- FPS --------------------------
	//****************************************
	// ф-я принимает сообщения от ClientSocketManager
	public void TCPMessage(string _tcp_Message) 
	{
		tcp_Message = _tcp_Message;
		int _slash = 0;
		for (int i = 0; i < tcp_Message.Length; i++)
		{
			if (tcp_Message[i] == '/')
			{
				if (_slash == 1 && tcp_Message[i + 1] == 'R')// Button_R 
				{
					player.SetRun("R");
				}
				if (_slash == 2)// vector3
				{
					int _colon = 0; string _t = ""; string _p = "";

					for (int j = i + 1; j < tcp_Message.Length; j++)
					{
						if (tcp_Message[j] != '`')
						{
							if (tcp_Message[j] == ':')
							{
								_colon = 1; j++;
							}
							if (_colon == 0)
							{
								if (tcp_Message[j] != '.')// !=
								{ _t = _t + tcp_Message[j]; }
								//else if (tcp_Message[j] == '.')// == 
								//{ _t = _t + ','; }// замена запятой на точку
							}
							else if (_colon == 1)
							{
								if (tcp_Message[j] != '.')// !=
								{ _p = _p + tcp_Message[j]; }
								//else if (tcp_Message[j] == '.')// == 
								//{ _p = _p + ','; }// замена запятой на точку
							}
						}
						if (tcp_Message[j] == '`')
						{
							//Debug.Log("RoomManager = " + tcp_Message);
							float _tr = float.Parse(_t);
							float _pt = float.Parse(_p);
							player.SetTransformPoint(_tr, _pt);
							break;
						}
					}
				}
				_slash++;
			}
		}
	}
}
