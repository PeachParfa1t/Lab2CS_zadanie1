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

            string[] firstWords = firstSentence.Split(' ');
            string[] secondWords = secondSentence.Split(' ');
            string[] uniqueFirstWords = firstWords.Distinct().ToArray();

            string result = "Результат:\n";
            foreach (string word in uniqueFirstWords)
            {
                if (secondWords.Contains(word))
                {
                    result += $"Слово '{word}' есть во втором предложении.\n";
                }
                else
                {
                    result += $"Слова '{word}' нет во втором предложении.\n";
                }
            }

            MessageBox.Show(result, "Проверка предложений", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveUserData();
        }

        private void label1_Click(object sender, EventArgs e) { }

        private void label3_Click(object sender, EventArgs e) { }

        private void textBox1_TextChanged(object sender, EventArgs e) { }

        private void textBox2_TextChanged(object sender, EventArgs e) { }
    }
}