
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new Vector3(0, 0.5f, 0);

    void LateUpdate()
    {
        // ตามตำแหน่ง
        transform.position = player.position + offset;

        // ไม่เอียงตามลูกบอล (ล็อคให้ตรง)
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
    }
}
