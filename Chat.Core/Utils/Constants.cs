namespace Chat.Core.Utils
{
    public class Constants
    {
        /* Rabbit Queues */
        public const string BOT_USERS_QUEUE = "BOT_USERS_QUEUE";
        public const string USER_BOT_QUEUE = "USER_BOT_QUEUE";

        /* Symbolizes language resources */
        public const string ERROR_COMMAND_NOT_FOUND = "Command not found!";
        public const string ERROR_NULL_COMMAND = "Command can't be null!";
        public const string ERROR_NULL_PARAMETER = "Parameter can't be null!";
        public const string ERROR_NULL_PARAMETER_INDICATOR = "Error! I guess you forgot to insert '='";

    }
}