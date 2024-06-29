using UnityEngine;

namespace OLD
{
    public class Explosion : MonoBehaviour
    {
        public float Radius;
        public RectTransform RectTransform;

        private float currentRadius;
        private static float explosionSpeed = 1350f;
        private bool isExpansion = true;

        private void Update()
        {
            if (isExpansion)
            {
                currentRadius += explosionSpeed * Time.deltaTime;
                if (currentRadius >= Radius)
                    isExpansion = false;
            }
            else
            {
                currentRadius -= explosionSpeed * Time.deltaTime;
                if (currentRadius <= 0)
                    Destroy(gameObject);
            }

            RectTransform.sizeDelta = new Vector2(currentRadius, currentRadius);
        }
    }
}