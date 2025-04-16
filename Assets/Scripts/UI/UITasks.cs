// Purpose: Handles various UI related processes
// Directions:
// Other notes:

public static class UITasks
{
    public static string CapitalizeFirstLetter(string textInput)
    {      
        if (textInput.Length != 0)
        {
            string outText = string.Empty;

            for (int i = 0; i < textInput.Length; i++)
            {
                char tempChar = textInput[i];

                if (i == 0)
                {
                    // capitalize
                    outText += char.ToUpper(tempChar);
                }
                else
                {
                    outText += char.ToLower(tempChar);
                }
            }

            return outText;
        }

        return string.Empty;
    }
}
