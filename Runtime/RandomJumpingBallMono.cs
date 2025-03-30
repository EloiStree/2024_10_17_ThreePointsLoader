using System;
using System.Collections;
using UnityEngine;
using Random = System.Random;

public class RandomJumpingBallMono : MonoBehaviour
{
    public Rigidbody m_toAffect;
    public float m_jumpForce = 10;
    public ForceMode m_forceMode = ForceMode.Impulse;
    public bool m_useRandomPush=true;
    public float m_minInterval = 1;
    public float m_maxInterval = 2;

    private void Reset() {
        m_toAffect = GetComponent<Rigidbody>();
    }

    Coroutine m_coroutine;
    void OnEnable() {

        if (m_useRandomPush) {
            if (m_coroutine != null)
                StopCoroutine(m_coroutine);

            m_coroutine =  StartCoroutine(JumpLoop());
        }
    }

    private IEnumerator JumpLoop()
    {
        if (!m_useRandomPush)
            yield break;
        while (true)
        {
            
            Vector3 vector3 = UnityEngine.Random.insideUnitSphere;
            m_toAffect.AddForce(vector3 * m_jumpForce, m_forceMode);
            float randomWait = UnityEngine.Random.Range(m_minInterval, m_maxInterval);
            yield return new WaitForSeconds(randomWait);
        }

    }
}
