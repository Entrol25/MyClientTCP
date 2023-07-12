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
    static private Thread th;// создать поток

    

    void Start()// Вход - кнопка 
    {
        Client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);// создать сокет 
        if (ip != null)// если IP найден 
        {
            try// обработчик ошибок 
            {
                Client.Connect(ip, port);// подкл. к серверу 
                th = new Thread(delegate () 
                { 
                    RecvMessage(); 
                });// создать поток 
                th.Start();// запуск потока 
            }
            catch (Exception ex)// если из try ничего не сработало 
            {
                //Debug.Log("void Start() = catch (Exception ex)");
                //Debug.Log(ex);
            }
        }
    }
    // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
    public static new void SendMessage(string _message)// ф-я с параметром string. отправляет сообщения на сервер 
    {
        string _m = "*" + _message + "`";
        if (_m != " " && _m != "")// есть сообщение 
        {
            byte[] buffer = new byte[1024];// создать буфер байтов - массив 
            buffer = Encoding.UTF8.GetBytes(_m);// получаем байт код 
            Debug.Log("SendMessage() = " +_m);
            Client.Send(buffer);// отправляем на сервер
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
                Debug.Log("private void RecvMessage()");
                Debug.Log(ex);
            }
        }
    }
    //***********************************************************
    public static void SetExit()// Выход - меню
    {
        if (th != null)// если есть поток 
        {
            th.Abort();// остановить поток 
        }
        if (Client != null)//  если работает клиент
        {
            Client.Close();// закрыть клиент 
        }
        //Application.Exit();// выход 
    }
}
