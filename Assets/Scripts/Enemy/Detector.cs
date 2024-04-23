using UnityEngine;

public class Detector : MonoBehaviour
{
    [SerializeField] private float detectorSize;
    [SerializeField] private Transform detectorPosition;
    [SerializeField] private LayerMask layerToDetect;

    public bool isInDetectArea()
    {
        bool isInArea = GetCollidersInDetectAreaSphere().Length > 0;     
        return isInArea;
    }

    public Collider[] GetCollidersInDetectAreaSphere()
    {
        return Physics.OverlapSphere(detectorPosition.position, 
            detectorSize, layerToDetect);
    }

    public Collider[] GetCollidersInDetectAreaBox(Vector3 centerPosition, Vector3 boxSize)
    {
        return Physics.OverlapBox(centerPosition, boxSize, Quaternion.identity, layerToDetect);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(detectorPosition.position, detectorSize);
    }
}
