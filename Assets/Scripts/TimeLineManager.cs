using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace TESF
{
    public class TimeLineManager : MonoBehaviour
    {
        [SerializeField]
        private Slider timeSlider;

        private List<TimeObject> _timeObjects = new List<TimeObject>();

        private void Awake()
        {
            _timeObjects = FindObjectsOfType<TimeObject>().ToList();
        }

        private void OnEnable()
        {
            timeSlider.onValueChanged.AddListener(delegate { TimeChanged(); });
        }

        private void OnDisable()
        {
            timeSlider.onValueChanged.RemoveListener(delegate { TimeChanged(); });
        }

        private void TimeChanged()
        {
            foreach (var timeObject in _timeObjects)
            {
                PointInTime pointToJumpTo = timeObject.currentPointInTime;

                foreach (var point in timeObject.pointsInTime)
                {
                    if (!point.isActiveAtTimestamp) continue;
                    if (timeSlider.value > point.timestamp)
                    {
                        pointToJumpTo = point;
                    }
                }

                timeObject.currentPointInTime = pointToJumpTo;
                timeObject.UpdateTimeObject();
            }
        }
    }
}
