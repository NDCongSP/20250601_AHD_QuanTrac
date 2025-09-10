using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics; // Dùng để đo thời gian bằng Stopwatch và Process.Start
using System.IO;          // Dùng để làm việc với các file và thư mục

namespace RegistrationForm1
{
    //namespace RegistrationForm1
    //{
    //    public partial class FrmThongtin : Form

    public partial class FrmThongtin : Form
    {
        private string _appSaveDirectory; // Thư mục để lưu các tệp đã tải lên

        public FrmThongtin()
        {
            InitializeComponent(); // Khởi tạo các thành phần giao diện của Form

            // Thiết lập các thuộc tính ban đầu cho Form
            this.Text = "Trình Xem Tệp";
            this.Size = new Size(1000, 700);
            this.MinimumSize = new Size(800, 600); // Kích thước tối thiểu
            this.StartPosition = FormStartPosition.CenterScreen;

            // Thiết lập thư mục lưu trữ ứng dụng và đảm bảo nó tồn tại
            _appSaveDirectory = Path.Combine(Application.StartupPath, "UploadedFiles");
            if (!Directory.Exists(_appSaveDirectory))
            {
                Directory.CreateDirectory(_appSaveDirectory);
            }

            // Cấu hình Panel Sidebar
            pnlSidebar.Dock = DockStyle.Left;
            pnlSidebar.Width = 280;
            pnlSidebar.BackColor = SystemColors.ControlLight;

            // Cấu hình Nút tải tệp
            btnUploadFile.Dock = DockStyle.Top;
            btnUploadFile.Margin = new Padding(10);
            btnUploadFile.Text = "➕ Quản lý tập tin...";
            btnUploadFile.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnUploadFile.Height = 40;

            // Cấu hình TreeView hiển thị các thư mục và tệp
            trvFiles.Dock = DockStyle.Fill;
            trvFiles.BorderStyle = BorderStyle.None;
            trvFiles.Font = new Font("Segoe UI", 10);
            trvFiles.Margin = new Padding(10, 0, 10, 10);

            // Tải cấu trúc thư mục ban đầu và các tệp đã lưu từ đĩa
            LoadTreeViewContent();
            trvFiles.AfterSelect += TrvFiles_AfterSelect; // Gắn sự kiện khi chọn node trong TreeView

            // Cấu hình Panel Main Content
            pnlMainContent.Dock = DockStyle.Fill;
            pnlMainContent.BackColor = Color.White;

            // Cấu hình Nhãn tên file
            lblFileName.Text = "Tên tệp:";
            lblFileName.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            lblFileName.AutoSize = true;
            lblFileName.Location = new Point(30, 30);

            // Cấu hình Nhãn thời gian tải
            lblLoadDuration.Text = "Thời gian tải:";
            lblLoadDuration.Font = new Font("Segoe UI", 10, FontStyle.Italic);
            lblLoadDuration.AutoSize = true;
            lblLoadDuration.Location = new Point(30, lblFileName.Bottom + 5);

            // Cấu hình Nhãn thời gian tải lên
            lblUploadTimestamp.Text = "Thời gian tải lên:";
            lblUploadTimestamp.Font = new Font("Segoe UI", 10, FontStyle.Italic);
            lblUploadTimestamp.AutoSize = true;
            lblUploadTimestamp.Location = new Point(30, lblLoadDuration.Bottom + 5);


            // Cấu hình nút "Mở Tệp" (Open File Externally)
            btnOpenFileExternal.Text = "Mở Tệp";
            btnOpenFileExternal.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            btnOpenFileExternal.Size = new Size(100, 30); // Kích thước nút
            btnOpenFileExternal.Anchor = AnchorStyles.Top | AnchorStyles.Right; // Neo vào góc trên bên phải
            // Vị trí của nút sẽ được điều chỉnh động dựa trên kích thước của pnlMainContent
            btnOpenFileExternal.Location = new Point(pnlMainContent.Width - btnOpenFileExternal.Width - 30, 30);
            btnOpenFileExternal.Enabled = false; // Mặc định vô hiệu hóa nút
            btnOpenFileExternal.Click += BtnOpenFileExternal_Click; // Gắn sự kiện Click cho nút

            // Cấu hình nút "Tải về" (Download File)
            btnDownloadFile.Text = "Tải về";
            btnDownloadFile.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            btnDownloadFile.Size = new Size(100, 30); // Kích thước nút
            btnDownloadFile.Anchor = AnchorStyles.Top | AnchorStyles.Right; // Neo vào góc trên bên phải
            // Đặt dưới nút "Mở Tệp"
            btnDownloadFile.Location = new Point(pnlMainContent.Width - btnDownloadFile.Width - 30, btnOpenFileExternal.Bottom + 5);
            btnDownloadFile.Enabled = false; // Mặc định vô hiệu hóa nút
            btnDownloadFile.Click += BtnDownloadFile_Click; // Gắn sự kiện Click cho nút

            // Cấu hình nút "Xóa tệp" mới
            btnDeleteFile.Text = "Xóa tệp";
            btnDeleteFile.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            btnDeleteFile.Size = new Size(100, 30); // Kích thước nút
            btnDeleteFile.BackColor = Color.LightCoral; // Màu nổi bật cho nút xóa
            btnDeleteFile.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            // Đặt dưới nút "Tải về"
            btnDeleteFile.Location = new Point(pnlMainContent.Width - btnDeleteFile.Width - 30, btnDownloadFile.Bottom + 5);
            btnDeleteFile.Enabled = false; // Mặc định vô hiệu hóa
            btnDeleteFile.Click += BtnDeleteFile_Click; // Gắn sự kiện Click

            // Cấu hình RichTextBox hiển thị nội dung file
            // Điều chỉnh vị trí bắt đầu của RichTextBox để tránh chồng chéo với các nút và nhãn mới
            rtbFileContent.Location = new Point(30, Math.Max(lblUploadTimestamp.Bottom, btnDeleteFile.Bottom) + 20);
            rtbFileContent.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            rtbFileContent.Width = pnlMainContent.Width - 60;
            rtbFileContent.Height = pnlMainContent.Height - rtbFileContent.Top - 30;
            rtbFileContent.ReadOnly = true;
            rtbFileContent.Font = new Font("Consolas", 10);
            rtbFileContent.WordWrap = true;
            rtbFileContent.Text = "Vui lòng tải lên tệp hoặc chọn một tệp từ danh sách để xem nội dung.";

            // Gắn sự kiện Click cho nút tải tệp
            btnUploadFile.Click += BtnUploadFile_Click;
            this.SizeChanged += Form1_SizeChanged; // Xử lý khi Form thay đổi kích thước để điều chỉnh vị trí nút "Mở Tệp" và "Tải về"
        }
        // Lớp tùy chỉnh để lưu thông tin chi tiết về một tệp
        public class FileEntry
        {
            public string DisplayName { get; set; } // Tên hiển thị trong TreeView
            public string FullPath { get; set; }    // Đường dẫn đầy đủ của tệp
            public DateTime UploadTimestamp { get; set; } // Thời gian tệp được tải lên

