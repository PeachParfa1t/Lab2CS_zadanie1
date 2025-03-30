using System;
using System.IO;
using System.Windows.Forms;
using Microsoft.VisualBasic.Logging;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        private const string SaveFilePath = "user_data.txt";

        public Form1()
        {
            InitializeComponent();
            LoadUserData();
        }

        private void LoadUserData()
        {
            if (File.Exists(SaveFilePath))
            {
                string[] data = File.ReadAllLines(SaveFilePath);
                if (data.Length >= 2)
                {
                    textBox1.Text = data[0];
                    textBox2.Text = data[1];
                }
            }
        }

        private void SaveUserData()
        {
            string[] data = { textBox1.Text, textBox2.Text };
            File.WriteAllLines(SaveFilePath, data);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string firstSentence = textBox1.Text;
            string secondSentence = textBox2.Text;

            string result = Logic.CompareSentences(firstSentence, secondSentence);

            MessageBox.Show(result, "Проверка предложений", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveUserData();
        }

        private void textBox1_TextChanged(object sender, EventArgs e) { }

        private void textBox2_TextChanged(object sender, EventArgs e) { }
    }

    public static class Logic
    {
        public static string CompareSentences(string firstSentence, string secondSentence)
        {
            string[] firstWords = ManualSplit(firstSentence); // Разбиваем первое предложение на слова
            string[] secondWords = ManualSplit(secondSentence); // Разбиваем второе предложение на слова
            string[] uniqueFirstWords = GetUniqueWords(firstWords); // Убираем дубликаты из первого предложения

            string result = "Результат:\n"; // Строка для хранения результата
            foreach (string word in uniqueFirstWords) // Перебираем уникальные слова из первого предложения
            {
                if (ContainsWord(secondWords, word)) // Проверяем, есть ли слово во втором предложении
                {
                    result += $"Слово '{word}' есть во втором предложении.\n"; // Добавляем в результат
                }
                else
                {
                    result += $"Слова '{word}' нет во втором предложении.\n"; // Добавляем в результат
                }
            }

            return result; // Возвращаем итоговую строку
        }

        // Метод для разбиения строки на слова без использования встроенного Split
        private static string[] ManualSplit(string sentence)
        {
            string[] words = new string[sentence.Length]; // Создаем массив максимальной возможной длины
            int wordCount = 0; // Счетчик количества слов
            string currentWord = ""; // Переменная для хранения текущего слова

            for (int i = 0; i < sentence.Length; i++) // Перебираем символы строки
            {
                if (sentence[i] != ' ') // Если символ не пробел, добавляем его в текущее слово
                {
                    currentWord += sentence[i];
                }
                else if (currentWord != "") // Если встретился пробел и текущее слово не пустое
                {
                    words[wordCount++] = currentWord; // Добавляем слово в массив
                    currentWord = ""; // Очищаем переменную для следующего слова
                }
            }

            if (currentWord != "") // Добавляем последнее слово, если оно есть
            {
                words[wordCount++] = currentWord;
            }

            Array.Resize(ref words, wordCount); // Обрезаем массив до фактического количества слов
            return words; // Возвращаем массив слов
        }

        // Метод для удаления дубликатов из массива слов
        private static string[] GetUniqueWords(string[] words)
        {
            string[] uniqueWords = new string[words.Length]; // Создаем массив для уникальных слов
            int uniqueCount = 0; // Счетчик уникальных слов

            foreach (string word in words) // Перебираем все слова
            {
                bool exists = false; // Флаг для проверки наличия слова в массиве уникальных
                for (int i = 0; i < uniqueCount; i++) // Проверяем, есть ли слово уже в уникальном массиве
                {
                    if (uniqueWords[i] == word)
                    {
                        exists = true; // Если нашли совпадение, выходим из цикла
                        break;
                    }
                }
                if (!exists) // Если слово уникально
                {
                    uniqueWords[uniqueCount++] = word; // Добавляем его в массив уникальных слов
                }
            }

            Array.Resize(ref uniqueWords, uniqueCount); // Обрезаем массив до фактического количества уникальных слов
            return uniqueWords; // Возвращаем массив уникальных слов
        }

        // Метод для проверки наличия слова в массиве слов
        private static bool ContainsWord(string[] words, string word)
        {
            foreach (string w in words) // Перебираем все слова в массиве
            {
                if (w == word) // Если нашли совпадение, возвращаем true
                {
                    return true;
                }
            }
            return false; // Если слово не найдено, возвращаем false
        }
    }


}
