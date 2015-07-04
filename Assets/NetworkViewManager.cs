using UnityEngine;
using System.Collections;

public class NetworkViewManager : MonoBehaviour 
{
	//接続状況
	public static bool connected = false;
	//saver IP 
	public string connectionIP="10.25.32.212";
	//接続ポート番号
	public int portNumber = 8080;

	void OnGUI()
	{
		GUILayout.Label("Connections" + Network.connections.Length.ToString ());

		if (connected) {
			GUILayout.Space (40);
			GUILayout.BeginHorizontal (GUILayout.Width (400));

			if (GUILayout.Button ("Disconnect")) {
				//せつだん
				Network.Disconnect ();
			}
		} else {
				//せつぞく
			if (GUILayout.Button ("Connect")) {
				Network.Connect (connectionIP, portNumber);
			}
			if(GUILayout.Button("Server"))
			{
				Network.InitializeServer(20,portNumber);
			}
		}
	}

	void OnPlayerConnectied(NetworkPlayer player)
	{
		Debug.Log ("Connected from" + player.ipAddress + ":" + player.port);
		connected = true;
	}

	void OnServerInitialized()
	{
		Debug.Log ("Server initialized and ready");
		connected = true;
	}

	//さーばーにせつぞくできたら
	void OnConnectedToServer()
	{
		Debug.Log ("Connected Sever");
		connected = true;
	}
	//さーばーにせつぞくできなかったら
		void OnDisconnectedFromServer(NetworkDisconnection info)
	{
		Debug.Log ("DisConnected Sever");
		connected=false;
	}
}
