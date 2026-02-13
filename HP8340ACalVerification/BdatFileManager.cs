using System;
using System.Collections.Generic;
using System.IO;

namespace HP8340ACalVerification
{
    /// <summary>
    /// Helper class for managing BDAT (Binary Data) files used by HP-BASIC programs.
    /// When BDAT files are accessed, this class logs the requirement and reminds
    /// developers to create GitHub issues for implementation.
    /// </summary>
    public static class BdatFileManager
    {
        private static readonly HashSet<string> AccessedFiles = new HashSet<string>();
        private static readonly object LockObject = new object();

        /// <summary>
        /// Register access to a BDAT file. This will log the access and create
        /// a reminder to implement the file handler.
        /// </summary>
        /// <param name="fileName">Name of the BDAT file</param>
        /// <param name="purpose">Purpose of the file (e.g., "Calibration Constants")</param>
        /// <param name="accessType">Type of access: "READ" or "WRITE"</param>
        public static void RegisterBdatAccess(string fileName, string purpose, string accessType)
        {
            lock (LockObject)
            {
                string key = $"{fileName}:{accessType}";
                
                if (!AccessedFiles.Contains(key))
                {
                    AccessedFiles.Add(key);
                    LogBdatAccess(fileName, purpose, accessType);
                }
            }
        }

        /// <summary>
        /// Check if a BDAT file has been implemented
        /// </summary>
        /// <param name="fileName">Name of the BDAT file</param>
        /// <returns>True if implemented, false if needs GitHub issue</returns>
        public static bool IsBdatImplemented(string fileName)
        {
            // For now, return false for all files
            // As BDAT handlers are implemented, add them to this check
            return false;
        }

        /// <summary>
        /// Get the GitHub issue creation message for a BDAT file
        /// </summary>
        /// <param name="fileName">Name of the BDAT file</param>
        /// <param name="purpose">Purpose of the file</param>
        /// <returns>Formatted message for GitHub issue</returns>
        public static string GetGitHubIssueMessage(string fileName, string purpose)
        {
            return $@"
================================================================================
BDAT FILE ACCESS DETECTED - GitHub Issue Required
================================================================================

File: {fileName}
Purpose: {purpose}

ACTION REQUIRED:
Create a GitHub issue to track implementation of this BDAT file handler.

Issue Title:
  Implement BDAT file handler: {fileName}

Issue Description:
  The HP-BASIC programs use BDAT (Binary Data) file '{fileName}' for {purpose}.
  
  This file needs to be implemented in C# to support the calibration procedures.
  
  Tasks:
  - [ ] Analyze HP-BASIC code to determine data structure
  - [ ] Design C# data model for the file content
  - [ ] Implement read/write methods
  - [ ] Add error handling and validation
  - [ ] Create unit tests
  - [ ] Document file format
  
  Reference: docs/BDAT_FILES.md

================================================================================
";
        }

        /// <summary>
        /// Log BDAT file access and display warning
        /// </summary>
        private static void LogBdatAccess(string fileName, string purpose, string accessType)
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string logMessage = $"[{timestamp}] BDAT {accessType}: {fileName} ({purpose})";
            
            // Log to console
            Console.WriteLine();
            Console.WriteLine("================================================================================");
            Console.WriteLine($"WARNING: BDAT file access detected - {fileName}");
            Console.WriteLine($"Purpose: {purpose}");
            Console.WriteLine($"Access Type: {accessType}");
            Console.WriteLine();
            Console.WriteLine("A GitHub issue should be created to implement this BDAT file handler.");
            Console.WriteLine("See docs/BDAT_FILES.md for more information.");
            Console.WriteLine("================================================================================");
            Console.WriteLine();
            
            // Also log to file for tracking
            try
            {
                string logFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bdat_access.log");
                File.AppendAllText(logFile, logMessage + Environment.NewLine);
            }
            catch
            {
                // Silently fail if we can't write the log file
            }
        }

        /// <summary>
        /// Get a summary of all accessed BDAT files
        /// </summary>
        /// <returns>Summary string</returns>
        public static string GetAccessSummary()
        {
            lock (LockObject)
            {
                if (AccessedFiles.Count == 0)
                {
                    return "No BDAT files have been accessed.";
                }

                var summary = "BDAT Files Accessed:\n";
                summary += "================================================================================\n";
                foreach (var file in AccessedFiles)
                {
                    summary += $"  - {file}\n";
                }
                summary += "================================================================================\n";
                summary += $"Total: {AccessedFiles.Count} file access(es) logged\n";
                summary += "\nReminder: Create GitHub issues for any unimplemented BDAT file handlers.\n";
                summary += "See docs/BDAT_FILES.md for details.\n";
                
                return summary;
            }
        }

        /// <summary>
        /// Clear the access log (for testing purposes)
        /// </summary>
        public static void ClearAccessLog()
        {
            lock (LockObject)
            {
                AccessedFiles.Clear();
            }
        }
    }
}