            public FileEntry(string displayName, string fullPath, DateTime uploadTimestamp)
            {
                DisplayName = displayName;
                FullPath = fullPath;
                UploadTimestamp = uploadTimestamp;
            }

            public override string ToString()
            {
                return DisplayName; // Điều này sẽ được hiển thị trong TreeView node
            }
        }

        // Điều chỉnh vị trí của các nút "Mở Tệp", "Tải về" và "Xóa tệp" khi Form thay đổi kích thước
        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            btnOpenFileExternal.Location = new Point(pnlMainContent.Width - btnOpenFileExternal.Width - 30, 30);
            btnDownloadFile.Location = new Point(pnlMainContent.Width - btnDownloadFile.Width - 30, btnOpenFileExternal.Bottom + 5);
            btnDeleteFile.Location = new Point(pnlMainContent.Width - btnDeleteFile.Width - 30, btnDownloadFile.Bottom + 5);

            // Điều chỉnh lại vị trí của các nhãn thông tin
            lblLoadDuration.Location = new Point(30, lblFileName.Bottom + 5);
            lblUploadTimestamp.Location = new Point(30, lblLoadDuration.Bottom + 5);

            // Cần điều chỉnh lại vị trí của rtbFileContent khi Form thay đổi kích thước
            rtbFileContent.Location = new Point(30, Math.Max(lblUploadTimestamp.Bottom, btnDeleteFile.Bottom) + 20);
            rtbFileContent.Width = pnlMainContent.Width - 60;
            rtbFileContent.Height = pnlMainContent.Height - rtbFileContent.Top - 30;
        }

