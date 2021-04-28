using Pathfinding;
using UnityEngine;

namespace BugTower.NPCs.Enemy
{
    public class FindPath
    {
        public Path CurrentPath { get; set; }
        private readonly Seeker _seeker;
        private int currentPoint = 0;

        public FindPath(Seeker seeker)
        {
            _seeker = seeker;
        }

        public void GeneratePath(Vector2 startPosition, Vector2 endPosition)
        {
            if (!_seeker.IsDone())
                return;

            _seeker.StartPath(startPosition, endPosition, OnPathComplete);
        }

        public bool MoveThrowPath(Rigidbody2D rigidbody, float speed)
        {
            if (CurrentPath == null)
                return false;

            Vector2 nextPoint = CurrentPath.vectorPath[currentPoint];
            Vector2 direction = (nextPoint - rigidbody.position).normalized;
            Vector2 moveVelocity = direction * speed * Time.deltaTime;

            rigidbody.velocity = moveVelocity;
            bool arrived = Vector2.Distance(rigidbody.position, nextPoint) < 0.2f;

            if (arrived)
            {
                bool pathHasEnded = currentPoint >= CurrentPath.vectorPath.Count - 1;

                if (!pathHasEnded)
                    currentPoint++;

                return pathHasEnded;
            }

            return false;
        }

        private void OnPathComplete(Path path)
        {
            if (path.error)
                return;

            CurrentPath = path;
            currentPoint = 0;

            string log = string.Empty;
            for (int i = 0; i < CurrentPath.vectorPath.Count; i++)
            {
                log += $"{i}: {CurrentPath.vectorPath[i]}\n";
            }
        }
    }
}
