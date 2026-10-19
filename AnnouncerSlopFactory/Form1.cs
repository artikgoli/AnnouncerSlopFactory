using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows.Forms;

namespace AnnouncerSlopFactory
{
    public partial class Form1 : Form
    {
        // ------------------------------------------------------------------
        // Model
        // ------------------------------------------------------------------
        public class DialogueEntry
        {
            public string Category = "";
            public string DisplayName = "";
            public int Variant = 1;
            public string? AudioPath;
            public string? DialogueText;

            public string DialogueId(int announcerId) => $"{Category}_{announcerId}_{Variant}";
            public string VoiceFile(int variant) => $"{Category}_{variant}.wav";
        }

        private class AnnouncerMeta
        {
            [JsonPropertyName("id")] public int Id { get; set; }
            [JsonPropertyName("name")] public string? Name { get; set; }
            [JsonPropertyName("imgStr")] public string? ImgStr { get; set; }

            [JsonPropertyName("baseId")]
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
            public int? BaseId { get; set; }

            [JsonPropertyName("walpurgisType")]
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
            public string? WalpurgisType { get; set; }
        }

        // ------------------------------------------------------------------
        // Category catalog
        // ------------------------------------------------------------------
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

            MakeExportButton();
            MakeBrowseButton();
        }

        private void MakeExportButton()
        {
            if (btnExport != null)
            {
                btnExport.Visible = false;
                btnExport.Enabled = false;
            }

            var btn = new Button
            {
                Text = "Export Mod",
                Size = new Size(150, 34),
                BackColor = Color.FromArgb(0, 120, 215),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font(this.Font.FontFamily, 9.5f, FontStyle.Bold),
                Cursor = Cursors.Hand,
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.Click += BtnExport_Click;

            pnlBottom.Controls.Add(btn);
            btn.BringToFront();

            void Place()
            {
                btn.Left = pnlBottom.ClientSize.Width - btn.Width;
                btn.Top = (pnlBottom.ClientSize.Height - btn.Height) / 2;
            }
            Place();
            pnlBottom.Resize += (s, e) => Place();
        }

        private void MakeBrowseButton()
        {
            // Hide the designer button (still wired, just not visible)
            if (btnBrowseModFolder != null)
            {
                btnBrowseModFolder.Visible = false;
                btnBrowseModFolder.Enabled = false;
            }

            var btn = new Button
            {
                Text = "Browse...",
                Size = new Size(100, 26),
                Cursor = Cursors.Hand,
                FlatStyle = FlatStyle.System,
            };
            btn.Click += BtnBrowseModFolder_Click;

            pnlTop.Controls.Add(btn);
            btn.BringToFront();

            void Place()
            {
                const int padding = 10;

                // Browse button — right-aligned, vertically centered
                btn.Left = pnlTop.ClientSize.Width - btn.Width - padding;
                btn.Top = (pnlTop.ClientSize.Height - btn.Height) / 2;

                // Mod folder textbox — stretch between label and button
                int textLeft = txtModFolder.Left;             // stays at 95 from designer
                int textRight = btn.Left - padding;
                if (textRight > textLeft + 100)
                    txtModFolder.Width = textRight - textLeft;
            }

            Place();
            pnlTop.Resize += (s, e) => Place();
        }

        // ------------------------------------------------------------------
        // Top bar
        // ------------------------------------------------------------------
        private void BtnBrowseModFolder_Click(object sender, EventArgs e)
        {
            using var fbd = new FolderBrowserDialog
            {
                Description = "Select the mod folder (the one that will contain custom_announcers/)"
            };
            if (fbd.ShowDialog() == DialogResult.OK)
                txtModFolder.Text = fbd.SelectedPath;
        }

        private void BtnSelectIcon_Click(object sender, EventArgs e)
        {
            using var ofd = new OpenFileDialog { Filter = "PNG Image|*.png", Title = "Select icon PNG" };
            if (ofd.ShowDialog() != DialogResult.OK) return;
            _iconPath = ofd.FileName;
            picIcon.Image = Image.FromFile(_iconPath);
        }

        // ------------------------------------------------------------------
        // Add Dialogue
        // ------------------------------------------------------------------
        private void BtnAddDialogue_Click(object sender, EventArgs e)
        {
            contextMenuCategories.Show(btnAddDialogue, new Point(0, btnAddDialogue.Height));
        }

        private void CategoryMenu_Click(object? sender, EventArgs e)
        {
            if (sender is not ToolStripMenuItem item) return;
            if (item.Tag is not string key) return;

            int nextVar = _entries
                .Where(x => x.Category == key)
                .Select(x => x.Variant)
                .DefaultIfEmpty(0)
                .Max() + 1;

            var entry = new DialogueEntry
            {
                Category = key,
                DisplayName = item.Text,
                Variant = nextVar,
            };
            _entries.Add(entry);
            RefreshGrid();
            EditEntry(entry);
        }

        // ------------------------------------------------------------------
        // Grid
        // ------------------------------------------------------------------
        private void DgvEntries_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = dgvEntries.Rows[e.RowIndex];
            if (row.Tag is not DialogueEntry entry) return;

            if (e.ColumnIndex == colCategory.Index)
                EditEntry(entry);
            else if (e.ColumnIndex == colDelete.Index)
            {
                _entries.Remove(entry);
                RenumberVariants(entry.Category);
                RefreshGrid();
            }
        }

