using BugTower.Patterns;
using UnityEngine;

namespace BugTower.NPCs.Enemy
{
    public class EnemyPatrolState : State
    {
        private readonly EnemyAI _ai;
        private readonly CheckForTarget _checker;
        private readonly Rigidbody2D _enemyRigidbody;
        private readonly PatrolRoutine _routine;
        private readonly FindPath _findPath;
        private readonly EnemyAnimation _animation;
        private readonly float _speed;
        private int currentPointIndex = 0;

        public EnemyPatrolState(EnemyAI ai, FindPath findPath, CheckForTarget checker, Rigidbody2D enemyRigidbody, PatrolRoutine routine, EnemyAnimation animation, float speed)
        {
            _ai = ai;
            _checker = checker;
            _enemyRigidbody = enemyRigidbody;
            _routine = routine;
            _findPath = findPath;
            _speed = speed;
            _animation = animation;
        }

        public override void EntryAction()
        {
            currentPointIndex = 0;
            _findPath.CurrentPath = null;
        }

        public override void FixedUpdateAction()
        {
            if(_findPath.CurrentPath == null)
                _findPath.GeneratePath(_enemyRigidbody.position, _routine.PatrolPoints[currentPointIndex]);

            Patrol();
        }

        public override void UpdateAction()
        {
            CheckForTransition();

            if (!_animation.IsMovingVerticaly)
                CheckForSideChange();

            _animation.CheckForAnimationChange(_enemyRigidbody.velocity, 0.2f);
        }

        private void Patrol()
        {
            bool moveHasEnded = _findPath.MoveThrowPath(_enemyRigidbody, _speed);

            if (moveHasEnded)
                GenerateNextPath();
        }

        private float xLeftTimer = 0f;
        private float xRightTimer = 0f;

        private void CheckForSideChange()
        {
            if (_enemyRigidbody.velocity.x < 0f)
            {
                xLeftTimer += Time.deltaTime;
                xRightTimer = 0f;
            }
            else if (_enemyRigidbody.velocity.x > 0f)
            {
                xRightTimer += Time.deltaTime;
                xLeftTimer = 0f;
            }

            float timeToSwap = 0.2f;

            if (xLeftTimer >= timeToSwap)
            {
                _enemyRigidbody.transform.localScale = new Vector3(-1f, 1f, 1f);
                xRightTimer = 0f;
                xLeftTimer = 0f;
            }
            else if (xRightTimer >= timeToSwap)
            {
                _enemyRigidbody.transform.localScale = new Vector3(1f, 1f, 1f);
                xRightTimer = 0f;
                xLeftTimer = 0f;
            }
        }

        private void GenerateNextPath()
        {
            bool indexInsideRange = currentPointIndex < _routine.PatrolPoints.Length - 1;
            currentPointIndex = indexInsideRange ? currentPointIndex + 1 : 0;
            _findPath.GeneratePath(_enemyRigidbody.position, _routine.PatrolPoints[currentPointIndex]);
        }

        private void CheckForTransition()
        {
            if (_ai.PlayerIsRolled)
                return;

            Vector2 enemyPosition = _enemyRigidbody.position;
            bool targetFound = _checker.TargetFounded(enemyPosition);

            if (targetFound)
                _ai.TransitionToState(_ai.StalkerState);
        }
    }
}