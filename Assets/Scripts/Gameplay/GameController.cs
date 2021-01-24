using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private float deleySpawnCard = 0.5f;
    [SerializeField]
    private int minCountCards = 4;
    [SerializeField]
    private int maxCountCards = 6;
    [SerializeField]
    private GameObject prefabCard = default;
    [SerializeField]
    private GameObject hand = default;
    [SerializeField]
    private TextureLoader textureLoader;


    private int indexSelectedCard = 0;


    private IEnumerator Start()
    {
        int countCard = Random.Range(minCountCards, maxCountCards + 1);

        //load content for cards
        Texture2D[] textures = new Texture2D[countCard];
        for (int i = 0; i < countCard; i++)
        {
            yield return textureLoader.LoadingTexture();
            textures[i] = textureLoader.loadedTexture;
        }
        Debug.Log("Download content done");

        //Fulfill player's hand with 4-6 cards in a fan-way. (arc)
        for (int i = 0; i < countCard; i++)
        {
            yield return new WaitForSeconds(deleySpawnCard);
            SpawnRandomCardIntoHand(textures[i]);
        }
        Debug.Log("Fulfill player's hand done");
    }

    private void SpawnRandomCardIntoHand(Texture2D content)
    {
        GameObject spawnedGO = Instantiate(prefabCard, hand.transform);
        CardUI spawnedCard = spawnedGO.GetComponent<CardUI>();
        spawnedCard.SetContent(content);
    }
    private Card GetSelectedCard()
    {
        Transform selectedChild = hand.transform.GetChild(indexSelectedCard);
        Card selectedCard = selectedChild.GetComponent<Card>();

        return selectedCard;
    }
    private void SelectNextCard()
    {
        indexSelectedCard++;
        if (indexSelectedCard >= hand.transform.childCount)
        {
            indexSelectedCard = 0;
        }
    }


    public void ApplyRandomValueBySequence()
    {
        Card selectedCard = GetSelectedCard();

        selectedCard.ApplyRandomValue();

        SelectNextCard();
    }
}
