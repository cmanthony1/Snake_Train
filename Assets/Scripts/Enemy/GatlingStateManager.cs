using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatlingStateManager : MonoBehaviour
{
    [SerializeField] private GunData gatlingData;
    [SerializeField] private Transform gatlingTransform;
    [SerializeField] private float detectionRadius;

    /* Agent Data */
    private Transform enemySpriteTransform;
    private float xEnemyScale;
    private float yGatlingScale;
    private Transform playerTransform;
    private Vector3 lastPlayerPosition;

    /* Weapon Data */
    private Transform firePointTransform;
    private Vector2 directionToPlayer;
    private bool isPlayerDetected;
    private bool canFire = true;
    private float drumCapacity;
    private float reloadSpeed;

    private GatlingState currentState;

    public enum GatlingState
    {
        Scanning,
        Firing,
        Inspecting,
        Reloading,
        Throwing
    }

    /*
     * Subscribes to the OnDeathPlayer event in the PlayerStats script.
     * Invokes: PlayerDead().
     */
    private void OnEnable()
    {
        PlayerStats.OnDeathPlayer += PlayerDead;
    }

    /* Unsubscribes from the OnDeathPlayer event in the PlayerStats script (if destroyed). */
    private void OnDisable()
    {
        PlayerStats.OnDeathPlayer -= PlayerDead;
    }

    /* Finds the child with the specified name of the object this script is attached to. */
    private void Awake()
    {
        enemySpriteTransform = transform.Find("SpriteEnemy").GetComponent<Transform>();
        xEnemyScale = enemySpriteTransform.localScale.x;
        yGatlingScale = gatlingTransform.localScale.y;
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        firePointTransform = transform.Find("Gatling (Pivot)/FirePoint").GetComponent<Transform>();
        drumCapacity = gatlingData.Capacity;
        reloadSpeed = gatlingData.ReloadSpeed;

        /* Sets current state of object. */
        currentState = GatlingState.Scanning;
    }

    private void Update()
    {
        if (playerTransform != null)
        {
            directionToPlayer = (playerTransform.position - gatlingTransform.position).normalized;
            Scan();
            AimWeapon();
        

            switch (currentState)
            {
                case GatlingState.Scanning:

                    currentState = (isPlayerDetected) ? GatlingState.Firing : GatlingState.Scanning;
                    break;

                case GatlingState.Firing:

                    Firing();
                    break;

                case GatlingState.Inspecting:

                    currentState = (drumCapacity < gatlingData.Capacity) ? GatlingState.Reloading : GatlingState.Scanning;
                    break;

                case GatlingState.Reloading:

                    StartCoroutine(Reload());
                    break;

                case GatlingState.Throwing:

                    ThrowMolotov();
                    break;
            }
        }
    }

    private void Scan()
    {
        if (Vector2.Distance(playerTransform.position, gatlingTransform.position) < detectionRadius)
        {
            isPlayerDetected = true;
            lastPlayerPosition = playerTransform.position;
        }
        else
        {
            isPlayerDetected = false;
        }
    }

    private void AimWeapon()
    {
        /* Calculates the angle between two points. */
        float aimAngle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;

        /* Sets the angle of this object to face the mouse position. */
        gatlingTransform.rotation = Quaternion.Euler(gatlingTransform.rotation.x, gatlingTransform.rotation.y, aimAngle);

        if (directionToPlayer.x < 0)
        {
            enemySpriteTransform.localScale = new Vector3(-xEnemyScale, enemySpriteTransform.localScale.y, enemySpriteTransform.localScale.z);
            gatlingTransform.localScale = new Vector3(gatlingTransform.localScale.x, -yGatlingScale , gatlingTransform.localScale.z);
        }
        else
        {
            enemySpriteTransform.localScale = new Vector3(xEnemyScale, enemySpriteTransform.localScale.y, enemySpriteTransform.localScale.z);
            gatlingTransform.localScale = new Vector3(gatlingTransform.localScale.x, yGatlingScale, gatlingTransform.localScale.z);
        }
    }

    /* Gizmos for visibility */
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(gatlingTransform.position, detectionRadius);

        if (playerTransform != null)
        {
            if (isPlayerDetected)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(firePointTransform.position, playerTransform.position);
            }
            else
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(firePointTransform.position, directionToPlayer * detectionRadius);
            }
            
        }
    }

    private void PlayerDead()
    {
        isPlayerDetected = false;
    }

    /* GATLING STATES */
    private void Firing()
    {
        if (!isPlayerDetected)
        {
            currentState = GatlingState.Throwing;
        }
        else if (drumCapacity > 0)
        {
            if (canFire)
            {
                GameObject projectile = BulletPooler.current.GetPooledObject();

                if (projectile != null)
                {
                    projectile.transform.position = firePointTransform.position;
                    projectile.transform.rotation = firePointTransform.rotation;
                    projectile.GetComponent<BulletController>().FireForce = gatlingData.FireForce;
                    int randomDamage = Mathf.FloorToInt(Random.Range(gatlingData.MinDamage, gatlingData.MaxDamage));
                    projectile.GetComponent<BulletController>().BulletDamage = randomDamage;
                    projectile.SetActive(true);
                    drumCapacity--;
                    StartCoroutine(FireTimer());
                }
                else
                {
                    return;
                }
            }
        }
        else
        {
            currentState = GatlingState.Inspecting;
        }
    }

    /* Fire Rate Control. */
    private IEnumerator FireTimer()
    {
        canFire = false;
        yield return new WaitForSeconds(gatlingData.FireRate);
        canFire = true;
    }

    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(reloadSpeed);
        drumCapacity = gatlingData.Capacity;
        currentState = GatlingState.Scanning;
    }

    private void ThrowMolotov()
    {
        GameObject throwable = MolotovPooler.current.GetPooledObject();

        if (throwable != null)
        {
            throwable.GetComponent<ThrowableController>().StartPosition = transform.position;
            throwable.GetComponent<ThrowableController>().EndPosition = lastPlayerPosition;
            throwable.transform.rotation = Quaternion.identity;
            throwable.transform.localScale = gatlingTransform.localScale;
            throwable.SetActive(true);

            currentState = GatlingState.Inspecting;
        }
        else
        {
            return;
        }
    }
}
