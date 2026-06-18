using UnityEngine;
using UnityEngine.InputSystem;

public class PrefabSpawner : MonoBehaviour
{
    [SerializeField] private GameObject itemPrefab;

    private float spawnPointX = 0f;

    private void Update()
    {
        if(Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Vector3 spawnPosition = transform.position + Vector3.forward * 2f;
            spawnPosition.x = spawnPointX;
            Instantiate(itemPrefab, spawnPosition, Quaternion.identity);
            spawnPointX += 1.1f;
        }
    }
}
