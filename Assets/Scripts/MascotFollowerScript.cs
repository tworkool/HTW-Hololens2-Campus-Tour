using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MascotFollowerScript : MonoBehaviour
{
    private Animator _virtualFollowerAnimator;
    private Renderer _virtualFollowerRenderer;
    private AudioSource _virtualFollowerAudioSource;

    // Max distance from camera (how far off the camera should be to start movement towards the camera)
    public float _cameraMaxLimit = 2f;
    // Min distance from the camera (how far off the mascot should come close to the camera to stop the movement)
    public float _cameraMinLimit = 1f;

    private Transform _hololensCameraTransform;

    private bool isWalking;
    public float speedFactor = 0.75f;
    private float walkingStartDistance = 0;

    // Start is called before the first frame update
    void Start()
    {
        _hololensCameraTransform = Camera.main.transform;
        _virtualFollowerAnimator = GetComponent<Animator>();
        _virtualFollowerRenderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // turn towards camera/player while visible by camera
        if (_virtualFollowerRenderer.isVisible)
        {
            transform.LookAt(new Vector3(_hololensCameraTransform.position.x, transform.position.y, _hololensCameraTransform.position.z));
        }

        if (isWalking)
        {
            // calculate direction towards camera and walk towards it
            Vector3 dir = (_hololensCameraTransform.position - transform.position).normalized;

            //// calculate easing
            //float distance = GetFlatDistance(_hololensCameraTransform.position, _virtualFollowerPrefab.transform.position);
            //float normalizedDistance = GetNormalizedValue(distance, walkingStartDistance, 0, 0f, 1f);
            //float speed = speedFactor * Time.deltaTime * (1 + Mathf.Sin(normalizedDistance * 2 * 3.14159f));
            dir *= speedFactor * Time.deltaTime;
            Vector3 newPos = new Vector3(transform.position.x + dir.x, transform.position.y, transform.position.z + dir.z);
            transform.position = newPos;

            if (IsMascotTooClose())
            {
                isWalking = false;
                _virtualFollowerAnimator.Play("Idle");
            }
        }
        else
        {
            if (IsMascotTooSlow())
            {
                isWalking = true;
                //walkingStartDistance = GetFlatDistance(_hololensCameraTransform.position, _virtualFollowerPrefab.transform.position);
                _virtualFollowerAnimator.Play("Walk");
            }
        }
    }

    //private float GetNormalizedValue(float value, float istart, float istop, float ostart, float ostop) => ostart + (ostop - ostart) * ((value - istart) / (istop - istart));


    private float GetFlatDistance(Vector3 a, Vector3 b)
    {
        var _a = new Vector2(a.x, a.z);
        var _b = new Vector2(b.x, b.z);
        return Vector2.Distance(_a, _b);
    }

    private bool IsMascotTooSlow() => GetFlatDistance(_hololensCameraTransform.position, transform.position) > _cameraMaxLimit;

    private bool IsMascotTooClose() => GetFlatDistance(_hololensCameraTransform.position, transform.position) < _cameraMinLimit;
}
