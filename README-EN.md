# Luxafor Device Controller

A .Net library that provides a simple API to control Luxafor devices.

## Luxafor

### Company Overview

[Luxafor](https://luxafor.com) is a company that designs and sells products for office productivity, such as availability indicators and notification tools. 

Their flagship product is an [LED availability indicator](https://luxafor.com/product/flag) that can be programmed to display different colors depending on the user's availability status. 

Luxafor's goal is to provide users with a simple and effective way to signal their availability to co-workers and improve communication and collaboration in the workplace.

### Quick overview of the devices

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

### Integration

These different devices are designed to be driven manually ('mechanical') for some, semi-automatically (manual driving via [software](https://luxaformanual.com)) / automatically (integration via [software](https://luxaformanual.com) to tools like Teams, Skype, Cisco, Zappier or via Webhook) for others. 

## Presentation of the library

This library aims to allow the integration of USB LED devices to your in-house applications without having to go through the Luxafor server (webhook).

It is developed in .Net Core and is based on the library [HidLibrairy](https://github.com/mikeobrien/HidLibrary) which allows to enumerate and communicate with HID compatible USB devices in .NET.

The code below presents an example of basic use of the library for the control of a [Luxafor Orb](https://luxafor.com/product/orb/) device.

```csharp
[Fact]
public void french_sequence() {
    LuxaforDevice orb = Luxafor.GetDevices().First();
    for (var i = 0; i < 3; i++) {
        orb.SetBasicColor(BasicColor.Blue);
        Thread.Sleep(500);
        orb.SetBasicColor(BasicColor.White);
        Thread.Sleep(500);
        orb.SetBasicColor(BasicColor.Red);
        Thread.Sleep(500);
        orb.SetBasicColor(BasicColor.Off);
        Thread.Sleep(1000);
    }
}
```

Line 3 shows how to connect to a single Orb connected to the machine's USB port.

I will quickly go through all the possible commands to send to devices from the `LuxaforDevice`.

### Turn off

```csharp
void TurnOff(); // Turns off all the LEDs of the device
void TurnOff(TargetedLeds targetedLeds); // Turn off the targeted LEDs of the device
```

### Set a single color

```csharp
void SetColor(BrightColor color); // Turns on the device's LEDs in a custom color.
void SetColor(TargetedLeds targetedLeds, BrightColor color); // Turns on the targeted device LEDs in a custom color.
```

### Make a transition (fade)

```csharp
void FadeColor(BrightColor color, FadeDuration duration); // Make a transition from all the LEDs of the device to a custom color
void FadeColor(TargetedLeds targetedLeds, BrightColor color, FadeDuration duration); // Performs a transition from the targeted device LEDs to a custom color
```

### Flashing (strobe effect)

```csharp
void Strobe(BrightColor color, Speed speed, Repeat repeat); // Flashes all the LEDs of the device in a custom color
void Strobe(TargetedLeds targetedLeds, BrightColor color, Speed speed, Repeat repeat); // Flashes the targeted device LEDs in a custom color
```

### Waves / built-in patterns

```csharp
void PlayPattern(WavePattern wavePattern, BrightColor color, Speed speed, Repeat repeat); // Starts a wave pattern that targets all the LEDs of the device based on a custom color
void PlayPattern(BuiltInPattern, Repeat repeat); // Starts an embedded pattern that targets all LEDs on the device
```

### Send a command

It is possible to create custom commands called `LightingCommand` so that they can be reused in the code:

```csharp
var command = LightingCommand.CreateStrobeCommand(TargetedLeds.All, BrightColor.Yellow, Speed.FromByte(20), Repeat.Count(3));
```

The `Send` method allows you to use these commands.

```csharp
void Send(LightingCommand command); // Send a command to the device
```
