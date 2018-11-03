using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZoneManager : Singleton<DeadZoneManager>
{
    public int[] deadZoneNotificationTime = new int[]{1,3,5,7};
    public int[] deadZoneBombTime = new int[] { 2,4,6,8 };
    public List<int> bombedZoneId;
    public List<int> safeZoneId;
    public int nextBombZoneId;
    public GameObject[] destroyedMapTiles;
    int startCheck = 0;
    // Start is called before the first frame update
    void Start()
    {
        bombedZoneId = new List<int>();
        safeZoneId = new List<int>() { 0, 1, 2, 3 };
        nextBombZoneId = -1;
        foreach(GameObject go in destroyedMapTiles)
        {
            go.SetActive(false);
        }
    }

    public void CheckBombStatus(int curHour)
    {
        for(int i = startCheck; i < 4; i++)
        {
            if (deadZoneNotificationTime[i] == curHour)
            {
                //send notification
                BRMessageViewController.Instance.AddCell(deadZoneBombTime[i] - deadZoneNotificationTime[i]);
                int randZoneId = Random.Range(0, safeZoneId.Count);
                int randZone = safeZoneId[randZoneId];
                safeZoneId.RemoveAt(randZoneId);
                MapViewController.Instance.UpdateMapColor(randZone, Color.red);
                nextBombZoneId = randZone;
            } else if(deadZoneBombTime[i] == curHour)
            {

                BombDamage(nextBombZoneId);

                BRMessageViewController.Instance.AddCell("BOMB! Black Zone is bombed and you are not allowed to get into it later.");
                MapViewController.Instance.UpdateMapColor(nextBombZoneId, new Color(0.2f,0,0));
                bombedZoneId.Add(nextBombZoneId);
                destroyedMapTiles[nextBombZoneId].SetActive(true);
                nextBombZoneId = -1;
                startCheck += 1;
            }
        }
    }

    void BombDamage(int index)
    {
        if (PositionToZoneIndex(HumanManager.Instance.heroInfo.transform.position) == index)
        {
            HumanManager.Instance.heroInfo.HurtHuman(1000, "DEAD ZONE");
        }
        foreach (HumanInfo info in OtherHumanManager.Instance.otherHumans)
        {
            if (PositionToZoneIndex(info.transform.position) == index)
            {
                info.HurtHuman(1000, "DEAD ZONE");
            }
        }
    }

    public bool IsInBombedDeadZone(Vector3 position)
    {
        return bombedZoneId.Contains(PositionToZoneIndex(position));
    }

    public int PositionToZoneIndex(Vector3 position)
    {
        if (position.x < 0 && position.y >= 0)
        {
            return 0;
        }
        if (position.x >= 0 && position.y >= 0)
        {
            return 1;
        }
        if (position.x < 0 && position.y < 0)
        {
            return 2;
        }
        if (position.x >= 0 && position.y < 0)
        {
            return 3;
        }

        return 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
