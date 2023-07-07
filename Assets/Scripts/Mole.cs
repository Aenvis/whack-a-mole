using System;
using System.Collections;
using JetBrains.Annotations;
using UnityEngine;
using Random = UnityEngine.Random;

public class Mole : MonoBehaviour
{
    public bool IsStunned { get; set; }
    public bool IsHidden { get; set; }

    //Add rotatoing Stars
    public GameObject objectToDisplay;

    [CanBeNull] private Animator _animator;
    private Transform _transform;
    private const float YPositionUp = -0.46f;
    private const float YPositionDown = -1.835f;
    private const float TransitionDuration = 1.5f;
    private const float MinShowUpTime = 2f;
    private const float MaxShowUpTime = 4f;
    private const float MinHiddenTime = 1f;
    private const float MaxHiddenTime = 6f;

    


    private float _currentShowOrHideTime;

    private void Start()
    {
        TryGetComponent(out _animator);
        StartCoroutine(RandomizeAnimation());
        
        _transform = GetComponent<Transform>();

        var position = transform.position;
        _transform.position = new Vector3(position.x, YPositionUp, position.z);
        
        IsHidden = true;

        GameManager.Instance.OnGameStart += OnGameStart;
        GameManager.Instance.OnGameStop += OnGameStop;
        GameManager.Instance.OnExitPostGameScreen += OnExitPostGameScreen;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameStart -= OnGameStart;
        GameManager.Instance.OnGameStop -= OnGameStop;
        GameManager.Instance.OnExitPostGameScreen -= OnExitPostGameScreen;
    }

    private void FixedUpdate()
    {
        if (!GameManager.Instance.GameRunning) return;
        
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

    private void OnGameStart()
    {
        IsStunned = false;
        _currentShowOrHideTime = Random.Range(MinHiddenTime, MaxHiddenTime);
        StartCoroutine(ShowHideTransition(transform.position, YPositionDown));
    }

    private void OnGameStop()
    {
        StartCoroutine(ShowHideTransition(transform.position, YPositionUp));
    }

    private void OnExitPostGameScreen()
    {
        StartCoroutine(ShowHideTransition(transform.position, YPositionUp));
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

        var elapsed = 0f;
        while (Mathf.Abs(transform.position.y - destinatedY) > 0.01f)
        {
            transform.localPosition = Vector3.Lerp(startPoint, endPoint, elapsed / TransitionDuration);
            elapsed += Time.fixedDeltaTime;
            yield return null;
        }

        if (!GameManager.Instance.GameRunning) yield return null;
        
        // if (!IsStunned && !IsHidden) GameManager.Instance.MolesWhacked--;
        
        IsHidden = destinatedY < -1f;
        if (IsStunned && IsHidden) IsStunned = false;
    }

    public IEnumerator Stun()
    {
        // TODO: play stun animation
        IsStunned = true;
        var position = transform.position;

        _animator = GetComponent<Animator>();
        _animator.SetInteger("StunIndex", Random.Range(0, 2));
        _animator.SetTrigger("Stun");

        //Chat GPT

        float timer = 0f;
        float displayTime = 1f;

        objectToDisplay.SetActive(true); 

        while (timer < displayTime)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        objectToDisplay.SetActive(false);

        //End of GPT

        _transform.position = new Vector3(position.x, YPositionUp, position.z);
        yield return new WaitForSeconds(0f);
        yield return StartCoroutine(ShowHideTransition(position, YPositionDown));

        
    }
}