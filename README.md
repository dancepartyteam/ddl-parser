## Quazal DDL Parser (Avalonia Port)

This project is a cross-platform port of the **DDLParserWV** tool originally found in the [acb-rdv repository](https://github.com/michal-kapala/acb-rdv/tree/master/DDLParserWV). It is designed to parse and visualize Quazal *.bpt (DDL) binary files, specifically targeting formats used in the Quazal RendezVous (RDV) servers.

The original Windows Forms implementation has been replaced with **Avalonia UI** to enable native support for macOS (ARM64/Intel), Linux, and Windows.

---

### Features

* **Binary Hex View**: Integrated hex editor powered by AvaloniaHex for inspecting raw file data.
* **Multi-Format Export**: Parse binary structures into JSON or Markdown formats.
* **Cross-Platform**: Built on .NET 8, allowing the tool to run natively on macOS ARM64 without requiring Windows emulation.

---

### Requirements

* **Runtime**: .NET 8.0 SDK or Desktop Runtime.
* **Operating System**: macOS (tested on Sequoia/ARM64), Windows 10/11, or Linux.

---

### Installation and Build

1. **Clone the repository**:
   ```bash
   git clone https://github.com/dancepartyteam/ddl-parser.git
   cd ddl-parser
   ```

2. **Clean previous build artifacts**:
   Especially important when moving between different operating systems to clear platform-specific caches.
   ```bash
   rm -rf bin obj
   ```

3. **Restore and Run**:
   ```bash
   dotnet restore
   dotnet run
   ```

---

### Usage

1. Launch the application.
2. Select **Scan binary** from the top menu.
3. Choose a compatible Quazal binary file.
4. Use the **Hex View** tab to inspect the raw bytes.
5. Use the **Parsed Output** tab and the radio buttons (JSON, Markdown, Debug) to view the interpreted structure.

---

### Acknowledgments

* **Original Logic**: Based on the DDLParserWV implementation by michal-kapala.
* **UI Framework**: Built with Avalonia UI.
* **Hex Component**: Utilizes the AvaloniaHex library.