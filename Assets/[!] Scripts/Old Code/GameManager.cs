using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] CharactersList;
    [SerializeField] private int SelectedCharID;
    private bool playerSpawned = false;
    void Update()
    {
        if (!playerSpawned)
        {
            //Instantiate(CharactersList[GameDataManager.GetSelectedChar()],
            //            this.gameObject.transform.position,
            //            CharactersList[GameDataManager.GetSelectedChar()].transform.rotation);

            Instantiate(CharactersList[GameDataManager.GetSelectedChar()],
                        this.gameObject.transform.position,
                        CharactersList[GameDataManager.GetSelectedChar()].transform.rotation);
            playerSpawned = true;
        }
    }
}
