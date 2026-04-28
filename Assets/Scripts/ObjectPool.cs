using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Pool
{
    public string tag;          // Tên nhận dạng (VD: "Box", "Bomb")
    public GameObject prefab;   // Prefab tương ứng
    public int size;            // Số lượng muốn tạo sẵn
}
public class ObjectPool : MonoBehaviour
{
    // Tạo Singleton để dễ dàng gọi từ các script khác
    public static ObjectPool Instance;
    [Header("Danh sách các Pool")]
    public List<Pool> pools; // Danh sách các ngăn tủ (Bạn sẽ thêm Box và Bomb vào đây)

    // Từ điển (Dictionary) để quản lý: Key là Tag (string), Value là Hàng đợi (Queue)
    private Dictionary<string, Queue<GameObject>> poolDictionary;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        // Duyệt qua từng loại Pool bạn đã cài đặt trong Inspector
        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectQueue = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.transform.SetParent(this.transform); // Đặt con của ObjectPool để dễ quản lý trong Hierarchy
                obj.SetActive(false);
                objectQueue.Enqueue(obj);
            }

            // Thêm hàng đợi vừa tạo vào Từ điển với chìa khóa là tag
            poolDictionary.Add(pool.tag, objectQueue);
        }
    }

    // Lấy vật thể ra dựa vào Tag
    public GameObject GetObjectFromPool(string tag)
    {
        // Kiểm tra xem tủ có ngăn nào tên là tag này không
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool với tag " + tag + " không tồn tại!");
            return null;
        }

        if (poolDictionary[tag].Count > 0)
        {
            Debug.Log("Đang lấy " + tag + " từ pool. Số lượng còn lại: " + (poolDictionary[tag].Count - 1));
            GameObject obj = poolDictionary[tag].Dequeue();
            obj.SetActive(true);
            return obj;
        }

        // (Tùy chọn) Có thể code thêm phần tự động Instantiate nếu hết đạn ở đây
        Debug.LogWarning("Pool " + tag + " đã cạn!");
        GameObject newObj = Instantiate(pools.Find(p => p.tag == tag).prefab);
        newObj.transform.SetParent(this.transform);
        return newObj;
    }

    // Trả vật thể về đúng ngăn tủ của nó
    public void ReturnObjectToPool(string tag, GameObject obj)
    {
        if (!poolDictionary.ContainsKey(tag)) return;

        obj.SetActive(false);
        poolDictionary[tag].Enqueue(obj);
    }
}
