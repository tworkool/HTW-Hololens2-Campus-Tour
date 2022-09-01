using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MascotFollowerScript : MonoBehaviour
{
    // Prefab for virtual follower
    public GameObject _virtualFollowerPrefab;
    private Animator _virtualFollowerAnimator;
    private Renderer _virtualFollowerRenderer;

    // Max distance from camera (how far off the camera should be to start movement towards the camera)
    public float _cameraMaxLimit = 2f;
    // Min distance from the camera (how far off the mascot should come close to the camera to stop the movement)
    public float _cameraMinLimit = 0.5f;

    private Transform _hololensCameraTransform;

    private bool isWalking;
    public float speedFactor = 1f;

    // Start is called before the first frame update
    void Start()
    {
        _hololensCameraTransform = Camera.main.transform;
        _virtualFollowerAnimator = _virtualFollowerPrefab.GetComponent<Animator>();
        _virtualFollowerRenderer = _virtualFollowerPrefab.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // turn towards camera/player while visible by camera
        if (_virtualFollowerRenderer.isVisible)
        {
            _virtualFollowerPrefab.transform.LookAt(new Vector3(_hololensCameraTransform.position.x, _virtualFollowerPrefab.transform.position.y, _hololensCameraTransform.position.z));
        }

        if (isWalking)
        {
            // calculate direction towards camera and walk towards it
            Vector3 dir = (_hololensCameraTransform.position - _virtualFollowerPrefab.transform.position).normalized;
            dir *= speedFactor * Time.deltaTime;
            Vector3 newPos = new Vector3(_virtualFollowerPrefab.transform.position.x + dir.x, _virtualFollowerPrefab.transform.position.y, _virtualFollowerPrefab.transform.position.z + dir.z);
            _virtualFollowerPrefab.transform.position = newPos;

            if (IsMascotTooClose())
            {
                isWalking = false;
                _virtualFollowerAnimator.Play("Idle");
            }
        } else
        {
            if (IsMascotTooSlow())
            {
                isWalking = true;
                _virtualFollowerAnimator.Play("Walk");
            }
        }
    }

    private float GetFlatDistance(Vector3 a, Vector3 b)
    {
        var _a = new Vector2(a.x, a.z); 
        var _b = new Vector2(b.x, b.z);
        return Vector2.Distance(_a, _b);
    }

    private bool IsMascotTooSlow() => GetFlatDistance(_hololensCameraTransform.position, _virtualFollowerPrefab.transform.position) > _cameraMaxLimit;

    private bool IsMascotTooClose() => GetFlatDistance(_hololensCameraTransform.position, _virtualFollowerPrefab.transform.position) < _cameraMinLimit;
}
