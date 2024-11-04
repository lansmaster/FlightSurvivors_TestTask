using UnityEngine;

public class CameraTracker : MonoBehaviour
{
    [SerializeField] private Unit _trackedUnit;
    [SerializeField] [Range(1f,2f)] private float _offsetSmoothing = 1.35f;

    private Vector3 _targetPosition;

    private void LateUpdate()
    {
        _targetPosition = new Vector3(_trackedUnit.transform.position.x, _trackedUnit.transform.position.y, transform.position.z);

        transform.position = Vector3.Lerp(transform.position, _targetPosition, Time.deltaTime * (_trackedUnit.Config.Speed / _offsetSmoothing));
    }
}