        /// <summary>
        /// Tải nội dung TreeView, bao gồm các thư mục mặc định và tệp đã lưu từ đĩa.
        /// </summary>
        private void LoadTreeViewContent()
        {
            trvFiles.Nodes.Clear(); // Xóa tất cả các node hiện có

            // Thêm các thư mục mặc định ban đầu
            TreeNode myFoldersNode = new TreeNode("📁 Thư mục của tôi");
            myFoldersNode.Tag = "Folder";

            TreeNode documentsNode = new TreeNode("📄 Tài liệu");
            documentsNode.Tag = "Folder";
            myFoldersNode.Nodes.Add(documentsNode);

            TreeNode downloadsNode = new TreeNode("⬇️ Tải về");
            downloadsNode.Tag = "Folder";
            myFoldersNode.Nodes.Add(downloadsNode);

            trvFiles.Nodes.Add(myFoldersNode);
            myFoldersNode.ExpandAll();

            // Tải các tệp đã lưu từ thư mục đĩa vào một node riêng
            LoadSavedFilesFromDisk();
        }

        /// <summary>
        /// Tải các tệp đã được lưu trữ vật lý trong _appSaveDirectory vào TreeView.
        /// </summary>
        private void LoadSavedFilesFromDisk()
        {
            // Tìm hoặc tạo node "Tệp đã tải lên"
            TreeNode uploadedFilesNode = null;
            foreach (TreeNode node in trvFiles.Nodes)
            {
                if (node.Text == "📁 Tệp đã tải lên" && node.Tag?.ToString() == "Folder")
                {
                    uploadedFilesNode = node;
                    break;
                }
            }

            if (uploadedFilesNode == null)
            {
                uploadedFilesNode = new TreeNode("📁 Tệp đã tải lên");
                uploadedFilesNode.Tag = "Folder";
                trvFiles.Nodes.Add(uploadedFilesNode);
            }

            uploadedFilesNode.Nodes.Clear(); // Xóa các tệp cũ trong node này trước khi tải lại

            if (Directory.Exists(_appSaveDirectory))
            {
                string[] files = Directory.GetFiles(_appSaveDirectory);
                foreach (string savedFilePath in files)
                {
                    string fileName = Path.GetFileName(savedFilePath);
                    // Lấy thời gian tạo tệp làm thời gian tải lên
                    DateTime creationTime = File.GetCreationTime(savedFilePath);
                    // Tạo FileEntry với đường dẫn đã lưu trữ và thời gian tạo
                    FileEntry fileEntry = new FileEntry(fileName, savedFilePath, creationTime);
                    TreeNode fileNode = new TreeNode(fileEntry.DisplayName);
                    fileNode.Tag = fileEntry;
                    uploadedFilesNode.Nodes.Add(fileNode);
                }
            }
            uploadedFilesNode.Expand(); // Mở rộng thư mục để hiển thị các tệp
            if (uploadedFilesNode.Nodes.Count > 0)
            {
                // Chọn tệp đầu tiên trong thư mục "Tệp đã tải lên" nếu có
                trvFiles.SelectedNode = uploadedFilesNode.Nodes[0];
            }
        }


        /// <summary>
        /// Xử lý sự kiện khi nút 'Tải tệp lên...' được nhấn.
        /// Sao chép tệp vào thư mục lưu trữ và thêm vào TreeView.
        /// </summary>
        private void BtnUploadFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Thiết lập bộ lọc cho các loại tệp phổ biến, ĐÃ THÊM .xlsx
            openFileDialog.Filter = "Tệp văn bản (*.txt)|*.txt|Tài liệu Word (*.docx)|*.docx|Tệp Excel (*.xlsx)|*.xlsx|Tệp PDF (*.pdf)|*.pdf|Tất cả tệp (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;
            openFileDialog.Multiselect = true; // Cho phép chọn nhiều tệp cùng lúc

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Xác định node thư mục đích trong TreeView
                TreeNode targetTreeNode = trvFiles.SelectedNode;

                // Nếu một node tệp tin được chọn, hãy sử dụng thư mục cha của nó
                if (targetTreeNode != null && targetTreeNode.Tag is FileEntry)
                {
                    targetTreeNode = targetTreeNode.Parent;
                }

                // Nếu không có thư mục hợp lệ được chọn, hoặc không có gì được chọn,
                // thì mặc định là node "Tệp đã tải lên"
                if (targetTreeNode == null || targetTreeNode.Tag?.ToString() != "Folder")
                {
                    // Thử tìm node "Tệp đã tải lên"
                    foreach (TreeNode node in trvFiles.Nodes)
                    {
                        if (node.Text == "📁 Tệp đã tải lên" && node.Tag?.ToString() == "Folder")
                        {
                            targetTreeNode = node;
                            break;
                        }
                    }
                    // Nếu "Tệp đã tải lên" node không tồn tại, tạo nó
                    if (targetTreeNode == null)
                    {
                        targetTreeNode = new TreeNode("📁 Tệp đã tải lên");
                        targetTreeNode.Tag = "Folder";
                        trvFiles.Nodes.Add(targetTreeNode);
                    }
                }

