#region Usings declarations

using System.Collections.Generic;

using Value;

#endregion

namespace Reefact.LuxaforLightingDeviceController {

    internal sealed class Rgb : ValueType<Rgb> {

        #region Constructors declarations

        public Rgb(byte red, byte green, byte blue) {
            Red   = red;
            Green = green;
            Blue  = blue;
        }

        #endregion

        public byte Red   { get; }
        public byte Green { get; }
        public byte Blue  { get; }

        /// <inheritdoc />
        protected override IEnumerable<object> GetAllAttributesToBeUsedForEquality() {
            return new object[] { Red, Green, Blue };
        }

    }

}