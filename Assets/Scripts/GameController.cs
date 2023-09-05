using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private Die die;
    [SerializeField] private Transform floor;
    [SerializeField] private Button startButton;
    [SerializeField] private TextMeshProUGUI rollCountText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI celebrationText;

    private const int MAX_ROLES = 10;

    private int remainingRoles;
    private int score;

    private void Start()
    {
        startButton.onClick.AddListener(StartGame);
    }

    private void Update()
    {
        MoveBoard();

        if (remainingRoles > 0) {
            if (die.IsComplete()) {
                score += die.Evaluate();
                remainingRoles--;
                UpdateUIText();

                if (remainingRoles > 0) {
                    die.Roll();
                }
            }
        } else {
            startButton.gameObject.SetActive(true);
            celebrationText.gameObject.SetActive(true);
            if (score == 60) {
                celebrationText.text = "PERFECT!!!";
            } else if (score >= 55) {
                celebrationText.text = "Incredible!!";
            } else if (score >= 50) {
                celebrationText.text = "Great!";
            } else if (score >= 42) {
                celebrationText.text = "Nice";
            } else if (score > 35) {
                celebrationText.text = "Better than average...";
            } else if (score > 20) {
                celebrationText.text = "You do know you can cheat?";
            } else if (score > 10) {
                celebrationText.text = "Are you trying to lose?";
            } else if (score == 10) {
                celebrationText.text = "Worst. Possible. Result.";
            } else {
                celebrationText.text = "";
            }
        }
    }

    private void UpdateUIText()
    {
        rollCountText.text = "Rolls: " + Mathf.Max(0, remainingRoles);
        scoreText.text = "Score: " + score;
    }

    private void StartGame()
    {
        startButton.gameObject.SetActive(false);
        celebrationText.gameObject.SetActive(false);
        remainingRoles = MAX_ROLES;
        score = 0;
        UpdateUIText();

        die.transform.position = Vector3.up;
        die.Roll();
    }

    private void MoveBoard()
    {
        Vector3 mousePosition = new Vector3(
            Mathf.Clamp(Input.mousePosition.x, 0, Screen.width),
            Mathf.Clamp(Input.mousePosition.y, 0, Screen.height));

        float x = mousePosition.x / Screen.width - 0.5f;
        float y = mousePosition.y / Screen.height - 0.5f;

        floor.rotation = Quaternion.AngleAxis(x * 20, Vector3.back) * Quaternion.AngleAxis(y * 20, Vector3.right);
    }
}
