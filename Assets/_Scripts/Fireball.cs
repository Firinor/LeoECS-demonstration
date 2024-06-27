using UnityEngine;

public class Fireball : MonoBehaviour
{
    public RectTransform target;
    public float explosionRadius;
    public RectTransform currentRect;

    private static float acceleration = .35f;

    [SerializeField] Explosion explosionPrefab;
    private float speed;

    public void Update()
    {
        speed += acceleration * Time.deltaTime;

        Vector2 direction = target.position - currentRect.position;

        Vector2 delta = direction.normalized * speed;

        currentRect.anchoredPosition += delta;

        if (direction.magnitude < delta.magnitude)
        {
            Instantiate(explosionPrefab, target.position, Quaternion.identity, BattleManager.BulletsParent).Radius = explosionRadius;
            Destroy(gameObject, 0.1f);
        }
    }
}
