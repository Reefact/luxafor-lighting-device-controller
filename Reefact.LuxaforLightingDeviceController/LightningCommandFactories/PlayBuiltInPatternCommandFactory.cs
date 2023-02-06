#region Usings declarations

using System;
using System.ComponentModel;

#endregion

namespace Reefact.LuxaforLightingDeviceController.LightingCommandFactories {

    internal sealed class PlayBuiltInPatternCommandFactory : LightingCommandFactory {

        #region Fields declarations

        private readonly BuiltInPattern _builtInPattern;
        private readonly Repeat         _repeat;

        #endregion

        #region Constructors declarations

        public PlayBuiltInPatternCommandFactory(BuiltInPattern builtInPattern, Repeat repeat) {
            if (repeat is null) { throw new ArgumentNullException(nameof(repeat)); }
            if (!Enum.IsDefined(typeof(BuiltInPattern), builtInPattern)) { throw new InvalidEnumArgumentException(nameof(builtInPattern), (int)builtInPattern, typeof(BuiltInPattern)); }

            _builtInPattern = builtInPattern;
            _repeat         = repeat;
        }

        #endregion

        /// <inheritdoc />
        public LightingCommand Create() {
            CommandMode mode                 = CommandMode.From(_builtInPattern);
            var         color                = new BrightColor(_repeat.ToByte(), 0, 0);
            string      stringRepresentation = CreateStringRepresentation();
            var         command              = new LightingCommand(CommandCode.ActivateBuiltInPatterns, mode, color, Option.UnUsed, Option.UnUsed, Option.UnUsed, stringRepresentation);

            return command;
        }

        private string CreateStringRepresentation() {
            return $"Play a built-in pattern {_builtInPattern} {_repeat}.";
        }

    }

}