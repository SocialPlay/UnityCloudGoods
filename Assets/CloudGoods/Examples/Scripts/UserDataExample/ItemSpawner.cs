using UnityEngine;
using System.Collections;

public class ItemSpawner : MonoBehaviour {

    public UserDataExample example;

    public GameObject Target;

    float timer = 0.0f;
    float maxTime = 1.0f;

    bool isSpawning = false;

    public void StartSpawning()
    {
        isSpawning = true;
    }


    void Update()
    {
        if (isSpawning == false)
            return;

        timer += Time.deltaTime;

        if(timer >= maxTime)
        {
            SpawnObject();
            timer = 0.0f;
        }
    }

	void SpawnObject()
    {
        SphereCollider sphereCollider = GetComponent<SphereCollider>();

        Vector3 spawnPosition = new Vector3(Random.insideUnitSphere.x * sphereCollider.radius / 2,
                 transform.position.y, Random.insideUnitSphere.z * sphereCollider.radius / 2);

        GameObject targetObj = (GameObject)GameObject.Instantiate(Target, spawnPosition, transform.rotation);
        targetObj.AddComponent<Target>().example = example;
    }
}