        private void RenumberVariants(string category)
        {
            int v = 1;
            foreach (var e in _entries.Where(x => x.Category == category).ToList())
                e.Variant = v++;
        }

        private void RefreshGrid()
        {
            dgvEntries.Rows.Clear();
            foreach (var entry in _entries
                .OrderBy(x => Array.FindIndex(Categories, c => c.Key == x.Category))
                .ThenBy(x => x.Variant))
            {
                int idx = dgvEntries.Rows.Add();
                var row = dgvEntries.Rows[idx];
                row.Tag = entry;

                string catText = entry.DisplayName;
                if (entry.Variant > 1) catText += $"  (v{entry.Variant})";

                row.Cells[colCategory.Index].Value = catText;
                row.Cells[colVariant.Index].Value = entry.Variant.ToString();
                row.Cells[colAudio.Index].Value = string.IsNullOrEmpty(entry.AudioPath)
                                                        ? "(no audio)"
                                                        : Path.GetFileName(entry.AudioPath);
                row.Cells[colDialogue.Index].Value = string.IsNullOrEmpty(entry.DialogueText)
                                                        ? "(no text)"
                                                        : entry.DialogueText.Replace("\r", "").Replace("\n", " ⏎ ");
                row.Cells[colDelete.Index].Value = "X";

                if (string.IsNullOrEmpty(entry.AudioPath))
                    row.Cells[colAudio.Index].Style.ForeColor = Color.Gray;
                if (string.IsNullOrEmpty(entry.DialogueText))
                    row.Cells[colDialogue.Index].Style.ForeColor = Color.Gray;
            }
        }

