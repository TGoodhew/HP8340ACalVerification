# HP8340A/B Calibration Verification

This is a modern recreation of the HP 8340A/B Attenuator calibration and operation verification test software using .NET and the NI-VISA libraries.

## Overview

The HP 8340A and 8340B are synthesized signal generators manufactured by Hewlett-Packard (now Keysight Technologies). This application provides a command-line interface for performing attenuator calibration and operation verification tests on these instruments.

## Features

- **Spectre.Console UI**: Modern, user-friendly command-line interface
- **NI-VISA Communication**: Industry-standard GPIB communication with test equipment
- **Modular Design**: Easy to extend with additional test procedures
- **Engineering Format**: Automatic conversion of values to proper engineering notation with SI prefixes

## Prerequisites

- **.NET Framework 4.7.2** or later (for Windows)
- **Visual Studio 2017** or later (recommended for building)
- **NI-VISA Runtime** (download from [National Instruments](https://www.ni.com/en-us/support/downloads/drivers/download.ni-visa.html))
- **GPIB interface hardware** (National Instruments GPIB-USB-HS, etc.)
- **HP 8340A or HP 8340B Signal Generator**

> **Note**: This is a .NET Framework 4.7.2 Windows application. It requires Visual Studio or MSBuild on Windows to build. The project uses packages.config for NuGet package management, following the traditional .NET Framework project structure.

## Installation

1. Clone this repository:
   ```bash
   git clone https://github.com/TGoodhew/HP8340ACalVerification.git
   cd HP8340ACalVerification
   ```

2. Open the solution in Visual Studio:
   - Open `HP8340ACalVerification.sln` in Visual Studio 2017 or later
   - Visual Studio will automatically restore NuGet packages

3. Build the project:
   - In Visual Studio: Build → Build Solution (or press Ctrl+Shift+B)
   - Or using MSBuild from command line:
     ```bash
     msbuild HP8340ACalVerification.sln /t:Build /p:Configuration=Release
     ```

4. Run the application:
   - In Visual Studio: Debug → Start Without Debugging (or press Ctrl+F5)
   - Or run the executable directly:
     ```bash
     HP8340ACalVerification\bin\Release\HP8340ACalVerification.exe
     ```

## Usage

1. **Set GPIB Address**: Configure the GPIB address of your HP 8340A/B (default is 19)
2. **Connect to Instrument**: Establish connection to the instrument via GPIB
3. **Query Instrument ID**: Verify communication with the instrument
4. **Run Attenuator Calibration**: Execute the attenuator calibration procedure
5. **Run Operation Verification**: Execute the operation verification tests

## Development Status

This project is under active development. Current status:

- [x] Basic application structure (.NET Framework 4.7.2)
- [x] GPIB connection functionality
- [x] Spectre.Console UI
- [x] Code review feedback addressed
- [x] Service manual received (9018-05913.pdf)
- [x] HP-BASIC reference files received
- [ ] Attenuator calibration procedures (implementation in progress)
- [ ] Operation verification tests (implementation in progress)
- [ ] Integration of HP-BASIC program logic into C#

## HP-BASIC Reference Files Received

The following HP-BASIC program files have been provided for reference:

- **Alc_ad.txt** - ALC (Automatic Level Control) Adjust and Verify
- **At_cal.txt** - Attenuator Calibration and Verification
- **Cal_co.txt** - Calibration Constants Utilities
- **Copy.txt** - Software copy program
- **Cw_acc.txt** - CW Frequency Accuracy Test
- **Fre_sw.txt** - Frequency Switching Time Test
- **FS_MANAGER.txt** - File System Manager
- **JANITOR.txt** - Cleanup and initialization routines
- **Max_le.txt** - Maximum Level Test
- **Pwracc.txt** - Power Accuracy Test
- **Thrash.txt** - System test utilities

These files are being analyzed to recreate the functionality in C# using modern .NET and NI-VISA libraries.

## BDAT File Handling

The HP-BASIC programs use BDAT (Binary Data) files for storing calibration data, test results, and configuration. The application includes a `BdatFileManager` class that:

- **Tracks BDAT file access** - Logs when code attempts to read or write BDAT files
- **Creates reminders** - Displays warnings when unimplemented BDAT files are accessed
- **Generates GitHub issue templates** - Provides formatted text for creating issues to track BDAT implementation

When a BDAT file is accessed, the application will display:
```
================================================================================
WARNING: BDAT file access detected - Cal_co0001
Purpose: Calibration Constants
Access Type: READ

A GitHub issue should be created to implement this BDAT file handler.
See docs/BDAT_FILES.md for more information.
================================================================================
```

For detailed information about BDAT files and implementation strategy, see `docs/BDAT_FILES.md`.

## Service Manual and HP-BASIC Files

**Note**: This application is being developed to replicate the functionality of the original HP-BASIC calibration and verification procedures. To complete the implementation, the following files are needed:

- HP 8340A/B Service Manual (for calibration procedures and specifications)
- Original HP-BASIC calibration programs
- Original HP-BASIC operation verification programs

These files will be referenced to ensure the procedures match the original HP specifications.

## Architecture

The application follows the architecture patterns established in other HP test equipment projects:

- Console application using Spectre.Console for UI
- NI-VISA for GPIB instrument communication
- Engineering format utilities for proper unit display
- Modular design for easy addition of test procedures

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Acknowledgments

- Hewlett-Packard/Keysight Technologies for the HP 8340A/B signal generator
- National Instruments for the VISA standard and libraries
- Spectre.Console for the excellent console UI framework

## Contributing

Contributions are welcome! Please feel free to submit pull requests or open issues for bugs, feature requests, or improvements.

## Contact

For questions or support, please open an issue on the GitHub repository.

