using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSolutionObject : MonoBehaviour
{
    private TimeObject _timeObject;

    private Sprite _sprite;
    private Transform _startParentTransform;

    private bool _isActive;
    private BoxCollider _collider;
    
    private void Awake()
    {
        _startParentTransform = transform.parent;
        _timeObject = GetComponent<TimeObject>();
        _timeObject.TimeObjectUpdate.AddListener(OnTimeObjectUpdated);

        _collider = GetComponent<BoxCollider>();
    }

    private void OnTimeObjectUpdated()
    {
        _isActive = _timeObject.currentPointInTime.snapshotData.isActiveAtTimeStamp;
        _sprite = _timeObject.currentPointInTime.snapshotData.sprite;

        _collider.enabled = _isActive;
    }
}
