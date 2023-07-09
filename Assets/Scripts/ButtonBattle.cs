using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBattle : MonoBehaviour
{
    private void OnMouseDown()
    {
        ClientSocket.SendMessage("1:2");// запрос в Room Online Game
    }
}
