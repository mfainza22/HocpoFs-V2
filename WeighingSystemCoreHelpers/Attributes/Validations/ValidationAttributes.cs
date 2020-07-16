using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.RegularExpressions;
using DataAccessLayer;
using System.Linq;
using WeighingSystemCoreHelpers.Enums;
using System.Collections.Generic;

namespace WeighingSystemCoreHelpers.Attributes.Validations
{
    public static class RegExStrings
    {
        public const string INVALID_YEAR = @"^\d{4}$";
        public const string INVALID_CHAR = @"^[a-zA-Z\sа-яА-ЯёЁ\-\.\,\:\/_\\[0-9]+$";
        public const string PHONE_NUMBER = @"^(?:(?:\(?(?:00|\+)([1-4]\d\d|[1-9]\d?)\)?)?[\-\.\ \\\/]?)?((?:\(?\d{1,}\)?[\-\.\ \\\/]?){0,})(?:[\-\.\ \\\/]?(?:#|ext\.?|extension|x)[\-\.\ \\\/]?(\d+))?$";
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
    public class ExistingValidator : ValidationAttribute
    {
        public ExistingValidator()
        {
            ErrorMessage = string.Empty;
            Existing = true;
        }

        public override bool RequiresValidationContext
        {
            get
            {
                return base.RequiresValidationContext;
            }
        }

        public bool Existing { get; set; }
        public string PropertyTableName { get; set; }

        public string PropertyIdFieldName { get; set; }

