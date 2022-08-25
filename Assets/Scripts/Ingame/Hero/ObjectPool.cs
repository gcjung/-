using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;

    [SerializeField]
    private GameObject poolingObjectPrefab;

    private Queue<knight> poolingObjectQueue = new Queue<knight> ();

    private void Awake()
    {
        Instance = this;

        Initialize(20);
    }

    private knight CreateNewKnight()
    {
        var newObject = Instantiate(poolingObjectPrefab, transform).GetComponent<knight>();
        newObject.gameObject.SetActive(false);
        return newObject;
    }

    private void Initialize(int count)      //입력받은 개수만큼 우선 오브젝트 풀에 만들어놓기
    {
        for(int i = 0; i < count; i++)
        {
            poolingObjectQueue.Enqueue(CreateNewKnight());
        }
    }
    
    public static knight GetObject()
    {
        if(Instance.poolingObjectQueue.Count>0)
        {
            var obj = Instance.poolingObjectQueue.Dequeue();
            obj.transform.SetParent(null);
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            var newObj = Instance.CreateNewKnight();
            newObj.transform.SetParent(null);
            newObj.gameObject.SetActive(true);
            return newObj;
        }
    }

    public static void ReturnObject(knight knight)  //빌려줬던 오브젝트를 돌려받는 함수
    {
        knight.gameObject.SetActive(false);
        knight.transform.SetParent(Instance.transform);
        Instance.poolingObjectQueue.Enqueue(knight);
    }
    
}
