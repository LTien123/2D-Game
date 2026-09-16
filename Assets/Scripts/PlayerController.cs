using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public float speed = 1f;
    public float burstSpeed = 3f;
    private float maxSpeed = 5f;
    private float elapsedTime = 0f;
    private float score = 0f;
    private float scoreMultiplier = 10f;
    private float highScore = 0f;

    private bool hasMoveing = false;

    public GameObject BoosterFlame;
    public UIDocument uIDocument;
    public GameObject explosionEffect;
    private Label scoreText;
    private Button restartButton;
    private Label highScoreText;

    //this fearture for the moblie version or webgl version
    public InputAction moveForward;
    public InputAction lookPosition;
    public InputAction boostAction;


    Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // this fearture for the moblie version or webgl version
        moveForward.Enable(); lookPosition.Enable(); boostAction.Enable();
        Input.multiTouchEnabled = true;

        rb = GetComponent<Rigidbody2D>();
        scoreText = uIDocument.rootVisualElement.Q<Label>("ScoreLabel");
        restartButton = uIDocument.rootVisualElement.Q<Button>("RestartButton");
        restartButton.style.display = DisplayStyle.None;
        restartButton.clicked += RestartGame;
        highScoreText = uIDocument.rootVisualElement.Q<Label>("HighScore");
        highScore = PlayerPrefs.GetFloat("HighScore", 0f);
        highScoreText.text = "High Score: " + highScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScore();
        MovePlayer();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
        HighScoreChange();
        Instantiate(explosionEffect, transform.position, transform.rotation); 
        restartButton.style.display = DisplayStyle.Flex;
        highScoreText.style.display = DisplayStyle.Flex;
    }

    private void HighScoreChange()
    {
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetFloat("HighScore", highScore);
            highScoreText.text = "High Score: " + highScore.ToString();
        }
    }

    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void UpdateScore()
    {
        elapsedTime += Time.deltaTime;
        score = Mathf.RoundToInt(elapsedTime * scoreMultiplier);
        scoreText.text = "Score: " + score.ToString();
    }

    private void TurnOffTheHighScore()
    {
        highScoreText.style.display = DisplayStyle.None;
    }


    private void MovePlayer()
    {
        if (moveForward.IsPressed())
        {
            if (!hasMoveing)
            {
                TurnOffTheHighScore();
                hasMoveing = true;
            }

            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(lookPosition.ReadValue<Vector2>());

            Vector2 direction = (mousePosition - transform.position).normalized;
            transform.up = direction;


            rb.AddForce(direction * speed);

            if (boostAction.IsPressed()) { rb.AddForce(direction * burstSpeed); }

            if (rb.linearVelocity.sqrMagnitude > maxSpeed * maxSpeed)
            {
                rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
            }
        }

        if (moveForward.IsPressed())
        {
            BoosterFlame.SetActive(true);

        }
        else if (moveForward.WasReleasedThisFrame())
        {
            BoosterFlame.SetActive(false);
        }
    }

}
