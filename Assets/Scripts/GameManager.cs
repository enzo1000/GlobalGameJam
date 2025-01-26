using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum NewsType
{
    NaturalEvent,
    FollowerBuy,
    FollowerSell
}

public class GameManager : MonoBehaviour
{

    [SerializeField] NewsScriptableObject newsData;
    [SerializeField] GameObject newsPrefab;
    [SerializeField] GameObject actionSpawnZone;
    public GameObject actionPrefab;

    [SerializeField] private Bubble bubble;
    private float bubbleTimer = 0.0f;
    private float actionTimer = 0.0f;
    private int[] actionDisplayedIndex;

    [SerializeField] private PlayerManager player;

    private float victoryGoal;
    List<ActionScript> visibleActions = new List<ActionScript>();

    private float newsTimer = 0.0f;
    // [TODO] à équilibrer
    private float timeBetweenNews = 10.0f;
    private float newsProba = 0.5f;

    private DynamicMusicManager musicManager;//Ame pour la musique

    void Start()
    {
        musicManager = FindFirstObjectByType<DynamicMusicManager>(); //Ame pour la musique  pas sure que ce soit la bonne méthode deprecated shit
    }

    void Update()
    {
        
        //AME TESTS__
        // condition de victoire
        /*if (player.shellNumber >= victoryGoal)
            Debug.Log("GG tu as gagne un milleTplat + cul de la cadreuse");
        */

        // cours de la bubble
        bubbleTimer += Time.deltaTime;

        actionTimer += Time.deltaTime;
        if (bubbleTimer >= 1.0f)
        {
            bubbleTimer = 0.0f;
            bubble.UpdateValue();
        }
        if (actionTimer >= 1.0f)
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
        action.transform.parent = GameObject.Find("ActionList").transform;
        action.transform.localScale = new Vector3(1, 1, 1);
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
}
