using UnityEngine;

namespace BugTower.NPCs.Enemy
{
    [CreateAssetMenu(fileName = "DialogueRoutine", menuName = "BugTower/NPCs/Routine", order = 0)]
    public class PatrolRoutine : ScriptableObject
    {
        [SerializeField] private Vector2[] patrolPoints;

        public Vector2[] PatrolPoints => patrolPoints;
    }
}
