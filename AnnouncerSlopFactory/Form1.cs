using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows.Forms;

namespace AnnouncerSlopFactory
{
    public partial class Form1 : Form
    {
        public class DialogueEntry
        {
            public string Category = "";
            public string DisplayName = "";
            public int Variant = 1;
            public string? AudioPath;
            public string? CustomIconPath;
            public string? DialogueText;

            public string DialogueId(int announcerId) => $"{Category}_{announcerId}_{Variant}";
            public string VoiceFile(int variant) => $"{Category}_{variant}.wav";
        }

        private class AnnouncerMeta
        {
            [JsonPropertyName("id")] public int Id { get; set; }
            [JsonPropertyName("name")] public string? Name { get; set; }
            [JsonPropertyName("imgStr")] public string? ImgStr { get; set; }

            [JsonPropertyName("neglectWaiting")]
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
            public int? NeglectWaiting { get; set; }

            [JsonPropertyName("neglectRandomize")]
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
            public int? NeglectRandomize { get; set; }

            [JsonPropertyName("baseId")]
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
            public int? BaseId { get; set; }

            [JsonPropertyName("walpurgisType")]
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
            public string? WalpurgisType { get; set; }

            [JsonPropertyName("volume")]
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
            public int? Volume { get; set; }
        }

        private static readonly (string Key, string Display)[] Categories =
        {
            ("announcer_equip",                    "Equipping Announcer"),
            ("announcer_cheer",                    "Rally"),
            ("announcer_specialcheer",             "Situational Rally"),
            ("announcer_battle_win",               "Victory Cry"),
            ("announcer_battle_defeat",            "Defeat Wail"),
            ("announcer_ally_adv",                 "Ally Advantage"),
            ("announcer_ally_advex",               "Ally Turnabout"),
            ("announcer_enemy_adv",                "Enemy Advantage"),
            ("announcer_danger",                   "Danger"),
            ("announcer_enemy_specialgimmick",     "Enemy Gimmick Warning"),
            ("announcer_enemy_specialskill",       "Powerful Enemy Skill Warning"),
            ("announcer_round_givebigdmg",         "Dealing Devastating Damage"),
            ("announcer_round_givebigdmg_after",   "Starting Turn after Dealing Devastating Damage"),
            ("announcer_round_takebigdmg",         "Taking Devastating Damage"),
            ("announcer_round_takebigdmg_after",   "Starting Turn after Taking Devastating Damage"),
            ("announcer_ally_specialbuff",         "Buffed Allies"),
            ("announcer_ally_specialdebuff",       "Debuffed Allies"),
            ("announcer_enemy_specialbuff",        "Buffed Enemies"),
            ("announcer_enemy_specialdebuff",      "Debuffed Enemies"),
            ("announcer_killenemy",                "Enemy Kill"),
            ("announcer_multikillenemy",           "Ally Multikill"),
            ("announcer_ally_break",               "Ally Staggered"),
            ("announcer_ally_dead",                "Ally Death"),
            ("announcer_multikillally",            "Multiple Ally Deaths"),
            ("announcer_enemy_break",              "Enemy Staggered"),
            ("announcer_enemy_destroy",            "Breaking Enemy Part"),
            ("announcer_enemy_destroy_after",      "Starting Turn after Breaking Enemy Part"),
            ("announcer_advatk_physical",          "Advantageous Attack Type"),
            ("announcer_advatk_attr",              "Advantageous Affinity Type"),
            ("announcer_disadvatk_physical",       "Disadvantageous Attack Type"),
            ("announcer_disadvatk_attr",           "Disadvantageous Affinity Type"),
            ("announcer_neglect",                  "No Input for an Extended Period of Time"),
        };

        private readonly List<DialogueEntry> _entries = new List<DialogueEntry>();
        private string? _iconPath;
        private string? _framePath;
        private SoundPlayer? _currentSoundPlayer;

