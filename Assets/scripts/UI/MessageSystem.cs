using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageSystem : MonoBehaviour
{
    [SerializeField] private Text textExample;
    [SerializeField] private Transform place;

    private Camera mainCamera;
    private ObjectPooling<Text> textPool;
    private float halfLength;

    // Start is called before the first frame update
    void Start()
    {
        textPool = new ObjectPooling<Text>(20, textExample.gameObject, place);
        mainCamera = GameManager.Instance.GetMainCamera;
        halfLength = textExample.rectTransform.sizeDelta.x / 2;
    }

    public void ShowMessageFrame(Vector3 position, string text, Color color)
    {
        StartCoroutine(playMessage(position, text, color));
    }

    private IEnumerator playMessage(Vector3 position, string text, Color color)
    {
        Text currentText = textPool.GetPrefab();
        currentText.color = color;
        currentText.text = text;
        currentText.gameObject.SetActive(true);

        for (float i = 0; i < 1; i+=Time.deltaTime)
        {
            Vector3 newPos = mainCamera.WorldToScreenPoint(position);
            currentText.rectTransform.anchoredPosition3D = new Vector3(newPos.x - halfLength, newPos.y, newPos.z);
            yield return new WaitForSeconds(Time.deltaTime);
        }

        currentText.gameObject.SetActive(false);
        textPool.ReturnObject(currentText);
    }
}
