using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//
using TMPro;//
						//using UnityEditor.EditorTools;
using System;

public class ClientSocketManager : MonoBehaviour
{
	[SerializeField] RoomManager roomManager;
	[SerializeField] ClientSocket clientSocket;
	[SerializeField] Player player;
	[SerializeField] Data data;

	[SerializeField] private TextMeshProUGUI netMeshPro;
	[SerializeField] private TextMeshProUGUI tcpMeshPro;
	[SerializeField] private TextMeshProUGUI fpsMeshPro;
	[SerializeField] private TextMeshProUGUI debugMeshPro;

	[SerializeField] private GameObject buttonBattle;
	[SerializeField] private GameObject buttonGoOut;
	[SerializeField] private GameObject buttonClose;
	[SerializeField] private GameObject buttonR;
	[SerializeField] private GameObject buttonL;

	//static private char[] status = {'-','-'};// 0 connect, 1 connect, 2 room onliyne game
	static private string status = "-:-";// 0 connect, 1 connect, 2 room onliyne game
	string vector;
	int id = 0;
	private string button = "0";
	private bool buttonState = false;
	private bool getMessage = false;
	string message = "";
	string tcp_Message = "";
	string debug_Message = "";

	bool freeze = false;
	[SerializeField] private int countMess = 4;//0 - 25 раз/c. 3 - 12 раз/c. 4 - 10 раз/c. 8 - 5 раз/c
	int countFreeze = 0;// заморозка 
	int countSendMessage = 0;// SendMessage
	float countFixedUpdate = 0.0f;//


