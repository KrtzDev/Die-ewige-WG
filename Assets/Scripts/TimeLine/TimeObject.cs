using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class TimeObject : MonoBehaviour
{
    [HideInInspector]
    public UnityEvent TimeObjectUpdate;

    [Header("References")]
    [SerializeField]
    public SpriteRenderer visuals;
    [SerializeField]
    private SpriteRenderer oldVisuals;

    public bool shouldUpdateVisuals;

    [Header("------------------------------------------------------------------------------------------")]
    public PointInTime currentPointInTime;

    [Space(20)]
    public List<PointInTime> pointsInTime = new List<PointInTime>();

    [Header("------------------------------------------------------------------------------------------")]
    [Header("SnapshotData")]
    [SerializeField]
    public bool editMode;

    [Space(20)]
    [Range(1895, 2018)]
    [SerializeField]
    private float snapshotTime;
    [SerializeField]
    public bool SnapshotIsActiveAtTimeStamp;
    [SerializeField]
    public Sprite SnapshotSprite;
    [Space(20)]
    [SerializeField]
    public Vector3 SnapshotPosition;

    private void OnValidate()
    {
        if (editMode)
        {
            FindObjectOfType<TESF.TimeLineManager>().timeSlider.value = snapshotTime;
            visuals.sprite = SnapshotSprite;
            SnapshotPosition = this.transform.position;
        }
        else if (pointsInTime.Count > 0)
        {
            currentPointInTime = pointsInTime[0];
            currentPointInTime.snapshotData.isActiveAtTimeStamp = pointsInTime[0].snapshotData.isActiveAtTimeStamp;
            visuals.sprite = pointsInTime[0].snapshotData.sprite;
            oldVisuals.color = new Color(oldVisuals.color.r, oldVisuals.color.g, oldVisuals.color.b, 0);
            visuals.color = new Color(visuals.color.r, visuals.color.g, visuals.color.b, 1);
            transform.position = pointsInTime[0].snapshotData.goalPosition;
            FindObjectOfType<TESF.TimeLineManager>().timeSlider.value = 1895f;
        }

        if (pointsInTime.Count > 0)
        {
            pointsInTime = pointsInTime.OrderBy(point => point.timeStamp).ToList();
        }
    }

    private void Awake()
    {
        if (pointsInTime.Count > 0 && pointsInTime[0].snapshotData.isActiveAtTimeStamp)
        {
            oldVisuals.color = new Color(oldVisuals.color.r, oldVisuals.color.g, oldVisuals.color.b, 0);
            visuals.color = new Color(visuals.color.r, visuals.color.g, visuals.color.b, 1);
        }
        else
        {
            oldVisuals.color = new Color(oldVisuals.color.r, oldVisuals.color.g, oldVisuals.color.b, 0);
            visuals.color = new Color(visuals.color.r, visuals.color.g, visuals.color.b, 0);
        }

        foreach (var point in pointsInTime)
        {
            if (point.snapshotData.goalPosition == Vector3.zero)
            {
                point.snapshotData.goalPosition = transform.position;
            }
            if (point.snapshotData.isActiveAtTimeStamp && !point.snapshotData.sprite)
            {
                point.snapshotData.sprite = visuals.sprite;
            }
        }

        if (pointsInTime.Count > 0)
        {
            currentPointInTime = pointsInTime[0];
            UpdateTimeObject();
        }
    }

    public void UpdateTimeObject()
    {
        TimeObjectUpdate.Invoke();

        LeanTween.cancel(gameObject);
        if (currentPointInTime.snapshotData.isActiveAtTimeStamp)
        {
            if (true)
            {
                oldVisuals.sprite = visuals.sprite;
                visuals.sprite = currentPointInTime.snapshotData.sprite;
            }

            if (visuals.color.a != 0)
            {
                oldVisuals.color = new Color(oldVisuals.color.r, oldVisuals.color.g, oldVisuals.color.b, 1);
            }
            LeanTween.alpha(oldVisuals.gameObject, 0, .2f);

            visuals.color = new Color(visuals.color.r, visuals.color.g, visuals.color.b, 0);
            LeanTween.alpha(visuals.gameObject, 1, .2f);
        }
        else
        {
            LeanTween.alpha(visuals.gameObject, 0, .2f);
            LeanTween.alpha(oldVisuals.gameObject, 0, .2f);
        }

        LeanTween.move(gameObject, currentPointInTime.snapshotData.goalPosition, .5f);
    }

    public void Deactivate()
    {
        enabled = false;
        visuals.enabled = false;
        oldVisuals.enabled = false;
    }

    public void Activate()
    {
        enabled = true;
        visuals.enabled = true;
        oldVisuals.enabled = true;
    }
}
