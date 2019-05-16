using UnityEngine;

public class CraftScript : MonoBehaviour {

    [System.Serializable]
    public struct PrefabsStruct
    {
        public GameObject Prefab;
        public string TargetTag;

        public PrefabsStruct(GameObject _pref, string _targetTag)
        {
            Prefab = _pref;
            TargetTag = _targetTag;
        }

    }

    public PrefabsStruct[] pref;

    private int activePrefab = 0;

    public void ChoosePrefab(int i)
    {
        activePrefab = i;
    }

    public void InstantiatePrefab()
    {
        RaycastHit hit = Raycast();
        Vector3 grid = PlaneGrid(hit);


        if (hit.collider.gameObject.tag == pref[activePrefab].TargetTag)
        {
            if (activePrefab > 0)
            {
             if (hit.collider.gameObject.GetComponentsInChildren<BoxCollider>().Length < 2)
                {
                    grid.x = hit.collider.gameObject.transform.position.x;
                    grid.z = hit.collider.gameObject.transform.position.z;

                    GameObject newPrefab = Instantiate(pref[activePrefab].Prefab, new Vector3(grid.x, grid.y + pref[activePrefab].Prefab.transform.localScale.y / 2, grid.z), Quaternion.identity, hit.collider.gameObject.transform);

                    newPrefab.transform.localScale = new Vector3(newPrefab.transform.localScale.x / hit.collider.gameObject.transform.lossyScale.x, newPrefab.transform.localScale.y / hit.collider.gameObject.transform.lossyScale.y, newPrefab.transform.localScale.z / hit.collider.gameObject.transform.lossyScale.z);
                }
            }
            else
            {
                GameObject newPrefab = Instantiate(pref[activePrefab].Prefab, new Vector3(grid.x, grid.y + pref[activePrefab].Prefab.transform.localScale.y / 2, grid.z), Quaternion.identity, hit.collider.gameObject.transform);

                newPrefab.transform.localScale = new Vector3(newPrefab.transform.localScale.x / hit.collider.gameObject.transform.lossyScale.x, newPrefab.transform.localScale.y / hit.collider.gameObject.transform.lossyScale.y, newPrefab.transform.localScale.z / hit.collider.gameObject.transform.lossyScale.z);
            }
        }
    }

    public void RemovePrefab()
    {
        RaycastHit hit = Raycast();

        if (hit.collider.gameObject.tag != "Ground")
            Destroy(hit.collider.gameObject);
    }


    private RaycastHit Raycast()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
        Physics.Raycast(ray, out hit);
        return hit;
    }

    private Vector3 PlaneGrid(RaycastHit hit)
    {
        float prefX = pref[activePrefab].Prefab.transform.lossyScale.x;
        float prefZ = pref[activePrefab].Prefab.transform.lossyScale.z;

        float newX = Mathf.Round(hit.point.x / prefX) * prefX - prefX/2;

        float newZ = Mathf.Round(hit.point.z / prefZ) * prefZ - prefZ/2;
        
        float newY = 0;
        if (hit.collider.GetType() == typeof(BoxCollider))
        {
            BoxCollider box = hit.collider as BoxCollider;
            newY = box.size.y + (2 * box.center.y);
        }

        return new Vector3(newX, newY, newZ);
    }
}
