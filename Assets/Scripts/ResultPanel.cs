using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class ResultPanel : MonoBehaviour
{
    [SerializeField] private float _showDuration = 1;
    private CanvasGroup _canvasGroup;
    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        
        _canvasGroup.blocksRaycasts = false;
        _canvasGroup.interactable = false;
        _canvasGroup.alpha = 0;
    }

    public void Show()
    {
        StartCoroutine(Show(_showDuration));
    }

    private IEnumerator Show(float duration)
    {
        float lostTime = 0;
        _canvasGroup.blocksRaycasts = true;
        _canvasGroup.interactable = true;

        while(lostTime < 1.1f)
        {
            _canvasGroup.alpha = lostTime;
            lostTime += Time.deltaTime / duration;
            yield return null;
        }
    }
}
