using System;
using System.Collections.Generic;
using System.Diagnostics; // Dùng để đo thời gian bằng Stopwatch và Process.Start
using System.Drawing;
using System.IO;   
using System.Windows.Forms;
using System.Linq;

// Dùng để làm việc với các file và thư mục

namespace RegistrationForm1
{


    public partial class FrmThongtin : Form //D:\SCADA\UploadFiles
    {
        private readonly string _appSaveDirectory = @"D:\SCADA\UploadFiles"; // Thư mục để lưu các tệp đã tải                                                                      //    private string _appSaveRootDirectory = "C:\\Data\\MyAppFiles"; // Ví dụ: Thư mục gốc để lưu trữ
        private System.Windows.Forms.WebBrowser webBrowserContent; // 👈 CHỈ GIỮ LẠI WEB BROWSER

        public FrmThongtin()
        {
            InitializeComponent(); // Khởi tạo các thành phần giao diện của Form

            // Thiết lập các thuộc tính ban đầu cho Form
            this.Text = "Trình Xem Tệp";
            this.Size = new Size(1000, 700);
            this.MinimumSize = new Size(800, 600); // Kích thước tối thiểu
            this.StartPosition = FormStartPosition.CenterScreen;

            //// Thiết lập thư mục lưu trữ ứng dụng và đảm bảo nó tồn tại
            //_appSaveDirectory = Path.Combine(Application.StartupPath, "UploadedFiles");
            //if (!Directory.Exists(_appSaveDirectory))
            //{
            //    Directory.CreateDirectory(_appSaveDirectory);
            //}

            // Cấu hình Panel Sidebar
            pnlSidebar.Dock = DockStyle.Left;
            pnlSidebar.Width = 280;
            pnlSidebar.BackColor = SystemColors.ControlLight;

            //// Cấu hình Nút tải tệp
            //btnUploadFile.Dock = DockStyle.Top;
            //btnUploadFile.Margin = new Padding(10);
            //btnUploadFile.Text = "➕ Quản lý tập tin...";
            //btnUploadFile.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            //btnUploadFile.Height = 40;

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
            // ✅ Đảm bảo các thuộc tính Font, Size, BackColor và Anchor được thiết lập đầy đủ
            btnDeleteFile.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            btnDeleteFile.Size = new Size(100, 30); // Kích thước nút
            btnDeleteFile.BackColor = Color.LightCoral; // Màu nổi bật cho nút xóa
            btnDeleteFile.Anchor = AnchorStyles.Top | AnchorStyles.Right; // Neo vào góc trên bên phải
            // Đặt dưới nút "Tải về"
            btnDeleteFile.Location = new Point(pnlMainContent.Width - btnDeleteFile.Width - 30, btnDownloadFile.Bottom + 5);
            btnDeleteFile.Enabled = false; // Mặc định vô hiệu hóa
        //   btnDeleteFile.Click += BtnDeleteFile_Click; // Gắn sự kiện Click

            // --- CẤU HÌNH WEB BROWSER (THAY THẾ RICH TEXT BOX) ---

            // ✅ Tối ưu hóa kích thước WebBrowser cho không gian tối đa
            // Vị trí y bắt đầu của content là 20px dưới control thấp nhất
            Point contentLocation = new Point(50, Math.Max(lblUploadTimestamp.Bottom, btnDeleteFile.Bottom) + 20);

            // Giảm lề ngang từ 60px (30 trái + 30 phải) xuống 30px (chỉ còn lề trái)
            // Lề phải sẽ do Anchor Right xử lý tốt hơn
            int contentWidth = pnlMainContent.Width - 30;

            // Giảm lề dưới từ 30px xuống 10px để tối đa hóa chiều cao
            int contentHeight = pnlMainContent.Height - contentLocation.Y - 10;

            // Khởi tạo và cấu hình WebBrowser
            webBrowserContent = new WebBrowser();
            pnlMainContent.Controls.Add(webBrowserContent);

            // Cấu hình vị trí và kích thước
            webBrowserContent.Location = contentLocation;
            // Dùng Anchor Top, Bottom, Left, Right để WebBrowser tự co giãn
            webBrowserContent.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            webBrowserContent.Width = contentWidth;
            webBrowserContent.Height = contentHeight;
            webBrowserContent.Visible = true; // Luôn hiển thị

            // Thiết lập nội dung ban đầu (dùng DocumentText vì không có RichTextBox)
            webBrowserContent.DocumentText = "<h3 style='font-family: Arial; padding: 10px;'>Vui lòng tải lên tệp hoặc chọn một tệp từ danh sách để xem nội dung.</h3>";

            // ------------------------------------------

            // Gắn sự kiện Click cho nút tải tệp
            btnUploadFile.Click += BtnUploadFile_Click;
            this.SizeChanged += Form1_SizeChanged; // Xử lý khi Form thay đổi kích thước
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
            // 1. Điều chỉnh vị trí của các nút
            btnOpenFileExternal.Location = new Point(pnlMainContent.Width - btnOpenFileExternal.Width - 30, 30);
            btnDownloadFile.Location = new Point(pnlMainContent.Width - btnDownloadFile.Width - 30, btnOpenFileExternal.Bottom + 5);
            btnDeleteFile.Location = new Point(pnlMainContent.Width - btnDeleteFile.Width - 30, btnDownloadFile.Bottom + 5);

            // 2. Điều chỉnh lại vị trí của các nhãn thông tin
            lblLoadDuration.Location = new Point(30, lblFileName.Bottom + 5);
            lblUploadTimestamp.Location = new Point(30, lblLoadDuration.Bottom + 5);

            // 3. ✅ Cần điều chỉnh lại vị trí và kích thước của webBrowserContent khi Form thay đổi kích thước

            // Tính toán lại vị trí Y bắt đầu dựa trên các control đã thay đổi kích thước/vị trí
            Point contentLocation = new Point(30, Math.Max(lblUploadTimestamp.Bottom, btnDeleteFile.Bottom) + 20);

            // ✅ Tối ưu hóa kích thước WebBrowser cho không gian tối đa (như đã làm trong Constructor)
            int contentWidth = pnlMainContent.Width + 30; // 30px lề trái
            // int contentHeight = pnlMainContent.Height - contentLocation.Y - 10; // 10px lề dưới
            int contentHeight = pnlMainContent.Height + 50;
            webBrowserContent.Location = contentLocation;
            webBrowserContent.Width = contentWidth;
            webBrowserContent.Height = contentHeight;
        }

