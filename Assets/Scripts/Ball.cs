using System.Collections;
using UnityEditor.ShaderGraph.Internal;
using UnityEditorInternal;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody rb;
    private bool hasHitGround = false;
    private SpawnManager sm;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sm = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();

        Vector3 direction = CalculateLaunchVector(GetTarget(true));
        rb.AddForce(direction, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !hasHitGround)
        {
            // Stop current momentum
            rb.linearVelocity = new Vector3();
            // Send ball back over net
            Vector3 direction = CalculateLaunchVector(GetTarget(false));
            rb.AddForce(direction, ForceMode.Impulse);
            sm.RegisterHit();
        } else if (collision.gameObject.CompareTag("Ground"))
        {
            hasHitGround = true;
            Invoke("RemoveBall", 5);
            if (transform.position.z < 0)
            {
                sm.EndGame();
            }
        }
    }

    private Vector3 CalculateLaunchVector(Vector3 target)
    {
        Vector3 launchVector = target - transform.position;

        float dz = -transform.position.z / launchVector.z;
        float y = 6;
        float g = Physics.gravity.y;

        /*
         * t = 0: p = spawn
         * t = x: p = target
         * t = x': p = (?, 5, 0)
         * dz = (0 - spawn.z) / (target.z - spawn.z)
         * x' = x * dz
         * 
         * y = vy * (x * dz) + 1/2 * g * (x * dz)^2
         * 0 = vy * x + 1/2 * g * x^2 = x(vy + g*x/2)
         * 0 = vy + gx/2 => vy = -gx / 2
         * 
         * y = 
         * 
         */

        float time = Mathf.Sqrt(2 * y / (dz * g * (dz - 1)));
        float vy = -g * time / 2;



        return new Vector3(launchVector.x / time, vy, launchVector.z / time) * rb.mass;
    }

    private Vector3 GetTarget(bool towardsPlayer)
    {
        float xLoc = Random.Range(-4.5f, 4.5f);
        float zLoc = towardsPlayer ? Random.Range(-9, -1) : Random.Range(1, 9);
        return new Vector3(xLoc, 0, zLoc);
    }

    private void RemoveBall()
    {
        Destroy(gameObject);
    }
}
