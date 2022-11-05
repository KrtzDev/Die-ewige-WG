using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TimeObject : MonoBehaviour
{
    public List<PointInTime> pointsInTime = new List<PointInTime>();

    [SerializeField]
    public PointInTime currentPointInTime;

    [Space(20)]
    [SerializeField]
    private SpriteRenderer visuals;
    [SerializeField]
    private SpriteRenderer oldVisuals;

    private void OnValidate()
    {
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
            if (point.goalPosition == Vector3.zero)
            {
                point.goalPosition = transform.position;
            }
            if (point.isActiveAtTimeStamp && !point.sprite)
            {
                point.sprite = visuals.sprite;
            }
        }

        if (pointsInTime.Count > 0)
        {
            currentPointInTime = pointsInTime[0];
        }
    }

    public void UpdateTimeObject()
    {
        LeanTween.cancel(gameObject);
        if (currentPointInTime.isActiveAtTimeStamp)
        {
            oldVisuals.sprite = visuals.sprite;
            visuals.sprite = currentPointInTime.sprite;

            oldVisuals.color = new Color(oldVisuals.color.r, oldVisuals.color.g, oldVisuals.color.b, 1);
            LeanTween.alpha(oldVisuals.gameObject, 0, .2f);

            visuals.color = new Color(visuals.color.r, visuals.color.g, visuals.color.b, 0);
            LeanTween.alpha(visuals.gameObject, 1, .2f);
        }
        else
        {
            LeanTween.alpha(visuals.gameObject, 0, .2f);
        }

        LeanTween.move(gameObject, currentPointInTime.goalPosition, .5f);
    }

    public void Activate()
    {
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
