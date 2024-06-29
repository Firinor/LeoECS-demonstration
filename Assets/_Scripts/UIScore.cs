using System.Collections;
using TMPro;
using UnityEngine;
namespace OLD
{
    public class UIScore : MonoBehaviour
    {
        public TextMeshProUGUI text;
        public bool RedTeam;

        private IEnumerator Start()
        {
            yield return null;

            if (RedTeam)
                BattleManager.inctance.redScoreChanched += Render;
            else
                BattleManager.inctance.blueScoreChanched += Render;
        }

        private void Render(int score)
        {
            text.text = "Score: " + score;
        }

        private void OnDestroy()
        {
            if (RedTeam)
                BattleManager.inctance.redScoreChanched -= Render;
            else
                BattleManager.inctance.blueScoreChanched -= Render;
        }
    }
}