namespace LuxaforDevicesController.UnitTests {

    public class UsageExamples {

        #region Statics members declarations

        private static void EndSequence(LuxaforDevice orb) {
            orb.Send(MyCommand.SetAllYellow);
            Thread.Sleep(1000);
        }

        private static void StartSequence(LuxaforDevice orb) {
            orb.Send(MyCommand.StrobeYellow);
            Thread.Sleep(4000);
        }

        #endregion

        [Fact(Skip = "Connect a Luxafor orb to the USB port, reactivate this test and verify that the sequence works.")]
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

        [Fact(Skip = "Connect a Luxafor orb to the USB port, reactivate this test and verify that the sequence works.")]
        public void create_a_strobe_sequence() {
            LuxaforDevice orb = Luxafor.GetDevices().First();
            orb.SetBasicColor(BasicColor.Magenta);
            orb.ActivateStrobe(TargetedLeds.Led_1, new Color(0, 255, 0), Speed.FromByte(10), Repeat.Count(5));
            Thread.Sleep(3500);
            orb.ActivateStrobe(TargetedLeds.Led_2, new Color(0, 255, 0), Speed.FromByte(10), Repeat.Count(5));
            Thread.Sleep(3500);
            orb.ActivateStrobe(TargetedLeds.Led_3, new Color(0, 255, 0), Speed.FromByte(10), Repeat.Count(5));
            Thread.Sleep(3500);
            orb.ActivateStrobe(TargetedLeds.Led_4, new Color(0, 255, 0), Speed.FromByte(10), Repeat.Count(5));
            Thread.Sleep(3500);
            orb.ActivateStrobe(TargetedLeds.Led_5, new Color(0, 255, 0), Speed.FromByte(10), Repeat.Count(5));
            Thread.Sleep(3500);
            orb.ActivateStrobe(TargetedLeds.Led_6, new Color(0, 255, 0), Speed.FromByte(10), Repeat.Count(5));
            Thread.Sleep(3500);
            orb.ActivateStrobe(TargetedLeds.TabSide, new Color(0, 255, 0), Speed.FromByte(10), Repeat.Count(5));
            Thread.Sleep(3500);
            orb.ActivateStrobe(TargetedLeds.BackSide, new Color(0, 255, 0), Speed.FromByte(10), Repeat.Count(5));
            Thread.Sleep(3500);
            orb.ActivateStrobe(TargetedLeds.All, new Color(0, 255, 0), Speed.FromByte(10), Repeat.Count(5));
            Thread.Sleep(3500);
            orb.SetBasicColor(BasicColor.Off);
        }

        [Fact(Skip = "Connect a Luxafor orb to the USB port, reactivate this test and verify that the sequence works.")]
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
                orb.SetColorWithoutFade(TargetedLeds.Led_2, Color.Basic.Green);
                Thread.Sleep(250);
                orb.Send(MyCommand.SetBackSideCyan);
                orb.Send(MyCommand.SetTabSideMagenta);
                orb.SetColorWithoutFade(TargetedLeds.Led_5, Color.Basic.Green);
                Thread.Sleep(250);
            }
            EndSequence(orb);

            // The end !
            orb.Send(MyCommand.Off);
        }

        #region Nested types declarations

        private static class MyCommand {

            #region Statics members declarations

            public static readonly Command SetAllYellow       = Command.CreateSetBasicColorCommand(BasicColor.Yellow);
            public static readonly Command StrobeYellow       = Command.CreateActivateStrobeCommand(TargetedLeds.All, Color.Basic.Yellow, Speed.FromByte(20), Repeat.Count(3));
            public static readonly Command SetAllMagenta      = Command.CreateSetBasicColorCommand(BasicColor.Magenta);
            public static readonly Command SetBackSideMagenta = Command.CreateSetColorWithoutFadeCommand(TargetedLeds.BackSide, Color.Basic.Magenta);
            public static readonly Command SetTabSideMagenta  = Command.CreateSetColorWithoutFadeCommand(TargetedLeds.TabSide, Color.Basic.Magenta);
            public static readonly Command SetAllCyan         = Command.CreateSetBasicColorCommand(BasicColor.Cyan);
            public static readonly Command SetBackSideCyan    = Command.CreateSetColorWithoutFadeCommand(TargetedLeds.BackSide, Color.Basic.Cyan);
            public static readonly Command SetTabSideCyan     = Command.CreateSetColorWithoutFadeCommand(TargetedLeds.TabSide, Color.Basic.Cyan);
            public static readonly Command CustomWaveAlpha    = Command.CreateActivateWaveCommand(WaveType.Wave_1, Color.Basic.Magenta, Speed.FromByte(1), Repeat.Count(20));
            public static readonly Command CustomWaveBeta     = Command.CreateActivateWaveCommand(WaveType.Wave_4, Color.Basic.Cyan, Speed.FromByte(1), Repeat.Count(20));
            public static readonly Command Off                = Command.CreateSetBasicColorCommand(BasicColor.Off);

            #endregion

        }

        #endregion

    }

}