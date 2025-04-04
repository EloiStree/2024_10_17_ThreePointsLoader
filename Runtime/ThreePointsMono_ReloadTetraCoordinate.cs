using Eloi.ThreePoints;
using UnityEngine;

public class ThreePointsMono_ReloadTetraCoordinate : MonoBehaviour {

    public ThreePointsMono_Transform3 m_whereToApply;
    public ThreePointsTriangleDefault m_triangle;
    public Transform m_whatToMove;

    public STRUCT_TetrahedronLongSideFootCoordinateWithSource m_savedGiven;

    public void SetWith(STRUCT_TetrahedronLongSideFootCoordinateWithSource input)
    {
        m_savedGiven = input;
        ThreePointsTriangleDefault triangle = new ThreePointsTriangleDefault(m_whereToApply);
        m_triangle = triangle;
        m_triangle.GetLongestSideWithFrontCorner(
            out Vector3 shortSidePoint,
            out Vector3 longSidePoint,
            out Vector3 cornerPoint,
            out Vector3 footPoint,
            out Vector3 upDirection,
            out Vector3 forwardDirection,
            out Vector3 rightDirection
            );

        Debug.DrawLine(footPoint, footPoint + upDirection, Color.green, Time.deltaTime);
        Debug.DrawLine(footPoint, footPoint + forwardDirection, Color.blue, Time.deltaTime);
        Debug.DrawLine(footPoint, footPoint + rightDirection, Color.red, Time.deltaTime);

        Quaternion forwardRotationSpace = Quaternion.LookRotation(forwardDirection, Vector3.up);
        Vector3 worldPointSpace = footPoint;
        Eloi.RelocationUtility.GetLocalToWorld_DirectionalPoint(
           input.m_localPositionFromFoot,
           input.m_localRotationFromFoot,
           worldPointSpace,
           forwardRotationSpace,
           out Vector3 worldPosition,
           out Quaternion worldRotation
           );

        Debug.DrawLine(worldPointSpace, worldPosition, Color.yellow, Time.deltaTime);
        Debug.DrawLine(worldPosition, worldPosition + worldRotation * Vector3.forward, Color.blue,  Time.deltaTime);

        m_whatToMove.position = worldPosition;
        m_whatToMove.rotation = worldRotation;

    }
}
