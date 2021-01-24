using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ArcLayoutGroup : LayoutGroup
{
    [SerializeField]
    private float distanceArcCenter = 100;
    [SerializeField]
    private float durationAnim = 1;

    private Vector3 ArcCenter => transform.position + axisToArcCenter.normalized * distanceArcCenter;

    private static Vector3 axisToArcCenter = Vector3.down;


    [Tooltip("axis along which to place the items")]
    public Vector3 expandAxis;
    [Tooltip("size of each item along the Normalized axis")]
    public float itemSize;

    protected override void OnEnable() { base.OnEnable(); CalculateRadial(); }
    public override void SetLayoutHorizontal()
    {
    }
    public override void SetLayoutVertical()
    {
    }
    public override void CalculateLayoutInputVertical()
    {
        CalculateRadial();
    }
    public override void CalculateLayoutInputHorizontal()
    {
        CalculateRadial();
    }
#if UNITY_EDITOR
    protected override void OnValidate()
    {
        base.OnValidate();
        CalculateRadial();
    }
#endif

    private void CalculateRadial()
    {
        m_Tracker.Clear();
        if (transform.childCount == 0)
            return;

        //one liner for figuring out the pivot (why not a utility function switch statement?)
        Vector2 pivot = new Vector2(((int)childAlignment % 3) * 0.5f, ((int)childAlignment / 3) * 0.5f);


        float lerp = 0;
        float step = 1f / transform.childCount;

        Vector3 expandVector = expandAxis.normalized * itemSize;
        Vector3 expandSize = expandVector * transform.childCount;
        Vector3 firstPosition = transform.position - (expandSize / 2f);
        Vector3 lastPosition = firstPosition + expandSize;

        for (int i = 0; i < transform.childCount; i++)
        {
            RectTransform child = (RectTransform)transform.GetChild(i);
            if (child != null)
            {
                //stop the user from altering certain values in the editor
                m_Tracker.Add(this, child,
                DrivenTransformProperties.Anchors |
                DrivenTransformProperties.AnchoredPosition |
                DrivenTransformProperties.Pivot |
                DrivenTransformProperties.Rotation);

                Vector3 targetPos = Vector3.Lerp(firstPosition, lastPosition, lerp);
                Vector3 directionToRoot = ArcCenter - targetPos;
                Quaternion targetRotation = Quaternion.FromToRotation(axisToArcCenter, directionToRoot);

                child.anchorMin = child.anchorMax = new Vector2(0.5f, 0.5f);
                child.pivot = pivot;


                if (Application.isPlaying)
                {
                    child.DOMove(targetPos, durationAnim);
                    child.DORotate(targetRotation.eulerAngles, durationAnim);
                }
                else
                {
                    child.position = targetPos;
                    child.rotation = targetRotation;
                }



                lerp += step;
            }
        }


    }
}



