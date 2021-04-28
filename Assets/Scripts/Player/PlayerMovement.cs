using UnityEngine;

namespace BugTower.Player
{
    public class PlayerMovement
    {
        private readonly Rigidbody2D _playerRigidbody;

        public PlayerMovement(Rigidbody2D playerRigidbody)
        {
            _playerRigidbody = playerRigidbody;
        }

        public void MoveTo(Vector2 position)
        {
            Vector2 newPosition = _playerRigidbody.position;
            newPosition += position;
            _playerRigidbody.MovePosition(newPosition);
        }
    }
}
