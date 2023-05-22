using System;
using System.Windows.Forms;

namespace CMDproject
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Select interface type:");
            Console.WriteLine("1. GUI");
            Console.WriteLine("2. Text-based");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    RunGui();
                    break;
                case "2":
                    RunTextBased();
                    break;
                default:
                    Console.WriteLine("Invalid choice. Exiting...");
                    break;
            }
        }

        static void RunGui()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MyForm());
        }

        static void RunTextBased()
        {
            Console.WriteLine("Running in text-based interface mode.");

            bool running = true;
            while (running)
            {
                Console.Write($"{FileOperations.GetCurrentDirectory()}> ");
                string command = Console.ReadLine();
                string[] parts = command.Split(' ');

                if (parts.Length > 0)
                {
                    switch (parts[0])
                    {
                        case "cd":
                            if (parts.Length > 1)
                            {
                                if (FileOperations.ChangeDirectory(parts[1]))
                                {
                                    Console.WriteLine($"Current directory changed to {FileOperations.GetCurrentDirectory()}.");
                                }
                                else
                                {
                                    Console.WriteLine("Directory not found.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Please provide a directory name.");
                            }
                            break;

                        case "ls":
                            var files = FileOperations.GetFileList();
                            foreach (var file in files)
                            {
                                Console.WriteLine(file);
                            }
                            break;

                        case "cat":
                            if (parts.Length > 1)
                            {
                                Console.WriteLine(FileOperations.ReadFile(parts[1]));
                            }
                            else
                            {
                                Console.WriteLine("Please provide a file name.");
                            }
                            break;

                        case "rm":
                            if (parts.Length > 1)
                            {
                                if (FileOperations.DeleteFile(parts[1]))
                                {
                                    Console.WriteLine("File deleted successfully.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Please provide a file name.");
                            }
                            break;

                        case "createfile":
                            if (parts.Length > 1)
                            {
                                if (FileOperations.CreateFile(parts[1]))
                                {
                                    Console.WriteLine("File created successfully.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Please provide a file name.");
                            }
                            break;

                        case "copyfile":
                            if (parts.Length > 2)
                            {
                                if (FileOperations.CopyFile(parts[1], parts[2]))
                                {
                                    Console.WriteLine("File copied successfully.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Please provide source and destination file names.");
                            }
                            break;

                        case "mkdir":
                            if (parts.Length > 1)
                            {
                                if (FileOperations.CreateDirectory(parts[1]))
                                {
                                    Console.WriteLine("Directory created successfully.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Please provide a directory name.");
                            }
                            break;

                        case "exit":
                            running = false;
                            break;

                        default:
                            Console.WriteLine("Invalid command.");
                            break;
                    }
                }
            }
        }
    }

    class MyForm : Form
    {
        private TextBox textBox;
        private Button button;

        public MyForm()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            textBox = new TextBox()
            {
                Location = new System.Drawing.Point(10, 10),
                Size = new System.Drawing.Size(200, 20)
            };

            button = new Button()
            {
                Text = "Execute",
                Location = new System.Drawing.Point(10, 40),
                Size = new System.Drawing.Size(75, 23)
            };

            button.Click += Button_Click;

            Controls.Add(textBox);
            Controls.Add(button);
        }

        private void Button_Click(object sender, EventArgs e)
        {
            string command = textBox.Text;
            string[] parts = command.Split(' ');

            if (parts.Length > 0)
            {
                switch (parts[0])
                {
                    case "cd":
                        if (parts.Length > 1)
                        {
                            if (FileOperations.ChangeDirectory(parts[1]))
                            {
                                MessageBox.Show($"Current directory changed to {FileOperations.GetCurrentDirectory()}.");
                            }
                            else
                            {
                                MessageBox.Show("Directory not found.");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Please provide a directory name.");
                        }
                        break;

                    case "ls":
                        var files = FileOperations.GetFileList();
                        string fileList = string.Join(Environment.NewLine, files);
                        MessageBox.Show(fileList);
                        break;

                    case "cat":
                        if (parts.Length > 1)
                        {
                            string content = FileOperations.ReadFile(parts[1]);
                            MessageBox.Show(content);
                        }
                        else
                        {
                            MessageBox.Show("Please provide a file name.");
                        }
                        break;

                    case "rm":
                        if (parts.Length > 1)
                        {
                            if (FileOperations.DeleteFile(parts[1]))
                            {
                                MessageBox.Show("File deleted successfully.");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Please provide a file name.");
                        }
                        break;

                    case "createfile":
                        if (parts.Length > 1)
                        {
                            if (FileOperations.CreateFile(parts[1]))
                            {
                                MessageBox.Show("File created successfully.");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Please provide a file name.");
                        }
                        break;

                    case "copyfile":
                        if (parts.Length > 2)
                        {
                            if (FileOperations.CopyFile(parts[1], parts[2]))
                            {
                                MessageBox.Show("File copied successfully.");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Please provide source and destination file names.");
                        }
                        break;

                    case "mkdir":
                        if (parts.Length > 1)
                        {
                            if (FileOperations.CreateDirectory(parts[1]))
                            {
                                MessageBox.Show("Directory created successfully.");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Please provide a directory name.");
                        }
                        break;

                    case "exit":
                        Application.Exit();
                        break;

                    default:
                        MessageBox.Show("Invalid command.");
                        break;
                }
            }
        }
    }
}

