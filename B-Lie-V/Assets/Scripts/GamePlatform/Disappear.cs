using UnityEngine;

public class Disappear : MonoBehaviour
{
	[SerializeField] private AudioClip explosionSE;
	private AudioSource audioSource;
	private void Start() 
	{
		audioSource = GetComponent<AudioSource>();
	}

	/// <summary>
	/// 衝突した時
	/// </summary>
	/// <param name="collision"></param>
	void OnCollisionEnter(Collision collision)
	{
		// 衝突した相手にPlayerタグが付いているとき
		if (collision.gameObject.CompareTag("Bottom") || collision.gameObject.CompareTag("Player"))
		{
			audioSource.PlayOneShot(explosionSE);
			// 0.01秒後に消える
			Destroy(gameObject, 0.01f);
		}
	}
}