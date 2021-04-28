using BugTower.Patterns;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace BugTower.Player
{
    [RequireComponent(typeof(Rigidbody2D), typeof(PlayerHealth))]
    public class PlayerController : StateMachine
    {
        public PlayerNormalState NormalState { get; private set; }
        public PlayerRolledState RolledState { get; private set; }
        public PlayerUpState UpState { get; private set; }

        public event Action<State> OnStateChange;

        [Header("Configuration")]
        [SerializeField] private Animator playerAnimator = null;
        [SerializeField] private SpriteRenderer playerSpriteRenderer = null;
        [SerializeField] private float playerNormalSpeed = 5f;
        [SerializeField] private AudioSource walkSound;

        [Header("Rolled Effects")]
        [SerializeField] private RectTransform maskTransform = null;
        [SerializeField] private Image blackImage = null;
        [SerializeField] private Camera mainCamera;

        private PlayerMovement movement;
        private PlayerInput input;
        private PlayerAnimation playerAnimation;

        private void Awake()
        {
            Rigidbody2D playerRigidbody = GetComponent<Rigidbody2D>();
            blackImage.color = new Color(blackImage.color.r, blackImage.color.g, blackImage.color.b, 0f);

            movement = new PlayerMovement(playerRigidbody);
            input = new PlayerInput();
            playerAnimation = new PlayerAnimation(playerAnimator, playerSpriteRenderer);

            NormalState = new PlayerNormalState(this, input, movement, playerAnimation, playerNormalSpeed);
            RolledState = new PlayerRolledState(this, input, playerAnimation, maskTransform, blackImage, mainCamera);
            UpState = new PlayerUpState(this, input, movement, playerAnimation, playerNormalSpeed);
            
            TransitionToState(NormalState);
        }

        private void Update()
        {
            input.RegisterInput();
            currentState.UpdateAction();

            if (playerAnimation.IsMoving && !walkSound.isPlaying)
                walkSound.Play();
            else if(!playerAnimation.IsMoving && walkSound.isPlaying)
                walkSound.Stop();
        }

        private void FixedUpdate()
        {
            currentState.FixedUpdateAction();
        }

        public override void TransitionToState(State state)
        {
            base.TransitionToState(state);
            OnStateChange?.Invoke(state);
        }
    }
}