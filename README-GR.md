_[English Version](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README.md) - [Version française](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-FR.md) - [Nederlandse versie](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-NL.md) - [Svensk version](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-SE.md) - [Deutsche Version](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-DE.md) - [Versión española](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-ES.md)_

# Ελεγκτής συσκευής Luxafor

Οδηγήστε τους δείκτες διαθεσιμότητας [Luxafor](https://luxafor.com) απευθείας από τις δικές σας εφαρμογές .NET, μιλώντας το πρωτόκολλο USB HID τους: χωρίς διακομιστή Luxafor, χωρίς webhook, χωρίς λογισμικό τρίτων προς εγκατάσταση.

- Ένα άμεσο API: `SetColor`, `FadeColor`, `Strobe`, `PlayPattern`, μέχρι και τον έλεγχο μίας LED τη φορά.
- `.NET Standard 2.0` και `.NET Framework 4.6.2`, με μία μόνο εξάρτηση ([HidLibrary](https://github.com/mikeobrien/HidLibrary)).
- **Μόνο για Windows**: οι συσκευές οδηγούνται μέσω του επιπέδου HID των Windows. Το πακέτο εγκαθίσταται σε οποιαδήποτε πλατφόρμα, αλλά η απαρίθμηση και ο έλεγχος των συσκευών λειτουργούν μόνο στα Windows.

## Εγκατάσταση

```shell
dotnet add package Reefact.LuxaforLightingDeviceController
```

## Γρήγορο ξεκίνημα

<!-- snippet: quick-start -->
```csharp
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
```
<!-- endSnippet -->

Η `Luxafor.GetDevices()` απαριθμεί τις συσκευές Luxafor που είναι συνδεδεμένες στις θύρες USB του μηχανήματος· η απαρίθμηση είναι κενή όταν δεν υπάρχει καμία συνδεδεμένη. Το `ILuxaforDevice` υλοποιεί το `IDisposable`: η δήλωση `using` απελευθερώνει το handle της συσκευής στο τέλος του μπλοκ. Κάθε εντολή επιστρέφει ένα `bool`: `true` όταν η συσκευή δέχτηκε την εντολή, `false` όταν η εγγραφή απέτυχε.

## Συμβατές συσκευές

Η βιβλιοθήκη ελέγχει, μέσω του πρωτοκόλλου USB HID, τις συσκευές Luxafor που προσδιορίζονται από το vendor id `1240` (`0x04D8`) και το product id `62322` (`0xF372`).

| Συσκευή | Κατάσταση |
| --- | --- |
| `Luxafor Orb` | **Δοκιμασμένη**: η συσκευή που χρησιμοποιήθηκε για την ανάπτυξη και την επικύρωση της βιβλιοθήκης (6 διευθυνσιοδοτούμενα LED). |
| `Luxafor Flag` | **Αναμένεται να λειτουργεί, δεν έχει δοκιμαστεί**: ίδια αναγνωριστικά και ίδιο πρωτόκολλο φωτισμού (6 διευθυνσιοδοτούμενα LED). |
| `Luxafor Mute Button`, `Luxafor Colorblind Flag` | **Αναμένεται να λειτουργούν, δεν έχουν δοκιμαστεί**: οι εντολές φωτισμού είναι οι ίδιες· η διάταξη των LED, το πλήθος τους και η απόδοση των χρωμάτων ενδέχεται να διαφέρουν. |
| `Luxafor Bluetooth`, `Luxafor Switch`, `Luxafor Cube`, `Luxafor Pomodoro-Timer`, `Luxafor CO2 Monitor` | **Δεν υποστηρίζονται**: οι συσκευές αυτές δεν οδηγούνται μέσω αυτού του πρωτοκόλλου USB HID. |

Κάθε σχόλιο σχετικά με μια συσκευή που δεν έχει δοκιμαστεί είναι ευπρόσδεκτο: [ανοίξτε ένα issue](https://github.com/Reefact/luxafor-lighting-device-controller/issues).

## Δυνατότητες

- **Σταθερό χρώμα** (`SetColor`) από ένα ονομασμένο χρώμα, έναν δεκαεξαδικό κωδικό ή κόκκινες / πράσινες / μπλε συνιστώσες.
- **Μετάβαση** (`FadeColor`) προς ένα χρώμα, σε επιλεγμένη διάρκεια μετάβασης.
- **Αναβόσβημα** (`Strobe`) με επιλεγμένη ταχύτητα και επιλεγμένο πλήθος επαναλήψεων.
- **Μοτίβα** (`PlayPattern`): χρωματιστά κύματα, ή μοτίβα ενσωματωμένα στη συσκευή (`Police`, `Rainbow`, `TrafficLight`, ...).
- **Σβήσιμο** (`TurnOff`), όλων των LED ή μόνο ενός μέρους τους.
- **Στόχευση LED**: όλες, μία πλευρά (`TabSide`, `BackSide`) ή μία συγκεκριμένη LED (`Led_1` έως `Led_6`).
- **Επαναχρησιμοποιήσιμες εντολές**: το `LightingCommand` περιγράφει μια εντολή μία φορά, ώστε να την ξαναπαίξετε με το `Send`.

## Αναλυτική τεκμηρίωση

- [Αναφορά του API](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/api-GR.md): όλες οι εντολές, οι παράμετροι και τα σφάλματά τους.
- [Luxafor, η εταιρεία και οι συσκευές της](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/luxafor-GR.md): σε τι χρησιμεύουν αυτοί οι δείκτες, και ποιες οδηγεί αυτή η βιβλιοθήκη.
- [Ημερολόγιο αλλαγών](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/CHANGELOG-GR.md) και [οδηγός συνεισφοράς](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/CONTRIBUTING.md).

## Άδεια χρήσης

Αυτή η βιβλιοθήκη διανέμεται υπό την άδεια [Apache-2.0](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/LICENSE).
