using UnityEngine;

public class Neck : MonoBehaviour
{
    [SerializeField] private Transform[] _children;

    [SerializeField] private Transform _headTrm;
    [SerializeField] private float _headOffset;
    [SerializeField] private Transform _neckStartTrm;
    [SerializeField] private float _neckStartOffset;

    private int _childCount => _children.Length;

    private void Update() {
        Vector2 bodyToHead = (_headTrm.position - _neckStartTrm.position).normalized;
        float angle = Mathf.Atan2(bodyToHead.y, bodyToHead.x) * Mathf.Rad2Deg;
        Quaternion quaterAngle = Quaternion.Euler(0, 0, angle + 90);

        for(int i = 0; i < _childCount; ++i) {
            _children[i].position = GetNeckPosition(i);
            _children[i].rotation = quaterAngle;
        }
    }

    private Vector2 GetNeckPosition(int index) {
        Vector2 bodyToHead = (_headTrm.position - _neckStartTrm.position).normalized;
        Vector2 startPos = (Vector2)_neckStartTrm.position + bodyToHead * _neckStartOffset;
        Vector2 endPos = (Vector2)_headTrm.position - bodyToHead * _headOffset;

        return Vector2.Lerp(startPos, endPos, index / (_children.Length - 1f));
    }

    #if UNITY_EDITOR
    private void OnDrawGizmos() {
        Vector2 bodyToHead = (_headTrm.position - _neckStartTrm.position).normalized;

        Gizmos.color = Color.red;
        Gizmos.DrawLine((Vector2)_neckStartTrm.position, (Vector2)_neckStartTrm.position + bodyToHead * _neckStartOffset);
        Gizmos.DrawLine((Vector2)_headTrm.position, (Vector2)_headTrm.position - bodyToHead * _headOffset);
    }
    #endif
}
