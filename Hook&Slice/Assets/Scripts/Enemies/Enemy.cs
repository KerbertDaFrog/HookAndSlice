﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
	public enum EnemyStates
    {
		idle,
		attacking,
		chasing,
		staggered,
		frenzy,
		dead,
		onHook,
		offHook
    }
	
	public EnemyStates currentState;

	[Header("NavMesh")]
	public NavMeshAgent nav;

	[Header("Player")]
	[SerializeField]
	private Transform player;

	[Header("GameObjects")]
	[SerializeField]
	private GameObject damageBox;

	[Header("Movement Speed Variables")]
	[SerializeField] 
	private float walkSpeed = 1f;
	[SerializeField] 
	private float runSpeed = 4f;

	[Header("Attack Variables")]
	[SerializeField] 
	private float timeBetweenAttacks;
    [SerializeField]
    private float setAttackDelay;
    [SerializeField]
    private float attackDelay;
    [SerializeField]
	private float attackRange;
	[SerializeField]
	private int damage;

	[Header("Booleans")]
	[SerializeField]
	protected bool playerInSightRange;
	[SerializeField]
	protected bool playerInAttackRange;
	[SerializeField]
	protected bool attacking;
    [SerializeField]
    private bool hasAttacked;
	[SerializeField]
	protected bool isDead = false;
	[SerializeField]
	protected bool chasing;
	[SerializeField]
	protected bool hasSeenPlayer;

	[Header("FOV Variables")]
	public float viewRadius;
	[Range(0, 360)]
	public float viewAngle;

	[Header("Layer Masks")]
	[SerializeField]
	private LayerMask targetMask;
	[SerializeField]
	private LayerMask obstacleMask;

	[Header("Lists")]
	//[HideInInspector]
	public List<Transform> visibleTargets = new List<Transform>();

	[Header("FOV Mesh")]
	[SerializeField]
	private float meshResolution;
	[SerializeField]
	private int edgeResolveIterations;
	[SerializeField]
	private float edgeDstThreshold;
	[SerializeField]
	private float maskCutawayDst = .1f;

	[SerializeField]
	private MeshFilter viewMeshFilter;
	Mesh viewMesh;

	[Header("Animator")]
	[SerializeField]
	protected Animator anim;

	private void Awake()
	{
		player = GameObject.Find("Player").transform;
		nav = GetComponent<NavMeshAgent>();
	}

	protected virtual void Start()
	{
		if (viewMeshFilter != null)
		{
			viewMesh = new Mesh();
			viewMesh.name = "View Mesh";
			viewMeshFilter.mesh = viewMesh;
		}

		StartCoroutine("FindTargetsWithDelay", .2f);

	}

	protected virtual void Update()
	{
		//playerInSightRange = Physics.CheckSphere(transform.position, sightRange, targetMask);
		//playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, targetMask);

		//If the enemy has already seen the player, this bool will be checked until death of said enemy.
		if (playerInSightRange)
			hasSeenPlayer = true;

		if (currentState == EnemyStates.dead)
			isDead = true;

		if (attacking)
			anim.SetBool("attack", true);		
		else if (!attacking)
			anim.SetBool("attack", false);

        if (!hasAttacked)
            attackDelay = Mathf.Clamp(attackDelay -= Time.deltaTime, 0f, setAttackDelay);

        EnemyBehaviour();
	}

	protected virtual void EnemyBehaviour()
    {
		if (!playerInSightRange && !playerInAttackRange && !hasSeenPlayer && !isDead)
        {
			SetState(EnemyStates.idle);
		}

		if (playerInSightRange && !playerInAttackRange && !isDead)
        {
			chasing = true;
			SetState(EnemyStates.chasing);
		}
		else
        {
			chasing = false;
        }

		if (playerInSightRange && playerInAttackRange && !isDead)
		{
			attacking = true;
			//attackDelay = setAttackDelay;
			//hasAttacked = false;
			SetState(EnemyStates.attacking);
		}
		else
        {
			attacking = false;
        }
	}

	private void Chase()
	{
		currentState = EnemyStates.chasing;
		if (chasing && !attacking && !isDead)
        {
			nav.speed = runSpeed;
			nav.SetDestination(player.position);
        }		
	}

	protected virtual void Attack()
    {
		damageBox.SetActive(true);
		//yield return new WaitForSeconds(0.1f);
		//if(attacking)
  //      {
		//	damageBox.SetActive(true);
		//	yield return new WaitForSeconds(0.1f);
		//	attacking = false;
		//}

		//if (!attacking)
		//	damageBox.SetActive(false);

		//hasAttacked = true;
    }

	IEnumerator OnDeath()
    {
		GoToDead();
		anim.SetBool("dead", true);
		yield return new WaitForSeconds(1f);
	}

	IEnumerator FindTargetsWithDelay(float delay)
	{
		while (true)
		{
			yield return new WaitForSeconds(delay);
			FindVisibleTargets();
		}
	}

	private void LateUpdate()
	{
		DrawFieldOfView();
	}

    #region StateManagers
	public virtual void SetState(EnemyStates state)
	{
        switch (state)
        {
            case EnemyStates.idle:
				//idle
                break;

            case EnemyStates.attacking:
                //attack
                StartCoroutine("Attack");
                break;

            case EnemyStates.chasing:
                //chasing
                Chase();
                break;

            case EnemyStates.dead:
                //dead
                StartCoroutine("OnDeath");
                break;

            case EnemyStates.staggered:
				GoToStaggered();
				break;

            case EnemyStates.frenzy:
                break;

            case EnemyStates.onHook:
				SetSpeed(EnemyStates.onHook);
                break;

            case EnemyStates.offHook:
				SetSpeed(EnemyStates.offHook);
                break;
        }

        currentState = state;
    }

	private void GoToStaggered()
    {
		// This is called by SetState
		currentState = EnemyStates.staggered;
		anim.SetBool("caught", true);
    }

	private void GoToDead()
    {
		// This is called from the OnDeath coroutine
		currentState = EnemyStates.dead;
		anim.SetBool("caught", false);
		anim.SetBool("attack", false);
		anim.SetBool("walk", false);
		anim.SetBool("dead", true);
    }

    public void SetSpeed(EnemyStates speed)
	{
		switch (speed)
		{
			case EnemyStates.onHook:
				nav.speed = 0;
				break;
			case EnemyStates.offHook:
				nav.speed = runSpeed;
				break;
		}
	}
    #endregion

    #region FindTargets
    private void FindVisibleTargets()
	{
		visibleTargets.Clear();
		Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);
		Collider[] targetsInAttackRadius = Physics.OverlapSphere(transform.position, attackRange, targetMask);

		for (int i = 0; i < targetsInViewRadius.Length; i++)
		{
			Transform target = targetsInViewRadius[i].transform;
			Vector3 dirToTarget = (target.position - transform.position).normalized;
			if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
			{
				float dstToTarget = Vector3.Distance(transform.position, target.position);
				if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
				{
					playerInSightRange = true;
					visibleTargets.Add(target);
				}
			}
		}

		for (int i = 0; i < targetsInAttackRadius.Length; i++)
		{
			Transform target = targetsInAttackRadius[i].transform;
			Vector3 dirToTarget = (target.position - transform.position).normalized;
			if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
			{
				float dstToTarget = Vector3.Distance(transform.position, target.position);
				if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
				{
					playerInAttackRange = true;
					visibleTargets.Add(target);
				}
			}
		}

		if (targetsInViewRadius.Length == 0)
			playerInSightRange = false;


		if (targetsInAttackRadius.Length == 0)
			playerInAttackRange = false;
	}
    #endregion

    #region FOVMeshDraw
    private void DrawFieldOfView()
	{
		int stepCount = Mathf.RoundToInt(viewAngle * meshResolution);
		float stepAngleSize = viewAngle / stepCount;
		List<Vector3> viewPoints = new List<Vector3>();
		ViewCastInfo oldViewCast = new ViewCastInfo();
		for (int i = 0; i <= stepCount; i++)
		{
			float angle = transform.eulerAngles.y - viewAngle / 2 + stepAngleSize * i;
			ViewCastInfo newViewCast = ViewCast(angle);

			if (i > 0)
			{
				bool edgeDstThresholdExceeded = Mathf.Abs(oldViewCast.dst - newViewCast.dst) > edgeDstThreshold;
				if (oldViewCast.hit != newViewCast.hit || (oldViewCast.hit && newViewCast.hit && edgeDstThresholdExceeded))
				{
					EdgeInfo edge = FindEdge(oldViewCast, newViewCast);
					if (edge.pointA != Vector3.zero)
					{
						viewPoints.Add(edge.pointA);
					}
					if (edge.pointB != Vector3.zero)
					{
						viewPoints.Add(edge.pointB);
					}
				}

			}


			viewPoints.Add(newViewCast.point);
			oldViewCast = newViewCast;
		}

		int vertexCount = viewPoints.Count + 1;
		Vector3[] vertices = new Vector3[vertexCount];
		int[] triangles = new int[(vertexCount - 2) * 3];

		vertices[0] = Vector3.zero;
		for (int i = 0; i < vertexCount - 1; i++)
		{
			vertices[i + 1] = transform.InverseTransformPoint(viewPoints[i]) + Vector3.forward * maskCutawayDst;

			if (i < vertexCount - 2)
			{
				triangles[i * 3] = 0;
				triangles[i * 3 + 1] = i + 1;
				triangles[i * 3 + 2] = i + 2;
			}
		}

		if (viewMeshFilter != null)
		{
			viewMesh.Clear();

			viewMesh.vertices = vertices;
			viewMesh.triangles = triangles;
			viewMesh.RecalculateNormals();
		}
	}


	EdgeInfo FindEdge(ViewCastInfo minViewCast, ViewCastInfo maxViewCast)
	{
		float minAngle = minViewCast.angle;
		float maxAngle = maxViewCast.angle;
		Vector3 minPoint = Vector3.zero;
		Vector3 maxPoint = Vector3.zero;

		for (int i = 0; i < edgeResolveIterations; i++)
		{
			float angle = (minAngle + maxAngle) / 2;
			ViewCastInfo newViewCast = ViewCast(angle);

			bool edgeDstThresholdExceeded = Mathf.Abs(minViewCast.dst - newViewCast.dst) > edgeDstThreshold;
			if (newViewCast.hit == minViewCast.hit && !edgeDstThresholdExceeded)
			{
				minAngle = angle;
				minPoint = newViewCast.point;
			}
			else
			{
				maxAngle = angle;
				maxPoint = newViewCast.point;
			}
		}

		return new EdgeInfo(minPoint, maxPoint);
	}


	ViewCastInfo ViewCast(float globalAngle)
	{
		Vector3 dir = DirFromAngle(globalAngle, true);
		RaycastHit hit;

		if (Physics.Raycast(transform.position, dir, out hit, viewRadius, obstacleMask))
		{
			return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
		}
		else
		{
			return new ViewCastInfo(false, transform.position + dir * viewRadius, viewRadius, globalAngle);
		}
	}

	public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
	{
		if (!angleIsGlobal)
		{
			angleInDegrees += transform.eulerAngles.y;
		}
		return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
	}

    public struct ViewCastInfo
	{
		public bool hit;
		public Vector3 point;
		public float dst;
		public float angle;

		public ViewCastInfo(bool _hit, Vector3 _point, float _dst, float _angle)
		{
			hit = _hit;
			point = _point;
			dst = _dst;
			angle = _angle;
		}
	}

	private struct EdgeInfo
	{
		public Vector3 pointA;
		public Vector3 pointB;

		public EdgeInfo(Vector3 _pointA, Vector3 _pointB)
		{
			pointA = _pointA;
			pointB = _pointB;
		}
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, attackRange);
		//Gizmos.color = Color.yellow;
		//Gizmos.DrawWireSphere(transform.position, sightRange);
	}
    #endregion

    #region Getters
    public int Damage()
    {
        return damage;
    }

    #endregion
}