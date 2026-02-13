using Ivi.Visa;
using NationalInstruments.Visa;
using Spectre.Console;
using System;
using System.Linq;
using System.Threading;

namespace HP8340ACalVerification
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int gpibAddress = 19; // Default GPIB address for HP 8340A/B
            SemaphoreSlim srqWait = new SemaphoreSlim(0, 1);
            NationalInstruments.Visa.ResourceManager? resManager = null;
            GpibSession? gpibSession = null;

            DisplayTitle(gpibAddress);

            // Main menu loop
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Select an option:")
                    .PageSize(10)
                    .AddChoices(new[] { 
                        "Set GPIB Address", 
                        "Connect to HP 8340A/B", 
                        "Query Instrument ID",
                        "Run Attenuator Calibration",
                        "Run Operation Verification",
                        "Exit" 
                    })
            );

            while (choice != "Exit")
            {
                switch (choice)
                {
                    case "Set GPIB Address":
                        SetGPIBAddress(ref gpibAddress);
                        break;

                    case "Connect to HP 8340A/B":
                        ConnectToDevice(gpibAddress, ref srqWait, ref resManager, ref gpibSession);
                        break;

                    case "Query Instrument ID":
                        if (gpibSession == null)
                        {
                            AnsiConsole.MarkupLine("[red]Error: Instrument must be connected first.[/]");
                            Thread.Sleep(1500);
                        }
                        else
                        {
                            QueryInstrumentID(gpibSession);
                        }
                        break;

                    case "Run Attenuator Calibration":
                        if (gpibSession == null)
                        {
                            AnsiConsole.MarkupLine("[red]Error: Instrument must be connected first.[/]");
                            Thread.Sleep(1500);
                        }
                        else
                        {
                            AnsiConsole.MarkupLine("[yellow]Attenuator calibration procedure not yet implemented.[/]");
                            AnsiConsole.MarkupLine("[yellow]This will be based on HP-BASIC procedures from the service manual.[/]");
                            Thread.Sleep(2000);
                        }
                        break;

                    case "Run Operation Verification":
                        if (gpibSession == null)
                        {
                            AnsiConsole.MarkupLine("[red]Error: Instrument must be connected first.[/]");
                            Thread.Sleep(1500);
                        }
                        else
                        {
                            AnsiConsole.MarkupLine("[yellow]Operation verification procedure not yet implemented.[/]");
                            AnsiConsole.MarkupLine("[yellow]This will be based on HP-BASIC procedures from the service manual.[/]");
                            Thread.Sleep(2000);
                        }
                        break;
                }

                choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Select an option:")
                        .PageSize(10)
                        .AddChoices(new[] { 
                            "Set GPIB Address", 
                            "Connect to HP 8340A/B", 
                            "Query Instrument ID",
                            "Run Attenuator Calibration",
                            "Run Operation Verification",
                            "Exit" 
                        })
                );
            }

            // Cleanup
            if (gpibSession != null)
            {
                gpibSession.Dispose();
            }
            if (resManager != null)
            {
                resManager.Dispose();
            }

            AnsiConsole.MarkupLine("[green]Goodbye![/]");
        }

        static void DisplayTitle(int gpibAddress)
        {
            AnsiConsole.Clear();
            AnsiConsole.Write(
                new FigletText("HP 8340A/B")
                    .Centered()
                    .Color(Color.Blue));

            AnsiConsole.Write(
                new Panel("[blue]Attenuator Calibration and Operation Verification[/]")
                    .Header("[yellow]HP 8340A/B Signal Generator[/]")
                    .BorderColor(Color.Blue)
                    .RoundedBorder()
                    .Expand());

            AnsiConsole.MarkupLine($"[dim]Current GPIB Address: {gpibAddress}[/]");
            AnsiConsole.WriteLine();
        }

        static void SetGPIBAddress(ref int gpibAddress)
        {
            gpibAddress = AnsiConsole.Prompt(
                new TextPrompt<int>("Enter the GPIB address (0-30):")
                    .DefaultValue(gpibAddress)
                    .ValidationErrorMessage("[red]Address must be between 0 and 30[/]")
                    .Validate(address =>
                    {
                        return address switch
                        {
                            < 0 => ValidationResult.Error("[red]Address must be 0 or greater[/]"),
                            > 30 => ValidationResult.Error("[red]Address must be 30 or less[/]"),
                            _ => ValidationResult.Success()
                        };
                    }));

            AnsiConsole.MarkupLine($"[green]GPIB address set to {gpibAddress}[/]");
            Thread.Sleep(1000);
        }

        static bool ConnectToDevice(int gpibAddress, ref SemaphoreSlim srqWait, ref NationalInstruments.Visa.ResourceManager? resManager, ref GpibSession? gpibSession)
        {
            // Create local variables for use in lambda
            NationalInstruments.Visa.ResourceManager? localResManager = resManager;
            GpibSession? localSession = null;

            try
            {
                AnsiConsole.Status()
                    .Start($"Connecting to HP 8340A/B at GPIB::{gpibAddress}...", ctx =>
                    {
                        ctx.Spinner(Spinner.Known.Dots);
                        ctx.SpinnerStyle(Style.Parse("green"));

                        // Initialize VISA resource manager
                        if (localResManager == null)
                        {
                            localResManager = new NationalInstruments.Visa.ResourceManager();
                        }

                        // Find resources
                        var resources = localResManager.Find("GPIB?*INSTR");
                        
                        if (resources == null || resources.Count() == 0)
                        {
                            throw new Exception("No GPIB instruments found. Make sure NI-VISA is installed and instruments are connected.");
                        }

                        // Connect to the specific GPIB address
                        string resourceName = $"GPIB0::{gpibAddress}::INSTR";
                        
                        ctx.Status($"Opening session to {resourceName}...");
                        localSession = (GpibSession)localResManager.Open(resourceName);
                        
                        // Configure timeout
                        localSession.TimeoutMilliseconds = 3000;

                        Thread.Sleep(500); // Brief pause for effect
                    });

                // Update ref parameters after lambda
                resManager = localResManager;
                gpibSession = localSession;

                AnsiConsole.MarkupLine($"[green]Successfully connected to HP 8340A/B at GPIB::{gpibAddress}[/]");
                Thread.Sleep(1000);
                return true;
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]Error connecting to instrument: {ex.Message}[/]");
                Thread.Sleep(2000);
                return false;
            }
        }

        static void QueryInstrumentID(GpibSession gpibSession)
        {
            try
            {
                AnsiConsole.Status()
                    .Start("Querying instrument ID...", ctx =>
                    {
                        ctx.Spinner(Spinner.Known.Dots);
                        ctx.SpinnerStyle(Style.Parse("green"));

                        // Send identification query
                        gpibSession.FormattedIO.WriteLine("*IDN?");
                        string response = gpibSession.FormattedIO.ReadLine();

                        AnsiConsole.WriteLine();
                        AnsiConsole.Write(
                            new Panel($"[green]{response}[/]")
                                .Header("[yellow]Instrument Identification[/]")
                                .BorderColor(Color.Blue)
                                .RoundedBorder());
                    });

                Thread.Sleep(2000);
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]Error querying instrument: {ex.Message}[/]");
                AnsiConsole.MarkupLine("[yellow]Note: The HP 8340A/B may not support the *IDN? command.[/]");
                AnsiConsole.MarkupLine("[yellow]Older instruments may require different identification commands.[/]");
                Thread.Sleep(3000);
            }
        }
    }
}
