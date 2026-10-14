
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public GameObject platformPrefab;

    public Vector2[] positions =
    {
        new Vector2(4f, -1.5f),
        new Vector2(8f, 0f),
        new Vector2(12f, -1.5f),
        new Vector2(16f, -3f)
    };

    void Start()
    {
        for (int i = 0; i < positions.Length; i++)
        {
            GameObject platform = Instantiate(
                platformPrefab,
                positions[i],
                Quaternion.identity
            );

            platform.name = "Platform_" + i;

            platform.transform.localScale =
                new Vector3(3f, 0.5f, 1f);
        }
    }
}
