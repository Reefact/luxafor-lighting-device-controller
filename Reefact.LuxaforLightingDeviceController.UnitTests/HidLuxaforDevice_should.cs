#region Usings declarations

using NFluent;

using Reefact.LuxaforLightingDeviceController.UnitTests.Fakes;

using Xunit;

#endregion

namespace Reefact.LuxaforLightingDeviceController.UnitTests;

public class HidLuxaforDevice_should {

    #region Statics members declarations

    /// <summary>All the ways to ask a device to light up, e.g. all the paths that end up writing to the HID device.</summary>
    public static IEnumerable<object[]> AllCommands() {
        yield return Command("Send", device => device.Send(LightingCommand.CreateSetColorCommand(BrightColor.Red)));
        yield return Command("TurnOff", device => device.TurnOff());
        yield return Command("TurnOff(targetedLeds)", device => device.TurnOff(TargetedLeds.TabSide));
        yield return Command("SetColor", device => device.SetColor(BrightColor.Red));
        yield return Command("SetColor(targetedLeds)", device => device.SetColor(TargetedLeds.Led_1, BrightColor.Red));
        yield return Command("FadeColor", device => device.FadeColor(BrightColor.Red, FadeDuration.From(10)));
        yield return Command("FadeColor(targetedLeds)", device => device.FadeColor(TargetedLeds.All, BrightColor.Red, FadeDuration.From(10)));
        yield return Command("Strobe", device => device.Strobe(BrightColor.Red, Speed.FromByte(10), Repeat.Twice));
        yield return Command("Strobe(targetedLeds)", device => device.Strobe(TargetedLeds.All, BrightColor.Red, Speed.FromByte(10), Repeat.Twice));
        yield return Command("PlayPattern(wave)", device => device.PlayPattern(WavePattern.Wave_1, BrightColor.Red, Speed.FromByte(10), Repeat.Once));
        yield return Command("PlayPattern(builtIn)", device => device.PlayPattern(BuiltInPattern.Police, Repeat.Once));
    }

    private static object[] Command(string name, Func<ILuxaforDevice, bool> sendCommand) {
        return new object[] { name, sendCommand };
    }

    #endregion

    [Fact]
    public void expose_the_path_and_the_description_of_the_underlying_hid_device() {
        // Setup
        FakeHidDeviceHandle handle = FakeHidDeviceHandle.ALuxaforDevice("hid#my-orb");
        handle.Description = "Luxafor Orb";
        using ILuxaforDevice device = new HidLuxaforDevice(handle);
        // Verify
        Check.That(device.Path).IsEqualTo("hid#my-orb");
        Check.That(device.Description).IsEqualTo("Luxafor Orb");
    }

    [Fact]
    public void write_the_command_buffer_to_the_hid_device() {
        // Setup
        FakeHidDeviceHandle handle = FakeHidDeviceHandle.ALuxaforDevice();
        using ILuxaforDevice device  = new HidLuxaforDevice(handle);
        LightingCommand     command = LightingCommand.CreateSetColorCommand(TargetedLeds.TabSide, BrightColor.Red);
        // Exercise
        bool succeeded = device.Send(command);
        // Verify
        Check.That(succeeded).IsTrue();
        Check.That(handle.WrittenBuffers).HasSize(1);
        Check.That(handle.WrittenBuffers[0]).ContainsExactly(0, 1, 65, 255, 0, 0, 0, 0, 0);
    }

    [Theory]
    [MemberData(nameof(AllCommands))]
    public void report_a_success_when_the_hid_device_accepts_the_write(string commandName, Func<ILuxaforDevice, bool> sendCommand) {
        // Setup
        FakeHidDeviceHandle handle = FakeHidDeviceHandle.ALuxaforDevice();
        using ILuxaforDevice device = new HidLuxaforDevice(handle);
        // Exercise
        bool succeeded = sendCommand(device);
        // Verify
        Check.WithCustomMessage($"{commandName} should have reported a success.").That(succeeded).IsTrue();
        Check.That(handle.WrittenBuffers).HasSize(1);
    }

    [Theory]
    [MemberData(nameof(AllCommands))]
    public void propagate_the_failure_when_the_hid_device_refuses_the_write(string commandName, Func<ILuxaforDevice, bool> sendCommand) {
        // Setup
        FakeHidDeviceHandle handle = FakeHidDeviceHandle.AFailingLuxaforDevice();
        using ILuxaforDevice device = new HidLuxaforDevice(handle);
        // Exercise
        bool succeeded = sendCommand(device);
        // Verify
        Check.WithCustomMessage($"{commandName} should have propagated the write failure.").That(succeeded).IsFalse();
        Check.That(handle.WrittenBuffers).HasSize(1);
    }

