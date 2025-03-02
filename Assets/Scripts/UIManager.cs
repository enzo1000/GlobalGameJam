using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject panelToToggle; // The panel to toggle
    private RectTransform panelRectTransform;
    public TMP_Text followersText; // number of followers
    public TMP_Text bubblesText; //number of bubbles
    // rajouter celle pour la valeur de 1 shell = x bubbles

    private PlayerManager playerManager;

    private bool isPanelOpen = false;

    void Start()
    {
        panelRectTransform = panelToToggle.GetComponent<RectTransform>();
        playerManager = FindFirstObjectByType<PlayerManager>(); // a voir si y a pas un meilleur moyen pour le trouver
    }

    void Update()
    {
        if (isPanelOpen && Input.GetMouseButtonDown(0))
        {
            if (!IsPointerOverPanel())
            {
                TogglePanel(false);
            }
        }

        // pour les textes
        if (playerManager != null)
        {
            followersText.text = $"{playerManager.GetFollowersCount()}";
            bubblesText.text = $"{playerManager.GetBubbleCount()}";
        }

    }

    // Toggles panel visibility
    public void TogglePanel(bool open)
    {
        isPanelOpen = open;
        panelToToggle.SetActive(open);
    }

    public void OnButtonClick()
    {
        if (!isPanelOpen) 
        {
            TogglePanel(true);
        }
    }

    // Checks if the pointer is over the panel
    private bool IsPointerOverPanel()
    {
        if (panelRectTransform == null)
            return false;

        Vector2 mousePosition = Input.mousePosition;
        return RectTransformUtility.RectangleContainsScreenPoint(panelRectTransform, mousePosition, Camera.main);
    }


    public void OnButtonClick_QuitMarketDefinitly()
    {
        Debug.Log("Button was clicked!");
    }


    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene("MainMenu"); 
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene("AmeScene");
    }

}

