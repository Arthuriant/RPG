using UnityEngine;

public class DugeonEnemyRoom : DugeonRoom
{
    public bool active;
    public BoolValue storedValue;
    public Door[] doors;

    void Start()
    {
     active = storedValue.RuntimeValue;
    }
    void Update()
    {
        CheckEnemies();
    }


   public void CheckEnemies()
{
    for (int i = 0; i < enemies.Length; i++)
    {
        if (enemies[i].gameObject.activeInHierarchy)
        {
            return; // Keluar jika ada musuh yang masih aktif
        }
    }
    // Jika semua musuh sudah tidak aktif
    active = true;
    storedValue.RuntimeValue = active;
    OpenDoors();
}

    public void OpenDoors()
    {
        for(int i = 0; i<doors.Length;i++)
        {
            doors[i].Open();
        }
    }
    public void CloseDoors()
    {
        for(int i = 0; i<doors.Length;i++)
        {
            doors[i].Close();
        }
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !other.isTrigger)
        {
            //active all enemies
             if(!active)
            {
                for (int i = 0 ; i<enemies.Length; i++)
            {
                ChangeActivation(enemies[i], true);
            }

            for (int i = 0 ; i<pots.Length; i++)
            {
                ChangeActivation(pots[i], true);
            }
            }
        CloseDoors();
        virtualCamera.SetActive(true);
        }
    }

    public override void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !other.isTrigger && active)
        {
        
            for (int i = 0 ; i<enemies.Length; i++)
            {
                ChangeActivation(enemies[i], false);
            }

            for (int i = 0 ; i<pots.Length; i++)
            {
                ChangeActivation(pots[i], false);
            }
            virtualCamera.SetActive(false);
        }
    }

}