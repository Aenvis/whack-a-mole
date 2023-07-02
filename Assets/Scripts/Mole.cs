using System;
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
        private const float yPositionUp = 0.139f;
        private float yPositionDown = -0.85f;
        private const float MinShowUpTime = 1f;
        private const float MaxShowUpTime = 3f;
        private const float MinHiddenTime = 1f;
        private const float MaxHiddenTime = 5f;

        private float _currentShowOrHideTime;

        private void Start()
        {
            TryGetComponent(out _animator);
            _transform = GetComponent<Transform>();
            _currentShowOrHideTime = Random.Range(MinHiddenTime, MaxHiddenTime);
            Hide();
        }

        private void FixedUpdate()
        {
            if (_currentShowOrHideTime > 0)
            {
                _currentShowOrHideTime -= Time.fixedDeltaTime;
                return;
            }
            
            if (IsHidden)
            {
                Show();
                _currentShowOrHideTime = Random.Range(MinShowUpTime, MaxShowUpTime);
            }
            else
            {
                Hide();
                _currentShowOrHideTime = Random.Range(MinHiddenTime, MaxHiddenTime);
            }
        }

        public void Show()
        {
            IsHidden = false;
            var p = _transform.position;
            _transform.position = new Vector3(p.x, yPositionUp, p.z);
        }

        public void Hide()
        {
            IsHidden = true;
            var p = _transform.position;
            _transform.position = new Vector3(p.x, yPositionDown, p.z);
        }

        public void Stun()
        {
            
        }
    }
}