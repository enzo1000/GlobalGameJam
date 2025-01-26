using UnityEngine;

public class PanelOpenerOnClick : MonoBehaviour
{
    public GameObject panel;

    public void OpenPanel()
    {
        if (panel != null)
        {
            //bool isActive = panel.activeSelf; panel.SetActive(!isActive);
            panel.SetActive(true);
        }
    }
}