	void Start()
	{
		buttonBattle.SetActive(false);
		buttonGoOut.SetActive(false);
		buttonClose.SetActive(false);
		buttonR.SetActive(false);
		buttonL.SetActive(false);
		//
		fpsMeshPro.text = "";
	}
	private void FixedUpdate()
	{
		Satus();
		netMeshPro.text = status;//
		tcpMeshPro.text = TcpMessage(tcp_Message);// убираем лишние знаки для tcpMeshPro
		debugMeshPro.text = debug_Message;
		Move();// ----------------------------
	}
	private string TcpMessage(string _tcp_Message)// убираем лишние знаки для tcpMeshPro
	{
		string _tcpM = "";
		for (int i = 0; i < _tcp_Message.Length; i++)
		{
			if (_tcp_Message[i] != '`')
			{
				_tcpM += _tcp_Message[i];
			}
			else if (_tcp_Message[i] == '`')
			{
				_tcpM += _tcp_Message[i];
				break;
			}
		}
		return _tcpM;
	}
	private void Move()// ----------------------------
	{
		if (buttonState == true)// test
		{
			countFixedUpdate++;
			if (countFixedUpdate >= 51)
			{
				//float _val = countFixedUpdate / countSendMessage;
				fpsMeshPro.text = Convert.ToString(countSendMessage);
				countFixedUpdate = 0;
				countSendMessage = 0;
			}
		}
		//-------------------------
		if (freeze == true)// заморозка 
		{
			countFreeze++;    // 4
			if (countFreeze >= countMess)//  4 - 10 раз/c. 8 - 5 раз/c
			{
				countFreeze = 0;
				freeze = false;
			}
		}
		else if (freeze == false)// SendMessage
		{
			if (buttonState == true)//  && getMessage == true
			{
				//getMessage = false;// !!!!!!!!!!!!!!!!!!!!!
				if (button == "R")
				{
					vector = player.GetVectorRun(button);// !!!!!!!!!!!!!!!!!!!!!
					//Debug.Log("Move() / " + vector);
					//         2:2      /    id    /    кнопка    /    vector
					message = status + "/" + id + "/" + button + "/" + vector;
					//Debug.Log("Move() " + message);
					clientSocket.SendMessage(message);// !!!!!!!!!!!!!!!!!!!!!

					countSendMessage++;
					freeze = true;
				}
				else if (button == "L") { }
			}
			else if (buttonState == false) { }
		}
	}
	private void Satus()// FixedUpdate()
	{
		if (status == "1:1")
		{
			buttonBattle.SetActive(true);// <<<<<<
			buttonGoOut.SetActive(false);
			buttonClose.SetActive(true);// <<<<<<
			buttonR.SetActive(false);
			buttonL.SetActive(false);
		}
		else if (status == "2:2")
		{
			buttonBattle.SetActive(false);
			buttonGoOut.SetActive(true);// <<<<<<
			buttonClose.SetActive(false);
			buttonR.SetActive(true);// <<<<<<
			buttonL.SetActive(true);// <<<<<<
		}
		else
		{
			buttonBattle.SetActive(false);
			buttonGoOut.SetActive(false);
			buttonClose.SetActive(false);
			buttonR.SetActive(false);
			buttonL.SetActive(false);
		}
	}
	//***********************************************************************************
	// ф-я принимает сообщения от ClientSocket 
	public void SetMessageStatus(string _tcp_Message)
	{                                           // в цикле принимаются все сообщения 
		tcp_Message = _tcp_Message;
		//                                            !=
		if (_tcp_Message[0] == '*' && _tcp_Message[4] != '`')// начало конец сообщения
		{//  отправлять Vector3 на сервер 
			if (_tcp_Message[1] == '2' && _tcp_Message[3] == '2')
			{
				getMessage = true;// !!!!!!!!!!!!!!!!!!!!!
				roomManager.TCPMessage(_tcp_Message);
			}
			else if (_tcp_Message[1] == '0' && _tcp_Message[3] == '0')// выход. Unity
			{
				Debug.Log("SetExit()------------------------------------");
				if (_tcp_Message[4] == 'x')// выход. Unity
				{
					status = "0:0";
					clientSocket.SetExit();// выход. Unity
				}
			}
		}//                                           ==
		if (_tcp_Message[0] == '*' && _tcp_Message[4] == '`')// начало конец сообщения
		{
			// подключились к int ServerStart::StartServer() / 0:0  
			if (_tcp_Message[1] == '0' && _tcp_Message[3] == '0')
			{
				status = "0:0";
				Debug.Log("Loading --------");
				string _login = data.GetLogin();
				clientSocket.SendMessage("0:1" + _login);// запрос в Accaunt
			}    // подключились к void Client::SendMessageToClient
			else // (int ID, SOCKET* Connections) / 1:1 
			if (_tcp_Message[1] == '1' && _tcp_Message[3] == '1')
			{
				status = "1:1";
				Debug.Log("Accaunt --------");
				//  --> Satus() --> class ButtonBattle -->
				// --> ClientSocket.SendMessage("1:2");// запрос в Room Online Game
			}
			else // подключились к Room Online Game?????????????????!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! 
			if (_tcp_Message[1] == '2' && _tcp_Message[3] == '2')// Room Online Game, длобит сюда !!!!!!!!
			{
				status = "2:2";
				inBattle = true;// Stop Coroutine. ожидание игроков закончено
				Debug.Log("Room Online Game --------");// длобит сюда !!!!!!!!!!!!!!!!!!!!!!!!!
				clientSocket.SendMessage("2:2");// подтверждение в Room Online Game
			}
		}
	}
	public void SetMove(string _button, bool _buttonState)// Button_R
	{
		button = _button; buttonState = _buttonState;
		//Debug.Log("public void SetMove()");
	}
	bool inBattle = false;
	public void ButtonBattleHelper(bool _active)// class ButtonBattle
	{
		if (_active == false)
		{
			// запрос в Room Online Game
			//ClientSocket.SendMessage("1:2");
			buttonBattle.SetActive(false);
			StartCoroutine(InBattle());// StartCoroutine 
		}
	}
	IEnumerator InBattle()// Coroutine 
	{
		clientSocket.SendMessage("1:2");// запрос в Room Online Game
		yield return new WaitForSeconds(10);
		//ClientSocket.SendMessage("1:2");// запрос в Room Online Game
		if (inBattle == false)
		{// рекурсия
			StartCoroutine(InBattle());// StartCoroutine 
		}

	}
	//static public void Room(string _vector3)// Button_R
	//{//             status
	//    string _s = "2:2:";
	//    _s = _s + _vector3;
	//    ClientSocket.SetSendMessage(_s);
	//}
	//static public void SetRoom(string _vector3)// Button_R
	//{
	//    Room(_vector3);
	//}

	public void DebugLog(string _debug)// ClientSocket
	{
		debug_Message = _debug;
	}
}
