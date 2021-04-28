using UnityEngine;

namespace BugTower.Cameras
{
    public class CameraFollow : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Transform _target;

        [Header("Parameters")]
        [SerializeField] private float _smoothTime;
        [SerializeField] private float _PPU;

        [Header("Limits")]
        [SerializeField] private float _leftLimit;
        [SerializeField] private float _rightLimit;
        [SerializeField] private float _bottomLimit;
        [SerializeField] private float _topLimit;

        [Header("Constraints")]
        [SerializeField] private bool _lockX;
        [SerializeField] private bool _lockY;

        private Vector2 velocity;

        private void Start() => NormalizeValues();

        private void Update() => Follow();

        #if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            NormalizeValues(out float left, out float right, out float top, out float bottom);

            Vector2 size = new Vector2(right - left, top - bottom);
            Vector2 center = new Vector2(left, bottom) + size / 2f;

            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(center, size);
        }
        #endif

        private void Follow()
        {
            if (_target == null)
                this.enabled = false;

            Vector2 endPos = _target.position;

            endPos = _lockX ? new Vector2(transform.position.x, endPos.y) : endPos;
            endPos = _lockY ? new Vector2(endPos.x, transform.position.y) : endPos;

            Vector2 proxyPosition = endPos;
            proxyPosition = new Vector2(Mathf.Clamp(proxyPosition.x, _leftLimit, _rightLimit),
                                        Mathf.Clamp(proxyPosition.y, _bottomLimit, _topLimit));

            proxyPosition.x = Mathf.Round(proxyPosition.x * _PPU) / _PPU;
            proxyPosition.y = Mathf.Round(proxyPosition.y * _PPU) / _PPU;

            transform.position = Vector2.SmoothDamp(transform.position, proxyPosition, ref velocity, _smoothTime);
        }

        private void NormalizeValues()
        {
            _leftLimit = Mathf.Round(_leftLimit * _PPU) / _PPU;
            _rightLimit = Mathf.Round(_rightLimit * _PPU) / _PPU;
            _topLimit = Mathf.Round(_topLimit * _PPU) / _PPU;
            _bottomLimit = Mathf.Round(_bottomLimit * _PPU) / _PPU;
        }

        private void NormalizeValues(out float left, out float right, out float top, out float bottom)
        {
            left = Mathf.Round(_leftLimit * _PPU) / _PPU;
            right = Mathf.Round(_rightLimit * _PPU) / _PPU;
            top = Mathf.Round(_topLimit * _PPU) / _PPU;
            bottom = Mathf.Round(_bottomLimit * _PPU) / _PPU;
        }
    }
}