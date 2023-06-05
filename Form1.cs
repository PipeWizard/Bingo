using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bingo
{
    public partial class Form1 : Form
    {
        private DataGridView dGV = new DataGridView();

        //colours
        private readonly Color cellBackColor = Color.White;
        private readonly Color cellGridColor = Color.Black;
        private readonly Color cellMarked = Color.LightSalmon;
        private Random random = new Random();

        // TODO - maybe turn all for loops into a series of 'GetGridData()' methods


        public Form1()
        {
            InitializeComponent();
            Directory.CreateDirectory("saves");
            Load += new EventHandler(Form1_Load);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SetupLayout();
            SetupDataGridView();
            PopulateDataGridView();
        }

        private void SetupLayout()
        {
            Menu = new MainMenu();

            MenuItem item = new MenuItem("File");
                Menu.MenuItems.Add(item);
                item.MenuItems.Add("Save", new EventHandler(Save_Click));
                item.MenuItems.Add("Load", new EventHandler(Load_Click));

            item = new MenuItem("Edit");
                Menu.MenuItems.Add(item);
                item.MenuItems.Add("Clear Board", new EventHandler(Clear_Board));
                item.MenuItems.Add("Clear Progress", new EventHandler(Clear_Progress));
                item.MenuItems.Add("New Board", new EventHandler(New_Board));

            buttonProgress.MouseClick += Button_Clear_Progress;
            buttonBoard.MouseClick += Button_New_Board;
            buttonLogbingo.MouseClick += Button_Log_Bingo;
            buttonGetBingos.MouseClick += Button_Get_Bingos;
        }

        private async void Button_Get_Bingos(object sender, MouseEventArgs e)
        {
            string currentDir = AppDomain.CurrentDomain.BaseDirectory;
            string file_Bingos = Path.Combine(currentDir, @"..\..\saves\bingos");
            string fPath_Bingos = Path.GetFullPath(file_Bingos);

            if (File.Exists(fPath_Bingos))
            {
                using (FileStream fs = File.Open(fPath_Bingos, FileMode.Open))
                {
                    //BinaryFormatter bf = new BinaryFormatter();
                    using (BinaryReader br = new BinaryReader(fs))
                    {
                        //bingoBox.Text = "Total number of Bingos: " + br.ReadInt32();
                        //bingoBox.Visible = true;
                        //bingoBox.ReadOnly = true;
                        DialogResult _ = MessageBox.Show("Bingos: " + br.ReadInt32(), "Bingo Log", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                }
                await Task.Delay(5000);
           }
            else
            {
                DialogResult _ = MessageBox.Show("No bingos logged.", "Bingo Log", MessageBoxButtons.OK, MessageBoxIcon.None);
            }
        }

        private void Button_Log_Bingo(object sender, MouseEventArgs e)
        {
            string currentDir = AppDomain.CurrentDomain.BaseDirectory;
            string file_Bingos = Path.Combine(currentDir, @"..\..\saves\bingos");
            string fPath_Bingos = Path.GetFullPath(file_Bingos);
            
            int acc = 0;
            using (var stream = File.Open(fPath_Bingos, FileMode.OpenOrCreate))
            {
                using (BinaryReader br = new BinaryReader(stream, Encoding.UTF8, true))
                {
                    if (br.PeekChar() != -1)
                        acc = br.ReadInt32();
                }
                using (BinaryWriter bw = new BinaryWriter(stream, Encoding.UTF8, false))
                {
                    acc++;
                    bw.Seek(0, SeekOrigin.Begin);
                    bw.Write(acc);
                }
            }
        }

        private void New_Board(object sender, EventArgs e)
        {
            Clear_Board(sender, e);
            PopulateDataGridView();
            RemoveEmptyRows();
            dGV.ClearSelection();
        }

        private void Clear_Progress(object sender, EventArgs e)
        {
            for (int i = 0; i < dGV.RowCount; i++)
            {
                for (int j = 0; j < dGV.ColumnCount; j++)
                {
                    dGV.Rows[i].Cells[j].Style.BackColor = cellBackColor;
                }
            }
        }

        private void Clear_Board(object sender, EventArgs e)
        {
            for (int i = 0; i < dGV.RowCount; i++)
            {
                for (int j = 0; j < dGV.ColumnCount; j++)
                {
                    dGV.Rows[i].Cells[j].Value = null;
                    dGV.Rows[i].Cells[j].Style.BackColor = cellBackColor;
                }
            }

            dGV.ClearSelection();
        }

        private void Load_Click(object sender, EventArgs e)
        {
            string currentDir = AppDomain.CurrentDomain.BaseDirectory;
            string file_Card = Path.Combine(currentDir, @"..\..\saves\card");
            string fPath_Card = Path.GetFullPath(file_Card);
            string file_Progress = Path.Combine(currentDir, @"..\..\saves\progress");
            string fPath_Progress = Path.GetFullPath(file_Progress);

            Clear_Board(sender, e);

            if (File.Exists(fPath_Card))
            {
                using (var stream = File.Open(fPath_Card, FileMode.Open))
                {
                    using (BinaryReader reader = new BinaryReader(stream, Encoding.UTF8, false))
                    {
                        for (int i = 0; i < dGV.RowCount; i++)
                        {
                            for (int j = 0; j < dGV.ColumnCount; j++)
                            {
                                dGV.Rows[i].Cells[j].Value = reader.ReadString();
                            }
                        }
                    }
                }

                using (var stream = File.Open(fPath_Progress, FileMode.Open))
                {
                    using (BinaryReader reader = new BinaryReader(stream, Encoding.UTF8, false))
                    {
                        //reader.BaseStream.Seek(0, SeekOrigin.Begin);
                        for (int i = 0; i < dGV.RowCount; i++)
                        {
                            for (int j = 0; j < dGV.ColumnCount; j++)
                            {
                                if (reader.ReadBoolean())
                                    dGV.Rows[i].Cells[j].Style.BackColor = cellMarked;
                            }
                        }
                    }
                }
            }
            else
            {
                DialogResult _ = MessageBox.Show("No save detected.  Please save a card before attempting to load one.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Save_Click(object sender, EventArgs e)
        {
            string currentDir = AppDomain.CurrentDomain.BaseDirectory;
            string file_Card = Path.Combine(currentDir, @"..\..\saves\card");
            string fPath_Card = Path.GetFullPath(file_Card);
            string file_Progress = Path.Combine(currentDir, @"..\..\saves\progress");
            string fPath_Progress = Path.GetFullPath(file_Progress);

            using (var stream = File.Open(fPath_Card, FileMode.OpenOrCreate))
            {
                using (BinaryWriter writer = new BinaryWriter(stream, Encoding.UTF8, false))
                {
                    for (int i = 0; i < dGV.RowCount; i++)
                    {
                        for (int j = 0; j < dGV.ColumnCount; j++)
                        {
                            writer.Write(dGV.Rows[i].Cells[j].Value.ToString());
                        }
                    }
                }
            }

            using (var stream = File.Open(fPath_Progress, FileMode.OpenOrCreate))
            {
                using (BinaryWriter writer = new BinaryWriter(stream, Encoding.UTF8, false))
                {
                    for (int i = 0; i < dGV.RowCount; i++)
                    {
                        for (int j = 0; j < dGV.ColumnCount; j++)
                        {
                            if (dGV.Rows[i].Cells[j].Style.BackColor == cellMarked)
                                writer.Write(true);
                            else
                                writer.Write(false);
                        }
                    }
                }
            }
        }

        private void SetupDataGridView()
        {
            Controls.Add(dGV);
            dGV.ColumnCount = 5;
            dGV.ColumnHeadersDefaultCellStyle.BackColor = cellBackColor;
            dGV.ColumnHeadersDefaultCellStyle.Font = new Font(dGV.Font, FontStyle.Bold);

            dGV.Name = "bingoDataGridView";
            //dataGridView.Location = new Point(8, 8);
            dGV.Size = new Size(800, 800);  //scale off monitor resolution? /shrug
            dGV.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dGV.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            dGV.GridColor = cellGridColor;
            dGV.RowHeadersVisible = false;
            //dataGridView.AutoResizeRows();
            dGV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dGV.AutoResizeColumns();
            dGV.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dGV.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dGV.AllowUserToAddRows = false;
            dGV.AllowUserToResizeRows = false;
            dGV.ReadOnly = true;
            dGV.AllowUserToResizeColumns = false;
            dGV.ColumnHeadersHeight = 40;
            dGV.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            foreach (DataGridViewColumn column in dGV.Columns)
            {
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            //column defines
            dGV.Columns[0].Name = "B";
            dGV.Columns[1].Name = "I";
            dGV.Columns[2].Name = "N";
            dGV.Columns[3].Name = "G";
            dGV.Columns[4].Name = "O";

            ////row defines
            //dataGridView.RowCount = 5;

            dGV.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dGV.MultiSelect = false;
            dGV.Dock = DockStyle.Fill;

            // Event Registration
            dGV.CellClick += new DataGridViewCellEventHandler(dGV_CellClick);
        }

        private void PopulateDataGridView()
        {
            string[] memes = { "Sulfuric acid is dead", "OpenTTD moment", "John disconnects something",
                                "New Foundry still not done", "X acid is dead", "Train kill",
                                ">5 refactor tags", ">15 refactor tags", "Incomprehensible screaming",
                                "Why isn't research ticking?", "Server crashes", "Power problems",
                                "Pipe says he'll do it later", "Making Pipe's stack a queue",
                                "John forgets what he was doing", "Pipe takes forever joining",
                                "Solder is dead", "Pipe complains about HE shells", "Fox lags out",
                                "Pipe wages a useless war", "John forgets he's the host",
                                "Fox assisted auto's", "Fox tries to offload work",
                                "Fox complains about spaghetti", "Pipe mentions fluid capacity",
                                "True Nukes almost crashes", "Update confusion", "John/Pipe spends ages in circuits",
                                "It's just temporary", "Update changes recipes" };

            RemoveEmptyRows();

            // 2D array of 5x5 with an index randomizer that adds all the strings
            // overrule the for loop for freespace
            // for each index, random pick from list of options, ensure pick isn't in array
            string[,] cards = new string[5, 5];
            List<string> memeCopy = new List<string>();

            for (int i = 0; i < memes.Length; i++)
            {
                string s = memes[i];
                memeCopy.Add(s); // copies memes string[] to a memeCopy List<>
            }

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    int indexed = random.Next(0, memeCopy.Count);
                    string pick = memeCopy[indexed];
                    memeCopy.RemoveAt(indexed);

                    cards[i, j] = pick;
                }
            }

            for (int r = 0; r < cards.GetLength(0); r++)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dGV);

                for (int c = 0; c < cards.GetLength(1); c++)
                {
                    row.Cells[c].Value = cards[r, c];
                }
                dGV.Rows.Insert(r, row);
            }

            dGV.ClearSelection();

            foreach (DataGridViewRow row in dGV.Rows)
            {
                row.Height = 50;
            }

            memeCopy.Clear();
            RemoveEmptyRows();
        }

        private void dGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewCell cell = dGV.Rows[e.RowIndex].Cells[e.ColumnIndex];

                if (cell.Style.BackColor != cellMarked)
                    cell.Style.BackColor = cellMarked;
                else
                    cell.Style.BackColor = cellBackColor;

                dGV.ClearSelection();
                //Console.WriteLine("Cell updated!");
            }
            catch (Exception ex)
            {
                if (ex is ArgumentOutOfRangeException) {} // This should be fixed with code, not try-catch
                else
                {
                    Console.WriteLine("Contact Pipe with the error:");
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("...And with steps to recreate");
                }
            }
        }

        private void Button_Clear_Progress(object sender, MouseEventArgs e)
        {
            Clear_Progress(sender, e);
        }

        private void Button_New_Board(object sender, MouseEventArgs e)
        {
            New_Board(sender, e);
        }

        private void RemoveEmptyRows()
        {
            for (int i = 0; i < dGV.RowCount; i++)
            {
                if (dGV.Rows[i].Cells[0].Value is null)
                {
                    dGV.Rows.RemoveAt(i);
                }
            }
            dGV.ClearSelection();
        }
    }
}
