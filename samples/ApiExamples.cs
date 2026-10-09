using System;

using Reefact.LuxaforLightingDeviceController;

namespace MyApplication {

    /// <summary>
    ///     The examples of the API reference pages (docs/api*.md). Every method below is the body of one
    ///     snippet; see QuickStart.cs for how the snippets travel from here to the markdown pages.
    /// </summary>
    public static class ApiExamples {

        public static void ListTheConnectedDevices() {
            // begin-snippet: list-devices
            foreach (ILuxaforDevice device in Luxafor.GetDevices()) {
                using (device) {
                    Console.Out.WriteLine(device.Description + " — " + device.Path);
                }
            }
            // end-snippet
        }

        public static void GetADeviceByItsPath(string devicePath) {
            // begin-snippet: get-device-by-path
            try {
                using ILuxaforDevice device = Luxafor.GetDevice(devicePath);

                device.SetColor(BrightColor.Red);
            } catch (LuxaforDeviceNotFoundException exception) {
                Console.Error.WriteLine(exception.DevicePath + " is not a connected Luxafor device.");
            }
            // end-snippet
        }

        public static void CheckTheResultOfACommand(ILuxaforDevice device) {
            // begin-snippet: command-result
            if (!device.SetColor(BrightColor.Red)) {
                Console.Error.WriteLine("The device refused the command: unplugged, or held by another application.");
            }
            // end-snippet
        }

        public static void CheckTheDeviceIsStillConnected(ILuxaforDevice device) {
            // begin-snippet: is-connected
            if (!device.IsConnected) {
                Console.Error.WriteLine(device.Path + " has been unplugged.");
            }
            // end-snippet
        }

        public static void TurnOff(ILuxaforDevice device) {
            // begin-snippet: turn-off
            device.TurnOff();
            device.TurnOff(TargetedLeds.BackSide);
            // end-snippet
        }

        public static void SetAColor(ILuxaforDevice device) {
            // begin-snippet: set-color
            device.SetColor(BrightColor.Green);
            device.SetColor(TargetedLeds.TabSide, BrightColor.Green);
            // end-snippet
        }

        public static void FadeToAColor(ILuxaforDevice device) {
            // begin-snippet: fade-color
            device.FadeColor(BrightColor.Blue, FadeDuration.From(30));
            device.FadeColor(TargetedLeds.BackSide, BrightColor.Blue, FadeDuration.From(30));
            // end-snippet
        }

        public static void Strobe(ILuxaforDevice device) {
            // begin-snippet: strobe
            device.Strobe(BrightColor.Yellow, Speed.FromByte(20), Repeat.Count(3));
            device.Strobe(TargetedLeds.TabSide, BrightColor.Yellow, Speed.FromByte(20), Repeat.Twice);
            // end-snippet
        }

        public static void PlayPatterns(ILuxaforDevice device) {
            // begin-snippet: play-pattern
            device.PlayPattern(WavePattern.Wave_1, BrightColor.Cyan, Speed.FromByte(10), Repeat.Count(5));
            device.PlayPattern(BuiltInPattern.Police, Repeat.Twice);
            // end-snippet
        }

        public static void TargetLeds(ILuxaforDevice device) {
            // begin-snippet: targeted-leds
            device.SetColor(TargetedLeds.All, BrightColor.Black);
            device.SetColor(TargetedLeds.TabSide, BrightColor.Green);   // LEDs n° 1, 2 and 3
            device.SetColor(TargetedLeds.BackSide, BrightColor.Red);    // LEDs n° 4, 5 and 6
            device.SetColor(TargetedLeds.Led_1, BrightColor.Blue);
            device.SetColor(LedIndex.From(6), BrightColor.Blue);
            // end-snippet
        }

        public static void ReuseCommands(ILuxaforDevice device) {
            // begin-snippet: reusable-commands
            LightingCommand alert = LightingCommand.CreateStrobeCommand(TargetedLeds.All, BrightColor.Yellow, Speed.FromByte(20), Repeat.Count(3));
            LightingCommand calm  = LightingCommand.CreateSetColorCommand(BrightColor.Green);

            device.Send(alert);
            device.Send(calm);
            // end-snippet
        }

        public static void BuildColors(ILuxaforDevice device) {
            // begin-snippet: colors
            device.SetColor(BrightColor.Red);                    // Green, Blue, Yellow, Cyan, Magenta, White, Black
            device.SetColor(BrightColor.From("#0F11A8"));        // from an hexadecimal representation
            device.SetColor(BrightColor.From(15, 17, 168));      // from its red, green and blue components
            // end-snippet
        }

    }

}
