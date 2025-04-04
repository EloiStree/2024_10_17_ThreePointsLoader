using System;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// I am a classe checking if the back part of a control is pressed and it is not moving. Meaning that we want to calibratre a point of the triangle.
/// The idea here is to stick the controller à tri points in the room to load easily and in a None Geek concept.
/// </summary>
public class ThreePointsMono_PressingAndNotMovingDetector : MonoBehaviour
{
    public bool m_isPressing;
    public bool m_isNotMoving;
    public float m_isNotMovingTimer=2;

    public Transform m_targetObserved;
    public float m_notMovingDeathzone=0.01f;
    public Vector3 m_deathZoneLastPoint;

    public NotMovingEvent[] m_notMovingEvents = new NotMovingEvent[] {

        new NotMovingEvent(){ m_timeToBeTriggerAsNotMoving=2}
    };

    [System.Serializable]
    public class NotMovingEvent { 
        public float m_timeToBeTriggerAsNotMoving;
        public bool m_notMoving;
        public UnityEvent<Transform> m_onIsNotMovingTrue;
        public string m_lastTriggered;
    }

    public void SetAsPressing(bool isPressingTouch) { 
    
        m_isPressing = isPressingTouch;
    }
    public void SetAsPressingTrue() => SetAsPressing(true);
    public void SetAsPressingFalse() => SetAsPressing(false);


    public void Update()
    {

        Vector3 currentPosition = transform.position;
        bool outOfRange = Vector3.Distance(m_deathZoneLastPoint, currentPosition) > m_notMovingDeathzone;
        m_isNotMoving = !outOfRange;
        if (outOfRange)
        {
            m_deathZoneLastPoint = currentPosition;
        }
        


        float previousTime = m_isNotMovingTimer;

        if (m_isNotMoving && m_isPressing)
        {

            m_isNotMovingTimer += Time.deltaTime;
        }
        else {
            m_isNotMovingTimer = 0;
        }
        float currentTime = m_isNotMovingTimer;
        if (currentTime <= 0) return;

        foreach (NotMovingEvent notMoving in m_notMovingEvents) {
            bool isNotMoving = previousTime < notMoving.m_timeToBeTriggerAsNotMoving && currentTime >= notMoving.m_timeToBeTriggerAsNotMoving;
            if (isNotMoving!= notMoving.m_notMoving) {
                notMoving.m_lastTriggered = DateTime.UtcNow.ToString();
                notMoving.m_onIsNotMovingTrue.Invoke(m_targetObserved);            
            }
        }

    }

}
