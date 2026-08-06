_[Français](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/api.md) - [English](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/api-EN.md) - [Nederlands](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/api-NL.md) - [Svenska](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/api-SE.md) - [Deutsch](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/api-DE.md) - [Español](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/api-ES.md)_

[← Επιστροφή στο README](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-GR.md)

# Αναφορά του API

Όλα βρίσκονται σε έναν μόνο χώρο ονομάτων, `Reefact.LuxaforLightingDeviceController`. Η στατική κλάση `Luxafor` είναι το σημείο εισόδου: δίνει στιγμιότυπα `ILuxaforDevice`, στα οποία στη συνέχεια στέλνονται εντολές.

Τα παραδείγματα αυτής της σελίδας εξάγονται από τον φάκελο [`samples`](https://github.com/Reefact/luxafor-lighting-device-controller/tree/main/samples), τον οποίο μεταγλωττίζει η CI: δεν μπορούν να ξεφύγουν από τον κώδικα.

## Ανάκτηση μιας συσκευής

| Υπογραφή | Περιγραφή |
| --- | --- |
| `IEnumerable<ILuxaforDevice> Luxafor.GetDevices()` | Όλες οι συσκευές Luxafor που είναι συνδεδεμένες στις θύρες USB. Η απαρίθμηση είναι κενή όταν δεν υπάρχει καμία συνδεδεμένη. |
| `ILuxaforDevice Luxafor.GetDevice(string devicePath)` | Η συσκευή Luxafor που βρίσκεται στη συγκεκριμένη διαδρομή. |

<!-- snippet: list-devices -->
```csharp
foreach (ILuxaforDevice device in Luxafor.GetDevices()) {
    using (device) {
        Console.Out.WriteLine(device.Description + " — " + device.Path);
    }
}
```
<!-- endSnippet -->

Το `ILuxaforDevice` υλοποιεί το `IDisposable`: η δήλωση `using` απελευθερώνει το handle της συσκευής στο τέλος του μπλοκ. Μια συσκευή εκθέτει επίσης τη διαδρομή της (`Path`) και την περιγραφή της (`Description`), όπως τις αναφέρουν τα Windows.

Η `Luxafor.GetDevice` ρίχνει μια `LuxaforDeviceNotFoundException` όταν δεν βρεθεί συσκευή στη διαδρομή που δόθηκε, όταν η συσκευή που βρέθηκε εκεί δεν είναι υποστηριζόμενη συσκευή Luxafor, ή όταν δεν είναι πλέον συνδεδεμένη. Η ιδιότητα `DevicePath` της εξαίρεσης επιστρέφει τη ζητούμενη διαδρομή.

<!-- snippet: get-device-by-path -->
```csharp
try {
    using ILuxaforDevice device = Luxafor.GetDevice(devicePath);

    device.SetColor(BrightColor.Red);
} catch (LuxaforDeviceNotFoundException exception) {
    Console.Error.WriteLine(exception.DevicePath + " is not a connected Luxafor device.");
}
```
<!-- endSnippet -->

## Αποτέλεσμα μιας εντολής

Κάθε εντολή επιστρέφει ένα `bool`: `true` όταν η συσκευή δέχτηκε την εντολή, `false` όταν η εγγραφή απέτυχε (αποσυνδεδεμένη συσκευή, δεσμευμένη από άλλη εφαρμογή, ...). Τα μη έγκυρα ορίσματα, αντιθέτως, ρίχνουν εξαίρεση (`ArgumentNullException`, `ArgumentOutOfRangeException`, `InvalidEnumArgumentException`).

<!-- snippet: command-result -->
```csharp
if (!device.SetColor(BrightColor.Red)) {
    Console.Error.WriteLine("The device refused the command: unplugged, or held by another application.");
}
```
<!-- endSnippet -->

## Σβήσιμο

| Υπογραφή | Περιγραφή |
| --- | --- |
| `bool TurnOff()` | Σβήνει όλα τα LED της συσκευής. |
| `bool TurnOff(TargetedLeds targetedLeds)` | Σβήνει τα στοχευμένα LED της συσκευής. |

<!-- snippet: turn-off -->
```csharp
device.TurnOff();
device.TurnOff(TargetedLeds.BackSide);
```
<!-- endSnippet -->

## Ορισμός χρώματος

| Υπογραφή | Περιγραφή |
| --- | --- |
| `bool SetColor(BrightColor color)` | Ανάβει όλα τα LED της συσκευής σε ένα χρώμα. |
| `bool SetColor(TargetedLeds targetedLeds, BrightColor color)` | Ανάβει τα στοχευμένα LED σε ένα χρώμα. |

<!-- snippet: set-color -->
```csharp
device.SetColor(BrightColor.Green);
device.SetColor(TargetedLeds.TabSide, BrightColor.Green);
```
<!-- endSnippet -->

## Μετάβαση (fade)

| Υπογραφή | Περιγραφή |
| --- | --- |
| `bool FadeColor(BrightColor color, FadeDuration duration)` | Μεταβαίνει όλα τα LED προς ένα χρώμα. |
| `bool FadeColor(TargetedLeds targetedLeds, BrightColor color, FadeDuration duration)` | Μεταβαίνει τα στοχευμένα LED προς ένα χρώμα. |

Η διάρκεια εκφράζεται στη μονάδα της συσκευής: `FadeDuration.From(byte)`, από `0` (ακαριαία) έως `255` (η πιο αργή).

<!-- snippet: fade-color -->
```csharp
device.FadeColor(BrightColor.Blue, FadeDuration.From(30));
device.FadeColor(TargetedLeds.BackSide, BrightColor.Blue, FadeDuration.From(30));
```
<!-- endSnippet -->

## Αναβόσβημα (στροβοσκοπικό εφέ)

| Υπογραφή | Περιγραφή |
| --- | --- |
| `bool Strobe(BrightColor color, Speed speed, Repeat repeat)` | Αναβοσβήνει όλα τα LED σε ένα χρώμα. |
| `bool Strobe(TargetedLeds targetedLeds, BrightColor color, Speed speed, Repeat repeat)` | Αναβοσβήνει τα στοχευμένα LED σε ένα χρώμα. |

Η ταχύτητα εκφράζεται επίσης στη μονάδα της συσκευής: `Speed.FromByte(byte)`. Το πλήθος των επαναλήψεων δηλώνεται με `Repeat.Once`, `Repeat.Twice` ή `Repeat.Count(byte)`.

<!-- snippet: strobe -->
```csharp
device.Strobe(BrightColor.Yellow, Speed.FromByte(20), Repeat.Count(3));
device.Strobe(TargetedLeds.TabSide, BrightColor.Yellow, Speed.FromByte(20), Repeat.Twice);
```
<!-- endSnippet -->

## Κύματα και ενσωματωμένα μοτίβα

| Υπογραφή | Περιγραφή |
| --- | --- |
| `bool PlayPattern(WavePattern wavePattern, BrightColor color, Speed speed, Repeat repeat)` | Ξεκινά ένα χρωματιστό κύμα σε όλα τα LED της συσκευής. |
| `bool PlayPattern(BuiltInPattern pattern, Repeat repeat)` | Ξεκινά ένα μοτίβο ενσωματωμένο στη συσκευή. |

Τα κύματα πάνε από `WavePattern.Wave_1` έως `WavePattern.Wave_5`. Τα ενσωματωμένα μοτίβα είναι `BuiltInPattern.Pattern_1` έως `Pattern_5`, καθώς και `Rainbow`, `TrafficLight`, `Police` και `Off`.

<!-- snippet: play-pattern -->
```csharp
device.PlayPattern(WavePattern.Wave_1, BrightColor.Cyan, Speed.FromByte(10), Repeat.Count(5));
device.PlayPattern(BuiltInPattern.Police, Repeat.Twice);
```
<!-- endSnippet -->

## Στόχευση LED

Το `TargetedLeds` προσδιορίζει τα LED που μια εντολή ανάβει, σβήνει ή εμψυχώνει ταυτόχρονα.

| Τιμή | LED που αφορά |
| --- | --- |
| `TargetedLeds.All` | Και τα έξι LED. |
| `TargetedLeds.TabSide` | Τα LED αρ. 1, 2 και 3. |
| `TargetedLeds.BackSide` | Τα LED αρ. 4, 5 και 6. |
| `TargetedLeds.Led_1` ... `TargetedLeds.Led_6` | Ένα μόνο LED. |

Ένα `LedIndex` (`LedIndex._1` έως `LedIndex._6`, ή `LedIndex.From(byte)`) μετατρέπεται σιωπηρά σε `TargetedLeds`, ώστε να μπορεί να δοθεί ένας δείκτης εκεί όπου αναμένεται στόχος. Οι υπόλοιποι συνδυασμοί δεν εκφράζονται: η συσκευή δεν τους δέχεται. Τότε χρειάζονται διαδοχικές εντολές, με τίμημα ένα ορατό εφέ κυματισμού, αφού το άναμμα γίνεται διαδοχικό.

<!-- snippet: targeted-leds -->
```csharp
device.SetColor(TargetedLeds.All, BrightColor.Black);
device.SetColor(TargetedLeds.TabSide, BrightColor.Green);   // LEDs n° 1, 2 and 3
device.SetColor(TargetedLeds.BackSide, BrightColor.Red);    // LEDs n° 4, 5 and 6
device.SetColor(TargetedLeds.Led_1, BrightColor.Blue);
device.SetColor(LedIndex.From(6), BrightColor.Blue);
```
<!-- endSnippet -->

## Χρώματα

Το `BrightColor` προσφέρει τα χρώματα `Red`, `Green`, `Blue`, `Yellow`, `Cyan`, `Magenta`, `White` και `Black`, ή κατασκευάζεται από μια δεκαεξαδική αναπαράσταση (`#RRGGBB`) ή από τρεις συνιστώσες.

<!-- snippet: colors -->
```csharp
device.SetColor(BrightColor.Red);                    // Green, Blue, Yellow, Cyan, Magenta, White, Black
device.SetColor(BrightColor.From("#0F11A8"));        // from an hexadecimal representation
device.SetColor(BrightColor.From(15, 17, 168));      // from its red, green and blue components
```
<!-- endSnippet -->

Η `BrightColor.From(string)` ρίχνει μια `FormatException` όταν η συμβολοσειρά δεν είναι έγκυρη δεκαεξαδική αναπαράσταση.

## Επαναχρησιμοποιήσιμες εντολές

Ένα `LightingCommand` περιγράφει μια εντολή μία φορά για πάντα, ώστε να την ξαναπαίξετε αργότερα με το `Send`.

| Εργοστάσιο | Ισοδυναμεί με |
| --- | --- |
| `LightingCommand.CreateTurnOffCommand()` | `TurnOff()` |
| `LightingCommand.CreateTurnOffCommand(TargetedLeds)` | `TurnOff(TargetedLeds)` |
| `LightingCommand.CreateSetColorCommand(BrightColor)` | `SetColor(BrightColor)` |
| `LightingCommand.CreateSetColorCommand(TargetedLeds, BrightColor)` | `SetColor(TargetedLeds, BrightColor)` |
| `LightingCommand.CreateFadeColorCommand(TargetedLeds, BrightColor, FadeDuration)` | `FadeColor(TargetedLeds, BrightColor, FadeDuration)` |
| `LightingCommand.CreateStrobeCommand(TargetedLeds, BrightColor, Speed, Repeat)` | `Strobe(TargetedLeds, BrightColor, Speed, Repeat)` |
| `LightingCommand.CreatePlayWavePatternCommand(WavePattern, BrightColor, Speed, Repeat)` | `PlayPattern(WavePattern, BrightColor, Speed, Repeat)` |
| `LightingCommand.CreatePlayBuiltInPatternCommand(BuiltInPattern, Repeat)` | `PlayPattern(BuiltInPattern, Repeat)` |

<!-- snippet: reusable-commands -->
```csharp
LightingCommand alert = LightingCommand.CreateStrobeCommand(TargetedLeds.All, BrightColor.Yellow, Speed.FromByte(20), Repeat.Count(3));
LightingCommand calm  = LightingCommand.CreateSetColorCommand(BrightColor.Green);

device.Send(alert);
device.Send(calm);
```
<!-- endSnippet -->
