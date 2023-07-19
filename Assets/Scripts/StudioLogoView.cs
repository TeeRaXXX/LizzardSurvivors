using System.Collections;
using UnityEngine;

public class StudioLogoView : MonoBehaviour
{
    [SerializeField] MainMenuView _mainMenuView;
    [SerializeField] private GameObject _studioLogo;
    [SerializeField] private GameObject _startScreen;
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private float _showStudioLogoTime = 3f;
    [SerializeField] private float _StudioLogoScaleSpeed = 1f;

    private bool _showingLogo;

    private static bool _showLogo = true;

    public void Initialize()
    {
        if (_showLogo)
        {
            _showingLogo = false;
            _studioLogo.SetActive(true);
            _startScreen.SetActive(false);
            _showLogo = false;
            _mainMenu.SetActive(false);

            StartCoroutine(ShowStudioLogo(_showStudioLogoTime));
        }
        else
        {
            _studioLogo.SetActive(false);
            _startScreen.SetActive(false);
            _mainMenu.SetActive(true);
        }
    }

    private IEnumerator ShowStudioLogo(float showTimeInSeconds)
    {
        _showingLogo = true;

        yield return new WaitForSeconds(showTimeInSeconds);

        _showingLogo = false;
        _studioLogo.SetActive(false);
        _startScreen.SetActive(true);
    }

    private void Update()
    {
        if (_showingLogo)
        {
            _studioLogo.transform.localScale = new Vector3(_studioLogo.transform.localScale.x + _StudioLogoScaleSpeed * Time.deltaTime,
                                                           _studioLogo.transform.localScale.y + _StudioLogoScaleSpeed * Time.deltaTime,
                                                           _studioLogo.transform.localScale.z);
        }
    }
}