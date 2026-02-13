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

- .NET 8.0 SDK or later
- NI-VISA Runtime (download from [National Instruments](https://www.ni.com/en-us/support/downloads/drivers/download.ni-visa.html))
- GPIB interface hardware (National Instruments GPIB-USB-HS, etc.)
- HP 8340A or HP 8340B Signal Generator

## Installation

1. Clone this repository:
   ```bash
   git clone https://github.com/TGoodhew/HP8340ACalVerification.git
   cd HP8340ACalVerification
   ```

2. Build the project:
   ```bash
   dotnet build
   ```

3. Run the application:
   ```bash
   dotnet run --project HP8340ACalVerification/HP8340ACalVerification.csproj
   ```

## Usage

1. **Set GPIB Address**: Configure the GPIB address of your HP 8340A/B (default is 19)
2. **Connect to Instrument**: Establish connection to the instrument via GPIB
3. **Query Instrument ID**: Verify communication with the instrument
4. **Run Attenuator Calibration**: Execute the attenuator calibration procedure
5. **Run Operation Verification**: Execute the operation verification tests

## Development Status

This project is under active development. Current status:

- [x] Basic application structure
- [x] GPIB connection functionality
- [x] Spectre.Console UI
- [ ] Attenuator calibration procedures (pending HP-BASIC reference files)
- [ ] Operation verification tests (pending HP-BASIC reference files)
- [ ] Detailed documentation from service manual

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

