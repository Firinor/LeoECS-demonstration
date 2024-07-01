using System.Collections.Generic;
using UnityEngine;

namespace OLD
{
    public class Fireball : MonoBehaviour
    {
        public RectTransform Target;
        public float ExplosionRadius;
        public int Attack;
        public RectTransform CurrentRect;

        private static float acceleration = .75f;

        [SerializeField] Explosion ExplosionPrefab;
        private float speed;

        public void Update()
        {
            speed += acceleration * Time.deltaTime;

            Vector2 direction = Target.position - CurrentRect.position;

            Vector2 delta = direction.normalized * speed;

            CurrentRect.anchoredPosition += delta;

            if (direction.magnitude * 100 >= delta.magnitude)
                return;

            Explosion explosion = Instantiate(ExplosionPrefab, Target.position, Quaternion.identity, BattleManager.BulletsParent);
            explosion.Radius = ExplosionRadius;
            DoAttack();
            Destroy(gameObject);
        }

        private void DoAttack()
        {
            Mage[] mages = FindEnemy();

            if (mages == null)
                return;

            if (Target.tag == "RedTeam")
                BattleManager.inctance.BlueScore += mages.Length * Attack;
            else if (Target.tag == "BlueTeam")
                BattleManager.inctance.RedScore += mages.Length * Attack;
        }

        private Mage[] FindEnemy()
        {
            List<Mage> result = new List<Mage>();

            RectTransform targetParent = (RectTransform)Target.parent;

            for (int i = 0; i < targetParent.childCount; i++)
            {
                RectTransform targetMage = (RectTransform)targetParent.GetChild(i);

                if (Vector2.Distance(targetMage.anchoredPosition, Target.anchoredPosition) <= ExplosionRadius)
                    result.Add(targetMage.GetComponent<Mage>());
            }

            return result.ToArray();
        }
    }
}