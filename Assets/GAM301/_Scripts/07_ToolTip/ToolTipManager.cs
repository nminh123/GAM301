using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolTipManager : MonoBehaviour
{
    public ToolTip toolTip;

    public void ShowToolTip(string _content, string _hearder = "")
    {
        toolTip.SetTextToolTip(_content, _hearder);
        toolTip.gameObject.SetActive(true);
    }
    public void HideToolTip()
    {
        toolTip.gameObject.SetActive(false);
    }
}
