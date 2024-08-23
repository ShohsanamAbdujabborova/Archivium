using System.Text.RegularExpressions;

namespace Archivium.Service.Helpers;

public static class ValidationHelper
{
    public static bool IsPasswordHard(string password)
    {
        if (password.Length < 8) return false;

        if (!password.Any(char.IsUpper)) return false;

        if (!password.Any(char.IsLower)) return false;

        if (!password.Any(char.IsDigit)) return false;

        return true;
    }

    public static bool IsPhoneValid(string phone)
    {
        string pattern = @"^\+998\d{9}$";

        return Regex.IsMatch(phone, pattern);
    }

    public static bool IsEmailValid(string email)
    {
        string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

        return Regex.IsMatch(email, pattern);
    }
}

