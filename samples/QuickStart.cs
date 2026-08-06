// The code of this folder is the source of truth for the examples printed in the READMEs and in the
// documentation pages:
//
//   - build/Sync-Snippets.ps1 copies each `begin-snippet` region into the markdown files, and the CI
//     fails when a page no longer matches its snippet;
//   - build/Test-PackageConsumption.ps1 compiles this folder against the produced NuGet package;
//   - the unit tests compile it against the library, on every target framework.
//
// An example that no longer compiles therefore breaks the build instead of misleading a reader.

// begin-snippet: quick-start
using System.Linq;

using Reefact.LuxaforLightingDeviceController;

namespace MyApplication {

    public static class BuildStatusLight {

        public static void Show(bool buildSucceeded) {
            using ILuxaforDevice device = Luxafor.GetDevices().First();

            if (buildSucceeded) {
                device.SetColor(BrightColor.Green);
            } else {
                device.Strobe(BrightColor.Red, Speed.FromByte(20), Repeat.Count(3));
            }
        }

    }

}
// end-snippet
