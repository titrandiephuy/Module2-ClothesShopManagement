using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace ClothesShop
{
    class LoginService : BaseService, ILoginService
    {
        private string loginFileName = "account.json";
        private AccountList accList = new AccountList();
        public LoginService()
        {
            accList = FileHelper.ReadFile<AccountList>(Path.Combine(path, loginFileName));
        }
        private bool ValidatePassword(string password, out string ErrorMessage)
        {
            var input = password;
            ErrorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(input))
            {
                throw new Exception("Password should not be empty");
            }

            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMiniMaxChars = new Regex(@".{8,15}");
            var hasLowerChar = new Regex(@"[a-z]+");
            var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

            if (!hasLowerChar.IsMatch(input))
            {
                ErrorMessage = "Password should contain At least one lower case letter";
                return false;
            }
            else if (!hasUpperChar.IsMatch(input))
            {
                ErrorMessage = "Password should contain At least one upper case letter";
                return false;
            }
            else if (!hasMiniMaxChars.IsMatch(input))
            {
                ErrorMessage = "Password should not be less than or greater than 12 characters";
                return false;
            }
            else if (!hasNumber.IsMatch(input))
            {
                ErrorMessage = "Password should contain At least one numeric value";
                return false;
            }

            else if (!hasSymbols.IsMatch(input))
            {
                ErrorMessage = "Password should contain At least one special case characters";
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool CreateAccount(string usernames, string password, string role)
        {
            try
            {
                Account acc = new Account();
                acc.usernames = usernames;
                acc.password = password;
                acc.role = role;
                accList.account.Add(acc);
                FileHelper.WriteFile<AccountList>(Path.Combine(path, loginFileName), accList);
                return true;
            }
            catch (System.Exception)
            {

                return false;
            }
        }

        public Account GetAccount(string keyword)
        {
            return accList.account.Where(p => p.usernames.Contains(keyword)).FirstOrDefault();
        }
    }
}