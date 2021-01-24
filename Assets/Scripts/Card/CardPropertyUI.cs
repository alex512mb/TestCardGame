using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using DG.Tweening.Core;

public class CardPropertyUI : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private Text valueText;

    private const float punchPower = 1.1f;
    private const float punchDuration = 0.4f;
    private const float changingValueDuration = 1.5f;

    private ChangeableValue<int> targetValue;
    private int previousValue;

    public void Init(ChangeableValue<int> value)
    {
        targetValue = value;
        SetValue(value.Value);
        value.OnValueChanged += TargetValue_OnChanged;
    }

    private void TargetValue_OnChanged(int previousValue, int currentValue)
    {
        
        iconImage.transform.DOPunchScale(Vector3.one * punchPower, punchDuration);

        DOSetter<int> setter = SetValue;
        DOGetter<int> getter = GetValue;
        DOTween.To(getter, setter, currentValue, changingValueDuration);
    }
    private void SetValue(int value)
    {
        valueText.text = value.ToString();
        previousValue = value;
    }
    private int GetValue()
    {
        return previousValue;
    }

    public void SetIcon(Sprite sprite)
    {
        iconImage.sprite = sprite;
    }
}
