_[Français](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/luxafor.md) - [English](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/luxafor-EN.md) - [Nederlands](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/luxafor-NL.md) - [Svenska](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/luxafor-SE.md) - [Español](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/luxafor-ES.md) - [Ελληνικά](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/luxafor-GR.md)_

[← Zurück zur README](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-DE.md)

# Luxafor, das Unternehmen und seine Geräte

Diese Seite liefert Kontext: Sie beschreibt die Hardware, nicht die Bibliothek. Für die Nutzung der Bibliothek genügen die [README](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-DE.md) und die [API-Referenz](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/api-DE.md).

## Das Unternehmen

[Luxafor](https://luxafor.com) entwickelt und verkauft Produkte für die Büroproduktivität, wie z. B. Verfügbarkeitsanzeigen und Benachrichtigungstools.

Ihr Vorzeigeprodukt ist ein [LED-Verfügbarkeitsindikator](https://luxafor.com/product/flag), der so programmiert werden kann, dass er je nach Verfügbarkeitsstatus des Nutzers unterschiedliche Farben anzeigt.

Das Ziel von Luxafor ist es, Nutzern eine einfache und effektive Möglichkeit zu bieten, Arbeitskollegen ihre Verfügbarkeit zu signalisieren und die Kommunikation und Zusammenarbeit im Unternehmen zu verbessern.

## Das Sortiment

Hier ist eine nicht erschöpfende Liste der [Luxafor-Geräte](https://luxafor.com/products):

- `Luxafor Flag`: eine LED-Anzeige, die die persönliche Verfügbarkeit anzeigt
- `Luxafor Bluetooth`: eine drahtlose, softwaregesteuerte LED-Verfügbarkeitsanzeige, die Benachrichtigungen und die persönliche Verfügbarkeit anzeigt
- `Luxafor Switch`: eine drahtlose, ferngesteuerte Verfügbarkeitsanzeige, die die Verfügbarkeit von Besprechungsräumen und Arbeitsplätzen in Echtzeit anzeigt
- `Luxafor Cube`: eine eigenständige LED-Verfügbarkeitsanzeige, die die Verfügbarkeit von Besprechungsräumen anzeigt
- `Luxafor Pomodoro-Timer`: ein USB-betriebener LED-Timer, der die Arbeit in kleine Zeitfenster aufteilt (siehe [Pomodoro](https://reefact.net/craftsmanship/tools/pomodoro))
- `Luxafor Orb`: eine USB-LED-Weitwinkel-Verfügbarkeitsanzeige
- `Luxafor CO2 Monitor`: ein Sensor, der die Luftqualität in einem Raum analysiert und Sie warnt, wenn Sie den Raum lüften müssen
- `Luxafor Mute Button`: Schalten Sie das Mikrofon mit einem einfachen Druck an/aus und zeigen Sie mit Rot/Grün an, ob Sie verfügbar sind
- `Luxafor Colorblind Flag`: einfarbiges USB-LED-Bereitschafts- und Besetztlicht, das Ablenkungen eliminiert und die Produktivität steigert

## Wie sie gesteuert werden

Diese verschiedenen Geräte sind so konzipiert, dass sie teilweise manuell ('mechanisch'), teilweise halbautomatisch (manuelle Steuerung über [Software](https://luxaformanual.com)) oder automatisch (Integration über [Software](https://luxaformanual.com) in Tools wie Teams, Skype, Cisco, Zappier, oder über Webhook) gesteuert werden können.

Genau diese Lücke schließt diese Bibliothek: die USB-LED-Geräte aus Ihren eigenen Anwendungen heraus zu steuern, ohne den Weg über den Luxafor-Server (Webhook) oder über Fremdsoftware. Sie stützt sich dafür auf [HidLibrary](https://github.com/mikeobrien/HidLibrary), womit sich HID-kompatible USB-Geräte in .NET auflisten und ansprechen lassen.

## Was die Bibliothek steuert

Betroffen sind nur die Geräte, die das USB-HID-Protokoll von Luxafor bereitstellen, erkennbar an der Vendor-ID `1240` (`0x04D8`) und der Produkt-ID `62322` (`0xF372`). Die Kompatibilitätstabelle steht in der [README](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README-DE.md#kompatible-geräte).
