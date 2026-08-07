_[Français](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/luxafor.md) - [Nederlands](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/luxafor-NL.md) - [Svenska](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/luxafor-SE.md) - [Deutsch](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/luxafor-DE.md) - [Español](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/luxafor-ES.md) - [Ελληνικά](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/luxafor-GR.md) - [Polski](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/luxafor-PL.md)_

[← Back to the README](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README.md)

# Luxafor, the company and its devices

This page is context: it describes the hardware, not the library. To use the library, the [README](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README.md) and the [API reference](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/docs/api-EN.md) are enough.

## The company

[Luxafor](https://luxafor.com) designs and sells products for office productivity, such as availability indicators and notification tools.

Their flagship product is an [LED availability indicator](https://luxafor.com/product/flag) that can be programmed to display different colors depending on the user's availability status.

Luxafor's goal is to provide users with a simple and effective way to signal their availability to co-workers and improve communication and collaboration in the workplace.

## The catalogue

Here is a non-exhaustive list of [Luxafor devices](https://luxafor.com/products):

- `Luxafor Flag`: an LED availability indicator that displays personal availability
- `Luxafor Bluetooth`: a wireless, software-controlled LED availability indicator that displays notifications and personal availability
- `Luxafor Switch`: a wireless, remote-controlled availability indicator that displays the availability of meeting rooms and workstations in real time
- `Luxafor Cube`: a standalone LED availability indicator that displays meeting room availability
- `Luxafor Pomodoro-Timer`: a USB-powered LED timer that divides work into smaller time slots (see [Pomodoro](https://reefact.net/craftsmanship/tools/pomodoro))
- `Luxafor Orb`: a wide angle USB LED availability indicator
- `Luxafor CO2 Monitor`: a sensor that analyzes the air quality of a room and alerts you when it needs to be ventilated
- `Luxafor Mute Button`: turn on/off the microphone with a single touch and indicate if you are available with the red/green
- `Luxafor Colorblind Flag`: monochrome USB LED availability light eliminates distractions and boosts productivity

## How they are driven

These different devices are designed to be driven manually ("mechanical") for some, semi-automatically (manual driving via [software](https://luxaformanual.com)) or automatically (integration via [software](https://luxaformanual.com) to tools like Teams, Skype, Cisco, Zappier, or via Webhook) for others.

That is exactly the gap this library fills: driving the USB LED devices from your own applications, without going through the Luxafor server (webhook) nor through third-party software. It relies for that on [HidLibrary](https://github.com/mikeobrien/HidLibrary), which makes it possible to enumerate and communicate with HID-compatible USB devices in .NET.

## What the library drives

Only the devices exposing the Luxafor USB HID protocol, identified by the vendor id `1240` (`0x04D8`) and the product id `62322` (`0xF372`), are concerned. The compatibility table is in the [README](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/README.md#compatible-devices).
