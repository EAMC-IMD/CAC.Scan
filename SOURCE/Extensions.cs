using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAC {
    static class Extensions {
        public static bool TryConvertFromBase32(this char[] code, out ulong? result) {
            int digit = 0;
            result = 0;
            try {
                for (int i = code.Length - 1; i >= 0; i--) {
                    ulong val = (ulong)(int)code[i];
                    if (!UInt64.TryParse(code[i].ToString(), out val)) {
                        val = (ulong)(int)code[i] - 55;
                    }
                    ulong place = (ulong)Math.Pow(32, digit);
                    result += place * val;
                    digit++;
                }
                return true;
            } catch {
                result = null;
                return false;
            }
        }

        public static bool TryConvertToDateTime(this ulong? value, DateTime baseDate, out DateTime? result) {
            if (value is null) {
                result = null;
                return false;
            }
            try {
                result = baseDate.AddDays((double)value);
                return true;
            } catch {
                result = null;
                return false;
            }
        }

        public static bool TryConvertToDateTime(this ulong? value, out DateTime? result) {
            if (value is null) {
                result = null;
                return false;
            }
            DateTime baseDate = new DateTime(1000, 1, 1);
            try {
                result = baseDate.AddDays((double)value);
                return true;
            } catch {
                result = null;
                return false;
            }
        }
    }
}
