using Unity.XR.CoreUtils;
using UnityEngine;

public class CarLogic : MonoBehaviour
{
    [SerializeField] private GameObject[] CarOptions;
    [SerializeField] private int MinSpeed;
    [SerializeField] private int MaxSpeed;
    private int MoveSpeed;
    void OnEnable()
    {
        int RandomOptions = Random.Range(0, CarOptions.Length);

        for (int counter = 0; counter < CarOptions.Length; counter++) 
        {
            CarOptions[counter].SetActive(counter == RandomOptions);
        }

        MoveSpeed = Random.Range(MinSpeed, MaxSpeed);
    }

    void Update()
    {
        MoveFunction();
    }

    void MoveFunction() 
    {
        this.gameObject.transform.Translate(new Vector3(0, 0, MoveSpeed * Time.deltaTime), Space.Self);

        if (this.gameObject.transform.position.z >= 85 || this.gameObject.transform.position.z <= -85) 
        { 
            Destroy(this.gameObject);
        }
    }
}
