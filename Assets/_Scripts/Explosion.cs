using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float Radius;
    public RectTransform rectTransform;

    private float currentRadius;
    private static float explosionSpeed = 350f;
    private bool isExpansion = true;

    private void Update()
    {
        if (isExpansion)
        {
            currentRadius += explosionSpeed * Time.deltaTime;
            if(currentRadius >= Radius)
                isExpansion = false;
        }
        else
        {
            currentRadius -= explosionSpeed * Time.deltaTime;
            if (currentRadius <= 0)
                Destroy(gameObject);
        }

        rectTransform.sizeDelta = new Vector2(currentRadius, currentRadius);
    }
}