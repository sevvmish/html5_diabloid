using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MessageSystem : MonoBehaviour
{
    [SerializeField] private TextMeshPro textExample;
    [SerializeField] private Transform place;

    private Camera mainCamera;
    private ObjectPooling<TextMeshPro> textPool;
    private float halfLength;

    // Start is called before the first frame update
    void Start()
    {
        textExample.fontSize = Globals.MessageFontSize;
        textPool = new ObjectPooling<TextMeshPro>(20, textExample.gameObject, place);
        mainCamera = GameManager.Instance.GetMainCamera;
        halfLength = textExample.rectTransform.sizeDelta.x / 2;
    }

    public void ShowMessageFrame(float height, string text, Color color, Transform place)
    {
        StartCoroutine(playMessage(height, text, color, place));
    }

    private IEnumerator playMessage(float height, string text, Color color, Transform place)
    {
        TextMeshPro currentText = textPool.GetPrefab();
        currentText.color = color;
        currentText.text = text;
        currentText.gameObject.SetActive(true);

        int sign = Random.Range(0, 2);
        float leftRight = Random.Range(0.3f, 0.6f);
        if (sign == 0)
        {
            leftRight = -leftRight;
        }        

        float d = 0.3f;
        Vector3 delta = new Vector3(Random.Range(-d, d), Random.Range(-d, d), Random.Range(-d, d));

        currentText.transform.position = place.position + Vector3.up * height + delta;
        currentText.transform.DOMove(currentText.transform.position + Vector3.up * 0.7f + Vector3.left * leftRight, 0.6f).SetEase(Ease.OutQuad);
        yield return new WaitForSeconds(0.6f);
        
        currentText.gameObject.SetActive(false);
        textPool.ReturnObject(currentText);
    }
}
