using NastyDoll.Utils;
using UnityEngine;

public class CharacterPointer : MonoBehaviour
{
    [SerializeField] SpriteRenderer _characterLogo;

    private Camera uiCamera;
    private Sprite _characterLogoSprite;
    private float _borderSize = 75f;
    private bool _isInited = false;

    public void Initialize(Sprite characterLogo)
    {
        _characterLogoSprite = characterLogo;
        uiCamera = GameObject.FindGameObjectWithTag(TagsHandler.GetPlayerCameraTag()).GetComponent<Camera>();
        _isInited = true;
    }

    private void Update()
    {
        if (_isInited)
        {
            Vector3 _targetTransformScreenPoint = Camera.main.WorldToScreenPoint(transform.parent.position);
            bool isOffScreen = _targetTransformScreenPoint.x <= 0f || _targetTransformScreenPoint.x >= Screen.width ||
                               _targetTransformScreenPoint.y <= 0f || _targetTransformScreenPoint.y >= Screen.height;

            if (isOffScreen)
            {
                _characterLogo.sprite = _characterLogoSprite;
                Vector3 cameraPosition = Camera.main.transform.position;
                cameraPosition.z = 0f;
                Vector3 direction = (cameraPosition - transform.position).normalized;

                Vector3 cappedTargetScreenPosition = _targetTransformScreenPoint;
                if (cappedTargetScreenPosition.x <= _borderSize) cappedTargetScreenPosition.x = _borderSize;
                if (cappedTargetScreenPosition.x >= Screen.width) cappedTargetScreenPosition.x = Screen.width - _borderSize;
                if (cappedTargetScreenPosition.y <= _borderSize) cappedTargetScreenPosition.y = _borderSize;
                if (cappedTargetScreenPosition.y >= Screen.height) cappedTargetScreenPosition.y = Screen.height - _borderSize;

                Vector3 pointerWorldPosition = uiCamera.ScreenToWorldPoint(cappedTargetScreenPosition);
                transform.position = pointerWorldPosition;
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0f);
            }
            else _characterLogo.sprite = null;
        }
    }
}