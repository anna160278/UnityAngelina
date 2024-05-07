using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelController : MonoBehaviour
{
    [SerializeField] private List<GameObject> levelPrefab = new List<GameObject>();
    [SerializeField] private List<GameObject> levels = new List<GameObject>();

    private Transform player;

    public float spawnPos = 0;
    public float levelLength = 200;
    private int startLevels = 2;

    public GameObject heartPrefab;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;

        for (var i = 0; i <= startLevels; i++)
        {
            SpawnLevel(Random.Range(0, levelPrefab.Count));
        }
        //SpawnLevel(0);
    }
    void Update()
    {
        if (player.position.z > spawnPos - levelLength * startLevels) // 
        {
            SpawnLevel(Random.Range(0, levelPrefab.Count));
        }

        // Проходим по каждому уровню в списке (от 0 до длины списка)
        for (int i = 0; i < levels.Count; i++)
        {
            // Локальная переменная level определяется по индексу элемента в списке
            // + получаем transform объекта для определения координат 
            Transform level = levels[i].transform;

            // Если позиция уровня по оси z < позиция игрока по оси z - длина уровня
            // Сравниваем 1: 0 < 210 - 200 => 0 < 10 => true
            // Сравниваем 2: 0 < 200 - 200 => 0 < 0 => false
            // Это означает, что игрок должен быть на растоянии более 200 метров от того уровня, который хотим удалить
            // 0[_____]200[__plr__]400[_____]600[_____]800[_____]
            if (level.position.z < player.position.z - levelLength)
            {
                Debug.Log($"Уничтожается уровень {level.name} в позиции Z: {level.position.z}.\n" +
                $"Растояние уровня от игрока равно: {player.position.z - level.position.z} метров");
                // Удаляем уровень из списка уровней
                levels.RemoveAt(i);
                // Уничтожаем игровой объект
                Destroy(level.gameObject);
            }
            else
            {
                // Если не удаляется, узнаем почему
                // Debug.Log($"Уровней для удаления нет. Самый первый уровень на расстоянии от игрока: {player.position.z - level.position.z} метров");
            }
        }
    }

    private void SpawnLevel(int tileIndex)
    {
        GameObject level = Instantiate(levelPrefab[tileIndex], transform.forward * spawnPos, transform.rotation);
        spawnPos += levelLength;
        SpawnHearts(level);

        // Добавляем уровень в список уровней
        levels.Add(level);
    }

    private void SpawnHearts(GameObject place)
    {
        // Получите границы уровня
        Collider levelCollider = place.GetComponent<Collider>();
        if (levelCollider != null)
        {
            Vector3 minPosition = levelCollider.bounds.min;
            Vector3 maxPosition = levelCollider.bounds.max;

            // Получаем случайное количество сердечек, которые нужно создать
            int numHearts = 1;//Random.Range(1, 3);

            // Проходимся по каждому сердечку и создаем его в случайных координатах внутри уровня
            for (int i = 0; i < numHearts; i++)
            {
                Vector3 heartPosition = new Vector3(
                    Random.Range(minPosition.x, maxPosition.x),
                    Random.Range(minPosition.y, maxPosition.y),
                    Random.Range(minPosition.z, maxPosition.z)
                );

                Instantiate(heartPrefab, place.transform.position + heartPosition, Quaternion.identity);

            }
        }
    }
}
