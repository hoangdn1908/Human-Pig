using UnityEngine;

public class DetectZone : MonoBehaviour
{
    private PigThrowTheBox pigThrowTheBox;

    private void Awake()
    {
        pigThrowTheBox = GetComponentInParent<PigThrowTheBox>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && pigThrowTheBox != null) // Kiểm tra nếu đối tượng va chạm có tag là "Player"
        {
            pigThrowTheBox.SetPlayerInRange(true); // Gọi phương thức để đặt trạng thái người chơi trong phạm vi
           
            Debug.Log("Player entered detection zone");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && pigThrowTheBox != null) // Kiểm tra nếu đối tượng rời khỏi vùng phát hiện có tag là "Player"
        {
            pigThrowTheBox.SetPlayerInRange(false); // Gọi phương thức để đặt trạng thái người chơi không còn trong phạm vi
            
            Debug.Log("Player exited detection zone");
        }
    }
}
