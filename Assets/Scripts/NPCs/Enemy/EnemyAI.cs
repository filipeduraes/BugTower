using BugTower.Patterns;
using UnityEngine;
using Pathfinding;
using BugTower.Player;

namespace BugTower.NPCs.Enemy
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Seeker))]
    public class EnemyAI : StateMachine
    {
        public EnemyPatrolState PatrolState { get; private set; }
        public EnemyStalkerState StalkerState { get; private set; }
        public EnemyAtackState AtackState { get; private set; }
        public bool PlayerIsRolled { get; private set; }

        [Header("General")]
        [SerializeField] private Animator enemyAnimator;
        [SerializeField] private PlayerController player;

        [Header("Pathfinding")]
        [SerializeField] private PatrolRoutine patrolRoutine;
        [SerializeField] private float speed;
        [SerializeField] private float checkerRadius;
        
        [Header("Melee Atack")]
        [SerializeField] private Transform[] atackPoints;
        [SerializeField] private float atackRadius = 0.5f;
        [SerializeField] private float atackTime = 3.6f;

        private Seeker seeker;
        private Rigidbody2D enemyRigidbody;
        private EnemyAnimation enemyAnimation;
        private FindPath findPath;
        private CheckForTarget patrolChecker;
        private CheckForTarget atackChecker;

        private void Awake()
        {
            int playerLayerMask = player.gameObject.layer.LayerToLayerMask();
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();

            enemyAnimation = new EnemyAnimation(enemyAnimator);
            seeker = GetComponent<Seeker>();
            enemyRigidbody = GetComponent<Rigidbody2D>();

            findPath = new FindPath(seeker);
            patrolChecker = new CheckForTarget(checkerRadius, playerLayerMask);
            atackChecker = new CheckForTarget(atackRadius, playerLayerMask);

            PatrolState = new EnemyPatrolState(this, findPath, patrolChecker, enemyRigidbody, patrolRoutine, enemyAnimation, speed);
            StalkerState = new EnemyStalkerState(this, findPath, atackPoints, enemyRigidbody, speed, player.transform, atackChecker, enemyAnimation);
            AtackState = new EnemyAtackState(this, atackChecker, atackPoints, playerHealth, enemyRigidbody, enemyAnimation, atackTime);

            TransitionToState(PatrolState);
        }

        private void OnEnable()
        {
            player.OnStateChange += Player_OnStateChange;
        }

        private void OnDisable()
        {
            player.OnStateChange -= Player_OnStateChange;
        }

        private void Player_OnStateChange(State state)
        {
            PlayerIsRolled = state is PlayerRolledState;
        }

        private void Update()
        {
            currentState.UpdateAction();
        }

        private void FixedUpdate()
        {
            currentState.FixedUpdateAction();
        }

        private void OnDrawGizmosSelected()
        {
            if (atackPoints == null)
                return;

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, checkerRadius);

            Gizmos.color = Color.blue;
            foreach (Transform point in atackPoints)
            {
                if (point == null)
                    continue;
                Gizmos.DrawWireSphere(point.position, atackRadius);
            }
        }
    }
}