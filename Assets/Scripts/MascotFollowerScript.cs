using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MascotFollowerScript : MonoBehaviour
{
    // Prefab for virtual follower
    public GameObject _virtualFollowerPrefab;

    // Max distance from camera (how far off the camera should be to start movement towards the camera)
    public float _maxCameraDistance = 5f;

    // Min distance from the camera (how far off the mascot should come close to the camera to stop the movement)
    public float _minCameraDistance = 2f;

    //public RangeAttribute _cameraDistanceLimits = new RangeAttribute(2f, 5f);

    private Transform _hololensCameraTransform;

    public Animation _walkAnimation;
    public Animation _idleAnimation;

    // Start is called before the first frame update
    void Start()
    {
        _hololensCameraTransform = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsMascotTooSlow())
        {
            // Walk towards Camera
        } else if (IsMascotTooClose()) {
            // Stop Walking
        }
    }

    private float GetFlatDistance(Vector3 a, Vector3 b)
    {
        var _a = new Vector2(a.x, a.z); 
        var _b = new Vector2(b.x, b.z);
        return Vector2.Distance(_a, _b);
    }

    private bool IsMascotTooSlow() => GetFlatDistance(_hololensCameraTransform.position, _virtualFollowerPrefab.transform.position) > _maxCameraDistance;

    private bool IsMascotTooClose() => GetFlatDistance(_hololensCameraTransform.position, _virtualFollowerPrefab.transform.position) < _minCameraDistance;
}
