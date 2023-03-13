using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerJumping : MonoBehaviour
{
    [SerializeField, Tooltip("ジャンプの力")]
    private float jumpForce = 10f;

    [SerializeField, Tooltip("大ジャンプの力")]
    private float greaterJumpForce = 17.0f;

    [SerializeField, Tooltip("接地判定用のレイヤーマスク")]
    private LayerMask groundMask;

    [SerializeField, Tooltip("レイキャストの長さ")]
    private float raycastLength = 0.8f;
    
    private Rigidbody playerRigidbody;

    private void Start() 
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    // ジャンプできるかどうかを判定する関数
    private bool CanJump()
    {
        // 接地している場合にジャンプできる
        return (Physics.Raycast(transform.position, Vector3.down, raycastLength, groundMask));
    }

    void Update()
    {
        // Xボタンが押され、ジャンプ可能な場合、ジャンプする
        if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.LTouch) && CanJump())
        {
            // 上方向に力を加えてジャンプさせる
            playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        //ゲームプレイ中は大ジャンプを可能にする
        if(SceneManager.GetActiveScene().name == "GamePlatform" && OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch))
        {
            //グリップボタン + Xボタンで大ジャンプ
            if(OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.LTouch) && CanJump())
            {
                playerRigidbody.AddForce(Vector3.up * greaterJumpForce, ForceMode.Impulse);
            }
        }
    }

    /// <summary>
    /// ゲーム開始後にプレイヤーのサイズ変更のため、レイキャストの長さを変更
    /// </summary>
    public void ChangeRayCastLength()
    {
        raycastLength = 1.8f;
    }
    
}
