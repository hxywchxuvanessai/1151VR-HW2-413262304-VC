
using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    private bool hasWon = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<DogController>() != null)
        {
            hasWon = true;
            Debug.Log("YOU WIN!");
        }
    }

    void OnGUI()
    {
        if (hasWon)
        {
            GUIStyle style = new GUIStyle(GUI.skin.label);
            style.fontSize = 50;
            style.alignment = TextAnchor.MiddleCenter;
            style.normal.textColor = Color.yellow;

            GUI.Label(
                new Rect(Screen.width / 2 - 200,
                         Screen.height / 2 - 50,
                         400, 100),
                "YOU WIN!",
                style
            );
        }
    }
}
