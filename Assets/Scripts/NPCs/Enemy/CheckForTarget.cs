using UnityEngine;

namespace BugTower.NPCs.Enemy
{
    public class CheckForTarget
    {
        private readonly float _radius;
        private readonly LayerMask _targetMask;

        public CheckForTarget(float radius, LayerMask targetMask)
        {
            _radius = radius;
            _targetMask = targetMask;
        }

        public bool TargetFounded(Vector2 currentPosition)
        {
            return Physics2D.OverlapCircle(currentPosition, _radius, _targetMask);
        }

        public bool TargetFounded(Vector2 currentPosition, out Collider2D collider)
        {
            collider = Physics2D.OverlapCircle(currentPosition, _radius, _targetMask);
            return collider;
        }
    }
}
