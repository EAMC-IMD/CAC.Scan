using System;
using System.Collections.Generic;

#nullable enable
namespace CAC {
    public enum BarcodeType {
        Code39,
        PDF417N,
        PDF417M
    }
    public enum PersonDesignatorTypeCode {
        ValidSSN, //S
        InvalidSSN, //N
        PreSSN, //P
        TempIN, //D
        ForeignIN, //F
        Test, //T
        TaxIN //I
    }
    public enum BranchCode {
        Army, //A
        CoastGuard, //C
        DoD, //D
        AirForce, //F
        PublicHealth, //H
        MarineCorps, //M
        Navy, //N
        NOAA, //O
        ForeignArmy, //1
        ForeignNavy, //2
        ForeignMarineCorps, //3
        ForeignAirForce, //4
        Other //X
    }
    public enum PersonCategoryCode {
        ActiveDuty,
        PresidentialAppointee,
        DoDCivilian,
        DisabledVeteran,
        DoDContractor,
        FormerMember,
        NationalGuardActive,
        MedalofHonor,
        OtherCivilian,
        AcademyStudent,
        DoDNAF,
        Lighthouse,
        NGO,
        NationalGuardInactive,
        OtherContract,
        ReserveRetired,
        ActiveRetired,
        ReserveActive,
        ForeignMilitary,
        ForeignNational,
        ReserveInactive,
        Beneficiary,
        CivilianRetired,
        NationalGuard,
        Reserve
    }
    public enum ScanStatus {
        Success,
        Limited,
        UnknownDataFormat,
        UnsupportedCard,
        InvalidScanData,
        NullInput
    }
    public enum TestResult {
        InvalidLength,
        InvalidVersionNumber,
        NullOrEmpty,
        InitiallyValid
    }
    public class Scan {
        //private readonly static Dictionary<char, PersonDesignatorTypeCode> PDTDecode = new Dictionary<char, PersonDesignatorTypeCode>() {
        //    {'S', PersonDesignatorTypeCode.ValidSSN },
        //    {'N', PersonDesignatorTypeCode.InvalidSSN },
        //    {'P', PersonDesignatorTypeCode.PreSSN },
        //    {'D', PersonDesignatorTypeCode.TempIN },
        //    {'F', PersonDesignatorTypeCode.ForeignIN },
        //    {'T', PersonDesignatorTypeCode.Test },
        //    {'I', PersonDesignatorTypeCode.TaxIN }
        //};
        private readonly static Dictionary<char, BranchCode> BranchDecode = new Dictionary<char, BranchCode>() {
            {'A', BranchCode.Army },
            {'C', BranchCode.CoastGuard },
            {'D', BranchCode.DoD },
            {'F', BranchCode.AirForce },
            {'H', BranchCode.PublicHealth },
            {'M', BranchCode.MarineCorps },
            {'N', BranchCode.Navy },
            {'O', BranchCode.NOAA},
            {'1', BranchCode.ForeignArmy },
            {'2', BranchCode.ForeignNavy },
            {'3', BranchCode.ForeignMarineCorps },
            {'4', BranchCode.ForeignAirForce },
            {'X', BranchCode.Other }
        };
        private static readonly Dictionary<char, PersonCategoryCode> PersonnelCCDecode = new Dictionary<char, PersonCategoryCode>() {
            {'A', PersonCategoryCode.ActiveDuty },
            {'B', PersonCategoryCode.PresidentialAppointee },
            {'C', PersonCategoryCode.DoDCivilian },
            {'D', PersonCategoryCode.DisabledVeteran },
            {'E', PersonCategoryCode.DoDContractor },
            {'F', PersonCategoryCode.FormerMember },
            {'H', PersonCategoryCode.MedalofHonor },
            {'I', PersonCategoryCode.OtherCivilian },
            {'J', PersonCategoryCode.AcademyStudent },
            {'K', PersonCategoryCode.DoDNAF },
            {'L', PersonCategoryCode.Lighthouse },
            {'M', PersonCategoryCode.NGO },
            {'N', PersonCategoryCode.NationalGuard },
            {'O', PersonCategoryCode.OtherContract },
            {'Q', PersonCategoryCode.ReserveRetired },
            {'R', PersonCategoryCode.ActiveRetired },
            {'T', PersonCategoryCode.ForeignMilitary },
            {'U', PersonCategoryCode.ForeignNational },
            {'V', PersonCategoryCode.Reserve },
            {'W', PersonCategoryCode.Beneficiary },
            {'Y', PersonCategoryCode.CivilianRetired }
        };
        //private static readonly Dictionary<char, PersonCategoryCode> MemberCCDecode = new Dictionary<char, PersonCategoryCode>() {
        //    {'A', PersonCategoryCode.ActiveDuty },
        //    {'B', PersonCategoryCode.PresidentialAppointee },
        //    {'C', PersonCategoryCode.DoDCivilian },
        //    {'D', PersonCategoryCode.DisabledVeteran },
        //    {'E', PersonCategoryCode.DoDContractor },
        //    {'F', PersonCategoryCode.FormerMember },
        //    {'G', PersonCategoryCode.NationalGuardActive },
        //    {'H', PersonCategoryCode.MedalofHonor },
        //    {'I', PersonCategoryCode.OtherCivilian },
        //    {'J', PersonCategoryCode.AcademyStudent },
        //    {'K', PersonCategoryCode.DoDNAF },
        //    {'L', PersonCategoryCode.Lighthouse },
        //    {'M', PersonCategoryCode.NGO },
        //    {'N', PersonCategoryCode.NationalGuardInactive },
        //    {'O', PersonCategoryCode.OtherContract },
        //    {'Q', PersonCategoryCode.ReserveRetired },
        //    {'R', PersonCategoryCode.ActiveRetired },
        //    {'S', PersonCategoryCode.ReserveActive },
        //    {'T', PersonCategoryCode.ForeignMilitary },
        //    {'U', PersonCategoryCode.ForeignNational },
        //    {'V', PersonCategoryCode.ReserveInactive },
        //    {'W', PersonCategoryCode.Beneficiary },
        //    {'Y', PersonCategoryCode.CivilianRetired }
        //};
        private char? _version;
        //private readonly string? _pdi;
        //private readonly PersonDesignatorTypeCode? _pdtc;
        private string? _edipi;
        private string? _firstname;
        private char? _middleinitial;
        private string? _surname;
        private DateTime? _dob;
        private PersonCategoryCode? _pcc;
        private BranchCode? _branch;
        //private Int16? _pect;
        private string? _rank;
        private string? _payplan;
        private short? _grade;
        private DateTime? _issue;
        private DateTime? _exp;
        private char? _instance;
        private readonly BarcodeType? _type;
        private readonly ScanStatus _scanStatus;

