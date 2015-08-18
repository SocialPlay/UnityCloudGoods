using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour {

    public UserDataExample example;

	public void Hit()
    {
        example.AddExperience(50);
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }

}
