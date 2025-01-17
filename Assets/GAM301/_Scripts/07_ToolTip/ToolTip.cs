using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToolTip : MonoBehaviour
{
    public TextMeshProUGUI headerText;
    public TextMeshProUGUI contentText;

    public LayoutElement layoutElement;

    public int charLimit;

    public void SetTextToolTip(string _content, string _header = "")
    {
        if(string.IsNullOrEmpty(_header))
            headerText.gameObject.SetActive(false);
        else
        {
            headerText.gameObject.SetActive(true);
            headerText.text = _header;
        }
        contentText.text = _content;

        if (Application.isEditor)
        {
            int headerLength = headerText.text.Length;
            int contentLength = contentText.text.Length;

            layoutElement.enabled = (headerLength > charLimit || contentLength > charLimit) ? true : false;

        }

        Vector2 pos = Input.mousePosition;

        //float pivotX = pos.x / Screen.width;
        //float pivotY = pos.y / Screen.height;

        //RectTransform rectTransform = GetComponent<RectTransform>();
        //rectTransform.pivot = new Vector2(pivotX, pivotY);

        transform.position = pos;
    }

    //private void Update()
    //{
    //    int headerLength = headerText.text.Length;
    //    int contentLength = contentText.text.Length;

    //    layoutElement.enabled = (headerLength > charLimit || contentLength > charLimit) ? true : false;
    //}
}
