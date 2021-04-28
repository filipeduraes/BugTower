using BugTower.Patterns;
using BugTower.Player;
using System.Collections;
using UnityEngine;

namespace BugTower.NPCs.Enemy
{
    public class EnemyAtackState : State
    {
        private readonly EnemyAI _ai;
        private readonly CheckForTarget _checker;
        private readonly Transform[] _atackPoints;
        private readonly PlayerHealth _playerHealth;
        private readonly Rigidbody2D _rigidbody;
        private readonly EnemyAnimation _animation;
        private readonly float _atackTime;

        private Coroutine atackOnTime;

        public EnemyAtackState(EnemyAI ai, CheckForTarget checker, Transform[] atackPoints, PlayerHealth playerHealth, Rigidbody2D rigidbody, EnemyAnimation animation, float atackTime)
        {
            _ai = ai;
            _checker = checker;
            _atackPoints = atackPoints;
            _playerHealth = playerHealth;
            _rigidbody = rigidbody;
            _animation = animation;
            _atackTime = atackTime;
        }

        public override void EntryAction()
        {
            atackOnTime = _ai.StartCoroutine(AtackOnRandomTime(0.5f, 2f));
            _rigidbody.velocity = Vector2.zero;
        }

        public override void ExitAction()
        {
            _ai.StopCoroutine(atackOnTime);
        }

        public override void UpdateAction()
        {
            CheckForTransition();
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
                playerIsClose |= _checker.TargetFounded(point.position);

                if (playerIsClose)
                    break;
            }

            if (!playerIsClose)
                _ai.TransitionToState(_ai.StalkerState);
        }

        private IEnumerator AtackOnRandomTime(float minTime, float maxTime)
        {
            while (true)
            {
                float time = Random.Range(minTime, maxTime);
                yield return new WaitForSeconds(time);

                _animation.PlayAtackAnimation();
                yield return new WaitForSeconds(_atackTime);

                Atack();
            }
        }

        private void Atack() 
        {
            _playerHealth.TakeDamage(1f);
        }
    }
}