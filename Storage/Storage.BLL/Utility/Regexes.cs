namespace Storage.BLL.Utility;

public class Regexes
{
    public const string Password = @"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@$!%*#?&\.,:;])[A-Za-z\d@$!%*#?&\.,:;]+$";
}