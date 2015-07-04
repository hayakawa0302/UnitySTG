using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Chat : MonoBehaviour {
	//Listは可変長配列で配列より若干動作が重い(ほぼ誤差)が便利
	private List<string> messages = new List<string> ();
	private string inputMessage = "";
	private new string name = "hayakawa";
	
	void OnGUI(){
		//ネットワーク接続がされていないときは処理を終了する
		if(!NetworkViewManager.connected){
			return;
		}
		
		GUILayout.Space (40);
		GUILayout.BeginHorizontal (GUILayout.Width (400));
		inputMessage = GUILayout.TextField (inputMessage);
		//Listにストリングを代入する
		if(GUILayout.Button ("Send") || Input.GetKeyUp (KeyCode.Return)){
			NetworkView view = GetComponent<NetworkView>();
			view.RPC ("chatMessage", RPCMode.All, name + ":" + inputMessage);
			//messages.Add (inputMessage);
			//inputMessage = ""; と同じ意味
			inputMessage = string.Empty;
		}
		GUILayout.EndHorizontal ();
		
		List<string> mes = new List<string> (messages);
		mes.Reverse ();
		//Listの中身をマイフレーム描画する
		//for(int i = 0; i < messages.Count; i++){
		//	GUILayout.Label(messages[i]);
		//}
		//Listをfor文で回す時は必ず以下の様に使う(for文はListでは使わない)
		//for文を使う例（iを使って添字番号がわからないと困る時はfor文を使う）
		foreach (string s in mes) {
			GUILayout.Label (s);
		}
		if(messages.Count > 10){
			messages.RemoveAt (0);
		}
		//一番上の行を消す
		//if (GUILayout.Button ("Remove")) {
		//	messages.RemoveAt (0);
		//}
		//全部消す
		//if (GUILayout.Button ("Clear")) {
		//	messages.Clear ();
		//}
		//ラムダ式の書式、上記のforeachと同じ処理になる
		//messages.ForEach (s => GUILayout.Label (s));
		//messages.Where (s => int.Parse (s) >= 10).ToList();
		//10以上の数字があった場合にログを出力する
		//for(int i; i < messages.Count; i++){
		//	if(int.Parse (messages[i]) >= 10){
		//	Debug.Log ();
		//	}
		//}
	}
	//[RPC]->外部からの監視対象になる
	[RPC]
	public void chatMessage(string msg){
		messages.Add (msg);
	}
}