        // ------------------------------------------------------------------
        // Edit dialog
        // ------------------------------------------------------------------
        private void EditEntry(DialogueEntry entry)
        {
            using var dlg = new Form
            {
                Text = $"Edit: {entry.DisplayName}  (variant {entry.Variant})",
                Size = new Size(580, 360),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                ShowInTaskbar = false,
                Font = this.Font,
            };

            var lblAudio = new Label { Text = "Audio File:", Location = new Point(15, 18), AutoSize = true };
            var txtAudio = new TextBox
            {
                Location = new Point(15, 40),
                Width = 435,
                ReadOnly = true,
                Text = entry.AudioPath ?? "",
            };
            var btnBrowse = new Button { Text = "Browse...", Location = new Point(455, 38), Size = new Size(95, 26) };
            btnBrowse.Click += (s, e) =>
            {
                using var ofd = new OpenFileDialog
                {
                    Filter = "WAV files|*.wav|All audio|*.wav;*.ogg;*.mp3;*.flac|All files|*.*",
                    Title = $"Pick audio for {entry.Category}_{entry.Variant}",
                };
                if (ofd.ShowDialog(dlg) == DialogResult.OK)
                    txtAudio.Text = ofd.FileName;
            };

            var lblDialogue = new Label { Text = "Dialogue Text  (\\n for newline):", Location = new Point(15, 78), AutoSize = true };
            var txtDialogue = new TextBox
            {
                Location = new Point(15, 100),
                Size = new Size(535, 160),
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                Text = entry.DialogueText ?? "",
            };

            var btnOk = new Button { Text = "OK", DialogResult = DialogResult.OK, Location = new Point(385, 275), Size = new Size(80, 30) };
            var btnCancel = new Button { Text = "Cancel", DialogResult = DialogResult.Cancel, Location = new Point(470, 275), Size = new Size(80, 30) };

            dlg.Controls.AddRange(new Control[] {
                lblAudio, txtAudio, btnBrowse,
                lblDialogue, txtDialogue,
                btnOk, btnCancel });
            dlg.AcceptButton = btnOk;
            dlg.CancelButton = btnCancel;

            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                entry.AudioPath = string.IsNullOrWhiteSpace(txtAudio.Text) ? null : txtAudio.Text.Trim();
                entry.DialogueText = txtDialogue.Text;
                RefreshGrid();
            }
        }

        // ------------------------------------------------------------------
        // Reset
        // ------------------------------------------------------------------
        private void BtnReset_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Clear all fields and dialogue entries?", "Reset",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            txtModFolder.Clear();
            txtModName.Clear();
            txtDisplayName.Clear();
            numAnnouncerId.Value = 9901;
            cmbWalpurgis.SelectedIndex = 2;
            numBaseId.Value = 0;
            picIcon.Image = null;
            _iconPath = null;
            _entries.Clear();
            RefreshGrid();
            lblStatus.Text = "Ready";
        }

        // ==================================================================
        // EXPORT — this is the button that creates the folder, JSONs, etc.
        // ==================================================================
        private void BtnExport_Click(object sender, EventArgs e)
        {
            // ----- 1. Validate -----
            string modFolder = txtModFolder.Text.Trim();
            string modName = txtModName.Text.Trim();
            string displayName = txtDisplayName.Text.Trim();
            string walpurgis = cmbWalpurgis.Text.Trim();
            int announcerId = (int)numAnnouncerId.Value;
            int baseId = (int)numBaseId.Value;

            if (string.IsNullOrEmpty(modFolder)) { Warn("Please select the mod folder."); return; }
            if (string.IsNullOrEmpty(modName)) { Warn("Please enter a mod name (folder name)."); return; }
            if (string.IsNullOrEmpty(displayName)) { Warn("Please enter a display name."); return; }
            if (announcerId < 100 || announcerId > 9999)
            {
                Warn("Announcer ID must be between 100 and 9999.");
                return;
            }
            if (!Directory.Exists(modFolder))
            {
                Warn($"Mod folder does not exist:\n{modFolder}\n\nCreate it in Explorer first or pick another folder.");
                return;
            }

            var filled = _entries
                .Where(x => !string.IsNullOrEmpty(x.AudioPath) || !string.IsNullOrWhiteSpace(x.DialogueText))
                .ToList();

            if (filled.Count == 0 &&
                MessageBox.Show("No dialogue entries have been set up. Export anyway?",
                    "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            // ----- 2. Target path + preview -----
            string basePath = Path.Combine(modFolder, "custom_announcers", modName);
            bool existing = Directory.Exists(basePath) && Directory.EnumerateFileSystemEntries(basePath).Any();

            string preview = BuildPreview(basePath, modName, filled, announcerId);

            if (!ShowPreviewDialog(existing ? "Export Preview — folder exists!" : "Export Preview", preview, existing))
                return;

            // ----- 3. Perform export -----
            try
            {
                // Overwrite / merge confirmation (belt-and-braces)
                if (existing &&
                    MessageBox.Show($"Folder already exists:\n{basePath}\n\nMerge (overwrite files with same name) and continue?",
                        "Confirm Overwrite", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                    return;

                // Create the three required folders
                Directory.CreateDirectory(basePath);
                Directory.CreateDirectory(Path.Combine(basePath, "Voice"));
                Directory.CreateDirectory(Path.Combine(basePath, "Dialogue"));

                // (a) <modName>.json — announcer definition
                var meta = new AnnouncerMeta
                {
                    Id = announcerId,
                    Name = displayName,
                    ImgStr = modName + "_icon",
                    WalpurgisType = string.IsNullOrEmpty(walpurgis) ? null : walpurgis,
                    BaseId = baseId > 0 ? (int?)baseId : null,
                };
                string metaJson = JsonSerializer.Serialize(meta,
                    new JsonSerializerOptions { WriteIndented = true, Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping });
                File.WriteAllText(Path.Combine(basePath, modName + ".json"), metaJson);

                // (b) <modName>_icon.png
                if (!string.IsNullOrEmpty(_iconPath) && File.Exists(_iconPath))
                    File.Copy(_iconPath, Path.Combine(basePath, modName + "_icon.png"), true);

                // (c) Dialogue/EN.json
                var dialogueOutput = new
                {
                    dataList = filled.Select(x => new
                    {
                        id = x.DialogueId(announcerId),
                        dlg = x.DialogueText ?? ""
                    }).ToList()
                };
                string dialogueJson = JsonSerializer.Serialize(dialogueOutput,
                    new JsonSerializerOptions { WriteIndented = true, Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping });
                File.WriteAllText(Path.Combine(basePath, "Dialogue", "EN.json"), dialogueJson);

                // (d) Voice/<category>_<variant>.wav
                int copied = 0;
                foreach (var entry in filled)
                {
                    if (string.IsNullOrEmpty(entry.AudioPath) || !File.Exists(entry.AudioPath)) continue;
                    File.Copy(entry.AudioPath,
                              Path.Combine(basePath, "Voice", entry.VoiceFile(entry.Variant)),
                              true);
                    copied++;
                }

                // ----- 4. Success report -----
                int totalFiles = Directory.GetFiles(basePath, "*", SearchOption.AllDirectories).Length;
                lblStatus.Text = $"Exported to {basePath}";

                var answer = MessageBox.Show(
                    $"Export complete!\n\n" +
                    $"Location:\n{basePath}\n\n" +
                    $"Files written: {totalFiles}\n" +
                    $"  • 1 × {modName}.json\n" +
                    $"  • 1 × {modName}_icon.png {(_iconPath == null ? "(skipped — no icon selected)" : "")}\n" +
                    $"  • 1 × Dialogue/EN.json ({filled.Count} entries)\n" +
                    $"  • {copied} × Voice/*.wav\n\n" +
                    $"Open the folder in Explorer?",
                    "Export Complete", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (answer == DialogResult.Yes)
                    Process.Start(new ProcessStartInfo("explorer.exe", $"\"{basePath}\"") { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Export failed:\n\n" + ex.Message, "Export Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ------------------------------------------------------------------
        // Preview text builder
        // ------------------------------------------------------------------
        private string BuildPreview(string basePath, string modName, List<DialogueEntry> filled, int announcerId)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Target folder:");
            sb.AppendLine("  " + basePath);
            sb.AppendLine();
            sb.AppendLine("Structure that will be created:");
            sb.AppendLine();
            sb.AppendLine($"  {modName}\\");
            sb.AppendLine($"  ├── {modName}.json");
            sb.AppendLine($"  ├── {modName}_icon.png{(_iconPath == null ? "   ← no icon selected" : "")}");
            sb.AppendLine($"  ├── Dialogue\\");
            sb.AppendLine($"  │   └── EN.json              ({filled.Count} entries)");
            sb.AppendLine($"  └── Voice\\");

            var withAudio = filled.Where(x => !string.IsNullOrEmpty(x.AudioPath)).ToList();
            for (int i = 0; i < withAudio.Count; i++)
            {
                bool last = (i == withAudio.Count - 1);
                string line = $"      {(last ? "└" : "├")}── {withAudio[i].VoiceFile(withAudio[i].Variant)}";
                sb.AppendLine(line);
            }
            if (withAudio.Count == 0)
                sb.AppendLine("      (no audio files selected)");

            sb.AppendLine();
            sb.AppendLine("Summary:");
            sb.AppendLine($"  • Announcer ID:     {announcerId}");
            sb.AppendLine($"  • Dialogue entries: {filled.Count}");
            sb.AppendLine($"  • Audio files:      {withAudio.Count}");
            sb.AppendLine($"  • Missing audio:    {filled.Count - withAudio.Count}");
            sb.AppendLine($"  • Icon:             {(_iconPath == null ? "not set" : Path.GetFileName(_iconPath))}");

            if (Directory.Exists(basePath) && Directory.EnumerateFileSystemEntries(basePath).Any())
            {
                sb.AppendLine();
                sb.AppendLine("⚠  WARNING: The target folder already exists and is not empty.");
                sb.AppendLine("   Files with the same name will be OVERWRITTEN.");
            }

            if (filled.Count - withAudio.Count > 0)
            {
                sb.AppendLine();
                sb.AppendLine($"⚠  {filled.Count - withAudio.Count} entries have dialogue text but no audio.");
                sb.AppendLine("   Those subtitles will play silent in-game.");
            }

            if (_iconPath == null)
            {
                sb.AppendLine();
                sb.AppendLine("⚠  No icon selected — no PNG will be written.");
                sb.AppendLine("   The game will fall back to the default portrait frame.");
            }

            return sb.ToString();
        }

        // ------------------------------------------------------------------
        // Preview dialog
        // ------------------------------------------------------------------
        private bool ShowPreviewDialog(string title, string message, bool overwrite)
        {
            using var dlg = new Form
            {
                Text = title,
                Size = new Size(660, 520),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                ShowInTaskbar = false,
                Font = this.Font,
            };

            var pnlBody = new Panel { Dock = DockStyle.Fill, Padding = new Padding(10) };
            var rtb = new RichTextBox
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                Font = new Font("Consolas", 9.5f),
                BackColor = Color.White,
                BorderStyle = BorderStyle.None,
                Text = message,
                WordWrap = false,
                ScrollBars = RichTextBoxScrollBars.Both,
            };
            pnlBody.Controls.Add(rtb);

            var pnlButtons = new Panel { Dock = DockStyle.Bottom, Height = 52 };
            var btnOk = new Button
            {
                Text = overwrite ? "Overwrite & Export" : "Export",
                DialogResult = DialogResult.OK,
                Size = new Size(150, 32),
                Location = new Point(pnlButtons.Width - 240, 10),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
            };
            var btnCancel = new Button
            {
                Text = "Cancel",
                DialogResult = DialogResult.Cancel,
                Size = new Size(80, 32),
                Location = new Point(pnlButtons.Width - 85, 10),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
            };
            pnlButtons.Controls.Add(btnOk);
            pnlButtons.Controls.Add(btnCancel);

            dlg.Controls.Add(pnlBody);
            dlg.Controls.Add(pnlButtons);
            dlg.AcceptButton = btnOk;
            dlg.CancelButton = btnCancel;

            return dlg.ShowDialog(this) == DialogResult.OK;
        }

        private static void Warn(string msg) =>
            MessageBox.Show(msg, "Missing Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void lblModName_Click(object sender, EventArgs e)
        {
        }
    }
}