        public Form1()
        {
            InitializeComponent();

            foreach (var (key, display) in Categories)
            {
                var item = new ToolStripMenuItem(display) { Tag = key };
                item.Click += CategoryMenu_Click;
                contextMenuCategories.Items.Add(item);
            }

            numAnnouncerId.ValueChanged += (s, e) => lblStatus.Text = $"ID set to {numAnnouncerId.Value}";
            trackBarVolume.ValueChanged += (s, e) => lblVolumeValue.Text = trackBarVolume.Value.ToString();
            chkNeglectWaiting.CheckedChanged += (s, e) => numNeglectWaiting.Enabled = chkNeglectWaiting.Checked;
            chkNeglectRandomize.CheckedChanged += (s, e) => numNeglectRandomize.Enabled = chkNeglectRandomize.Checked;

            cmbWalpurgis.TextChanged += (s, e) => UpdateLivePreview(null);
            cmbWalpurgis.SelectedIndexChanged += (s, e) => UpdateLivePreview(null);

            _toolTip.SetToolTip(chkNeglectWaiting, "Seconds of idling before announcer_neglect can play. Default: 180.");
            _toolTip.SetToolTip(chkNeglectRandomize, "Chance (0-100%) that announcer_neglect plays. 100 = always. Default: 50.");
            _toolTip.SetToolTip(trackBarVolume, "Volume: 0 = silent, 100 = full volume.");

            // Clicking anywhere else on the form de-selects the grid
            this.MouseClick += (s, e) => { if (!dgvEntries.Bounds.Contains(PointToClient(Cursor.Position))) dgvEntries.ClearSelection(); };
            pnlLeft.MouseClick += (s, e) => dgvEntries.ClearSelection();
            pnlTop.MouseClick += (s, e) => dgvEntries.ClearSelection();
            pnlBottom.MouseClick += (s, e) => dgvEntries.ClearSelection();
            pnlRight.MouseClick += (s, e) => dgvEntries.ClearSelection();
            dgvEntries.MouseDown += DgvEntries_MouseDown;
            dgvEntries.Leave += (s, e) => BeginInvoke(new Action(() => dgvEntries.ClearSelection()));

            this.FormClosed += (s, e) => {
                _currentSoundPlayer?.Stop();
                _currentSoundPlayer?.Dispose();
                Application.Exit();
            };
        }

        private void DgvEntries_MouseDown(object? sender, MouseEventArgs e)
        {
            var hit = dgvEntries.HitTest(e.X, e.Y);
            if (hit.Type == DataGridViewHitTestType.None)
            {
                dgvEntries.ClearSelection();
            }
        }

        private void PlayAudio(string filePath)
        {
            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath)) return;

            _currentSoundPlayer?.Stop();
            _currentSoundPlayer?.Dispose();

