using BugTower.Patterns;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace BugTower.Player
{
    public class PlayerRolledState : State
    {
        private readonly PlayerInput playerInput;
        private readonly PlayerAnimation playerAnimation;
        private readonly RectTransform maskTansform;
        private readonly Image blackImage;
        private readonly Camera camera;
        private readonly PlayerController player;
        private readonly float initialSize;
        private readonly float timeToLerp = 0.5f;
        private bool cameraCoroutineFinished = false;

        public PlayerRolledState(PlayerController playerFSM, PlayerInput input, PlayerAnimation animation, RectTransform mask, Image background, Camera mainCamera)
        {
            playerInput = input;
            playerAnimation = animation;
            maskTansform = mask;
            blackImage = background;
            camera = mainCamera;
            player = playerFSM;

            initialSize = camera.orthographicSize;
        }

        public override void EntryAction()
        {
            playerAnimation.IsRolled = true;
            cameraCoroutineFinished = false;
            player.StartCoroutine(CameraGetCloser(2f));           
        }

        public override void ExitAction()
        {
            playerAnimation.IsRolled = false;
            player.StartCoroutine(SetOpacitySmoothly(0.8f, 0f));
            player.StartCoroutine(CameraGetCloser(initialSize));
        }

        public override void UpdateAction()
        {
            if (playerInput.ZKeyReleased)
                player.TransitionToState(player.NormalState);

            if (cameraCoroutineFinished)
            {
                maskTansform.position = camera.WorldToScreenPoint(player.transform.position);
                player.StartCoroutine(SetOpacitySmoothly(0f, 0.8f));
                cameraCoroutineFinished = false;
            }
        }

        private IEnumerator SetOpacitySmoothly(float initial, float final)
        {
            float timer = 0f;
            float alpha = initial;

            while (alpha.Different(final)) 
            {
                alpha = Mathf.Lerp(alpha, final, timer / timeToLerp);
                timer += Time.deltaTime;

                Color color = blackImage.color;
                color.a = alpha;
                blackImage.color = color;

                yield return new WaitForEndOfFrame();
            }
        }

        private IEnumerator CameraGetCloser(float final)
        {
            return CameraGetCloser(camera.orthographicSize, final);
        }

        private IEnumerator CameraGetCloser(float initial, float final)
        {
            cameraCoroutineFinished = false;

            float timer = 0f;
            float size = initial;

            while (size.Different(final))
            {
                size = Mathf.Lerp(size, final, timer / timeToLerp);

                timer += Time.deltaTime;
                camera.orthographicSize = size;

                yield return new WaitForEndOfFrame();
            }

            cameraCoroutineFinished = true;
        }
    }
}