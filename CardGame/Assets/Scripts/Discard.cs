using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Discard : MonoBehaviour
{
    public RectTransform DiscardRoot;

    public void DiscardCard(GameObject DiscardCard)
    {
        DiscardCard.transform.SetParent(DiscardRoot);
        var DiscardCardRect = DiscardCard.GetComponent<RectTransform>();
        DiscardCardRect.localEulerAngles = new Vector3(0, 0, Random.Range(0, 30));
        DiscardCardRect.anchoredPosition = new Vector2(Random.Range(-100, 100), 0);
    }
}
