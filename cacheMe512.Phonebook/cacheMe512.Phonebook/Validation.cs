using System.Text.RegularExpressions;

namespace cacheMe512.Phonebook;

internal class Validation
{
    internal static bool IsStringValid(string stringInput)
    {
        if (String.IsNullOrEmpty(stringInput))
        {
            return false;
        }

        foreach (char c in stringInput)
        {
            if (!Char.IsLetter(c) && c != '/' && c != ' ')
                return false;
        }

        return true;
    }

    public static bool IsIdValid(string stringInput)
    {

        if (String.IsNullOrEmpty(stringInput))
        {
            return false;
        }

        foreach (char c in stringInput)
        {
            if (!Char.IsDigit(c))
                return false;
        }

        return true;
    }

    /*
     regular expression matches expected email prefix, 
     domain parts, and top-level domain
    */
    public static bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;

        string pattern = @"^[A-Za-z0-9]+(?:[._-][A-Za-z0-9]+)*" +
                         "@" +
                         @"[A-Za-z0-9-]+(?:\.[A-Za-z0-9-]+)*" +
                         @"\.[A-Za-z0-9-]{2,}$";

        return Regex.IsMatch(email, pattern);
    }

    /* 
     The E.164 format:
     1. Must start with a '+'.
     2. The first digit after '+' must be between 1 and 9.
     3. Followed by 1 to 14 additional digits (ensuring the total number of digits is between 1 and 15).
     4. No spaces, dashes, or other punctuation are allowed.
    */
    public static bool IsValidPhoneNumber(string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
            return false;

        string pattern = @"^\+[1-9]\d{1,14}$";

        return Regex.IsMatch(phoneNumber, pattern);
    }
}
