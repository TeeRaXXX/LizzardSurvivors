using UnityEngine;
using NastyDoll.Utils;

public class ArrowPointer : MonoBehaviour
{
    [SerializeField] private Transform _targetTransform;
    [SerializeField] private float _borderSize = 100f;

    private Camera uiCamera;
    private Vector3 _basePosition;

    private void Awake()
    {
        _basePosition = transform.localPosition;
        _baseScale = transform.localScale;
        uiCamera = GameObject.FindGameObjectWithTag(TagsHandler.GetPlayerCamera()).GetComponent<Camera>();
    }

    private void Update()
    {
        transform.localPosition = _basePosition;
        transform.eulerAngles = Vector3.zero;


        Vector3 _targetTransformScreenPoint = Camera.main.WorldToScreenPoint(_targetTransform.position);
        bool isOffScreen = _targetTransformScreenPoint.x <= 0f || _targetTransformScreenPoint.x >= Screen.width ||
                           _targetTransformScreenPoint.y <= 0f || _targetTransformScreenPoint.y >= Screen.height;

        if (isOffScreen)
        {
            Vector3 cameraPosition = Camera.main.transform.position;
            cameraPosition.z = 0f;
            Vector3 direction = (cameraPosition - _targetTransform.position).normalized;
            float angle = UtilsClass.GetAngleFromVectorFloat(direction);
            transform.localEulerAngles = new Vector3(0f, 0f, angle - 90f);

            Vector3 cappedTargetScreenPosition = _targetTransformScreenPoint;
            if (cappedTargetScreenPosition.x <= _borderSize) cappedTargetScreenPosition.x = _borderSize;
            if (cappedTargetScreenPosition.x >= Screen.width) cappedTargetScreenPosition.x = Screen.width - _borderSize;
            if (cappedTargetScreenPosition.y <= _borderSize) cappedTargetScreenPosition.y = _borderSize;
            if (cappedTargetScreenPosition.y >= Screen.height) cappedTargetScreenPosition.y = Screen.height - _borderSize;

            Vector3 pointerWorldPosition = uiCamera.ScreenToWorldPoint(cappedTargetScreenPosition);
            transform.position = pointerWorldPosition;
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0f);
        }
    }
}