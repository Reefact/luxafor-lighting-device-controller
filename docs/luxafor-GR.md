_[English](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/luxafor.md) - [Français](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/luxafor-FR.md) - [Nederlands](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/luxafor-NL.md) - [Svenska](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/luxafor-SE.md) - [Deutsch](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/luxafor-DE.md) - [Español](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/luxafor-ES.md) - [Polski](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/luxafor-PL.md)_

[← Επιστροφή στο README](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-GR.md)

# Luxafor, η εταιρεία και οι συσκευές της

Αυτή η σελίδα δίνει το πλαίσιο: περιγράφει το υλικό, όχι τη βιβλιοθήκη. Για τη χρήση της βιβλιοθήκης αρκούν το [README](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-GR.md) και η [αναφορά του API](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/api-GR.md).

## Η εταιρεία

Η [Luxafor](https://luxafor.com) σχεδιάζει και πωλεί προϊόντα για την παραγωγικότητα του γραφείου, όπως δείκτες διαθεσιμότητας και εργαλεία ειδοποίησης.

Η ναυαρχίδα τους είναι ένας [δείκτης διαθεσιμότητας LED](https://luxafor.com/product/flag) που μπορεί να προγραμματιστεί ώστε να εμφανίζει διαφορετικά χρώματα ανάλογα με την κατάσταση διαθεσιμότητας του χρήστη.

Στόχος της Luxafor είναι να παρέχει στους χρήστες έναν απλό και αποτελεσματικό τρόπο για να δηλώνουν τη διαθεσιμότητά τους στους συναδέλφους τους και να βελτιώνουν την επικοινωνία και τη συνεργασία στον χώρο εργασίας.

## Ο κατάλογος

Ακολουθεί ένας μη εξαντλητικός κατάλογος [συσκευών Luxafor](https://luxafor.com/products):

- `Luxafor Flag`: μια ένδειξη διαθεσιμότητας LED που εμφανίζει την προσωπική διαθεσιμότητα
- `Luxafor Bluetooth`: μια ασύρματη, ελεγχόμενη από λογισμικό ένδειξη διαθεσιμότητας LED που εμφανίζει ειδοποιήσεις και προσωπική διαθεσιμότητα
- `Luxafor Switch`: ένας ασύρματος, τηλεχειριζόμενος δείκτης διαθεσιμότητας που εμφανίζει τη διαθεσιμότητα των αιθουσών συσκέψεων και των θέσεων εργασίας σε πραγματικό χρόνο
- `Luxafor Cube`: μια αυτόνομη ένδειξη διαθεσιμότητας LED που εμφανίζει τη διαθεσιμότητα των αιθουσών συνεδριάσεων
- `Luxafor Pomodoro-Timer`: ένας χρονοδιακόπτης LED με τροφοδοσία USB που επιτρέπει τον διαχωρισμό της εργασίας σε μικρότερα χρονικά διαστήματα (βλ. [Pomodoro](https://reefact.net/craftsmanship/tools/pomodoro))
- `Luxafor Orb`: μια ευρυγώνια ένδειξη διαθεσιμότητας LED USB
- `Luxafor CO2 Monitor`: ένας αισθητήρας που αναλύει την ποιότητα του αέρα ενός δωματίου και σας προειδοποιεί όταν χρειάζεται εξαερισμός
- `Luxafor Mute Button`: ενεργοποιήστε/απενεργοποιήστε το μικρόφωνο με ένα απλό άγγιγμα και δείξτε αν είστε διαθέσιμοι με το κόκκινο/πράσινο χρώμα
- `Luxafor Colorblind Flag`: μονόχρωμο φως διαθεσιμότητας USB LED που εξαλείφει τους περισπασμούς και ενισχύει την παραγωγικότητα

## Πώς οδηγούνται

Αυτές οι διαφορετικές συσκευές έχουν σχεδιαστεί για να οδηγούνται χειροκίνητα («μηχανικά») για ορισμένες, ημιαυτόματα (χειροκίνητη οδήγηση μέσω [λογισμικού](https://luxaformanual.com)) ή αυτόματα (ενσωμάτωση μέσω [λογισμικού](https://luxaformanual.com) με εργαλεία όπως Teams, Skype, Cisco, Zappier, ή μέσω Webhook) για άλλες.

Ακριβώς αυτό το κενό καλύπτει η βιβλιοθήκη: οδηγεί τις συσκευές LED USB από τις δικές σας εφαρμογές, χωρίς να περνά από τον διακομιστή Luxafor (webhook) ούτε από λογισμικό τρίτων. Στηρίζεται γι' αυτό στη [HidLibrary](https://github.com/mikeobrien/HidLibrary), η οποία επιτρέπει την απαρίθμηση και την επικοινωνία με συσκευές USB συμβατές με HID στο .NET.

## Τι οδηγεί η βιβλιοθήκη

Αφορά μόνο τις συσκευές που εκθέτουν το πρωτόκολλο USB HID της Luxafor, με vendor id `1240` (`0x04D8`) και product id `62322` (`0xF372`). Ο πίνακας συμβατότητας βρίσκεται στο [README](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-GR.md#συμβατές-συσκευές).
