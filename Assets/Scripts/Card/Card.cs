using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField]
    private int hp;
    [SerializeField]
    private int attack;
    [SerializeField]
    private int mana;

    public ChangeableValue<int> hpProperty = default;
    public ChangeableValue<int> attackProperty = default;
    public ChangeableValue<int> manaProperty = default;


    private ChangeableValue<int>[] allProperties;


    private void Awake()
    {
        hpProperty = new ChangeableValue<int>(hp);
        attackProperty = new ChangeableValue<int>(attack);
        manaProperty = new ChangeableValue<int>(mana);

        allProperties = new ChangeableValue<int>[3];
        allProperties[0] = hpProperty;
        allProperties[1] = attackProperty;
        allProperties[2] = manaProperty;
    }
    public void ApplyRandomValue()
    {
        int randomValue = Random.Range(-2, 9);
        ChangeableValue<int> randomProperty = allProperties[Random.Range(0, allProperties.Length)];
        randomProperty.Value = randomValue;
    }
}
