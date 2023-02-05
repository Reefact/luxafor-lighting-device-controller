namespace Reefact.LuxaforDevicesController.UnitTests {

    public class UsageExamples {

        #region Statics members declarations

        private static void EndSequence(LuxaforDevice orb) {
            orb.Strobe(TargetedLeds.All, BrightColor.Yellow, Speed.FromByte(1), Repeat.Twice);
            Thread.Sleep(200);
            orb.Send(MyCommand.SetAllYellow);
            Thread.Sleep(2000);
            orb.SetColor(BrightColor.Black);
        }

        private static void StartSequence(LuxaforDevice orb) {
            orb.SetColor(BrightColor.Black);
            orb.Send(MyCommand.StrobeYellow);
            Thread.Sleep(4000);
        }

        #endregion

        [Fact(Skip = "Connect a Luxafor Orb to the USB port, reactivate this test and verify that the sequence works.")]
        public void use_led_index_as_target() {
            LuxaforDevice orb = Luxafor.GetDevices().First();
            StartSequence(orb);

            var repeat = 10;
            var sleep  = 1000;
            for (var r = 0; r < 4; r++) {
                for (byte i = 1; i <= 6; i++) {
                    LedIndex ledIndex = LedIndex.From(i);
                    orb.Strobe(ledIndex, BrightColor.Red, Speed.FromByte(1), Repeat.Count((byte)repeat));
                    Thread.Sleep(sleep);
                }
                repeat /= 2;
                sleep  /= 2;
            }

            EndSequence(orb);
        }

        [Fact(Skip = "Connect a Luxafor Orb to the USB port, reactivate this test and verify that the sequence works.")]
        public void turn_off_specified_leds() {
            LuxaforDevice orb = Luxafor.GetDevices().First();
            StartSequence(orb);

            orb.SetColor(BrightColor.Red);
            Thread.Sleep(1000);
            orb.TurnOff(TargetedLeds.TabSide);
            Thread.Sleep(1000);
            orb.TurnOff(TargetedLeds.BackSide);
            Thread.Sleep(1000);

            EndSequence(orb);
        }

        [Fact(Skip = "Connect a Luxafor Orb to the USB port, reactivate this test and verify that the sequence works.")]
        public void french_sequence() {
            LuxaforDevice orb = Luxafor.GetDevices().First();
            StartSequence(orb);

            for (var i = 0; i < 3; i++) {
                orb.SetColor(BrightColor.Blue);
                Thread.Sleep(500);
                orb.SetColor(BrightColor.White);
                Thread.Sleep(500);
                orb.SetColor(BrightColor.Red);
                Thread.Sleep(500);
                orb.SetColor(BrightColor.Black);
                Thread.Sleep(1000);
            }

            EndSequence(orb);
        }

        [Fact(Skip = "Connect a Luxafor Orb to the USB port, reactivate this test and verify that the sequence works.")]
        public void create_a_strobe_sequence() {
            LuxaforDevice orb = Luxafor.GetDevices().First();
            StartSequence(orb);

            orb.Strobe(LedIndex._1, BrightColor.Green, Speed.FromByte(10), Repeat.Count(5));
            Thread.Sleep(3500);
            orb.Strobe(LedIndex._2, BrightColor.Green, Speed.FromByte(10), Repeat.Count(5));
            Thread.Sleep(3500);
            orb.Strobe(LedIndex._3, BrightColor.Green, Speed.FromByte(10), Repeat.Count(5));
            Thread.Sleep(3500);
            orb.Strobe(LedIndex._4, BrightColor.Green, Speed.FromByte(10), Repeat.Count(5));
            Thread.Sleep(3500);
            orb.Strobe(LedIndex._5, BrightColor.Green, Speed.FromByte(10), Repeat.Count(5));
            Thread.Sleep(3500);
            orb.Strobe(LedIndex._6, BrightColor.Green, Speed.FromByte(10), Repeat.Count(5));
            Thread.Sleep(3500);
            orb.Strobe(TargetedLeds.TabSide, BrightColor.Green, Speed.FromByte(10), Repeat.Count(5));
            Thread.Sleep(3500);
            orb.Strobe(TargetedLeds.BackSide, BrightColor.Green, Speed.FromByte(10), Repeat.Count(5));
            Thread.Sleep(3500);
            orb.Strobe(TargetedLeds.All, BrightColor.Green, Speed.FromByte(10), Repeat.Count(5));
            Thread.Sleep(3500);

            EndSequence(orb);
        }

        [Fact(Skip = "Connect a Luxafor Orb to the USB port, reactivate this test and verify that the sequence works.")]
        public void run_a_list_of_commands() {
            LuxaforDevice orb = Luxafor.GetDevices().First();

            // Sequence 1
            StartSequence(orb);
            orb.Send(MyCommand.SetAllCyan);
            Thread.Sleep(2000);
            orb.Send(MyCommand.CustomWaveAlpha);
            Thread.Sleep(3000);
            orb.Send(MyCommand.SetAllCyan);
            Thread.Sleep(2000);
            orb.Send(MyCommand.CustomWaveBeta);
            Thread.Sleep(3000);
            EndSequence(orb);

            // Sequence 2
            StartSequence(orb);
            for (var i = 0; i < 6; i++) {
                orb.Send(MyCommand.SetAllCyan);
                Thread.Sleep(250);
                orb.Send(MyCommand.SetAllMagenta);
                Thread.Sleep(250);
            }
            EndSequence(orb);

            // Sequence 3
            StartSequence(orb);
            for (var i = 0; i < 6; i++) {
                orb.Send(MyCommand.SetBackSideMagenta);
                orb.Send(MyCommand.SetTabSideCyan);
                orb.SetColor(TargetedLeds.Led_2, BrightColor.Green);
                Thread.Sleep(250);
                orb.Send(MyCommand.SetBackSideCyan);
                orb.Send(MyCommand.SetTabSideMagenta);
                orb.SetColor(TargetedLeds.Led_5, BrightColor.Green);
                Thread.Sleep(250);
            }
            EndSequence(orb);
        }

        [Fact(Skip = "Connect a Luxafor Orb to the USB port, reactivate this test and verify that the sequence works.")]
        public void set_two_leds_at_once() {
            LuxaforDevice orb = Luxafor.GetDevices().First();
            StartSequence(orb);

            orb.SetColor(BrightColor.Black);
            orb.SetColor(TargetedLeds.Led_1, BrightColor.Blue);
            orb.SetColor(TargetedLeds.Led_4, BrightColor.Blue);
            Thread.Sleep(3000);

            EndSequence(orb);
        }

        [Fact(Skip = "Connect a Luxafor Orb to the USB port, reactivate this test and verify that the sequence works.")]
        public void simulate_simultaneous_custom_targeted_leds() {
            LuxaforDevice orb = Luxafor.GetDevices().First();
            StartSequence(orb);

            for (var i = 0; i < 4; i++) {
                orb.SetColor(TargetedLeds.Led_2, BrightColor.Black);
                orb.SetColor(TargetedLeds.Led_4, BrightColor.Black);
                orb.SetColor(TargetedLeds.Led_6, BrightColor.Black);
                orb.SetColor(TargetedLeds.Led_1, BrightColor.Blue);
                orb.SetColor(TargetedLeds.Led_3, BrightColor.Blue);
                orb.SetColor(TargetedLeds.Led_5, BrightColor.Blue);
                Thread.Sleep(500);
                orb.SetColor(TargetedLeds.Led_1, BrightColor.Black);
                orb.SetColor(TargetedLeds.Led_3, BrightColor.Black);
                orb.SetColor(TargetedLeds.Led_5, BrightColor.Black);
                orb.SetColor(TargetedLeds.Led_2, BrightColor.Blue);
                orb.SetColor(TargetedLeds.Led_4, BrightColor.Blue);
                orb.SetColor(TargetedLeds.Led_6, BrightColor.Blue);
                Thread.Sleep(500);
            }

            EndSequence(orb);
        }

        #region Nested types declarations

        private static class MyCommand {

            #region Statics members declarations

            public static readonly LightningCommand SetAllYellow       = LightningCommand.CreateSetColorCommand(BrightColor.Yellow);
            public static readonly LightningCommand StrobeYellow       = LightningCommand.CreateStrobeCommand(TargetedLeds.All, BrightColor.Yellow, Speed.FromByte(20), Repeat.Count(3));
            public static readonly LightningCommand SetAllMagenta      = LightningCommand.CreateSetColorCommand(BrightColor.Magenta);
            public static readonly LightningCommand SetBackSideMagenta = LightningCommand.CreateSetColorCommand(TargetedLeds.BackSide, BrightColor.Magenta);
            public static readonly LightningCommand SetTabSideMagenta  = LightningCommand.CreateSetColorCommand(TargetedLeds.TabSide, BrightColor.Magenta);
            public static readonly LightningCommand SetAllCyan         = LightningCommand.CreateSetColorCommand(BrightColor.Cyan);
            public static readonly LightningCommand SetBackSideCyan    = LightningCommand.CreateSetColorCommand(TargetedLeds.BackSide, BrightColor.Cyan);
            public static readonly LightningCommand SetTabSideCyan     = LightningCommand.CreateSetColorCommand(TargetedLeds.TabSide, BrightColor.Cyan);
            public static readonly LightningCommand CustomWaveAlpha    = LightningCommand.CreateWaveCommand(WaveType.Wave_1, BrightColor.Magenta, Speed.FromByte(1), Repeat.Count(20));
            public static readonly LightningCommand CustomWaveBeta     = LightningCommand.CreateWaveCommand(WaveType.Wave_4, BrightColor.Cyan, Speed.FromByte(1), Repeat.Count(20));

            #endregion

        }

        #endregion

    }

}