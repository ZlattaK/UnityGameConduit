using UnityEngine;
using System.IO;

public class PasswordFileSpawner : MonoBehaviour
{
    public string fileName = "read_me.txt";
    [TextArea]
    public string fileText = "Экспедиция A-6.\r\nЗапись продолжается… если это всё ещё считается записью.\r\n\r\n" +
        "Нулевой уровень нестабилен. Мы ошибались.\r\nОн не просто меняется — он реагирует.\r\n\r\n" +
        "4 раза за последние сутки приборы фиксировали отклик без источника.\r\nМы проверили всё. Ошибка исключена.\r\n\r\n" +
        "7 часов назад один из участников утверждал, что слышал сигнал.\r\nМы не зафиксировали ничего." +
        "\r\nНо он назвал координаты… до того, как мы их получили.\r\n\r\n" +
        "2 объекта были перемещены без физического контакта.\r\nСледов воздействия нет.\r\n\r\n" +
        "9 минут назад…\r\nзапись прерывалась.\r\nЯ не помню, что происходило в этот промежуток.\r\n\r\n…\r\n\r\n" +
        "Если кто-то найдёт этот файл —\r\nдоступ к ящику был ограничен не нами.\r\n\r\n" +
        "Ключ был скрыт для сохранности.\r\nНо наблюдатель всегда видит больше, чем участник.\r\n\r\n" +
        "Если ты смотришь —\r\nзначит, система уже открыта для тебя.";

    private bool created = false;

    public void CreateFile()
    {
        if (created) return;

        string path = "";

#if UNITY_EDITOR
        path = Path.Combine(Application.dataPath, fileName);
#else
        string desktop = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);
        path = Path.Combine(desktop, fileName);
#endif

        File.WriteAllText(path, fileText);

        Debug.Log("Файл создан: " + path);

        created = true;
    }
}