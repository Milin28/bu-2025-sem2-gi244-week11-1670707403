using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 5f;
    public Transform focalPoint;

    private Rigidbody rb;

    [Header("Power Up")]
    public bool hasPowerUp = false;
    public float powerUpDuration = 20f;
    public GameObject powerIndicator;

    private InputAction smashAction;
    private InputAction moveAction;
    private InputAction breakAction;

    private Coroutine powerRoutine;
    public float stunDuration = 5f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        moveAction = InputSystem.actions.FindAction("Move");
        smashAction = InputSystem.actions.FindAction("Smash");
        breakAction = InputSystem.actions.FindAction("Break");

        // ปิดวงแหวนตอนเริ่ม
        if (powerIndicator != null)
            powerIndicator.SetActive(false);
    }

    void FixedUpdate()
    {
        Vector2 move = moveAction.ReadValue<Vector2>();

        // เดินตามกล้อง
        Vector3 dir = focalPoint.forward * move.y;
        rb.AddForce(dir * speed, ForceMode.Force);

        // ปุ่มเบรก
        if (breakAction.IsPressed())
        {
            rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, Vector3.zero, 0.2f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerUp)
        {
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();

            if (enemyRb != null)
            {
                Vector3 dir = collision.transform.position - transform.position;
                enemyRb.AddForce(dir.normalized * 8f, ForceMode.Impulse);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            ActivatePowerUp();
            Destroy(other.gameObject);
        }
    }

    void ActivatePowerUp()
    {
        hasPowerUp = true;

        // เปิดวงแหวน
        if (powerIndicator != null)
            powerIndicator.SetActive(true);

        // รีเซ็ตเวลา
        if (powerRoutine != null)
            StopCoroutine(powerRoutine);

        powerRoutine = StartCoroutine(PowerUpCountdown());
    }

    IEnumerator PowerUpCountdown()
    {
        yield return new WaitForSeconds(powerUpDuration);

        hasPowerUp = false;

        // ปิดวงแหวน
        if (powerIndicator != null)
            powerIndicator.SetActive(false);

        powerRoutine = null;
    }
    public void ActivateStun()
    {
        StartCoroutine(StunEnemies());
    }

    IEnumerator StunEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<Enemy>()?.SetStunned(true);
        }

        yield return new WaitForSeconds(stunDuration);

        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
                enemy.GetComponent<Enemy>()?.SetStunned(false);
        }
    }
}