        public char? Version => _version;
        //public string? Pdi => _pdi;
        //public PersonDesignatorTypeCode? PersonDesignatorType => _pdtc;
        public string? Edipi => _edipi;
        public string? FirstName => _firstname;
        public char? MiddleInitial => _middleinitial;
        public string? Surname => _surname;
        public DateTime? DateofBirth => _dob;
        public PersonCategoryCode? PersonCategory => _pcc;
        public BranchCode? Branch => _branch;
        public string? Rank => _rank;
        public string? PayPlan => _payplan;
        public short? Grade => _grade;
        public DateTime? IssueDate => _issue;
        public DateTime? ExpDate => _exp;
        public char? Instance => _instance;
        public BarcodeType? Barcode => _type;
        public ScanStatus ScanResult => _scanStatus;
        public static TestResult TestScan(string scan) {
            if (String.IsNullOrEmpty(scan)) {
                return TestResult.NullOrEmpty;
            }
            switch (scan.Length) {
                case int n when (n < 18):
                    return TestResult.InvalidLength;
                case int n when (n == 18):
                    if (scan.Substring(0, 1) == "1") {
                        return TestResult.InitiallyValid;
                    } else {
                        return TestResult.InvalidVersionNumber;
                    }
                case int n when (n > 18 && n < 88):
                    return TestResult.InvalidLength;
                case int n when (n == 88 || n == 89):
                    if (scan.Substring(0, 1) == "1" || scan.Substring(0, 1) == "N") {
                        return TestResult.InitiallyValid;
                    } else {
                        return TestResult.InvalidVersionNumber;
                    }
                case int n when (n > 89 && n < 99):
                    return TestResult.InvalidLength;
                case int n when (n == 99):
                    if (scan.Substring(0, 1) == "M") {
                        return TestResult.InitiallyValid;
                    } else {
                        return TestResult.InvalidVersionNumber;
                    }
                default:
                    return TestResult.InvalidLength;
            }
        }

