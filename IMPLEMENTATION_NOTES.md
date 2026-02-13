# Implementation Notes

## Conversion to .NET Framework 4.7.2

This project has been converted from .NET 8.0 to .NET Framework 4.7.2 as requested to align with the user's existing HP equipment test software architecture.

### Project Structure

```
HP8340ACalVerification/
├── HP8340ACalVerification.sln          # Visual Studio solution file
├── HP8340ACalVerification/
│   ├── HP8340ACalVerification.csproj   # Traditional .NET Framework project
│   ├── App.config                       # Runtime configuration
│   ├── packages.config                  # NuGet packages (traditional format)
│   ├── Properties/
│   │   └── AssemblyInfo.cs             # Assembly metadata
│   ├── Program.cs                       # Main application
│   └── ToEngineeringFormat.cs          # Utility class
├── docs/
│   ├── README.md                        # Documentation guide
│   └── hp-basic/                        # HP-BASIC reference files
│       ├── README.md                    # HP-BASIC documentation
│       ├── Alc_ad.txt                   # ALC calibration
│       ├── At_cal.txt                   # Attenuator calibration
│       └── ... (9 more HP-BASIC files)
└── README.md                            # Project README
```

### Building the Project

**Requirements:**
- Visual Studio 2017 or later (recommended)
- .NET Framework 4.7.2 Developer Pack
- NI-VISA runtime installed

**Build Steps:**
1. Open `HP8340ACalVerification.sln` in Visual Studio
2. Visual Studio will automatically restore NuGet packages
3. Build → Build Solution (Ctrl+Shift+B)
4. Run the application (Ctrl+F5)

**Alternative: Command Line Build**
```bash
# Restore packages (if not using Visual Studio)
nuget restore HP8340ACalVerification.sln

# Build with MSBuild
msbuild HP8340ACalVerification.sln /t:Build /p:Configuration=Release

# Run the application
HP8340ACalVerification\bin\Release\HP8340ACalVerification.exe
```

### Dependencies

**NuGet Packages:**
- Spectre.Console 0.54.0 - Console UI framework
- NationalInstruments.Visa 25.5.0.13 - GPIB communication
- IviFoundation.Visa 8.0.2 - IVI Foundation VISA interfaces
- Supporting packages for System.Text.Json, System.Memory, etc.

### Code Review Feedback Addressed

1. **Title Display Refresh** - Display is now refreshed after GPIB address change
2. **Trailing Spaces Fixed** - ToEngineeringFormat no longer adds trailing spaces for empty units
3. **Ternary Operators** - Used where appropriate for cleaner code
4. **Float Comparison** - Using Math.Abs with double.Epsilon instead of == 0
5. **Resource Cleanup** - Added nested try-catch for proper ResourceManager disposal on errors

### HP-BASIC Reference Files

All provided HP-BASIC files have been saved to `docs/hp-basic/` for reference during C# implementation:

| File | Size | Purpose |
|------|------|---------|
| Alc_ad.txt | 60 KB | ALC Adjust and Verify |
| At_cal.txt | 77 KB | Attenuator Calibration |
| Cal_co.txt | 19 KB | Calibration Constants |
| Copy.txt | 25 KB | Software Copy Utility |
| Cw_acc.txt | 31 KB | CW Frequency Accuracy |
| Fre_sw.txt | 22 KB | Frequency Switching Time |
| FS_MANAGER.txt | 109 KB | File System Manager |
| JANITOR.txt | 205 KB | Initialization Routines |
| Max_le.txt | 82 KB | Maximum Level Test |
| Pwracc.txt | 53 KB | Power Accuracy Test |
| Thrash.txt | 7 KB | System Utilities |

### Next Implementation Phases

**Phase 1: Core Infrastructure** ✓
- [x] Project structure
- [x] GPIB connection
- [x] Menu system
- [x] Basic utilities

**Phase 2: GPIB Command Layer** (Next)
- [ ] Create GPIB command wrapper class
- [ ] Implement common HP 8340A/B commands
- [ ] Add command validation and error handling
- [ ] Create instrument state management

**Phase 3: Attenuator Calibration** (Priority)
- [ ] Implement At_cal procedures from HP-BASIC
- [ ] Add flatness data handling
- [ ] Create calibration data storage
- [ ] Add pass/fail criteria checking

**Phase 4: ALC Adjustment**
- [ ] Implement Alc_ad procedures from HP-BASIC
- [ ] Add ADC verification
- [ ] Implement level offset calibration

**Phase 5: Additional Tests**
- [ ] CW Frequency Accuracy (Cw_acc)
- [ ] Frequency Switching Time (Fre_sw)
- [ ] Power Accuracy (Pwracc)
- [ ] Maximum Level (Max_le)

**Phase 6: Calibration Constants**
- [ ] Implement Cal_co utilities
- [ ] Read/write calibration data
- [ ] Compare and validate constants

### Technical Notes

**GPIB Communication Pattern:**
```csharp
// HP-BASIC: OUTPUT @Dut;"IP PL0DB"
// C# with NI-VISA:
gpibSession.FormattedIO.WriteLine("IP PL0DB");

// HP-BASIC: ENTER @Dut;Result
// C# with NI-VISA:
string result = gpibSession.FormattedIO.ReadLine();
```

**Test Mode Mapping:**
- FSC = Field Service Calibration
- FSV = Field Service Verification
- FSP = Field Service Procedure
- EI_ = Engineering Investigation

**Key HP 8340A/B Commands:**
- `IP` - Initialize and Preset
- `CW <freq>` - Set CW frequency
- `PL<level>` - Set power level
- `SHCF <freq>` - Set hop-to frequency
- `SHM1` - Set hop mode

### Known Limitations

1. **Build Environment**: Requires Windows with Visual Studio or MSBuild
2. **Runtime**: Requires .NET Framework 4.7.2 runtime (Windows only)
3. **VISA**: Requires NI-VISA runtime installed and GPIB hardware
4. **Service Manual**: PDF manual (9018-05913.pdf) needs to be analyzed for detailed specifications

### Security

- CodeQL scan: 0 alerts
- No vulnerable dependencies
- Proper resource disposal patterns
- No hardcoded credentials or sensitive data

---

Last Updated: 2026-02-13
Project Status: Phase 1 Complete, Phase 2 Ready to Begin
