using UnityEngine;

public class PlatformLogic : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject Player = GameObject.FindGameObjectWithTag("Player");

        float distanceX = Player.transform.position.x - this.gameObject.transform.position.x;

        if (distanceX >= 160) 
        {
            Destroy(this.gameObject);
        }
    }
}
