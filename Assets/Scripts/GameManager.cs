using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public enum NewsType
{
    NaturalEvent,
    FollowerBuy,
    FollowerSell
}

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject BG;
    [SerializeField] GameObject Winn;
    [SerializeField] GameObject Lose;

    [Header("Interface")]
    public GameObject canvasInterfaceBase;
    public GameObject canvasGestionActions;

    [SerializeField] NewsScriptableObject newsData;
    [SerializeField] GameObject newsPrefab;
    [SerializeField] GameObject actionSpawnZone;
    public GameObject actionPrefab;

    [SerializeField] private Bubble bubble;
    private float bubbleTimer = 0.0f;
    private float actionTimer = 0.0f;
    private int[] actionDisplayedIndex;

    [SerializeField] private PlayerManager player;

    public float victoryGoal = 3000;
    List<ActionScript> visibleActions = new List<ActionScript>();

    private float newsTimer = 0.0f;
    // [TODO] à équilibrer
    private float timeBetweenNews = 10.0f;
    private float newsProba = 0.5f;
    private GameObject UI_actionList;

    private DynamicMusicManager musicManager;//Ame pour la musique

    void Start()
    {
        UI_actionList = GameObject.Find("ActionList");
        musicManager = FindFirstObjectByType<DynamicMusicManager>(); //Ame pour la musique  pas sure que ce soit la bonne méthode deprecated shit
    }

    void Update()
    {
        
        bubbleTimer += Time.deltaTime;

        actionTimer += Time.deltaTime;
        if (bubbleTimer >= 1.0f)
        {
            bubbleTimer = 0.0f;
            bubble.UpdateValue();
        }
        if (actionTimer >= 4.0f)
        {
            actionTimer = 0.0f;
            spawnAction();
        }
    }

    private void spawnAction()
    {
        BoxCollider2D spawnZone = actionSpawnZone.GetComponent<BoxCollider2D>();
        float randSpawnX = Random.Range(spawnZone.bounds.min.x, spawnZone.bounds.max.x);
        float randSpawnY = Random.Range(spawnZone.bounds.min.y, spawnZone.bounds.max.y);
        UnityEngine.Vector3 spawnPoint = new Vector3(randSpawnX, randSpawnY, 0);
        GameObject action = Instantiate(actionPrefab, spawnPoint, Quaternion.identity);
        Destroy(action, 10.0f); //TO IMPLEMENT IN PREFAB
        action.transform.parent = UI_actionList.transform;
        action.transform.localScale = new Vector3(1, 1, 1);

        // ici mettre le son de spawn de l'action lie au personnage recuperer le nomm et activer son X si perso X son Y si perso Y dans l'audiosource 2 
        // Get the ActionScript from the instantiated action
        ActionScript actionScript = action.GetComponent<ActionScript>();

        // Play the sound for the specific character
        //musicManager.PlayActionSpawnSfx(actionScript.VendorIndex);
        StartCoroutine(PlaySoundAfterDelay(action));
        Debug.Log("Action spawned");
    }

    private IEnumerator PlaySoundAfterDelay(GameObject action) //pire systeme ever mais ca marche
    {
        yield return new WaitForSeconds(0.2f); // Attendre 0.2 secondes
        ActionScript actionScript = action.GetComponent<ActionScript>();
        musicManager.PlayActionSpawnSfx(actionScript.VendorIndex);
    }
    public void OnClickManageActions()
    {
        canvasGestionActions.SetActive(true);
        canvasInterfaceBase.SetActive(false);
    }
    public void OnClickBackButton()
    {
        canvasInterfaceBase.SetActive(true);
        canvasGestionActions.SetActive(false);
    }

    // [TODO] à équilibrer
    public void UpdateTimeBetweenNews(float nbFollowers)
    {

    }

    // [TODO] à équilibrer
    public void UpdateNewProba(float nbFollowers)
    {

    }

    // --------------|-------------------------------|--------------
    // bulle explose  bulle n'explose pas et augmente  bulle n'explose pas et descend quand meme (mais peut remonter)

    public void SummonNews(ActionScript action, NewsType type)
    {
        float variation = computeVariationValue(action.investDanger);

        switch(type)
        {
            case NewsType.NaturalEvent:
                if (action.asExplosed || Random.value <= action.investDanger) // bulle explose
                {
                    variation *= -1.0f;
                    if (musicManager != null)
                    {
                        musicManager.PlayBadVariation(); // Play bad news variation
                    }

                }
                else
                {
                    if (Random.value >= 1 - action.investDanger) // [TODO] à voir
                    {
                        variation *= -1.0f;
                        if (musicManager != null)
                        {
                            musicManager.PlayGoodVariation(); // Play good news variation
                        }
                    }
                }
                break;
            case NewsType.FollowerBuy:
                if (action.asExplosed || Random.value <= action.investDanger) // bulle explose
                {
                    variation *= -1.0f;
                    if (musicManager != null)
                    {
                        musicManager.PlayBadVariation(); // Play bad news variation
                    }
                }
                break;
            case NewsType.FollowerSell:
                variation *= -1.0f;
                break;
            default:
                return;
        }

        NewsScript news = Instantiate(newsPrefab).GetComponent<NewsScript>();
        news.InitData(Random.Range(0, newsData.newsParamList.Count));
    }

    // [TODO] à équilibrer
    private float computeVariationValue(float speculativeBubbleChance)
    {
        float variation = 1f;
        return variation;
    }

    public void EndGame()
    {
        if (player.shellNumber >= victoryGoal)
        {
            Debug.Log("GG tu as gagne un milleTplat + cul de la cadreuse");
            BG.SetActive(false);
            Winn.SetActive(true);
            string score = ((int)player.shellNumber).ToString();
            Winn.GetComponentInChildren<TMP_Text>().text = score + " / " + victoryGoal.ToString();
        }
        else
        {
            Debug.Log(
                "\r\neya tsu tsa paveri paveron" +
                "\r\nlantic ta deli landing standoun" +
                "\r\nLa dibidabidam la rou patirou pidam" +
                "\r\nCurican gu geaki geganku" +
                "\r\n" +
                "\r\nAra tsapitsa yalibilabidi labidi" +
                "\r\nStandin landen lando" +
                "\r\nAbaritapita pari pari" +
                "\r\nPari bilibilibili stenden lando" +
                "\r\nYabadin la sten lande yalo" +
                "\r\nAlabala balebele bedou yavou" +
                "\r\n" +
                "\r\nBali zdale lazde lando" +
                "\r\nBadageda gedageda ya de dou dou" +
                "\r\nde ya do"
            );
            BG.SetActive(false);
            Lose.SetActive(true);
            string score = ((int)player.shellNumber).ToString();
            Lose.GetComponentInChildren<TMP_Text>().text = score + " / " + victoryGoal.ToString();
        }
    }
}
