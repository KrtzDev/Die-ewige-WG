using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace TESF
{
    public class TimeLineManager : MonoBehaviour
    {
        public Slider timeSlider;

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
                    if (timeSlider.value > point.timeStamp)
                    {
                        pointToJumpTo = point;
                    }
                }
                if (timeObject.currentPointInTime != pointToJumpTo)
                {
                    timeObject.currentPointInTime = pointToJumpTo;
                    timeObject.UpdateTimeObject();
                }
            }
        }
    }
}
