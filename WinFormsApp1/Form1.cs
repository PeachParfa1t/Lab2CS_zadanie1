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

            MessageBox.Show(result, "�������� �����������", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            string[] firstWords = ManualSplit(firstSentence); // ��������� ������ ����������� �� �����
            string[] secondWords = ManualSplit(secondSentence); // ��������� ������ ����������� �� �����
            string[] uniqueFirstWords = GetUniqueWords(firstWords); // ������� ��������� �� ������� �����������

            string result = "���������:\n"; // ������ ��� �������� ����������
            foreach (string word in uniqueFirstWords) // ���������� ���������� ����� �� ������� �����������
            {
                if (ContainsWord(secondWords, word)) // ���������, ���� �� ����� �� ������ �����������
                {
                    result += $"����� '{word}' ���� �� ������ �����������.\n"; // ��������� � ���������
                }
                else
                {
                    result += $"����� '{word}' ��� �� ������ �����������.\n"; // ��������� � ���������
                }
            }

            return result; // ���������� �������� ������
        }

        // ����� ��� ��������� ������ �� ����� ��� ������������� ����������� Split
        private static string[] ManualSplit(string sentence)
        {
            string[] words = new string[sentence.Length]; // ������� ������ ������������ ��������� �����
            int wordCount = 0; // ������� ���������� ����
            string currentWord = ""; // ���������� ��� �������� �������� �����

            for (int i = 0; i < sentence.Length; i++) // ���������� ������� ������
            {
                if (sentence[i] != ' ') // ���� ������ �� ������, ��������� ��� � ������� �����
                {
                    currentWord += sentence[i];
                }
                else if (currentWord != "") // ���� ���������� ������ � ������� ����� �� ������
                {
                    words[wordCount++] = currentWord; // ��������� ����� � ������
                    currentWord = ""; // ������� ���������� ��� ���������� �����
                }
            }

            if (currentWord != "") // ��������� ��������� �����, ���� ��� ����
            {
                words[wordCount++] = currentWord;
            }

            Array.Resize(ref words, wordCount); // �������� ������ �� ������������ ���������� ����
            return words; // ���������� ������ ����
        }

        // ����� ��� �������� ���������� �� ������� ����
        private static string[] GetUniqueWords(string[] words)
        {
            string[] uniqueWords = new string[words.Length]; // ������� ������ ��� ���������� ����
            int uniqueCount = 0; // ������� ���������� ����

            foreach (string word in words) // ���������� ��� �����
            {
                bool exists = false; // ���� ��� �������� ������� ����� � ������� ����������
                for (int i = 0; i < uniqueCount; i++) // ���������, ���� �� ����� ��� � ���������� �������
                {
                    if (uniqueWords[i] == word)
                    {
                        exists = true; // ���� ����� ����������, ������� �� �����
                        break;
                    }
                }
                if (!exists) // ���� ����� ���������
                {
                    uniqueWords[uniqueCount++] = word; // ��������� ��� � ������ ���������� ����
                }
            }

            Array.Resize(ref uniqueWords, uniqueCount); // �������� ������ �� ������������ ���������� ���������� ����
            return uniqueWords; // ���������� ������ ���������� ����
        }

        // ����� ��� �������� ������� ����� � ������� ����
        private static bool ContainsWord(string[] words, string word)
        {
            foreach (string w in words) // ���������� ��� ����� � �������
            {
                if (w == word) // ���� ����� ����������, ���������� true
                {
                    return true;
                }
            }
            return false; // ���� ����� �� �������, ���������� false
        }
    }


}
