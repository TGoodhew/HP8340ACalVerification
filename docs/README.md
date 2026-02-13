# HP 8340A/B Documentation

This directory contains documentation for the HP 8340A/B Calibration Verification application.

## Contents

- `hp-basic/` - Directory for original HP-BASIC reference programs
- Service manual sections (to be added)

## Service Manual Upload Instructions

When you have the HP 8340A/B service manual, please:

1. Upload the complete manual or relevant sections as PDF to this directory
2. Name the file appropriately (e.g., `HP-8340A-Service-Manual.pdf`)
3. Specific sections of interest:
   - Attenuator calibration procedures
   - Operation verification test procedures
   - GPIB command reference
   - Calibration specifications and tolerances

## HP-BASIC Programs

Original HP-BASIC calibration and verification programs should be placed in the `hp-basic/` subdirectory. These will serve as the reference implementation for the .NET version.

## Integration Plan

Once the service manual and HP-BASIC files are provided:

1. **Phase 1: Analysis**
   - Review calibration procedures from manual
   - Analyze HP-BASIC program structure
   - Identify GPIB commands and sequences
   - Document test points and expected values

2. **Phase 2: Implementation**
   - Translate HP-BASIC procedures to C#
   - Implement GPIB command sequences
   - Add appropriate error handling
   - Include progress reporting with Spectre.Console

3. **Phase 3: Testing**
   - Dry-run testing with simulated instrument
   - Live testing with actual HP 8340A/B
   - Validation against manual specifications
   - Documentation of results

## Contributing

If you have additional documentation or resources related to HP 8340A/B calibration, please add them to this directory with appropriate naming and documentation.
