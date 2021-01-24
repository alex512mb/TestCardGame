using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Card))]
public class CardUI : MonoBehaviour
{
    [SerializeField]
    private CardPropertyUI hpProperty;
    [SerializeField]
    private CardPropertyUI attackProperty;
    [SerializeField]
    private CardPropertyUI manaProperty;

    [SerializeField]
    private Image contentImage;


    private Card targetCard;


    private void Start()
    {
        targetCard = GetComponent<Card>();

        hpProperty.Init(targetCard.hpProperty);
        attackProperty.Init(targetCard.attackProperty);
        manaProperty.Init(targetCard.manaProperty);
    }

    public void SetContent(Texture2D texture)
    {
        Rect spriteRect = new Rect(0, 0, texture.width, texture.height);
        Vector2 spritePivot = new Vector2(0.5f, 0.5f);
        contentImage.sprite = Sprite.Create((Texture2D)texture, spriteRect, spritePivot);
    }
}