        public string PropertyFieldName { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try
            {
                bool isValid;
                object propertyId = 0;
                object propertyValue = validationContext.ObjectType.GetProperty(validationContext.MemberName).GetValue(validationContext.ObjectInstance, null);

                if (propertyValue == null)
                {
                    isValid = true;
                }
                else
                {
                    try
                    {
                        if (Existing)
                        {
                            var objectType = validationContext.ObjectType.GetProperty(PropertyIdFieldName);

                            if (objectType.PropertyType.Name == "String")
                            {
                                propertyId = validationContext.ObjectType.GetProperty(PropertyIdFieldName).GetValue(validationContext.ObjectInstance, null);
                            }
                            else if (objectType.PropertyType.Name == "Int64")
                            {
                                propertyId = Convert.ToInt64(validationContext.ObjectType.GetProperty(PropertyIdFieldName).GetValue(validationContext.ObjectInstance, null));
                            }
                        }

                        if (propertyValue.GetType() == typeof(string))
                        {
                            if (propertyValue == null || String.IsNullOrEmpty(propertyValue.ToString())) return ValidationResult.Success;
                        }
                        else if (propertyValue.GetType() == typeof(long))
                        {
                            if (Convert.ToInt64(propertyValue) == 0) return ValidationResult.Success;
                        }
                        else if (propertyValue.GetType() == typeof(DateTime))
                        {
                           if (Convert.ToDateTime(propertyValue) == null) return ValidationResult.Success;
                            propertyValue = Convert.ToDateTime(propertyValue).ToString("yyyy-MMM-dd hh:mm:ss tt");
                        } else
                        {
                            propertyValue = propertyValue.ToString().Trim();
                        }



                        System.Text.RegularExpressions.Regex mtch;
                        mtch = new System.Text.RegularExpressions.Regex(RegExStrings.INVALID_CHAR);
                        if (mtch.Matches(propertyValue.ToString()).Count == 0) { return ValidationResult.Success; }

                        if (Existing)
                        {
                            StringBuilder strQuery = new StringBuilder();
                            strQuery.AppendLine($"Select count(*) as Existing from {PropertyTableName}");
                            strQuery.AppendLine($"where {validationContext.MemberName} = '{propertyValue}'");
                            strQuery.AppendLine($"and {PropertyIdFieldName} <> '{propertyId}'");
                            long result = DBContext.GetRecordCount(strQuery.ToString());
                            isValid = result == 0 ? true : false;
                        }
                        else
                        {
                            string propertyField = string.IsNullOrEmpty(PropertyFieldName) ? validationContext.MemberName : PropertyFieldName;
                            StringBuilder strQuery = new StringBuilder();
                            strQuery.AppendLine($"Select count(*) as Existing from {PropertyTableName}");
                            strQuery.AppendLine($"where {propertyField} = '{propertyValue}'");
                            long result = DBContext.GetRecordCount(strQuery.ToString());
                            isValid = result == 1;
                        }

                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }

                if (isValid)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    if (ErrorMessage != string.Empty)
                    {
                        return new ValidationResult(ErrorMessage);
                    }
                    else
                    {
                        return new ValidationResult("* " + validationContext.DisplayName + " is invalid");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.GetBaseException().Message);
            }
        }

    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
    public class PhoneNumberValidator : ValidationAttribute
    {
        public PhoneNumberValidator()
        {
            ErrorMessage = string.Empty;
        }

        public override bool RequiresValidationContext
        {
            get
            {
                return base.RequiresValidationContext;
            }
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try
            {
                bool isValid;
                object propertyId = 0;
                object propertyValue = validationContext.ObjectType.GetProperty(validationContext.MemberName).GetValue(validationContext.ObjectInstance, null);

                if (propertyValue == null)
                {
                    isValid = true;
                }
                else
                {
                    try
                    {

                        string input = propertyValue.ToString();

                        if (propertyValue == null || String.IsNullOrEmpty(propertyValue.ToString())) return ValidationResult.Success;
                        var res = Regex.Matches(Regex.Replace(input, @"[^0-9]+", "", RegexOptions.IgnoreCase), @"[0-9]+", RegexOptions.IgnoreCase);

                        if (Regex.Matches(Regex.Replace(input, @"[^0-9]+", "", RegexOptions.IgnoreCase), @"[0-9]+", RegexOptions.IgnoreCase).Count > 0 &&
                            Regex.Matches(Regex.Replace(input, @"[^0-9]+", "", RegexOptions.IgnoreCase), @"[0-9]+", RegexOptions.IgnoreCase)[0].Length < 6)
                        {
                            isValid = false;
                        }
                        else
                        {
                            string pattern = @"^(?:(?:\(?(?:00|\+)([1-4]\d\d|[1-9]\d?)\)?)?[\-\.\ \\\/]?)?((?:\(?\d{1,}\)?[\-\.\ \\\/]?){0,})(?:[\-\.\ \\\/]?(?:#|ext\.?|extension|x)[\-\.\ \\\/]?(\d+))?$";
                            System.Text.RegularExpressions.Regex mtch;
                            mtch = new System.Text.RegularExpressions.Regex(pattern);
                            if (mtch.Matches(propertyValue.ToString()).Count != 0) { isValid = true; } else { isValid = false; }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }

                if (isValid)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    if (ErrorMessage != string.Empty)
                    {
                        return new ValidationResult(ErrorMessage);
                    }
                    else
                    {
                        return new ValidationResult("* " + validationContext.DisplayName + " is invalid");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.GetBaseException().Message);
            }
        }

    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
    public class CustomRange : RangeAttribute
    {
        public CustomRange(double minimum, double maximum) : base(minimum,maximum)
        {
            ErrorMessage = string.Empty;
        }
       
        public string RequiredValueMemberName { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try
            {
                bool isValid = true;
                object propertyId = 0;
                bool required = true;

                if (RequiredValueMemberName != string.Empty) required = Convert.ToBoolean(validationContext.ObjectType.GetProperty(RequiredValueMemberName).GetValue(validationContext.ObjectInstance, null));

                if (value == null)
                {
                    isValid = false;
                }
                else
                {
                    try
                    {
                        if (required == true)
                        {
                            var dblvalue = Convert.ToDouble(value);
                            isValid = dblvalue >= Convert.ToDouble(Minimum) && dblvalue <=Convert.ToDouble(Maximum);
                        }
                        else
                        {
                            isValid = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }

                if (isValid)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    if (ErrorMessage != string.Empty)
                    {
                        return new ValidationResult(ErrorMessage);
                    }
                    else
                    {
                        return new ValidationResult(validationContext.DisplayName + " is invalid");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.GetBaseException().Message);
            }
        }

    }


    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
    public class ToleranceCheck : ValidationAttribute
    {
        public ToleranceCheck()
        {
            ErrorMessage = string.Empty;
            Existing = true;
        }

        public override bool RequiresValidationContext
        {
            get
            {
                return base.RequiresValidationContext;
            }
        }

        public bool Existing { get; set; }
        public string PropertyTolFieldName { get; set; }

        public string PropertyEmptyPackFieldName { get; set; }

        public string PropertyWtPerPackFieldName { get; set; }

        public bool AllowZero { get; set; } 

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try
            {
                bool isValid = true ;
                object propertyId = 0;
                var propertyValue = Convert.ToDecimal(validationContext.ObjectType.GetProperty(validationContext.MemberName).GetValue(validationContext.ObjectInstance, null));
                var tolValue = Convert.ToDecimal(validationContext.ObjectType.GetProperty(PropertyTolFieldName).GetValue(validationContext.ObjectInstance, null));
                var emptyPackWt = Convert.ToDecimal(validationContext.ObjectType.GetProperty(PropertyEmptyPackFieldName).GetValue(validationContext.ObjectInstance, null));
                var wtPerPack = Convert.ToDecimal(validationContext.ObjectType.GetProperty(PropertyWtPerPackFieldName).GetValue(validationContext.ObjectInstance, null));

                var result = CheckWtPerPackageActual(tolValue, propertyValue, emptyPackWt, wtPerPack);

                if (result == Enums.Enums.WeighStatus.NONE.ToString())
                {
                    isValid = AllowZero;
                    if (!isValid) return ValidationResult.Success;
                }
                if (result == Enums.Enums.WeighStatus.FAILED.ToString())
                {
                    isValid = false;
                }

                if (isValid)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    if (ErrorMessage != string.Empty)
                    {
                        return new ValidationResult(ErrorMessage);
                    }
                    else
                    {
                        return new ValidationResult("* " + validationContext.DisplayName + " is invalid");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.GetBaseException().Message);
            }
        }

        public static string CheckWtPerPackageActual(decimal tol, decimal wtPerPackActual, decimal emptyPackWt, decimal wtPerPack)
        {

            var targetPack = wtPerPack + emptyPackWt;
            var minTol = (targetPack - (targetPack) * tol);
            var maxTol = (targetPack + (targetPack) * tol);
            if (wtPerPackActual == 0)
            {
                return Enums.Enums.WeighStatus.NONE.ToString();
            }
            if (wtPerPackActual > maxTol || wtPerPackActual < minTol)
            {
                return Enums.Enums.WeighStatus.FAILED.ToString();
            }
            else
            {
                return Enums.Enums.WeighStatus.PASSED.ToString();
            }
        }

    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
    public class TransactionCustomRequired : ValidationAttribute
    {
        public TransactionCustomRequired()
        {
            ErrorMessage = string.Empty;
        }

        public override bool RequiresValidationContext
        {
            get
            {
                return base.RequiresValidationContext;
            }
        }

        public bool AllowZero { get; set; }

        public string AcceptableTransactionProc { get; set; }

        public string TransactionProcessFieldName { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try
            {
                bool isValid = false;
                object propertyId = 0;
                var propertyValue = Convert.ToString(validationContext.ObjectType.GetProperty(validationContext.MemberName).GetValue(validationContext.ObjectInstance, null));
                var tProcess = Convert.ToString(validationContext.ObjectType.GetProperty(TransactionProcessFieldName).GetValue(validationContext.ObjectInstance, null));

                var acceptableTProc = new List<string>();

                if (!String.IsNullOrEmpty(AcceptableTransactionProc))
                {
                    acceptableTProc.AddRange(AcceptableTransactionProc.Split((char)44));
                }

                isValid = !(propertyValue == string.Empty || (AllowZero ? true : propertyValue == "0"));

                if (isValid == false)
                {
                    isValid = acceptableTProc.Contains(tProcess);
                }

                if (isValid)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    if (ErrorMessage != string.Empty)
                    {
                        return new ValidationResult(ErrorMessage);
                    }
                    else
                    {
                        return new ValidationResult("* " + validationContext.DisplayName + " is invalid");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.GetBaseException().Message);
            }
        }

        public static string CheckWtPerPackageActual(decimal tol, decimal wtPerPackActual, decimal emptyPackWt, decimal wtPerPack)
        {

            var targetPack = wtPerPack + emptyPackWt;
            var minTol = (targetPack - (targetPack) * tol);
            var maxTol = (targetPack + (targetPack) * tol);
            if (wtPerPackActual == 0)
            {
                return Enums.Enums.WeighStatus.NONE.ToString();
            }
            if (wtPerPackActual > maxTol || wtPerPackActual < minTol)
            {
                return Enums.Enums.WeighStatus.FAILED.ToString();
            }
            else
            {
                return Enums.Enums.WeighStatus.PASSED.ToString();
            }
        }

    }


    //[AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
    //public class RequiredOnOut : ValidationAttribute
    //{
    //    public RequiredOnOut()
    //    {

    //    }
    //}
}
