using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TimeObject : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private SpriteRenderer visuals;
    [SerializeField]
    private SpriteRenderer oldVisuals;

    [Header("------------------------------------------------------------------------------------------")]
    public PointInTime currentPointInTime;


    [Space(20)]
    public List<PointInTime> pointsInTime = new List<PointInTime>();


    [Header("------------------------------------------------------------------------------------------")]
    [Header("SnapshotData")]
    [SerializeField]
    private bool editMode;

    [Space(20)]
    [SerializeField]
    private float snapshotTime;
    [SerializeField]
    public bool SnapshotIsActiveAtTimeStamp;
    [SerializeField]
    public Sprite SnapshotSprite;
    [Space(20)]
    [SerializeField]
    public Vector3 SnapshotPosition;

    [ExecuteInEditMode]
    private void OnValidate()
    {
        if (editMode)
        {
            FindObjectOfType<TESF.TimeLineManager>().timeSlider.value = snapshotTime;
            visuals.sprite = SnapshotSprite;
            SnapshotPosition = this.transform.position;
        }
        else if(pointsInTime.Count > 0)
        {
            currentPointInTime = pointsInTime[0];
            currentPointInTime.snapshotData.isActiveAtTimeStamp = pointsInTime[0].snapshotData.isActiveAtTimeStamp;
            visuals.sprite = pointsInTime[0].snapshotData.sprite;
            oldVisuals.color = new Color(oldVisuals.color.r, oldVisuals.color.g, oldVisuals.color.b, 0);
            visuals.color = new Color(visuals.color.r, visuals.color.g, visuals.color.b, 1);
            transform.position = pointsInTime[0].snapshotData.goalPosition;
            FindObjectOfType<TESF.TimeLineManager>().timeSlider.value = 0;
        }

        if (pointsInTime.Count > 0)
        {
            pointsInTime = pointsInTime.OrderBy(point => point.timeStamp).ToList();
        }
    }

    private void Awake()
    {
        LeanTween.alpha(oldVisuals.gameObject, 0, 0);
        LeanTween.alpha(visuals.gameObject, 1, 0);

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
        LeanTween.cancel(gameObject);
        if (currentPointInTime.snapshotData.isActiveAtTimeStamp)
        {
            oldVisuals.sprite = visuals.sprite;
            visuals.sprite = currentPointInTime.snapshotData.sprite;

            oldVisuals.color = new Color(oldVisuals.color.r, oldVisuals.color.g, oldVisuals.color.b, 1);
            LeanTween.alpha(oldVisuals.gameObject, 0, .2f);

            visuals.color = new Color(visuals.color.r, visuals.color.g, visuals.color.b, 0);
            LeanTween.alpha(visuals.gameObject, 1, .2f);
        }
        else
        {
            LeanTween.alpha(visuals.gameObject, 0, .2f);
        }

        LeanTween.move(gameObject, currentPointInTime.snapshotData.goalPosition, .5f);
    }
}