        /// <summary>
        /// Tải nội dung TreeView, bao gồm các thư mục mặc định và tệp đã lưu từ đĩa.
      // </summary>
        private void LoadTreeViewContent()
        {
          // Tải các tệp đã lưu từ thư mục đĩa vào một node riêng
            LoadSavedFilesFromDisk();
        }
        private void LoadSavedFilesFromDisk()
        {
            // Đảm bảo thư mục tồn tại
            try
            {
                if (!Directory.Exists(_appSaveDirectory))
                {
                    Directory.CreateDirectory(_appSaveDirectory);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể tạo thư mục lưu trữ:\n" + ex.Message);
                return;
            }

            // Tìm node gốc
            TreeNode uploadedFilesNode = trvFiles.Nodes
                .Cast<TreeNode>()
                .FirstOrDefault(n => n.Text == "📁 Tệp đã tải lên" && (string)n.Tag == "Folder");

            if (uploadedFilesNode == null)
            {
                uploadedFilesNode = new TreeNode("📁 Tệp đã tải lên") { Tag = "Folder" };
                trvFiles.Nodes.Add(uploadedFilesNode);
            }

            uploadedFilesNode.Nodes.Clear();
            TreeNode firstFileNode = null;

            try
            {
                var dateDirectories = Directory.GetDirectories(_appSaveDirectory)
                    .OrderByDescending(dir => Directory.GetLastWriteTime(dir));

                foreach (string dateDir in dateDirectories)
                {
                    string folderName = Path.GetFileName(dateDir);
                    var dateNode = new TreeNode($"📅 {folderName}") { Tag = "Folder" };

                    uploadedFilesNode.Nodes.Add(dateNode);

                    var files = Directory.GetFiles(dateDir)
                        .OrderByDescending(f => File.GetLastWriteTime(f));

                    foreach (string filePath in files)
                    {
                        string fileName = Path.GetFileName(filePath);
                        DateTime time = File.GetLastWriteTime(filePath);

                        var entry = new FileEntry(fileName, filePath, time);
                        TreeNode fileNode = new TreeNode(entry.DisplayName) { Tag = entry };
                        dateNode.Nodes.Add(fileNode);

                        firstFileNode ??= fileNode;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách tệp:\n" + ex.Message);
            }

            uploadedFilesNode.Expand();
            trvFiles.SelectedNode = firstFileNode ?? uploadedFilesNode.Nodes.Cast<TreeNode>().FirstOrDefault();
        }

        /// <summary>
        /// Tải các tệp đã được lưu trữ vật lý trong _appSaveDirectory vào TreeView.
        /// </summary>
        //private void LoadSavedFilesFromDisk()
        //{
        //    // Tìm hoặc tạo node "📁 Tệp đã tải lên"
        //    TreeNode uploadedFilesNode = null;
        //    foreach (TreeNode node in trvFiles.Nodes)
        //    {
        //        if (node.Text == "📁 Tệp đã tải lên" && node.Tag?.ToString() == "Folder")
        //        {
        //            uploadedFilesNode = node;
        //            break;
        //        }
        //    }

        //    if (uploadedFilesNode == null)
        //    {
        //        uploadedFilesNode = new TreeNode("📁 Tệp đã tải lên");
        //        uploadedFilesNode.Tag = "Folder";
        //        trvFiles.Nodes.Add(uploadedFilesNode);
        //    }

        //    uploadedFilesNode.Nodes.Clear(); // Xóa các tệp cũ trong node này

        //    TreeNode firstFileNode = null;

        //    if (Directory.Exists(_appSaveDirectory))
        //    {
        //        // 1️⃣ Lấy danh sách tất cả thư mục con
        //        string[] dateDirectories = Directory.GetDirectories(_appSaveDirectory);

        //        // 2️⃣ Sắp xếp theo thời gian sửa đổi hoặc tạo mới nhất (descending)
        //        var sortedDateDirs = dateDirectories
        //            .OrderByDescending(dir => Directory.GetCreationTime(dir))
        //            .ToArray();

        //        // 3️⃣ Duyệt qua các thư mục đã sắp xếp
        //        foreach (string dateDirectoryPath in sortedDateDirs)
        //        {
        //            string folderName = Path.GetFileName(dateDirectoryPath);

        //            // Tạo node thư mục ngày tháng
        //            TreeNode dateFolderNode = new TreeNode($"📅 {folderName}");
        //            dateFolderNode.Tag = "Folder";
        //            uploadedFilesNode.Nodes.Add(dateFolderNode);

        //            // Lấy danh sách file trong thư mục này
        //            string[] filesInDateFolder = Directory.GetFiles(dateDirectoryPath);

        //            // Sắp xếp file theo thời gian tạo mới nhất trước
        //            var sortedFiles = filesInDateFolder
        //                .OrderByDescending(f => File.GetCreationTime(f))
        //                .ToArray();

        //            foreach (string savedFilePath in sortedFiles)
        //            {
        //                string fileName = Path.GetFileName(savedFilePath);
        //                DateTime creationTime = File.GetCreationTime(savedFilePath);

        //                FileEntry fileEntry = new FileEntry(fileName, savedFilePath, creationTime);
        //                TreeNode fileNode = new TreeNode(fileEntry.DisplayName);
        //                fileNode.Tag = fileEntry;

        //                dateFolderNode.Nodes.Add(fileNode);

        //                // Lưu node file đầu tiên
        //                firstFileNode ??= fileNode;
        //            }
        //        }
        //    }

        //    uploadedFilesNode.Expand();

        //    // Chọn node hiển thị ban đầu
        //    if (firstFileNode != null)
        //    {
        //        trvFiles.SelectedNode = firstFileNode;
        //    }
        //    else if (uploadedFilesNode.Nodes.Count > 0)
        //    {
        //        trvFiles.SelectedNode = uploadedFilesNode.Nodes[0];
        //    }
        //}



        /// <summary>
        /// Xử lý sự kiện khi nút 'Tải tệp lên...' được nhấn.
        /// Sao chép tệp vào thư mục lưu trữ và thêm vào TreeView.
        /// </summary>
        private void BtnUploadFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Thiết lập bộ lọc cho các loại tệp phổ biến
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

            // SỬA ĐỔI QUAN TRỌNG: Giải phóng tệp cũ bằng cách tải nội dung trống ĐỒNG BỘ
            // Điều này buộc WebBrowser giải phóng khóa tệp ngay lập tức.
            if (webBrowserContent.Url != null)
            {
                webBrowserContent.DocumentText = "<h3 style='font-family: Arial; padding: 10px;'>Đang tải...</h3>";
                // KHÔNG dùng webBrowserContent.Navigate("about:blank");
            }


            if (e.Node != null && e.Node.Tag is FileEntry fileEntry)
            {
                // Node được chọn là một tệp (Tag của nó là FileEntry)

                // KIỂM TRA: Đảm bảo hàm này KHÔNG mở FileStream mà không đóng
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
                // Đặt nội dung trống đồng bộ khi chọn thư mục
                webBrowserContent.DocumentText = "<h3 style='font-family: Arial; padding: 10px;'>Vui lòng chọn một tệp từ danh sách để xem nội dung.</h3>";
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
        /// Tải nội dung của tệp và hiển thị lên Form bằng WebBrowser.
        /// </summary>
        /// <param name="filePath">Đường dẫn đầy đủ của tệp.</param>
        private void LoadAndDisplayFileContent(string filePath)
        {
            lblFileName.Text = "Tên tệp: Đang tải...";
            lblLoadDuration.Text = "Thời gian tải: Đang tính...";
            lblUploadTimestamp.Text = "Thời gian tải lên: Đang tính..."; // Cập nhật trạng thái

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            string fileName = Path.GetFileName(filePath);
            string fileExtension = Path.GetExtension(filePath).ToLower();

            lblFileName.Text = "Tên tệp: " + fileName;

            // SỬA ĐỔI QUAN TRỌNG: Dùng DocumentText để giải phóng khóa tệp CŨ một cách đồng bộ
            webBrowserContent.DocumentText = "<h3 style='font-family: Arial; padding: 10px;'>Đang tải...</h3>";

            try
            {
                if (fileExtension == ".txt" || fileExtension == ".html")
                {
                    // Đối với file văn bản, đọc nội dung và gán vào DocumentText.
                    // File.ReadAllText TỰ ĐỘNG ĐÓNG LUỒNG, KHÔNG KHÓA FILE.
                    string content;
                    using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                    using (var reader = new StreamReader(stream))
                    {
                        content = reader.ReadToEnd();
                    }
                    webBrowserContent.DocumentText = $"<pre style='font-family: Consolas;'>{content}</pre>";
                }
                else
                {
                    // Đối với file nhị phân (PDF, DOCX, XLSX), PHẢI DÙNG Navigate.
                    // Cảnh báo: Lệnh này SẼ KHÓA FILE cho đến khi điều hướng lại.
                    webBrowserContent.Navigate(new Uri(filePath));
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

                // Hiển thị thông báo lỗi trong WebBrowser
                webBrowserContent.DocumentText = $"<h3 style='font-family: Arial; color: red; padding: 10px;'>Không thể tải nội dung tệp do lỗi:</h3><p style='font-family: Arial; padding: 10px;'>{ex.Message}</p>";
            }
        }

        private void btnDeleteFile_Click(object sender, EventArgs e)
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
                        // BƯỚC 1: Giải phóng tệp đang bị WebBrowser khóa (Quan trọng!)
                        // Kiểm tra xem WebBrowser có đang hiển thị tệp này không
                        // Sử dụng hàm kiểm tra đường dẫn an toàn hơn (chú ý: webBrowserContent.Url có thể null)
                        bool isFileLockedByBrowser = webBrowserContent.Url != null && webBrowserContent.Url.IsFile &&
                                                     webBrowserContent.Url.LocalPath.Equals(fileEntry.FullPath, StringComparison.OrdinalIgnoreCase);

                        if (isFileLockedByBrowser)
                        {
                            // CÁCH MẠNH MẼ NHẤT: Bắt buộc giải phóng khóa
                            webBrowserContent.DocumentText = ""; // 1. Tải nội dung trống đồng bộ

                            Application.DoEvents();             // 2. Xử lý các sự kiện đang chờ

                            System.Threading.Thread.Sleep(100); // 3. Dừng luồng để cho hệ điều hành giải phóng khóa file
                        }
                        

                        // BƯỚC 2: Xóa tệp vật lý
                        File.Delete(fileEntry.FullPath);

                        // BƯỚC 3: Xóa node khỏi TreeView và cập nhật UI (Giữ nguyên logic của bạn)
                        TreeNode parentNode = fileNode.Parent; // Lưu node cha trước khi xóa
                        fileNode.Remove(); // Xóa node khỏi TreeView
                        MessageBox.Show("Tệp đã được xóa thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Cập nhật lại UI sau khi xóa
                        lblFileName.Text = "Tên tệp:";
                        lblLoadDuration.Text = "Thời gian tải:";
                        lblUploadTimestamp.Text = "Thời gian tải lên:";
                        webBrowserContent.DocumentText = "<h3 style='font-family: Arial; padding: 10px;'>Tệp đã được xóa. Vui lòng chọn một tệp khác hoặc tải lên tệp mới.</h3>";
                        btnOpenFileExternal.Enabled = false;
                        btnDownloadFile.Enabled = false;
                        btnDeleteFile.Enabled = false;

                        // Cố gắng chọn một node khác nếu node cha tồn tại
                        if (parentNode != null)
                        {
                            trvFiles.SelectedNode = parentNode;
                            if (parentNode.Nodes.Count > 0)
                            {
                                trvFiles.SelectedNode = parentNode.Nodes[0];
                            }
                        }
                    }
                    catch (IOException ex) when (ex.Message.Contains("being used by another process"))
                    {
                        // Xử lý cụ thể lỗi tệp đang bị khóa
                        MessageBox.Show($"Lỗi: Tệp đang bị khóa. Vui lòng đảm bảo tệp không bị mở bởi ứng dụng khác và thử lại. Chi tiết: {ex.Message}", "Lỗi Xóa Tệp", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
    }
}