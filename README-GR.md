_[Version française](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README.md) - [English Version](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-EN.md) - [Nederlandse versie](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-NL.md) - [Svensk version](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-SE.md) - [Deutsche Version](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-DE.md) - [Versión española](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-ES.md)_

# Ελεγκτής συσκευής Luxafor

Μια βιβλιοθήκη .Net που παρέχει ένα απλό API για τον έλεγχο των συσκευών Luxafor.

## Luxafor

### Επισκόπηση της εταιρείας

[Luxafor](https://luxafor.com) είναι μια εταιρεία που σχεδιάζει και πωλεί προϊόντα για την παραγωγικότητα του γραφείου, όπως δείκτες διαθεσιμότητας και εργαλεία ειδοποίησης. 

Η ναυαρχίδα τους είναι ένας [δείκτης διαθεσιμότητας LED](https://luxafor.com/product/flag) που μπορεί να προγραμματιστεί ώστε να εμφανίζει διαφορετικά χρώματα ανάλογα με την κατάσταση διαθεσιμότητας του χρήστη. 

Στόχος της Luxafor είναι να παρέχει στους χρήστες έναν απλό και αποτελεσματικό τρόπο για να δηλώνουν τη διαθεσιμότητά τους στους συναδέλφους τους και να βελτιώνουν την επικοινωνία και τη συνεργασία στο χώρο εργασίας.

### Γρήγορη επισκόπηση των συσκευών

Ακολουθεί ένας μη εξαντλητικός κατάλογος [συσκευών Luxafor](https://luxafor.com/products):

- `Luxafor Flag`: μια ένδειξη διαθεσιμότητας LED που εμφανίζει την προσωπική διαθεσιμότητα
- `Luxafor Bluetooth`: μια ασύρματη, ελεγχόμενη από λογισμικό ένδειξη διαθεσιμότητας LED που εμφανίζει ειδοποιήσεις και προσωπική διαθεσιμότητα
- `Luxafor Switch`: ένας ασύρματος, τηλεχειριζόμενος δείκτης διαθεσιμότητας που εμφανίζει τη διαθεσιμότητα των αιθουσών συσκέψεων και των θέσεων εργασίας σε πραγματικό χρόνο.
- `Luxafor Cube`: μια αυτόνομη ένδειξη διαθεσιμότητας LED που εμφανίζει τη διαθεσιμότητα των αιθουσών συνεδριάσεων
- `Luxafor Pomodoro-Timer`: ένας χρονοδιακόπτης LED με τροφοδοσία USB που επιτρέπει τον διαχωρισμό της εργασίας σε μικρότερα χρονικά διαστήματα (βλ. [Pomodoro](https://reefact.net/craftsmanship/tools/pomodoro))
- `Luxafor Orb`: μια ευρυγώνια ένδειξη διαθεσιμότητας LED USB
- `Luxafor CO2 Monitor`: ένας αισθητήρας που αναλύει την ποιότητα του αέρα ενός δωματίου και σας προειδοποιεί όταν χρειάζεται εξαερισμός.
- `Κουμπί σίγασης Luxafor`: ενεργοποιήστε/απενεργοποιήστε το μικρόφωνο με ένα απλό άγγιγμα και δείξτε αν είστε διαθέσιμοι με το κόκκινο/πράσινο χρώμα.
- `Luxafor Colorblind Flag`: μονόχρωμο φως διαθεσιμότητας USB LED εξαλείφει τους περισπασμούς και ενισχύει την παραγωγικότητα

### Ενσωμάτωση

Αυτές οι διαφορετικές συσκευές έχουν σχεδιαστεί για να οδηγούνται χειροκίνητα ("μηχανικά") για ορισμένες, ημιαυτόματα (χειροκίνητη οδήγηση μέσω [λογισμικού](https://luxaformanual.com)) / αυτόματα (ενσωμάτωση μέσω [λογισμικού](https://luxaformanual.com) με εργαλεία όπως Teams, Skype, Cisco, Zappier ή μέσω Webhook) για άλλες. 

## Παρουσίαση της βιβλιοθήκης

Αυτή η βιβλιοθήκη έχει ως στόχο να επιτρέψει την ενσωμάτωση συσκευών LED USB στις εσωτερικές σας εφαρμογές χωρίς να χρειάζεται να περάσετε από τον διακομιστή Luxafor (webhook).

Στοχεύει στα `.NET Standard 2.0` και `.NET Framework 4.6.2` και βασίζεται στη βιβλιοθήκη [HidLibrary](https://github.com/mikeobrien/HidLibrary), η οποία επιτρέπει την απαρίθμηση και την επικοινωνία με συσκευές USB συμβατές με HID στο .NET.

> **Μόνο για Windows.** Οι συσκευές οδηγούνται μέσω του επιπέδου HID των Windows: το πακέτο εγκαθίσταται σε οποιαδήποτε πλατφόρμα, αλλά η απαρίθμηση και ο έλεγχος των συσκευών λειτουργούν μόνο στα Windows.

### Υποστηριζόμενες συσκευές

Η βιβλιοθήκη μιλάει το πρωτόκολλο USB HID των συσκευών Luxafor που προσδιορίζονται από το vendor id `1240` (`0x04D8`) και το product id `62322` (`0xF372`).

| Συσκευή | Κατάσταση |
| --- | --- |
| `Luxafor Orb` | **Δοκιμασμένη**: η συσκευή που χρησιμοποιήθηκε για την ανάπτυξη και την επικύρωση της βιβλιοθήκης (6 διευθυνσιοδοτούμενα LED). |
| `Luxafor Flag` | **Αναμένεται να λειτουργεί, δεν έχει δοκιμαστεί**: ίδια αναγνωριστικά και ίδιο πρωτόκολλο φωτισμού (6 διευθυνσιοδοτούμενα LED). |
| `Luxafor Mute Button`, `Luxafor Colorblind Flag` | **Αναμένεται να λειτουργούν, δεν έχουν δοκιμαστεί**: οι εντολές φωτισμού είναι οι ίδιες· η διάταξη των LED, το πλήθος τους και η απόδοση των χρωμάτων ενδέχεται να διαφέρουν. |
| `Luxafor Bluetooth`, `Luxafor Switch`, `Luxafor Cube`, `Luxafor Pomodoro-Timer`, `Luxafor CO2 Monitor` | **Δεν υποστηρίζονται**: οι συσκευές αυτές δεν οδηγούνται μέσω αυτού του πρωτοκόλλου USB HID. |

Κάθε σχόλιο σχετικά με μια συσκευή που δεν έχει δοκιμαστεί είναι ευπρόσδεκτο: [ανοίξτε ένα issue](https://github.com/Reefact/luxafor-lighting-device-controller/issues).

### Εγκατάσταση

```shell
dotnet add package Reefact.LuxaforLightingDeviceController
```

### Γρήγορο ξεκίνημα

Ο παρακάτω κώδικας δείχνει ένα παράδειγμα βασικής χρήσης της βιβλιοθήκης για την οδήγηση μιας συσκευής [Luxafor Orb](https://luxafor.com/product/orb/).

```csharp
[Fact]
public void french_sequence() {
    using ILuxaforDevice orb = Luxafor.GetDevices().First();
    for (var i = 0; i < 3; i++) {
        orb.SetColor(BrightColor.Blue);
        Thread.Sleep(500);
        orb.SetColor(BrightColor.White);
        Thread.Sleep(500);
        orb.SetColor(BrightColor.Red);
        Thread.Sleep(500);
        orb.TurnOff();
        Thread.Sleep(1000);
    }
}
```

Η γραμμή 3 δείχνει πώς να συνδεθείτε σε ένα μόνο Orb συνδεδεμένο στη θύρα USB του μηχανήματος. Το `ILuxaforDevice` υλοποιεί το `IDisposable`: η δήλωση `using` απελευθερώνει τον χειριστή της συσκευής στο τέλος του μπλοκ.

### Λήψη μιας συσκευής

```csharp
IEnumerable<ILuxaforDevice> GetDevices(); // Όλες οι συσκευές Luxafor που είναι συνδεδεμένες στις θύρες USB (κενό αν δεν υπάρχει καμία)
ILuxaforDevice GetDevice(string devicePath); // Η συσκευή Luxafor που βρίσκεται στη συγκεκριμένη διαδρομή
```

Η `Luxafor.GetDevice` ρίχνει μια `LuxaforDeviceNotFoundException` όταν δεν βρεθεί συσκευή στη διαδρομή που δόθηκε, όταν η συσκευή που βρέθηκε εκεί δεν είναι υποστηριζόμενη συσκευή Luxafor, ή όταν δεν είναι πλέον συνδεδεμένη.

```csharp
using ILuxaforDevice orb = Luxafor.GetDevice(@"\\?\hid#vid_04d8&pid_f372#...");
```

Θα παρουσιάσω γρήγορα όλες τις εντολές που μπορούν να σταλούν στις συσκευές από το `ILuxaforDevice`.

Κάθε εντολή επιστρέφει ένα `bool`: `true` όταν η συσκευή δέχτηκε την εντολή, `false` όταν η εγγραφή απέτυχε (αποσυνδεδεμένη συσκευή, δεσμευμένη από άλλη εφαρμογή, ...). Τα μη έγκυρα ορίσματα ρίχνουν εξαίρεση (`ArgumentNullException`, `ArgumentOutOfRangeException`, `InvalidEnumArgumentException`).

### Απενεργοποίηση

```csharp
bool TurnOff(); // Απενεργοποιεί όλες τις λυχνίες LED της συσκευής
bool TurnOff(TargetedLeds targetedLeds); // Απενεργοποίηση των LED της στοχευμένης συσκευής
```

### Ορίστε ένα μόνο χρώμα

```csharp
bool SetColor(BrightColor color); // Ενεργοποιεί τις λυχνίες LED της συσκευής σε ένα προσαρμοσμένο χρώμα.
bool SetColor(TargetedLeds targetedLeds, BrightColor color); // Ενεργοποιεί τα LED της στοχευμένης συσκευής σε ένα προσαρμοσμένο χρώμα.
```

### Κάντε μια μετάβαση (fade)

```csharp
bool FadeColor(BrightColor color, FadeDuration duration); // Μεταβαίνει όλα τα LED της συσκευής σε ένα προσαρμοσμένο χρώμα.
bool FadeColor(TargetedLeds targetedLeds, BrightColor color, FadeDuration duration); // Μετάβαση των LED της στοχευμένης συσκευής σε ένα προσαρμοσμένο χρώμα
```

### Αναβόσβημα (στροβοσκόπιο)

```csharp
bool Strobe(BrightColor color, Speed speed, Repeat repeat); // Αναβοσβήνει όλα τα LED της συσκευής σε ένα προσαρμοσμένο χρώμα.
bool Strobe(TargetedLeds targetedLeds, BrightColor color, Speed speed, Repeat repeat); // Αναβοσβήνει τα LED της στοχευμένης συσκευής σε ένα προσαρμοσμένο χρώμα.
```

### Κύματα / Ενσωματωμένα μοτίβα

```csharp
bool PlayPattern(WavePattern wavePattern, BrightColor color, Speed speed, Repeat repeat); // Ξεκινάει ένα μοτίβο κύματος που στοχεύει όλα τα LED της συσκευής με βάση ένα προσαρμοσμένο χρώμα.
bool PlayPattern(BuiltInPattern pattern, Repeat repeat); // Εκκίνηση ενός ενσωματωμένου μοτίβου που στοχεύει σε όλες τις λυχνίες LED της συσκευής
```

### Αποστολή εντολής

Είναι δυνατή η δημιουργία προσαρμοσμένων εντολών με την ονομασία `LightingCommand` ώστε να μπορούν να επαναχρησιμοποιηθούν στον κώδικα:

```csharp
var command = LightingCommand.CreateStrobeCommand(TargetedLeds.All, BrightColor.Yellow, Speed.FromByte(20), Repeat.Count(3));
```

Η μέθοδος `Send` σας επιτρέπει να χρησιμοποιήσετε αυτές τις εντολές.

```csharp
bool Send(LightingCommand command); // Αποστολή μιας εντολής στη συσκευή
```

### Χρώματα

```csharp
BrightColor.Red; // καθώς και Green, Blue, Yellow, Cyan, Magenta, White, Black
BrightColor.From("#0F11A8"); // Από τη δεκαεξαδική αναπαράστασή του
BrightColor.From(15, 17, 168); // Από τις κόκκινες, πράσινες και μπλε συνιστώσες του
```

## Μεταγλώττιση της βιβλιοθήκης

```shell
dotnet build -c Release
dotnet test -c Release
dotnet pack Reefact.LuxaforLightingDeviceController -c Release -o artifacts
```

## Άδεια χρήσης

Αυτή η βιβλιοθήκη διανέμεται υπό την άδεια [Apache-2.0](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/LICENSE).
