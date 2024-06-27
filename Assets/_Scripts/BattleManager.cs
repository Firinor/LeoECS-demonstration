using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public int partyCount = 10;

    public Mage blueMagePrefab;
    public Mage redMagePrefab;

    public RectTransform blueTeam;
    public RectTransform redTeam;

    private static RectTransform bulletsParent;
    public static RectTransform BulletsParent 
    { 
        get
        {
            if (!bulletsParent)
                bulletsParent = GameObject.Find("BulletsParent").GetComponent<RectTransform>();

            return bulletsParent;
        } 
    }

    void Start()
    {
        for(int i = 0; i < partyCount; i++)
        {
            Mage blueMage = Instantiate(blueMagePrefab, blueTeam);
            float randomX = Random.Range(0, blueTeam.rect.width);
            float randomY = Random.Range(0, blueTeam.rect.height);
            blueMage.GetComponent<RectTransform>().anchoredPosition = new Vector2(randomX, randomY);

            Mage redMage = Instantiate(redMagePrefab, redTeam);
            randomX = Random.Range(0, redTeam.rect.width);
            randomY = Random.Range(0, redTeam.rect.height);
            redMage.GetComponent<RectTransform>().anchoredPosition = new Vector2(randomX, randomY);
        }
    }
}
