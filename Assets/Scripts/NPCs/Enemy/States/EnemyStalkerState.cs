using BugTower.Patterns;
using System.Collections;
using UnityEngine;

namespace BugTower.NPCs.Enemy
{
    public class EnemyStalkerState : State
    {
        private readonly EnemyAI _ai;
        private readonly FindPath _finder;
        private readonly Rigidbody2D _rigidbody;
        private readonly Transform _player;
        private readonly CheckForTarget _atackChecker;
        private readonly EnemyAnimation _animation;
        private readonly Transform[] _atackPoints;
        private readonly float _enemySpeed;
        private Coroutine pathOnTime;

        public EnemyStalkerState(EnemyAI ai, FindPath finder, Transform[] atackPoints, Rigidbody2D rigidbody, float enemySpeed, Transform player, CheckForTarget atackChecker, EnemyAnimation animation)
        {
            _ai = ai;
            _finder = finder;
            _rigidbody = rigidbody;
            _enemySpeed = enemySpeed;
            _player = player;
            _atackChecker = atackChecker;
            _animation = animation;
            _atackPoints = atackPoints;
        }

        public override void EntryAction()
        {
            pathOnTime = _ai.StartCoroutine(GeneratePathOnTime(0.5f));
        }

        public override void ExitAction()
        {
            _ai.StopCoroutine(pathOnTime);
        }

        private IEnumerator GeneratePathOnTime(float time)
        {
            while (true)
            {
                if (_player == null)
                    _ai.TransitionToState(_ai.PatrolState);

                _finder.GeneratePath(_rigidbody.position, _player.position);
                yield return new WaitForSeconds(time);
            }
        }

        public override void FixedUpdateAction()
        {
            _finder.MoveThrowPath(_rigidbody, _enemySpeed);

            if(!_animation.IsMovingVerticaly)
                CheckForSideChange();

            _animation.CheckForAnimationChange(_rigidbody.velocity, 1f);
        }

        public override void UpdateAction()
        {
            CheckForTransition();
        }

        private void CheckForSideChange()
        {
            float difference = _player.position.x - _rigidbody.position.x;

            if (difference < 0f)
                _rigidbody.transform.localScale = new Vector3(-1f, 1f, 1f);
            else if (difference > 0f)
                _rigidbody.transform.localScale = new Vector3(1f, 1f, 1f);
        }

        private void CheckForTransition()
        {
            if (_ai.PlayerIsRolled)
            {
                _ai.TransitionToState(_ai.PatrolState);
                return;
            }

            bool playerIsClose = false;

            foreach (Transform point in _atackPoints)
            {
                playerIsClose |= _atackChecker.TargetFounded(point.position);

                if (playerIsClose)
                    break;
            }

            if (playerIsClose)
                _ai.TransitionToState(_ai.AtackState);
        }
    }
}