    [Fact]
    public void refuse_a_null_command() {
        // Setup
        using ILuxaforDevice device = new HidLuxaforDevice(FakeHidDeviceHandle.ALuxaforDevice());
        // Exercise
        Check.ThatCode(() => device.Send(null!))
             .Throws<ArgumentNullException>();
    }

    [Fact]
    public void refuse_to_be_built_without_a_hid_device() {
        Check.ThatCode(() => new HidLuxaforDevice(null!))
             .Throws<ArgumentNullException>();
    }

    [Fact]
    public void dispose_the_underlying_hid_device() {
        // Setup
        FakeHidDeviceHandle handle = FakeHidDeviceHandle.ALuxaforDevice();
        ILuxaforDevice       device = new HidLuxaforDevice(handle);
        // Exercise
        device.Dispose();
        // Verify
        Check.That(handle.DisposeCount).IsEqualTo(1);
    }

    [Fact]
    public void dispose_the_underlying_hid_device_at_the_end_of_a_using_block() {
        // Setup
        FakeHidDeviceHandle handle = FakeHidDeviceHandle.ALuxaforDevice();
        // Exercise
        using (ILuxaforDevice device = new HidLuxaforDevice(handle)) {
            device.SetColor(BrightColor.Green);
            Check.That(handle.DisposeCount).IsEqualTo(0);
        }
        // Verify
        Check.That(handle.DisposeCount).IsEqualTo(1);
    }

    [Fact]
    public void forward_every_dispose_call_to_the_underlying_hid_device() {
        // Setup
        FakeHidDeviceHandle handle = FakeHidDeviceHandle.ALuxaforDevice();
        ILuxaforDevice       device = new HidLuxaforDevice(handle);
        // Exercise
        device.Dispose();
        device.Dispose();
        // Verify
        Check.That(handle.DisposeCount).IsEqualTo(2);
    }

    [Fact]
    public void send_the_expected_buffer_for_each_command_of_the_api() {
        // Setup
        FakeHidDeviceHandle handle = FakeHidDeviceHandle.ALuxaforDevice();
        using ILuxaforDevice device = new HidLuxaforDevice(handle);
        // Exercise
        device.TurnOff();
        device.TurnOff(TargetedLeds.BackSide);
        device.SetColor(BrightColor.Green);
        device.SetColor(TargetedLeds.Led_3, BrightColor.Blue);
        device.FadeColor(BrightColor.Red, FadeDuration.From(50));
        device.FadeColor(TargetedLeds.Led_3, BrightColor.Blue, FadeDuration.From(50));
        device.Strobe(BrightColor.White, Speed.FromByte(25), Repeat.Count(10));
        device.Strobe(TargetedLeds.All, BrightColor.White, Speed.FromByte(25), Repeat.Count(10));
        device.PlayPattern(WavePattern.Wave_3, new BrightColor(200, 0, 200), Speed.FromByte(80), Repeat.Count(5));
        device.PlayPattern(BuiltInPattern.Police, Repeat.Count(33));
        // Verify
        Check.That(handle.WrittenBuffers).HasSize(10);
        Check.That(handle.WrittenBuffers[0]).ContainsExactly(0, 1, 255, 0, 0, 0, 0, 0, 0);
        Check.That(handle.WrittenBuffers[1]).ContainsExactly(0, 1, 66, 0, 0, 0, 0, 0, 0);
        Check.That(handle.WrittenBuffers[2]).ContainsExactly(0, 1, 255, 0, 255, 0, 0, 0, 0);
        Check.That(handle.WrittenBuffers[3]).ContainsExactly(0, 1, 3, 0, 0, 255, 0, 0, 0);
        Check.That(handle.WrittenBuffers[4]).ContainsExactly(0, 2, 255, 255, 0, 0, 50, 0, 0);
        Check.That(handle.WrittenBuffers[5]).ContainsExactly(0, 2, 3, 0, 0, 255, 50, 0, 0);
        Check.That(handle.WrittenBuffers[6]).ContainsExactly(0, 3, 255, 255, 255, 255, 25, 0, 10);
        Check.That(handle.WrittenBuffers[7]).ContainsExactly(0, 3, 255, 255, 255, 255, 25, 0, 10);
        Check.That(handle.WrittenBuffers[8]).ContainsExactly(0, 4, 3, 200, 0, 200, 0, 5, 80);
        Check.That(handle.WrittenBuffers[9]).ContainsExactly(0, 6, 5, 33, 0, 0, 0, 0, 0);
    }

}
