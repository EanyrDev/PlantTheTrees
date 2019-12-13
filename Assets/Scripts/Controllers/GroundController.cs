using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour
{
    [SerializeField] private AnimationCurve[] _rotationLayouts;


    private float _phase = 0;

    public void ResetRotation(bool activateNew)
    {
        if (activateNew)
        {
            Utils.Constants.CURRENT_ROTATION_LAYOUT = Random.Range(0, _rotationLayouts.Length);
        }
        _phase = 0;
    }

    void Update()
    {
        _phase += Time.deltaTime * Utils.Constants.PHASE_SPEED;

        if (_phase >= 5)
            _phase = 0;

        transform.Rotate(Vector3.forward * Time.deltaTime * Utils.Constants.ROTATION_SPEED * _rotationLayouts[Utils.Constants.CURRENT_ROTATION_LAYOUT].Evaluate(_phase));
    }
}
