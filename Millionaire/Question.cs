using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Millionaire
{
    struct Question
    {
        public string question;
        public string[] answers;
        public int correctAnswer;

        public Question(StreamReader reader)
        {
            question = reader.ReadLine().Replace("\\n", "\n");
            answers = new string[4];
            for (int i = 0; i < 4; i++)
            {
                answers[i] = reader.ReadLine().Replace("\\n", "\n");
            }
            correctAnswer = int.Parse(reader.ReadLine());
        }
    }
}
