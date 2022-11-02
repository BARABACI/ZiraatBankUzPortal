using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZiraatBankUzPortal.Shared.Constants
{
    public static class LocalizationConstants
    {
        public static readonly LanguageCode[] SupportedLanguages = {
            new LanguageCode
            {
                Code = "en-US",
                DisplayName= "English"
            },
            new LanguageCode
            {
                Code = "tr-TR",
                DisplayName = "Türkçe"
            },
            new LanguageCode
            {
                Code = "ru-RU",
                DisplayName = "Русский"
            }
        };
    }
}
