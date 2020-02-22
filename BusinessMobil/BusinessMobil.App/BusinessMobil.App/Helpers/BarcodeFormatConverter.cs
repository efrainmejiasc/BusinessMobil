using System;
using System.Collections.Generic;
using System.Text;
using ZXing;

namespace BusinessMobil.App.Helpers
{
    public static class BarcodeFormatConverter
    {
        public static string ConvertEnumToString(Enum eEnum) => Enum.GetName(eEnum.GetType(), eEnum);
        public static BarcodeFormat ConvertStringToEnum(string value) => (BarcodeFormat)Enum.Parse(typeof(BarcodeFormat), value);
    }
}