                foreach (string originalFilePath in openFileDialog.FileNames)
                {
                    // Sửa lỗi ở đây: chỉ cần gọi Path.GetFileName
                    string fileName = Path.GetFileName(originalFilePath);
                    string destinationPath = Path.Combine(_appSaveDirectory, fileName);

                    // Xử lý trường hợp trùng tên tệp trong thư mục lưu trữ
                    string uniqueFileName = fileName;
                    int count = 1;
                    while (File.Exists(destinationPath))
                    {
                        string nameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
                        string extension = Path.GetExtension(fileName);
                        uniqueFileName = $"{nameWithoutExtension} ({count}){extension}";
                        destinationPath = Path.Combine(_appSaveDirectory, uniqueFileName);
                        count++;
                    }

                    try
                    {
                        File.Copy(originalFilePath, destinationPath, false); // Sao chép tệp

                        // Tạo FileEntry mới với tên, đường dẫn và thời gian tải lên hiện tại
                        FileEntry newFileEntry = new FileEntry(uniqueFileName, destinationPath, DateTime.Now);
                        TreeNode fileNode = new TreeNode(newFileEntry.DisplayName);
                        fileNode.Tag = newFileEntry; // Lưu đối tượng FileEntry vào Tag của node
                        targetTreeNode.Nodes.Add(fileNode); // Thêm node tệp tin vào thư mục đã chọn/mặc định
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi khi lưu tệp {fileName}: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                targetTreeNode.Expand(); // Mở rộng thư mục để hiển thị các tệp mới

                // Chọn tệp cuối cùng vừa được thêm vào để hiển thị ngay lập tức
                if (targetTreeNode.Nodes.Count > 0)
                {
                    trvFiles.SelectedNode = targetTreeNode.Nodes[targetTreeNode.Nodes.Count - 1];
                }
            }
        }

        /// <summary>
        /// Xử lý sự kiện khi một node (thư mục hoặc tệp) được chọn trong TreeView.
        /// </summary>
        private void TrvFiles_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // Vô hiệu hóa cả ba nút "Mở Tệp", "Tải về" và "Xóa tệp" mặc định
            btnOpenFileExternal.Enabled = false;
            btnDownloadFile.Enabled = false;
            btnDeleteFile.Enabled = false;

