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
    [SerializeField] private NewsScriptableObject newsData;
    [SerializeField] private GameObject newsPrefab;

    [SerializeField] private Bubble bubble;
    private float bubbleTimer = 0.0f;

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
        //__AME TESTS
        // Test musics delete later
        if (Input.GetKeyDown(KeyCode.G)) 
        {
            if (musicManager != null)
            {
                musicManager.PlayGoodVariation();
                Debug.Log("Playing Good Variation");
            }
        }

        if (Input.GetKeyDown(KeyCode.B)) 
        {
            if (musicManager != null)
            {
                musicManager.PlayBadVariation();
                Debug.Log("Playing Bad Variation");
            }
        }
        //AME TESTS__
        // condition de victoire
        if (player.shellNumber >= victoryGoal)
            Debug.Log("GG tu as gagne un milleTplat + cul de la cadreuse");

        // cours de la bubble
        bubbleTimer += Time.deltaTime;
        if(bubbleTimer >= 0.2f)
        {
            bubbleTimer = 0.0f;
            bubble.UpdateValue();
        }
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