            _currentSoundPlayer = new SoundPlayer(filePath);
            _currentSoundPlayer.Play();
        }

        private void BtnBrowseModFolder_Click(object? sender, EventArgs e)
        {
            using var fbd = new FolderBrowserDialog { Description = "Select the mod folder (containing custom_announcers/)" };
            if (fbd.ShowDialog() == DialogResult.OK) txtModFolder.Text = fbd.SelectedPath;
        }

        private void BtnSelectIcon_Click(object? sender, EventArgs e)
        {
            using var ofd = new OpenFileDialog { Filter = "PNG Image|*.png", Title = "Select announcer icon PNG" };
            if (ofd.ShowDialog() != DialogResult.OK) return;
            _iconPath = ofd.FileName;
            picIcon.Image = Image.FromFile(_iconPath);
            UpdateLivePreview(null);
        }

        private void BtnSelectFrame_Click(object? sender, EventArgs e)
        {
            using var ofd = new OpenFileDialog { Filter = "PNG Image|*.png", Title = "Select overlay frame PNG" };
            if (ofd.ShowDialog() != DialogResult.OK) return;
            _framePath = ofd.FileName;

            cmbWalpurgis.Text = "Custom";
            UpdateLivePreview(null);
        }

        private void DgvEntries_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvEntries.SelectedRows.Count > 0)
            {
                var row = dgvEntries.SelectedRows[0];
                if (row.Tag is DialogueEntry entry)
                {
                    UpdateLivePreview(entry);
                }
            }
            else
            {
                UpdateLivePreview(null);
            }
        }

        private void UpdateLivePreview(DialogueEntry? selectedEntry)
        {
            picPreview.Image?.Dispose();
            picPreview.Image = null;

            Image? activeFrame = null;
            bool isCustom = cmbWalpurgis.Text.Equals("Custom", StringComparison.OrdinalIgnoreCase);

            if (isCustom)
            {
                if (!string.IsNullOrEmpty(_framePath) && File.Exists(_framePath))
                {
                    activeFrame = Image.FromFile(_framePath);
                }
            }
            else
            {
                activeFrame = GetEmbeddedFrame(cmbWalpurgis.Text);
            }

            string? activeIconPath = _iconPath;
            if (selectedEntry != null && !string.IsNullOrEmpty(selectedEntry.CustomIconPath))
            {
                activeIconPath = selectedEntry.CustomIconPath;
            }

            if (activeFrame == null)
            {
                if (!string.IsNullOrEmpty(activeIconPath) && File.Exists(activeIconPath))
                    picPreview.Image = Image.FromFile(activeIconPath);
                return;
            }

            if (string.IsNullOrEmpty(activeIconPath) || !File.Exists(activeIconPath))
            {
                picPreview.Image = activeFrame;
                return;
            }

            try
            {
                using var icon = Image.FromFile(activeIconPath);
                using var frame = new Bitmap(activeFrame);

                var bmp = new Bitmap(frame.Width, frame.Height);
                using (var g = Graphics.FromImage(bmp))
                {
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.PixelOffsetMode = PixelOffsetMode.HighQuality;

                    float refW = 400f;
                    float refH = 250f;

                    float maskX = frame.Width * (62f / refW);
                    float maskY = frame.Height * (16.5f / refH);
                    float maskW = frame.Width * (274.8f / refW);
                    float maskH = frame.Height * (222.75f / refH);

                    var maskRect = new RectangleF(maskX, maskY, maskW, maskH);

                    float boxW = maskW * 1.44f;
                    float boxH = maskH * 1.244f;
                    float boxX = maskX - (boxW - maskW) / 2f;
                    float boxY = maskY - (boxH - maskH) / 2f - (0.083f * maskH);

                    using var clipPath = new GraphicsPath();
                    clipPath.AddRectangle(maskRect);
                    g.SetClip(clipPath);

                    DrawImageContain(g, icon, new RectangleF(boxX, boxY, boxW, boxH));

                    g.ResetClip();
                    g.DrawImage(frame, 0, 0);
                }

                picPreview.Image = bmp;
            }
            catch { }
            finally
            {
                activeFrame?.Dispose();
            }
        }

        private Image? GetEmbeddedFrame(string type)
        {
            string? resourceName = type.ToUpperInvariant() switch
            {
                "LOR" => "AnnouncerSlopFactory.Images.LOR.png",
                "LC_QOH" => "AnnouncerSlopFactory.Images.LC_QOH.png",
                "PILGRIMAGE" => "AnnouncerSlopFactory.Images.PILGRIMAGE.png",
                "NONE" => "AnnouncerSlopFactory.Images.NONE.png",
                "LC" => "AnnouncerSlopFactory.Images.LC.png",
                _ => null
            };

            if (resourceName == null) return null;

            var assembly = Assembly.GetExecutingAssembly();
            using Stream? stream = assembly.GetManifestResourceStream(resourceName);

            if (stream != null)
            {
                return new Bitmap(stream);
            }
            return null;
        }

        private void DrawImageContain(Graphics g, Image img, RectangleF targetRect)
        {
            float scale = Math.Min(targetRect.Width / img.Width, targetRect.Height / img.Height);
            float w = img.Width * scale;
            float h = img.Height * scale;
            float x = targetRect.X + (targetRect.Width - w) / 2;
            float y = targetRect.Y + (targetRect.Height - h) / 2;

            g.DrawImage(img, x, y, w, h);
        }

        private void BtnAddDialogue_Click(object? sender, EventArgs e) => contextMenuCategories.Show(btnAddDialogue, new Point(0, btnAddDialogue.Height));

        private void CategoryMenu_Click(object? sender, EventArgs e)
        {
            if (sender is not ToolStripMenuItem item || item.Tag is not string key) return;
            int nextVar = _entries.Where(x => x.Category == key).Select(x => x.Variant).DefaultIfEmpty(0).Max() + 1;
            var entry = new DialogueEntry { Category = key, DisplayName = item.Text ?? "", Variant = nextVar };
            _entries.Add(entry);
            RefreshGrid();
            EditEntry(entry);
        }

        private void DgvEntries_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = dgvEntries.Rows[e.RowIndex];
            if (row.Tag is not DialogueEntry entry) return;

            if (e.ColumnIndex == colCategory.Index) EditEntry(entry);
            else if (e.ColumnIndex == colDelete.Index)
            {
                _entries.Remove(entry);
                int v = 1;
                foreach (var ex in _entries.Where(x => x.Category == entry.Category).ToList()) ex.Variant = v++;
                RefreshGrid();
            }
        }

        private void RefreshGrid()
        {
            dgvEntries.Rows.Clear();
            foreach (var entry in _entries.OrderBy(x => Array.FindIndex(Categories, c => c.Key == x.Category)).ThenBy(x => x.Variant))
            {
                int idx = dgvEntries.Rows.Add();
                var row = dgvEntries.Rows[idx];
                row.Tag = entry;
                string catText = entry.DisplayName + (entry.Variant > 1 ? $"  (v{entry.Variant})" : "");
                row.Cells[colCategory.Index].Value = catText;
                row.Cells[colVariant.Index].Value = entry.Variant.ToString();
                row.Cells[colAudio.Index].Value = string.IsNullOrEmpty(entry.AudioPath) ? "(no audio)" : Path.GetFileName(entry.AudioPath);
                row.Cells[colDialogue.Index].Value = string.IsNullOrEmpty(entry.DialogueText) ? "(no text)" : entry.DialogueText.Replace("\r", "").Replace("\n", " ⏎ ");
                row.Cells[colDelete.Index].Value = "X";

                if (string.IsNullOrEmpty(entry.AudioPath)) row.Cells[colAudio.Index].Style.ForeColor = Color.Gray;
                if (string.IsNullOrEmpty(entry.DialogueText)) row.Cells[colDialogue.Index].Style.ForeColor = Color.Gray;
            }
        }

        private void EditEntry(DialogueEntry entry)
        {
            using var dlg = new Form
            {
                Text = $"Edit: {entry.DisplayName}  (variant {entry.Variant})",
                Size = new Size(580, 480),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                ShowInTaskbar = false,
                Font = this.Font,
            };

            var lblAudio = new Label { Text = "Audio File:", Location = new Point(15, 18), AutoSize = true };
            var txtAudio = new TextBox { Location = new Point(15, 40), Width = 335, ReadOnly = true, Text = entry.AudioPath ?? "" };
            var btnBrowse = new Button { Text = "Browse...", Location = new Point(355, 38), Size = new Size(95, 26) };
            var btnPlayAudio = new Button { Text = "▶ Play", Location = new Point(455, 38), Size = new Size(95, 26) };

            btnBrowse.Click += (s, e) =>
            {
                using var ofd = new OpenFileDialog { Filter = "WAV files|*.wav|All audio|*.wav;*.ogg;*.mp3;*.flac|All files|*.*", Title = $"Pick audio for {entry.Category}_{entry.Variant}" };
                if (ofd.ShowDialog(dlg) == DialogResult.OK) txtAudio.Text = ofd.FileName;
            };
            btnPlayAudio.Click += (s, e) => PlayAudio(txtAudio.Text);

            var lblDialogue = new Label { Text = "Dialogue Text  (\\n for newline):", Location = new Point(15, 78), AutoSize = true };
            var txtDialogue = new TextBox { Location = new Point(15, 100), Size = new Size(535, 120), Multiline = true, ScrollBars = ScrollBars.Vertical, Text = entry.DialogueText ?? "" };

            var lblCustomIcon = new Label { Text = "Custom Icon (Optional):", Location = new Point(15, 235), AutoSize = true };
            var picCustomIcon = new PictureBox { Location = new Point(15, 255), Size = new Size(60, 60), BorderStyle = BorderStyle.FixedSingle, SizeMode = PictureBoxSizeMode.Zoom, BackColor = Color.FromArgb(240, 240, 240) };
            var txtCustomIconPath = new TextBox { Location = new Point(85, 255), Width = 350, ReadOnly = true, Text = entry.CustomIconPath ?? "" };
            var btnSelectCustomIcon = new Button { Text = "Select...", Location = new Point(85, 285), Size = new Size(95, 26) };
            var btnClearCustomIcon = new Button { Text = "Clear", Location = new Point(185, 285), Size = new Size(95, 26) };

            if (!string.IsNullOrEmpty(entry.CustomIconPath) && File.Exists(entry.CustomIconPath))
                picCustomIcon.Image = Image.FromFile(entry.CustomIconPath);

            btnSelectCustomIcon.Click += (s, e) =>
            {
                using var ofd = new OpenFileDialog { Filter = "PNG Image|*.png", Title = "Select custom icon for this line" };
                if (ofd.ShowDialog(dlg) != DialogResult.OK) return;
                txtCustomIconPath.Text = ofd.FileName;
                picCustomIcon.Image?.Dispose();
                picCustomIcon.Image = Image.FromFile(ofd.FileName);
            };

            btnClearCustomIcon.Click += (s, e) =>
            {
                txtCustomIconPath.Text = "";
                picCustomIcon.Image?.Dispose();
                picCustomIcon.Image = null;
            };

            var btnOk = new Button { Text = "OK", DialogResult = DialogResult.OK, Location = new Point(385, 400), Size = new Size(80, 30) };
            var btnCancel = new Button { Text = "Cancel", DialogResult = DialogResult.Cancel, Location = new Point(470, 400), Size = new Size(80, 30) };

            dlg.Controls.AddRange(new Control[] {
                lblAudio, txtAudio, btnBrowse, btnPlayAudio,
                lblDialogue, txtDialogue,
                lblCustomIcon, picCustomIcon, txtCustomIconPath, btnSelectCustomIcon, btnClearCustomIcon,
                btnOk, btnCancel
            });
            dlg.AcceptButton = btnOk;
            dlg.CancelButton = btnCancel;

            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                entry.AudioPath = string.IsNullOrWhiteSpace(txtAudio.Text) ? null : txtAudio.Text.Trim();
                entry.DialogueText = txtDialogue.Text;
                entry.CustomIconPath = string.IsNullOrWhiteSpace(txtCustomIconPath.Text) ? null : txtCustomIconPath.Text.Trim();
                RefreshGrid();
                UpdateLivePreview(entry);
            }
        }

        private void BtnReset_Click(object? sender, EventArgs e)
        {
            if (MessageBox.Show("Clear all fields and dialogue entries?", "Reset", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            _currentSoundPlayer?.Stop();
            txtModFolder.Clear(); txtModName.Clear(); txtDisplayName.Clear();
            numAnnouncerId.Value = 9901; cmbWalpurgis.Text = "LOR"; numBaseId.Value = 0;
            trackBarVolume.Value = 100;
            chkNeglectWaiting.Checked = false; chkNeglectRandomize.Checked = false;
            picIcon.Image = null; _iconPath = null;
            _framePath = null; picPreview.Image = null;
            _entries.Clear(); RefreshGrid(); lblStatus.Text = "Ready";
        }

        private string GetMD5Hash(string filePath)
        {
            using var md5 = MD5.Create();
            using var stream = File.OpenRead(filePath);
            var hash = md5.ComputeHash(stream);
            return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
        }

        private void BtnExport_Click(object? sender, EventArgs e)
        {
            string modFolder = txtModFolder.Text.Trim();
            string modName = txtModName.Text.Trim();
            string displayName = txtDisplayName.Text.Trim();
            string walpurgis = cmbWalpurgis.Text.Trim();
            int announcerId = (int)numAnnouncerId.Value;
            int baseId = (int)numBaseId.Value;
            int volume = trackBarVolume.Value;

            if (string.IsNullOrEmpty(modFolder) || string.IsNullOrEmpty(modName) || string.IsNullOrEmpty(displayName))
            { MessageBox.Show("Please fill in Mod Folder, Mod Name, and Display Name.", "Missing Info", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            if (walpurgis.Equals("Custom", StringComparison.OrdinalIgnoreCase))
            {
                if (string.IsNullOrEmpty(_framePath) || !File.Exists(_framePath))
                {
                    MessageBox.Show("You must select an Overlay Frame PNG when Tape Overlay is set to 'Custom'.", "Missing Overlay", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (!Directory.Exists(modFolder))
            { MessageBox.Show($"Mod folder does not exist:\n{modFolder}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

            var filled = _entries.Where(x => !string.IsNullOrEmpty(x.AudioPath) || !string.IsNullOrWhiteSpace(x.DialogueText)).ToList();
            if (filled.Count == 0 && MessageBox.Show("No dialogue entries set up. Export anyway?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            string basePath = Path.Combine(modFolder, "custom_announcers", modName);
            bool existing = Directory.Exists(basePath) && Directory.EnumerateFileSystemEntries(basePath).Any();

            if (!ShowPreviewDialog(existing ? "Export Preview — folder exists!" : "Export Preview", BuildPreview(basePath, modName, filled, announcerId, volume), existing)) return;

            try
            {
                if (existing && MessageBox.Show($"Folder already exists:\n{basePath}\n\nMerge and continue?", "Confirm Overwrite", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;

                Directory.CreateDirectory(basePath);
                Directory.CreateDirectory(Path.Combine(basePath, "Voice"));
                Directory.CreateDirectory(Path.Combine(basePath, "Dialogue"));

                string exportWalpurgis = walpurgis;
                if (walpurgis.Equals("Custom", StringComparison.OrdinalIgnoreCase))
                {
                    exportWalpurgis = modName + "_overlay";
                }

                var meta = new AnnouncerMeta
                {
                    Id = announcerId,
                    Name = displayName,
                    ImgStr = modName + "_icon",
                    NeglectWaiting = chkNeglectWaiting.Checked ? (int?)numNeglectWaiting.Value : null,
                    NeglectRandomize = chkNeglectRandomize.Checked ? (int?)numNeglectRandomize.Value : null,
                    BaseId = baseId > 0 ? (int?)baseId : null,
                    WalpurgisType = string.IsNullOrEmpty(exportWalpurgis) ? null : exportWalpurgis,
                    Volume = volume != 100 ? (int?)volume : null,
                };

                File.WriteAllText(Path.Combine(basePath, modName + ".json"), JsonSerializer.Serialize(meta, new JsonSerializerOptions { WriteIndented = true, Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping }));

                if (!string.IsNullOrEmpty(_iconPath) && File.Exists(_iconPath))
                    File.Copy(_iconPath, Path.Combine(basePath, modName + "_icon.png"), true);

                if (!string.IsNullOrEmpty(_framePath) && File.Exists(_framePath) && walpurgis.Equals("Custom", StringComparison.OrdinalIgnoreCase))
                {
                    File.Copy(_framePath, Path.Combine(basePath, modName + "_overlay.png"), true);
                }

                // MD5 Deduplication Logic for Custom Icons
                var iconHashes = new Dictionary<string, string>();
                int iconCounter = 1;
                var dialogueList = new List<object>();

                foreach (var entry in filled)
                {
                    string? entryImgStr = null;

                    if (!string.IsNullOrEmpty(entry.CustomIconPath) && File.Exists(entry.CustomIconPath))
                    {
                        string hash = GetMD5Hash(entry.CustomIconPath);
                        if (!iconHashes.ContainsKey(hash))
                        {
                            string newFileName = $"{modName}_icon_{iconCounter++}";
                            File.Copy(entry.CustomIconPath, Path.Combine(basePath, newFileName + ".png"), true);
                            iconHashes[hash] = newFileName;
                        }
                        entryImgStr = iconHashes[hash];
                    }

                    if (entryImgStr != null)
                    {
                        dialogueList.Add(new { id = entry.DialogueId(announcerId), dlg = entry.DialogueText ?? "", imgStr = entryImgStr });
                    }
                    else
                    {
                        dialogueList.Add(new { id = entry.DialogueId(announcerId), dlg = entry.DialogueText ?? "" });
                    }
                }

                var dialogueOutput = new { dataList = dialogueList };
                File.WriteAllText(Path.Combine(basePath, "Dialogue", "EN.json"), JsonSerializer.Serialize(dialogueOutput, new JsonSerializerOptions { WriteIndented = true, Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping }));

                int copied = 0;
                foreach (var entry in filled)
                {
                    if (string.IsNullOrEmpty(entry.AudioPath) || !File.Exists(entry.AudioPath)) continue;
                    File.Copy(entry.AudioPath, Path.Combine(basePath, "Voice", entry.VoiceFile(entry.Variant)), true);
                    copied++;
                }

                lblStatus.Text = $"Exported to {basePath}";
                if (MessageBox.Show($"Export complete!\n\nLocation:\n{basePath}\n\nOpen folder in Explorer?", "Done", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    Process.Start(new ProcessStartInfo("explorer.exe", $"\"{basePath}\"") { UseShellExecute = true });
            }
            catch (Exception ex) { MessageBox.Show("Export failed:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private string BuildPreview(string basePath, string modName, List<DialogueEntry> filled, int announcerId, int volume)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Target folder:");
            sb.AppendLine("  " + basePath);
            sb.AppendLine("\nStructure that will be created:\n");
            sb.AppendLine($"  {modName}\\");
            sb.AppendLine($"  ├── {modName}.json");
            sb.AppendLine($"  ├── {modName}_icon.png{(_iconPath == null ? "   ← no icon selected" : "")}");

            if (cmbWalpurgis.Text.Equals("Custom", StringComparison.OrdinalIgnoreCase))
                sb.AppendLine($"  ├── {modName}_overlay.png (frame)");

            int customIcons = filled.Count(x => !string.IsNullOrEmpty(x.CustomIconPath));
            if (customIcons > 0)
                sb.AppendLine($"  ├── {modName}_icon_*.png (custom line icons)");

            sb.AppendLine($"  ├── Dialogue\\");
            sb.AppendLine($"  │   └── EN.json              ({filled.Count} entries)");
            sb.AppendLine($"  └── Voice\\");

            var withAudio = filled.Where(x => !string.IsNullOrEmpty(x.AudioPath)).ToList();
            for (int i = 0; i < withAudio.Count; i++) sb.AppendLine($"      {(i == withAudio.Count - 1 ? "└" : "├")}── {withAudio[i].VoiceFile(withAudio[i].Variant)}");
            if (withAudio.Count == 0) sb.AppendLine("      (no audio files selected)");

            sb.AppendLine("\nSummary:");
            sb.AppendLine($"  • Announcer ID:     {announcerId}");
            sb.AppendLine($"  • Dialogue entries: {filled.Count}");
            sb.AppendLine($"  • Audio files:      {withAudio.Count}");
            sb.AppendLine($"  • Custom Icons:     {customIcons}");
            sb.AppendLine($"  • Volume:           {(volume == 100 ? "100 (default)" : volume.ToString())}");
            sb.AppendLine($"  • Tape Overlay:     {(string.IsNullOrWhiteSpace(cmbWalpurgis.Text) ? "(default)" : cmbWalpurgis.Text)}");

            if (Directory.Exists(basePath) && Directory.EnumerateFileSystemEntries(basePath).Any())
                sb.AppendLine("\n⚠  WARNING: The target folder already exists and is not empty.\n   Files with the same name will be OVERWRITTEN.");

            return sb.ToString();
        }

        private bool ShowPreviewDialog(string title, string message, bool overwrite)
        {
            using var dlg = new Form { Text = title, Size = new Size(660, 560), StartPosition = FormStartPosition.CenterParent, FormBorderStyle = FormBorderStyle.FixedDialog, MaximizeBox = false, MinimizeBox = false, ShowInTaskbar = false, Font = this.Font };
            var pnlBody = new Panel { Dock = DockStyle.Fill, Padding = new Padding(10) };
            var rtb = new RichTextBox { Dock = DockStyle.Fill, ReadOnly = true, Font = new Font("Consolas", 9.5f), BackColor = Color.White, BorderStyle = BorderStyle.None, Text = message, WordWrap = false, ScrollBars = RichTextBoxScrollBars.Both };
            pnlBody.Controls.Add(rtb);

            var pnlButtons = new Panel { Dock = DockStyle.Bottom, Height = 52 };
            var btnOk = new Button { Text = overwrite ? "Overwrite & Export" : "Export", DialogResult = DialogResult.OK, Size = new Size(150, 32), Location = new Point(pnlButtons.Width - 240, 10), Anchor = AnchorStyles.Top | AnchorStyles.Right };
            var btnCancel = new Button { Text = "Cancel", DialogResult = DialogResult.Cancel, Size = new Size(80, 32), Location = new Point(pnlButtons.Width - 85, 10), Anchor = AnchorStyles.Top | AnchorStyles.Right };
            pnlButtons.Controls.Add(btnOk); pnlButtons.Controls.Add(btnCancel);

            dlg.Controls.Add(pnlBody); dlg.Controls.Add(pnlButtons);
            dlg.AcceptButton = btnOk; dlg.CancelButton = btnCancel;
            return dlg.ShowDialog(this) == DialogResult.OK;
        }
    }
}