            if (e.Node != null && e.Node.Tag is FileEntry fileEntry)
            {
                // Node được chọn là một tệp (Tag của nó là FileEntry)
                LoadAndDisplayFileContent(fileEntry.FullPath);
                lblUploadTimestamp.Text = "Thời gian tải lên: " + fileEntry.UploadTimestamp.ToString("dd/MM/yyyy HH:mm:ss");

                string fileExtension = Path.GetExtension(fileEntry.FullPath).ToLower();
                // Kích hoạt nút "Mở Tệp" nếu là file DOCX, XLSX hoặc PDF
                if (fileExtension == ".docx" || fileExtension == ".xlsx" || fileExtension == ".pdf")
                {
                    btnOpenFileExternal.Enabled = true;
                }
                btnOpenFileExternal.Tag = fileEntry.FullPath; // Luôn lưu đường dẫn file vào Tag của nút "Mở Tệp"

                // Kích hoạt nút "Tải về" và "Xóa tệp" cho MỌI tệp đã chọn
                btnDownloadFile.Enabled = true;
                btnDownloadFile.Tag = fileEntry.FullPath; // Lưu đường dẫn file vào Tag của nút "Tải về"

                btnDeleteFile.Enabled = true;
                btnDeleteFile.Tag = e.Node; // Lưu cả node để dễ dàng xóa khỏi TreeView sau này
            }
            else
            {
                // Node được chọn là một thư mục hoặc không có gì được chọn
                lblFileName.Text = "Tên tệp:";
                lblLoadDuration.Text = "Thời gian tải:";
                lblUploadTimestamp.Text = "Thời gian tải lên:";
                rtbFileContent.Text = "Vui lòng chọn một tệp từ danh sách để xem nội dung.";
            }
        }

        /// <summary>
        /// Xử lý sự kiện khi nút "Mở Tệp" được nhấn.
        /// Mở tệp đã chọn bằng ứng dụng mặc định của hệ thống.
        /// </summary>
        private void BtnOpenFileExternal_Click(object sender, EventArgs e)
        {
            if (btnOpenFileExternal.Tag is string filePath && File.Exists(filePath))
            {
                try
                {
                    Process.Start(filePath); // Mở tệp bằng ứng dụng mặc định
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Không thể mở tệp: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Không có tệp hợp lệ để mở.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Xử lý sự kiện khi nút "Tải về" được nhấn.
        /// Cho phép người dùng lưu tệp đã chọn vào máy tính.
        /// </summary>
        private void BtnDownloadFile_Click(object sender, EventArgs e)
        {
            if (btnDownloadFile.Tag is string sourceFilePath && File.Exists(sourceFilePath))
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.FileName = Path.GetFileName(sourceFilePath); // Tên file mặc định là tên của tệp nguồn
                saveFileDialog.Filter = "Tất cả tệp (*.*)|*.*"; // Cho phép lưu với bất kỳ định dạng nào

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string destinationPath = saveFileDialog.FileName;
                    try
                    {
                        File.Copy(sourceFilePath, destinationPath, true); // Sao chép tệp, cho phép ghi đè
                        MessageBox.Show($"Đã tải tệp về thành công: {destinationPath}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi khi tải tệp về: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Không có tệp hợp lệ để tải về.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Xử lý sự kiện khi nút "Xóa tệp" được nhấn.
        /// Xóa tệp đã chọn khỏi hệ thống và khỏi TreeView.
        /// </summary>
        private void BtnDeleteFile_Click(object sender, EventArgs e)
        {
            if (btnDeleteFile.Tag is TreeNode fileNode && fileNode.Tag is FileEntry fileEntry)
            {
                DialogResult dialogResult = MessageBox.Show(
                    $"Bạn có chắc chắn muốn xóa tệp này?\n\nTên tệp: {fileEntry.DisplayName}",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (dialogResult == DialogResult.Yes)
                {
                    try
                    {
                        File.Delete(fileEntry.FullPath);
                        fileNode.Remove(); // Xóa node khỏi TreeView
                        MessageBox.Show("Tệp đã được xóa thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Cập nhật lại UI sau khi xóa
                        lblFileName.Text = "Tên tệp:";
                        lblLoadDuration.Text = "Thời gian tải:";
                        lblUploadTimestamp.Text = "Thời gian tải lên:";
                        rtbFileContent.Text = "Tệp đã được xóa. Vui lòng chọn một tệp khác hoặc tải lên tệp mới.";
                        btnOpenFileExternal.Enabled = false;
                        btnDownloadFile.Enabled = false;
                        btnDeleteFile.Enabled = false;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi khi xóa tệp: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một tệp hợp lệ để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Tải nội dung của tệp và hiển thị lên Form.
        /// </summary>
        /// <param name="filePath">Đường dẫn đầy đủ của tệp.</param>
        private void LoadAndDisplayFileContent(string filePath)
        {
            lblFileName.Text = "Tên tệp: Đang tải...";
            lblLoadDuration.Text = "Thời gian tải: Đang tính...";
            lblUploadTimestamp.Text = "Thời gian tải lên: Đang tính..."; // Cập nhật trạng thái
            rtbFileContent.Text = "Đang tải nội dung tệp...";

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            string fileName = Path.GetFileName(filePath);
            string fileExtension = Path.GetExtension(filePath).ToLower();

            lblFileName.Text = "Tên tệp: " + fileName;

            try
            {
                // Giả lập một độ trễ nhỏ để dễ quan sát thời gian tải
                System.Threading.Thread.Sleep(300);

                if (fileExtension == ".txt")
                {
                    string fileContent = File.ReadAllText(filePath);
                    rtbFileContent.Text = fileContent; // Hiển thị nội dung file .txt
                }
                else if (fileExtension == ".docx" || fileExtension == ".xlsx" || fileExtension == ".pdf")
                {
                    rtbFileContent.Text = $"Không thể xem trước nội dung tệp {fileExtension} này trong ứng dụng đơn giản.\n\n" +
                                            "Vui lòng nhấn nút 'Mở Tệp' để xem bằng ứng dụng mặc định của hệ thống hoặc 'Tải về' để lưu vào máy tính.";
                }
                else
                {
                    rtbFileContent.Text = "Loại tệp này không được hỗ trợ để xem trước nội dung trực tiếp.";
                }

                stopwatch.Stop();
                TimeSpan ts = stopwatch.Elapsed;
                string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                    ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                lblLoadDuration.Text = "Thời gian tải: " + elapsedTime;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi đọc tệp: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblFileName.Text = "Tên tệp: Lỗi!";
                lblLoadDuration.Text = "Thời gian tải: Không thể tính!";
                lblUploadTimestamp.Text = "Thời gian tải lên: Lỗi!";
                rtbFileContent.Text = "Không thể tải nội dung tệp do lỗi.";
            }
        }


    }
}