        private string InvertCase(string input) {
            char[] chars = input.ToCharArray();
            for (int i = 0; i < chars.Length; i++) {
                if (Char.IsLetter(chars[i])) {
                    if (Char.IsUpper(chars[i])) {
                        chars[i] = Char.ToLower(chars[i]);
                    } else {
                        chars[i] = Char.ToUpper(chars[i]);
                    }
                }
            }
            return new string(chars);
        }

        public Scan(string scan, bool limited = false) {
            if (String.IsNullOrEmpty(scan)) {
                _scanStatus = ScanStatus.NullInput;
                return;
            }
            scan = scan.Trim();
            if (Char.IsLower(scan.ToCharArray()[0])) {
                scan = InvertCase(scan);
            }
            switch (scan.Length) {
                case int n when (n < 18):
                    _scanStatus = ScanStatus.UnknownDataFormat;
                    return;
                case int n when (n == 18):
                    _type = BarcodeType.Code39;
                    break;
                case int n when (n > 18 && n < 88):
                    _scanStatus = ScanStatus.UnknownDataFormat;
                    return;
                case int n when (n == 88 || n == 89):
                    _type = BarcodeType.PDF417N;
                    break;
                case int n when (n > 89 && n < 99):
                    _scanStatus = ScanStatus.UnknownDataFormat;
                    return;
                case int n when (n == 99):
                    _type = BarcodeType.PDF417M;
                    break;
                case int n when (n > 99):
                    _scanStatus = ScanStatus.UnknownDataFormat;
                    return;
            }
            if (scan.Substring(0, 1) == "%" || scan.Substring(0, 4) == "IDUS") {
                _scanStatus = ScanStatus.UnsupportedCard;
                return;
            }
            if (_type == BarcodeType.Code39 && !ParseCode39(scan.ToCharArray())) {
                _scanStatus = ScanStatus.InvalidScanData;
                return;
            }
            if (_type == BarcodeType.PDF417N && !ParsePDF417N(scan.ToCharArray())) {
                _scanStatus = ScanStatus.InvalidScanData;
                if (limited && _firstname != null) _scanStatus = ScanStatus.Limited;
                return;
            }
            if (_type == BarcodeType.PDF417M && !ParsePDF417M(scan.ToCharArray())) {
                _scanStatus = ScanStatus.InvalidScanData;
                if (limited && _firstname != null) _scanStatus = ScanStatus.Limited;
                return;
            }
            _scanStatus = ScanStatus.Success;
        }
        private bool ParseCode39(char[] scan) {
            if (scan[0] != '1' && scan[0] != '4') {
                return false;
            }
            char version = scan[0];
            //char[]? pdi;
            //char? pdtc;
            char[] edipi;
            char pcc;
            char branch;
            char instance;
            PersonCategoryCode personCategoryCode;
            BranchCode branchCode;
            bool succeed;
            if (version == '1') {
                succeed = true;
                //pdi = scan[1..6];
                //pdtc = scan[7];
                edipi = scan[8..14];
                pcc = scan[15];
                branch = scan[16];
                instance = scan[17];
                _version = version;
                //succeed &= PDTDecode.TryGetValue((char)pdtc, out _pdtc);
                succeed &= edipi.TryConvertFromBase32(out ulong? dodid);
                succeed &= PersonnelCCDecode.TryGetValue(pcc, out personCategoryCode);
                succeed &= BranchDecode.TryGetValue(branch, out branchCode);
                _instance = instance;
                if (succeed) {
                    _edipi = dodid.ToString();
                    _pcc = personCategoryCode;
                    _branch = branchCode;
                    //pdi.TryConvertFromBase32(out ulong? pdidigits);
                    //_pdi = pdidigits.ToString();
                }
            } else {
                succeed = true;
                edipi = scan[1..17];
                pcc = scan[15];
                branch = scan[16];
                instance = scan[17];
                _version = version;
                succeed &= edipi.TryConvertFromBase32(out ulong? dodid);
                succeed &= PersonnelCCDecode.TryGetValue(pcc, out personCategoryCode);
                succeed &= BranchDecode.TryGetValue(branch, out branchCode);
                _instance = instance;
                if (succeed) {
                    _edipi = dodid.ToString();
                    _pcc = personCategoryCode;
                    _branch = branchCode;
                }
            }
            return succeed;
        }
        private bool ParsePDF417N(char[] scan, bool limited = false) {
            bool succeed = true;
            bool limitedsucceed = false;
            if (scan[0] != 'N' && scan[0] != '1') {
                return false;
            }
            char version = scan[0];
            //char[] pdi = scan[1..6];
            //char pdtc = scan[7];
            char[] edipi = scan[8..14];
            char[] firstname = scan[15..34];
            char[] lastname = scan[35..60];
            char[] dob = scan[61..64];
            char pcc = scan[65];
            char branch = scan[66];
            //char[] pect = scan[67..68];
            char[] rank = scan[69..74];
            char[] payplan = scan[75..76];
            char[] paygrade = scan[77..78];
            char[] issue = scan[79..82];
            char[] exp = scan[83..86];
            char instance = scan[87];
            char? middleinitial = null;
            if (version != '1') {
                middleinitial = scan[88];
            }
            _version = version;
            //succeed &= PDTDecode.TryGetValue(pdtc, out _pdtc);
            succeed &= edipi.TryConvertFromBase32(out ulong? dodid);
            if (succeed)
                limitedsucceed = true;
            succeed &= dob.TryConvertFromBase32(out ulong? dobdays);
            succeed &= dobdays.TryConvertToDateTime(out _dob);
            succeed &= PersonnelCCDecode.TryGetValue(pcc, out PersonCategoryCode personCategoryCode);
            succeed &= BranchDecode.TryGetValue(branch, out BranchCode branchCode);
            if (!short.TryParse(new string(paygrade).Trim(), out short gradeout))
                gradeout = 0;
            succeed &= issue.TryConvertFromBase32(out ulong? issuedays);
            succeed &= issuedays.TryConvertToDateTime(out _issue);
            succeed &= exp.TryConvertFromBase32(out ulong? expdays);
            succeed &= expdays.TryConvertToDateTime(out _exp);
            if (limitedsucceed) {
                _edipi = dodid.ToString();
                _firstname = new string(firstname).Trim();
                _surname = new string(lastname).Trim();
            }
            if (succeed) {
                //pdi.TryConvertFromBase32(out ulong? pdidigits);
                //_pdi = pdidigits.ToString();
                _rank = new string(rank).Trim();
                _payplan = new string(payplan).Trim();
                _grade = gradeout;
                _pcc = personCategoryCode;
                _branch = branchCode;
                if (!String.IsNullOrEmpty(middleinitial.ToString()))
                    _middleinitial = middleinitial;
                _instance = instance;
            }
            return succeed;
        }
        private bool ParsePDF417M(char[] scan, bool limited = false) {
            bool succeed = true;
            bool limitedsucceed = false;
            if (scan[0] != 'M') {
                return false;
            }
            char version = scan[0];
            char[] edipi = scan[1..8];
            char[] firstname = scan[16..36];
            char? middleinitial = scan[36];
            char[] lastname = scan[37..63];
            char[] dob = scan[63..67];
            //char[] citiz = scan[67..98];
            char pcc = scan[70];
            char branch = scan[71];
            //char[] pect = scan[67..68];
            char[] rank = scan[74..80];
            char[] payplan = scan[80..82];
            char[] paygrade = scan[82..84];
            char[] issue = scan[85..89];
            char[] exp = scan[89..93];
            char instance = scan[98];
            _version = version;
            succeed &= edipi.TryConvertFromBase32(out ulong? dodid);
            if (succeed)
                limitedsucceed = true;
            succeed &= dob.TryConvertFromBase32(out ulong? dobdays);
            succeed &= dobdays.TryConvertToDateTime(out _dob);
            succeed &= PersonnelCCDecode.TryGetValue(pcc, out PersonCategoryCode personCategoryCode);
            succeed &= BranchDecode.TryGetValue(branch, out BranchCode branchCode);
            if (!short.TryParse(new string(paygrade).Trim(), out short gradeout))
                gradeout = 0;
            succeed &= issue.TryConvertFromBase32(out ulong? issuedays);
            succeed &= issuedays.TryConvertToDateTime(out _issue);
            succeed &= exp.TryConvertFromBase32(out ulong? expdays);
            succeed &= expdays.TryConvertToDateTime(out _exp);
            if (limitedsucceed) {
                _edipi = dodid.ToString();
                _firstname = new string(firstname).Trim();
                _middleinitial = middleinitial;
                _surname = new string(lastname).Trim();
            }
            if (succeed) {
                _pcc = personCategoryCode;
                _branch = branchCode;
                _rank = new string(rank).Trim();
                _payplan = new string(payplan).Trim();
                _grade = gradeout;
                _instance = instance;
            }
            return succeed;
        }
    }
}
