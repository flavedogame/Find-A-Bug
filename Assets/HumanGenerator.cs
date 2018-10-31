using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HumanGenerator : MonoBehaviour
{
    public GameObject hero;
    public GameObject humanPrefab;
    public int NumOfHuman = 10;
    public Vector3 humanOriginPosition = new Vector3(0.54f, 0.78f, 013f);
    public int widthOfMap=30;
    public int heightOfMap = 20;
    public float initialDistance = 6.0f;
    // Start is called before the first frame update

    Vector3 RandomPosition()
    {
        int width = Random.Range(-widthOfMap, widthOfMap);
        int height = Random.Range(-heightOfMap, heightOfMap);
        return new Vector3(humanOriginPosition.x + width, humanOriginPosition.y + height, humanOriginPosition.z);
    }
    void OnEnable()
    {
        List<Vector3> positions = new List<Vector3>();
        List<InventoryEnum> inventorys = new List<InventoryEnum>();
        foreach (InventoryEnum inv in System.Enum.GetValues(typeof(InventoryEnum)))
        {
            inventorys.Add(inv);
        }
        List<string> names = new List<string>();
        List<bool> isBoys = new List<bool>();
        List<string> bestFriends = new List<string>();
        List<string> lovers = new List<string>();
        Dictionary<string, string> bestFriendPair = new Dictionary<string, string>();
        Dictionary<string, string> loverPair = new Dictionary<string, string>();
        for (int i = 0; i < NumOfHuman; i++)
        {
            isBoys.Add(Random.Range(0, 2)>0);
            names.Add(isBoys[i] ? EnumParser.boysNames[Random.Range(0, EnumParser.boysNames.Length)] :
                EnumParser.girlsNames[Random.Range(0, EnumParser.girlsNames.Length)]);
            bestFriends.Add(names[i]);
            lovers.Add(names[i]);
        }

            for (int i = 0; i < NumOfHuman; i++)
        {
            HumanInfo humanInfo = new HumanInfo() ;
            Vector3 position = new Vector3(0,0,0);
            if (i == 0)//init hero
            {
                position = RandomPosition();
                hero.transform.position = position;
                humanInfo = hero.GetComponent<HumanInfo>();
                HumanManager.Instance.hero = hero;
                HumanManager.Instance.heroInfo = humanInfo;
                
            } else
            {
                while (true)
                {
                    bool success = true;
                    position = RandomPosition();
                    foreach(Vector3 po in positions)
                    {
                        if (Vector3.Distance(position, po) < initialDistance)
                        {
                            success = false;
                        }
                    }
                    if (success)
                    {
                        break;
                    }
                }
                GameObject human = Instantiate(humanPrefab, position, Quaternion.identity);
                humanInfo = human.GetComponent<HumanInfo>();
            }
            positions.Add(position);

            humanInfo.Name = names[i];
            humanInfo.isBoy = isBoys[i];

            //relationship
            if (bestFriendPair.ContainsKey(humanInfo.Name))
            {
                humanInfo.bestFriendName = bestFriendPair[humanInfo.Name];
            }
            else
            {

                bestFriends.Remove(humanInfo.Name);
                humanInfo.bestFriendName = bestFriends[Random.Range(0, bestFriends.Count)];
                bestFriendPair[humanInfo.bestFriendName] = humanInfo.Name;
                bestFriends.Remove(humanInfo.bestFriendName);
            }
            if (loverPair.ContainsKey(humanInfo.Name))
            {
                humanInfo.loverName = loverPair[humanInfo.Name];
            }
            else
            {
                lovers.Remove(humanInfo.Name);
                humanInfo.loverName = lovers[Random.Range(0, lovers.Count)];
                loverPair[humanInfo.loverName] = humanInfo.Name;
                lovers.Remove(humanInfo.loverName);
            }


            if (HumanManager.Instance.heroInfo.bestFriendName.Equals(humanInfo.Name))
            {
                humanInfo.relationDescriptionEnum = RelationDescriptionEnum.bestFriend;
            }
            else if (HumanManager.Instance.heroInfo.loverName.Equals(humanInfo.Name))
            {
                humanInfo.relationDescriptionEnum = RelationDescriptionEnum.lover;
            }
            else
            {
                humanInfo.relationDescriptionEnum = (RelationDescriptionEnum)Random.Range(0, System.Enum.GetValues(typeof(RelationDescriptionEnum)).Length - 2);
            }

            //inventory
            humanInfo.Init();
            InventoryEnum inventory = (InventoryEnum)Random.Range(1, inventorys.Count);
            inventorys.Remove(inventory);
            humanInfo.inventories.Add(inventory);
            humanInfo.healthDescriptionEnum = HealthDescriptionEnum.healthy;
        }
        //generate human
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
