using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
using System.Text;
//using System.Threading.Tasks;
//
//using System.Collections;
using UnityEngine;
//
using System.Net;// Net 
using System.Net.Sockets;// сокеты 
using System.Threading;// поток

public class ClientSocket : MonoBehaviour
{
	[SerializeField] ClientSocketManager clientSocketManager;

	static private Socket Client;// сокет для клиента 
	private IPAddress ip = IPAddress.Parse("127.0.0.1");// тут храним IP 
	private int port = 7890;// изначально порт = 0
	static private Thread thread;// создать поток

	void Start()// Вход - кнопка 
	{

		Client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);// создать сокет 
		if (ip != null)// если IP найден 
		{
			try// обработчик ошибок 
			{
				Client.Connect(ip, port);// подкл. к серверу 

				thread = new Thread(/*delegate ()*/ () =>
				{
					RecvMessage();
				});// создать поток 
				thread.Start();// запуск потока 
			}
			catch (Exception ex)// если из try ничего не сработало 
			{
				Debug.Log("void Start() = catch (Exception ex)");
				Debug.Log(ex);
			}
		}
	}
	// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
	// public static new void SendMessage(string _message)
	public new void SendMessage(string _message)// ф-я с параметром string. отправляет сообщения на сервер 
	{
		string _m = "*" + _message + "`";
		if (thread != null && Client != null)// есть поток && работает. Socket Client;
		{
			if (_m != " " && _m != "")// есть сообщение 
			{
				byte[] buffer = new byte[1024];// создать буфер байтов - массив 
				buffer = Encoding.UTF8.GetBytes(_m);// получаем байт код 
				Debug.Log("SendMessage() = " + _m);
				Client.Send(buffer);// отправляем на сервер
			}
		}
	}
	// <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
	private void RecvMessage()// ф-я принимает сообщения от сервера 
	{
		byte[] buffer = new byte[1024];// массив - буфер байтов 
		for (int i = 0; i < buffer.Length; i++)// чистим буфер
		{
			buffer[i] = 0;// чистим буфер
		}

		for (; ; )// в цикле принимаются все сообщения 
		{
			try// обработчик ошибок 
			{
				Client.Receive(buffer);// приём сообщения 
				string message = Encoding.UTF8.GetString(buffer);// переводим байты в стринг 
				int count = message.IndexOf("\0");// не '`', будет ошибка. ищет ;;;5 - конец сообщения 
				if (count == -1)// ищет ;;;5 - конец сообщения
				{
					continue;// продолжаем цикл  
				}

				string Clear_Message = "";// очищаем сообщение для работы 

				for (int i = 0; i < count; i++)// 
				{
					Clear_Message += message[i];// добавляем буквы реального сообщения 
				}
				for (int i = 0; i < buffer.Length; i++)// чистим буфер
				{
					buffer[i] = 0;// чистим буфер
				}
				Debug.Log("RecvMessage() = " + Clear_Message);
				clientSocketManager.SetMessageStatus(Clear_Message);// Status
			}
			catch (Exception ex)// если из try ничего не сработало 
			{
				//Debug.Log("private void RecvMessage()");
				//Debug.Log(ex);

				Debug.Log("--------- thread.Join() && thread.Abort() ---------");
				thread.Join();// дождаться окончания потока <<<<<<<<<<<<<<<<<<<<
				thread.Abort();// остановить поток <<<<<<<<<<<<<<<<<<<<<<<<<<<<<
			}
		}
	}
	//***********************************************************
	public void SetExit()// Выход. class ClientSocketManager
	{
		//Debug.Log("SetExit()******************************");
		if (Client != null)//  если работает. Socket Client;
		{
			//Debug.Log("= 1.0 =");
			Client.Close();// закрыть. Socket Client;
			//Debug.Log("= 1.1 =");
		}
		if (thread != null)// если есть поток 
		{// https://professorweb.ru/my/csharp/thread_and_files/1/1_14.php 
			//Debug.Log("= 2.0 =");
			clientSocketManager.DebugLog("= 2.0 =");
			/* завершаем поток в ошибке после закрытия игры.
			   RecvMessage() catch (Exception ex) {}. 
				 Иначе поток не закрывает. */
		}
		Application.Quit();// выход. Unity
	}
	public void Button_Exit()//
	{
		if (thread != null && Client != null)// есть поток && работает. Socket Client;
		{
			//Debug.Log("Button_Exit()===================");
			SendMessage("1:0x");// выход. Unity
		}
		else
		{
			SetExit(); 
		}
	}
}
