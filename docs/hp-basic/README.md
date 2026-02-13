# HP-BASIC Reference Files

This directory contains the original HP-BASIC calibration and verification programs for reference.

## Files Provided

The following HP-BASIC program files are included:

1. **Alc_ad.txt** (60 KB) - ALC (Automatic Level Control) Adjust and Verify
   - Part #: 08340-90059
   - ALC adjustment and verification procedures
   - ADC (Analog-to-Digital Converter) testing

2. **At_cal.txt** (77 KB) - Attenuator Calibration and Verification  
   - Part #: 08340-90088
   - Attenuator calibration for various dB steps
   - Flatness data handling
   - Setup and verification procedures

3. **Cal_co.txt** (19 KB) - Calibration Constants Utilities
   - Part #: 08340-90088
   - Reading and writing calibration constants
   - Comparing DUT constants to image files
   - Display and print utilities

4. **Copy.txt** (25 KB) - Test Software Copy Program
   - Disc copying utilities
   - File management for test software distribution

5. **Cw_acc.txt** (31 KB) - CW Frequency Accuracy Test
   - M/N loop divider tests
   - 20/30 loop divider tests
   - Frequency endpoint accuracy verification

6. **Fre_sw.txt** (22 KB) - Frequency Switching Time Test
   - Part #: 08340-90065
   - Measures frequency switching speed
   - NEG BLANK timing measurements

7. **FS_MANAGER.txt** (109 KB) - File System Manager
   - MANAGER test program interface
   - Test selection and execution
   - Data management

8. **JANITOR.txt** (205 KB) - Cleanup and Initialization Routines
   - Instrument setup and initialization
   - Common utility functions
   - GPIB communication helpers
   - Error handling

9. **Max_le.txt** (82 KB) - Maximum Level Test
   - Output power verification
   - Level accuracy measurements

10. **Pwracc.txt** (53 KB) - Power Accuracy Test
    - Power accuracy verification procedures
    - Flatness corrections

11. **Thrash.txt** (7 KB) - System Test Utilities
    - Utility functions for testing

## Purpose

These files serve as reference implementations for creating the C# version using modern .NET Framework 4.7.2 and NI-VISA libraries. The HP-BASIC programs provide:

- Test procedures and sequences
- GPIB command syntax for HP 8340A/B
- Calibration algorithms
- Data storage formats
- Test specifications and tolerances

## Implementation Strategy

1. **Phase 1: Core GPIB Communication**
   - Map HP-BASIC GPIB commands to NI-VISA C# equivalents
   - Implement device initialization sequences
   - Create command wrappers

2. **Phase 2: Attenuator Calibration (At_cal.txt)**
   - Implement attenuator calibration procedures
   - Add flatness data handling
   - Create calibration data storage

3. **Phase 3: ALC Adjustment (Alc_ad.txt)**
   - Implement ALC calibration
   - Add ADC verification
   - Include level offset DAC calibration

4. **Phase 4: Additional Tests**
   - CW Frequency Accuracy
   - Frequency Switching Time
   - Power Accuracy
   - Maximum Level

5. **Phase 5: Calibration Constants (Cal_co.txt)**
   - Read/write calibration data
   - Compare and validate constants
   - Display utilities

## Key Observations from HP-BASIC Files

### Common Patterns
- Use of HPIB (GPIB) addresses via COM blocks
- Subroutine-based architecture
- Extensive error handling and user prompts
- Data storage using OUTPUT @Disc commands
- Spec-based pass/fail criteria with margins

### GPIB Commands Used
- `IP` - Initialize and preset
- `CW` - Set CW frequency
- `PL` - Set power level
- Various custom commands for calibration

### Test Modes
- `FSC` - Field Service Calibration
- `FSV` - Field Service Verification  
- `FSP` - Field Service Procedure
- `EI_` - Engineering Investigation

## Next Steps

1. Analyze the service manual (9018-05913.pdf) for detailed specifications
2. Map HP-BASIC SUBs to C# classes/methods
3. Identify required test equipment and setup
4. Implement core calibration procedures
5. Create data storage format compatible with or improved from original
6. Add comprehensive logging and reporting
