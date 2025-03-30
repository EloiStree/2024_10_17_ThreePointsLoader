using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Eloi.ThreePoints
{


    public abstract class ThreePointsMono_AbstractEdgesSizeToAction<T,G> : MonoBehaviour 
    where T:AbstractEdgesSizeAction<G>{

        public UnityEvent<G> m_onActionDetected;
        public UnityEvent<I_ThreePoints, G> m_onActionDetectedWithSource;
        public List<T> m_actions = new List<T>();

        public void SetWith(I_ThreePointsGet triangle) {

            ThreePointsTriangleDefault received = new ThreePointsTriangleDefault(triangle);
            foreach (var item in m_actions)
            {
                bool hasSameEdge = ThreePointsUtility.HasAlmostTheSameEdge(
                    received,
                    item.m_edges.m_distanceA,
                    item.m_edges.m_distanceB,
                    item.m_edges.m_distanceC,
                    item.m_edgeToleranceInMeter

                    );
                if (hasSameEdge)
                {
                    m_onActionDetected.Invoke(item.m_parameter);
                }
            }
        }
    }

    [System.Serializable]
    public class AbstractEdgesSizeAction<T>
    {

        [ContextMenuItem("Set To A4", "SetToA4")]
        [ContextMenuItem("Set To A3", "SetToA3")]
        [ContextMenuItem("Set To A2", "SetToA2")]
        [ContextMenuItem("Set To A1", "SetToA1")]
        [ContextMenuItem("Set To A0", "SetToA0")]
        [ContextMenuItem("Set To A5", "SetToA5")]
        [ContextMenuItem("Set To A6", "SetToA6")]
        [ContextMenuItem("Set To A7", "SetToA7")]
        [ContextMenuItem("Set To A8", "SetToA8")]
        [ContextMenuItem("Set To A9", "SetToA9")]
        [ContextMenuItem("Set To A10", "SetToA10")]
        [ContextMenuItem("Set To Tennis Table", "SetToTennisTable")]
        public T m_parameter;
        public float m_edgeToleranceInMeter = 0.05f;
        public STRUCT_EdgeDistanceABC m_edges;

        public void SetToTennisTable() {

            m_edges.m_distanceA = 1.525f;
            m_edges.m_distanceB = 2.74f;
            m_edges.m_distanceC = 4.575f;
        }

        public void SetToA0()
        {
            APaper.GetDimensionMeter(APaperEnum.A0, out m_edges.m_distanceA, out m_edges.m_distanceB, out m_edges.m_distanceC);
        }
        public void SetToA1()
        {
            APaper.GetDimensionMeter(APaperEnum.A1, out m_edges.m_distanceA, out m_edges.m_distanceB, out m_edges.m_distanceC);
        }
        public void SetToA2()
        {
            APaper.GetDimensionMeter(APaperEnum.A2, out m_edges.m_distanceA, out m_edges.m_distanceB, out m_edges.m_distanceC);
        }
        public void SetToA3()
        {
            APaper.GetDimensionMeter(APaperEnum.A3, out m_edges.m_distanceA, out m_edges.m_distanceB, out m_edges.m_distanceC);
        }

        public void SetToA4()
        {
            APaper.GetDimensionMeter(APaperEnum.A4, out m_edges.m_distanceA, out m_edges.m_distanceB, out m_edges.m_distanceC);
        }
        public void SetToA5()
        {
            APaper.GetDimensionMeter(APaperEnum.A5, out m_edges.m_distanceA, out m_edges.m_distanceB, out m_edges.m_distanceC);
        }
        public void SetToA6()
        {
            APaper.GetDimensionMeter(APaperEnum.A6, out m_edges.m_distanceA, out m_edges.m_distanceB, out m_edges.m_distanceC);
        }
        public void SetToA7()
        {
            APaper.GetDimensionMeter(APaperEnum.A7, out m_edges.m_distanceA, out m_edges.m_distanceB, out m_edges.m_distanceC);
        }
        public void SetToA8()
        {
            APaper.GetDimensionMeter(APaperEnum.A8, out m_edges.m_distanceA, out m_edges.m_distanceB, out m_edges.m_distanceC);
        }
        public void SetToA9()
        {
            APaper.GetDimensionMeter(APaperEnum.A9, out m_edges.m_distanceA, out m_edges.m_distanceB, out m_edges.m_distanceC);
        }
        public void SetToA10()
        {
            APaper.GetDimensionMeter(APaperEnum.A10, out m_edges.m_distanceA, out m_edges.m_distanceB, out m_edges.m_distanceC);
        }

    }

    [System.Serializable]
    public class EdgesSizeUnityEvent : AbstractEdgesSizeAction<UnityEvent>
    {
    }
    [System.Serializable]
    public class EdgesSizeActionInt : AbstractEdgesSizeAction<int>
    {
    }
    [System.Serializable]
    public class EdgesSizeActionPrefab : AbstractEdgesSizeAction<GameObject>
    {
    }
    [System.Serializable]
    public class EdgesSizeActionTransform : AbstractEdgesSizeAction<Transform>
    {
    }

    [System.Serializable]
    public class EdgesSizeActionString : AbstractEdgesSizeAction<string>
    {
    }
    public class SleepyCode_LoadSceneFromTriangleSubmit : MonoBehaviour
{
        public void Reset()
        {
            APaper.GetDimensionMeter(APaperEnum.A4, out m_squareWidth, out m_squareHeight, out m_squareDiagonal);
        }
        //A4 210 x 297 mm
        public float m_squareWidth;
        public float m_squareHeight;
        public float m_squareDiagonal;
        public float m_tolerance = 0.03f;

        public ThreePointsTriangleDefault m_received;
        public bool m_hasSameEdge = false;
        public string m_sceneToLoad = "SceneToLoad";
        public void SetWith(Eloi.ThreePoints.I_ThreePointsGet triangle) {

            m_received  = new ThreePointsTriangleDefault(triangle);
            m_hasSameEdge = ThreePointsUtility.HasAlmostTheSameEdge(m_received, m_squareWidth, m_squareHeight, m_squareDiagonal, m_tolerance);
            if (m_hasSameEdge)
            {
                Debug.Log("Has the same edge");
                UnityEngine.SceneManagement.SceneManager.LoadScene(m_sceneToLoad);
            }
            else
            {
                Debug.Log("Has not the same edge");
            }
        }
}

}