using UnityEngine;

public class ThreePointsMono_ForTestingMoveOnTick : MonoBehaviour {

    public Transform[] m_listOfPoints;
    public Transform m_whatToMove;
    public int m_index;
    public bool m_useRotation = true;

    [ContextMenu("Move Next")]
    public void MoveNext()
    {
        if (m_listOfPoints.Length <= 0)
            return;

        m_index = (m_index + 1) % m_listOfPoints.Length;
        MoveToIndex();

    }

    [ContextMenu("Move Previous")]
    public void MovePrevious()
    {
        if (m_listOfPoints.Length <= 0) 
            return;
        
        m_index -= 1;
        if (m_index < 0) { 
            m_index = m_listOfPoints.Length-1;
        }
        MoveToIndex();

    }

    [ContextMenu("Move To Index")]
    private void MoveToIndex()
    {
        if (m_whatToMove == null) 
            return;
        if (m_listOfPoints.Length<=0)
            return;
        m_whatToMove.transform.position = m_listOfPoints[m_index].position;
        if (m_useRotation)
            m_whatToMove.transform.rotation = m_listOfPoints[m_index].rotation;

    }
}
