//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class ButtonBattle : MonoBehaviour
{
	[SerializeField] ClientSocketManager clientSocketManager;

	private void OnMouseDown()
	{   // запрос в Room Online Game
			//ClientSocket.SendMessage("1:2");
			// перенёс в clientSocketManager.ButtonBattleHelper(false);

		clientSocketManager.ButtonBattleHelper(false);
	}
}
