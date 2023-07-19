using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DamageDigitView : MonoBehaviour
{
    [SerializeField] Material _healMaterial;
    [SerializeField] Material _damageMaterial;
    [SerializeField] TMP_Text _text;
    [SerializeField] float _showTimeInSeconds;
    [SerializeField] float _moveSpeed;
    [SerializeField] float _scaleSpeed;

    private bool isInit = false;
    private bool isHeal = false;

    public void Initialize(float value)
    {
        if (value < 0)
        {
            isHeal = true;
            _text.text = Mathf.Abs(value).ToString("+0.0");
        }
        else _text.text = ((int)Mathf.Abs(value)).ToString();

        if (value < 0)
            GetComponent<Renderer>().sharedMaterial = _healMaterial;
        else GetComponent<Renderer>().sharedMaterial = _damageMaterial;

        isInit = true;
        StartCoroutine(ShowWigit());
    }

    public void Update()
    {
        if (isInit && isHeal)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + _moveSpeed / 3f * Time.deltaTime, transform.position.z);
            transform.localScale = new Vector3(transform.localScale.x - _scaleSpeed / 2f * Time.deltaTime,
                                               transform.localScale.y - _scaleSpeed / 2f * Time.deltaTime,
                                               transform.localScale.z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + _moveSpeed * Time.deltaTime, transform.position.z);
            transform.localScale = new Vector3(transform.localScale.x - _scaleSpeed * Time.deltaTime,
                                               transform.localScale.y - _scaleSpeed * Time.deltaTime,
                                               transform.localScale.z);
        }
    }

    private IEnumerator ShowWigit()
    {
        yield return new WaitForSeconds(_showTimeInSeconds);
        Destroy(gameObject);
    }
}