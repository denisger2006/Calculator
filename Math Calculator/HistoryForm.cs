namespace Math_Calculator
{
    public partial class HistoryForm : Form
    {
        private List<(string, string)> History;

        public HistoryForm()
        {
            InitializeComponent();
        }
        public HistoryForm(List<(string, string)> history) : this()
        {
            History = history;

            CalcForm.historyForm = this;
            History = history ?? [];
            UpdateView();
        }
        public void Clear() => listView1.Clear();
        //Перерисовывает весь список истории в listView
        public void UpdateView()
        {
            listView1.BeginUpdate();
            try
            {
                listView1.Items.Clear();

                foreach (var elem in History)
                {
                    AddRow(elem.Item1, elem.Item2);
                }
            }
            finally
            {
                listView1.EndUpdate();
            }

            AdjustColumnWidths();
        }

        //Автоматически рассчитывает ширину столбцов по заголовку и содержимому
        private void AdjustColumnWidths()
        {
            foreach (ColumnHeader column in listView1.Columns)
            {
                int headerWidth = TextRenderer.MeasureText(column.Text, listView1.Font).Width + 16;

                column.Width = -1;
                int contentWidth = column.Width;

                column.Width = Math.Max(contentWidth, headerWidth);
            }
        }

        //Добавляет отдельную строку с выражением и результатом в ListView
        public void AddRow(string expr, string result)
        {
            var item = new ListViewItem(expr);
            item.SubItems.Add(result);
            listView1.Items.Add(item);
        }

        //Обрабатывает горячие клавиши в ListView (сочетание Ctrl+C)
        private void listView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                CopySelectedItems();
            }
        }

        //Форматирует и копирует выделенные строки истории в буфер обмена
        private void CopySelectedItems()
        {
            if (listView1.SelectedItems.Count == 0) return;

            var sb = new System.Text.StringBuilder();

            foreach (ListViewItem item in listView1.SelectedItems)
            {
                string expr = item.Text;
                string result = item.SubItems.Count > 1 ? item.SubItems[1].Text : "";

                sb.AppendLine($"{expr} = {result}");
            }

            Clipboard.SetText(sb.ToString().TrimEnd());
        }

        //Блокирует ручной ресайз столбцов пользователем
        private void listView1_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.Cancel = true;
            e.NewWidth = listView1.Columns[e.ColumnIndex].Width;
        }
    }
}