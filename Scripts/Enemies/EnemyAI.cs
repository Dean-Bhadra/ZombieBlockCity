using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    // AI Settings
    public float lookRadius = 10f;
    public float attackRange = 1.4f; // This should match the Sphere Collider's radius
    public float attackCooldown = 2f;
    public int attackDamage = 10;

    // Private Variables
    private Transform player;
    private NavMeshAgent agent;
    private Health playerHealth;
    private float lastAttackTime = 0f;

    // This new variable tracks if the player is in the attack zone
    private bool isPlayerInAttackRange = false;

    void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
            playerHealth = playerObject.GetComponent<Health>();
        }

        agent = GetComponent<NavMeshAgent>();
        // Set the stopping distance to be slightly less than the attack range
        // This ensures the zombie stops just outside of physical contact range
        agent.stoppingDistance = 1.5f;
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(player.position, transform.position);

        // --- Chasing Logic ---
        // The zombie will always try to chase the player if they are within the lookRadius
        if (distance <= lookRadius)
        {
            agent.SetDestination(player.position);
        }

        // --- Attacking Logic ---
        // The zombie only attacks if the player is inside the trigger zone
        if (isPlayerInAttackRange)
        {
            AttackPlayer();
        }
    }

    // This function runs automatically when an object ENTERS the Sphere Collider trigger
    void OnTriggerEnter(Collider other)
    {
        // Check if the object that entered is the player
        if (other.CompareTag("Player"))
        {
            isPlayerInAttackRange = true;
        }
    }

    // This function runs automatically when an object EXITS the Sphere Collider trigger
    void OnTriggerExit(Collider other)
    {
        // Check if the object that exited is the player
        if (other.CompareTag("Player"))
        {
            isPlayerInAttackRange = false;
        }
    }

    void AttackPlayer()
    {
        // Stop the agent from moving while attacking
        agent.SetDestination(transform.position);

        Vector3 targetPosition = new Vector3(player.position.x, transform.position.y, player.position.z);
        transform.LookAt(targetPosition);

        if (Time.time >= lastAttackTime + attackCooldown)
        {
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage);
            }
            lastAttackTime = Time.time;
        }
    }
}