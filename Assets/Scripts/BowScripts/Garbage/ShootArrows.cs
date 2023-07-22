using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootArrows : MonoBehaviour
{
    [SerializeField]
    private GameObject _arrowPrefab;

    [SerializeField]
    private Transform _arrowTransform;

    [SerializeField]
    private float _maximumArrowForce = 200f;

    private AudioSource _bowStretchSound;
    private float _currentArrowForce;

    private float _maxLerpTime = 1f;
    private float _currentLerpTime;

    private Animator _bowAnimator;

    // Start is called before the first frame update
    void Start()
    {
        _bowAnimator = GetComponent<Animator>();
        _bowStretchSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.GameStarted)
        {
            if (Input.GetMouseButtonDown(0))
            {
                DrawBow();
            }

            if (Input.GetMouseButton(0))
            {
                PowerUpBow();
            }

            if (Input.GetMouseButtonUp(0))
            {
                ReleaseBow();
            }
        }
        else if (!GameManager.GameStarted)
        {
            _bowAnimator.SetBool("drawing", false);
            _currentArrowForce = 0;
            _bowStretchSound.Stop();
        }
    }

    private void DrawBow()
    {
        _bowAnimator.SetBool("drawing", true);
        _bowStretchSound.Play();
    }

    private void PowerUpBow()
    {
        if (_bowAnimator.GetBool("drawing"))
        {
            _currentLerpTime += Time.deltaTime;

            if (_currentLerpTime > _maxLerpTime)
            {
                _currentLerpTime = _maxLerpTime;
            }

            float perc = _currentLerpTime / _maxLerpTime;
            _currentArrowForce = Mathf.Lerp(0, _maximumArrowForce, perc);
        }
    }

    private void ReleaseBow()
    {
        _bowAnimator.SetBool("drawing", false);

        GameObject arrow = Instantiate(_arrowPrefab, _arrowTransform.position, _arrowTransform.rotation) as GameObject;
        arrow.GetComponent<Rigidbody>().AddForce(_arrowTransform.forward * _currentArrowForce);

        _currentLerpTime = 0;
        _currentArrowForce = 0;

        _bowStretchSound.Stop();
    }
}
