using Eloi.ThreePoints;
using UnityEngine;
using UnityEngine.Events;
public class ThreePointsMono_SaveCurrentContextTransformSleepyCode : MonoBehaviour
{
    public ThreePointsMono_Transform3 m_context;
    public Transform m_targetTransform;
    public ThreePointsTriangleDefault m_lastTriangle;


    public Vector3 shortSidePoint;
    public Vector3 longSidePoint;
    public Vector3 cornerPoint;
    public Vector3 footPoint;
    public Vector3 upDirection;
    public Vector3 forwardDirection;
    public Vector3 rightDirection;
    public Quaternion localRotationOfPoint;
    public Vector3 localPositionOfPoint;

    public STRUCT_TetrahedronLongSideFootCoordinateWithSource m_saved;
    public UnityEvent<STRUCT_TetrahedronLongSideFootCoordinateWithSource> m_savedContext;

    public bool m_updateRefresh;

    private void Update()
    {
        if (m_updateRefresh)
        {
            Refresh(Time.deltaTime);
        }
    }




    [ContextMenu("Refresh")]
    public void RefreshInEditor() { }


    public void Refresh(float timeDraw) {
        ThreePointsTriangleDefault triangle = new ThreePointsTriangleDefault(m_context);
        m_lastTriangle = triangle;
        STRUCT_TetrahedronLongSideFootTransformPreBuild prebuild = new();
        triangle.GetThreePoints(out Vector3 worldPointA, out Vector3 worldPointB, out Vector3 worldPointC);
        prebuild.SetWith(worldPointA, worldPointB, worldPointC, m_targetTransform);
        m_lastTriangle.GetLongestSideWithFrontCorner(
            out shortSidePoint,
            out longSidePoint,
            out cornerPoint,
            out footPoint,
            out upDirection,
            out forwardDirection,
            out rightDirection
            );

        Debug.DrawLine(footPoint, footPoint + upDirection, Color.green, timeDraw);
        Debug.DrawLine(footPoint, footPoint + forwardDirection, Color.blue, timeDraw);
        Debug.DrawLine(footPoint, footPoint + rightDirection, Color.red, timeDraw);
        Quaternion forwardRotationSpace = Quaternion.LookRotation(forwardDirection, Vector3.up);
        Vector3 worldPointSpace = footPoint;

        Eloi.RelocationUtility.GetWorldToLocal_DirectionalPoint(
            m_targetTransform.position,
            m_targetTransform.rotation,
            worldPointSpace,
            forwardRotationSpace,
            out localPositionOfPoint,
            out localRotationOfPoint
            );

        Debug.DrawLine(Vector3.zero, localPositionOfPoint, Color.yellow, timeDraw);
        Debug.DrawLine(localPositionOfPoint, localPositionOfPoint+ localRotationOfPoint * Vector3.forward, Color.blue, timeDraw);

        Eloi.RelocationUtility.GetLocalToWorld_DirectionalPoint(
            localPositionOfPoint,
            localRotationOfPoint,
            worldPointSpace,
            forwardRotationSpace,
            out Vector3 worldPosition,
            out Quaternion worldRotation
            );

        Debug.DrawLine(worldPointSpace, worldPosition, Color.yellow, timeDraw);
        Debug.DrawLine(worldPosition, worldPosition + worldRotation * Vector3.forward, Color.blue, timeDraw);

        m_saved.m_worldPointA = worldPointA;
        m_saved.m_worldPointB = worldPointB;
        m_saved.m_worldPointC = worldPointC;
        m_saved.m_localPositionFromFoot = localPositionOfPoint;
        m_saved.m_localRotationFromFoot = localRotationOfPoint;
        m_savedContext.Invoke(m_saved);
    }
}

public  static class TetrahedronUtility {


    public static void GetTetrahedronLongFootCoordinate(
        STRUCT_TetrahedronLongSideFootTransformPreBuild input,
        out STRUCT_TetrahedronLongSideFootCoordinate result)
    {

        ThreePointsTriangleDefault triangle = new ThreePointsTriangleDefault();
        triangle.SetThreePoints(input.m_worldTrianglePointA, input.m_worldTrianglePointB, input.m_worldTrianglePointC);
        throw new System.NotImplementedException();
    }
}

/// <summary>
/// Make a line from the opposite corner to the longest line. From this line, you can create a front line on the bigest side of the triangle.
/// Now that you have a top and front line, you can create a plane. From the cross section of the line, You can create a 3D space to the "right".
/// Relocate the Unity3D world rotation and position in that space. 
/// </summary>
[System.Serializable]
public struct STRUCT_TetrahedronLongSideFootCoordinate
{
    /// <summary>
    /// Represent  the position from the boot a perpendicular line to the longest side of the triangle on the short side of the triangle to the right
    /// </summary>
    public Vector3 m_localPositionFromFoot;

    public Quaternion m_localRotationFromFoot;
}

[System.Serializable]
public struct STRUCT_TetrahedronLongSideFootCoordinateWithSource
{
    public Vector3 m_worldPointA;
    public Vector3 m_worldPointB;
    public Vector3 m_worldPointC;
    /// <summary>
    /// Represent  the position from the boot a perpendicular line to the longest side of the triangle on the short side of the triangle to the right
    /// </summary>
    public Vector3 m_localPositionFromFoot;

    public Quaternion m_localRotationFromFoot;
}



// Represents the triangle and the transform point in Unity3D space before beeing transform in STRUCT_TetrahedronLongSideFootTransform

[System.Serializable]
public struct STRUCT_TetrahedronLongSideFootTransformPreBuild
{
    public Vector3 m_worldTrianglePointA;
    public Vector3 m_worldTrianglePointB;
    public Vector3 m_worldTrianglePointC; 
    public Vector3 m_worldPositionTransform;
    public Quaternion m_worldRotationTransform;

    public void SetWith(Vector3 pointA, Vector3 pointB, Vector3 pointC, Transform target)
    {
        m_worldTrianglePointA = pointA;
        m_worldTrianglePointB = pointB;
        m_worldTrianglePointC = pointC;
        m_worldPositionTransform = target.position;
        m_worldRotationTransform = target.rotation;
    }
    public void SetWith(Vector3 pointA, Vector3 pointB, Vector3 pointC, Vector3 worldPointPosition, Quaternion worldPointRotation)
    {
        m_worldTrianglePointA = pointA;
        m_worldTrianglePointB = pointB;
        m_worldTrianglePointC = pointC;
        m_worldPositionTransform = worldPointPosition;
        m_worldRotationTransform = worldPointRotation;
    }


}
