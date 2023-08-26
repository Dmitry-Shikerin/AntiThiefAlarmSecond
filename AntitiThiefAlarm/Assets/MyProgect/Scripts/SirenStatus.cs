using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SirenStatus : MonoBehaviour
{
    [SerializeField] private AlarmTrigger _trigger;

    private WaitForSeconds _waitForSeconds = new WaitForSeconds(2f);
    private float _maxValue = 1;
    private float _minValue = 0;
    private float _recoveryRate = 0.1f;
    private Coroutine _coroutine;
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _audioSource.volume = _minValue;
    }

    private void OnEnable()
    {
        _trigger.Entered += Activate;
        _trigger.Exitered += Deactivate;
    }

    private void OnDisable()
    {
        _trigger.Entered -= Activate;
        _trigger.Exitered -= Deactivate;
    }

    private void Activate()
    {
        StopCoroutine();
        _coroutine = StartCoroutine(ChangeVolume(_maxValue));
    }

    private void Deactivate()
    {
        StopCoroutine();
        _coroutine = StartCoroutine(ChangeVolume(_minValue));
    }

    private void StopCoroutine()
    {
        if (_coroutine == null)
        {
            return;
        }

        StopCoroutine(_coroutine);
    }

    private IEnumerator ChangeVolume(float target)
    {
        _audioSource.Play();

        while (Mathf.Abs(_audioSource.volume - target) > Mathf.Epsilon)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, target, _recoveryRate);

            yield return _waitForSeconds;
        }

        if (_audioSource.volume < 0)
        {
            _audioSource.Stop();
        }
    }
}

