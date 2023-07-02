using System;
using System.Collections;
using JetBrains.Annotations;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public class Mole : MonoBehaviour
    {
        public bool IsStunned { get; set; }
        public bool IsHidden { get; set; }

        [CanBeNull] private Animator _animator;
        private Transform _transform;
        private const float YPositionUp = 0.14f;
        private const float YPositionDown = -1.2f;
        private const float TransitionDuration = 1f;
        private const float MinShowUpTime = 1f;
        private const float MaxShowUpTime = 3f;
        private const float MinHiddenTime = 1f;
        private const float MaxHiddenTime = 5f;

        private float _currentShowOrHideTime;

        private void Start()
        {
            TryGetComponent(out _animator);
            StartCoroutine(RandomizeAnimation());
            _transform = GetComponent<Transform>();
            _currentShowOrHideTime = Random.Range(MinHiddenTime, MaxHiddenTime);
            var position = transform.position;
            _transform.position = new Vector3(position.x, YPositionDown, position.z);
        }

        private void FixedUpdate()
        {
            if (_currentShowOrHideTime > 0)
            {
                _currentShowOrHideTime -= Time.fixedDeltaTime;
                return;
            }

            if (IsStunned) return;
            
            if (IsHidden)
            {
                StartCoroutine(ShowHideTransition(transform.position, YPositionUp));
                _currentShowOrHideTime = Random.Range(MinShowUpTime, MaxShowUpTime);
            }
            else
            {
                StartCoroutine(ShowHideTransition(transform.position, YPositionDown));
                _currentShowOrHideTime = Random.Range(MinHiddenTime, MaxHiddenTime);
            }
        }

        private IEnumerator RandomizeAnimation()
        {
            var delay = Random.Range(MinHiddenTime, MaxHiddenTime - 0.5f);

            yield return new WaitForSeconds(delay);
            _animator?.Play("idle");

            yield return null;
        }

        private IEnumerator ShowHideTransition(Vector3 startPoint, float destinatedY)
        {
            var endPoint = new Vector3(startPoint.x, destinatedY, startPoint.z);
            IsHidden = destinatedY < 0;

            var elapsed = 0f;
            while (Mathf.Abs(transform.position.y - destinatedY) > 0.05f)
            {
                transform.localPosition = Vector3.Lerp(startPoint, endPoint, elapsed / TransitionDuration);
                elapsed += Time.fixedDeltaTime;
                yield return null;
            }
        }

        public IEnumerator Stun()
        {
            // TODO: play stun animation
            var position = transform.position;
            _transform.position = new Vector3(position.x, YPositionUp, position.z);
            yield return new WaitForSeconds(1f);
            yield return StartCoroutine(ShowHideTransition(position, YPositionDown));
        }
    }
}
