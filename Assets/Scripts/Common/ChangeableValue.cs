using System;

public class ChangeableValue<T> where T : struct
{
    public ChangeableValue(T value)
    {
        this.value = value;
        previousValue = value;
        OnValueChanged = null;
    }
    public T Value
    {
        get => value;
        set
        {
            if (!previousValue.Equals(value))
            {
                this.value = value;
                OnValueChanged?.Invoke(previousValue, value);
                previousValue = value;
            }
        }
    }
    public event ChangingDelegate<T> OnValueChanged;

    private T value;
    private T previousValue;

}

public delegate void ChangingDelegate<T>(T previousValue, T currentValue);
