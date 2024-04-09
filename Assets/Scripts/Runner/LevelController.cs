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

        // ѕроходим по каждому уровеню в списке (от 0 до длины списка)
        for (int i = 0; i < levels.Count; i++)
        {
            // Ћокальна€ переменна€ level определ€етс€ по индексу элемента в списке 
            // + получаем transform объекта дл€ определени€ координат 
            Transform level = levels[i].transform;

            // ≈сли позици€ уровн€ по оси z < позици€ игрока по оси z - длина уровн€
            // —равниваем 1: 0 < 210 - 200 => 0 < 10 => true
            // —равниваем 2: 0 < 200 - 200 => 0 < 0 => false
            // Ёто означает, что игрок должен быть на расто€нии более 200 метров от того уровн€, который хотим удалить
            // 0[_____]200[__plr__]400[_____]600[_____]800[_____]
            if (level.position.z < player.position.z - levelLength)
            {
                Debug.Log($"”ничтожаетс€ уровень {level.name} в позиции Z: {level.position.z}.\n" +
                    $"–асто€ние уровн€ от игрока равно: {player.position.z - level.position.z} метров");
                // ”дал€ем уровень из списка уровней
                levels.RemoveAt(i);
                // ”ничтожаем игровой объект
                Destroy(level.gameObject);
            }
            else
            {
                // ≈сли не удал€етс€, узнаем почему
                // Debug.Log($"”ровней дл€ удалени€ нет. —амый первый уровень на рассто€нии от игрока: {player.position.z - level.position.z} метров");
            }
        }
    }

    private void SpawnLevel(int tileIndex)
    {
        GameObject level = Instantiate(levelPrefab[tileIndex], transform.forward * spawnPos, transform.rotation);
        spawnPos += levelLength;
        SpawnHearts(level);

        // ƒобавл€ем уровень в список уровней
        levels.Add(level);
    }

    private void SpawnHearts(GameObject place)
    {
        // ѕолучите границы уровн€
        Collider levelCollider = place.GetComponent<Collider>();
        if (levelCollider != null)
        {
            Vector3 minPosition = levelCollider.bounds.min;
            Vector3 maxPosition = levelCollider.bounds.max;

            // ѕолучаем случайное количество сердечек, которые нужно создать
            int numHearts = 1;//Random.Range(1, 3);

            // ѕроходимс€ по каждому сердечку и создаем его в случайных координатах внутри уровн€
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
