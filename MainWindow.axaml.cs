using Avalonia.Controls;
using Avalonia.Interactivity;
using AvaloniaHex.Document;
using System;
using System.IO;
using System.Text;

namespace DDLParserWV
{
    public partial class MainWindow : Window
    {
        private BPTFile? _bptFile;
        private bool _ok = true;
        private string? _json;
        private string? _markdown;
        private StringBuilder? _debugOutput;

        public MainWindow() => InitializeComponent();

        private async void ScanBinary_Click(object? sender, RoutedEventArgs e)
        {
            var dialog = new Avalonia.Controls.OpenFileDialog();
            var result = await dialog.ShowAsync(this);
            if (result == null || result.Length == 0) return;

            string path = result[0];
            _ok = true;
            _debugOutput = new StringBuilder();

            byte[] data = File.ReadAllBytes(path);

            tabControl.SelectedIndex = 0;

            Avalonia.Threading.Dispatcher.UIThread.Post(() => 
            {
                hexEditor.Document = new ByteArrayBinaryDocument(data);
            }, Avalonia.Threading.DispatcherPriority.Background);

            hexEditor.Document = new ByteArrayBinaryDocument(data);

            var m = new MemoryStream(data);
            var file = new BPTFile(Path.GetFileName(path));

            while (m.Position < data.Length)
            {
                try
                {
                    uint magic = Utils.ReadU32(m);
                    if (magic == Utils.BPT_MAGIC)
                    {
                        var tree = new ParseTree(m, _debugOutput);
                        file.ParseTrees.Add(tree);
                    }
                }
                catch (Exception ex)
                {
                    _ok = false;
                    Log($"[ERROR] {ex}");
                    Log($"[ERROR] Position = 0x{m.Position:X8}");
                    outputBox.Text = _debugOutput.ToString();
                }
            }

            if (_ok)
            {
                _bptFile = file;
                try
                {
                    _json = _bptFile.ToJson();
                    _markdown = _bptFile.ToMarkdown();
                }
                catch (Exception ex)
                {
                    _ok = false;
                    Log($"[ERROR] {ex}");
                    outputBox.Text = _debugOutput.ToString();
                    return;
                }

                UpdateOutput();
            }

            tabControl.SelectedIndex = 1;
        }

        private void Radio_Changed(object? sender, RoutedEventArgs e)
        {
            // Only run if the radio button being clicked is the one becoming 'True'
            if (sender is RadioButton { IsChecked: true })
            {
                UpdateOutput();
            }
        }

        private void UpdateOutput()
        {
            if (_bptFile == null || !_ok) return;

            if (radioJson.IsChecked == true)
                outputBox.Text = _json;
            else if (radioMarkdown.IsChecked == true)
                outputBox.Text = _markdown;
            else
                outputBox.Text = _debugOutput?.ToString();
        }

        private void Log(string s) => _debugOutput?.AppendLine(s);
    }
}
