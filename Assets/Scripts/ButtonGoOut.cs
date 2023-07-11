//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class ButtonGoOut : MonoBehaviour
{
    private void OnMouseDown()
    {
        ClientSocket.SendMessage("2:1");// запрос на выход в Accaunt
    }
}
