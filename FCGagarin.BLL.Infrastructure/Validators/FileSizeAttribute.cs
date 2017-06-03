using System.ComponentModel.DataAnnotations;
using System.Web;

namespace FCGagarin.BLL.Infrastructure.Validators
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
            return $"Размер файла не должен превышать {_maxSize / (1024 * 1024)} мб";
        }
    }
}