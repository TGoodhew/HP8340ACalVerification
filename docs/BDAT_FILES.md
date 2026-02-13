# BDAT File Handling Requirements

## Overview

The HP-BASIC programs use BDAT (Binary Data) files extensively for storing:
- Calibration constants
- Test results  
- Serial number data
- Status information
- Menu configurations

## BDAT File Usage in HP-BASIC

### File Creation Pattern
```basic
CREATE BDAT Path$&File_name$&Msus$,Num_records
ASSIGN @File TO Path$&File_name$&Msus$
OUTPUT @File;Data
ASSIGN @File TO *
```

### Common BDAT Files

1. **Cal_co<SerialNum>** - Calibration Constants
   - Stores calibration coefficients
   - One record per file
   - Created in JANITOR.txt line 5073

2. **SERIAL<SerialNum>** - Serial Number Data
   - Stores serial number and related information
   - Created in JANITOR.txt line 5409

3. **Status Files** - Test Status
   - Stores test execution status
   - Variable number of records based on menu size
   - Created in JANITOR.txt line 4063

4. **Manager System Files** - Manager Data
   - Stores manager system configuration
   - Created in FS_MANAGER.txt lines 2376, 2714

## C# Implementation Strategy

### Option 1: Binary File Format
Replicate the HP-BASIC BDAT format using binary file I/O in C#:
```csharp
using (FileStream fs = new FileStream(path, FileMode.Create))
using (BinaryWriter writer = new BinaryWriter(fs))
{
    // Write calibration data
}
```

### Option 2: Modern Serialization
Use modern serialization formats (JSON, XML) for easier debugging and portability:
```csharp
string json = JsonSerializer.Serialize(calibrationData);
File.WriteAllText(path, json);
```

### Option 3: SQLite Database
Store all calibration data in a single SQLite database for better management:
```csharp
using (SQLiteConnection conn = new SQLiteConnection(connectionString))
{
    // Store calibration data
}
```

## Action Required

When the C# implementation needs to read or write BDAT files, a GitHub issue should be created with:

1. **Title**: "Implement BDAT file: [filename]"
2. **Description**:
   - Purpose of the file
   - Data structure (from HP-BASIC analysis)
   - Read/write requirements
   - Suggested implementation approach

## BDAT Files Identified

| File Pattern | Purpose | HP-BASIC Reference |
|--------------|---------|-------------------|
| Cal_co\<serial\> | Calibration constants | JANITOR.txt:5073 |
| SERIAL\<serial\> | Serial number data | JANITOR.txt:5409 |
| Status files | Test status tracking | JANITOR.txt:4063 |
| \<serial\> | Manager system data | FS_MANAGER.txt:2376 |
| 8485A\<id\> | 8485A sensor data | FS_MANAGER.txt:2714 |

## Implementation Notes

- HP-BASIC BDAT files are block-structured binary files
- Record size is 256 bytes per block
- Files can have multiple records
- Mass storage unit suffix (Msus$) typically ":,700,0" or ":,700,1"
- Directory structure uses Dut_dir$ variable

## Next Steps

1. ✅ Document BDAT file requirements (this file)
2. ⏳ Create mechanism to generate GitHub issues when BDAT files are needed
3. ⏳ Implement BDAT file handler class for each identified file type
4. ⏳ Add file format conversion utilities if needed
