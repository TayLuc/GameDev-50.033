using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.AI;

public class EntityTargetting : MonoBehaviour
{
    // public float attackRange = 5f;
    public string enemyTag = "Enemy"; // Adjust based on actual tag
    public int firingDelay; // dictates the time in seconds before the ship can fire again
    public Transform currentTarget;
    // Reference to targeting component
    private EntityTargetting targeting;
    private ShipHandler shipHandler;


    void Awake()
    {
        shipHandler = GetComponent<ShipHandler>();
    }

    void Update()
    {

        FindNewTarget();


        if (currentTarget != null)
        {
            AttackTarget();
        }
    }

    void FindNewTarget()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, shipHandler.stats.attackRange);
        Transform closestEnemy = null;
        float closestDistance = Mathf.Infinity;


        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag(enemyTag))
            {
                float distance = Vector3.Distance(transform.position, collider.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = collider.transform;
                }
            }
        }

        // Assign the closest enemy as the target
        currentTarget = closestEnemy;
    }

    void AttackTarget()
    {
        // Attack logic goes here (e.g., dealing damage over time)
        shipHandler.Fire();


        // Check if target is destroyed
        if (currentTarget == null || !currentTarget.gameObject.activeInHierarchy)
        {
            currentTarget = null;
            FindNewTarget(); // Assign a new target when the current one is gone
        }
    }
}
