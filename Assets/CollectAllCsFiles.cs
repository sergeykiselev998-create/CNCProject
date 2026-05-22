#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;
using System.Collections.Generic;

public class CollectAllCsFiles : MonoBehaviour
{
    // Путь к папке относительно корня проекта (например: "Assets/Scripts")
    // По умолчанию — ищем все .cs в Assets/
    public string folderPath = "Assets/Scripts";

    [ContextMenu("Собрать все .cs файлы")]
    public void CollectAndMergeCsFiles()
    {
        // Получаем абсолютный путь к папке проекта (где лежит Assets/)
        string projectRoot = Path.GetFullPath(Path.Combine(Application.dataPath, ".."));
        string targetFolderPath = Path.Combine(projectRoot, folderPath);

        if (!Directory.Exists(targetFolderPath))
        {
            Debug.LogError($"Папка не найдена: {targetFolderPath}");
            return;
        }

        // Рекурсивный поиск всех .cs файлов
        string[] csFiles = Directory.GetFiles(targetFolderPath, "*.cs", SearchOption.AllDirectories);

        if (csFiles.Length == 0)
        {
            Debug.LogWarning($"Не найдено ни одного .cs файла в: {targetFolderPath}");
            return;
        }

        Debug.Log($"Найдено {csFiles.Length} .cs файлов. Начинаю объединение...");

        var sb = new StringBuilder();
        int fileCount = 0;

        foreach (string filePath in csFiles)
        {
            // Добавляем заголовок с путём (относительно корня проекта)
            string relativePath = filePath.Replace(projectRoot + Path.DirectorySeparatorChar, "");
            sb.AppendLine($"// ===== {relativePath} =====");
            sb.AppendLine();

            try
            {
                string content = File.ReadAllText(filePath, Encoding.UTF8);
                sb.AppendLine(content);
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"Ошибка чтения файла {filePath}: {ex.Message}");
                sb.AppendLine($"// !!! ОШИБКА ЧТЕНИЯ ФАЙЛА: {ex.Message}");
            }

            sb.AppendLine();
            sb.AppendLine("// " + new string('=', 80));
            sb.AppendLine();
            fileCount++;
        }

        // Путь для выходного файла: корень проекта / AllCsFiles.txt
        string outputPath = Path.Combine(projectRoot, "AllCsFiles.txt");

        try
        {
            File.WriteAllText(outputPath, sb.ToString(), Encoding.UTF8);
            Debug.Log($"✅ Файл сохранён: {outputPath}");
            EditorUtility.RevealInFinder(outputPath); // Открывает проводник на macOS / Проводник Windows
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"❌ Ошибка записи файла: {ex.Message}");
        }
    }
}
#endif