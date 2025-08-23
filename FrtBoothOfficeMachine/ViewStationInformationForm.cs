using FrtAfcApiClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FrtBoothOfficeMachine
{
    public partial class ViewStationInformationForm : Form
    {
        private List<StationDisplayInfo> _stationData;

        /// <summary>
        /// Sets a WinForms control to use double buffering if not connected via RDP.
        /// </summary>
        /// <param name="c">The control to enable double buffering on.</param>
        public static void SetDoubleBuffered(Control c)
        {
            // Don't use double buffering over RDP
            if (SystemInformation.TerminalServerSession)
                return;

            PropertyInfo aProp =
                  typeof(Control).GetProperty(
                        "DoubleBuffered",
                        BindingFlags.NonPublic |
                        BindingFlags.Instance);

            aProp.SetValue(c, true, null);
        }

        public ViewStationInformationForm()
        {
            InitializeComponent();

            // Use double buffering for smoother rendering
            SetDoubleBuffered(StationsDataGridView);

            _stationData = new List<StationDisplayInfo>();

            // Configure DataGridView appearance
            ConfigureDataGridView();
        }

        private void ConfigureDataGridView()
        {
            // Set up sorting
            StationsDataGridView.AutoGenerateColumns = false;
            StationsDataGridView.AllowUserToOrderColumns = true;

            // Style the DataGridView
            StationsDataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(120, 120, 120);
            StationsDataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            StationsDataGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            StationsDataGridView.ColumnHeadersHeight = 30;

            // Alternating row colors for better readability
            StationsDataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
            StationsDataGridView.DefaultCellStyle.BackColor = Color.White;
            StationsDataGridView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(51, 153, 255);
            StationsDataGridView.DefaultCellStyle.SelectionForeColor = Color.White;

            // Row height
            StationsDataGridView.RowTemplate.Height = 25;

            // Grid lines
            StationsDataGridView.GridColor = Color.FromArgb(200, 200, 200);
            StationsDataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;

            // Configure columns for sorting
            foreach (DataGridViewColumn column in StationsDataGridView.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.Automatic;
            }
        }

        private async void ViewStationInformationForm_Load(object sender, EventArgs e)
        {
            await LoadStationDataAsync();
        }

        private async Task LoadStationDataAsync()
        {
            try
            {
                // Check if we have an authenticated API client
                if (GlobalCredentials.ApiClient == null)
                {
                    StatusLabel.Text = "错误：API客户端未初始化";
                    StatusLabel.ForeColor = Color.Red;
                    MessageBox.Show("API客户端未初始化。请先登录系统。", "错误",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Show loading status
                StatusLabel.Text = "正在加载站点信息...";
                StatusLabel.ForeColor = Color.Blue;
                RefreshButton.Enabled = false;
                this.Cursor = Cursors.WaitCursor;

                // Clear existing data
                _stationData.Clear();
                StationsDataGridView.DataSource = null;

                // Load stations from API
                var stations = await GlobalCredentials.ApiClient.GetAllStationsAsync();

                // Convert to display format
                _stationData = stations.Select(s => new StationDisplayInfo
                {
                    StationCode = s.StationCode,
                    ChineseName = s.ChineseName,
                    EnglishName = s.EnglishName,
                    ZoneId = s.ZoneId,
                    IsActive = s.IsActive,
                    StatusText = s.IsActive ? "运营中" : "停用"
                }).ToList();

                // Bind to DataGridView
                StationsDataGridView.DataSource = _stationData;

                // Update status and count
                UpdateStationCount();
                StatusLabel.Text = "站点信息加载完成";
                StatusLabel.ForeColor = Color.Green;

            }
            catch (FrtAfcApiException ex) when (ex.Message.Contains("Insufficient permissions"))
            {
                StatusLabel.Text = "权限不足";
                StatusLabel.ForeColor = Color.Red;
                MessageBox.Show("权限不足：您没有查看站点信息的权限。\n\n请联系系统管理员。",
                              "权限错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (FrtAfcApiException ex)
            {
                StatusLabel.Text = "服务器连接失败";
                StatusLabel.ForeColor = Color.Red;
                string errorMessage = $"无法从服务器获取站点信息：\n\n{ex.Message}\n\n请检查网络连接和服务器状态。";
                MessageBox.Show(errorMessage, "服务器错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                StatusLabel.Text = "加载失败";
                StatusLabel.ForeColor = Color.Red;
                string errorMessage = $"加载站点信息时发生错误：\n\n{ex.Message}\n\n请联系技术支持。";
                MessageBox.Show(errorMessage, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                RefreshButton.Enabled = true;
                this.Cursor = Cursors.Default;
            }

            // Configure sorting AFTER data binding
            foreach (DataGridViewColumn column in StationsDataGridView.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.Automatic;
            }
        }

        private void UpdateStationCount()
        {
            if (_stationData != null)
            {
                int totalStations = _stationData.Count;
                int activeStations = _stationData.Count(s => s.IsActive);
                int inactiveStations = totalStations - activeStations;

                StationCountLabel.Text = $"共 {totalStations} 个站点（运营中：{activeStations}，停用：{inactiveStations}）";
            }
            else
            {
                StationCountLabel.Text = "共 0 个站点";
            }
        }

        private async void RefreshButton_Click(object sender, EventArgs e)
        {
            await LoadStationDataAsync();
        }

        private void ViewStationInformationForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    this.Close();
                    break;
                case Keys.F5:
                    RefreshButton.PerformClick();
                    break;
                case Keys.R when e.Alt:
                    RefreshButton.PerformClick();
                    break;
                case Keys.C when e.Alt:
                    CloseButton.PerformClick();
                    break;
            }
        }

        /// <summary>
        /// Display class for station information in the DataGridView
        /// </summary>
        private class StationDisplayInfo
        {
            public string StationCode { get; set; }
            public string ChineseName { get; set; }
            public string EnglishName { get; set; }
            public int ZoneId { get; set; }
            public bool IsActive { get; set; }
            public string StatusText { get; set; } // "运营中" or "停用"
        }
    }
}