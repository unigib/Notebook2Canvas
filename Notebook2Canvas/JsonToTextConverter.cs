using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Windows.Forms.VisualStyles;

public class JsonToTextConverter
{
    /// <summary>
    /// Converts a JSON file into a plain text file by extracting all string values.
    /// </summary>
    public StringBuilder Convert(string jsonFilePath)
    {
        if (!File.Exists(jsonFilePath))
            throw new FileNotFoundException("JSON file not found.", jsonFilePath);

        string jsonContent = File.ReadAllText(jsonFilePath);

        JsonNode root = JsonNode.Parse(jsonContent);
        StringBuilder sb = new StringBuilder();
        StringBuilder sbQuestions = new StringBuilder();

        //ExtractText(root, sb);

        ExtractQuestion(root, sbQuestions);
              

        return sbQuestions;    }

    /// <summary>
    /// Extract JSON from a notebook format and then create a file that can be converted by texttoqti - python code.
    /// This works in a particular format - since the questions exported are known
    /// </summary>
    private void ExtractQuestion(JsonNode node, StringBuilder sb)
    {
        int QuestionCount = 0;

        JsonArray qArray = FindQuestionsArray(node);
        if (qArray == null)
        {
            Console.WriteLine("No questions array found.");
            return;
        }

        int nQuestions = qArray.Count;
        Console.WriteLine("Number of questions: " + nQuestions);


        foreach (JsonNode itemNode in qArray)
        {
            if (!(itemNode is JsonObject item))
                continue;

            JsonObject questionItem = item;

            Console.WriteLine("Name: " + item.ToString());

            String question = GetString(questionItem, "question");
            String hint = GetString(questionItem, "hint");
            String mcqType = GetString(questionItem, "type");

            JsonArray questions = FindAnswerOptions(questionItem);

            QuestionCount++;

            sb.AppendLine("Title: Question " + QuestionCount);
            // TODO can make this a global from the UI
            sb.AppendLine("Points: 1");
            int qNumber = 1;
            sb.AppendLine(qNumber + ". " + question);

            // If more than one option is marked correct, indicate this is a multiple-answer question
            int correctCount = 0;
            if (questions != null)
            {
                foreach (var q in questions)
                {
                    if (q is JsonObject qq && GetBool(qq, "isCorrect"))
                        correctCount++;
                }
            }

            if (correctCount > 1 || mcqType.Equals("multiple_select", StringComparison.OrdinalIgnoreCase))
            {
                sb.AppendLine("[Select all that apply]");
            }

            if (!string.IsNullOrWhiteSpace(hint))
                sb.AppendLine("... " + hint);

            // Ascii here
            char c = 'a';

            if (questions != null)
            {
                foreach (var q in questions)
                {
                    if (q is JsonObject jObj)
                    {

                        Boolean isCorrect = GetBool(jObj, "isCorrect");
                        String rational = GetString(jObj, "rationale");
                        String optionText = GetString(jObj, "text");
                        String label = c + ") " + optionText;

                        if (mcqType.Equals("multiple_select", StringComparison.OrdinalIgnoreCase))
                        {
                            if (isCorrect)
                            {
                                sb.AppendLine("[*] " + label);
                            }
                            else
                            {
                                sb.AppendLine("[ ] " + label);
                            }

                            if (!string.IsNullOrWhiteSpace(rational))
                                sb.AppendLine("... " + rational);
                        }
                        else
                        {
                            if (isCorrect)
                            {
                                sb.AppendLine("*" + label);
                                if (!string.IsNullOrWhiteSpace(rational))
                                    sb.AppendLine("... " + rational);
                            }
                            else
                            {
                                sb.AppendLine(label);
                                if (!string.IsNullOrWhiteSpace(rational))
                                    sb.AppendLine(string.Format("... {0}", rational));
                            }
                        }

                        c++;
                    }
                }
            }

            if (GetBool(questionItem, "hasAllOfTheAbove"))
            {
                sb.AppendLine(c + ") All of the above");
                c++;
            }
            if (GetBool(questionItem, "hasNoneOfTheAbove"))
            {
                sb.AppendLine(c + ") None of the above");
            }

                //bool isCorrect = q["isCorrect"].toBoolean();
                //bool isCorrect = q["isCorrect"].toBoolean();

        }
    }

    private JsonArray FindQuestionsArray(JsonNode node)
    {
        if (node == null)
            return null;

        if (node is JsonArray arr)
        {
            if (LooksLikeQuestionsArray(arr))
                return arr;

            foreach (var child in arr)
            {
                var nested = FindQuestionsArray(child);
                if (nested != null)
                    return nested;
            }
            return null;
        }

        if (node is JsonObject obj)
        {
            string[] keys = { "questions", "items", "data", "results", "content" };
            foreach (var key in keys)
            {
                if (obj[key] is JsonArray a)
                    return a;
            }

            foreach (var prop in obj)
            {
                var nested = FindQuestionsArray(prop.Value);
                if (nested != null)
                    return nested;
            }
        }

        return null;
    }

    private JsonArray FindAnswerOptions(JsonObject questionItem)
    {
        if (questionItem["answerOptions"] is JsonArray answerOptions)
            return answerOptions;
        if (questionItem["options"] is JsonArray options)
            return options;
        if (questionItem["answers"] is JsonArray answers)
            return answers;
        return null;
    }

    private bool LooksLikeQuestionsArray(JsonArray array)
    {
        foreach (var item in array)
        {
            if (item is JsonObject obj && (obj["question"] != null || obj["answerOptions"] != null))
                return true;
        }
        return false;
    }

    private static String GetString(JsonObject obj, string key)
    {
        if (obj == null || obj[key] == null)
            return string.Empty;

        JsonNode value = obj[key];
        if (value is JsonValue jv)
        {
            if (jv.TryGetValue<string>(out var s))
                return s ?? string.Empty;
            return jv.ToString();
        }

        return value.ToString();
    }

    private static bool GetBool(JsonObject obj, string key)
    {
        if (obj == null || obj[key] == null)
            return false;

        JsonNode value = obj[key];
        if (value is JsonValue jv)
        {
            if (jv.TryGetValue<bool>(out var b))
                return b;
            if (jv.TryGetValue<string>(out var s) && bool.TryParse(s, out var parsed))
                return parsed;
            if (bool.TryParse(jv.ToString(), out parsed))
                return parsed;
        }
        else
        {
            if (bool.TryParse(value.ToString(), out var parsed))
                return parsed;
        }

        return false;
    }
    

    /// <summary>
    /// Recursively extracts all string values from a JSON node.
    /// </summary>
    private void ExtractText(JsonNode node, StringBuilder sb)
    {
        switch (node)
        {
            case JsonValue value:
                if (value.TryGetValue<string>(out var str))
                {
                    sb.AppendLine(str);
                }
                break;

            case JsonObject obj:
                foreach (var property in obj)
                {
                    ExtractText(property.Value, sb);
                }
                break;

            case JsonArray array:
                foreach (var item in array)
                {
                    ExtractText(item, sb);
                }
                break;
        }
    }
}
