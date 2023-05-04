namespace Flashcards
{
    public partial class Form1 : Form
    {
        string[] fronts = { };
        string[] backs = { };
        int i = 0;
        bool front = true;
        bool fileLoaded = false;
        public Form1()
        {
            InitializeComponent();
            this.MaximizeBox = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (fileLoaded)
            {
                if (i >= fronts.Length || i < 0) i = 0;
                i += (backs.Length << 1);
                i -= 1;
                i %= backs.Length;
                front = true;
                label1.Text = fronts[i];
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;

                    //Read the contents of the file into a stream
                    var fileStream = openFileDialog.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        fileContent = reader.ReadToEnd();
                    }
                }
            }
            string[] lines = fileContent.Split("\n");
            if ((lines.Length & 1) != 0) {
                MessageBox.Show("File Invalid", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            fronts = new string[0];
            backs = new string[0];
            for (int j = 0; j < lines.Length; j+=2) {
                // MessageBox.Show(j.ToString());
                // MessageBox.Show(lines[j]);
                fronts = fronts.Append(lines[j]).ToArray();
                backs = backs.Append(lines[j+1]).ToArray();
            }
            fileLoaded = true;
            front = true;
            label1.Text = fronts[i];
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (fileLoaded)
            {
                if (i >= fronts.Length || i < 0) i = 0;
                front = !front;
                if (front)
                {
                    label1.Text = fronts[i];
                }
                else
                {
                    label1.Text = backs[i];
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {       if (fileLoaded)
                {
                    if (i >= fronts.Length || i < 0) i = 0;
                    i += (backs.Length << 1);
                    i += 1;
                    i %= backs.Length;
                    front = true;
                    label1.Text = fronts[i];
                }
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            System.Type objType = System.Type.GetTypeFromProgID("SAPI.spvoice");
            dynamic comObject = System.Activator.CreateInstance(objType);
            comObject.Speak(label1.Text);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}