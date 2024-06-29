using UnityEngine;

namespace LeoECS
{
    public class BattleConfig : MonoBehaviour
    {
        public int MageCount = 10;

        public RectTransform RedMagePrefab;
        public RectTransform BlueMagePrefab;

        public RectTransform blueTeam;
        public RectTransform redTeam;

        public RectTransform FireballPrefab;
        public RectTransform ExplosionPrefab;
    }
}