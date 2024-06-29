using UnityEngine;
namespace OLD
{
    public class Mage : MonoBehaviour
    {
        public Fireball FireboltPrefab;
        public string EnemyTag;
        public RectTransform castPoint;

        [Space]
        public int Attack = 5;
        private int Radius = 250;
        private float Reattack = 2;

        private float currentCooldown;

        void Start()
        {
            RandomizeStat();
        }

        private void RandomizeStat()
        {
            Attack = Random.Range(3, 8);
            Radius = Random.Range(200, 301);
            Reattack = Random.Range(0.5f, 1.5f);
        }

        void Update()
        {
            if (currentCooldown <= 0)
            {
                CastFirebolt();
                return;
            }
            else
                currentCooldown -= Time.deltaTime;
        }

        private void CastFirebolt()
        {
            Mage target = FindEnemy(EnemyTag);

            if (!target)
            {
                enabled = false;
                return;
            }

            Fireball fireball = Instantiate(FireboltPrefab, castPoint.position, Quaternion.identity, BattleManager.BulletsParent);

            fireball.Target = target.GetComponent<RectTransform>();
            fireball.ExplosionRadius = Radius;
            fireball.Attack = Attack;

            currentCooldown += Reattack;
        }

        private Mage FindEnemy(string tag)
        {
            Mage result = null;

            GameObject[] targets = GameObject.FindGameObjectsWithTag(tag);

            if (targets.Length > 0)
            {
                int targetIndex = Random.Range(0, targets.Length);
                result = targets[targetIndex].GetComponent<Mage>();
            }

            return result;
        }
    }
}