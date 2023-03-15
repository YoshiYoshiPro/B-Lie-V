using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
 
public class Timer : MonoBehaviour {
 
	//　トータル制限時間
	private float totalTime;
	//　制限時間（分）
	[SerializeField]
	private int minute;
	//　制限時間（秒）
	[SerializeField]
	private float seconds;
	[SerializeField] private TextMeshProUGUI[] timerTexts = new TextMeshProUGUI[3];
	[SerializeField] private GamePlayManager gamePlayManager;

	//　前回Update時の秒数
	private float oldSeconds;
	// ゲームが終了したかのフラグ
	private bool isGameEnd = false;
	//フラッシュフラグ
	private bool isActive = false;
	//最初の1回だけ実行するためのフラグ
	private bool isFirst = true;
 
	private void Start () {

		totalTime = minute * 60 + seconds;
		oldSeconds = 0f;
	}
 
	private void Update () {

		if(isGameEnd && isFirst)
		{
			isFirst = false;
			DOVirtual.DelayedCall(1.0f, () => FlashingTimer(isActive)).SetLoops(-1, LoopType.Restart);
		}
		else
		{
			//　制限時間が0秒以下なら何もしない
			if (totalTime <= 0f) {
				return;
			}
			//　一旦トータルの制限時間を計測；
			totalTime = minute * 60 + seconds;
			totalTime -= Time.deltaTime;
	
			//　再設定
			minute = (int) totalTime / 60;
			seconds = totalTime - minute * 60;
	
			//　タイマー表示用UIテキストに時間を表示する
			if((int)seconds != (int)oldSeconds) {
				timerTexts[0].text = minute.ToString("00") + ":" + ((int) seconds).ToString("00");
				timerTexts[1].text = minute.ToString("00") + ":" + ((int) seconds).ToString("00");
				timerTexts[2].text = minute.ToString("00") + ":" + ((int) seconds).ToString("00");
			}
			oldSeconds = seconds;

			//　制限時間以下になったらゲームマスター側の勝利
			if(totalTime <= 0f) {
				gamePlayManager.GameMasterVictory();
			}
		}
	}

	private void FlashingTimer(bool enable)
	{
		foreach(TextMeshProUGUI text in timerTexts)
		{
			text.gameObject.SetActive(enable);
		}

		isActive = !isActive;
	}

	public void GameEnd()
	{
		isGameEnd = true;
	}
}
