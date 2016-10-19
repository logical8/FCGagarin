using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FCGagarin.WebUI.Validators
{
    public class FileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxSize;

        public FileSizeAttribute(int maxSize)
        {
            _maxSize = maxSize;
        }

        public override bool IsValid(object value)
        {
            if (value == null) return true;
            return (value as HttpPostedFileBase).ContentLength <= _maxSize;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format("Размер файла не должен превышать {0} мб", _maxSize / (1024 * 1024));
        }
    }
}