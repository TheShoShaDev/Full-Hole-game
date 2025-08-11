using UnityEngine;

public class SpiralGenerator : MonoBehaviour
{
    [Header("Основные параметры")]
    public GameObject prefab;
    public int objectCount = 200;
    public float size = 10f;
    public float height = 0f;

    [Header("Настройки внешнего вида")]
    public bool useGoldenAngle = true;
    public float angleStep = 137.5f; // Золотой угол
    public float sizeVariation = 0.1f;

    [ContextMenu("Сгенерировать спираль")]
    public void GenerateSpiral()
    {
        ClearSpiral();

        // Используем золотой угол по умолчанию
        if (useGoldenAngle) angleStep = 137.508f;

        float goldenAngle = angleStep * Mathf.Deg2Rad;

        for (int i = 1; i <= objectCount; i++)
        {
            // Формула спирали Ферма
            float r = size * Mathf.Sqrt(i / (float)objectCount);
            float theta = i * goldenAngle;

            Vector3 position = new Vector3(
                r * Mathf.Cos(theta),
                height,
                r * Mathf.Sin(theta)
            );

            // Создаем объект с небольшими вариациями
            GameObject obj = Instantiate(
                prefab,
                position,
                Quaternion.Euler(0, theta * Mathf.Rad2Deg, 0),
                transform
            );

            // Добавляем случайное изменение размера
           // float scale = 1f + Random.Range(-sizeVariation, sizeVariation);
           // obj.transform.localScale = Vector3.one * scale;
        }
    }

    [ContextMenu("Очистить спираль")]
    public void ClearSpiral()
    {
        for (int i = 0; i <= 10; i++)
        {
            foreach (Transform child in transform)
            {
                if (Application.isPlaying)
                    Destroy(child.gameObject);
                else
                    DestroyImmediate(child.gameObject);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        if (transform.childCount > 1)
        {
            for (int i = 1; i < transform.childCount; i++)
            {
                Gizmos.DrawLine(
                    transform.GetChild(i - 1).position,
                    transform.GetChild(i).position
                );
            }
        }
    }
}