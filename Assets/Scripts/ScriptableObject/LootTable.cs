using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]

public class Loot
{
    public PowerUp thisloot;
    public int lootChance;
}
[CreateAssetMenu]
public class LootTable : ScriptableObject
{
    public Loot[] loots;
    public PowerUp LootPowerup()
    {
        int cumProb = 0;
        int currentProb = Random.Range(0,100);
        for(int i=0;i<loots.Length;i++)
        {
            cumProb += loots[i].lootChance;
            if(currentProb <= cumProb)
            {
                return loots[i].thisloot;
            }
        }
        return null;
    }
}
