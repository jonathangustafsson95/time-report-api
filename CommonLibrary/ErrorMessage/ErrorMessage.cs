using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLibrary.ErrorMessage
{
    public class ErrorMessage
    {
        public const string MissionDoesNotExistError= "Mission does not exist";
        public const string DatabaseCommunicationError = "An error occured when trying to communicate with the database.";
        public const string InvalidDateTime = "Invalid DateTime";
        public const string InvalidEditingRights = "You can not edit someone elses registries.";
        public const string InvalidDeletingRights = "You can not delete someone elses registries.";
        public const string InvalidCredential = "Invalid credentials...";
    }
}
