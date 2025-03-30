using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerLogic : MonoBehaviour
{
    [Header("---- Jumping Data ----")]
    [SerializeField] private float JumpForce = 8f;
    [SerializeField] private float ForwardForce = 5f;
    [SerializeField] private GameObject GroundCheckPoint;
    [SerializeField] private float CheckRadius = 0.2f;
    [SerializeField] private LayerMask WhatIsGround;

    private Rigidbody _playerRB;
    private bool IsGrounded;
    private bool _hasJumped;

    [Header("---- Resources Gathering ----")]

    private AudioSource _playerAudio;
    [SerializeField] private AudioClip CoinGatheredSFX;
    [SerializeField] private AudioClip DieEffectSXF;
    [SerializeField] private AudioClip PlayerJumpSFX;

    private Camera mainCamera;
    private Vector3 camDistance;
    [SerializeField] private float lerpSpeed = 10f;

    [SerializeField] private GameObject CoinGatheredEffect;
    [SerializeField] private GameObject DieEffect;
    
    private GameObject Score_GO;
    private TMP_Text Score_Text;
    private int PlayerScoreAndCoin = 0;
    private bool Able2Move = true;
    void Start()
    {
        _playerRB = GetComponent<Rigidbody>();
        _playerAudio = GetComponent<AudioSource>();

        mainCamera = Camera.main;

        if (mainCamera != null)
        {
            camDistance = mainCamera.transform.position - transform.position;
        }

        Score_GO = GameObject.FindGameObjectWithTag("ScoreText");
        
        if (Score_GO != null)
        {
            Score_Text = Score_GO.GetComponent<TMP_Text>();
        }

        PlayerScoreAndCoin = 0;
    }

    void Update()
    {
        if (Able2Move)
        {
            MovementFunction();
            Score_Text.text = PlayerScoreAndCoin.ToString("D4");
            GameDataManager.NewestCoinNumbGathered(PlayerScoreAndCoin);
        }
    }

    private void MovementFunction()
    {
        IsGrounded = Physics.CheckSphere(GroundCheckPoint.transform.position, CheckRadius, WhatIsGround);

        if (IsGrounded)
        {
            _hasJumped = false;
        }

        if (IsGrounded && Input.GetMouseButtonDown(0) && !_hasJumped)
        {
            
            Vector3 jumpForce = new Vector3(0, JumpForce, 0);
            _playerRB.AddForce(jumpForce, ForceMode.Impulse);
            _playerAudio.PlayOneShot(PlayerJumpSFX);

        }

        if (!IsGrounded && !_hasJumped) 
        {
            _playerRB.linearVelocity = new Vector3(ForwardForce, _playerRB.linearVelocity.y, 0);
            _hasJumped = true;
        }

        Vector3 desiredPosition = transform.position + camDistance;

        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, desiredPosition, Time.deltaTime * lerpSpeed);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(GroundCheckPoint.transform.position, CheckRadius);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            _playerAudio.PlayOneShot(CoinGatheredSFX);
            Instantiate(CoinGatheredEffect, other.transform.position, Quaternion.identity);
            PlayerScoreAndCoin++;
            GameDataManager.AddCoins(1);
        }

        if (other.CompareTag("Car"))
        {
            Destroy(other.gameObject); 
            Able2Move = false;
            _playerAudio.PlayOneShot(DieEffectSXF);
            this.gameObject.transform.localScale = Vector3.zero;
            Instantiate(DieEffect, new Vector3(this.gameObject.transform.position.x,
                this.gameObject.transform.position.y + 2.5f,
                this.gameObject.transform.position.z), Quaternion.identity);
            StartCoroutine(SceneChange());
        }
    }

    IEnumerator SceneChange()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Result");
    }
}