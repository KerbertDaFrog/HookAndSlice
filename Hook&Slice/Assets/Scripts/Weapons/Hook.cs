using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    [SerializeField]
    private float currentSpeed;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float dist;
    //The distance of the hook from the harpoon gun.
    [SerializeField]
    private float distanceFromGun;
    [SerializeField]
    private float setTimer;
    [SerializeField]
    private float currentTimer;
    [SerializeField]
    private float totalDist;
    private float maxDist = 1000f;
    private float gapDist = 0.1f;

    [SerializeField]
    private bool done;
    [SerializeField]
    private bool retracting;
    [SerializeField]
    private bool firing;
    public bool retracted;

    public int damage;
    public int maxEnemies;

    private int index = 0;

    [SerializeField]
    private List<Vector3> targets = new List<Vector3>();

    public List<Enemy> enemies = new List<Enemy>();

    private Harpoon harpoon;

    [SerializeField]
    private GameObject firePoint;

    [SerializeField]
    private Vector3 origin;
    [SerializeField]
    private Vector3 target;

    private List<GameObject> hookedEnemies = new List<GameObject>();

    private bool hasDoneThingsOnEnemies = false;
    
    private void Awake()
    {
        /*stats = FindObjectOfType<StatsManager>();*/ // If you're doing a find then why make the stats manager static? //true :/
        firePoint = GameObject.Find("FirePoint");
        harpoon = FindObjectOfType<Harpoon>();
    }

    private void Start()
    {
        StatsManager._instance.hooksShot += 1;
        origin = transform.position;
        currentTimer = setTimer;
        //Take all of the elements from the hitPoints list in the Harpoon Component and add/copy them to the targets list in this script.
        targets.AddRange(harpoon.hitPoints);
        this.transform.SetParent(null);
    }

    private void Update()
    {
        //If the targets list is not null, then the Vector3 variable for target will be equal to the first element of targets index.
        if (targets != null && !done)
            target = targets[index];

        if(!done)
            print("my target = " + index);   

        if(PauseMenu._instance.paused == false)
        {
            //Pause Menu Stuff: If Paused, current speed is set to 0 until not paused, which will return currentSpeed to the player set speed.
            currentSpeed = speed;

            if (!retracted)
            {
                totalDist += dist;

                dist = Vector3.Distance(origin, target);

                distanceFromGun = Vector3.Distance(this.transform.position, harpoon.transform.position);
            }
        }
        else if(PauseMenu._instance == true)
        {
            currentSpeed = 0;
        }
       
        origin = transform.position;

        transform.LookAt(target);

        //If targets list is not null and the hook has not yet retracted fully back to the gun after being shot, keep running Vector3.MoveTowards on the hook to make it travel from the origin to the target at it's current speed.
        if(targets != null && !retracted)
            transform.position = Vector3.MoveTowards(origin, target, currentSpeed);

        //If the distance to the target is less 0.1 than change the index to one element up.
        if (dist < 0.1)
            index++;  

        //If the index int is equal to the amount of elements in the Vector3 targets list and the distance to the target is less than 0.1, do this.
        if (index == targets.Count && dist < 0.1)
            done = true;

        //If done, change the target to the harpoon gun's transform position.
        if(done)
        {
            target = harpoon.transform.position;
            retracting = true;
        }

        //If done and the hook's distance to the harpoon gun is less than 0.1 and the index count of enemies is 0, turn off done, turn off hasShot to false in Harpoon script, and destroy hook game object.
        if(done && distanceFromGun < 0.1 && enemies.Count == 0)
        {
            done = false;
            harpoon.hasShot = false;
            harpoon.returned = true;
            OnHookDestroyed();
        }   
        
        //If the number of enemies on the enemies list is more than 0, start the countdown timer.
        if(enemies.Count > 0 && retracted != false)
            currentTimer = Mathf.Clamp(currentTimer -= Time.deltaTime, 0f, setTimer);

        //If the harpoon has been shot (hasShot = true), the Enemy count is more than zero and the hook's distance from the harpoon gun is less than 0.1, set retracting to false and retracted to true.
        if(harpoon.hasShot != false && enemies.Count > 0 && distanceFromGun < 0.1)
        {
            retracting = false;
            retracted = true;
        }

        //If retracted not equal to false and the Enemy count is zero, set retracted to false.
        if(retracted != false && enemies.Count <= 0)
            retracted = false;

        //If index larger than maxEnemies than Remove all elements until maxEnemies limit reached.
        if(enemies.Count > maxEnemies)
            enemies.RemoveAt(maxEnemies);

        if(currentTimer <= 0)
            OnTimerEnd();

        if(targets.Count > 0)
            PullEnemyToPlayer();
        
        if(harpoon.hookCancelled)
            OnHookCancelled();

        if (totalDist >= maxDist)
            OnHookCancelled();


        //When the hook has been retracted and has not yet operated on the enemies it has it
        if (retracted && enemies.Count > 0 && hasDoneThingsOnEnemies == false)
        {
            IfThereAreZeroEnemiesOnHookDestroyHook();

            foreach(Enemy e in enemies)
            {
                if(e.currentState != Enemy.EnemyStates.dead)
                {
                    e.SetState(Enemy.EnemyStates.onHook);
                }
            }

            hasDoneThingsOnEnemies = true;
        }
        
        if (hasDoneThingsOnEnemies)
        {
            // Once enemies have been operated on once this will run
            SetEnemyPosAndSpacingOnHook();
        }
    }

    private void SetEnemyPosAndSpacingOnHook()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i] != null)
            {               
                // get the world space position of firepoint transform
                Vector3 onChainPos = firePoint.transform.position;

                // get firepoint forward facing axis
                Vector3 firepointForward = firePoint.transform.forward;

                // multiply gap dist by i and move along firepoint local z axis
                firepointForward = firepointForward * gapDist * i;

                onChainPos += firepointForward;

                enemies[i].transform.position = onChainPos;
            }
        }
    }

    private void IfThereAreZeroEnemiesOnHookDestroyHook()
    {
        for (int i = enemies.Count - 1; i >= 0; i--)
        {
            if (enemies[i] == null)
            {
                OnHookDestroyed();
            }
        }
    }    

    private void PullEnemyToPlayer()
    {
        //For each enemy in the enemies transform list: Set as a child of this transform(the hook) and set their transform.position to the same position that the hook is at.
        foreach (Enemy enemy in enemies)
        {
            if(enemy != null)
                enemy.nav.speed = 0;

            //if (retracted && enemy != null)
            //{

            //    Come back to this line(WIP)

            //    enemy.SetParent(this.transform);
            //    Change to instant teleport
            //    enemy.transform.position = Vector3.MoveTowards(origin, target, currentSpeed);
            //}
        }
    }

    private void OnTimerEnd()
    {
        //If current timer is less than or equal to 0 than run a for loop that iterates through the enemies list backwards starting at the last element of the index
        //and unparent each enemy from the list then remove all the elements from the list.
        for (int i = enemies.Count - 1; i >= 0; i--)
        {
            if (enemies[i] != null)
            {
                if (enemies[i].currentState != Enemy.EnemyStates.dead)
                {
                    enemies[i].SetState(Enemy.EnemyStates.offHook);
                    enemies.Remove(enemies[i]);
                }
                else
                {
                    enemies.Remove(enemies[i]);
                }
            }
        }
    }

    private void OnHookCancelled()
    {
        for (int i = enemies.Count - 1; i >= 0; i--)
        {
            if (enemies[i] != null)
            {
                retracted = true;
            }
        }

        if (enemies.Count == 0)
            OnHookDestroyed();
    }

    private void OnHookDestroyed()
    {
        harpoon.staticHook.SetActive(true);
        Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        Debug.Log("Total distance travelled:" + totalDist);
    }

    private void OnTriggerEnter(Collider other)
    {
        //If the trigger collided object has tag "Enemy", then add the Transform to the enemies list.
        if(other.gameObject.tag == "Enemy")
        {
            if (!hookedEnemies.Contains(other.gameObject))
            {
                Enemy stabbedEnemy = other.GetComponent<Enemy>();
                enemies.Add(stabbedEnemy);
                stabbedEnemy.SetState(Enemy.EnemyStates.staggered);
                hookedEnemies.Add(other.gameObject);
            }
        }
    }
}
