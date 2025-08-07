using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessHealthTracker.Domain
{
    public static class Errors
    {
        public static string UserAlreadyExistsMessage = "User already exists";
        public static string UserDoesntExistsMessage = "User doesn't exist";
        public static string WrongPasswordMessage = "Password is wrong";
        public static string LoginErrorMessage = "Login Error";
        public static string TokenErrorMessage = "Token cannot be created";
        public static string NoDataFoundMessage = "No data found";
        public static string AddingErrorMessage = "Error during adding data";
        public static string DeletingErrorMessage = "Error during deleting data";
        public static string UpdatingErrorMessage = "Error during updating data";

    }
}
