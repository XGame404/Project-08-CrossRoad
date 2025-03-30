using UnityEngine;

public class CoinScript : MonoBehaviour
{
    void Update()
    {
        this.gameObject.transform.Rotate(-50f * Time.deltaTime, 0, 0);
    }
}
