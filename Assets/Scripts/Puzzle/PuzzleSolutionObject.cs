using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSolutionObject : MonoBehaviour
{
    [SerializeField]
    private Sprite completedSprite;
    [SerializeField]
    private AudioClip completedClip;

    [SerializeField]
    private GameObject _soundEmmiterPrefab;

    private TimeObject _timeObject;

    private Transform _startParentTransform;

    private bool _isActive;
    private BoxCollider2D _collider;
    
    private void Awake()
    {
        _startParentTransform = transform.parent;
        _timeObject = GetComponent<TimeObject>();
        _timeObject.TimeObjectUpdate.AddListener(OnTimeObjectUpdated);

        _collider = GetComponent<BoxCollider2D>();
    }

    private void OnTimeObjectUpdated()
    {
        _isActive = _timeObject.currentPointInTime.snapshotData.isActiveAtTimeStamp;
        _collider.enabled = _isActive;
    }

    public void Completed()
    {
        _timeObject.visuals.sprite = completedSprite;
        _timeObject.shouldUpdateVisuals = false;
        GameObject newSoundEmmiter = Instantiate(_soundEmmiterPrefab);

        newSoundEmmiter.GetComponent<SoundEmmiter>().PlayAudio(completedClip);
        Puzzlemanager.Instance.PuzzleSolved();
    }


}
