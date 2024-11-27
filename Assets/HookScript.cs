using UnityEngine;

public class HookScript : MonoBehaviour
{
    public float fallSpeed = 5f;          // Speed for downward movement
    public float horizontalSpeed = 5f;   // Speed for left-right movement
    public int score = 0;                // Score to track points

    void Start()
    {
        Debug.Log("Hook initialized");
    }

    void Update()
    {
        //if (GameObject.FindObjectOfType<LogicScript>().isGameOver) return;
        // Move the hook downward
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);

        // Move the hook left and right with arrow keys
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * horizontalSpeed * Time.deltaTime);
            Debug.Log("Hook moving left");
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * horizontalSpeed * Time.deltaTime);
            Debug.Log("Hook moving right");
        }
    }
}
